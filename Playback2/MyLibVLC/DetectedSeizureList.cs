using System;
using System.Collections.Generic;
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
        public bool isLoaded;
        private List<DetectedSeizureType> DetectedSeizures;
        private List<DetectedSeizureType> DetectedSeizuresBackup;
        public List<int> HCL;



        //private List<DetectedSeizureType> VisSeizures;
        public DetectedSeizureFileType()
        {
            FileName = "";
            DetectedSeizures = new List<DetectedSeizureType>();
            DetectedSeizuresBackup = new List<DetectedSeizureType>();

            isLoaded = false;
            HCL = new List<int>();


        }
        public void OpenFile(string FN)
        {
            DetectedSeizures.Clear();
            DetectedSeizuresBackup.Clear();
            DetectedSeizureType TempSz;
            FileName = FN;
            StreamReader F = new StreamReader(FN);
            while (!F.EndOfStream)
            {
                TempSz = ParseLine(F.ReadLine());
                DetectedSeizures.Add(TempSz);
                //Console.WriteLine(TempSz.Channel);
                DetectedSeizuresBackup.Add(TempSz);

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
            if (SeizureNumber + 1 > DetectedSeizures.Count)
            {
                SeizureNumber = DetectedSeizures.Count - 1;
                return false;
            }
            return true;
        }
        public bool SetSeizureNumber(int Number)
        {

            if (Number + 1 > DetectedSeizures.Count)
            {
                SeizureNumber = DetectedSeizures.Count - 1;
                return false;
            }
            SeizureNumber = Number;
            return true;
        }
        public bool ChangeDisplaySeizure(int Number)
        {

            if (Number + 1 > DetectedSeizures.Count)
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
            for (int i = 0; i < DetectedSeizures.Count; i++)
            {
                DetectedSeizures[i].Display = false;
            }
        }
        public DetectedSeizureType GetCurrentSeizure()
        {

            if (DetectedSeizures.Count == 0)
            {
                return null;
            }

            DetectedSeizureType TempSz = DetectedSeizures[SeizureNumber];

            return TempSz;
        }



        public void HCLSync(List<int> a)
        {
            HCL = a;
        }



        public void ResetDFS()
        {

            //Console.WriteLine("Count before Reset: " + Count);
            DetectedSeizureType TempSz;
            DetectedSeizures.Clear();
            for (int i = 0; i < DetectedSeizuresBackup.Count; i++)
            {
                TempSz = DetectedSeizuresBackup[i];

                DetectedSeizures.Add(TempSz);


            }

            Count = DetectedSeizures.Count;
            //Console.WriteLine("Count after Reset: " + Count);
            //Console.WriteLine(DetectedSeizures[0].Channel + "    " + DetectedSeizuresBackup[0].Channel);

            RemoveHiddenChan();



        }

        public void RemoveHiddenChan()
        {

            //Console.WriteLine("Count before removing hidden channels " + Count);
            //for (int i = 0; i < DetectedSeizures.Count; i++)
            //{
            //    if (HCL.Contains(DetectedSeizures[i].Channel))
            //    {
            //        DetectedSeizures.RemoveAt(i);
            //    }
            //}

            for (int i = 0; i < HCL.Count; i++)
            {
                for (int j = 0; j < DetectedSeizures.Count; j++)
                {
                    if ((int)HCL[i] == (int)DetectedSeizures[j].Channel)
                    {
                        DetectedSeizures.RemoveAt(j);
                        j--; 
                    }
                }
            }




            Count = DetectedSeizures.Count;
            //Console.WriteLine("Count after removing hidden channels " + Count);
        }


        public int IsDisplayed()
        {
            int r = 0;
            foreach (DetectedSeizureType DS in DetectedSeizures)
            {
                if (DS.Display) r++;
            }

            return r;


        }

        public int FRIndex()
        {
            
            int i = 0;
            DetectedSeizureType TempSZ = GetCurrentSeizure();
            foreach (DetectedSeizureType DS in DetectedSeizures)
            {
                if (DS.Display)
                {
                    
                    i++;
                    if (DS == TempSZ)
                    {
                        
                        return i;
                    }
                    
                }
            }
            return i;
        }
    }
}

