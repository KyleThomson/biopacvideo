using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using FileHelpers;

namespace SeizureHeatmap
{

    /// <summary>
    /// BEGINNING OF MainData DEFINITION
    /// </summary>
    class MainData
    {
        // Initialize variables
        public List<RecordAnimals> Animals;
        public string mainPath;
        public string fileName;

        public MainData(string Inpt)
        {
            fileName = Inpt;
            mainPath = Path.GetDirectoryName(Inpt);
            Animals = new List<RecordAnimals>();
            // Check for file directory and create one if it doesn't exist.
            if (!Directory.Exists(mainPath + "\\Data"))
            {
                Directory.CreateDirectory(mainPath + "\\Data");
            }
        }
        public void Open() // Create method to open file.
        {
            if (File.Exists(fileName))
            {
                StreamReader F = new StreamReader(fileName); // Create new StreamReader so that we can read an input file.
                while (!F.EndOfStream)
                {
                    CsvParse(F.ReadLine());
                }

            }
        }
        private void CsvParse(string dataLine)
        {
            string[] dateFormats = { "M/d/yyyy", "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy" }; // specify acceptable formats for file date
            string[] data; // variable will be used to store parsed csv data
            DateTime dateStart; // extract start time of file which should be in very first line
            List<double> srsTimes = new List<double>();
            List<double> trtTimes = new List<double>();
            List<string> trtMethods = new List<string>();
            List<int> srsStages = new List<int>();
            List<string> seizureNotes = new List<string>();
            
            data = dataLine.Split(',');
            // Attempt to extract the file creation date
            if (DateTime.TryParseExact(data[0], dateFormats, new CultureInfo("en-US"), DateTimeStyles.None, out dateStart))
            {
                try
                {
                    dateStart = DateTime.Parse(data[0]);
                }
                catch
                {
                    Console.WriteLine("Failure identifying file start time.");
                }
            }
            else// all other data
            {
                int CurrentAnimal = FindAnimal(data[0]);

                if (double.TryParse(data[1], out _) && data[1].Length > 1)
                {
                    for (int i = 1; i < data.Length; i++)
                    {
                        if (Double.TryParse(data[i], out _))
                        {
                            // store all seizure times and add to Animals object
                            srsTimes.Add(Convert.ToDouble(data[i]));
                        }

                    }
                    Animals[CurrentAnimal].seizureTimes = srsTimes;
                }
                else if (double.TryParse(data[1], out _) && data[1].Length == 1)
                {
                    for (int i = 1; i < data.Length; i++)
                    {
                        if (int.TryParse(data[i], out _))
                        {
                            // store all seizure stages and add to Animals object
                            srsStages.Add(Convert.ToInt32(data[i]));
                        }

                    }
                    Animals[CurrentAnimal].seizureStages = srsStages;
                }
                else if (data[1] == "IJT")
                {
                    for (int i = 2; i <data.Length; i++)
                    {
                        if (Double.TryParse(data[i], out _))
                        {
                            // store all treatment times
                            trtTimes.Add(Convert.ToDouble(data[i]));
                        }

                    }
                    Animals[CurrentAnimal].trtTimes = trtTimes;
                }
                else if (data[1] == "IJC")
                {
                    for (int i = 2; i < data.Length; i++)
                    {
                        if (data[i] != "")
                        {
                            // store all treatment methods
                            trtMethods.Add(data[i]);
                        }

                    }
                    Animals[CurrentAnimal].trtMethods = trtMethods;
                }
                else
                {
                    for (int i = 1; i < data.Length; i++)
                    {
                        if (data[i] != "")
                        {
                            seizureNotes.Add(data[i]);
                        }
                    }
                    Animals[CurrentAnimal].seizureNotes = seizureNotes;
                }

            }
        }
        private int FindAnimal(string An)
        {
            int CurrentAnimal;
            RecordAnimals A;
            An = An.Replace(" ", string.Empty);
            CurrentAnimal = Animals.FindIndex(
                delegate (RecordAnimals X)
                {
                    return X.animalID == An;
                });
            if (CurrentAnimal == -1)
            {
                A = new RecordAnimals();
                A.animalID = An;
                Animals.Add(A);
                CurrentAnimal = Animals.IndexOf(A);
            }
            return CurrentAnimal;
        }
        public void GrpSeizures()
        {
            
            for (int i = 0; i < Animals.Count; i++)
            {
                if (Animals[i].seizureTimes != null)
                {
                    // All seizures for ith animal
                    List<int> tempSrs = Animals[i].seizureTimes.Select(x => Math.Floor(x / 24)).ToList().ConvertAll(Convert.ToInt32);
                    List<int> srsPerDay = new List<int>();
                    // Unique days where seizures occurred
                    List<int> distinctDays = tempSrs.Distinct().ToList();
                    // iterate thru unique days and find all occurrences of kth unique day in all seizures list
                    for (int k = 0; k < distinctDays.Count; k++)
                    {
                        int newDay = distinctDays[k];
                        var g = tempSrs.FindAll(x => x == newDay);
                        var srsCount = g.Count;
                        // Count seizures per unique day then add the unique day -- this data will be used to generate a heatmap
                        srsPerDay.Add(srsCount);
                    }
                    Animals[i].srsPerDay = srsPerDay;
                    Animals[i].days = distinctDays;
                    Animals[i].maxTrtDay = (int)Math.Floor(Animals[i].trtTimes.Max() / 24);
                }
                else
                {
                    continue;
                }


            }
        }
        public void ExtractData()
        {
            List<int> tempSrsFreq;   
            List<int> maxDays = new List<int>(); // set to null to get the compiler to shut up
            

            for (int i = 0; i < Animals.Count; i++)
            {
                try
                {
                    tempSrsFreq = Animals[i].srsPerDay;
                    //int tempMaxDay = Animals[i].days.Max();
                    maxDays.Add(Animals[i].maxTrtDay);
                }
                catch (System.ArgumentNullException e)
                {
                    continue;
                }
            }
            int trueMaxDay = maxDays.Max();
            int[,] allSeizures = new int[Animals.Count, trueMaxDay];
            List<int> dayRange = new List<int>();
            // With highest day among all animals, create a vector/list with range of numbers (1 to max day)
            for (int i = 1; i<trueMaxDay+1; i++)
            {
                dayRange.Add(i);
            }

            //Nested loop structure to add zeros to days that animal had no seizures
            for (int k = 0; k<Animals.Count; k++)
            {
                if (Animals[k].seizureTimes != null)
                {
                    // init. vector of zeros for each animal
                    int[] tempAllDaySrs = new int[dayRange.Count];
                    for (int x = 0; x < Animals[k].days.Count; x++)
                    {
                        // use a flag to determine if a match between days is found -- we'd like to move on to the next match when this occurs
                        bool flag = false;
                        for (int i = 0; i < trueMaxDay; i++)
                        {

                            // Search for matching days since not all animals have seizures every day in range
                            if (Animals[k].days[x] == dayRange[i])
                            {
                                tempAllDaySrs[i] = Animals[k].srsPerDay[x];

                                flag = true;
                                break;
                            }
                            else if (tempAllDaySrs[i] != 0)
                            {
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (flag) continue;
                    }
                    Animals[k].allDaySrs = tempAllDaySrs;
                }
                else
                {
                    // If no seizures then assign all zero values
                    Animals[k].allDaySrs = new int[dayRange.Count];
                    continue;
                }
            }

        }                                                                                  
        public void XlsWriteSrs()
        {
            string savePath = mainPath + "\\results.xlsx";
            Excel excel = new Excel();
            excel.CreateNewFile();
            excel.SaveAs(savePath);
          
            // Iterate through rows (# of animals)
            for (int i = 0; i < Animals.Count; i++)
            {

                            excel.WriteToCell(i, 0, Animals[i].animalID);
                            excel.Save();

                int[] tempSRS = (int[])Animals[i].allDaySrs;
                            for (int  k = 0; k < Animals[i].allDaySrs.Length; k++)
                            {
                                excel.WriteToCell(i, k+1, tempSRS[k].ToString());
                                excel.Save();
                            }
            }
            excel.Close();


        }
    }
}
