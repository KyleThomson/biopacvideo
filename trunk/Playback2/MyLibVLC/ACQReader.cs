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
        private int ExtLenHeader;
        private int ChanLenHeader;
        long EOF;
        public float Zoom; 
        private int ForeignHeader;
        public int FileTime;
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
            FILE = new FileStream(FullName, FileMode.Open, FileAccess.ReadWrite);            
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
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (Chans+.5));
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
            //Seek to data point, 2 because they are 2 bytes each, dumbass.
            SeekPoint = DataType*TimeStart * Chans * SampleRate + DataStart; 
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            if (DataType == 4)
            {
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE.Position >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;
                        return false;
                    }
                    data[i % Chans][i / Chans] = FID.ReadInt32();
                }
            }
            else
            {
                for (int i = 0; i < Length * Chans * SampleRate; i++)
                {
                    if (FILE.Position >= EOF)
                    {
                        SampleSize = (i - 1) / Chans;
                        return false;
                    }
                    data[i % Chans][i / Chans] = (Int32)FID.ReadInt16();
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
            //Seek to data point, 2 because they are 2 bytes each, dumbass.
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
