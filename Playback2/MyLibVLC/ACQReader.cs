using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
namespace SeizurePlayback
{
    public class ACQReader
    {
        public string FullName;
        public Int32[][] data;
        public bool[] HideChan;
        public string[] ID;
        public int VisibleChans; 
        public int Chans;
        public int SelectedChan;
        public int SampleRate;
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
        private int Xmax,Ymax;
        private int Voltage;
        private int DataType;
        public Bitmap offscreen;
        public Bitmap ScreenCopy;
        private List<TimeSpan> SzTime;
        private List<int> SzChannel; 
        Graphics g;
        public ACQReader()
        {
            Zoom = 1; 
            Chans = new int();
            ID = new string[16];
            HideChan = new bool[16];            
            Voltage = 2000*1000;                        
            WavePen = new Pen(Color.Black);
            SelectedPen = new Pen(Color.Red);
            SelectedChan = -1;
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
            VoltageSpacing = (int)(Ymax / (Chans+.5));
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
            VoltageSpacing = (int)(Ymax / (Chans + .5));
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
        public bool ReadData(int TimeStart, int Length) // In seconds
        {
            long SeekPoint;
            data = new Int32[Chans][];
            SampleSize = SampleRate * Length;
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
        public void initDisplay(int X, int Y)
        {
            offscreen = new Bitmap(Math.Max(X,1),Math.Max(1,Y));
            Xmax = X;
            Ymax = Y;            
            g = Graphics.FromImage(offscreen);
            g.Clear(Color.White);
        }
        public void cleargraph()
        {
            g.Clear(Color.White);
        }
        public void ResetScale()
        {
            VoltageSpacing = (int)(Ymax / (VisibleChans + .5));
        }
        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {
            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);

            float m = (maxPixel - minPixel) / (65536);
            float b = 2^15;
            float result = ((m * volt) + b)*Zoom;
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

        public void FixChans(int BreakNum)
        {
            
            string Fname = FullName.Substring(0,FullName.Length-4) + "_F.acq";
            FileStream FOUT = new FileStream(Fname, FileMode.Create);
            BinaryWriter FOUT_ID = new BinaryWriter(FOUT);
            long EndPoint = DataStart + SampleRate * Chans * Position * 4;
            FILE.Seek(0, SeekOrigin.Begin);
            //Copy data up to the broken part            
            if (BreakNum > 0)
            {
                for (long i = 0; i < EndPoint; i++)
                {
                    FOUT_ID.Write(FID.ReadByte()); //Read a byte and write it
                }
                for (int i=0; i < BreakNum; i++)
                {
                    FOUT_ID.Write((Int32)0); //Write the two missing samples
                }

            }
            else
            {
                for (long i = 0; i < EndPoint+4*BreakNum; i++)
                {
                    FOUT_ID.Write(FID.ReadByte()); //Read a byte and write it
                }
                FILE.Seek(-4 * BreakNum, SeekOrigin.Current);
            }
            long EndPoint2 = FID.BaseStream.Length-FID.BaseStream.Position;
            for (long i = 0; i < EndPoint2; i++)
            {
               FOUT_ID.Write(FID.ReadByte());
            }
            FOUT_ID.Close();
            FOUT.Close();
            FILE.Close();
            FID.Close(); 
            if (File.Exists(FullName.Substring(0, FullName.Length - 4) + ".bak"))
            {
                File.Delete(FullName.Substring(0, FullName.Length - 4) + ".bak");
            }
            File.Move(FullName, FullName.Substring(0, FullName.Length - 4) + ".bak");
            File.Move(FullName.Substring(0, FullName.Length - 4) + "_F.acq", FullName);
            FILE = new FileStream(FullName, FileMode.Open, FileAccess.Read);
            FID = new BinaryReader(FILE);

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
            int NotDisp;
            PointF[][] WaveC;
            Font F = new Font("Arial", 10);
            SolidBrush B = new SolidBrush(Color.Red);
            try
            {
             g.Clear(Color.White);
             NotDisp = 0;
             
                 WaveC = new PointF[Chans][];
                 for (int i = 0; i < Chans; i++)
                 {
                     WaveC[i] = new PointF[SampleSize];
                 }
                 for (int j = 0; j < Chans; j++)
                 {

                     if (!HideChan[j])
                     {
                         if (HL && (SelectedChan == j))
                         {
                             SolidBrush myBrush = new SolidBrush(System.Drawing.Color.LightGreen);
                             g.FillRectangle(myBrush, new Rectangle((int)(HLS * PointSpacing * SampleRate), (int)(VoltageSpacing * (SelectedChan - NotDisp + 0.25F)), (int)((HLE - HLS) * PointSpacing * SampleRate), (Ymax / VisibleChans)));

                         }                         
                         g.DrawString(ID[j], F, B, new PointF(1, .25F + (j - NotDisp) * (Ymax / VisibleChans)));
                         for (int i = 0; i < SampleSize; i++)
                         {

                             PointF TempPoint = new PointF((float)i * PointSpacing, VoltageSpacing * ((j - NotDisp) + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(data[j][i]), Ymax / VisibleChans));
                             WaveC[j][i] = TempPoint;
                         }
                         if (j == SelectedChan)
                             g.DrawLines(SelectedPen, WaveC[j]);
                         else
                             g.DrawLines(WavePen, WaveC[j]);
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

    }
} 
