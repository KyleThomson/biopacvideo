using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Testing;

namespace ProjectManager
{
    public class SeizureAnalysis
    {
        public float vehicleSEM;
        public float drugSEM;
        public float baselineSEM;

        public List<string> groups;

        public float avgDrugBurden; public float avgVehBurden; public float avgBaseBurden;
        public int vehFreedomSum; public int drugFreedomSum; public int baseFreedomSum;

        public List<float> drugBurdenList; public List<float> vehicleBurdenList; public List<float> baselineBurdenList;
        public List<int> drugFreedomList; public List<int> vehicleFreedomList; public List<int> baselineFreedomList;

        public Dictionary<string, double> PVALUES = new Dictionary<string, double>();
        public SeizureAnalysis()
        {
            
        }
        public TESTTYPES test;
        public void SzFreedomSignificance(int drugAnimals, int baselineAnimals, int vehicleAnimals)
        {
            // 
            // Get integers for fisher exact test
            if (test == TESTTYPES.T35)
            {
                // Do Drug vs. Baseline comparison first
                int drugAndBaselineAnimals = drugAnimals + baselineAnimals; // total number of animals for drug and baseline
                int seizedDrugAnimals = drugAnimals - drugFreedomSum; int seizedBaselineAnimals = baselineAnimals - baseFreedomSum;
                int notSeizedDrugAnimals = drugFreedomSum; int notSeizedBaselineAnimals = baseFreedomSum;
                double drugVsBaselinePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedBaselineAnimals, notSeizedDrugAnimals, notSeizedBaselineAnimals, drugAndBaselineAnimals);

                // Do Vehicle vs. Baseline comparison next
                int vehicleAndDrugAnimals = vehicleAnimals + drugAnimals; // total number of animals for vehicle and baseline
                int seizedVehicleAnimals = vehicleAnimals - vehFreedomSum;
                int notSeizedVehicleAnimals = vehFreedomSum;
                double drugVsVehiclePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedVehicleAnimals, notSeizedDrugAnimals, notSeizedVehicleAnimals, vehicleAndDrugAnimals);
                PVALUES.Add("SF: Drug vs Baseline", drugVsBaselinePvalue);
                PVALUES.Add("SF: Drug vs Vehicle", drugVsVehiclePvalue);
            }
            else if (test == TESTTYPES.T36)
            {
                // Do Drug vs. Baseline comparison ONLY
                int drugAndBaselineAnimals = drugAnimals + baselineAnimals; // total number of animals for drug and baseline
                int seizedDrugAnimals = drugAnimals - drugFreedomSum; int seizedBaselineAnimals = baselineAnimals - baseFreedomSum;
                int notSeizedDrugAnimals = drugFreedomSum; int notSeizedBaselineAnimals = baseFreedomSum;
                double drugVsBaselinePvalue = ExtraMath.FisherExact(seizedDrugAnimals, seizedBaselineAnimals, notSeizedDrugAnimals, notSeizedBaselineAnimals, drugAndBaselineAnimals);
            }

        }
        public double SzBurdenSignificance(double[] sample1, double[] sample2)
        {
            // generate new mww test
            var mwwTest = new MannWhitneyWilcoxonTest(sample1, sample2, TwoSampleHypothesis.FirstValueIsSmallerThanSecond , exact: false);

            // extract pvalue and return
            double pvalue = mwwTest.PValue;
            return pvalue;
        }
        public void SeizureBurden(AnimalType animal, DateTime Earliest)
        {
            float vehicleBurden;
            float drugBurden;
            float baselineBurden;
            int bubbleSeverity = default; // default
            int noteSeverity = default; // default

            // running seizure stage totals
            int vehicleScore = 0;
            int drugScore = 0;
            int baselineScore = 0;

            // counts for seizures
            int vCounts = 0;
            int dCounts = 0;
            int blCounts = 0;

            List<int> vehicleSz = new List<int>();
            List<int> drugSz = new List<int>();
            List<int> baselineSz = new List<int>();
            
            if (test == TESTTYPES.T35)
            {
                // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                List<InjectionType> vehicleI = animal.Injections.Where(I => I.ADDID == "vehicle").ToList();
                List<InjectionType> drugI = animal.Injections.Where(I => I.ADDID != "vehicle").ToList();
                List<double> vehicleTimes = vehicleI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                List<double> drugTimes = drugI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();

                // Number of days for each group
                float vehicleDays = (float)((vehicleTimes.Max() - vehicleTimes.Min()) / 24);
                float drugDays = (float)((drugTimes.Max() - drugTimes.Min()) / 24);
                // Initialize days in baseline. Not sure how else to do this since compiler doesn't like variables being designed in a conditional statement
                float baselineDays = -1;
                if (drugTimes.Min() < vehicleTimes.Min())
                {
                    baselineDays = (float)(drugTimes.Min() / 24);
                }
                else if (drugTimes.Min() > vehicleTimes.Min())
                {
                    baselineDays = (float)(vehicleTimes.Min() / 24);
                }

                if (animal.Sz.Count > 0)
                {
                    foreach (SeizureType S in animal.Sz)
                    {
                        if (S.Severity >= 0 && S.Severity <= 5) { bubbleSeverity = S.Severity; }

                        else if (S.Severity == 0) { bubbleSeverity = 1; }

                        if (S.Notes.Length > 0)
                        {
                            noteSeverity = ParseSeizure(S.Notes);
                            if (noteSeverity <= 5 && noteSeverity >= 0) { }

                            else if (noteSeverity == 0) { noteSeverity = 1; }

                            else { noteSeverity = -1; }
                        }

                        // Check if bubble and note match and flag if it doesn't
                        if (bubbleSeverity != noteSeverity) { S.stageAgreement = false; }// Do something

                        double szTime = S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours;
                        // Add seizure severity to running total depending on treatment that seizure occurred during

                        //Seizure happened during vehicle treatment
                        if (szTime >= vehicleTimes.Min() && szTime <= vehicleTimes.Max())
                        {
                            vehicleScore += Math.Max(bubbleSeverity, noteSeverity);
                            vCounts++;
                        }
                        //Seizure happened during drug treatment
                        else if (szTime >= drugTimes.Min() && szTime <= drugTimes.Max())
                        {
                            drugScore += Math.Max(bubbleSeverity, noteSeverity);
                            dCounts++;
                        }
                        //Seizure happened outside of both treatments
                        else
                        {
                            baselineScore += Math.Max(bubbleSeverity, noteSeverity);
                            blCounts++;
                        }

                        // reset severity
                        bubbleSeverity = default;
                        noteSeverity = default;
                    }
                    // Set seizure burdens for each group
                    vehicleBurden = vehicleScore / vehicleDays;
                    drugBurden = drugScore / drugDays;
                    baselineBurden = baselineScore / baselineDays;

                    // szmetrics to add burdens
                    foreach (SzMetrics M in animal.metrics)
                    {
                        if (M.treatment == TRTTYPE.Baseline) 
                        { M.szBurden = baselineBurden; }

                        else if (M.treatment == TRTTYPE.Vehicle)
                        { M.szBurden = vehicleBurden; }

                        else if (M.treatment == TRTTYPE.Drug)
                        { M.szBurden = drugBurden; }
                    }
                }
            }
            else if (test == TESTTYPES.T36)// Test 36
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
                    foreach (SeizureType S in animal.Sz)
                    {
                        if (S.Severity > 0 && S.Severity <= 5)
                        {
                            bubbleSeverity = S.Severity;
                        }
                        else if ( S.Severity == 0) { bubbleSeverity = 1; }
                        if (S.Notes.Length > 0)
                        {
                            string storeNum = String.Join("", S.Notes.Where(char.IsDigit));
                            if (storeNum.Length > 0)
                            {
                                if (int.Parse(storeNum) <= 5 && int.Parse(storeNum) > 0)
                                {
                                    noteSeverity = int.Parse(storeNum);
                                }
                                else if ( int.Parse(storeNum) == 0) { noteSeverity = 1; }
                                else
                                {
                                    noteSeverity = -1;
                                }
                            }
                        }
                        double szTime = S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours;
                        // Add seizure severity to running total depending on treatment that seizure occurred during
                        //Seizure happened during drug treatment
                        if (szTime >= medicatedTimes.Min() && szTime <= medicatedTimes.Max())
                        {
                            drugScore += Math.Max(bubbleSeverity, noteSeverity);
                            dCounts++;
                        }
                        //Seizure happened outside of both treatments
                        else
                        {
                            baselineScore += Math.Max(bubbleSeverity, noteSeverity);
                            blCounts++;
                        }
                        // reset severity
                        bubbleSeverity = default;
                        noteSeverity = default;
                    }
                    // Set seizure burdens for each group
                    drugBurden = drugScore / medicatedDays;
                    baselineBurden = baselineScore / baselineDays;

                    // szmetrics
                    foreach (SzMetrics M in animal.metrics)
                    {
                        if (M.treatment == TRTTYPE.Baseline)
                        { M.szBurden = baselineBurden; }

                        else if (M.treatment == TRTTYPE.Drug)
                        { M.szBurden = drugBurden; }
                    }
                }
            }
        }
        public void SeizureFreedom(AnimalType animal, DateTime Earliest)
        {
            // This method answers the question: Did animal have seizure during treatment?
            int baselineSzFreedom;
            int vehicleSzFreedom;
            int drugSzFreedom;
            if (test == TESTTYPES.T35)
            {
                // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                List<InjectionType> vehicleI = animal.Injections.Where(I => I.ADDID == "vehicle").ToList();
                List<InjectionType> drugI = animal.Injections.Where(I => I.ADDID != "vehicle").ToList();
                List<double> vehicleTimes = vehicleI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                List<double> drugTimes = drugI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                List<SeizureType> drugSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= drugTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= drugTimes.Max()).ToList();
                List<SeizureType> vehicleSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= vehicleTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= vehicleTimes.Max()).ToList();
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
                List<SeizureType> baselineSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < baselineTime).ToList();

                if (drugSz.Count > 0)
                {
                    drugSzFreedom = 0;
                }
                else
                {
                    drugSzFreedom = 1;
                }
                if (vehicleSz.Count > 0)
                {
                    vehicleSzFreedom = 0;
                }
                else
                {
                    vehicleSzFreedom = 1;
                }
                if (baselineSz.Count > 0)
                {
                    baselineSzFreedom = 0;
                }
                else
                {
                    baselineSzFreedom = 1;
                }

                // szmetrics
                foreach (SzMetrics M in animal.metrics)
                {
                    if (M.treatment == TRTTYPE.Baseline)
                    { M.szFreedom = baselineSzFreedom; }

                    else if (M.treatment == TRTTYPE.Vehicle)
                    { M.szFreedom = vehicleSzFreedom; }

                    else if (M.treatment == TRTTYPE.Drug)
                    { M.szFreedom = drugSzFreedom; }
                }
            }
            else if (test == TESTTYPES.T36) // Test 36
            {
                // Break up meals into both groups
                List<MealType> baselineMeals = animal.Meals.Where(m => m.type.ToUpper() == "U").ToList();
                List<MealType> medicatedMeals = animal.Meals.Where(m => m.type.ToUpper() == "M").ToList();

                // Get meal times
                List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                // seizures during unmedicated and medicated meals
                List<SeizureType> drugSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= medicatedTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= medicatedTimes.Max()).ToList();
                List<SeizureType> baselineSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < medicatedTimes.Min() || S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours > medicatedTimes.Max()).ToList();

                // Answer seizure freedom
                if (drugSz.Count > 0)
                {
                    drugSzFreedom = 0;
                }
                else
                {
                    drugSzFreedom = 1;
                }
                if (baselineSz.Count > 0)
                {
                    baselineSzFreedom = 0;
                }
                else
                {
                    baselineSzFreedom = 1;
                }

                // szmetrics
                foreach (SzMetrics M in animal.metrics)
                {
                    if (M.treatment == TRTTYPE.Baseline)
                    { M.szFreedom = baselineSzFreedom; }

                    else if (M.treatment == TRTTYPE.Drug)
                    { M.szFreedom = drugSzFreedom; }
                }
            }
        }
        public float SEM(List<float> sz)
        {
            float sem;
            float sigma;
            float variance = 0;
            // find standard deviation of sz burden
            for (int i = 0; i < sz.Count; i++)
            {
                variance += (float)(Math.Pow(sz[i] - sz.Average(), 2));
            }
            sigma = (float)Math.Sqrt(variance / (sz.Count - 1));
            sem = (float)Math.Round((float)(sigma / Math.Sqrt(sz.Count - 1)), 2);
            return (float)Math.Round(sem,2);
        }
        public int CompareSeizures(SeizureType seizure, string animalID)
        {
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
                if (int.Parse(storeNum) <= 5 && int.Parse(storeNum) >= 0)
                {
                    severity = int.Parse(storeNum);
                }
                else
                {
                    severity = -1;
                }
            }
            return severity;
        }
        public void AverageBurdens(List<AnimalType> animals)
        {
            // Iterate thru each animal and each list of SzMetrics to acquire list of burdens to compute average burden
            // Also pass list of burdens to SEM()
            List<float> allVehicleSzBurdens = new List<float>();
            List<float> allBaselineSzBurdens = new List<float>();
            List<float> allDrugSzBurdens = new List<float>();
            foreach (AnimalType A in animals)
            {
                foreach (SzMetrics M in A.metrics)
                {
                    if (M.treatment == TRTTYPE.Baseline)
                    { allBaselineSzBurdens.Add(M.szBurden); }

                    else if (M.treatment == TRTTYPE.Vehicle)
                    { allVehicleSzBurdens.Add(M.szBurden); }

                    else if (M.treatment == TRTTYPE.Drug)
                    { allDrugSzBurdens.Add(M.szBurden); }
                }
            }
            if (test == TESTTYPES.T35)
            {
                if (allDrugSzBurdens != null) { avgDrugBurden = (float)Math.Round(allDrugSzBurdens.Average(), 2); }
                if (allBaselineSzBurdens != null) { avgBaseBurden = (float)Math.Round(allBaselineSzBurdens.Average(), 2); }
                if (allVehicleSzBurdens != null) { avgVehBurden = (float)Math.Round(allVehicleSzBurdens.Average(), 2); }

                // SEM
                baselineSEM = SEM(allBaselineSzBurdens);
                drugSEM = SEM(allDrugSzBurdens);
                vehicleSEM = SEM(allVehicleSzBurdens);
            }
            else if (test == TESTTYPES.T36)
            {
                if (allDrugSzBurdens != null) { avgDrugBurden = (float)Math.Round(allDrugSzBurdens.Average(), 2); }
                if (allBaselineSzBurdens != null) { avgBaseBurden = (float)Math.Round(allBaselineSzBurdens.Average(), 2); }

                // SEM
                baselineSEM = SEM(allBaselineSzBurdens);
                drugSEM = SEM(allDrugSzBurdens);
            }
            drugBurdenList = allDrugSzBurdens; vehicleBurdenList = allVehicleSzBurdens; baselineBurdenList = allBaselineSzBurdens;
        }
        public void SumFreedoms(List<AnimalType> animals)
        {
            List<int> allBaselineSzFreedoms = new List<int>();
            List<int> allVehicleSzFreedoms = new List<int>();
            List<int> allDrugSzFreedoms = new List<int>();
            foreach (AnimalType A in animals)
            {
                foreach (SzMetrics M in A.metrics)
                {
                    if (M.treatment == TRTTYPE.Baseline)
                    { allBaselineSzFreedoms.Add(M.szFreedom); }

                    else if (M.treatment == TRTTYPE.Vehicle)
                    { allVehicleSzFreedoms.Add(M.szFreedom); }

                    else if (M.treatment == TRTTYPE.Drug)
                    { allDrugSzFreedoms.Add(M.szFreedom); }
                }
            }
            if (test == TESTTYPES.T35)
            {
                drugFreedomSum = allDrugSzFreedoms.Sum();
                baseFreedomSum = allBaselineSzFreedoms.Sum();
                vehFreedomSum = allVehicleSzFreedoms.Sum();

                drugFreedomList = allDrugSzFreedoms;
                baselineFreedomList = allBaselineSzFreedoms;
                vehicleFreedomList = allVehicleSzFreedoms;
            }
            else if (test == TESTTYPES.T36)
            {
                drugFreedomSum = allDrugSzFreedoms.Sum();
                baseFreedomSum = allBaselineSzFreedoms.Sum();
            }
        }
    }
}

