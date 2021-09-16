using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SeizurePlayback
{
    class DetectedSeizureType
    {
        public int TimeInSec; 
        public int Channel;
        public bool Display; 
        public DetectedSeizureType(int a, int b, bool c)
        {
            Channel = a; 
            TimeInSec = b;
            Display = c; 
        }
    }

    class DetectedSeizureFileType
    {
        private string FileName;
        public int SeizureNumber;
        public int Count;
        public bool isLoaded;
        private List<DetectedSeizureType> DetectedSeizures;
        public DetectedSeizureFileType()
        {
            FileName = "";
            DetectedSeizures = new List<DetectedSeizureType>();
            isLoaded = false; 
        }
        public void OpenFile(string FN)
        {
            DetectedSeizures.Clear(); 
            DetectedSeizureType TempSz; 
            FileName =FN;
            StreamReader F = new StreamReader(FN);
            while (!F.EndOfStream)
            {
               TempSz = ParseLine(F.ReadLine());
               DetectedSeizures.Add(TempSz);
            }
            Count = DetectedSeizures.Count;
            SeizureNumber = -1;
            isLoaded = true;
        }
        private DetectedSeizureType ParseLine(string p)
        {
            DetectedSeizureType TempSz;
            int Ch;
            int T;
            string[] data = p.Split(',');
            int.TryParse(data[0], out Ch);
            int.TryParse(data[1], out T);
            TempSz = new DetectedSeizureType(Ch, T, true);
            return TempSz;
        }
        public bool Inc()
        {
            SeizureNumber = SeizureNumber + 1; 
            if (SeizureNumber+1 > DetectedSeizures.Count)
            {
                SeizureNumber = DetectedSeizures.Count-1;
                return false;
            }
            return true;
        }
        public bool SetSeizureNumber(int Number)
        {
            
            if (Number+1 > DetectedSeizures.Count)
            {
                SeizureNumber = DetectedSeizures.Count - 1;
                return false;
            }
            SeizureNumber = Number;
            return true;
        }
        public bool ChangeDisplaySeizure(int Number)
        {
            if (Number+1>DetectedSeizures.Count)
            {
                return false;
            }
            DetectedSeizures[Number].Display = !DetectedSeizures[Number].Display;
            return true; 
            
        }
       public bool Dec()
        {
            SeizureNumber = SeizureNumber - 1;
            if (SeizureNumber < 0)
            {
                SeizureNumber = 0;
                return false;
            }
            return true;
        }
        public void ResetDisplay()
        {
            for(int i=0; i<DetectedSeizures.Count; i++)
            {
                DetectedSeizures[i].Display = false; 
            }
        }
        public DetectedSeizureType GetCurrentSeizure()
        {
            DetectedSeizureType TempSz = DetectedSeizures[SeizureNumber];
            return TempSz; 
        }
    }
}
