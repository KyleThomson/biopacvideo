using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;


namespace ProjectManager
{
    public enum TESTTYPES
    {
        Default,
        T35,    // 0
        T36,    // 1
        IAK,    // 2
        T39,
        UNDEFINED
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
        public bool _fileChanged = default; // flag to indicate if changes have been made to file
        //public List<EEGOrganizer> SList;
        public bool pDir = false;
        public FileStream BigDAT;
        public BinaryWriter BBigDAT;
        public long Offset;
        public string CDatName;
        public int DatCount;
        public bool CDatExists;
        
       
        public Project(string Inpt, bool newP)
        {
            _fileChanged = false; // initialize file as not changed

            if (newP)
            {
                Filename = Inpt + ".pjt";
                CDatName = Inpt + ".dat";
                CDatExists = true;
                createCDat(CDatName);
            } else
            {
                Filename = Inpt;
                string tempDir = Inpt.Substring(0, Inpt.Length - 4);
                if (File.Exists(tempDir + ".dat"))
                {
                    CDatName = tempDir + ".dat";
                    CDatExists = true;
                    OpenCDat(CDatName);
                } else
                {
                    CDatExists = true;
                    createCDat(CDatName);
                }
            }
            

            Animals = new List<AnimalType>();
            Files = new List<FileType>();
            Groups = new List<GroupType>();
            analysis = new SeizureAnalysis(test);
            //SList = new List<EEGOrganizer>();
            Offset = 0;
            
            
        }
        public void GetPath()
        {
            P = Path.GetDirectoryName(Filename);
            if (!Directory.Exists(P + "\\Data"))
            {
                Directory.CreateDirectory(P + "\\Data");
                //Directory.Move(Filename, P + "\\Data");
                
            } 
                //Directory.Move(Filename, P + "\\Data");
            
        }

        //public void checkCD(bool exists)
        //{
        //    P = Path.GetDirectoryName(Filename);

        //    if (exists)
        //    {
        //        char[] splitter = new char[2];
        //        splitter[0] = '\\';
        //        splitter[1] = '.';
        //        string[] pathA = P.ToString().Split(splitter);
                
        //        string[] fileA = Filename.Split(splitter);
        //        if (!fileA[fileA.Length-1].Equals(".pjt"))
        //        {
        //            string[] temp = new string[fileA.Length + 1];
        //            for (int i = 0; i < fileA.Length; i++)
        //            {
        //                temp[i] = fileA[i];
        //            }
        //            temp[temp.Length - 1] = ".pjt";
        //            fileA = temp;
        //        }

        //        //if (!pathA[pathA.Length - 1].Equals(fileA[fileA.Length - 2]))
        //        if (!pathA[pathA.Length-1].Equals("PM_" + fileA[fileA.Length-2]))
        //        {
                    
        //        Console.WriteLine("File Before: " + Filename);
        //        Directory.CreateDirectory(P + "\\PM_" + fileA[fileA.Length - 2]);
        //        P = P + "\\PM_" + fileA[fileA.Length - 2];
        //        Console.WriteLine(P);
        //        //File tempFI = new File(Filename);
        //        File.Move(Filename, P + "\\" + fileA[fileA.Length-2] + ".pjt");
        //        //tempFI.mo(P + "\\" + fileA[fileA.Length - 2] + @".pjt");
        //            string[] fName1 = Directory.GetFiles(P + "\\", "*.pjt");
        //            Filename = fName1[0];
        //            Console.WriteLine(P);
        //            Console.WriteLine("File After: " + Filename);
        //        }

                



        //    } else
        //    {

        //    }

        //}

        public void createCDat(string name) //IMPORTANT! First 4 Bytes of the CDat are reserved for DATCount
        {
            //File.Create(name);
            
            Offset = 0;
            DatCount = 0;
            BigDAT = new FileStream(name, FileMode.Create, FileAccess.ReadWrite);
            BBigDAT = new BinaryWriter(BigDAT);
            Int32 TotCount = 0;
            BBigDAT.Write(TotCount);
            
        }

