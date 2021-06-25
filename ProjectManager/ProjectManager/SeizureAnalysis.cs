using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class SeizureAnalysis
    {
        public TESTTYPES test;
        public List<string> groups;
        public bool _analysisDone = false;
        public Dictionary<string, GroupedData> groupedData = new Dictionary<string, GroupedData>();

        public SeizureAnalysis(TESTTYPES typeOfTest)
        {
            test = typeOfTest;
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

        private double FisherExact(string group1, string group2)
        {
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
                groupedData[group].burdenSEM = SEM(burdens);
                groupedData[group].szFreedom = groupFreedom;
                groupedData[group].numAnimals = groupAnimals;
                groupedData[group].szBurden = Math.Round(burdens.Average(), 1);
            }

        }
        public double SEM(List<double> sz)
        {
            var n = sz.Count;
            var sigma = ExtraMath.StdDev(sz);
            var sem = sigma / Math.Sqrt(n);
            return Math.Round(sem, 1);
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
            else if (noteSeverity == -1)
            {
                finalStage = bubbleSeverity;
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
    }
}

