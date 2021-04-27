using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace ProjectManager
{
    public enum TESTTYPES
    {
        T35,    // 0
        T36,    // 1
        IAK,    // 2
        T39     // 3 etc.
    }
    public enum TRTTYPE
    {
        Baseline,
        Vehicle,
        Drug
    }

    /****************************************************************************************************************8
     *
     * 
     * Start project definition
     * 
     * 
     * 
     * ******************************************************************************************************************/



    public class Project
    {
        public string P;
        public string Filename;
        public List<FileType> Files;
        public List<GroupType> Groups;
        public List<AnimalType> Animals;
        public List<LabelType> Labels;
        public TESTTYPES test;
        public SeizureAnalysis analysis;
        public Project(string Inpt)
        {
            Filename = Inpt;
            P = Path.GetDirectoryName(Inpt);
            Animals = new List<AnimalType>();
            Files = new List<FileType>();
            Groups = new List<GroupType>();
            analysis = new SeizureAnalysis();
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
                    ParseLineDuplicate(F.ReadLine());
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
            foreach (GroupType G in Groups)
            {
                s = "Gp," + G.IDNum + ", " + G.Name + ", " + G.count;
                F.WriteLine(s);
            }
            foreach (AnimalType A in Animals)
            {
                if (A.Group.IDNum != 0) //Save Group Name
                {
                    s = "An," + A.ID + ", gp," + A.Group.IDNum + ", " + A.Group.Name;
                    F.WriteLine(s);
                }
                foreach (SeizureType S in A.Sz)
                {
                    answer = string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
                    s = "An," + A.ID + ", sz, " + S.d.ToString() + ", " + answer + ", " + S.Notes + "," + S.length + "," + S.file + "," + S.Severity;
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
                foreach (RemovalType R in A.Removals)
                {
                    s = "An," + A.ID + ", rm, " + R.dt.ToString() + "," + R.count.ToString() + "," + R.pt;
                    F.WriteLine(s);
                }
                foreach (ImportantDateType I in A.ImportantDates)
                {
                    s = "An," + A.ID + ", dt, " + I.LabelID.ToString() + "," + I.Date.ToString();
                    F.WriteLine(s);
                }
                foreach (BloodDrawType B in A.BloodDraws)
                {
                    s = "An," + A.ID + ", bd, " + B.dt.ToString() + "," + B.EnteredTime.ToString() + "," + B.ID;
                    F.WriteLine(s);
                }
                foreach (InjectionType I in A.Injections)
                {
                    s = "An," + A.ID + ", ij, " + I.TimePoint.ToString() + "," + I.DoseNum.ToString() + "," + I.ADDID + "," + I.Dose.ToString() + "," + I.DoseAmount.ToString() + "," + I.Route + "," + I.solvent;
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
               delegate (AnimalType X)
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
                answer = string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
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
            Files.Sort(delegate (FileType F1, FileType F2) { return DateTime.Compare(F1.Start, F2.Start); });
            Animals.Sort(delegate (AnimalType A1, AnimalType A2) { return string.Compare(A1.ID, A2.ID); });
            foreach (AnimalType A in Animals)
            {
                A.Sz.Sort(delegate (SeizureType S1, SeizureType S2) { return DateTime.Compare(S1.d, S2.d); });
                A.Meals.Sort(delegate (MealType M1, MealType M2) { return DateTime.Compare(M1.d, M2.d); });
            }

        }
        public void Analysis()
        {
            // Computer burden and freedoms
            CalculateSzBurden();
            SeizureFreedom();

            // SEM
            analysis.baselineSEM = analysis.SEM(analysis.baselineBurdens.Values.ToList());
            analysis.drugSEM = analysis.SEM(analysis.drugBurdens.Values.ToList());
            analysis.vehicleSEM = analysis.SEM(analysis.vehicleBurdens.Values.ToList());
        }
        public void RemoveTreatment(string A)
        {

        }
        public void CompareStageConflicts()
        {
            foreach (AnimalType A in Animals)
            {
                foreach (SeizureType S in A.Sz)
                {
                    analysis.CompareSeizures(S, A.ID);
                }
            }
        }
        public void DetermineTest()
        {
            // check for project class traits that indicate the test that should performed, then set class variable to that test
            analysis.test = TESTTYPES.T35;

            // Sort AFTER determining test!
            ParseGroups();
            TestSort();
            
        }
        public void TestSort()
        {
            if (test == TESTTYPES.T35)
            {
                // Remove animals w/o injections and one type of ADDID
                Animals.RemoveAll(a => a.Injections.Count == 0);
                Animals.RemoveAll(a => a.Injections[0].ADDID == a.Injections[a.Injections.Count - 1].ADDID);

                // Sort animals according to vehicle first
                List<AnimalType> sortedA = Animals.OrderBy(a => a.Injections[0].ADDID).ToList();
                Animals = sortedA;
            }
            else if (test == TESTTYPES.T36)
            {
                // Iterate through animals backwards so we don't get unhandled exception when removing list elements
                for (int i = Animals.Count - 1; i >= 0; i--)
                {
                    // Break up meals into both groups
                    List<MealType> baselineMeals = Animals[i].Meals.Where(m => m.type == "U").ToList();
                    List<MealType> medicatedMeals = Animals[i].Meals.Where(m => m.type == "M").ToList();

                    if (baselineMeals.Count <= 1 || medicatedMeals.Count <= 1)
                    {
                        Animals.RemoveAt(i);

                    }
                }
            }
        }
        public void ParseGroups()
        {
            // Use Damerau-Levenshtein algorithm to find groups
            List<string> groups = new List<string>();
            string notVehicle = "";
            int notVehTest = new int();
            foreach (AnimalType A in Animals)
            {
                if (A.Injections.Count > 0)
                {
                    foreach (InjectionType I in A.Injections)
                    {
                        if (!String.IsNullOrEmpty(notVehicle)) 
                        // Test if string that is found as not vehicle group is similar to current ADDID.
                        { notVehTest = DamerauLevenshtein.DamerauLevenshteinDistanceTo(I.ADDID.ToLower(), notVehicle); }

                        // Test if ADDID is sufficiently similar to "vehicle".
                        int vehTest = DamerauLevenshtein.DamerauLevenshteinDistanceTo(I.ADDID.ToLower(), "vehicle");

                        // check if vehicle is a group yet
                        if (!groups.Contains("vehicle") && groups.Count < 2) { groups.Add("vehicle"); }

                        // if this is satisfied then injection is vehicle
                        if (vehTest <= 3 && groups.Contains("vehicle") && groups.Count < 3) 
                        { I.ADDID = "vehicle"; }

                        // Identified ADDID as unique and not vehicle.
                        else if (vehTest > 3 && notVehTest <= 3 && groups.Count < 3)
                        { notVehicle = I.ADDID; }

                        // check if not vehicle is a group yet
                        if (!groups.Contains(notVehicle) && notVehTest <= 3 && vehTest > 3 && groups.Count < 2) 
                        { groups.Add(I.ADDID); }

                        // Throw error if we get a third group.
                        else if (groups.Count >= 3) { throw (new Exception("More than two groups found.")); }
                    }
                }
                else if (A.Meals.Count > 0)
                {
                    // run block of code for medicated and unmedicated meals
                }
            }
            // Pass groups found to analysis.
            analysis.groups = groups;
        }
        public void CalculateSzBurden()
        {
            foreach (AnimalType A in Animals)
            {
                // SeizureBurden() calculates seizure burden for all relevant groups and finds their SEM's
                analysis.SeizureBurden(A, Files[0].Start.Date);
            }
            analysis.AverageBurdens();
        }
        public void SeizureFreedom()
        {
            // Answer seizure freedom question for each animal
            foreach (AnimalType A in Animals)
            {
                analysis.SeizureFreedom(A, Files[0].Start.Date);
            }

            // Add up animal seizure freedoms
            analysis.SumFreedoms();
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
                FileType Fs = Files.Find(delegate (FileType Ft) { return ((DateTime.Compare(Ft.Start, F.Start) == 0) && (string.Compare(F.AnimalIDs[0], Ft.AnimalIDs[0]) == 0)); });
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
                        if (Line.IndexOf("Feeder") > -1)
                        {
                            DateTime.TryParse(Line.Substring(0, Line.IndexOf("Feeder") - 1), out d);
                            int.TryParse(Line.Substring(Line.IndexOf("Pellets: ") + 9, 2), out PC);
                            int.TryParse(Line.Substring(Line.IndexOf("Feeder: ") + 8, 2), out Feeder);
                            CurrentAnimal = FindAnimal(F.AnimalIDs[Feeder / 2]);
                            if (Feeder % 2 == 1) //Figure out which feeder went off using modulus. 
                                                 //Odd = Back, i.e. "_M_edicated" 
                                                 //Even = Front, i.e. "_U_nmedicated"
                            {
                                Type = "M"; //M for medicated
                            }
                            else
                            {
                                Type = "U"; //U for unmedicated
                            }
                            MealType M = new MealType(d.ToString(), Type, PC.ToString()); //create temporary MealType M
                            Animals[CurrentAnimal].Meals.Add(M); //Add it to the correct animal
                        }
                        else if (Line.IndexOf("Removal") > -1)
                        {
                            int AID;
                            string type;
                            DateTime.TryParse(Line.Substring(0, Line.IndexOf("Removal") - 1), out d);
                            int.TryParse(Line.Substring(Line.IndexOf("P: ") + 3), out PC);
                            int.TryParse(Line.Substring(Line.IndexOf("Removal: ") + 9, 2), out AID);
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID - 1]);
                            if (Line.IndexOf("Tot") > -1)
                            {
                                type = "T";
                            }
                            else
                            {
                                type = "M";
                            }
                            RemovalType R = new RemovalType(d.ToString(), PC.ToString(), type);
                            Animals[CurrentAnimal].Removals.Add(R);
                        }
                        else if (Line.IndexOf("BloodDraw") > -1)
                        {
                            int AID;
                            TimeSpan T;
                            DateTime.TryParse(Line.Substring(0, Line.IndexOf("Blood") - 1), out d);
                            int.TryParse(Line.Substring(Line.IndexOf("BloodDraw: ") + 11, 2), out AID);
                            TimeSpan.TryParse(Line.Substring(Line.IndexOf("Time: ") + 5, 8), out T);
                            string S = Line.Substring(Line.IndexOf("ID: ") + 4, 3);
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID - 1]);
                            BloodDrawType B = new BloodDrawType(d.ToString(), T.ToString(), S);
                            Animals[CurrentAnimal].BloodDraws.Add(B);
                        }
                        else if (Line.IndexOf("Inj") > -1)
                        {
                            int Inj;
                            int AID;
                            string[] data;
                            data = Line.Split(',');
                            DateTime.TryParse(Line.Substring(0, Line.IndexOf("Inj") - 1), out d);
                            int.TryParse(Line.Substring(Line.IndexOf("Inj") + 3, 1), out Inj);
                            int.TryParse(data[1], out AID);
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID - 1]);
                            //Date DoseNum ADDID Dose DoseAmount Route Solvent
                            InjectionType I = new InjectionType(d.ToString(), Inj.ToString(), data[3], data[4], data[7], data[5], data[6]);
                            Animals[CurrentAnimal].Injections.Add(I);
                        }
                        else
                        {
                            Console.WriteLine("Unparsable Line in feeding file: " + Line);
                        }

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
            SeizureType S;
            string F = File.Substring(File.LastIndexOf('\\') + 1);
            dt = ConvertFileToDT(F);
            StreamReader TmpTxt = new StreamReader(File);
            while (!TmpTxt.EndOfStream)
            {
                str = TmpTxt.ReadLine();
                TmpStr = str.Split(',');
                CurrentAnimal = FindAnimal(TmpStr[1]);
                TimeSpan.TryParse(TmpStr[3], out t);
                t = t.Add(dt.TimeOfDay);
                if (TmpStr.Length == 7)
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6]);
                }
                else
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6], TmpStr[7]);
                }
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
                output[c] = I.Date.ToString() + " - " + I.LabelID.ToString();
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
            string st, st2, st3;
            DateTime Earliest = Files[0].Start.Date;
            DateTime Latest = Files[Files.Count - 1].Start.Date;
            st = Earliest.ToShortDateString() + "," + Earliest.ToShortTimeString();
            F.WriteLine(st);
            string sz;
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
                                F.WriteLine(I.Date.ToShortDateString() + ", " + I.LabelID);
                            }
                        }
                        F.WriteLine(S.d.ToShortDateString() + ", " + S.t.ToString() + ", " + S.Notes);
                        LastDate = S.d;
                    }

                    foreach (ImportantDateType I in A.ImportantDates)
                    {
                        if ((DateTime.Compare(I.Date, LastDate) > 0))
                        {
                            F.WriteLine(I.Date.ToShortDateString() + ", " + I.LabelID.ToString());
                        }
                    }
                }
                F.Close();
                return;
            }
            if (E.BloodDrawList)
            {
                foreach (AnimalType A in Animals)
                {
                    foreach (BloodDrawType B in A.BloodDraws)
                    {
                        st = A.ID + ',' + B.dt.Date.ToString("MM/dd/yyyy") + " " + B.EnteredTime.ToString() + "," + B.ID;
                        F.WriteLine(st);
                    }
                }
                F.Close();
                return;
            }
            st = "Animal";
            /* for (DateTime i = Earliest; i <= Latest; i=i.AddDays(1))
            {
                st += "," + i.ToShortDateString();
                Console.WriteLine(st);
            }
            F.WriteLine(st);*/
            foreach (AnimalType A in Animals)
            {
                if (E.Sz) //Add up daily seizure count. 
                {
                    st = A.ID;
                    for (DateTime i = Earliest; i <= Latest; i = i.AddDays(1))
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
                        st += ", " + Math.Round(S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours, 2).ToString();
                    }
                    F.WriteLine(st);
                }
                if (E.SeverityIndx)
                    st = A.ID;
                foreach (SeizureType S in A.Sz)
                {
                    st += ", " + S.Severity.ToString();
                }
                F.WriteLine(st);
                if (E.Notes)
                {
                    st = A.ID;
                    foreach (SeizureType S in A.Sz)
                    {
                        st += "," + S.Notes;
                    }
                    F.WriteLine(st);
                }
                if (E.Meal)
                {
                    st = A.ID;
                    st2 = A.ID;
                    st3 = A.ID;
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
                        if (E.Pellet)
                        {
                            st3 += ", " + M.pelletcount.ToString();
                        }
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                    F.WriteLine(st3);
                }
                if (E.BloodDraw)
                {
                    st = "BDT," + A.ID; //Blood Draw Time
                    st2 = "BDL," + A.ID; //Blood Draw Label 
                    foreach (BloodDrawType B in A.BloodDraws)
                    {
                        st += "," + Math.Round(B.dt.Date.Subtract(Earliest).TotalHours + B.EnteredTime.TotalHours, 2).ToString();
                        st2 += "," + B.ID;
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.Pellet)
                {
                    st = "PLRT," + A.ID;  //Pellet Removal Time
                    st2 = "PLRC," + A.ID;  //Pellet Removal Count 
                    foreach (RemovalType R in A.Removals)
                    {
                        st += "," + Math.Round(R.dt.Subtract(Earliest).TotalHours, 2).ToString();
                        st2 += "," + R.count.ToString();
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.Injections)
                {
                    st = A.ID + ",IJT";
                    st2 = A.ID + ",IJC";
                    foreach (InjectionType I in A.Injections)
                    {
                        st += "," + Math.Round(I.TimePoint.Subtract(Earliest).TotalHours, 2).ToString();
                        st2 += "," + I.ADDID;
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.InjectionsList)
                {

                }


            }
            if (E.binSz)
            {
                // Create new file dialog box for saving exported binned seizures .csv
                SaveFileDialog binned = new SaveFileDialog();
                binned.DefaultExt = "csv";
                binned.Title = "Binned Seizures .csv";
                binned.DefaultExt = ".csv";
                binned.InitialDirectory = "D:\\";

                if (binned.ShowDialog() == DialogResult.OK)
                {

                    // Open binned seizure file
                    StreamWriter sw = new StreamWriter(binned.FileName);
                    sw.AutoFlush = true;
                    foreach (AnimalType A in Animals)
                    {
                        // first injection
                        double alignBy = Math.Round(A.Injections[0].TimePoint.Subtract(Earliest).TotalDays - 7, 2);

                        // bin seizures if option was selected
                        sz = A.ID;
                        int numDays = Files.Count;
                        // Create list for days that seizures happen
                        List<double> szDay = new List<double>();
                        List<double> binSeizures = new List<double>(new double[numDays]);
                        foreach (SeizureType seizureType in A.Sz)
                        {
                            if (seizureType.d.Subtract(Earliest).TotalDays + seizureType.t.TotalDays >= alignBy)
                            {
                                szDay.Add(Math.Floor(seizureType.d.Subtract(Earliest).TotalDays + seizureType.t.TotalDays - alignBy));
                            }
                        }
                        var g = szDay.GroupBy(i => i);
                        foreach (var bin in g)
                        {
                            if (bin.Key > 0)
                            {
                                binSeizures[(int)(bin.Key - 1)] = bin.Count();
                            }
                            else
                            {
                                binSeizures[(int)(bin.Key)] = bin.Count();
                            }
                        }
                        for (int i = 0; i < binSeizures.Count; i++)
                        {
                            sz += "," + binSeizures[i].ToString();
                        }
                        sw.WriteLine(sz); // write seizures
                    }
                    sw.Close(); // close writer
                }
            }
            F.Close();
        }

        //This function takes the data from the project file and loads it into memory. 
        void ParseLine(string L)
        {
            string[] data;
            string[] IDs;
            int Chans;
            data = L.Split(',');
            //Data format - Record Type - Record Start
            //Record types - An = Animal, Fl = File, Gp = Group, Lb = Label.
            if (data[0].IndexOf("Fl") != -1)
            {

                DateTime TempDate;
                try
                {
                    TempDate = ConvertFileToDT(data[1]);
                    //Get the ACQ info. 
                    int.TryParse(data[3], out Chans);
                    IDs = new string[Chans];
                    for (int i = 0; i < Chans; i++)
                    {
                        IDs[i] = data[4 + i];
                    }
                    FileType F = new FileType(IDs, Chans, TempDate, data[2]);
                    Files.Add(F);
                }
                catch
                {
                    Console.WriteLine("FAILURE IN DATE PARSE");
                }

            }
            else if (data[0].IndexOf("Gp") != -1)
            {
                GroupType G = new GroupType(data[1], data[2], data[3]);
                Groups.Add(G);
            }
            else if (data[0].IndexOf("Lb") != -1)
            {
                LabelType Lb;
                Lb = new LabelType(data[1], data[2]);
                Labels.Add(Lb);
            }
            else if (data[0].IndexOf("An") != -1)
            {
                int CurrentAnimal = FindAnimal(data[1]);
                switch (data[2])
                {
                    case " gp":
                        int.TryParse(data[3], out Animals[CurrentAnimal].Group.IDNum);
                        Animals[CurrentAnimal].Group.Name = data[4];
                        break;
                    case " bd":
                        BloodDrawType B = new BloodDrawType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].BloodDraws.Add(B);
                        break;
                    case " rm":
                        RemovalType R = new RemovalType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].Removals.Add(R);
                        break;
                    case " wt":
                        WeightType W = new WeightType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].WeightInfo.Add(W);
                        break;
                    case " sz":
                        SeizureType S;
                        if (data.Length == 8)
                        {
                            //Old way - no severity score
                            S = new SeizureType(data[3], data[4], data[5], data[6], data[7]);
                        }
                        else
                        {
                            //New way, has severity info
                            S = new SeizureType(data[3], data[4], data[5], data[6], data[7], data[8]);
                        }
                        Animals[CurrentAnimal].Sz.Add(S);
                        break;
                    case " ml":
                        MealType M = new MealType(data[3], data[4], data[5]);
                        Animals[CurrentAnimal].Meals.Add(M);
                        break;
                    case " dt":
                        ImportantDateType I = new ImportantDateType(data[3], data[4]);
                        Animals[CurrentAnimal].ImportantDates.Add(I);
                        break;
                    case " ij":
                        InjectionType Inj = new InjectionType(data[3], data[4], data[5], data[6], data[7], data[8], data[9]);
                        Animals[CurrentAnimal].Injections.Add(Inj);
                        break;
                    default:
                        Console.WriteLine(data[2] + ": ERROR IN COMPARE");
                        break;
                }
            }

        }
        void ParseLineDuplicate(string L)
        {
            string[] data;
            string[] IDs;
            bool Pass; //Check if Data Is duplicated;
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
                    int.TryParse(data[3], out Chans);
                    IDs = new string[Chans];
                    for (int i = 0; i < Chans; i++)
                    {
                        IDs[i] = data[4 + i];
                    }
                    FileType F = new FileType(IDs, Chans, TempDate, data[2]);
                    Pass = true;
                    foreach (FileType C in Files)
                    {
                        if (C.Compare(F)) Pass = false;
                    }
                    if (Pass) Files.Add(F);
                }
                catch
                {
                    Console.WriteLine("FAILURE IN DATE PARSE");
                }

            }
            else if (data[0].IndexOf("Gp") != -1)
            {
                //Need to make sure group is not duplicated
                int tempG;
                int.TryParse(data[3], out tempG);
                bool pass = true;
                foreach (GroupType G in Groups)
                {
                    if (G.IDNum == tempG)
                        pass = false;
                }
                if (pass)
                {
                    GroupType G = new GroupType(data[3], data[4], data[5]);
                    Groups.Add(G);
                }
            }
            else if (data[0].IndexOf("Lb") != -1)
            {
                int tempL;
                int.TryParse(data[3], out tempL);
                bool pass = true;
                foreach (LabelType Lb in Labels)
                {
                    if (Lb.IDNum == tempL)
                        pass = false;
                }
                if (pass)
                {
                    LabelType Lb = new LabelType(data[3], data[4]);
                    Labels.Add(Lb);
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
                        Pass = true;
                        Animals[CurrentAnimal].WeightInfo.Add(W);
                        break;
                    case " sz":
                        SeizureType S = new SeizureType(data[3], data[4], data[5], data[6], data[7]);
                        Pass = true;
                        foreach (SeizureType C in Animals[CurrentAnimal].Sz)
                        {
                            if (C.Compare(S)) Pass = false;
                        }
                        if (Pass) Animals[CurrentAnimal].Sz.Add(S);
                        break;
                    case " ml":
                        MealType M = new MealType(data[3], data[4], data[5]);
                        Pass = true;
                        foreach (MealType C in Animals[CurrentAnimal].Meals)
                        {
                            if (C.Compare(M)) Pass = false;
                        }
                        if (Pass) Animals[CurrentAnimal].Meals.Add(M);
                        break;
                    case " id":
                        ImportantDateType I = new ImportantDateType(data[4], data[3]);
                        Animals[CurrentAnimal].ImportantDates.Add(I);
                        break;
                    case " gp":
                        //If the animal doesn't have a group, assign one, otherwise, ignore.
                        if (Animals[CurrentAnimal].Group.IDNum == 0)
                        {
                            int.TryParse(data[3], out Animals[CurrentAnimal].Group.IDNum);
                            Animals[CurrentAnimal].Group.Name = data[4];
                        }
                        break;
                    default:
                        Console.WriteLine(data[2] + ": ERROR IN COMPARE");
                        break;
                }
            }

        }

    }
}

