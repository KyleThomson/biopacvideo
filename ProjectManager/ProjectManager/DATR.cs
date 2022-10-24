using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;



namespace ProjectManager
{

    public class OffsetName
    {
        public int AnimalIndex;
        public int SZNum;
        public bool Selected;
        public OffsetName(int Animal, int Num, bool sel)
        {
            AnimalIndex = Animal;
            Selected = sel;
            SZNum = Num;
        }



    }


    public class DATR
    {
        public string FullName;
       
        public string[] ID;
        //holds the alternative ID for the channels
        public bool ChanID = false;
        
        public int SelectedChan;
        public int SampleRate;
        public bool Randomized;
        public bool Telemetry;
        public int[] RandomOrder;
        private BinaryReader FID;
        private FileStream FILES;
        
        
        long EOF;
        public float Zoom;
        
        public int FileTime;
        public int TotFileTime;
        
        public int Position;
        private int DataStart;
        private int MaxDrawSize;
        private float PointSpacing;
        private int DisplayLength;
        private int SampleSize;
        
        public bool Loaded;
        Pen WavePen, SelectedPen;
        private int VoltageSpacing;
        private int Xmax, Ymax, GXmax, GYmax;
        public float frepos = 60;
        private int Voltage;
        private int DataType;
        public Bitmap offscreen;
        public Bitmap GOffscreen;
        
        public Graphics g;
        public Graphics Gg;
        public int numPerPage;
        public int PosInSample = 0;
        
        Int32[] TData;
        public int yOff = 50;
        public bool MasterZoom = true;
        public int TelemHLOffset = 0;
        public int totalFiles;
        public int drawMode = 0;
        public bool oneCol = false;
        
        

        public DATR(string Fname)
        {
            Zoom = 1;
            ID = new string[16];
            Voltage = 2000 * 1000;
            WavePen = new Pen(Color.Black);
            SelectedPen = new Pen(Color.Red);
            SelectedChan = -1;
            RandomOrder = new int[16];
            Randomized = false;
            Position = 0;




            openDAT(Fname);

        }
        public void closeDAT()
        {
            FILES.Close();
            FID.Close();
            if (GOffscreen != null)
            {
                Gg.Dispose();
                GOffscreen.Dispose();
            }
            offscreen.Dispose();
            g.Dispose();
            TData = null;

        }

        public void DrawSZ(long offset, int length, int X, int Y, bool display, bool vidSelect)
        {
            PointF[] WaveC;
            int expectedSampleSize = DisplayLength * SampleRate;
            int YBoxMax = (Ymax / (numPerPage / 2));
            float YPoint;
            Pen BoxPen = new Pen(Color.Red, 3);
            Pen empty = new Pen(Color.LightGray, 1);
            SampleSize = length * SampleRate;
            TData = new Int32[SampleSize];
            int xOff = 2;
            if (drawMode == 1) xOff = 1;
            ReadData(offset);
            float YDraw;
            WaveC = new PointF[expectedSampleSize];
            YDraw = (Ymax / (numPerPage / 2)) * Y;
            if (SampleSize < expectedSampleSize)
            {
                int diff = (expectedSampleSize - SampleSize);
                int DiffF = diff / 2;
                int j = 0;
                for (int i = 0; i < expectedSampleSize; i++)
                {

                   

                    if (i < DiffF)
                    {
                        YPoint = Ymax / numPerPage / 2 + yOff - 40;

                    }
                    else if (i >= SampleSize + DiffF)
                    {
                        YPoint = Ymax / numPerPage / 2 + yOff - 40;
                    }
                    else
                    {
                        YPoint = ScaleVoltsToPixel(Convert.ToSingle(TData[j]), (Ymax / (float)(numPerPage / 2)));
                        YPoint += yOff;
                        j++;
                    }

                    
                    if (YPoint > (Ymax / (numPerPage / 2))) YPoint = ((float)Ymax / ((float)numPerPage / 2f) - YDraw);         //Kyle's Mistake                                                                        
                    if (YPoint < 0) YPoint = 0;
                    
                    PointF TempPoint = new PointF((float)(i + (X * expectedSampleSize)) / xOff * PointSpacing, YDraw + YPoint);
                    WaveC[i] = TempPoint;
                }
            }
            else
            {
                int diff = (SampleSize - expectedSampleSize);
                int DiffF = diff / 2;

                for (int i = 0; i < expectedSampleSize; i++)
                {


                    YPoint = ScaleVoltsToPixel(Convert.ToSingle(TData[i + DiffF]), (Ymax / (float)(numPerPage / 2)));
                    YPoint += yOff;


                    if (YPoint > Ymax / (numPerPage / 2)) YPoint = ((float)Ymax / ((float)numPerPage / 2f) - YDraw);       //Kyle's Mistake                                                                        
                    if (YPoint < 0) YPoint = 0;
                    PointF TempPoint = new PointF((float)(i + (X * expectedSampleSize)) / xOff * PointSpacing, YDraw + YPoint);
                    if (TempPoint.X > offscreen.Width)
                    {

                    }
                    WaveC[i] = TempPoint;
                }
            }


            g.DrawLines(WavePen, WaveC);



            if (display)
            {


                if (vidSelect)
                {
                    float VSPointSpacing;
                    VSPointSpacing = (float)GXmax / (SampleSize);

                    BoxPen = new Pen(Color.Red, 1);
                    Pen YBoxPen = new Pen(Color.Green, 4);
                    //g.DrawRectangle(BoxPen, 3 + X * (Xmax / xOff), 3 + YDraw, Xmax / xOff - 6, Ymax / (numPerPage / 2) - 6);
                    g.FillRectangle(empty.Brush, 2 + X * (Xmax / xOff), 2 + YDraw, Xmax / xOff - 2, Ymax / (numPerPage / 2) - 2);
                    g.DrawLines(WavePen, WaveC);
                    g.DrawRectangle(YBoxPen, 2 + X * (Xmax / xOff), 2 + YDraw, Xmax / xOff - 2, Ymax / (numPerPage / 2) - 2);
                    WaveC = new PointF[SampleSize];
                    X = 0;
                    for (int i = 0; i < SampleSize; i++)
                    {


                        YPoint = ScaleVoltsToPixel(Convert.ToSingle(TData[i]), (GYmax));

                        YPoint += yOff;


                        if (YPoint > GYmax) YPoint = (GYmax);        //Kyle's Mistake                                                                        
                        if (YPoint < 0) YPoint = 0;
                        //PointF TempPoint = new PointF((float)(i + (X * SampleSize)) / xOff * PointSpacing, GYmax + YPoint);
                        PointF TempPoint = new PointF((float)(i) * VSPointSpacing, YPoint);
                        if (TempPoint.X > offscreen.Width)
                        {

                        }
                        WaveC[i] = TempPoint;

                    }
                    Gg.DrawLines(WavePen, WaveC);
                }
                //else g.DrawRectangle(BoxPen, 3 + X * (Xmax / xOff), 3 + YDraw, Xmax / xOff - 6, Ymax / (numPerPage / 2) - 6);

            }
            else
            {
                g.DrawRectangle(WavePen, X * (Xmax / xOff), YDraw, Xmax / xOff, Ymax / (numPerPage / 2));
            }

            TData = null;

        }

