﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class SeizureAnalysis
    {
        public float vehicleSEM;
        public float drugSEM;
        public float baselineSEM;
        public List<int> vehicleSzFreedom;
        public List<int> baselineSzFreedom;
        public List<int> drugSzFreedom;
        public List<float> baselineBurdens;
        public List<float> vehicleBurdens;
        public List<float> drugBurdens;
        public List<string> groups;

        public float avgDrugBurden; public float avgVehBurden; public float avgBaseBurden;
        public int vehFreedomSum; public int drugFreedomSum; public int baseFreedomSum;

        public SeizureAnalysis()
        {
            baselineBurdens = new List<float>();
            vehicleBurdens = new List<float>();
            drugBurdens = new List<float>();

            baselineSzFreedom = new List<int>();
            vehicleSzFreedom = new List<int>();
            drugSzFreedom = new List<int>();
        }
        public TESTTYPES test;
        public void SeizureBurden(AnimalType animal, DateTime Earliest)
        {
            float vehicleBurden;
            float drugBurden;
            float baselineBurden;
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
                        if (S.Severity >= 0 && S.Severity <= 5)
                        {
                            bubbleSeverity = S.Severity;
                        }
                        if (S.Notes.Length > 0)
                        {
                            noteSeverity = ParseSeizure(S.Notes);
                            if (noteSeverity <= 5 && noteSeverity >= 0)
                            {
                                // Nothing
                            }
                            else
                            {
                                noteSeverity = -1;
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

                    // add burden to list
                    vehicleBurdens.Add(vehicleBurden);
                    drugBurdens.Add(drugBurden);
                    baselineBurdens.Add(baselineBurden);
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

                    // add burden to list
                    drugBurdens.Add(drugBurden);
                    baselineBurdens.Add(baselineBurden);
                }
            }
        }
        public void SeizureFreedom(AnimalType animal, DateTime Earliest)
        {
            // This method answers the question: Did animal have seizure during treatment?

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
                     drugSzFreedom.Add(0);
                }
                else
                {
                    drugSzFreedom.Add(1);
                }
                if (vehicleSz.Count > 0)
                {
                    vehicleSzFreedom.Add(0);
                }
                else
                {
                    vehicleSzFreedom.Add(1);
                }
                if (baselineSz.Count > 0)
                {
                    baselineSzFreedom.Add(0);
                }
                else
                {
                    baselineSzFreedom.Add(1);
                }
            }
            else if (test == TESTTYPES.T36) // Test 36
            {
                // Break up meals into both groups
                List<MealType> baselineMeals = animal.Meals.Where(m => m.type == "U").ToList();
                List<MealType> medicatedMeals = animal.Meals.Where(m => m.type == "M").ToList();

                // Get meal times
                List<double> baselineTimes = baselineMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();
                List<double> medicatedTimes = medicatedMeals.Select(m => (double)m.d.Subtract(Earliest).TotalHours).ToList();

                // seizures during unmedicated and medicated meals
                List<SeizureType> drugSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours >= medicatedTimes.Min() && S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours <= medicatedTimes.Max()).ToList();
                List<SeizureType> baselineSz = animal.Sz.Where(S => S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours < medicatedTimes.Min() || S.d.Date.Subtract(Earliest).TotalHours + S.t.TotalHours > medicatedTimes.Max()).ToList();

                // Answer seizure freedom
                if (drugSz.Count > 0)
                {
                    drugSzFreedom.Add(0);
                }
                else
                {
                    drugSzFreedom.Add(1);
                }
                if (baselineSz.Count > 0)
                {
                    baselineSzFreedom.Add(0);
                }
                else
                {
                    baselineSzFreedom.Add(1);
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
                stageDialog.ShowDialog(bubbleSeverity, noteSeverity, ID);
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
        public void AverageBurdens()
        {
            if (test == TESTTYPES.T35)
            {
                avgDrugBurden = (float)Math.Round(drugBurdens.Average(),2);
                avgBaseBurden = (float)Math.Round(baselineBurdens.Average(),2);
                avgVehBurden = (float)Math.Round(vehicleBurdens.Average(),2);
            }
            else if (test == TESTTYPES.T36)
            {
                avgDrugBurden = (float)Math.Round(drugBurdens.Average(),2);
                avgBaseBurden = (float)Math.Round(baselineBurdens.Average(),2);
            }
        }
        public void SumFreedoms()
        {
            if (test == TESTTYPES.T35)
            {
                drugFreedomSum = drugSzFreedom.Sum();
                baseFreedomSum = baselineSzFreedom.Sum();
                vehFreedomSum = vehicleSzFreedom.Sum();
            }
            else if (test == TESTTYPES.T36)
            {
                drugFreedomSum = drugSzFreedom.Sum();
                baseFreedomSum = baselineSzFreedom.Sum();
            }
        }
        
    }
}

