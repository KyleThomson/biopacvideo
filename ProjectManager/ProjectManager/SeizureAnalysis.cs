using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using Accord.Math;
using System.Globalization;

namespace ProjectManager
{
    public enum Treatment
    {
        Default,
        Injection,
        Meal
    }
    public class SeizureAnalysis
    {
        public TESTTYPES test;
        public List<string> groups;
        public bool _analysisDone = false;
        public Treatment treatment;
        public Dictionary<string, GroupedData> groupedData = new Dictionary<string, GroupedData>();
        public SeizureAnalysis(TESTTYPES typeOfTest)
        {
            test = typeOfTest;
            groups = new List<string>();
        }
        public void SeizureFreedomPValue()
        {
            switch (test)
            {
                case TESTTYPES.T35:
                    var drugGroup = groups.Where(g => g != "vehicle" && g != "Baseline").ToList();

                    if (drugGroup.Count < 1) throw new NullReferenceException("Could not find drug");

                    groupedData[drugGroup[0]].freedomPValue = FisherExact(drugGroup[0], "Baseline");
                    groupedData["vehicle"].freedomPValue = FisherExact("vehicle", "Baseline");

                    break;
            }

        }

        public void DetermineTreatment(List<AnimalType> animals)
        {
            // This method identifies if the animals were mediated via injection or meal. Decides how to detect groups
            bool _injections = false;
            bool _meals = false;

            foreach (AnimalType animal in animals)
            {
                if (animal.Injections.Count > 0)
                    _injections = true;
                if (animal.Meals.Count > 0)
                    _meals = true;
            }

            if (_injections)
                treatment = Treatment.Injection;
            else if (_meals)
                treatment = Treatment.Meal;
        }
        private double FisherExact(string group1, string group2)
        {
            // This method gets seizure freedom information and organizes it into a 2x2 contingency table.
            // Calls on static FisherExact method
            double pvalue;

            int group1Seized = groupedData[group1].numAnimals - groupedData[group1].szFreedom;
            int group1NotSeized = groupedData[group1].szFreedom;

            int group2Seized = groupedData[group2].numAnimals - groupedData[group2].szFreedom;
            int group2NotSeized = groupedData[group2].szFreedom;

            pvalue = ExtraMath.FisherExact(group1Seized, group2Seized, group1NotSeized, group2NotSeized);

            return pvalue;
        }
        private double MWW(string group1, string group2)
        {
            // Mann-Whitney Wilcoxon hypothesis test between seizure burdens

            // Gets data from two different groups and uses static MannWhitneyWilcoxon to find significance
            double[] group1Burdens = groupedData[group1].szBurdens.ToArray();
            double[] group2Burdens = groupedData[group2].szBurdens.ToArray();

            var pvalue = ExtraMath.MannWhitneyWilcoxon(group1Burdens, group2Burdens);

            return pvalue;
        }
        public void SeizureBurdenPValue()
        {
            switch (test)
            {
                case TESTTYPES.T35:
                    var drugGroup = groups.Where(g => g != "vehicle" && g != "Baseline").ToList();

                    if (drugGroup.Count < 1) throw new NullReferenceException("Could not find drug");

                    groupedData["Baseline"].burdenPValue = MWW(drugGroup[0], "Baseline");
                    groupedData["vehicle"].burdenPValue = MWW(drugGroup[0], "vehicle");

                    break;

            }

        }
        private double SumSeizures(List<SeizureType> seizures)
        {
            // Sum severities in list of seizures
            var severitySum = 0;
            if (seizures.Count > 0)
                foreach (SeizureType seizure in seizures)
                {
                    if (seizure.Severity > 0)
                    {
                        severitySum += seizure.Severity;
                    }
                    else if (seizure.Severity == 0)
                    {
                        severitySum++;
                    }
                }

            return severitySum;
        }
        public void SzBurdenAndFreedom(List<AnimalType> animals, DateTime Earliest, string treatment)
        {
            // Calculate seizure burden and freedom. Currently only supports injection

            // manually add baseline condition
            groups.Add("Baseline");
            foreach (string group in groups)
            {
                groupedData.Add(group, new GroupedData(group));
                List<double> burdens = new List<double>();
                int groupFreedom = 0;
                int groupAnimals = 0;
                foreach (AnimalType animal in animals)
                {
                    if (treatment == "Injection")
                    {
                        // Extract relevant injection IDs and the injection times
                        List<InjectionType> groupTreatment = animal.Injections.Where(I => I.ADDID == group).ToList();
                        if (groupTreatment.Count > 0 || group == "Baseline")
                        {

                            //First count animal
                            groupAnimals++;

                            List<double> groupTimes = new List<double>();
                            if (group != "Baseline")
                            {
                                groupTimes = groupTreatment
                                    .Select(o => (double) o.TimePoint.Subtract(Earliest).TotalHours).ToList();

                                // Expand injection window to 12 hours after injections are done
                                groupTimes = groupTimes.OrderBy(x => x).ToList();
                                groupTimes[groupTimes.Count - 1] += 12;
                            }
                            else
                            // Baseline condition manually add times - baseline always t = 0 to first injection
                            // (This might have to be adjusted to just be 7 days before first injection, which would be a simple change in this block of code)
                            {
                                groupTimes = new List<double>
                                {
                                    0,
                                    animal.Injections[0].TimePoint.Subtract(Earliest).TotalHours - 0.1
                                };
                            }

                            var numDays = Math.Round((groupTimes.Max() - groupTimes.Min()) / 24, 1);

                            // Grab seizures in the injection window, then compute burden and answer freedom question
                            var groupSeizures =
                                animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= groupTimes.Min()
                                                     && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= groupTimes.Max()).ToList();

                            var seizureSum = SumSeizures(groupSeizures);
                            burdens.Add(Math.Round(seizureSum / numDays, 1));


                            // Seizure Freedom
                            if (groupSeizures.Count == 0)
                                groupFreedom++;
                        }
                    }
                }

                groupedData[group].szBurdens = burdens;
                groupedData[group].burdenSEM = ExtraMath.Sem(burdens);
                groupedData[group].szFreedom = groupFreedom;
                groupedData[group].numAnimals = groupAnimals;
                groupedData[group].szBurden = Math.Round(burdens.Average(), 1);
            }

        }
        