        public bool ReadData(long pos)
        {
            FILES.Seek(pos, 0);

            for (int i = 0; i < SampleSize; i++)
            {
                if (i + pos > EOF) return false;
                TData[i] = FID.ReadInt32();

            }
            return true;
        }


        public void openDAT(string FName)
        { //open File for reading

            FullName = FName;
            FILES = new FileStream(FName, FileMode.Open, FileAccess.Read);
            FID = new BinaryReader(FILES);
            FILES.Seek(0, SeekOrigin.End);
            EOF = FILES.Position;
            //Get header info            
            FILES.Seek(0, SeekOrigin.Begin);
            totalFiles = FID.ReadInt32();

            FILES.Seek(4, SeekOrigin.Begin);
            SampleRate = 500;

            numPerPage = 8;

            DataType = 4;
            DataStart = 4;
            FileTime = (int)((FILES.Length - (long)DataStart) / (DataType * SampleRate));
            TotFileTime = FileTime;
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (numPerPage));
            SetDispLength(30);
        }

        public void SetDispLength(int DS)
        {
            DisplayLength = DS;
            MaxDrawSize = SampleRate * DisplayLength;
            PointSpacing = (float)Xmax / MaxDrawSize;
        }

        public void initDisplay(int X, int Y)
        {

            if (offscreen != null) offscreen.Dispose();
            offscreen = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));


            Xmax = X;
            Ymax = Y;


            g = Graphics.FromImage(offscreen);


            g.Clear(Color.White);

        }

        public void initDisplay(int X, int Y, int GX, int GY)
        {

            if (offscreen != null)
            {
                offscreen.Dispose();
                g.Dispose();
            }
            if (GOffscreen != null)
            {
                GOffscreen.Dispose();
            }
            offscreen = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            GOffscreen = new Bitmap(Math.Max(GX, 1), Math.Max(1, GY));

            Xmax = X;
            Ymax = Y;
            GXmax = GX;
            GYmax = GY;

            g =  Graphics.FromImage(offscreen);
            Gg = Graphics.FromImage(GOffscreen);

            Gg.Clear(Color.White);
            g.Clear(Color.White);
        }

        public void cleargraph()
        {
            g.Clear(Color.White);
            if (Gg != null) Gg.Clear(Color.White);
        }



       

        public int RandomizeList(object x, object y)
        {
            Random ran = new Random();
            int a = ran.Next();
            int b = ran.Next();
            if (a > b) return 1;
            else if (a < b) return -1;
            else return 0;

        }



        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {


            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);
            float m;
            float b;
            if (Telemetry)
            {
                m = (maxPixel - minPixel) / (Int32.MaxValue / 2);
                b = 2 ^ 30;
                TelemHLOffset = 0;
            }
            else
            {
                m = (maxPixel - minPixel) / (65536);
                b = 2 ^ 15;
                TelemHLOffset = 25;
            }
            float result = ((m * volt) * Zoom) + b;
            //result = (result > maxPixel) ? maxPixel: result;
            //result = (result < minPixel) ? minPixel: result;
            return (result);
        }


    }
}
