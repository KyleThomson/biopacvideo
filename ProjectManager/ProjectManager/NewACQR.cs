using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;



namespace ProjectManager
{
    
    class OffsetName
    {
        int AnimalIndex;
        long Offset;
        int length;
        int SZNum;
        public OffsetName(int Animal, int Num) 
        {
            AnimalIndex = Animal;
            
            SZNum = Num;
        }

        

}


class NewACQR
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
        private int Xmax, Ymax, GVmax;
        public float frepos = 60;
        private int Voltage;
        private int DataType;
        public Bitmap offscreen;
        public Bitmap GVOffscreen;
        private List<TimeSpan> SzTime;
        private List<int> SzChannel;
        Graphics g;
        Graphics GVg;
        public int numPerPage;
        public int PosInSample;
        public int TotalSamples;
        public int ChanPass;
        
        public bool MasterZoom = true;
        public int TelemHLOffset = 0;
        public int totalFiles;
        public List<AnimalType> AnimalList;
        public List<OffsetName> Offset;
        
        


        public NewACQR(List<AnimalType> AL, int numDats)
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
            data = new Int32[AL.Count][];
            AnimalList = AL;
            totalFiles = numDats;

        }
        public void closeACQ()
        {
            FILE.Close();
            FID.Close();
        }

        public void ListCreation()
        {
            Offset = new List<OffsetName>();
            OffsetName tempOff;
            int aCount = 0;
            int totalSZ = 0;

            foreach (AnimalType A in AnimalList)
            {
                int numSz = 0;
                foreach (SeizureType S in A.Sz)
                {
                    tempOff = new OffsetName(aCount, numSz);
                    Offset.Add(tempOff);
                    numSz++;
                    totalSZ++;
                    
                }
                aCount++;
            }
            totalFiles = totalSZ;
            

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
            FILE.Seek(0, SeekOrigin.Begin);
            totalFiles = FID.ReadInt32();
            
            FILE.Seek(4, SeekOrigin.Begin);
            SampleRate = 500;

            numPerPage = 8;
         
            DataType = 4;
            DataStart = 4;
            FileTime = (int)((FILE.Length - (long)DataStart) / (DataType * SampleRate));
            TotFileTime = FileTime;
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (numPerPage));
        }

        public void SetDispLength(int DS)
        {
            DisplayLength = DS;
            MaxDrawSize = SampleRate * DisplayLength;
            PointSpacing = (float)Xmax / MaxDrawSize;
        }

        public void initDisplay(int X, int Y, int GVY)
        {
            offscreen = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            GVOffscreen = new Bitmap(Math.Max(X, 1), Math.Max(1, GVY));
            Xmax = X;
            Ymax = Y;
            GVmax = GVY;
            g = Graphics.FromImage(offscreen);
            GVg = Graphics.FromImage(GVOffscreen);
            GVg.Clear(Color.White);
            g.Clear(Color.White);
        }

        public void cleargraph()
        {
            g.Clear(Color.White);
            GVg.Clear(Color.White);
        }

        public void ResetScale()
        {
            VoltageSpacing = (int)(Ymax / (Math.Max(VisibleChans, 1)));
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

        public void DisplayDraw(int ViewType)
        {

            if (ViewType == 1)  //gallery mode drawing
            {

            } else if (ViewType == 2) //animal mode drawing
            {

            } else //default mode drawing
            {

            }


        }

    }
}
