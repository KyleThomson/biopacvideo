using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.IO;


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
        public bool isrecording = false;
        public bool isconnected = false;        
        public bool[] FeederTest;
        private bool[] DigitalChannel;
        private int MaxDrawSize;
        Thread AcqThread = null;
        Thread FeederTestThread = null;        
        public String Filename;
        public String RecordingDirectory;
        private BinaryWriter BinaryFileID;
        private FileStream BinaryFile;
        public int SelectedChannel;
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
        int UpdateSpeed = 23;
        Pen wavePen;
        Pen TimePen;
        private uint last_received;
        private PointF[] WaveCords;
        private Graphics g;
        public Bitmap offscreen;
        public double[] rec_buffer;
        private double[] draw_buffer;
        private byte[] byte_buffer;
        public MPCODE MPReturn;
        public bool[] RecordAC = new bool[16];
        public int SampleRate;
        public int DisplayLength;
        private Single PointSpacing;
        public TimeSpan Buftime;
        private int samplecount;
        private long[] ChannelDataSizeLocation;
        private int AcqChan;        




        public MPTemplate() //Constructor
        {
            wavePen = new Pen(Color.Black);
            TimePen = new Pen(Color.Red);
            CurPointPos = 0;
            FeederTest = new bool[8];
            DigitalChannel = new bool[16];
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
            offscreen = new Bitmap((int)(X - 100), (int)(240));
            MaxDrawSize = SampleRate * DisplayLength;

            WaveCords = new PointF[MaxDrawSize];
        }



        int TotChan()
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
            BinaryFileID.Seek(0, SeekOrigin.Begin);
            //Extended Header
            BinaryFileID.Write((short)0); //nItemHeaderLen - Not used
            BinaryFileID.Write((Int32)45); //File Version
            BinaryFileID.Write((Int32)13104); //Extended Header Length
            BinaryFileID.Write((short)TotChan()); //Number of Channels 
            BinaryFileID.Write((short)0); //Horizontal Scale Type, Time in Seconds
            BinaryFileID.Write((short)SelectedChannel); //Currently Selected Channel
            BinaryFileID.Write((double)(1000 / SampleRate)); //Number of miliseconds per sample
            BinaryFileID.Write((double)0); //Current Time Offset
            BinaryFileID.Write((double)(DisplayLength * 1000)); //Time Scale in miliseconds per division
            BinaryFileID.Write((double)0); //Cursor 1 Position
            BinaryFileID.Write((double)0); //Cursor 2 Position
            BinaryFileID.Write((double)0); //RECT = size and position. Set to 0 for default.
            for (int i = 0; i < 6; i++) BinaryFileID.Write((short)0); //No measurements selected            
            BinaryFileID.Write((short)0); //Do Not Gray Selected WaveForms
            BinaryFileID.Write((double)0); //Initial Time Offset in Miliseconds
            BinaryFileID.Write((short)0); //Do Not Autoscale after transforms

            char[] C = new char[40];
            string S = "Seconds";
            for (int i = 0; i < S.Length; i++) C[i] = S[i];
            BinaryFileID.Write(C); //Horizontal Units Tet

            C = new char[10];
            S = "Sec";
            for (int i = 0; i < S.Length; i++) C[i] = S[i];
            BinaryFileID.Write(C); //Horizontal Units Text Abbreviated


            BinaryFileID.Write((short)0); //Keep File in Memory
            BinaryFileID.Write((short)1); //Enable grid Display
            BinaryFileID.Write((short)1); //Enable marker display
            BinaryFileID.Write((short)1); //Enable draft plotting
            BinaryFileID.Write((short)0); //Display Mode 0: Scope 1: Chart
            BinaryFileID.Write((short)0); //Reserved

            BinaryFileID.Write((short)0); // BShowToolBar
            BinaryFileID.Write((short)0); //BShowChannelButtons 
            BinaryFileID.Write((short)0); //BShowMeasurements
            BinaryFileID.Write((short)0); //BShowMarkers
            BinaryFileID.Write((short)0); //BShowJournal
            BinaryFileID.Write((short)SelectedChannel); //CurXChannel
            BinaryFileID.Write((short)0); //MmtPrecision
            BinaryFileID.Write((short)0); //Number of Measurement rows
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Functions
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Channels
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operand 1
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operand 2
            for (int i = 0; i < 40; i++) BinaryFileID.Write((short)0); //Measurement Calculation Operation
            for (int i = 0; i < 40; i++) BinaryFileID.Write((double)0); //Measurement Channels - Constant
            BinaryFileID.Write((Int32)0); //New grid with minor line
            BinaryFileID.Write((Int32)0); //COLORREF
            BinaryFileID.Write((Int32)0); //COLORREF
            BinaryFileID.Write((short)0); //Major Grid Style
            BinaryFileID.Write((short)0); //Minor Grid Style
            BinaryFileID.Write((short)0); //Major Grid Width
            BinaryFileID.Write((short)0); //Minor Grid Width
            BinaryFileID.Write((Int32)0); //Locked/Unlocked gridLines
            BinaryFileID.Write((Int32)0); //show Gridlines
            BinaryFileID.Write((double)0); // start point to draw grid
            for (int i = 0; i < 60; i++) BinaryFileID.Write((double)0); // Offset of Vertical Value per channel
            BinaryFileID.Write((double)0); // Horizontal Grid Spacing
            for (int i = 0; i < 60; i++) BinaryFileID.Write((double)0); //Vertical grid spacing per channel
            BinaryFileID.Write((Int32)0); //Enable Wavetools 
            //Get Current File Position, this is where we start writting data.
            BinaryFileID.Write((short)0); //digital precision fr units in Horizontal Axis
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
            for (int i = 0; i < 40; i++) BinaryFileID.Write((Int32)0);

            ChannelDataSizeLocation = new long[16];

            //Per Channel Data Section
            for (int Chanloop = 0; Chanloop < TotChan(); Chanloop++)
            {
                BinaryFileID.Write((Int32)262); //Channel Header Size
                BinaryFileID.Write((short)GetChan(Chanloop) + 1); //Number of Channel
                C = new char[40];
                BinaryFileID.Write(C); //Comment Text
                BinaryFileID.Write((Int32)0); //Color
                BinaryFileID.Write((short)0); //Display option
                BinaryFileID.Write((double)0); // Amplitude Offset (volts)
                BinaryFileID.Write((double)0); // Amplitude scale (volts/div)
                C = new char[20];
                BinaryFileID.Write(C); //Units Text
                ChannelDataSizeLocation[GetChan(Chanloop)] = BinaryFile.Position;
                BinaryFileID.Write((Int32)0); //Number of DataSamples 
                BinaryFileID.Write((double)0); //Units/count
                BinaryFileID.Write((double)0); //Units
                BinaryFileID.Write((short)Chanloop);  //Channel Order 
                BinaryFileID.Write((short)2); //Channel Partition Size
                //Version 3.0 and above
                BinaryFileID.Write((short)0); //PlotMode
                BinaryFileID.Write((double)0); // vMid
                //Version 3.7.0 and above
                C = new char[128];
                BinaryFileID.Write(C); //szDescription
                BinaryFileID.Write((short)0); //Channel Divider of main frequency
                //Version 3.7.3 
                BinaryFileID.Write((short)0); //digital of precision for units in  Vertical Axis for each channel
                //Version 3.8.2 
                BinaryFileID.Write((Int32)0); //Active Segment Color
                BinaryFileID.Write((Int32)0); // Active Segment Style
            }
            //Foreign Data Packet
            BinaryFileID.Write((short)4); //Total Length of Foreign Data Packet
            BinaryFileID.Write((short)0); //Foreign Data Packet Size

            for (int i = 0; i < TotChan(); i++)
            {
                BinaryFileID.Write((short)8); //Channel Data Size in Bytes (2 for int 16, 8 for double)
                BinaryFileID.Write((short)1); //Channel Data Type 1 = double, 2 = int
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

        private void writebuffer()
        {

            BinaryFile.Seek(CurrentWriteLoc, SeekOrigin.Begin);
            BinaryFileID.Write(byte_buffer);
            CurrentWriteLoc = BinaryFile.Position;          
        }

        private void drawbuffer()
        {
            PointF[] WaveC;
            while (true)
            {
                _DrawHandle.WaitOne();
                if (ClearDisplay)
                {
                    lock (g)
                        g.Clear(Color.White);
                    CurPointPos = 0;
                    ClearDisplay = false;
                }
                int SamplesLeft;
                if (samplesize + CurPointPos > MaxDrawSize)
                {
                    SamplesLeft = (samplesize + CurPointPos - MaxDrawSize);
                    g.Clear(Color.White);
                    CurPointPos = 0;
                }
                else
                {
                    SamplesLeft = samplesize;
                }
                WaveC = new PointF[SamplesLeft];
                int SamplePos = 0;
                for (int i = 0; i < last_received; i++)
                {                    
                    if (((i + 1) % AcqChan) == (SelectedChannel % AcqChan))
                    {
                        if (SamplesLeft < samplesize)
                        {
                            if (i / AcqChan >= samplesize - SamplesLeft)
                            {
                                PointF TempPoint = new PointF(CurPointPos * PointSpacing, ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax));
                                WaveC[SamplePos] = TempPoint;
                                SamplePos++;
                                CurPointPos++;
                            }
                        }
                        else
                        {
                            PointF TempPoint = new PointF(CurPointPos * PointSpacing, ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax));
                            WaveC[SamplePos] = TempPoint;
                            SamplePos++;
                            CurPointPos++;
                        }
                    }
                }
                if (SamplePos > 2)
                {
                    lock (g)
                        g.DrawLines(wavePen, WaveC);
                    _DisplayHandle.Set();
              
                }
            }
        }

        private void updateheader()
        {
            for (int i = 0; i < TotChan(); i++)
            {
                BinaryFile.Seek(ChannelDataSizeLocation[GetChan(i)], SeekOrigin.Begin);
                BinaryFileID.Write((Int32)samplecount);
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
            isrecording = true;
            g = Graphics.FromImage(offscreen);
            ClearDisplay = true;
            Ymax = Convert.ToSingle(g.VisibleClipBounds.Height);
            Xmax = Convert.ToSingle(g.VisibleClipBounds.Width);         
            PointSpacing = Convert.ToSingle(Xmax / MaxDrawSize);         
            samplecount = 0;
            MPReturn = MPCLASS.setDigitalAcqChannels(DigitalChannel);
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
            writeheader();
            IsFileWriting = true;
            return true;
        }
        public void StopWriting()
        {
            isrecording = false;
            while (AcqThread.IsAlive)
            {
                //Pause to wait for thread to close
            }
            updateheader();
            BinaryFile.Close();
            AcqThread = new Thread(new ThreadStart(RecordingThread)); //Initialize recording thread
            AcqThread.Start();
        }



/******************************************************************************
 * 
 *                  RECORDING THREAD!
 *                  
 * ****************************************************************************/

        private void RecordingThread()
        {
            uint received;         
            AcqChan = TotChan();            
            Thread BuffDraw = new Thread(new ThreadStart(drawbuffer));
            BuffDraw.Start();
            BuffSize = ((uint)(SampleRate/UpdateSpeed))*(uint)AcqChan;
            rec_buffer = new double[BuffSize];
            draw_buffer = new double[BuffSize];
            byte_buffer = new byte[BuffSize * 8];                
            MPReturn = MPCLASS.startAcquisition();            
            if (MPReturn != MPCODE.MPSUCCESS)
                return;
            while (isrecording)
            {                                
                MPReturn = MPCLASS.receiveMPData(rec_buffer, BuffSize, out received);                                
                if (MPReturn != MPCODE.MPSUCCESS)
                {
                    MessageBox.Show(MPCLASS.getMPDaemonLastError().ToString());
                    return;
                }
                if (IsFileWriting)
                {
                    Buffer.BlockCopy(rec_buffer, 0, byte_buffer, 0, (int)received * 8);
                    BinaryFile.Seek(CurrentWriteLoc, SeekOrigin.Begin);
                    BinaryFileID.Write(byte_buffer);
                    CurrentWriteLoc = BinaryFile.Position;
                }
                last_received = received;
                samplesize = (int)last_received / AcqChan;
                samplecount += samplesize;
                //Handle Feeding ()
                Buffer.BlockCopy(rec_buffer, 0, draw_buffer, 0, (int)received * 8);                        
                _DrawHandle.Set();                                       
                
            }
            return;
        }

        public void StopRecording()
        {
            isrecording = false;
            while (AcqThread.IsAlive)
            {
                //Pause to wait for thread to close
            }            
            updateheader();
            MPReturn = MPCLASS.stopAcquisition();
            BinaryFile.Close();
        }

        /**************************************************************
         * 
         *      Feeder Fucnctions
         *      
         * ************************************************************/
        private void FeederTestThreadDefinition()
        {
            for (int i = 0; i < 8; i++)
            {
                FeederTest[i] = false;
            }
            for (uint i = 0; i < 2; i++)
            {
                MPCLASS.setDigitalIO(i, true, true, MPCLASS.DIGITALOPT.SET_LOW_BITS);
                Thread.Sleep(1000);
                MPCLASS.setDigitalIO(i, false, true, MPCLASS.DIGITALOPT.SET_LOW_BITS);                
            }            
        }
        public void RunFeederTest()
        {
            FeederTestThread = new Thread(new ThreadStart(FeederTestThreadDefinition));
            FeederTestThread.Start();
            while (FeederTestThread.IsAlive) { };
        }
    }
}