        public void closeCDat()
        {
            BigDAT.Close();
            BBigDAT.Close();
        }

        public void OpenCDat(string name)
        {
            if (BigDAT != null) return;
            if (File.Exists(name))
            {

                FileStream tempRead = new FileStream(name, FileMode.Open, FileAccess.Read);
                BinaryReader tempBRead = new BinaryReader(tempRead);
                DatCount = tempBRead.ReadInt32();
                tempBRead.Close();
                tempRead.Close();
                //BigDAT = new FileStream(name, FileMode.Append, FileAccess.Write);
                BigDAT = new FileStream(name, FileMode.Append, FileAccess.Write);
                BBigDAT = new BinaryWriter(BigDAT);
                
                
            } else
            {
                createCDat(name);
            }
        }

        public long AppendCDat(string FileN)
        {

            if (BigDAT == null)
            {
                BigDAT = new FileStream(CDatName, FileMode.Append, FileAccess.Write);
                BBigDAT = new BinaryWriter(BigDAT);
            }
            if (!File.Exists(FileN)) return -1;


            //if (DatCount < 2)
            //{
            //    Console.WriteLine("Number: " + DatCount);
            //    Console.WriteLine("For: " + FileN);
            //    Console.WriteLine("Pos Start: " + BigDAT.Position);
            //}
            
            long r = BigDAT.Position;
            

            FileStream InFile = new FileStream(FileN, FileMode.Open, FileAccess.Read);
            BinaryReader BInFile = new BinaryReader(InFile);
            

            for (int i = 0; i < InFile.Length / 4; i++)
            {
                //Int32 temp = BInFile.ReadInt32();
                //BBigDAT.Write(temp);
                BBigDAT.Write(BInFile.ReadInt32());
                //if (DatCount == 0 && i == 0) Console.WriteLine(temp);
            }

            //if (DatCount < 2) Console.WriteLine("Pos End: " + BigDAT.Position + "\n\n\n");
            InFile.Close();
            BInFile.Close();
            DatCount++;

            return r;
            //for (int i = 0; i < data.Length; i ++)
            //{
            //    BBigDAT.Write(data[i]);
            //}

            

            


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
        public bool ParseNegativeOne(string input)
        {
            bool _ignoreNumber = false;

            for (int i = 1; i < input.Length; i++)
            {
                if (!int.TryParse(input[i].ToString(), out _))
                    continue;

                if (int.Parse(input[i].ToString()) == 1 && input[i - 1].ToString() == "-")
                    _ignoreNumber = true;
            }
            return _ignoreNumber;
                
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
                //checkCD(true);
            }
        }

