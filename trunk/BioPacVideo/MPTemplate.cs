﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Xml;

using MPCLASS = Biopac.API.MPDevice.MPDevImports;
using MPCODE = Biopac.API.MPDevice.MPDevImports.MPRETURNCODE;


namespace BioPacVideo
{

    class MPTemplate
    {

         static readonly MPTemplate instance=new MPTemplate();

        public static string[] MPRET = new string[] {"NULL", "SUCCESS", "DRIVERERROR", "DLLBUSY","INVALIDPARAM", "MP_NOT_CONNECTED","MPREADY","PRETRIGGER",
    "TRIGGER","BUSY","NOACTIVECHANNELS","COMERROR", "INVTYPE", "NO_NETWORK_CONNECT", "OVERWROTESAMPLES","MEMERROR",
    "SOCKETERROR","UNDERFLOW","PRESETERROR","XMLERROR" };
        static EventWaitHandle _DrawHandle = new AutoResetEvent(false);
        public EventWaitHandle _DisplayHandle = new AutoResetEvent(false);
        public bool isstreaming = false;
        public bool isconnected = false;        
        public bool[] FeederTest;
        private bool[] DigitalChannel;
        private int MaxDrawSize;
        Thread AcqThread = null;
        FeederTemplate Feeder;     
        public String Filename;
        public String RecordingDirectory;
        private BinaryWriter BinaryFileID;
        private FileStream BinaryFile;
        public int SelectedChannel;
        public int Gain;
        public bool Enabled;
        public bool IsFileWriting;
        public int samplesize;
        public bool ClearDisplay;
        private float Xmax;
        private float Ymax;
        public int FileCount;
        public int Voltage;
        private long CurrentWriteLoc;
        uint BuffSize;



        //GRAPHICS VARIABLES
        int CurPointPos;
        int UpdateSpeed = 30;
        Pen wavePen;
        Pen TimePen;
        private uint last_received;
        private PointF[] WaveCords;
        private Graphics g;
        public Bitmap offscreen;
        public double[] rec_buffer;
        private double[] draw_buffer;        
        public MPCODE MPReturn;
        public bool[] RecordAC = new bool[17];
        public int SampleRate;
        public int DisplayLength;
        private Single PointSpacing;         
        private int samplecount;
        private long[] ChannelDataSizeLocation;
        private int AcqChan;
        private int VoltageSpacing;




        public MPTemplate() //Constructor
        {
            wavePen = new Pen(Color.Black);
            TimePen = new Pen(Color.Red);
            Feeder = FeederTemplate.Instance;
            RecordAC[16] = false;
            CurPointPos = 0;
            FeederTest = new bool[8];
            DigitalChannel = new bool[16];
            wavePen = new Pen(Color.Black);
            for (int i = 0; i < 16; i++)
            {
                DigitalChannel[i] = false;
            }
        }

        public static MPTemplate Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitializeDisplay(int X, int Y)
        {
            offscreen = new Bitmap((int)(X - 60), Y-350);
            MaxDrawSize = SampleRate * DisplayLength;

            WaveCords = new PointF[MaxDrawSize];
        }

        public void ResetDisplaySize()
        {
            MaxDrawSize = SampleRate * DisplayLength;
            WaveCords = new PointF[MaxDrawSize];
            PointSpacing = Convert.ToSingle(Xmax / MaxDrawSize);                     
        }

        public int TotChan()
        {
            int Tot = 0;
            for (int i = 0; i < 16; i++)
            {
                if (RecordAC[i]) Tot++;
            }
            return Tot;
        }
        private int GetChan(int ChanNum)
        {
            int CurChan = 0;
            int ChanTot = 0;
            while (true)
            {
                if (RecordAC[CurChan] && ChanNum == ChanTot)
                {
                    return (CurChan);
                }
                else if (RecordAC[CurChan])
                {
                    ChanTot++;
                }
                CurChan++;


            }

        }


