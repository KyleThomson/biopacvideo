using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SeizurePlayback
{
    class WeightType
    {
        public double wt;
        public DateTime dt;
        public WeightType(string a, string b)
        {
            double.TryParse(a, out wt);
            DateTime.TryParse(b, out dt);
        }
    }
    class MealType
    {
        public DateTime d;
        public string type;
        public int pelletcount;
        public MealType(string a, string b, string c)
        {
            DateTime.TryParse(a, out d);
            type = b;
            int.TryParse(c, out pelletcount);
        }
    }
    class SeizureType
    {
        public TimeSpan t;
        public DateTime d;
        public string Notes;
        public SeizureType(string a, string b, string c)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            Console.WriteLine(t);
            c = Notes;
        }   
    }
    class AnimalType
    {
        public string ID;
        public List<WeightType> WeightInfo;
        public List<SeizureType> Sz;
        public DateTime Death;        
        
        public int Group;
        public AnimalType()
        {
            Sz = new List<SeizureType>();
            WeightInfo = new List<WeightType>();
        }
    }
    class Project
    {
        public string P;
        public string Filename;
        List<AnimalType> Animals;
        List<DateTime> Files;         
        public Project(string Inpt)
        {
            Filename = Inpt;
            P = Path.GetDirectoryName(Inpt);
            Animals = new List<AnimalType>();
            if (!Directory.Exists(P + "\\Data"))
            {
                Directory.CreateDirectory(P + "\\Data");
            }
        }
        public void Open()
        {
            if (File.Exists(Filename))
            {
            StreamReader F = new StreamReader(Filename);
            while (!F.EndOfStream)
            {
                ParseLine(F.ReadLine());
            }
            F.Dispose();
            }
        }
        public void Save()
        {
            StreamWriter F = new StreamWriter(Filename);
            string s;
            string answer;
            foreach (AnimalType A in Animals)
            {
                foreach (SeizureType S in A.Sz)
                {
                    answer = string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
                    s = A.ID + ", sz, " + S.d.ToShortDateString() + ", " + answer + ", " + S.Notes;
                    F.WriteLine(s);
                }
                foreach (WeightType W in A.WeightInfo)
                {
                    s = A.ID + ", wt, " + W.wt.ToString() + ", " + W.dt.ToString();
                }
            }
            F.Close();
        }
        private int FindAnimal(string An) //Finds an Animal Index, or creates a new one if not found
        {
            int CurrentAnimal;
            AnimalType A;
            An.Replace("  ", string.Empty);
            CurrentAnimal = Animals.FindIndex(
               delegate(AnimalType X)
               {
                   return X.ID == An;
               });
            if (CurrentAnimal == -1) //New Animal
            {
                A = new AnimalType();
                A.ID = An;
                Animals.Add(A);
                CurrentAnimal = Animals.IndexOf(A);
            }
            return CurrentAnimal;
        }
        public string[] Get_Seizures(string A)
        {
            string[] Szs;
            int i = 0;
            int idx = FindAnimal(A);
            string answer;
            Szs = new string[Animals[idx].Sz.Count];
            foreach (SeizureType S in Animals[idx].Sz)
            {
                answer =  string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
                Szs[i] = S.d.ToShortDateString() + " " + answer;
                i++;
            }

            return Szs;

        }
        public void Sort()
        {
            Animals.Sort(delegate(AnimalType A1, AnimalType A2) { return string.Compare(A1.ID, A2.ID); });
            foreach (AnimalType A in Animals)
            {
                A.Sz.Sort(delegate(SeizureType S1, SeizureType S2) { return DateTime.Compare(S1.d, S2.d); });
            }
        }
        public string[] Get_Animals()
        {
            string[] X;
            X = new string[Animals.Count];
            int i = 0;
            foreach (AnimalType A in Animals)
            {
                X[i] = A.ID;
                i++;
            }
            return X;
        }

        public void ImportSzFile(string File)
        {            
            DateTime dt;
            string[] TmpStr;            
            int CurrentAnimal;
            string str;
            string F = File.Substring(File.LastIndexOf('\\'));
            int y,m, d;
            int.TryParse(F.Substring(1, 4), out y);
            int.TryParse(F.Substring(5, 2), out m);
            int.TryParse(F.Substring(7, 2), out d);
            dt = new DateTime(y, m, d);
            StreamReader TmpTxt = new StreamReader(File);
            while (!TmpTxt.EndOfStream)
            {
                str = TmpTxt.ReadLine();
                TmpStr = str.Split(',');
                CurrentAnimal = FindAnimal(TmpStr[1]);
                Console.WriteLine(TmpStr[3]);
                SeizureType S = new SeizureType(dt.ToString(), TmpStr[3], TmpStr[5]);
                Animals[CurrentAnimal].Sz.Add(S);
            }
        }

        void ParseLine(string L)
        {
            string[] data;
            data = L.Split(',');
            int CurrentAnimal = FindAnimal(data[0]);            
            data[1].Replace(" ", string.Empty);            
            switch (data[1])
            {
                case " wt":
                    WeightType W = new WeightType(data[2], data[3]);
                    Animals[CurrentAnimal].WeightInfo.Add(W);
                    break;
                case " sz":
                    SeizureType S = new SeizureType(data[2], data[3], data[4]);
                    Animals[CurrentAnimal].Sz.Add(S);
                    break;
                case " gp":

                    break;
                default:
                    Console.WriteLine(data[1] + ": ERROR IN COMPARE");
                    break;
            }                            
            

        }
        
    }
}
