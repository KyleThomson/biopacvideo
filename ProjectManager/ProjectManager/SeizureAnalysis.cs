using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Testing;

namespace ProjectManager
{
    public enum AnalysisTypes
    {
        // T35 & T36
        Baseline_vs_Vehicle,
        Baseline_vs_Drug,
        Drug_vs_Vehicle,
            
        // IAK
        GroupA_vs_GroupB
    }
    public class SeizureAnalysis
    {
        public TESTTYPES test;
        public List<string> groups;

        public Dictionary<AnalysisTypes, double> BurdenPVALUES = new Dictionary<AnalysisTypes, double>();
        public Dictionary<AnalysisTypes, double> FreedomPVALUES = new Dictionary<AnalysisTypes, double>();
        public Dictionary<TRTTYPE, SzMetrics> seizureData = new Dictionary<TRTTYPE, SzMetrics>();
        public Dictionary<TRTTYPE, double> seizureFreedom = new Dictionary<TRTTYPE, double>();

        public SeizureAnalysis(TESTTYPES typeOfTest)
        {
            test = typeOfTest;      
        }
        public void SzFreedomSignificance()
        {
            // 
            // Get integers for fisher exact test (2x2 contingency table)
            if (test == TESTTYPES.T35)
            {
                // Do Drug vs. Baseline comparison first
                int seizedDrugAnimals = seizureData[TRTTYPE.Drug].numAnimals - seizureData[TRTTYPE.Drug].szFreedom; 
                int seizedBaselineAnimals = seizureData[TRTTYPE.Baseline].numAnimals - seizureData[TRTTYPE.Baseline].szFreedom;
                int notSeizedDrugAnimals = seizureData[TRTTYPE.Drug].szFreedom;
                int notSeizedBaselineAnimals = seizureData[TRTTYPE.Baseline].szFreedom;
                double drugVsBaselinePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedBaselineAnimals, notSeizedDrugAnimals, notSeizedBaselineAnimals);

                // Do Vehicle vs. Baseline comparison next
                int seizedVehicleAnimals = seizureData[TRTTYPE.Vehicle].numAnimals - seizureData[TRTTYPE.Vehicle].szFreedom;
                int notSeizedVehicleAnimals = seizureData[TRTTYPE.Vehicle].szFreedom;
                double drugVsVehiclePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedVehicleAnimals, notSeizedDrugAnimals, notSeizedVehicleAnimals);
                FreedomPVALUES.Add(AnalysisTypes.Baseline_vs_Drug, drugVsBaselinePvalue);
                FreedomPVALUES.Add(AnalysisTypes.Drug_vs_Vehicle, drugVsVehiclePvalue);
            }
            else if (test == TESTTYPES.T36)
            {
                // Do Drug vs. Baseline comparison ONLY
                int seizedDrugAnimals = seizureData[TRTTYPE.Drug].numAnimals - seizureData[TRTTYPE.Drug].szFreedom; 
                int seizedBaselineAnimals = seizureData[TRTTYPE.Baseline].numAnimals - seizureData[TRTTYPE.Baseline].szFreedom;
                int notSeizedDrugAnimals = seizureData[TRTTYPE.Drug].szFreedom;
                int notSeizedBaselineAnimals = seizureData[TRTTYPE.Baseline].szFreedom;
                double drugVsBaselinePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedBaselineAnimals, notSeizedDrugAnimals, notSeizedBaselineAnimals);
                FreedomPVALUES.Add(AnalysisTypes.Baseline_vs_Drug, drugVsBaselinePvalue);
            }

        }
        public double MWWTest(double[] sample1, double[] sample2)
        {
            // generate new mww test
            var mwwTest = new MannWhitneyWilcoxonTest(sample1, sample2, TwoSampleHypothesis.FirstValueIsSmallerThanSecond , exact: false);

            // extract pvalue and return
            double pvalue = mwwTest.PValue;
            return pvalue;
        }
        private int GetSeverityScore(List<SeizureType> seizures, List<double> times, DateTime Earliest)
        {
            int score = default;
            foreach (SeizureType seizure in seizures)
            {
                double szTime = seizure.d.Date.Subtract(Earliest).TotalHours + seizure.t.TotalHours;
                // Add seizure severity to running total depending on treatment that seizure occurred during

                //Seizure happened during vehicle treatment
                if (szTime >= times.Min() && szTime <= times.Max())
                {
                    score += seizure.Severity;
                }
            }
            return score;
        }
        public void SeizureBurden(List<AnimalType> animals, DateTime Earliest)
        {
            // count animals here as well
            int baselineAnimals = animals.Count();
            int drugAnimals = 0;
            int vehicleAnimals = 0;

            List<int> vehicleSz = new List<int>();
            List<int> drugSz = new List<int>();
            List<int> baselineSz = new List<int>();
            switch (test)
            {
                case TESTTYPES.T35:
                    // running seizure stage totals
                    int vehicleScore = 0;
                    int drugScore = 0;
                    int baselineScore = 0;

                    // list of each animal's seizure burden
                    List<double> vehicleBurden = new List<double>();
                    List<double> drugBurden = new List<double>();
                    List<double> baselineBurden = new List<double>();
                    foreach (AnimalType animal in animals)
                    {
                        // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                        List<InjectionType> vehicleI = animal.Injections.Where(I => I.ADDID == "vehicle").ToList();
                        List<InjectionType> drugI = animal.Injections.Where(I => I.ADDID != "vehicle").ToList();
                        List<double> vehicleTimes = vehicleI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                        List<double> drugTimes = drugI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();

                        // count animals
                        if (vehicleI.Count > 0)
                        { vehicleAnimals++; }
                        if (drugI.Count > 0)
                        { drugAnimals++; }

                        // Number of days for each group
                        float vehicleDays = (float)((vehicleTimes.Max() - vehicleTimes.Min()) / 24);
                        float drugDays = (float)((drugTimes.Max() - drugTimes.Min()) / 24);

                        // Initialize days in baseline. Not sure how else to do this since compiler doesn't like variables being designed in a conditional statement
                        float baselineDays = -1;

                        // Create baselineTimes and add 0 (beginning of recording) and just before the first treatment
                        List<double> baselineTimes = new List<double>();
                        baselineTimes.Add(0);
                        if (drugTimes.Min() < vehicleTimes.Min())
                        {
                            baselineTimes.Add(drugTimes.Min() - 0.1);
                            baselineDays = (float)(drugTimes.Min() / 24);
                        }
                        else if (drugTimes.Min() > vehicleTimes.Min())
                        {
                            baselineTimes.Add(vehicleTimes.Min() - 0.1);
                            baselineDays = (float)(vehicleTimes.Min() / 24);
                        }
                        // get sum of severities for each group
                        if (animal.Sz.Count > 0)
                        {
                            baselineScore = GetSeverityScore(animal.Sz, baselineTimes, Earliest);
                            vehicleScore = GetSeverityScore(animal.Sz, vehicleTimes, Earliest);
                            drugScore = GetSeverityScore(animal.Sz, drugTimes, Earliest);

                            // count animals

                        }
                        if (baselineScore != default)
                        { baselineBurden.Add(baselineScore / baselineDays); }
                        if (vehicleScore != default)
                        { vehicleBurden.Add(vehicleScore / vehicleDays); }
                        if (drugScore != default)
                        { drugBurden.Add(drugScore / drugDays); }
                    }

                    // Set metrics
                    SzMetrics baselineMetrics = new SzMetrics(TRTTYPE.Baseline);
                    baselineMetrics.szBurden = Math.Round(baselineBurden.Average(), 2);
                    baselineMetrics.numAnimals = baselineAnimals;
                    baselineMetrics.burdenSEM = SEM(baselineBurden);

                    SzMetrics vehicleMetrics = new SzMetrics(TRTTYPE.Vehicle);
                    vehicleMetrics.szBurden = Math.Round(vehicleBurden.Average(), 2);
                    vehicleMetrics.numAnimals = vehicleAnimals;
                    vehicleMetrics.burdenSEM = SEM(vehicleBurden);

                    SzMetrics drugMetrics = new SzMetrics(TRTTYPE.Drug);
                    drugMetrics.szBurden = Math.Round(drugBurden.Average(), 2);
                    drugMetrics.numAnimals = drugAnimals;
                    drugMetrics.burdenSEM = SEM(drugBurden);

                    seizureData.Add(TRTTYPE.Baseline, baselineMetrics);
                    seizureData.Add(TRTTYPE.Vehicle, vehicleMetrics);
                    seizureData.Add(TRTTYPE.Drug, drugMetrics);

                    // MWW Test
                    BurdenPVALUES.Add(AnalysisTypes.Drug_vs_Vehicle, MWWTest(drugBurden.ToArray(), vehicleBurden.ToArray()));
                    BurdenPVALUES.Add(AnalysisTypes.Baseline_vs_Drug, MWWTest(drugBurden.ToArray(), baselineBurden.ToArray()));

                    break;

                case TESTTYPES.T36:
                    // running seizure stage totals
                    int medicatedScore = 0;
                    int unmedicatedScore = 0;

                    // list of each animal's seizure burden
                    List<double> medicatedBurden = new List<double>();
                    List<double> unmedicatedBurden = new List<double>();

                    foreach (AnimalType animal in animals)
                    {
                        // Break up meals into both groups
                        List<MealType> baselineMeals = animal.Meals.Where(m => m.type == "U").ToList();
                        List<MealType> medicatedMeals = animal.Meals.Where(m => m.type == "M").ToList();

                        // Get meal times
                        List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                        List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                        // Number of days for each group
                        float baselineDays = (float)((baselineTimes.Max() - baselineTimes.Min()) / 24);
                        float medicatedDays = (float)((medicatedTimes.Max() - medicatedTimes.Min()) / 24);

                        if (animal.Sz.Count > 0)
                        {
                            unmedicatedScore = GetSeverityScore(animal.Sz, baselineTimes, Earliest);
                            medicatedScore = GetSeverityScore(animal.Sz, medicatedTimes, Earliest);
                        }
                        if (unmedicatedScore != default)
                        { unmedicatedBurden.Add(unmedicatedScore / baselineDays); }
                        if (medicatedScore != default)
                        { medicatedBurden.Add(medicatedScore / medicatedDays); }
                    }

                    SzMetrics medicatedMetrics = new SzMetrics(TRTTYPE.Drug);
                    medicatedMetrics.szBurden = medicatedBurden.Average();
                    medicatedMetrics.burdenSEM = SEM(medicatedBurden);
                    medicatedMetrics.numAnimals = medicatedBurden.Count();

                    SzMetrics unmedicatedMetrics = new SzMetrics(TRTTYPE.Baseline);
                    unmedicatedMetrics.szBurden = unmedicatedBurden.Average();
                    unmedicatedMetrics.burdenSEM = SEM(unmedicatedBurden);
                    unmedicatedMetrics.numAnimals = unmedicatedBurden.Count();

                    seizureData.Add(TRTTYPE.Drug, medicatedMetrics);
                    seizureData.Add(TRTTYPE.Baseline, unmedicatedMetrics);

                    break;

                case TESTTYPES.IAK:

                    break;
            }
        }
        public void SeizureFreedom(List<AnimalType> animals, DateTime Earliest)
        {
            // This method answers the question: Did animal have seizure during treatment?
            int baselineSzFreedom = 0;
            int vehicleSzFreedom = 0;
            int drugSzFreedom = 0;

            if (test == TESTTYPES.T35)
            {
                foreach (AnimalType animal in animals)
                {
                    // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                    List<InjectionType> vehicleI = animal.Injections.Where(I => I.ADDID == "vehicle").ToList();
                    List<InjectionType> drugI = animal.Injections.Where(I => I.ADDID != "vehicle").ToList();
                    List<double> vehicleTimes = vehicleI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                    List<double> drugTimes = drugI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                    List<SeizureType> drugSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= drugTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= drugTimes.Max() && S.Severity != -1).ToList();
                    List<SeizureType> vehicleSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= vehicleTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= vehicleTimes.Max() && S.Severity != -1).ToList();
                    double baselineTime = -1; // initialize baseline
                    if (drugTimes.Min() < vehicleTimes.Min())
                    {
                        baselineTime = drugTimes.Min();
                    }
                    else if (drugTimes.Min() > vehicleTimes.Min())
                    {
                        baselineTime = vehicleTimes.Min();
                    }

                    // where there were baseline seizures
                    List<SeizureType> baselineSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < baselineTime && S.Severity != -1).ToList();

                    if (drugSz.Count > 0)
                    { }
                    else
                    { drugSzFreedom++; }
                    if (vehicleSz.Count > 0)
                    { }
                    else
                    { vehicleSzFreedom++; }
                    if (baselineSz.Count > 0)
                    { }
                    else
                    { baselineSzFreedom++; }

                }
                seizureData[TRTTYPE.Baseline].szFreedom = baselineSzFreedom;
                seizureData[TRTTYPE.Vehicle].szFreedom = vehicleSzFreedom;
                seizureData[TRTTYPE.Drug].szFreedom = drugSzFreedom;
            }
            else if (test == TESTTYPES.T36) // Test 36
            {
                foreach (AnimalType animal in animals)
                {
                    // Break up meals into both groups
                    List<MealType> baselineMeals = animal.Meals.Where(m => m.type.ToUpper() == "U").ToList();
                    List<MealType> medicatedMeals = animal.Meals.Where(m => m.type.ToUpper() == "M").ToList();

                    // Get meal times
                    List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                    List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                    // seizures during unmedicated and medicated meals
                    List<SeizureType> drugSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= medicatedTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= medicatedTimes.Max() && S.Severity != -1).ToList();
                    List<SeizureType> baselineSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < medicatedTimes.Min() || S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours > medicatedTimes.Max() && S.Severity != -1).ToList();

                    // Answer seizure freedom
                    if (drugSz.Count > 0)
                    { }
                    else
                    { drugSzFreedom += 1; }
                    if (baselineSz.Count > 0)
                    { }
                    else
                    { baselineSzFreedom += 1; }
                    seizureData[TRTTYPE.Baseline].szFreedom = baselineSzFreedom;
                    seizureData[TRTTYPE.Drug].szFreedom = drugSzFreedom;
                }
            }
        }
        public double SEM(List<double> sz)
        {
            double sem;
            double sigma;
            double variance = 0;
            double mean = sz.Average();
            int n = sz.Count();
            // find standard deviation of sz burden
            for (int i = 0; i < sz.Count; i++)
            {
                variance += (float)Math.Pow(sz[i] - mean, 2) / (n - 1);
            }
            sigma = Math.Sqrt(variance);
            sem = sigma / Math.Sqrt(n - 1);
            return Math.Round(sem, 2);
        }
        public int CompareSeizures(SeizureType seizure, string animalID)
        {
            // dictionary to replace parsed integers with string
            Dictionary<int, string> numbers = new Dictionary<int, string>();
            numbers.Add(1, "one"); numbers.Add(2, "two");
            numbers.Add(3, "three"); numbers.Add(4, "four");
            numbers.Add(5, "five"); numbers.Add(6, "six");
            numbers.Add(7, "one"); numbers.Add(8, "eight");
            numbers.Add(9, "nine");

            int bubbleSeverity = default; // default
            int noteSeverity = default; // default
            int finalStage;
            if (seizure.Severity >= 0 && seizure.Severity <= 5)
            {
                bubbleSeverity = seizure.Severity;
            }
            if (seizure.Notes.Length > 0)
            {
                noteSeverity = ParseSeizure(seizure.Notes);
            }

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
                        // step backward thru seizure notes and insert word corresponding to number in notes
                        // solution to save conflict results between bubble and notes
                        if (int.TryParse(seizure.Notes[i].ToString(), out int result))
                        {
                            string numberToInsert = numbers[result];
                            seizure.Notes = seizure.Notes.Insert(i, numberToInsert);
                            seizure.Notes = seizure.Notes.Remove(i + numberToInsert.Length, 1);
                        }
                    }
                }
            }// Do something
            else { finalStage = bubbleSeverity; }

            return finalStage;
        }
        public int ParseSeizure(string note)
        {
            int severity = default;
            string storeNum = String.Join("", note.Where(char.IsDigit));
            if (storeNum.Length > 0)
            {
                severity = int.Parse(storeNum);
            }
            return severity;
        }
    }
}