        private void open_ACQ_file()
        {
            string FName = Filename + string.Format("-{0:000}.acq", FileCount);
            while (File.Exists(FName))
            {
                FileCount++;
                FName = Filename + string.Format("-{0:000}.acq", FileCount);
            }
            BinaryFile = new FileStream(FName, FileMode.CreateNew);
            BinaryFileID = new BinaryWriter(BinaryFile);
        }

        private void writeheader()
        {
            //All of this comes from the BioPac Spec Document
            BinaryFileID.Seek(0, SeekOrigin.Begin);
            //Extended Header
            BinaryFileID.Write((short)4660); //nItemHeaderLen - Not used
            BinaryFileID.Write((Int32)38); //File Version
            BinaryFileID.Write((Int32)1894); //Extended Header Length
            BinaryFileID.Write((short)TotChan()); //Number of Channels 
            BinaryFileID.Write((short)0); //Horizontal Scale Type, Time in Seconds
            BinaryFileID.Write((short)SelectedChannel); //Currently Selected Channel
            BinaryFileID.Write((double)(1000 / SampleRate)); //Number of miliseconds per sample
            BinaryFileID.Write((double)0); //Current Time Offset
            BinaryFileID.Write((double)2000); //Time Scale in miliseconds per division
            BinaryFileID.Write((double)0); //Cursor 1 Position
            BinaryFileID.Write((double)0); //Cursor 2 Position
            BinaryFileID.Write((double)0); //RECT = size and position. Set to 0 for default.
            for (int i = 0; i < 6; i++) BinaryFileID.Write((short)0); //No measurements selected            
            BinaryFileID.Write((short)0); //Do Not Gray Selected WaveForms
            BinaryFileID.Write((double)0); //Initial Time Offset in Miliseconds
            BinaryFileID.Write((short)0); //Do Not Autoscale after transforms

            byte[] C = new byte[40];            
            BinaryFileID.Write(C); //Horizontal Units Tet

            C = new byte[10];      
            BinaryFileID.Write(C); //Horizontal Units Text Abbreviated


            BinaryFileID.Write((short)0); //Keep File in Memory
            BinaryFileID.Write((short)1); //Enable grid Display
            BinaryFileID.Write((short)1); //Enable marker display
            BinaryFileID.Write((short)0); //Enable draft plotting
            BinaryFileID.Write((short)1); //Display Mode 0: Scope 1: bytet
            BinaryFileID.Write((short)1); //Reserved

            BinaryFileID.Write((short)1); // BShowToolBar
            BinaryFileID.Write((short)1); //BShowChannelButtons 
            BinaryFileID.Write((short)1); //BShowMeasurements
            BinaryFileID.Write((short)1); //BShowMarkers
            BinaryFileID.Write((short)0); //BShowJournal
            BinaryFileID.Write((short)SelectedChannel); //CurXChannel
            BinaryFileID.Write((short)5); //MmtPrecision
            BinaryFileID.Write((short)1); //Number of Measurement rows
            BinaryFileID.Write((short)15);
            BinaryFileID.Write((short)16);
            BinaryFileID.Write((short)17);
            BinaryFileID.Write((short)18);
            BinaryFileID.Write((short)5);
            for (int i = 0; i < 35; i++) BinaryFileID.Write((short)1); //Measurement Functions
            for (int i = 0; i < 40; i++) BinaryFileID.Write((ushort)0xFFFF); //Measurement Channels
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operand 1
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operand 2
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operation
            for (int i = 0; i < 40; i++) BinaryFileID.Write((double)0); //Measurement Channels - Constant
            BinaryFileID.Write((Int32)1); //New grid with minor line
            BinaryFileID.Write((Int32)9474192); //COLORREF
            BinaryFileID.Write((Int32)14671839); //COLORREF
            BinaryFileID.Write((short)0); //Major Grid Style
            BinaryFileID.Write((short)0); //Minor Grid Style
            BinaryFileID.Write((short)1); //Major Grid Width
            BinaryFileID.Write((short)1); //Minor Grid Width
            BinaryFileID.Write((Int32)0); //Locked/Unlocked gridLines
            BinaryFileID.Write((Int32)0); //show Gridlines
            BinaryFileID.Write((double)0); // start point to draw grid
            for (int i = 0; i < 60; i++) BinaryFileID.Write((double)0); // Offset of Vertical Value per channel
            BinaryFileID.Write((double)4); // Horizontal Grid Spacing
            for (int i = 0; i < 60; i++) BinaryFileID.Write((double)4); //Vertical grid spacing per channel
            BinaryFileID.Write((Int32)0); //Enable Wavetools          
            //Verision 3.7.3 and above
            /*BinaryFileID.Write((short)0); //digital precision fr units in Horizontal Axis
            //Version 3.8.1 and above
            for (int i = 0; i < 20; i++) BinaryFileID.Write((byte)0); //Reserved
            BinaryFileID.Write((Int32)0); //Overlap Mode
            BinaryFileID.Write((Int32)0); //Hardware visibility
            BinaryFileID.Write((Int32)0); //Autplot during acq.
            BinaryFileID.Write((Int32)0); // AutoScroll during acq.
            BinaryFileID.Write((Int32)0); //Startbutton Visiblity 
            BinaryFileID.Write((Int32)0); //This file is compressed
            BinaryFileID.Write((Int32)0); //Alway show start button            
            //Verion 3.8.2 and above
            for (int i = 0; i < 260; i++) BinaryFileID.Write((byte)0); //Path to video
            BinaryFileID.Write((Int32)0); //Option: Use sync delay
            BinaryFileID.Write((double)0); //value of sync delay in ms
            BinaryFileID.Write((Int32)0); //Option: Paste measurements to journal
            BinaryFileID.Write((Int32)0); // Type of Graph
            for (int i = 0; i < (256 * 40); i++) BinaryFileID.Write((byte)0);
            for (int i = 0; i < 40; i++) BinaryFileID.Write((Int32)0);
            for (int i = 0; i < 40; i++) BinaryFileID.Write((Int32)0);
            for (int i = 0; i < 40; i++) BinaryFileID.Write((Int32)0);
            for (int i = 0; i < 40; i++) BinaryFileID.Write((Int32)0);*/                
            ChannelDataSizeLocation = new long[16];            
            //Per Channel Data Section
            for (int Chanloop = 0; Chanloop < TotChan(); Chanloop++)
            {                
                BinaryFileID.Write((Int32)252); //Channel Header Size                
                BinaryFileID.Write((short)(GetChan(Chanloop)+1)); //Number of Channel                
                C = new byte[40];                
                /*C[0] = (byte)'E'; C[1] = (byte)'E'; C[2] = (byte)'G';
                C[3] = (byte)'1'; C[4] = (byte)'0'; C[5] = (byte)'0'; C[6] = (byte)'C';*/
                for (int i = 0; i < Feeder.Rats[Chanloop].ID.Length; i++)
                {
                    C[i] = (byte)Feeder.Rats[Chanloop].ID[i];
                }                
                BinaryFileID.Write(C); //Comment Text                           
                //RGB
                BinaryFileID.Write((Int32)255);
                BinaryFileID.Write((short)2); //Display option
                BinaryFileID.Write((double)0); // Amplitude Offset (volts)
                BinaryFileID.Write((double)0.25); // Amplitude scale (volts/div)
                C = new byte[20];
                C[0] = (byte)'m'; C[1] = (byte)'V';
                BinaryFileID.Write(C); //Units Text
                ChannelDataSizeLocation[GetChan(Chanloop)] = BinaryFile.Position;
                BinaryFileID.Write((Int32)0); //Number of DataSamples 
                BinaryFileID.Write((double)1.52587890625e-005); //Units/count
                BinaryFileID.Write((double)0); //Units
                BinaryFileID.Write((short)(Chanloop+1));  //Channel Order 
                BinaryFileID.Write((short)1092); //Channel Partition Size
                //Version 3.0 and above
                BinaryFileID.Write((short)0); //PlotMode
                BinaryFileID.Write((double)0); // vMid
                //Version 3.7.0 and above
                C = new byte[128];
                BinaryFileID.Write(C); //szDescription
                BinaryFileID.Write((short)1); //Channel Divider of main frequency
                /*//Version 3.7.3 
                BinaryFileID.Write((short)0); //digital of precision for units in  Vertical Axis for each channel
                //Version 3.8.2 
                BinaryFileID.Write((Int32)0); //Active Segment Color
                BinaryFileID.Write((Int32)0); // Active Segment Style*/                
            }
            //Foreign Data Packet
            BinaryFileID.Write((short)8); //Total Length of Foreign Data Packet
            BinaryFileID.Write((short)0); //Foreign Data Packet Size
            BinaryFileID.Write((short)0); //Empty
            BinaryFileID.Write((short)0); //Empty

            for (int i = 0; i < TotChan(); i++)
            {
                BinaryFileID.Write((short)4); //Channel Data Size in Bytes (2 for int 16, 8 for double, 4 for int32)
                BinaryFileID.Write((short)3); //Channel Data Type 1 = double, 2 = int16, 3 = int32
            }
            CurrentWriteLoc = BinaryFile.Position;
        }



        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {
            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);

