using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using Accord.Math;

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
                    List<string> copyGroups = groups;
                    copyGroups.Remove("Baseline");
                    copyGroups.Remove("vehicle");
                    string drugGroup = copyGroups[0];
                    groupedData["Baseline"].freedomPValue = FisherExact(drugGroup, "Baseline");
                    groupedData["vehicle"].freedomPValue = FisherExact(drugGroup, "vehicle");

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
                    List<string> copyGroups = groups;
                    copyGroups.Remove("Baseline");
                    copyGroups.Remove("vehicle");
                    string drugGroup = copyGroups[0];

                    groupedData["Baseline"].burdenPValue = MWW(drugGroup, "Baseline");
                    groupedData["vehicle"].burdenPValue = MWW(drugGroup, "vehicle");

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
        
        public int CompareSeizures(SeizureType seizure, string animalID)
        {
            // dictionary to replace parsed integers with string
            Dictionary<int, string> numbers = new Dictionary<int, string>();
            numbers.Add(0, "zero");  numbers.Add(1, "one"); 
            numbers.Add(2, "two");   numbers.Add(3, "three");
            numbers.Add(4, "four");  numbers.Add(5, "five");
            numbers.Add(6, "six");   numbers.Add(7, "seven");
            numbers.Add(8, "eight"); numbers.Add(9, "nine");

            int bubbleSeverity = default; // default
            int noteSeverity = default; // default
            int finalStage = default;
            if (seizure.Severity >= 0 && seizure.Severity <= 5)
            {
                bubbleSeverity = seizure.Severity;
            }
            if (seizure.Notes.Length > 0)
            {
                noteSeverity = ParseSeizure(seizure.Notes);
            }

            if (noteSeverity >= 0 && noteSeverity <= 5)
            {
                // Check if bubble and note match and flag if it doesn't -- want to prompt user with messagebox
                if (bubbleSeverity != noteSeverity)
                {
                    // Open dialog for user to select correct seizure
                    string ID = animalID + " had seizure at " + seizure.d.ToString();
                    SeizureStageDialog stageDialog = new SeizureStageDialog();
                    stageDialog.ShowDialog(bubbleSeverity, noteSeverity, ID, seizure.Notes);
                    finalStage = stageDialog.returnSeverity;

                    // Only change notes if bubble severity was selected from dialog.
                    if (finalStage == bubbleSeverity)
                    {
                        // change seizure note so that there are no more numbers
                        for (int i = seizure.Notes.Length - 1; i >= 0; i--)
                        {
                            int result = -1;
                            // step backward thru seizure notes and insert word corresponding to number in notes
                            // solution to save conflict results between bubble and notes
                            if (int.TryParse(seizure.Notes[i].ToString(), out int _))
                            {
                                string numberToInsert = numbers[result];
                                seizure.Notes = seizure.Notes.Insert(i, numberToInsert);
                                seizure.Notes = seizure.Notes.Remove(i + numberToInsert.Length, 1);
                            }
                            else
                            {

                            }
                        }
                    }
                } // Do something
                else
                {
                    finalStage = bubbleSeverity;
                }
            }
            // if user input a -1 into notes, handle it
            else if (noteSeverity == -1)
            {
                
            }
            else
            {
                finalStage = bubbleSeverity;
            }

            return finalStage;
        }
        public int ParseSeizure(string note)
        {
            int severity = -1;
            string storeNum = String.Join("", note.Where(char.IsDigit));
            if (storeNum.Length > 0)
            {
                severity = int.Parse(storeNum);
            }
            return severity;
        }

        public void ParseGroups(List<AnimalType> Animals)
        {
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

