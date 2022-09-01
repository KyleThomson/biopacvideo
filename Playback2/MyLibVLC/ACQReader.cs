using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
namespace SeizurePlayback
{

    public class SeizureHighlight
    {
        public int Channel;
        public int tS;
        public int tE;
        public SeizureHighlight(int c, int s, int e)
        {
            Channel = c;
            tS = s;
            tE = e;
        }
    }


    public class ACQReader
    {
        public string FullName;
        public Int32[][] data;
        public bool[] HideChan;
        public string[] ID;
        public string[] ID2; //holds the alternative ID for the channels
        public bool ChanID = false;
        public int VisibleChans; 
        public int Chans;
        public int SelectedChan;
        public int SampleRate;
        public bool Randomized;
        public bool Telemetry; 
        public int[] RandomOrder;
        private BinaryReader FID;
        private FileStream FILE;
        private BinaryReader FID2;
        private FileStream FILE2;        
        private bool MultiFile; 
        private int ExtLenHeader;
        private int ChanLenHeader;
        long EOF;
        public float Zoom; 
        private int ForeignHeader;
        public int FileTime;
        public int TotFileTime;
        public int ExtFileTime;
        public int Position;
        private int DataStart;
        private int MaxDrawSize; 
        private float PointSpacing;
        private int DisplayLength;
        private int SampleSize;
        private bool HL;
        private int HLS, HLE;
        public bool Loaded;
        Pen WavePen, SelectedPen;
        private int VoltageSpacing;
        private int Xmax,Ymax, FRYmax;
        public float frepos = 60;
        private int Voltage;
        private int DataType;
        public Bitmap offscreen;
        public Bitmap ScreenCopy;
        public Bitmap frOffscreen;
        private List<TimeSpan> SzTime;
        private List<int> SzChannel; 
        Graphics g;
        Graphics frg;
        public int numPerPage = 24;
        public List<SeizureHighlight> SeizureHighlights;
        public int PosInSample;
        public int TotalSamples;
        public int ChanPass;
        public float[] CurrentChannelZoom;
        public bool MasterZoom = true;
        

        
        public ACQReader()
        {
            Zoom = 1; 
            Chans = new int();
            ID = new string[16];
            ID2 = new string[16];
            HideChan = new bool[16];            
            Voltage = 2000*1000;                        
            WavePen = new Pen(Color.Black);
            SelectedPen = new Pen(Color.Red);            
            SelectedChan = -1;
            RandomOrder = new int[16];
            Randomized = false;
            SeizureHighlights = new List<SeizureHighlight>();
            CurrentChannelZoom = new float[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
            
        }
        public void closeACQ()
        {
            FILE.Close();
            FID.Close();
        }
        public void openACQ(string FName)
        { //open File for reading
            byte CharN;
            FullName = FName;
            FILE = new FileStream(FName, FileMode.Open, FileAccess.Read);            
            FID = new BinaryReader(FILE);
            FILE.Seek(0, SeekOrigin.End);            
            EOF = FILE.Position;
            //Get header info            
            FILE.Seek(6, SeekOrigin.Begin);
            ExtLenHeader = FID.ReadInt32();                        
            Chans = (int)FID.ReadInt16();
            FILE.Seek(16, SeekOrigin.Begin);
            SampleRate = (int)(1000/FID.ReadDouble());            
            FILE.Seek(ExtLenHeader, SeekOrigin.Begin);
            ChanLenHeader =FID.ReadInt32();
            for (int i = 0; i < Chans; i++)
            {
                ID[i] = "";
                ID2[i] = ("Channel " + (i + 1));
                FILE.Seek(ExtLenHeader + ChanLenHeader * i + 6, SeekOrigin.Begin);
                CharN = FID.ReadByte();
                while (CharN != 0)
                {
                    ID[i] += (char)CharN;
                    CharN = FID.ReadByte();
                }
            }
            FILE.Seek(ExtLenHeader + (ChanLenHeader*Chans), SeekOrigin.Begin);       
     
            ForeignHeader = FID.ReadInt32();
            FILE.Seek(ForeignHeader + ExtLenHeader + (ChanLenHeader * Chans), SeekOrigin.Begin);
            DataType = 4; // FID.ReadInt16();
            DataStart = ForeignHeader + 4 * Chans + (ChanLenHeader *Chans) + ExtLenHeader;
            FileTime = (int)((FILE.Length - (long)DataStart) / (DataType * Chans * SampleRate));
            TotFileTime = FileTime;
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (Chans));
        }
        public void AppendACQ(string FName)
        {
            //We can make a bunch of assumptions, since this file is just an extension of the first
            FILE2 = new FileStream(FName, FileMode.Open, FileAccess.ReadWrite);
            FID2 = new BinaryReader(FILE2); 
            ExtFileTime = (int)((FILE2.Length - (long)DataStart) / (DataType * Chans * SampleRate));
            TotFileTime += ExtFileTime;
            MultiFile = true;
        }
        public void RefreshDisplay()
        {
            VoltageSpacing = (int)(Ymax / (Chans));
            PointSpacing = (float)Xmax / MaxDrawSize;
        }
        public void UpdateIDs()
        {
            BinaryWriter FID2 = new BinaryWriter(FILE);            
            for (int i = 0; i < Chans; i++)
            {                
                FILE.Seek(ExtLenHeader + ChanLenHeader * i + 6, SeekOrigin.Begin);
                for (int j = 0; j < ID[i].Length; j++)
                {
                    FID2.Write((char)ID[i][j]);
                }
                for (int j = ID[i].Length; j < 40; j++)
                {
                    FID2.Write((byte)0);
                }
            }
        }
        public bool DisplayDetection(int TimeStart, int Channel, int X, int Y, bool Display)
        {
                       
            PointF[] WaveC;
            float YPoint;
            Pen BoxPen = new Pen(Color.Red, 3);
            ReadData(TimeStart, 30);
            Font F = new Font("Arial", 10);
            
                    
                float YDraw;
                WaveC = new PointF[30*SampleRate];
                YDraw = (FRYmax / (numPerPage / 2)) * Y;                                                               
                for (int i = 0; i < SampleSize; i++)
                {
                    YPoint = ScaleVoltsToPixel(Convert.ToSingle(data[Channel - 1][i]), (FRYmax / (float)(numPerPage / 2))); // -(Ymax / (float)16) 
                    YPoint += (float)FRYmax / (float)frepos;
                    if (YPoint > FRYmax / (numPerPage/2)) YPoint = (FRYmax / (numPerPage / 2));        //Kyle's Mistake                                                                        
                    if (YPoint < 0) YPoint = 0;
                    PointF TempPoint = new PointF((float)(i + (X*SampleSize)) * PointSpacing, YDraw + YPoint);
                    WaveC[i] = TempPoint;
                }          
                frg.DrawLines(WavePen, WaveC);
                if (Display)
                {
                   frg.DrawRectangle(BoxPen, 3+(X*(Xmax/2)),3+YDraw, (Xmax / 2)-6, (FRYmax / (numPerPage / 2)) -6);
                }
                else
                 {
                    frg.DrawRectangle(WavePen, X * (Xmax / 2), YDraw, Xmax / 2, FRYmax / (numPerPage / 2));
                }
            /*}
            catch
            {
                return false; 
            }*/
            return true; 
        }
        public bool ReadData(int TimeStart, int Length) // In seconds
        {
            
            long SeekPoint;
            data = new Int32[Chans][];
            SampleSize = SampleRate * Length;
            PosInSample = TimeStart;
            TotalSamples = SampleRate * TotFileTime;

            for (int i = 0; i < Chans; i++)
            {
                data[i] = new Int32[SampleSize];
            }            

            SeekPoint = DataType*TimeStart * Chans * SampleRate + DataStart; 
            //Three cases - 
            //Data is all in first file                          
            if (((SeekPoint < FILE.Length) && (SeekPoint+(DataType*Length * Chans * SampleRate) <= FILE.Length)) || !MultiFile)
            {
                FILE.Seek(SeekPoint, SeekOrigin.Begin);                
                //Pull Data from file                
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE.Position >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;
                        return false;
                    }
                    if (DataType == 4)
                    {   
                        data[i % Chans][i / Chans] = FID.ReadInt32();
                    }
                    else
                    {
                        data[i % Chans][i / Chans] = (Int32)FID.ReadInt16();
                    }
                }                               
            }
            else if ((SeekPoint < FILE.Length) && (SeekPoint + (DataType * Length * Chans * SampleRate) > FILE.Length))
            //Data is partially in first, partially in second. 
            {
                int SP = 0;

                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE.Position + DataType - 1 >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;
                        SP = i;
                        break;
                    }
                    else
                    {
                        if (DataType == 4)
                        {
                            data[i % Chans][i / Chans] = FID.ReadInt32();
                        }
                        else
                        {
                            data[i % Chans][i / Chans] = (Int32)FID.ReadInt16();
                        }
                    }
                }
                FILE2.Seek(DataStart, SeekOrigin.Begin);
                for (int i = SP; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE2.Position >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;
                        return false;
                    }
                    if (DataType == 4)
                    {
                        data[i % Chans][i / Chans] = FID2.ReadInt32();
                    }
                    else
                    {
                        data[i % Chans][i / Chans] = (Int32)FID2.ReadInt16();
                    }
                }                
            }
            else            
            //Data is all in second file    
            {
                //Calculate amount of data in first file
                //This is really difficult
                SeekPoint = (SeekPoint - FILE.Length) + DataStart * 2; //I think this math works out
                FILE2.Seek(SeekPoint, SeekOrigin.Begin);

                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE2.Position >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;                        
                    }
                    if (DataType == 4)
                    {
                        data[i % Chans][i / Chans] = FID2.ReadInt32();
                    }
                    else
                    {
                        data[i % Chans][i / Chans] = (Int32)FID2.ReadInt16();
                    }
                }
            }
            return true;

        }
        public void SetDispLength(int DS)
        {
            DisplayLength = DS;
            MaxDrawSize = SampleRate * DisplayLength;
            PointSpacing = (float)Xmax / MaxDrawSize;
        }

        public void initDisplay(int X, int Y, int FRY)
        {
            offscreen = new Bitmap(Math.Max(X,1),Math.Max(1,Y));
            frOffscreen = new Bitmap(Math.Max(X, 1), Math.Max(1, FRY));
            Xmax = X;
            Ymax = Y;
            FRYmax = FRY;
            g = Graphics.FromImage(offscreen);
            frg = Graphics.FromImage(frOffscreen);
            frg.Clear(Color.White);
            
            g.Clear(Color.White);   
            
        }
        public void cleargraph()
        {
            g.Clear(Color.White);
            frg.Clear(Color.White);
        }

        public void ResetScale()
        {
            VoltageSpacing = (int)(Ymax / (Math.Max(VisibleChans, 1)));
        }
        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {
            float tempZoom;
            if (MasterZoom) tempZoom = Zoom;
            else tempZoom = Zoom * CurrentChannelZoom[ChanPass];
            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);
            float m;
            float b;
            if (Telemetry)
            {
                m = (maxPixel - minPixel) / (Int32.MaxValue / 2);
                b = 2 ^ 30;
            }
            else
            {
                m = (maxPixel - minPixel) / (65536);
                b = 2 ^ 15;
            }            
            float result = ((m * volt) * tempZoom) + b;
            //result = (result > maxPixel) ? maxPixel: result;
            //result = (result < minPixel) ? minPixel: result;
            return (result);
        }
        public Int32[] GetData(int Chan, int St, int Length)
        {
            Int32[][] InternalData;
            int SS = SampleRate* Length;            
            InternalData = new Int32[Chans][];     
            for (int i = 0; i < Chans; i++)
            {
                InternalData[i] = new Int32[SS];
            }            
            //This is where it gets hard
            int SeekPoint = DataType * St * Chans * SampleRate + DataStart;
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            if (DataType == 4)
            {
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    InternalData[i % Chans][i / Chans] = FID.ReadInt32();
                }                
                
            }
               return InternalData[Chan];
            }

        public void DumpData(string Fname, int Chan, int St, int Length)
        {
            int SeekPoint;
            FileStream FOUT = new FileStream(Fname, FileMode.Create);
            BinaryWriter FOUT_ID = new BinaryWriter(FOUT);            
            data = new Int32[Chans][];
            SampleSize = SampleRate * Length;
            for (int i = 0; i < Chans; i++)
            {
                data[i] = new Int32[SampleSize];
            }            
            //This is where it gets hard
            SeekPoint = DataType * St * Chans * SampleRate + DataStart;
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            if (DataType == 4)
            {
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    data[i % Chans][i / Chans] = FID.ReadInt32();
                }
                for (int j = 0; j < SampleSize; j++)
                {
                    FOUT_ID.Write(data[Chan][j]);
                }
            }
            else
            {
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    data[i % Chans][i / Chans] = (Int32)FID.ReadInt16();
                }
                for (int j = 0; j < SampleSize; j++)
                {
                    FOUT_ID.Write(data[Chan][j]);
                }
            }

            FOUT_ID.Close();
            FOUT.Close();
        }
        public void sethighlight(int Start, int End)
        {
            HLS = Start;
            HLE = End;
            HL = true;
        }
        public void EndHighlight()
        {
            HL = false;
        }
        public void AddSeizure(TimeSpan S, int T) 
        {
            //SzTimeStart.Add(S);
 
        }
        public Bitmap drawchan(int start, int end)
        {
            //Graphics f;
            //graphics temp; 
            //Bitmap B = new Bitmap(f);
            return null;

        }
        public void drawbuffer()
        {
            if (VisibleChans < 1) return;
            
            int YMoVC = Ymax / VisibleChans;
            //if (VisibleChans == 1) YMoVC = Ymax / 2;
            
            
            int NotDisp;
            PointF[][] WaveC;
            Font F = new Font("Arial", 10);
            SolidBrush B = new SolidBrush(Color.Red);
            try
            {
             g.Clear(Color.White);
             NotDisp = 0;
             float YDraw; 
             
                 WaveC = new PointF[Chans][];
                 for (int i = 0; i < Chans; i++)
                 {
                     WaveC[i] = new PointF[SampleSize];
                 }
                 for (int j = 0; j < Chans; j++)
                 {
                    ChanPass = j;

                     if (!HideChan[j])
                     {

                         if (Randomized)
                         {
                             YDraw = VoltageSpacing * (Array.FindIndex(RandomOrder, item => item ==  j));
                         }
                         else
                         {
                             YDraw = VoltageSpacing * (j - NotDisp);
                            if (ChanID)
                            {
                                g.DrawString(ID2[j], F, B, new PointF(1, .75F + (j - NotDisp) * (Ymax / VisibleChans)));
                            } else
                            {
                                g.DrawString(ID[j], F, B, new PointF(1, .75F + (j - NotDisp) * (Ymax / VisibleChans)));
                            }
                             
                         }
                         if (HL && (SelectedChan == j))
                         {
                             SolidBrush myBrush = new SolidBrush(System.Drawing.Color.LightGreen);
                             g.FillRectangle(myBrush, new Rectangle((int)(HLS * PointSpacing * SampleRate), (int)(YDraw + (VoltageSpacing * 0.25F)), (int)((HLE - HLS) * PointSpacing * SampleRate), (Ymax / VisibleChans)));

                         }
                        for (int k = 0; k < SeizureHighlights.Count; k++)
                        {
                            //if (j == SeizureHighlights[k].Channel && (PosInSample - (SeizureHighlights[k].tS * ) > -DisplayLength && Position - SeizureHighlights[k].tS < DisplayLength))
                            if (j == SeizureHighlights[k].Channel && (Math.Abs(SeizureHighlights[k].tS - Position) < (DisplayLength + SeizureHighlights[k].tE)))
                            {
                                //Console.WriteLine("PASS - Channel " + j + ", TIME: " + PosInSample);
                                {
                                    SolidBrush myBrush2 = new SolidBrush(System.Drawing.Color.LightGray);

                                    g.FillRectangle(myBrush2, new Rectangle((int)((SeizureHighlights[k].tS - PosInSample) * PointSpacing * SampleRate), (int)(YDraw + (VoltageSpacing * 0.25F)), (int)((SeizureHighlights[k].tE) * PointSpacing * SampleRate), (Ymax / VisibleChans)));
                                    //g.FillRectangle(myBrush2, new Rectangle((int)(SeizureHighlights[k].tS * PointSpacing * SampleRate), (int)(YDraw + (VoltageSpacing * 0.25F)), (int)((SeizureHighlights[k].tE) * PointSpacing * SampleRate), (Ymax / VisibleChans)));

                                }

                            }

                        }
                        for (int i = 0; i < SampleSize; i++)
                         {
                            
                             PointF TempPoint = new PointF((float)i * PointSpacing, YDraw + ScaleVoltsToPixel(Convert.ToSingle(data[j][i]), YMoVC));
                            //

                            if (VisibleChans == 1) TempPoint.Y += Ymax/2;
                             WaveC[j][i] = TempPoint;



                        
                            
                         }


                      




                                if (j == SelectedChan)
                             g.DrawLines(SelectedPen, WaveC[j]);
                         else
                             g.DrawLines(WavePen, WaveC[j]);

                        //Console.WriteLine(Position);


 


                    }
                     else
                     {
                         NotDisp++;
                     }
                 }
             }
             catch
             {
             }
            }
        public void Randomizer()
        {
            Random rnd = new Random();                      
            int[] TempHold = new int[Chans];
            int TotChan = -1; //Total Number of Channels displayed            
            int RChan;                     
            for (int i = 0; i < Chans; i++) 
            {
                if (!HideChan[i])
                {
                    TotChan++; //increment number of channels
                    TempHold[TotChan] = i; //Generate Temporary (Visible) Channel list. 
                }
            }
            for (int i = 0; i < TotChan+1; i++)
            {
                RChan = rnd.Next(0, TotChan - i); //Get Random Number the length of remaining channels
                RandomOrder[i] = TempHold[RChan]; //Take that channel from the array
                int TempPlace = 0; //Need to skip Channel that is taken
                for (int j = 0; j < TotChan - i; j++) //Rebuild Random order
                {
                    if (j == RChan) 
                    {
                        TempPlace++; //Skip the channel we took from the array
                    }
                    TempHold[j] = TempHold[TempPlace]; //Grab number 
                    TempPlace++; //Increment location regardless 
                }
            }
            for (int i = 0; i < TotChan+1; i++)
            {
                Console.WriteLine("C" + (i + 1).ToString() + " - " + (RandomOrder[i]+1).ToString());
            }
                
        }

        public void SwapIDs(object sender, EventArgs e, bool check)
        {

            if (check)
            {
                ChanID = true;
            }
            if (!check)
            {
                ChanID = false;
            }

            
            drawbuffer();

        }

        //public void ResetID()
        //{
        //    if (ID[0] == "Channel 1")
        //    {
        //        for (int i = 0; i < Chans; i++)
        //        {
        //            ID[i] = ID2[i];
        //        }
        //        for (int i = 0; i < Chans; i++)
        //        {
        //            ID2[i] = "Channel " + (i + 1);
        //        }
        //    }
        //}


    }

} 
