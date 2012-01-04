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
        private int SampleRate;
        private BinaryReader FID;
        private FileStream FILE;
        private int ExtLenHeader;
        private int ChanLenHeader;
        private int ForeignHeader;
        private int DataStart;
        private int MaxDrawSize;
        private float PointSpacing;
        private int DisplayLength;
        private int SampleSize;
        private int VoltageSpacing;
        private int Ymax;
        public Bitmap offscreen;
        
        Graphics g;
        public ACQReader()
        {
            Chans = new int();            
            SampleRate = 1000;
            DisplayLength = 30;
        }

        void openACQ()
        { //open File for reading
            FILE = new FileStream(FullName, FileMode.Open);
            FID = new BinaryReader(FILE);
            //Get header info
            FILE.Seek(6, SeekOrigin.Begin);
            ExtLenHeader = FID.ReadInt32();         
            Chans = (int)FID.ReadInt16();
            FILE.Seek(ExtLenHeader, SeekOrigin.Begin);
            ChanLenHeader =FID.ReadInt32();
            FILE.Seek(ExtLenHeader + (ChanLenHeader*Chans), SeekOrigin.Begin);
            ForeignHeader = FID.ReadInt32();
            DataStart = ForeignHeader + 4 * Chans + (ChanLenHeader *Chans) + ExtLenHeader;
        }
        void ReadData(int TimeStart, int Length) // In seconds
        {
            long SeekPoint;
            data = new Int16[Chans][];
            SampleSize = SampleRate * Length;
            for (int i = 0; i < Chans; i++)
            {
                data[i] = new Int16[SampleSize];
            }
            //Seek to data point
            SeekPoint = TimeStart * Chans * SampleRate + DataStart; 
            FILE.Seek(SeekPoint, SeekOrigin.Begin);
            //Pull Data from file
            for (int i = 0; i < Length * Chans *  SampleRate; i++)
            {
                data[i%Chans][i/Chans] = FID.ReadInt16();
            }
        }   
        public void initDisplay(int X, int Y)
        {
            offscreen = new Bitmap(X,Y);
            MaxDrawSize = SampleRate * DisplayLength;
            PointSpacing = Convert.ToSingle(X / MaxDrawSize);
            Ymax = Y;
            VoltageSpacing = (int)(Ymax / (AcqChan + 1));
        }
        private void drawbuffer()
        {
            PointF[][] WaveC;                           
             g.Clear(Color.White);
                WaveC = new PointF[Chans][];
                for (int i = 0; i < Chans; i++)
                {
                    WaveC[i] = new PointF[SampleSize];
                }
                
                for (int j = 0; j < Chans; j++)
                {                                       
                    for (int i = 0; i < SampleSize; i++)
                    {
                   
                        PointF TempPoint = new PointF(i * PointSpacing, VoltageSpacing * ((i % Chans) + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(data[j][i]), Ymax / (Chans)));
                        WaveC[j][i] = TempPoint;                        
                    }                                
                    g.DrawLines(wavePen, WaveC[j]);                                     
                }
            }

    }
} 
