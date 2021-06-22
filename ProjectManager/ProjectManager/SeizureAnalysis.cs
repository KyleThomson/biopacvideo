using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public enum AnalysisTypes
    {
        // T35 & T36
        Baseline_vs_Vehicle,
        Baseline_vs_Drug,
        Drug_vs_Vehicle,

        // IAK
        GroupA_vs_GroupB,
        GroupA_vs_Baseline,
        GroupB_vs_Baseline
    }
    public class SeizureAnalysis
    {
        public TESTTYPES test;
        public List<string> groups;

        public Dictionary<AnalysisTypes, double> BurdenPVALUES = new Dictionary<AnalysisTypes, double>();
        public Dictionary<AnalysisTypes, double> FreedomPVALUES = new Dictionary<AnalysisTypes, double>();
        public Dictionary<TRTTYPE, SzMetrics> seizureData = new Dictionary<TRTTYPE, SzMetrics>();
        public Dictionary<string, GroupedData> groupedData = new Dictionary<string, GroupedData>();

        public SeizureAnalysis(TESTTYPES typeOfTest)
        {
            test = typeOfTest;
        }
        public void SeizureFreedomPValue()
        {
            if (groupedData.ContainsKey("Baseline"))
            {
                // Get baseline animals
                int baselineSeized = groupedData["Baseline"].numAnimals - groupedData["Baseline"].szFreedom;
                int baselineNotSeized = groupedData["Baseline"].szFreedom;
                foreach (KeyValuePair<string, GroupedData> kvpGroupedData in groupedData)
                {
                    if (kvpGroupedData.Key != "Baseline")
                    {
                        int groupSeized = kvpGroupedData.Value.numAnimals - kvpGroupedData.Value.szFreedom;
                        int groupNotSeized = kvpGroupedData.Value.szFreedom;
                        kvpGroupedData.Value.freedomPValue = ExtraMath.FisherExact(groupSeized, baselineSeized,
                                                                                    groupNotSeized, baselineNotSeized);
                    }
                }
            }
        }

        public void SeizureBurdenPValue()
        {
            if (groupedData.ContainsKey("Baseline"))
            {
                double[] baselineBurdens = groupedData["Baseline"].szBurdens.ToArray();
                foreach (KeyValuePair<string, GroupedData> kvpGroupedData in groupedData)
                {
                    double[] groupedBurdens = kvpGroupedData.Value.szBurdens.ToArray();
                    kvpGroupedData.Value.burdenPValue = MWWTest(baselineBurdens, groupedBurdens);
                }
            }
        }
        public double MWWTest(double[] sample1, double[] sample2)
        {
            // generate new mww test
            var mwwTest = new MannWhitneyWilcoxonTest(sample1, sample2, TwoSampleHypothesis.FirstValueIsSmallerThanSecond, exact: false);

            // extract pvalue and return
            double pvalue = mwwTest.PValue;
            return pvalue;
        }
        private double SumSeizures(List<SeizureType> seizures)
        {
            // Sum severities in list of seizures
            var severitySum = 0;
            if (seizures.Count > 0)
                foreach (SeizureType seizure in seizures)
                {
                    severitySum += seizure.Severity;
                }

            return severitySum;
        }
        public void GroupAnalysis(List<AnimalType> animals, DateTime Earliest, string treatment)
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
                    if (treatment == "injection")
                    {
                        // Extract relevant injection IDs and the injection times
                        List<InjectionType> groupTreatment = animal.Injections.Where(I => I.ADDID == group).ToList();
                        if (groupTreatment.Count > 0 || group == "Baseline")
                        {
                            List<double> groupTimes = new List<double>();
                            //First count animal
                            groupAnimals++;
                            if (group != "Baseline")
                            {
                                groupTimes = groupTreatment
                                    .Select(o => (double) o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                                // Expand injection window to 12 hours after injections are done
                                groupTimes[groupTimes.Count - 1] += 12;
                            }
                            else
                            {
                                groupTimes = new List<double>();
                                groupTimes.Add(0);
                                groupTimes.Add(animal.Injections[0].TimePoint.Subtract(Earliest).TotalHours);
                            }

                            // Grab seizures in the injection window, then compute burden and answer freedom question
                            List<SeizureType> groupSeizures =
                                animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours >= groupTimes.Min()
                                                     && S.d.Date.Subtract(Earliest).TotalHours <= groupTimes.Max()).ToList();

                            double seizureSum = SumSeizures(groupSeizures);
                            burdens.Add(seizureSum / groupSeizures.Count);

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
            }

        }
        public double SEM(List<double> sz)
        {
            double variance = 0;
            double mean = sz.Average();
            int n = sz.Count();
            // find standard deviation of sz burden
            for (int i = 0; i < sz.Count; i++)
            {
                variance += (float)Math.Pow(sz[i] - mean, 2) / (n - 1);
            }
            var sigma = Math.Sqrt(variance);
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

