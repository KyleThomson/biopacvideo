using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
namespace SeizurePlayback
{
    class ACQReader
    {
        public string FullName;
        public Int16[][] data;        
        public int Chans;
        public int SelectedChan;
        public int SampleRate;
        private BinaryReader FID;
        private FileStream FILE;
        private int ExtLenHeader;
        private int ChanLenHeader;
        long EOF;
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
        public Bitmap offscreen;
        
        Graphics g;
        public ACQReader()
        {
            Chans = new int();            
            SampleRate = 500;            
            Voltage = 2000*1000;                        
            WavePen = new Pen(Color.Black);
            SelectedPen = new Pen(Color.Red);
            SelectedChan = -1;
        }

        public void openACQ(string FName)
        { //open File for reading
            FullName = FName;
            FILE = new FileStream(FullName, FileMode.Open);            
            FID = new BinaryReader(FILE);
            FILE.Seek(0, SeekOrigin.End);
            EOF = FILE.Position;
            //Get header info            
            FILE.Seek(6, SeekOrigin.Begin);
            ExtLenHeader = FID.ReadInt32();            
            Chans = (int)FID.ReadInt16();                      
            FILE.Seek(ExtLenHeader, SeekOrigin.Begin);
            ChanLenHeader =FID.ReadInt32();
            FILE.Seek(ExtLenHeader + (ChanLenHeader*Chans), SeekOrigin.Begin);            
            ForeignHeader = FID.ReadInt32();
            DataStart = ForeignHeader + 4 * Chans + (ChanLenHeader *Chans) + ExtLenHeader;
            FileTime = (int)((FILE.Length - (long)DataStart) / (2 * Chans * SampleRate));
            
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (Chans+.5));
        }
        public bool ReadData(int TimeStart, int Length) // In seconds
        {
            long SeekPoint;
            data = new Int16[Chans][];
            SampleSize = SampleRate * Length;
            for (int i = 0; i < Chans; i++)
            {
                data[i] = new Int16[SampleSize];
            }
            //Seek to data point, 2 because they are 2 bytes each, dumbass.
            SeekPoint = 2*TimeStart * Chans * SampleRate + DataStart; 
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            for (int i = 0; i < Length * Chans *  SampleRate; i++)
            {
                if (FILE.Position >= EOF)
                {
                    SampleSize = (i-1)/Chans;
                    return false;
                }
                data[i % Chans][i / Chans] = FID.ReadInt16();
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
            offscreen = new Bitmap(X,Y);
            Xmax = X;
            Ymax = Y;            
            g = Graphics.FromImage(offscreen);
            g.Clear(Color.White);            
        }
        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {
            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);

            float m = (maxPixel - minPixel) / (65536);
            float b = 2^15;
            float result = (m * volt) + b;
            //result = (result > maxPixel) ? maxPixel: result;
            //result = (result < minPixel) ? minPixel: result;
            return (result);
        }
        public void DumpData(string Fname, int Chan, int St, int Length)
        {
            int SeekPoint;
            FileStream FOUT = new FileStream(Fname, FileMode.Create);
            BinaryWriter FOUT_ID = new BinaryWriter(FOUT);
            data = new Int16[Chans][];
            SampleSize = SampleRate * Length;
            for (int i = 0; i < Chans; i++)
            {
                data[i] = new Int16[SampleSize];
            }
            //Seek to data point, 2 because they are 2 bytes each, dumbass.
            SeekPoint = 2 * St * Chans * SampleRate + DataStart;
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            for (int i = 0; i < Length * Chans * SampleRate; i++)
            {
                data[i % Chans][i / Chans] = FID.ReadInt16();
            }
            for (int j = 0; j < SampleSize; j++)
            {
                FOUT_ID.Write(data[Chan][j]);
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
            PointF[][] WaveC;                           
             g.Clear(Color.White);
             if (HL)
             {                                  
                 SolidBrush myBrush = new SolidBrush(System.Drawing.Color.LightGreen);                    
                 g.FillRectangle(myBrush, new Rectangle((int)(HLS * PointSpacing * SampleRate), (int)(VoltageSpacing * (SelectedChan + 0.5F)), (int)((HLE - HLS) * PointSpacing * SampleRate), (Ymax / Chans)));                 
                 
             }
                WaveC = new PointF[Chans][];
                for (int i = 0; i < Chans; i++)
                {
                    WaveC[i] = new PointF[SampleSize];
                }
                
                for (int j = 0; j < Chans; j++)
                {                                       
                    for (int i = 0; i < SampleSize; i++)
                    {
                   
                        PointF TempPoint = new PointF((float)i * PointSpacing, VoltageSpacing * (j + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(data[j][i]), Ymax / (Chans)));
                        WaveC[j][i] = TempPoint;                        
                    }                    
                    if (j == SelectedChan) 
                        g.DrawLines(SelectedPen, WaveC[j]);                                     
                    else
                        g.DrawLines(WavePen, WaveC[j]);                                     
                }                
            }        

    }
} 
