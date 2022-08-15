using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace SeizurePlayback
{


    /* The DetectedSeizureType class is used for objects designated as "detected seizures". 
     A detected seizure is any seizure flagged by the seizure detection software, and are what is shown during Fast Review
    They have three properties:
        TimeInSec: The time (in seconds) where the seizure segment began
        Channel: The channel of the detected seizure
        Display: A boolean value that is true if the seizures are selected during fast review, and false if not
            This display value determines which seizures are highlighted and stepped to during normal playback    
     
     */
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
        public int Count; //shows the count of VISIBLE channel seizures
        //public int VisChanCount;
        public bool isLoaded;
        private List<DetectedSeizureType> DetectedSeizures;
        public List<int> HCL;
        public List<int> VisIndex;
        
        
        //private List<DetectedSeizureType> VisSeizures;
        public DetectedSeizureFileType()
        {
            FileName = "";
            DetectedSeizures = new List<DetectedSeizureType>();
            //VisSeizures = new List<DetectedSeizureType>();
            isLoaded = false;
            HCL = new List<int>();
            VisIndex = new List<int>();
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
            //VisChanCount = DetectedSeizures.Count;
            SeizureNumber = -1;
            isLoaded = true;
            ResetVisI();
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
            Number = VisIndex[Number];
            if (Number+1> DetectedSeizures.Count)
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
            for(int i=0; i< DetectedSeizures.Count; i++)
            {
                DetectedSeizures[i].Display = false; 
            }
        }
        public DetectedSeizureType GetCurrentSeizure()
        {
            SkipToVisChan();

            DetectedSeizureType TempSz = DetectedSeizures[SeizureNumber];
            
            return TempSz; 
        }

        //public bool IsSelected(int sN)
        //{
        //    for (int i = 0; i < DetectedSeizures.Count; i++)
        //    {
        //        if (DetectedSeizures[sN].Display == true) return true;
                
        //    }
        //    return false;
        //}

        //public void RemoveChannel(int a)
        //{
        //    for (int i = 0; i < DetectedSeizures.Count; i++)
        //    {
        //        if (DetectedSeizures[i].Channel == a)
        //        {
        //            DetectedSeizures.RemoveAt(i);
                    
        //        }
        //    }
        //    Count = DetectedSeizures.Count;
        //}

        //public void RemoveChannel(List<int> hcl)
        //{
        //    if (hcl.Count < 1) return;

        //    for (int i = 0; i < DetectedSeizures.Count; i++)
        //    {
        //        if (hcl.Contains(DetectedSeizures[i].Channel))
        //        {
        //            DetectedSeizures.RemoveAt(i);
        //        }
        //    }

        //    Count = DetectedSeizures.Count;


        //    //foreach (int hc in hcl)
        //    //{
                
        //    //}


        //}

        private void SkipToVisChan()
        {
            while (HCL.Contains(DetectedSeizures[SeizureNumber].Channel))
            {
                //VisChanCount--;
                VisIndex.Remove(SeizureNumber);
                Inc();
            }
        }

        public void VisCount()
        {
            Count = DetectedSeizures.Count;

            for (int i = 0; i < DetectedSeizures.Count; i ++)
            {
                if (HCL.Contains(DetectedSeizures[i].Channel))
                {
                    Count--;
                }
            }
            
        }

        public void ResetVisI()
        {
            for (int i = 0; i < DetectedSeizures.Count; i++)
            {
                VisIndex.Add(i);
            }


            if (HCL.Count > 0)
            {

                for (int i = 0; i < DetectedSeizures.Count; i++)
                {   
                    if (HCL.Contains(VisIndex[i]))
                    {
                    VisIndex.Remove(i);
                    }

                }
            }
        }

        //public void EqualizeLists()
        //{
        //    VisSeizures.Clear();
        //    DetectedSeizureType TempSz;
            
        //    int tempChan;
        //    int tempTime;
        //    int tempDisp;
        //    foreach (DetectedSeizureType Seizure in DetectedSeizures)
        //    {
        //        TempSz = Seizure;
        //        tempChan = TempSz.Channel;
        //        tempTime = TempSz.TimeInSec;
        //        tempDisp = TempSz.Display;

        //    }
        //}
    }
}
