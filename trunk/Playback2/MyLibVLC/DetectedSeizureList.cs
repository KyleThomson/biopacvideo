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
        public DetectedSeizureType(int a, int b)
        {
            Channel = a; 
            TimeInSec = b;            
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
            TempSz = new DetectedSeizureType(Ch, T);
            return TempSz;
        }
        public void Inc()
        {
            SeizureNumber = SeizureNumber + 1; 
            if (SeizureNumber+1 > DetectedSeizures.Count)
            {
                SeizureNumber = DetectedSeizures.Count-1;
            }
        }
       public void Dec()
        {
            SeizureNumber = SeizureNumber - 1;
            if (SeizureNumber < 0)
                SeizureNumber = 0; 
        }
        public DetectedSeizureType GetCurrentSeizure()
        {
            DetectedSeizureType TempSz = DetectedSeizures[SeizureNumber];
            return TempSz; 
        }
    }
}
