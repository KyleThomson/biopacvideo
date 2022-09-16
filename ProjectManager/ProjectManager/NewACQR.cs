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
        private int Xmax, Ymax, FRYmax;
        public float frepos = 60;
        private int Voltage;
        private int DataType;
        public Bitmap offscreen;
        public Bitmap frOffscreen;
        private List<TimeSpan> SzTime;
        private List<int> SzChannel;
        Graphics g;
        Graphics galG;
        public int numPerPage = 24;
        public int PosInSample;
        public int TotalSamples;
        public int ChanPass;
        public float[] IndividualZoom;
        public bool MasterZoom = true;
        public int TelemHLOffset = 0;
        public int totalFiles;
        public List<AnimalType> AnimalList;
        


        public NewACQR(List<AnimalType> AL, int numDats)
        {
            Zoom = 1;
            Chans = new int();
            ID = new string[16];
            ID2 = new string[16];
            Voltage = 2000 * 1000;
            WavePen = new Pen(Color.Black);
            SelectedPen = new Pen(Color.Red);
            SelectedChan = -1;
            RandomOrder = new int[16];
            Randomized = false;
            Position = 0;

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
            SampleRate = (int)(1000 / FID.ReadDouble());
            FILE.Seek(ExtLenHeader, SeekOrigin.Begin);
            ChanLenHeader = FID.ReadInt32();
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
            FILE.Seek(ExtLenHeader + (ChanLenHeader * Chans), SeekOrigin.Begin);

            ForeignHeader = FID.ReadInt32();
            FILE.Seek(ForeignHeader + ExtLenHeader + (ChanLenHeader * Chans), SeekOrigin.Begin);
            //DataType = 4; // FID.ReadInt16();
            //DataStart = ForeignHeader + 4 * Chans + (ChanLenHeader * Chans) + ExtLenHeader;
            FileTime = (int)((FILE.Length - (long)DataStart) / (DataType * Chans * SampleRate));
            TotFileTime = FileTime;
            Position = 0;
            Loaded = true;
            VoltageSpacing = (int)(Ymax / (Chans));
        }

    }
}