        public int[] CompareSeizures(SeizureType seizure, string animalID)
        {
            // Compare seizure stages from bubble and the stage written in the note.
            // Give user the end decision through a dialog box.

            //this function returns an array. The 0 index is either 0 or 1, 0 being a fail and 1 being a success in determining the correct rating
            //the 1 index will be the actual value of the rating if the rating is successful, it will contain the notes rating if not.

            // dictionary to replace parsed integers with string
            Dictionary<int, string> numbers = new Dictionary<int, string>();
            numbers.Add(0, "zero");  numbers.Add(1, "one"); 
            numbers.Add(2, "two");   numbers.Add(3, "three");
            numbers.Add(4, "four");  numbers.Add(5, "five");
            numbers.Add(6, "six");   numbers.Add(7, "seven");
            numbers.Add(8, "eight"); numbers.Add(9, "nine");

            // assign bubble severity as the checked severity value
            int bubbleSeverity = seizure.Severity;
            int[] r = new int[2];
            // initialize noteSeverity
            int noteSeverity;
            if (seizure.Notes.Length > 0)
            {
                noteSeverity = ParseSeizure(seizure.Notes);
            }
            else
            {
                // if notes are empty just take bubble severity
                r[0] = 1;
                r[1] = bubbleSeverity;
                return r;

            }
               

            // if severities already match then get out of this function
            if (bubbleSeverity == noteSeverity)
            {
                r[0] = 1;
                r[1] = bubbleSeverity;
                return r;
            } else
            {
                r[0] = 0; //fail!
                r[1] = noteSeverity;
                return r;
            }


                

            //// Open dialog for user to select correct seizure
            //string ID = animalID + " had seizure at " + seizure.d.ToString();
            //SeizureStageDialog stageDialog = new SeizureStageDialog();
            //stageDialog.ShowDialog(bubbleSeverity, noteSeverity, ID, seizure.Notes);

            //// set function return value to the severity that the user selected
            //int finalStage = stageDialog.returnSeverity;

            //// Only change notes if bubble severity was selected from dialog.
            //if (finalStage == bubbleSeverity)
            //    {
            //        // change seizure note so that there are no more numbers
            //        for (int i = seizure.Notes.Length - 1; i >= 0; i--)
            //        {
            //            // step backward thru seizure notes and insert word corresponding to number in notes
            //            // solution to save conflict results between bubble and notes
            //            if (int.TryParse(seizure.Notes[i].ToString(), out _))
            //            {
            //                // find string to replace number with
            //                // this is a crude solution to not have the dialog repeatedly show up when re-opening a project file
            //                string numberToInsert = numbers[bubbleSeverity];
            //                seizure.Notes = seizure.Notes.Insert(i, numberToInsert);
            //                seizure.Notes = seizure.Notes.Remove(i + numberToInsert.Length, 1);
            //            }
            //        }
            //    }
            //// return final stage if code makes it all the way to this return pathway
            //return finalStage;
        }
        public int ParseSeizure(string note)
        {
            // ParseSeizure takes an input string and returns an integer as the seizure severity stage. Used to compare to the bubble severity.

            int severity = default;

            // Check if negaitve one is contained in the notes
            if (note.Contains('-') && note.Contains('1'))
                return -1;

            // if no negative one then parse stage 'normally'
            string storeNum = String.Join("", note.Where(char.IsDigit)); //look at the digit that is included - SH
            if (storeNum.Length > 0)
            {
                severity = int.Parse(storeNum);
                if (severity == 5) //if the seizure severity if 5, it could mean popcorn or just stage 5 - SH
                {
                    if (note.Contains('p') || note.Contains('P')) // if there is a p in the note, probably means popcorn - SH
                    {
                        severity = 6; 
                    }
                    else //otherwise it is likely just a 5 - SH
                    {
                        severity = 5; 
                    }
                }
            }
            // if there is no digit, then it likely means status or dravet
            else if (note.Contains('d') || note.Contains('D')) // if there is a d in the notes, probably means dravet - SH
            {
                severity = 7;
            }
            else if (note.Contains("status") || note.Contains("Status") || note.Contains("se") || note.Contains("Se") || note.Contains("SE")) //idk dude - SH
            {
                severity = 8; 
            }
            else   // if notes are empty just return -1 so it doesn't return 0 and look like a nc seizure
                return -1;
            return severity;
        }

        public void ParseGroups(List<AnimalType> Animals)
        {
            // ParseGroups identifies the different treatments and adds them to groups. Currently only supports injections and is used to calculate seizure burdens and seizure freedom for distinct groups.

            switch (treatment)
            {
                case Treatment.Injection:
                    foreach (var animal in Animals)
                    {
                        foreach (var injection in animal.Injections)
                        {
                            // Use DL algorithm to do a check if vehicle is an injection ID. other injection IDs dont matter and should be added to groups
                            var result = DamerauLevenshtein.DamerauLevenshteinDistanceTo(
                                injection.ADDID.ToLower(), "vehicle") <= 3 ? "vehicle" : injection.ADDID;

                            // New group found, add to Groups and the analysis groups
                            if (result == "vehicle" && !groups.Contains("vehicle"))
                                groups.Add(result);
                            else if (result == injection.ADDID && !groups.Contains(injection.ADDID))
                                groups.Add(result);
                            if (result == "vehicle")
                                injection.ADDID = result;
                        }
                        // Sort groups alphabetically
                        groups = groups.OrderBy(o => o).ToList();
                    }
                    break;

                case Treatment.Meal:
                    break;
            }

        }
    }
}