            float m = 1000* (maxPixel - minPixel) / (2 * (Voltage));
            float b = maxPixel - (m * Voltage/1000);
            float result = (m * volt) + b;
            //result = (result > maxPixel) ? maxPixel: result;
            //result = (result < minPixel) ? minPixel: result;
            return (result);
        }

        private void GenerateXML()
        {
        }


  
        private void drawbuffer()
        {
            PointF[][] WaveC;
            Font F = new Font("Arial",10); 
            SolidBrush B = new SolidBrush(Color.Red);
            while (true)
            {
                _DrawHandle.WaitOne();
                if (ClearDisplay)
                {
                    lock (g)
                        g.Clear(Color.White);
                    for (int i = 0; i < AcqChan; i++)
                        g.DrawString(Feeder.Rats[i].ID, F, B, new PointF(1, (i+.25F) * (Ymax / AcqChan)));
                    CurPointPos = 0;
                    ClearDisplay = false;
                }
                int SamplesLeft;
                if (samplesize + CurPointPos > MaxDrawSize)
                {
                    SamplesLeft = (samplesize + CurPointPos - MaxDrawSize);
                    g.Clear(Color.White);
                    for (int i = 0; i < AcqChan; i++)
                        g.DrawString(Feeder.Rats[i].ID, F, B, new PointF(1, (i+.25F) * (Ymax / AcqChan)));
                    CurPointPos = 0;
                }
                else
                {
                    SamplesLeft = samplesize;
                }
                WaveC = new PointF[AcqChan][];
                for (int i = 0; i < AcqChan; i++)
                {
                    WaveC[i] = new PointF[SamplesLeft];
                }
                int SamplePos = 0;
                for (int i = 0; i < last_received; i++)
                {                                       
                    
                        if (SamplesLeft < samplesize)
                        {
                            if (i / AcqChan >= samplesize - SamplesLeft)
                            {
                                PointF TempPoint = new PointF(CurPointPos * PointSpacing, VoltageSpacing * ((i % AcqChan) + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax/(AcqChan)));
                                WaveC[i % AcqChan][SamplePos] = TempPoint;
                                if (i % AcqChan == AcqChan - 1)
                                {
                                    SamplePos++;
                                    CurPointPos++;
                                }
                            }
                        }
                        else
                        {
                            PointF TempPoint = new PointF(CurPointPos * PointSpacing, VoltageSpacing * ((i % AcqChan) + (float)0.5) +  ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax/(AcqChan)));
                            WaveC[i % AcqChan][SamplePos] = TempPoint;
                            if (i % AcqChan == AcqChan-1) 
                            {
                                SamplePos++;
                                CurPointPos++;
                            }
                        }                    
                }
                if (SamplePos > 2)
                {
                    lock (g)
                        for (int i = 0; i < AcqChan; i++)
                            g.DrawLines(wavePen, WaveC[i]);
                    _DisplayHandle.Set();
              
                }
            }
        }

        private void updateheader()
        {
            for (int i = 0; i < TotChan(); i++)
            {
                BinaryFile.Seek(ChannelDataSizeLocation[GetChan(i)], SeekOrigin.Begin);
                BinaryFileID.Write((Int32)(samplecount));   
            }
            //Add write foreign data crap here
        }






        /**********************************************************************
         * 
         *  Connection/Recording functions
         *  
         ***********************************************************************/
        public bool Connect()
        {
            //connect to the MP Device
            this.MPReturn = MPCLASS.connectMPDev(MPCLASS.MPTYPE.MP150, MPCLASS.MPCOMTYPE.MPUDP, "auto");

            if (this.MPReturn != MPCODE.MPSUCCESS)
            {
                return false;
            }
            return true;
        } 

        public bool Disconnect()
        {
            this.MPReturn = MPCLASS.disconnectMPDev();
            if (this.MPReturn != MPCODE.MPSUCCESS)
                return true;
            else
                return false;
        }
      
        public bool StartRecording()
        {
            AcqThread = new Thread(new ThreadStart(RecordingThread)); //Initialize recording thread
            AcqThread.Priority = ThreadPriority.Highest;
            isstreaming = true;
            g = Graphics.FromImage(offscreen);
            ClearDisplay = true;
            Ymax = Convert.ToSingle(g.VisibleClipBounds.Height);
            Xmax = Convert.ToSingle(g.VisibleClipBounds.Width);         
            PointSpacing = Convert.ToSingle(Xmax / MaxDrawSize);                     
            MPReturn = MPCLASS.setDigitalAcqChannels(DigitalChannel);
            if (this.MPReturn != MPCODE.MPSUCCESS)
                return false;
            MPReturn = MPCLASS.setSampleRate(1000 / SampleRate);
            if (this.MPReturn != MPCODE.MPSUCCESS)
                return false;

            this.MPReturn = MPCLASS.setAcqChannels(this.RecordAC);

            if (this.MPReturn != MPCODE.MPSUCCESS)
                return false;

            this.MPReturn = MPCLASS.startMPAcqDaemon();

            if (MPReturn != MPCODE.MPSUCCESS)
                return false;           

            AcqThread.Start();
            return true;
        }

        public bool StartWriting()
        {
            open_ACQ_file();
            samplecount = 0;
            writeheader();
            IsFileWriting = true;
            return true;
        }
        public void StopWriting()
        {            
            updateheader();
            BinaryFile.Close();
            IsFileWriting = false;
            isstreaming = true;         
        }



