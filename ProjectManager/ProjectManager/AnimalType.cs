using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class AnimalType
    {
        public string ID;
        public List<WeightType> WeightInfo;
        public List<SeizureType> Sz;
        public List<MealType> Meals;
        public List<ImportantDateType> ImportantDates;
        public List<BloodDrawType> BloodDraws;
        public List<RemovalType> Removals;
        public GroupType Group;
        public List<InjectionType> Injections;
        public float vehicleBurden;
        public float drugBurden;
        public float baselineBurden;
        public int vehicleSzCount;
        public int drugSzCount;
        public int baselineSzCount;

        public int baselineFreedom;
        public int drugFreedom;
        public int vehicleFreedom;
        public AnimalType()
        {
            Sz = new List<SeizureType>();
            WeightInfo = new List<WeightType>();
            Meals = new List<MealType>();
            ImportantDates = new List<ImportantDateType>();
            Group = new GroupType();
            BloodDraws = new List<BloodDrawType>();
            Removals = new List<RemovalType>();
            Injections = new List<InjectionType>();
        }
        public void SeizureBurden(string test, DateTime Earliest)
        {
            int bubbleSeverity = default; // default
            int noteSeverity = default; // default
            int vehicleScore = 0;
            int drugScore = 0;
            int baselineScore = 0;
            int vCounts = 0;
            int dCounts = 0;
            int blCounts = 0;

            List<int> vehicleSz = new List<int>();
            List<int> drugSz = new List<int>();
            List<int> baselineSz = new List<int>();

            if (test == "T35")
            {

                // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                List<InjectionType> vehicleI = Injections.Where(I => I.ADDID == "Vehicle").ToList();
                List<InjectionType> drugI = Injections.Where(I => I.ADDID != "Vehicle").ToList();
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


                if (Sz.Count > 0)
                {
                    foreach (SeizureType S in Sz)
                    {
                        if (S.Severity >= 0 && S.Severity <= 5)
                        {
                            bubbleSeverity = S.Severity;

                        }
                        if (S.Notes.Length > 0)
                        {
                            string storeNum = String.Join("", S.Notes.Where(char.IsDigit));
                            if (storeNum.Length > 0)
                            {
                                if (int.Parse(storeNum) <= 5 && int.Parse(storeNum) >= 0)
                                {
                                    noteSeverity = int.Parse(storeNum);
                                }
                                else
                                {
                                    noteSeverity = -1;
                                }
                            }

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

                    // Set seizure counts for each group
                    vehicleSzCount = vCounts;
                    drugSzCount = dCounts;
                    baselineSzCount = blCounts;
                }
            }
            else if (test == "T36")// Test 36
            {
                // Break up meals into both groups
                List<MealType> baselineMeals = Meals.Where(m => m.type == "U").ToList();
                List<MealType> medicatedMeals = Meals.Where(m => m.type == "M").ToList();

                // Get meal times
                List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                // Number of days for each group
                float baselineDays = (float)((baselineTimes.Max() - baselineTimes.Min()) / 24);
                float medicatedDays = (float)((medicatedTimes.Max() - medicatedTimes.Min()) / 24);

                if (Sz.Count > 0)
                {
                    foreach (SeizureType S in Sz)
                    {
                        if (S.Severity >= 0 && S.Severity <= 5)
                        {
                            bubbleSeverity = S.Severity;
                        }
                        if (S.Notes.Length > 0)
                        {
                            string storeNum = String.Join("", S.Notes.Where(char.IsDigit));
                            if (storeNum.Length > 0)
                            {
                                if (int.Parse(storeNum) <= 5 && int.Parse(storeNum) >= 0)
                                {
                                    noteSeverity = int.Parse(storeNum);
                                }
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

                    // Set seizure counts for each group
                    drugSzCount = dCounts;
                    baselineSzCount = blCounts;
                }
            }

        }
        public void SzFreedom(string test, DateTime Earliest)
        {
            // This method answers the question: Did animal have seizure during treatment?

            if (test == "T35")
            {
                // Find drug and vehicle injections so we can determine if the seizure occurred during drug/vehicle treatment
                List<InjectionType> vehicleI = Injections.Where(I => I.ADDID == "Vehicle").ToList();
                List<InjectionType> drugI = Injections.Where(I => I.ADDID != "Vehicle").ToList();
                List<double> vehicleTimes = vehicleI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                List<double> drugTimes = drugI.Select(o => (double)o.TimePoint.Subtract(Earliest).TotalHours).ToList();
                List<SeizureType> drugSz = Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= drugTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= drugTimes.Max()).ToList();
                List<SeizureType> vehicleSz = Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= vehicleTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= vehicleTimes.Max()).ToList();
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
                List<SeizureType> baselineSz = Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < baselineTime).ToList();

                if (drugSz.Count > 0)
                {
                    drugFreedom = 0;
                }
                else
                {
                    drugFreedom = 1;
                }
                if (vehicleSz.Count > 0)
                {
                    vehicleFreedom = 0;
                }
                else
                {
                    vehicleFreedom = 1;
                }
                if (baselineSz.Count > 0)
                {
                    baselineFreedom = 0;
                }
                else
                {
                    baselineFreedom = 1;
                }
            }
            else // Test 36
            {
                // Break up meals into both groups
                List<MealType> baselineMeals = Meals.Where(m => m.type == "U").ToList();
                List<MealType> medicatedMeals = Meals.Where(m => m.type == "M").ToList();

                // Get meal times
                List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                // seizures during unmedicated and medicated meals
                List<SeizureType> drugSz = Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= medicatedTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= medicatedTimes.Max()).ToList();
                List<SeizureType> baselineSz = Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < medicatedTimes.Min() || S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours > medicatedTimes.Max()).ToList();

                // Answer seizure freedom
                if (drugSz.Count > 0)
                {
                    drugFreedom = 0;
                }
                else
                {
                    drugFreedom = 1;
                }
                if (baselineSz.Count > 0)
                {
                    baselineFreedom = 0;
                }
                else
                {
                    baselineFreedom = 1;
                }
            }
        }

    }
}