        public void pjtCreate()
        {
            StreamWriter F = new StreamWriter(Filename);
            F.Write("");
            F.Close();
        }
        public void Save(string fileToSave)
        {
            StreamWriter F = new StreamWriter(fileToSave);
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
                    s = "An," + A.ID + ", sz, " + S.d.ToString() + ", " + answer + ", " + S.Notes + "," + S.length + "," + S.file + "," + S.Severity + "," + S.Offset;
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
            _fileChanged = false; // after file has saved, we can reset flag
        }
        public void SaveAs()
        {
            // Save .pjt file as -
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Filter = "PJT files (*.pjt) |*.pjt";
            saveAsDialog.DefaultExt = ".pjt";
            saveAsDialog.Title = "Save as project (.pjt) file";
            saveAsDialog.InitialDirectory = "D:\\";

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveAsDialog.FileName != "")
                {
                    Save(saveAsDialog.FileName);
                    // Set new filename for project
                    Filename = saveAsDialog.FileName;
                }
            }
        }
        public void FileChanged()
        {
            // set flag to true to indicate that the current project file has been modified
            _fileChanged = true;
        }

        public bool OpenEEGView()
        {
            return true;
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
                Szs[i] = S.d.ToShortDateString() + " " + answer + "  SEVERITY: " + S.Severity.ToString();
                i++;
            }

            return Szs;

        }
        public string[] GetSeizuresInFile(FileType file)
        {
            // initialize list of seizure output
            string[] seizures;

            // create list to add seizures to for the input file
            List<SeizureType> fileSeizures = new List<SeizureType>();
            List<SeizureType> tempSeizures = new List<SeizureType>();

            // iterate thru animals and create list of seizures for the animals in file
            foreach (string animalID in file.AnimalIDs)
            {
                // locate animal in list
                int idx = FindAnimal(animalID);

                // match animal seizures to file
                tempSeizures = Animals[idx].Sz.Where(s => s.file.Contains(file.fileName)).ToList();
                fileSeizures.AddRange(tempSeizures);
            }

            seizures = new string[fileSeizures.Count];
            string date;
            int i = 0;
            foreach (SeizureType seizure in fileSeizures)
            {
                date = string.Format("{0:D2}:{1:D2}:{2:D2}", seizure.t.Hours, seizure.t.Minutes, seizure.t.Seconds);
                seizures[i] = seizure.d.ToShortDateString() + " " + date;
                i++;
            }
            return seizures;
        }
        public string[] Get_Injections(string A)
        {
            string[] injections;
            int i = 0;
            int idx = FindAnimal(A);
            string answer;
            injections = new string[Animals[idx].Injections.Count];

            foreach (InjectionType injection in Animals[idx].Injections)
            {
                answer = string.Format("{0:D2}:{1:D2}:{2:D2}", injection.TimePoint.Hour, injection.TimePoint.Minute, injection.TimePoint.Second);
                injections[i] = injection.TimePoint.Date.ToShortDateString() + " " + answer + "  ADDID: " + injection.ADDID;
                i++;
            }
            return injections;
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
            // check if there was a test performed
            if (analysis.test == TESTTYPES.UNDEFINED) return;
            if (analysis._analysisDone) return;

            // Computer burden and freedoms
            analysis.SzBurdenAndFreedom(Animals, Earliest: Files[0].Start.Date, "Injection");
            analysis.SeizureFreedomPValue();
            analysis.SeizureBurdenPValue();
            analysis._analysisDone = true;
        }
        public void CompareStageConflicts()
        {
            foreach (AnimalType A in Animals)
            {
                foreach (SeizureType S in A.Sz)
                {
                    // ask user for the seizure severity
                    int finalStage = analysis.CompareSeizures(S, A.ID);

                    // set new severity
                    S.Severity = finalStage;
                }
            }
            // Save the changes made to severity
            Save(Filename);
        }
        public void TestSort()
        {
            // TestSort sorts Animals based on the test being performed then initializes SzMetrics for each animal accordingly.
            if (analysis.test == TESTTYPES.T35)
            {
                // Remove animals w/o injections and one type of ADDID
                Animals.RemoveAll(a => a.Injections.Count == 0);
                Animals.RemoveAll(a => a.Injections[0].ADDID == a.Injections[a.Injections.Count - 1].ADDID);

                // Sort animals according to vehicle first
                List<AnimalType> sortedA = Animals.OrderBy(a => a.Injections[0].ADDID).ToList();
                Animals = sortedA;
            }
            else if (analysis.test == TESTTYPES.T36)
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
            else if (analysis.test == TESTTYPES.IAK)
            {
            }
            else if (analysis.test == TESTTYPES.UNDEFINED)
            { return; }
        }
        public string[] Get_Animals()
        {
            string[] X;
            X = new string[Animals.Count];
            int i = 0;
            foreach (AnimalType A in Animals)
            {
                //if (Files.Any(F => F.))
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
        public int ImportDirectory(string Dir, bool RejectIncomplete)
        {
            ACQReader TempACQ = new ACQReader();
            FileType F = new FileType();
            string[] IniFiles;
            IniFile BioINI;
            //Open the ACQ file
            string[] FName = Directory.GetFiles(Dir, "*.acq");
            if (FName.Length > 0)
            {
                IniFiles = Directory.GetFiles(Dir, "*_Settings.txt");
                BioINI = new IniFile(IniFiles[0]);
                double PercentCompletion = BioINI.IniReadValue("Review", "Complete", (double)0);
                if (RejectIncomplete)
                {
                    if (Math.Ceiling(PercentCompletion) < 100)
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            FName[0] + " at %" + Math.Round(PercentCompletion,2).ToString() + " - Import Anyway?",
                            "Review Not Complete",
                             MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            return 2;
                        }
                        else if (dialogResult == DialogResult.Yes)
                        {
                            //return 1;
                        }
                    }
                }
                F.Reviewer = BioINI.IniReadValue("Review", "Reviewer", "");
                DateTime.TryParse(BioINI.IniReadValue("Review", "LastReviewed", ""), out F.ReviewDate);
                TempACQ.openACQ(FName[0]);
                string Fn = FName[0].Substring(FName[0].LastIndexOf('\\') + 1);
                F.Start = ConvertFileToDT(Fn);
                F.Chans = TempACQ.Chans;
                F.AnimalIDs = TempACQ.ID;
                F.Duration = TimeSpan.FromSeconds(TempACQ.FileTime);

                TempACQ.closeACQ();
                FileType Fs = Files.Find(delegate (FileType Ft) { return ((DateTime.Compare(Ft.Start, F.Start) == 0) &&
                    (string.Compare(F.AnimalIDs[0], Ft.AnimalIDs[0]) == 0)); });
                //Determine if duplicate file - compare animal name and file start
                if (Fs != null)
                {
                    //Boot us out of the function                   
                    return 1;
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
                        ImportSzFile(SZFile[0], Dir + "\\Seizure\\");
                    }
                }
            }
            return 0;
        }
        private DateTime ConvertFileToDT(string F)
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
        private string DTS(DateTime dt)
        {

            return string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", dt);
        }
        public void ImportSzFile(string File, string Dir)
        {
            DateTime dt;
            if (Dir == null) return;
            string[] TmpStr;
            int CurrentAnimal;
            string str;
            TimeSpan t;
            SeizureType S;
            
            string F = File.Substring(File.LastIndexOf('\\') + 1);
            dt = ConvertFileToDT(F);
            StreamReader TmpTxt = new StreamReader(File);
            //long offset = 0;
            while (!TmpTxt.EndOfStream)
            {
                str = TmpTxt.ReadLine();
                long offset;
                TmpStr = str.Split(',');
                if (TmpStr.Length == 0) return;
                string tempDAT = Dir + TmpStr[6].Replace(" ", string.Empty) + ".dat";
                offset = AppendCDat(tempDAT);
                
                

                CurrentAnimal = FindAnimal(TmpStr[1]);
                TimeSpan.TryParse(TmpStr[3], out t);
                t = t.Add(dt.TimeOfDay);
                //Read in DAT file
                //Append to Current Data stream
                //Copy video file into subdirectory
                //Put offset into into S 
                if (TmpStr.Length == 7)
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6]);
                }
                else if (offset < 0)
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6], TmpStr[7]);
                }
                else
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6], TmpStr[7], offset); //
                }

                Animals[CurrentAnimal].Sz.Add(S);
                //This should be part of S 
                
                //Animals[CurrentAnimal].SZF.Add(Dir +  TmpStr[6].Replace(" ", string.Empty));
                //Console.WriteLine("Clear: " + Dir + TmpStr[6].Replace(" ", string.Empty)); //you're working on adding file names!
                //EEGOrganizer tempEO = new EEGOrganizer(TmpStr[1], Dir + TmpStr[6], CurrentAnimal);
                //SList.Add(tempEO);


            }
            TmpTxt.Close();
        }

        public long loadDats(string Path)
        {
            return 1;
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
        public void TrackAllAnimals()
        {
            // Track each animal throughout files
            foreach (AnimalType animal in Animals)
            {
                TrackAnimal(animal);
            }
        }
        private void TrackAnimal(AnimalType animal)
        {
            // method determines if animal is missing from file and logs it
            DateTime earliest = default; DateTime latest = default;
            bool _appeared = false;
            bool _firstAppearance = true;
            // Step thru files and determine if animal ID appears, disappears, or reappears
            int i = 0;
            foreach (FileType file in Files)
            {
                if (file.AnimalIDs.Contains(animal.ID) && !_appeared && _firstAppearance && i > 0)
                    // Track earliest time that animal ID appears 
                {
                    earliest = Files[i - 1].Start; 
                    _appeared = true; 
                    _firstAppearance = false;
                }

                else if (_appeared && !file.AnimalIDs.Contains(animal.ID) && file.AnimalIDs.Contains("e"))
                // If file doesn't have animal ID log a new latest time and set flag to false
                {
                    latest = file.Start;
                    _appeared = false;
                }
                i++;
            }
            animal.earliestAppearance = earliest;
            animal.latestAppearance = latest;
        }
        public void ExportBinnedSz(ExportType exporter)
        {
            // Create new file dialog box for saving exported binned seizures .csv
            SaveFileDialog binned = new SaveFileDialog();
            binned.Filter = "CSV files (*.csv) |*.csv";
            binned.DefaultExt = "csv";
            binned.Title = "Seizure Frequency per Day .csv";
            binned.InitialDirectory = "D:\\";

            DateTime Earliest = Files[0].Start.Date;
            DateTime Latest = Files[Files.Count - 1].Start.Date;

            string sz;
            if (binned.ShowDialog() == DialogResult.OK)
            {
                // Open binned seizure file
                StreamWriter sw = new StreamWriter(binned.FileName);
                sw.AutoFlush = true;
                int numDays = (int)Math.Floor(Latest.Subtract(Earliest).TotalDays) + 1;

                // get dates that animal recordings span
                var dates = "Date";
                var allDates = new List<DateTime>();
                for (var dt = Earliest; dt <= Latest; dt = dt.AddDays(1))
                {
                    dates += ", " + dt.ToString("d");
                    allDates.Add(dt);
                }



                sw.WriteLine(dates);

                if (exporter.ungrouped)
                {
                    foreach (var animal in Animals)
                    {
                        sz = animal.ID;
                        // align seizure dates by this value

                        // assign alignBy to zero first
                        double alignBy = 0;

                        // check if user selected align
                        if (exporter.align)
                            // change alignBy only if there are injections
                            if (animal.Injections.Count > 0)
                                alignBy = Math.Round(animal.Injections[0].TimePoint.Subtract(allDates[0]).TotalDays - 7, 1);

                        // bin seizures into integer array
                        var counts = BinSeizure(allDates, animal.Sz, alignBy);

                        // add counts to string that gets written to file
                        foreach (var count in counts)
                            sz += "," + count.ToString("D");

                        sw.WriteLine(sz);
                    }

                    sw.Close(); // close writer
                }
                else if (exporter.grouped)
                {
                    // Check for number of groups first and return out of function if there aren't any
                    if (analysis.groups.Count < 1) return;

                    foreach (string group in analysis.groups)
                    {
                        // Write the group name to file
                        sw.WriteLine(group);

                        // if the group is baseline go to next group
                        if (group == "Baseline") continue;

                        foreach (AnimalType animal in Animals)
                        {
                            // first injection to align to
                            double alignBy =
                                Math.Round(animal.Injections[0].TimePoint.Subtract(Earliest).TotalDays - 7, 2);

                            // Extract Injection times for the specific group
                            var groupTimes = animal.GetInjectionTimes(group, Earliest, alignBy);

                            // Check if any grouped times were found and if they weren't move on to next animal
                            if (groupTimes.Count < 1)
                                continue;

                            // Add 12 hours/half day to last injection for cross-over
                            groupTimes[groupTimes.Count - 1] += 0.5;

                            // group seizures based on injection/treatment times and exclude -1 racine score
                            var groupSeizures =
                                animal.Sz.Where(S => Math.Floor(S.d.Date.Subtract(Earliest).TotalDays) + S.t.TotalDays >= groupTimes.Min()
                                                        && Math.Floor(S.d.Date.Subtract(Earliest).TotalDays) + S.t.TotalDays <= groupTimes.Max()
                                                        && S.Severity != -1).ToList();

                            // bin into array
                            var counts = BinSeizure(allDates, groupSeizures, alignBy);

                            // initialize string to write seizures to with animal name
                            sz = animal.ID;
                            // write to file
                            foreach (var count in counts)
                                sz += "," + count.ToString("D");

                            sw.WriteLine(sz); // write seizures
                        }
                    }
                    sw.Close();
                }
            }
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
                        st = A.ID + ',' + B.dt.Date.ToString("MM/dd/yyyy") + " " + B.EnteredTime.ToString() + "," +
                             B.ID;
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

                if (E.seizureDuration)
                {
                    st = A.ID;
                    foreach (SeizureType seizure in A.Sz)
                    {
                        var duration = seizure.length;
                        st += ", " + duration.ToString();
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
                        st += "," + Math.Round(B.dt.Date.Subtract(Earliest).TotalHours + B.EnteredTime.TotalHours, 2)
                            .ToString();
                        st2 += "," + B.ID;
                    }

                    F.WriteLine(st);
                    F.WriteLine(st2);
                }

                if (E.Pellet)
                {
                    st = "PLRT," + A.ID; //Pellet Removal Time
                    st2 = "PLRC," + A.ID; //Pellet Removal Count 
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
            F.Close();
        }
        private int[] BinSeizure(List<DateTime> dates, List<SeizureType> seizures, double alignBy)
        {
            // Function takes seizures, range of dates, and a time to align (days)
            // the seizures by to bin them into counts, relative to the align time

            // initialize output
            var counts = new int[dates.Count];

            // get seizure datetimes
            List<DateTime> seizureDates = new List<DateTime>();             
            foreach (SeizureType S in seizures)
            {
                // solution to exclude seizures marked with "-1" stage from output
                if (S.Severity>-1)
                {
                    seizureDates.Add(S.d.Date.AddDays(S.t.TotalDays - alignBy)); 
                }
            }
            
            int i = 0; // counter for dates/bins
            foreach (var date in dates)
            {
                // add aligned days
                DateTime shiftedDate = date.AddDays(-alignBy);
                
                // bin by date, need to add upper limit by 1 day to bin by entire day
                var binnedDates = seizureDates.Where(dt => dt  >= shiftedDate && dt < shiftedDate.AddDays(1)).ToList();
                // insert into array
                counts[i] = binnedDates.Count;
                i++; // next bin
            }

            return counts;
        }

        //This function takes the data from the project file and loads it into memory. 
        private void ParseLine(string L)
        {
            string[] data;
            string[] IDs;
            int Chans;
            data = L.Split(',');
            if (data.Length == 1)
            {
                long.TryParse(data[0], out Offset);
            }
            //Data format - Record Type - Record Start
            //Record types - An = Animal, Fl = File, Gp = Group, Lb = Label.
            if (data[0].IndexOf("Fl") != -1)
            {
                FileType F;
                DateTime TempDate;
                DateTime ReviewDate;
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
                    if (data.Length <= 5 + Chans)
                    {
                        F = new FileType(IDs, Chans, TempDate, data[2], data[1]);
                    }
                    else
                    {
                        DateTime.TryParse(data[5], out ReviewDate);
                        F = new FileType(IDs, Chans, TempDate, data[2], data[4 + Chans], data[1], ReviewDate);
                    }
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
                    FileType F = new FileType(IDs, Chans, TempDate, data[2], data[1]);
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
                        SeizureType S = new SeizureType(data[3], data[4], data[5], data[6], data[7], data[8]);
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

