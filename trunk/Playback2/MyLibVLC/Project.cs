using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public class ExportType
    {
        public bool Sz;
        public bool Pellet;
        public bool Med;
        public bool wt;
        public bool SzTime;
        public bool Meal;
        public bool DetailList;
        public ExportType()
        {
            Sz = false;
            Pellet = false;
            Med = false;
            wt = false;
            SzTime = false;
            Meal = false;
            DetailList = false;
        }

    }
    class WeightType
    {
        public double wt;
        public DateTime dt;
        public int pt;
        public WeightType(string a, string b, string c)
        {
            double.TryParse(a, out wt);
            DateTime.TryParse(b, out dt);
            int.TryParse(c, out pt);
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
    public class FileType
    {
        public string[] AnimalIDs;
        public int Chans;
        public DateTime Start;
        public TimeSpan Duration;
        public FileType(string[] A, int B, DateTime C, string D)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);            
         }
        public FileType()
        {
        }
    }
        
    class SeizureType
    {
        public TimeSpan t;        
        public DateTime d;
        public int length;
        public string Notes;       
        public string file;        
        public SeizureType(string a, string b, string c, string e, string f)
        {
            DateTime.TryParse(a, out d);            
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;            
            Notes = c;
        }   
    }
    public class ImportantDateType
    {
        public string Label;
        public DateTime Date;
        public ImportantDateType(string a, string b)
        {
            Label = a;
            DateTime.TryParse(b, out Date);
        }
        
    }
    class AnimalType
    {
        public string ID;
        public List<WeightType> WeightInfo;
        public List<SeizureType> Sz;
        public List<MealType> Meals;
        public List<ImportantDateType> ImportantDates;    
        public int Group;
        public AnimalType()
        {            
            Sz = new List<SeizureType>();
            WeightInfo = new List<WeightType>();
            Meals = new List<MealType>();
            ImportantDates = new List<ImportantDateType>();
        }
    }
    public class Project
    {
        public string P;
        public string Filename;
        public List<FileType> Files;
        List<AnimalType> Animals;                 
        public Project(string Inpt)
        {
            Filename = Inpt;
            P = Path.GetDirectoryName(Inpt);
            Animals = new List<AnimalType>();
            Files = new List<FileType>();
            if (!Directory.Exists(P + "\\Data"))
            {
                Directory.CreateDirectory(P + "\\Data");
            }
        }
        public void MergeProject(string Inpt)
        {
            if (File.Exists(Inpt))
            {
                StreamReader F = new StreamReader(Inpt);
                while (!F.EndOfStream)
                {
                    ParseLine(F.ReadLine());
                }
                F.Dispose();
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
            foreach (FileType Fe in Files)
            {
                answer = "";
                for (int i = 0; i < Fe.Chans; i++)
                {
                    answer += "," + Fe.AnimalIDs[i];
                }
                s = "Fl," + DTS(Fe.Start) + "," + Fe.Duration.ToString() + "," + Fe.Chans.ToString() + answer;
                F.WriteLine(s);
            }
            foreach (AnimalType A in Animals)
            {
                foreach (SeizureType S in A.Sz)
                {
                    answer = string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
                    s ="An," + A.ID + ", sz, " + S.d.ToString() + ", " + answer + ", " + S.Notes + "," + S.length + "," + S.file;
                    F.WriteLine(s);
                }
                foreach (WeightType W in A.WeightInfo)
                {
                    s = "An," + A.ID + ", wt, " + W.wt.ToString() + ", " + W.dt.ToString() + ", " + W.pt.ToString();
                    F.WriteLine(s);
                }
                foreach (MealType M in A.Meals)
                {
                    s = "An," + A.ID + ", ml, " + M.d.ToString() + "," + M.type + "," + M.pelletcount.ToString();
                    F.WriteLine(s);
                }
                foreach (ImportantDateType I in A.ImportantDates)
                {
                    s = "An," + A.ID + ", id, " + I.Date.ToString() + "," + I.Label;
                    F.WriteLine(s);
                }
            }
            F.Close();
            F.Dispose();
        }
        private int FindAnimal(string An) //Finds an Animal Index, or creates a new one if not found
        {
            int CurrentAnimal;
            AnimalType A;
            An = An.Replace(" ", string.Empty);
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

        public string[] Get_Seizures(string A) //Get the seizure info for display
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
        public string[] Get_Meals(string A) //Get the meal info for display
        {
            string[] Ms;
            int i = 0;
            int idx = FindAnimal(A);            
            Ms = new string[Animals[idx].Meals.Count];
            foreach (MealType M in Animals[idx].Meals)
            {
                Ms[i] = M.d.Month.ToString() + "/" + M.d.Day.ToString() + " - " + M.pelletcount.ToString() + M.type;
                i++;
            }
            return Ms;
        }


        public void Sort() //Sort the entire database
        {
            Files.Sort(delegate(FileType F1, FileType F2) { return DateTime.Compare(F1.Start, F2.Start); });
            Animals.Sort(delegate(AnimalType A1, AnimalType A2) { return string.Compare(A1.ID, A2.ID); });
            foreach (AnimalType A in Animals)
            {
                A.Sz.Sort(delegate(SeizureType S1, SeizureType S2) { return DateTime.Compare(S1.d, S2.d); });
                A.Meals.Sort(delegate(MealType M1, MealType M2) { return DateTime.Compare(M1.d, M2.d); });
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
        public string[] Get_Files()
        {
            string[] X;
            X = new string[Files.Count];
            int i = 0;
            foreach (FileType F in Files)
            {
                X[i] = DTS(F.Start);
                i++;
            }
            return X;
        }
        public void AddWeight(string AID, int Wt, int Pt, DateTime dt)
        {
            int An = FindAnimal(AID);
            WeightType W = new WeightType(dt.ToShortDateString(), Wt.ToString(), Pt.ToString());
            Animals[An].WeightInfo.Add(W);
        }
        public int GetAnimalRecordingInfo(string AID, DateTime D)
        {            
            int Percent = 0;
            foreach (FileType Fe in Files)
            {
                foreach (string Animal in Fe.AnimalIDs)
                {
                    if (String.Compare(Animal, AID) == 0)
                    {
                        
                        if ((Fe.Start.Day == D.Day) && (Fe.Start.Month == D.Month) && (Fe.Start.Year == D.Year))
                        {                            
                            Percent = Percent + (int)Math.Round((decimal)Fe.Duration.TotalSeconds / 864);                            
                        }
                    }
                }
            }
            Percent = Math.Min(100, Percent);
            return Percent; 

        }
        public bool ImportDirectory(string Dir)
        {
            ACQReader TempACQ = new ACQReader();
            FileType F = new FileType();
            //Open the ACQ file
            string[] FName = Directory.GetFiles(Dir, "*.acq");
            if (FName.Length > 0) 
            {
                TempACQ.openACQ(FName[0]);
                string Fn = FName[0].Substring(FName[0].LastIndexOf('\\') + 1);
                F.Start = ConvertFileToDT(Fn);                
                F.Chans = TempACQ.Chans;
                F.AnimalIDs = TempACQ.ID;
                F.Duration = TimeSpan.FromSeconds(TempACQ.FileTime);
                TempACQ.closeACQ();
                FileType Fs = Files.Find(delegate(FileType Ft) { return ((DateTime.Compare(Ft.Start, F.Start) == 0) && (string.Compare(F.AnimalIDs[0], Ft.AnimalIDs[0])== 0)); }); 
                //Determine if duplicate file - compare animal name and file start
                if (Fs != null)
                {
                    //Boot us out of the function                   
                    return false;
                }                
                Files.Add(F);
                //Open the Feeder file
                string[] FLogName = Directory.GetFiles(Dir, "*Feeder.log");
                if (FLogName.Length != 0)
                {
                    DateTime d;
                    string Type;
                    int PC;
                    string Line;
                    int CurrentAnimal;
                    int Feeder;
                    StreamReader FLog = new StreamReader(FLogName[0]);

                    while (!FLog.EndOfStream)
                    {
                        //5/1/2012 3:00:04 AM  Feeder: 1  Pellets: 8 Medicated                
                        Line = FLog.ReadLine();
                        DateTime.TryParse(Line.Substring(0, Line.IndexOf("Feeder") - 1), out d);
                        int.TryParse(Line.Substring(Line.IndexOf("Pellets: ") + 9, 2), out PC);
                        int.TryParse(Line.Substring(Line.IndexOf("Feeder: ") + 8, 2), out Feeder);
                        CurrentAnimal = FindAnimal(F.AnimalIDs[Feeder / 2]);
                        if (Feeder%2 == 1) //Figure out type
                        {
                            Type = "M";
                        }
                        else
                        {
                            Type = "U";
                        }
                        MealType M = new MealType(d.ToString(), Type, PC.ToString());
                        Animals[CurrentAnimal].Meals.Add(M);
                    }
                    FLog.Close();
                }
                if (Directory.Exists(Dir + "\\Seizure"))
                {
                    string[] SZFile = Directory.GetFiles(Dir + "\\Seizure", "*.txt");
                    if (SZFile[0] != null)
                    {
                        ImportSzFile(SZFile[0]);
                    }
                }
            }
            return true;
        }
        public DateTime ConvertFileToDT(string F)
        {            
            int y, M, d;
            int h, m, s;
            int.TryParse(F.Substring(0, 4), out y);
            int.TryParse(F.Substring(4, 2), out M);
            int.TryParse(F.Substring(6, 2), out d);
            int.TryParse(F.Substring(9, 2), out h);
            int.TryParse(F.Substring(11, 2), out m);
            int.TryParse(F.Substring(13, 2), out s);
            return new DateTime(y, M, d, h, m, s);
            

        }
        public string DTS(DateTime dt)
        {

            return string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", dt);      
        }            
        public void ImportSzFile(string File)
        {            
            DateTime dt;
            string[] TmpStr;            
            int CurrentAnimal;
            string str;
            TimeSpan t;
            string F = File.Substring(File.LastIndexOf('\\')+1);            
            dt = ConvertFileToDT(F);
            StreamReader TmpTxt = new StreamReader(File);
            while (!TmpTxt.EndOfStream)
            {
                str = TmpTxt.ReadLine();
                TmpStr = str.Split(',');
                CurrentAnimal = FindAnimal(TmpStr[1]);           
                TimeSpan.TryParse(TmpStr[3], out t);
                t = t.Add(dt.TimeOfDay);                
                SeizureType S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6]);
                Animals[CurrentAnimal].Sz.Add(S);
            }
            TmpTxt.Close();
        }
        public void AddImportantDate(string AnimalName, string d, string Text)
        {
            int CurrentAnimal = FindAnimal(AnimalName);
            ImportantDateType N = new ImportantDateType(Text, d);
            Animals[CurrentAnimal].ImportantDates.Add(N);
        }
        public void RemoveImportantDate(string AnimalName, int loc)
        {
            int CurrentAnimal = FindAnimal(AnimalName);
            Animals[CurrentAnimal].ImportantDates.RemoveAt(loc);            
        }
        public string[] GetImportantDates(string AnimalName)
        {
            
            int CurrentAnimal = FindAnimal(AnimalName);
            int c = 0;
            string[] output = new string[Animals[CurrentAnimal].ImportantDates.Count];
            foreach (ImportantDateType I in Animals[CurrentAnimal].ImportantDates)
            {
                output[c] = I.Date.ToString() + " - " + I.Label;
                c++;
            }
            return output;
            
        }
        public ImportantDateType CheckImportantDate(string AnimalName, DateTime N)
        {
            int CurrentAnimal = FindAnimal(AnimalName);
            foreach (ImportantDateType I in Animals[CurrentAnimal].ImportantDates)
            {
                if (DateTime.Compare(N.Date, I.Date.Date) == 0) //Compare only the day, not the time 
                {
                    return I; 
                }
            }
            return null;
        }

        public void ExportData(string Fname, ExportType E)
        {
            //Open File
            StreamWriter F = new StreamWriter(Fname);
            F.AutoFlush = true;
            //
            Sort();
            int c;
            string st, st2;
            DateTime Earliest = Files[0].Start.Date;
            DateTime Latest = Files[Files.Count - 1].Start.Date;
            
            if (E.DetailList)
            {

                foreach (AnimalType A in Animals)
                {
                    F.WriteLine(A.ID);
                    DateTime LastDate = Earliest;
                    foreach (SeizureType S in A.Sz)
                    {
                        foreach (ImportantDateType I in A.ImportantDates)
                        {
                            if ((DateTime.Compare(I.Date, LastDate) > 0) && (DateTime.Compare(I.Date, S.d) <= 0))
                            {
                                F.WriteLine(I.Date.ToShortDateString() + ", " + I.Label);
                            }
                        }
                        F.WriteLine(S.d.ToShortDateString() + ", " + S.t.ToString() + ", " + S.Notes);
                        LastDate = S.d;
                    }

                    foreach (ImportantDateType I in A.ImportantDates)
                    {
                        if ((DateTime.Compare(I.Date, LastDate) > 0))
                        {
                            F.WriteLine(I.Date.ToShortDateString() + ", " + I.Label);
                        }
                    }
                }
            F.Close();                    
                return;
            }
            st = "Animal";
            for (DateTime i = Earliest; i <= Latest; i=i.AddDays(1))
            {
                st += "," + i.ToShortDateString();
                Console.WriteLine(st);
            }
            F.WriteLine(st);
            foreach (AnimalType A in Animals)
            {                                
                if (E.Sz) //Add up daily seizure count. 
                {
                    st = A.ID;
                    for (DateTime i = Earliest; i <= Latest; i=i.AddDays(1))
                    {
                        c = 0;                                        
                        foreach (SeizureType S in A.Sz)
                        {
                            if (DateTime.Compare(i.Date, S.d.Date) == 0) c++;                             
                        }
                        st += "," + c;
                    }
                    F.WriteLine(st);    
                }
                if (E.SzTime)
                {
                    st = A.ID;
                    foreach (SeizureType S in A.Sz)
                    {
                        st += ", " + Math.Floor(S.d.Subtract(Earliest).TotalHours + S.t.TotalHours).ToString();
                    }
                    F.WriteLine(st); 
                }
                if (E.Meal)
                {
                     st = A.ID;
                     st2 = A.ID;
                     foreach (MealType M in A.Meals)
                     {
                         st += ", " + Math.Floor(M.d.Subtract(Earliest).TotalHours).ToString();
                         if (M.type == "M")
                         {
                             st2 += ", 1"; 
                         }
                         else
                         {
                             st2 += ", 0"; 
                         }
                     }
                     F.WriteLine(st);
                     F.WriteLine(st2); 
                }
                if (E.Pellet) 
                {
                    st = A.ID;
                    for (DateTime i = Earliest; i <= Latest; i=i.AddDays(1))
                    {
                        c = 0;                                        
                        foreach (MealType M in A.Meals)
                        {
                            if (DateTime.Compare(i.Date, M.d.Date) == 0) 
                            {
                                if (M.type == "M")
                                {
                                    c+= M.pelletcount;
                                }
                                else
                                {
                                    c-= M.pelletcount;
                                }
                            }
                        }
                        st += "," + c;
                    }
                    F.WriteLine(st);    
                }
            }
            F.Close();
        }
        void ParseLine(string L)
        {
            string[] data;
            string[] IDs;
            int Chans;
            data = L.Split(',');
            //Data format - Record Type - Record Start
            //Record types - An = Animal, Fl = File, 
            if (data[0].IndexOf("Fl") != -1)
            {

                DateTime TempDate;
                try
                {
                    TempDate = ConvertFileToDT(data[1]);
                    //Get the ACQ info. 
                    int.TryParse(data[3],out Chans);
                    IDs = new string[Chans];
                    for (int i = 0; i < Chans; i++)
                    {
                        IDs[i] = data[4+i];
                    }
                    FileType F = new FileType(IDs, Chans, TempDate, data[2]);
                    Files.Add(F);                                     
                }
                catch
                {
                    Console.WriteLine("FAILURE IN DATE PARSE");
                }

            }
            else if (data[0].IndexOf("An") != -1)
            {
                int CurrentAnimal = FindAnimal(data[1]);
                //data[2].Replace(" ", string.Empty);            
                switch (data[2])
                {
                    case " wt":
                        WeightType W = new WeightType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].WeightInfo.Add(W);
                        break;
                    case " sz":
                        SeizureType S = new SeizureType(data[3], data[4], data[5], data[6], data[7]);
                        Animals[CurrentAnimal].Sz.Add(S);
                        break;
                    case " ml":
                        MealType M = new MealType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].Meals.Add(M);
                        break;
                    case " id":
                        ImportantDateType I = new ImportantDateType(data[4], data[3]);
                        Animals[CurrentAnimal].ImportantDates.Add(I);
                        break;
                    case " gp":                        
                        break;
                    default:
                        Console.WriteLine(data[2] + ": ERROR IN COMPARE");
                        break;
                }
            }

        }
        
    }
}