/******************************************************************************
 * 
 *                  RECORDING THREAD!
 *                  
 * ****************************************************************************/

        private void RecordingThread()
        {
            uint received;         
            DateTime Start = DateTime.Now;
            TimeSpan Elapsed;
            AcqChan = TotChan();  //How many channels are we going to acquire   
            VoltageSpacing =(int)(Ymax / (AcqChan+1));
            Thread BuffDraw = new Thread(new ThreadStart(drawbuffer)); //Set up thread for buffering 
            BuffDraw.Start(); //and then start it 
            BuffSize = ((uint)(SampleRate/UpdateSpeed))*(uint)AcqChan; //Need to determine the right buffer size.
            rec_buffer = new double[BuffSize]; //Place to copy the buffer of recieved data 
            draw_buffer = new double[BuffSize]; //Place to copy the buffer to, for drawing 
            //Need that so we can do thread-safe operations. 
            Int32 transbuffer = new Int32();  //Translational buffer to write bytes instead of doubles.             
            MPReturn = MPCLASS.startAcquisition();  //Start actual acquisition        
            if (MPReturn != MPCODE.MPSUCCESS) //If acquisition fails, error out. 
            { 
            }
            while (isstreaming) //Thread stopping variable - set to false to end the recording thread. 
            {                
                Elapsed = DateTime.Now - Start;
                //Console.WriteLine("Time Elapsed: {0} ms",Elapsed.Milliseconds);
                
                MPReturn = MPCLASS.receiveMPData(rec_buffer, BuffSize, out received); //Get the latest buffer
                //This function pauses until the buffer is full - making the 'recieved' variable somewhat useless.                 
                //If this fails, the recieved will be smaller than the buffsize, but you have bigger issues. 
                Start = DateTime.Now;
                if (MPReturn != MPCODE.MPSUCCESS) //Make sure we were successful in getting the buffer. 
                {
                    MessageBox.Show(MPReturn.ToString() + "   " + MPCLASS.getMPDaemonLastError().ToString());
                    MPReturn = MPCLASS.stopAcquisition(); //Have to restart the aquisition. 
                    return;
                }
                if (IsFileWriting) //If we are writing to the file, we want to handle it immediately. 
                {

                    BinaryFile.Seek(CurrentWriteLoc, SeekOrigin.Begin); //Make sure the File writting is in the right place
                    for (int j = 0; j < BuffSize; j++) //Cycle through the buffer. 
                    {
                        rec_buffer[j] = Math.Min(rec_buffer[j], (double)Int32.MaxValue/Gain); //Make sure we don't exceed the maxes
                        rec_buffer[j] = Math.Max(rec_buffer[j], (double)Int32.MinValue/Gain);//Make sure we don't exceed the minmum
                        transbuffer = Convert.ToInt32(rec_buffer[j] * Gain); //Convert the value
                        BinaryFileID.Write((Int32)transbuffer); //Write the bytes to the file. The int16 probably isn't necessary to cast, 
                        //better safe than sorry. 
                    }                    
                    CurrentWriteLoc = BinaryFile.Position;  //Update the current location.
                }
                /*bool a,b;
                MPCLASS.getDigitalIO(5, out a, MPCLASS.DIGITALOPT.READ_LOW_BITS);
                MPCLASS.getDigitalIO(6, out b, MPCLASS.DIGITALOPT.READ_LOW_BITS);
                if (a && b)
                {
                    Feeder.State = 3;
                    Feeder.StateText = "READY";
                }
                if (!a && b)
                {
                    Feeder.State = 1;
                    Feeder.StateText = "RECEIVING";
                }
                if (a && !b)
                {
                    Feeder.State = 2;
                    Feeder.StateText = "EXECUTING";
                }
                if (!a && !b)
                {
                    Feeder.State = 0;
                    Feeder.StateText = "ERROR";
                }*/
                if ((Feeder.CommandSize > 0) && Feeder.CommandReady && (Feeder.gap == 0))
                { 
                    byte v;                    
                    v = Feeder.GetTopCommand();
                    Feeder.gap++;
                    for (byte k = 0; k < 5; k++)                       
                    {
                        bool x =!((v&(1 << k)) > 0);                               
                        MPCLASS.setDigitalIO((uint)k, x, true, MPCLASS.DIGITALOPT.SET_LOW_BITS);
                    }
                    MPCLASS.setDigitalIO(7, true, true, MPCLASS.DIGITALOPT.SET_LOW_BITS);
                    Thread.Sleep(1);
                    MPCLASS.setDigitalIO(7, false, true, MPCLASS.DIGITALOPT.SET_LOW_BITS);                    
                }
                else if (Feeder.gap > 0)
                {
                    Feeder.gap++;
                    if (Feeder.gap == 4) { Feeder.gap = 0; }
                }
                last_received = received;
                samplesize = (int)last_received / AcqChan;
                samplecount += samplesize;
                //Handle Feeding ()
                Buffer.BlockCopy(rec_buffer, 0, draw_buffer, 0, (int)received * 8);                        
                _DrawHandle.Set();                                       
                
            }
            BuffDraw.Abort();            
            MPReturn = MPCLASS.stopAcquisition();  //We won't get here unless the thread stops. 
            return;
        }
        public void StopRecording()
            {
                isstreaming = false;
            while (AcqThread.IsAlive)
            {
                //Pause to wait for thread to close
            }                     
                       
        }
    
    }
}  