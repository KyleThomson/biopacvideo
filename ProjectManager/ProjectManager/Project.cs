using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace ProjectManager
{
    public class ExportType
    {
        public bool Sz;
        public bool Pellet;
        public bool Med;
        public bool wt;
        public bool SzTime;
        public bool Meal;
        public bool DetailList;
        public bool Notes;
        public bool SeverityIndx;
        public bool BloodDraw;
        public bool BloodDrawList;
        public bool Injections;
        public bool InjectionsList;
        public bool binSz;
        
        public ExportType()
        {
            Sz = false;
            Pellet = false;
            Med = false;
            wt = false;
            SzTime = false;
            Meal = false;
            DetailList = false;
            Notes = false;
            SeverityIndx = false;
            BloodDraw = false;
            BloodDrawList = false;
            Injections = false;
            InjectionsList = false;
            binSz = false;
        }

    }
    public class LabelType
    {
        public string Name;
        public int IDNum;
 
        public LabelType(string a, string b) 
        {
            int.TryParse(a, out IDNum);
            Name = b;
        }
        public string LabelMatch(int IDtest, string returnstr)
        {
            if (IDtest == IDNum)
                return Name;
            else
                return returnstr;
        }
    }
    public class GroupType
    {
        public string Name;
        public int count;
        public int IDNum; 
        public GroupType()
        {
            Name = "";
            count = 0;
            IDNum = 0;
        }
        public GroupType(string a, string b, string c)
        {
            Name = b;
            int.TryParse(a, out IDNum);
            int.TryParse(c, out count);            
        }
    }
    public class RemovalType
    {
        public DateTime dt;        
        public int count;        
        public string pt;
        public RemovalType(string a, string b, string c)
        {
            DateTime.TryParse(a, out dt);            
            int.TryParse(b, out count);
            pt = c;
        }
    }
    public class BloodDrawType
    {
        public DateTime dt;
        public TimeSpan EnteredTime;
        public string ID;
        public BloodDrawType(string a, string b, string c)
        {
            DateTime.TryParse(a, out dt);
            TimeSpan.TryParse(b, out EnteredTime);
            ID = c; 
        }

    }
    public class WeightType
    {
        public double wt;
        public DateTime dt;
        public int pt;
        public WeightType(string a, string b, string c)
        {
            double.TryParse(a, out wt);
            DateTime.TryParse(b, out dt);
            int.TryParse(c, out pt);
        }
    }
    public class MealType
    {
        public DateTime d;
        public string type;
        public int pelletcount;
        public MealType(string a, string b, string c)
        {
            DateTime.TryParse(a, out d);
            type = b;
            int.TryParse(c, out pelletcount);
        }
        public bool Compare(MealType C)
        {
            if ((DateTime.Compare(d, C.d) == 0) && (string.Compare(type, C.type) == 0) &&  (pelletcount == C.pelletcount))
                return true;
            else
                return false;
        }
    }
    public class InjectionType
    {
        public string Route;
        public int Dose;
        public double DoseAmount;
        public int DoseNum;
        public string ADDID;
        public string solvent; 
        public DateTime TimePoint;

        //Date DoseNum ADDID Dose DoseAmount Route Solvent
        public InjectionType(string a, string b, string c, string d, string e, string f, string g)
        {
            DateTime.TryParse(a, out TimePoint);
            int.TryParse(b, out DoseNum); 
            ADDID = c;            
            int.TryParse(d, out Dose);
            double.TryParse(e, out DoseAmount); 
            Route = f;
            solvent = g;
        }
    }
    public class FileType
    {
        public string[] AnimalIDs;
        public int Chans;
        public DateTime Start;
        public TimeSpan Duration;
        public FileType(string[] A, int B, DateTime C, string D)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);            
         }
        public FileType()
        {
        }
        public bool Compare(FileType C)
        {
            if ((DateTime.Compare(Start, C.Start) == 0) && (string.Compare(AnimalIDs[0], C.AnimalIDs[0]) == 0))
                return true;
            else
                return false;
        }
    }
        
    public class SeizureType
    {
        public TimeSpan t;        
        public DateTime d;
        public int length;
        public string Notes;       
        public string file;
        public int Severity; 
        public SeizureType(string a, string b, string c, string e, string f)
        {
            DateTime.TryParse(a, out d);            
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;            
            Notes = c;
            Severity = -1; 
        }
        public SeizureType(string a, string b, string c, string e, string f, string g)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            Notes = c;
            int.TryParse(g, out Severity);
        }
        public bool Compare(SeizureType C)
        {
            if ((DateTime.Compare(d, C.d) == 0) && (TimeSpan.Compare(t, C.t) == 0) && (length == C.length))
                return true;
            else
                return false;
        }        
    }
    public class ImportantDateType
    {
        public int LabelID;
        public DateTime Date;
        public ImportantDateType(string a, string b)
        {
            int.TryParse(a, out LabelID);
            DateTime.TryParse(b, out Date);
        }
        
    }
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
            int bubbleSeverity = default ; // default
            int noteSeverity = default; // default
            int vehicleScore = 0;
            int drugScore= 0;
            int baselineScore= 0;
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
    public class Test35_Stats
    {

    }
    public class SzGraph
    {
        // instance of graph for test 35 or test 36
        public GraphProperties graph;

        // string variables for header info for the pdf
        public string test;
        public string header;
        public string subHeader;
        public string ETSP;
        public string batch;
        public string dose;
        public string frequency;
        public SzGraph(int X, int Y, Project pjt, string testType)
        {
            // Sort project based on test
            if (testType == "T35")
            {
                // Sort by injections
                pjt.T35Sort();
            }
            else
            {
                pjt.T36Sort();
            }
            
            // Find max day of project
            DateTime Earliest = pjt.Files[0].Start.Date;
            DateTime Latest = pjt.Files[pjt.Files.Count - 1].Start.Date;
            double totalHours = Latest.Subtract(Earliest).TotalHours;
            int tempMax = (int)Math.Round(totalHours / 24, 2);

            // Initialize graph by drawing labels, tick points, inputting numbers into GraphProperties
            graph = new GraphProperties(X, Y, tempMax + 2, pjt.Animals.Count);
            graph.DrawAxes(4);
            graph.BoundingBox(4);

            // Save type of test we are doing (test 35 or test 36 for now)
            test = testType;

            // Calculate seizure burdens for animals
            pjt.CalculateSzBurden();
            pjt.SeizureFreedom(); 

            // x tick every 7 days           
            int xTickInterval = 7;
            int numXTicks = ((tempMax + 2) / xTickInterval) + 1;

            // Get axis tick labels for graph properties
            List<string> xTickString = GetXTickLabels(pjt, xTickInterval);
            List<string> yTickString = GetYTickLabels(pjt);

            // Draw ticks and label axes           
            Font aFont = new Font("Arial", 12 * graph.objectScale);
            graph.DrawTicks(numXTicks, pjt.Animals.Count, 3.0F, xTickString, yTickString);
            graph.WriteXLabel("Time (days)", aFont);
            graph.WriteYLabel("Animals", aFont);
        }
        public List<string> GetXTickLabels(Project pjt, int xTickInterval)
        {
            List<string> xTickString = new List<string>();
            //Obtain basis for y and x axis labeling
            for (int i = 0; i <= pjt.Files.Count; i += xTickInterval)
            {
                if (i % xTickInterval == 0)// && i != 0)
                {
                    xTickString.Add((i).ToString());
                }

            }
            return xTickString;
        }
        public List<string> GetYTickLabels(Project pjt)
        {
            List<string> yTickString = new List<string>();
            //Obtain basis for y and x axis labelling
            for (int i = 0; i < pjt.Animals.Count; i++)
            {
                yTickString.Add(pjt.Animals[i].ID);
            }
            return yTickString;
        }
        public void PlotSz(Project pjt)
        {
            // Plot seizures the same for both test 35 and test 36
            int markerSize = 8;
            Color szColor = Color.FromName("Black");
            DateTime Earliest = pjt.Files[0].Start.Date;
            DateTime Latest = pjt.Files[pjt.Files.Count - 1].Start.Date;
            for (int i = 0; i < pjt.Animals.Count; i++)
            {
                float yCoord = i + 1;
                for (int j = 0; j < pjt.Animals[i].Sz.Count; j++)
                {
                    float xCoord = (float)(pjt.Animals[i].Sz[j].d.Date.Subtract(Earliest).TotalHours + pjt.Animals[i].Sz[j].t.TotalHours) / 24;

                    if (pjt.Animals[i].Sz[j].Severity > 0)
                    {
                        graph.PlotPoints(xCoord, yCoord, markerSize, "o", szColor);
                    }
                    else if (pjt.Animals[i].Sz[j].Severity == 0)
                    {
                        graph.PlotPoints(xCoord, yCoord, markerSize / 2, ".", szColor);
                    }

                }
            }
        }
        public void PlotTrt(Project pjt)
        {
            DateTime Earliest = pjt.Files[0].Start.Date;
            float lineWidth = 4;
            Color vehicleColor = Color.FromName("Teal");
            Color drugColor = Color.FromName("Red");

            // If Test 35, use injections to draw lines for treatment
            if (test == "T35")
            {
                for (int i = 0; i < pjt.Animals.Count; i++)
                {
                    float yCoord = (float)(i + 0.5);

                    // Initialize vehicle and drug treatment times
                    List<float> vehicleTimes = new List<float>();
                    List<float> drugTimes = new List<float>();

                    for (int j = 0; j < pjt.Animals[i].Injections.Count; j++)
                    {
                        foreach (InjectionType I in pjt.Animals[i].Injections)
                        {
                            if (I.ADDID == "Vehicle")
                            {
                                vehicleTimes.Add((float)Math.Round(I.TimePoint.Subtract(Earliest).TotalHours / 24, 2));
                            }
                            else
                            {
                                drugTimes.Add((float)Math.Round(I.TimePoint.Subtract(Earliest).TotalHours / 24, 2));
                            }
                        }
                        // Draw vehicle line
                        if (vehicleTimes.Count > 1)
                        {
                            graph.Line(vehicleTimes[0], yCoord, vehicleTimes[vehicleTimes.Count - 1], yCoord, lineWidth, vehicleColor);
                        }


                        // Draw drug line
                        if (drugTimes.Count > 1)
                        {
                            graph.Line(drugTimes[0], yCoord, drugTimes[drugTimes.Count - 1], yCoord, lineWidth, drugColor);
                        }


                    }
                }
            }
            else if (test == "T36")
            {
                Color unmedicatedColor = Color.FromName("Blue");
                float animalCounter = 0.5F;
                foreach (AnimalType A in pjt.Animals)
                {
                    // Get y coordinate
                    float yCoord = animalCounter;
                    animalCounter++;

                    // Initialize vehicle and drug treatment times
                    List<float> vehicleTimes = new List<float>();
                    List<float> drugTimes = new List<float>();

                    foreach (MealType M in A.Meals)
                    {
                        float xCoord = (float)M.d.Subtract(Earliest).TotalHours / 24;
                        if (M.type == "M")
                        {         
                            graph.PlotPoints(xCoord, yCoord, 8, ".", drugColor);
                        }
                        else
                        {
                            graph.PlotPoints(xCoord, yCoord, 8, ".", unmedicatedColor);
                        }
                    }

                }

            }
        }
        public void Legend()
        {
            // Method that draws on legend for injection type and seizure type
            int markerSize = 8;
            Font legendFont = new Font("Arial", 12 * graph.objectScale);
            SolidBrush legendBrush = new SolidBrush(Color.Black);
            Pen drugPen = new Pen(Brushes.Red);
            drugPen.Width = 4F * graph.objectScale;
            Pen vehiclePen = new Pen(Brushes.Teal);
            vehiclePen.Width = 4F * graph.objectScale;
            Pen szPen = new Pen(Brushes.Black);

            // If Test 35
            // Placement point for drug treatment legend
            if (test == "T35")
            {
                string drugString = "Drug Treatment";
                PointF drugStringPoint = new PointF(graph.xAxisStart, (float)(graph.axes[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.DrawLine(drugPen, drugStringPoint.X, drugStringPoint.Y + drugStringSize.Height, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height);

                // If Test 35
                // Placement for vehicle treatment
                string vehicleString = "Vehicle Treatment";
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.xAxisLength - vehicleStringSize.Width, (float)(graph.axes[0].Y * 1.1));
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.DrawLine(vehiclePen, vehicleStringPoint.X, vehicleStringPoint.Y + vehicleStringSize.Height, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height);
            }
            else if (test == "T36")
            {
                string drugString = "Medicated Meal:";
                SolidBrush drugBrush = new SolidBrush(Color.Red);
                PointF drugStringPoint = new PointF(graph.xAxisStart, (float)(graph.axes[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.FillEllipse(drugBrush, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);

                // If Test 35
                // Placement for vehicle treatment
                string vehicleString = "Unmedicated Meal:";
                SolidBrush unmedicatedBrush = new SolidBrush(Color.Blue);
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.xAxisLength - vehicleStringSize.Width, (float)(graph.axes[0].Y * 1.1));
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.FillEllipse(unmedicatedBrush, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);
            }

            // Placement for focal seizure
            string focalSzString = "Focal Seizure:";
            SizeF focalSzStringSize = graph.graphics.MeasureString(focalSzString, legendFont);
            PointF focalSzStringPoint = new PointF(graph.xAxisStart, (float)(graph.axes[0].Y * 1.15));
            graph.graphics.DrawString(focalSzString, legendFont, legendBrush, focalSzStringPoint.X, focalSzStringPoint.Y);
            graph.graphics.FillEllipse(legendBrush, focalSzStringPoint.X + focalSzStringSize.Width, focalSzStringPoint.Y + focalSzStringSize.Height / 4, markerSize / 2 * graph.objectScale, markerSize / 2 * graph.objectScale);

            // Placement for generalized seizure
            string generalSzString = "Generalized Seizure:";
            SizeF generalSzStringSize = graph.graphics.MeasureString(generalSzString, legendFont);
            PointF generalSzStringPoint = new PointF(graph.xAxisLength - generalSzStringSize.Width, (float)(graph.axes[0].Y * 1.15));
            graph.graphics.DrawString(generalSzString, legendFont, legendBrush, generalSzStringPoint.X, generalSzStringPoint.Y);
            graph.graphics.DrawEllipse(szPen, generalSzStringPoint.X + generalSzStringSize.Width, generalSzStringPoint.Y + generalSzStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);

        }
        public void DisplayHeader()
        {
            // Initialize header information for drawing text
            string headerString = "Epilepsy Therapy Screening Program";
            Font headerFont = new Font("Arial", 16F * graph.objectScale);
            string subheader = "";
            if (test == "T35")
            {
                subheader = "Test 35 - Chronic Post-SE (KA) Spontaneously Seizing Rats: Stage 1 (IP Administration)";
            }
            else if (test == "T36")
            {
                subheader = "Test 36 - Chronic Post-SE (KA) Spontaneously Seizing Rats: Stage 2 (Oral Administration - Drug in Food)";
            }
            Font subFont = new Font("Arial", 10F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);


            // Get sizes and points for string placement
            SizeF headerSize = graph.graphics.MeasureString(headerString, headerFont);
            SizeF subSize = graph.graphics.MeasureString(subheader, subFont);

            PointF headerPoint = new PointF(graph.mainPlot.Width / 2 - headerSize.Width / 2, (float)(graph.mainPlot.Height * 0.05));
            PointF subPoint = new PointF(graph.mainPlot.Width / 2 - subSize.Width / 2, (float)(graph.mainPlot.Height * 0.05 + headerSize.Height));

            // Draw rectangles first
            RectangleF headerRect = new RectangleF((int)(graph.mainPlot.Width / 2 - subSize.Width / 2), (int)(graph.mainPlot.Height * 0.05), (int)subSize.Width, (int)(headerSize.Height + subSize.Height));
            Pen headerPen = new Pen(Brushes.Black);
            headerPen.Width = 2.0F * graph.objectScale;
            SolidBrush solidBrush = new SolidBrush(Color.LightSlateGray);
            graph.graphics.FillRectangle(solidBrush, headerRect);
            graph.graphics.DrawRectangle(headerPen, headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height);
            
            // Now draw strings over rectangles
            graph.graphics.DrawString(headerString, headerFont, headerBrush, headerPoint);
            graph.graphics.DrawString(subheader, subFont, headerBrush, subPoint);

            // Set values for ETSP, Batch, Dose, and Frequency
            // If user did not input values for these set to ----
            ETSP = "ETSP: ----";
            batch = "Batch: ----";
            dose = "Dose: ----";
            frequency = "Frequnecy: ----";
            // else, set to user defined values

            // Draw area and strings for etsp, batch, etc.
            SizeF etspSize = graph.graphics.MeasureString(ETSP, headerFont);
            SizeF batchSize = graph.graphics.MeasureString(batch, headerFont);
            SizeF doseSize = graph.graphics.MeasureString(dose, headerFont);
            SizeF freqSize = graph.graphics.MeasureString(frequency, headerFont);
            float etspAndbatchY = (float)((headerRect.Y + headerSize.Height + subSize.Height) * 1.1);
            
            RectangleF etspAndbatch = new RectangleF(headerRect.X, etspAndbatchY, headerRect.Width, Math.Max(etspSize.Height, batchSize.Height));
            float doseAndfreqY = (float)((etspAndbatch.Y + Math.Max(etspSize.Height, batchSize.Height)) * 1.075);
            RectangleF doseAndfreq = new RectangleF(headerRect.X, doseAndfreqY, headerRect.Width, Math.Max(doseSize.Height, freqSize.Height));

            graph.graphics.FillRectangle(solidBrush, etspAndbatch);
            graph.graphics.DrawRectangle(headerPen, etspAndbatch.X, etspAndbatch.Y, etspAndbatch.Width, etspAndbatch.Height);
            graph.graphics.FillRectangle(solidBrush, doseAndfreq);
            graph.graphics.DrawRectangle(headerPen, doseAndfreq.X, doseAndfreq.Y, doseAndfreq.Width, doseAndfreq.Height);

            // Draw strings for etsp, batch, dose, frequency
            graph.graphics.DrawString(ETSP, headerFont, headerBrush, headerRect.X, etspAndbatchY);
            graph.graphics.DrawString(batch, headerFont, headerBrush, (float)(headerRect.Width * 1.5 - batchSize.Width), etspAndbatchY);
            graph.graphics.DrawString(dose, headerFont, headerBrush, headerRect.X, doseAndfreqY);
            graph.graphics.DrawString(frequency, headerFont, headerBrush, (float)(headerRect.Width * 1.5 - freqSize.Width), doseAndfreqY);

        }
        public void DisplayStats(Project pjt)
        {
            Font headerFont = new Font("Arial", 12F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);

            // Draw Daily Seizure Burden header
            string burdenString = "Daily Seizure Burden";
            SizeF burdenSize = graph.graphics.MeasureString(burdenString, headerFont);
            PointF burdenPoint = new PointF((float)(graph.mainPlot.Width / 2 - burdenSize.Width / 0.65), (float)(graph.yAxisStart * 0.65));
            graph.graphics.DrawString(burdenString, headerFont, headerBrush, burdenPoint);

            // Draw Seizure Freedom header
            string freedomString = "Seizure Freedom";
            SizeF freedomSize = graph.graphics.MeasureString(freedomString, headerFont);
            PointF freedomPoint = new PointF((float)(graph.mainPlot.Width / 2 + freedomSize.Width / 1.25), (float)(graph.yAxisStart * 0.65));
            graph.graphics.DrawString(freedomString, headerFont, headerBrush, freedomPoint);

            if (test == "T35")
            {
                // if test 35, do baseline, vehicle, and drug
                Font statsFont = new Font("Arial", 8F * graph.objectScale);
                Pen boundingPen = new Pen(Brushes.Black);
                boundingPen.Width = 1.25F * graph.objectScale;

                string baselineBurden = pjt.baselineBurden.ToString() + "\u00B1" + pjt.baselineSEM.ToString();
                string drugBurden = pjt.drugBurden.ToString() + "\u00B1" + pjt.drugSEM.ToString();
                string vehicleBurden = pjt.vehicleBurden.ToString() + "\u00B1" + pjt.vehicleSEM.ToString();

                SizeF baselineStringSize = graph.graphics.MeasureString(baselineBurden, statsFont);
                SizeF drugStringSize = graph.graphics.MeasureString(drugBurden, statsFont);
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleBurden, statsFont);
                float boxLength = new List<float>() { baselineStringSize.Width, drugStringSize.Width, vehicleStringSize.Width }.Max();
                float boxHeight = new List<float>() { baselineStringSize.Height, drugStringSize.Height, vehicleStringSize.Height }.Max();

                // Baseline Burden
                graph.graphics.DrawString(baselineBurden, statsFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Baseline", headerFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Drug Burden       
                graph.graphics.DrawString(drugBurden, statsFont, headerBrush, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Drug", headerFont, headerBrush, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Vehicle Burden       
                graph.graphics.DrawString(vehicleBurden, statsFont, headerBrush, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Vehicle", headerFont, headerBrush, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Vehicle freedom
                SizeF vehicleSize = graph.graphics.MeasureString("Vehicle", headerFont);
                float startPt = (float)(graph.mainPlot.Width * 0.75 - vehicleSize.Width);
                float boxStart = (float)(graph.mainPlot.Width * 0.75 - boxLength);
                string vehicleFreedom = pjt.vehicleSzFreedom.ToString() + "/" + pjt.Animals.Count.ToString();
                graph.graphics.DrawString(vehicleFreedom, statsFont, headerBrush, boxStart, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Vehicle", headerFont, headerBrush, boxStart, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Drug freedom
                SizeF drugSize = graph.graphics.MeasureString("Vehicle", headerFont);
                string drugFreedom = pjt.drugSzFreedom.ToString() + "/" + pjt.Animals.Count.ToString();
                graph.graphics.DrawString(drugFreedom, statsFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Drug", headerFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Baseline freedom
                SizeF baselineSize = graph.graphics.MeasureString("Vehicle", headerFont);
                string baselineFreedom = pjt.drugSzFreedom.ToString() + "/" + pjt.Animals.Count.ToString();
                SizeF blFreedomS = graph.graphics.MeasureString(baselineFreedom, statsFont);
                graph.graphics.DrawString(baselineFreedom, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Baseline", headerFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));
            }
            else if (test == "T36")
            {
                // test 36 just vehicle and drug
            }
        }
        public void ExportPDF()
        {

        }


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
        public float vehicleSEM;
        public float drugSEM;
        public float baselineSEM;
        public float vehicleBurden;
        public float drugBurden;
        public float baselineBurden;
        public int vehicleSzFreedom;
        public int baselineSzFreedom;
        public int drugSzFreedom;
        public string test;
        public Project(string Inpt)
        {
            Filename = Inpt;
            P = Path.GetDirectoryName(Inpt);
            Animals = new List<AnimalType>();
            Files = new List<FileType>();
            Groups = new List<GroupType>();
            if (!Directory.Exists(P + "\\Data"))
            {
                Directory.CreateDirectory(P + "\\Data");
            }
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
            }
        }
        
        public void Save()
        {
            StreamWriter F = new StreamWriter(Filename);
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
                    s ="An," + A.ID + ", sz, " + S.d.ToString() + ", " + answer + ", " + S.Notes + "," + S.length + "," + S.file + "," + S.Severity;
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
                    s = "An," + A.ID + ", ij, " + I.TimePoint.ToString() + "," + I.DoseNum.ToString() + "," + I.ADDID + "," + I.Dose.ToString() + "," + I.DoseAmount.ToString() + "," +  I.Route + "," + I.solvent;
                    F.WriteLine(s); 
                }
                
            }
            F.Close();
            F.Dispose();
        }
        private int FindAnimal(string An) //Finds an Animal Index, or creates a new one if not found
        {
            int CurrentAnimal;
            AnimalType A;
            An = An.Replace(" ", string.Empty);
            CurrentAnimal = Animals.FindIndex(
               delegate(AnimalType X)
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
                answer =  string.Format("{0:D2}:{1:D2}:{2:D2}", S.t.Hours, S.t.Minutes, S.t.Seconds);
                Szs[i] = S.d.ToShortDateString() + " " + answer;
                i++;
            }

            return Szs;

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
            Files.Sort(delegate(FileType F1, FileType F2) { return DateTime.Compare(F1.Start, F2.Start); }); 
            Animals.Sort(delegate(AnimalType A1, AnimalType A2) { return string.Compare(A1.ID, A2.ID); });
            foreach (AnimalType A in Animals)
            {
                A.Sz.Sort(delegate(SeizureType S1, SeizureType S2) { return DateTime.Compare(S1.d, S2.d); });
                A.Meals.Sort(delegate(MealType M1, MealType M2) { return DateTime.Compare(M1.d, M2.d); });
            }            
            
        }
        public void T35Sort()
        {
            // Remove animals w/o injections and one type of ADDID
            Animals.RemoveAll(a => a.Injections.Count == 0);
            Animals.RemoveAll(a => a.Injections[0].ADDID == a.Injections[a.Injections.Count-1].ADDID);

            // Sort animals according to vehicle first
            List<AnimalType> sortedA = Animals.OrderBy(a => a.Injections[0].ADDID).ToList();
            Animals = sortedA;
        }
        public void T36Sort()
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
        public void CalculateSzBurden()
        {
            foreach (AnimalType A in Animals)
            {
                // SeizureBurden() calculates seizure burden for all relevant groups and finds their SEM's
                A.SeizureBurden(test, Files[0].Start.Date);
            }

            // list for seizure burdens to calculate SEM
            vehicleBurden = (float)Math.Round(Animals.Select(a => a.vehicleBurden).ToList().Average(),2);
            drugBurden = (float)Math.Round(Animals.Select(a => a.drugBurden).ToList().Average(),2);
            baselineBurden = (float)Math.Round(Animals.Select(a => a.baselineBurden).ToList().Average(),2);

            // Find SEM for each group
            vehicleSEM = SEM(Animals.Select(a => a.vehicleBurden).ToList());
            drugSEM = SEM(Animals.Select(a => a.drugBurden).ToList());
            baselineSEM = SEM(Animals.Select(a => a.baselineBurden).ToList());
        }
        public void SeizureFreedom()
        {
            // Answer seizure freedom question for each animal
            foreach (AnimalType A in Animals)
            {
                A.SzFreedom(test, Files[0].Start.Date);
            }

            // Add up animal seizure freedoms
            vehicleSzFreedom = Animals.Select(a => a.vehicleFreedom).ToList().Sum();
            baselineSzFreedom = Animals.Select(a => a.baselineFreedom).ToList().Sum();
            drugSzFreedom = Animals.Select(a => a.drugFreedom).ToList().Sum();

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
            sem = (float)Math.Round((float)(sigma / Math.Sqrt(sz.Count - 1)),2);
            return sem;
        }

        public string[] Get_Animals()
        {
            string[] X;
            X = new string[Animals.Count];
            int i = 0;
            foreach (AnimalType A in Animals)
            {
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
        public bool ImportDirectory(string Dir)
        {
            ACQReader TempACQ = new ACQReader();
            FileType F = new FileType();
            //Open the ACQ file
            string[] FName = Directory.GetFiles(Dir, "*.acq");
            if (FName.Length > 0) 
            {
                TempACQ.openACQ(FName[0]);
                string Fn = FName[0].Substring(FName[0].LastIndexOf('\\') + 1);
                F.Start = ConvertFileToDT(Fn);                
                F.Chans = TempACQ.Chans;
                F.AnimalIDs = TempACQ.ID;
                F.Duration = TimeSpan.FromSeconds(TempACQ.FileTime);
                TempACQ.closeACQ();
                FileType Fs = Files.Find(delegate(FileType Ft) { return ((DateTime.Compare(Ft.Start, F.Start) == 0) && (string.Compare(F.AnimalIDs[0], Ft.AnimalIDs[0])== 0)); }); 
                //Determine if duplicate file - compare animal name and file start
                if (Fs != null)
                {
                    //Boot us out of the function                   
                    return false;
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
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID-1]);
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
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID-1]);
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
                            CurrentAnimal = FindAnimal(F.AnimalIDs[AID-1]);
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
                        ImportSzFile(SZFile[0]);
                    }
                }
            }
            return true;
        }
        public DateTime ConvertFileToDT(string F)
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
        public string DTS(DateTime dt)
        {

            return string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", dt);      
        }            
        public void ImportSzFile(string File)
        {            
            DateTime dt;
            string[] TmpStr;            
            int CurrentAnimal;
            string str;
            TimeSpan t;
            SeizureType S;
            string F = File.Substring(File.LastIndexOf('\\')+1);            
            dt = ConvertFileToDT(F);
            StreamReader TmpTxt = new StreamReader(File);
            while (!TmpTxt.EndOfStream)
            {
                str = TmpTxt.ReadLine();
                TmpStr = str.Split(',');                
                CurrentAnimal = FindAnimal(TmpStr[1]);           
                TimeSpan.TryParse(TmpStr[3], out t);
                t = t.Add(dt.TimeOfDay);
                if (TmpStr.Length == 7)
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6]);
                }
                else
                {
                    S = new SeizureType(dt.ToString(), t.ToString(), TmpStr[5], TmpStr[4], TmpStr[6], TmpStr[7]);
                }
                Animals[CurrentAnimal].Sz.Add(S);
            }
            TmpTxt.Close();
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

        public void ExportData(string Fname, ExportType E, int numDays, string binnedFile)
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

            // Open binned seizure file
            StreamWriter sw = new StreamWriter(binnedFile);
            sw.AutoFlush = true;
            string sz;
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
                        st = A.ID + ',' + B.dt.Date.ToString("MM/dd/yyyy") + " " + B.EnteredTime.ToString() + "," + B.ID; 
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
                        st += "," + Math.Round(B.dt.Date.Subtract(Earliest).TotalHours + B.EnteredTime.TotalHours, 2).ToString();
                        st2 += "," + B.ID;
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.Pellet)
                {
                    st = "PLRT," + A.ID;  //Pellet Removal Time
                    st2 = "PLRC," + A.ID;  //Pellet Removal Count 
                    foreach (RemovalType R in A.Removals)
                    {
                        st += "," + Math.Round(R.dt.Subtract(Earliest).TotalHours,2).ToString();
                        st2 += "," + R.count.ToString();
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.Injections)
                {
                    st = A.ID + ",IJT";
                    st2 = A.ID +",IJC";
                    foreach (InjectionType I in A.Injections)
                    {
                        st += "," + Math.Round(I.TimePoint.Subtract(Earliest).TotalHours,2).ToString();
                        st2 += "," + I.ADDID;
                    }
                    F.WriteLine(st);
                    F.WriteLine(st2);
                }
                if (E.InjectionsList)
                {

                }
                if (E.binSz)
                {
                    
                    // bin seizures if option was selected
                    sz = A.ID;
                   
                    // Create list for days that seizures happen
                    List<double> szDay = new List<double>();
                    List<double> binSeizures = new List<double>(new double[numDays]);
                    foreach (SeizureType seizureType in A.Sz)
                    {
                        szDay.Add(Math.Floor(seizureType.d.Subtract(Earliest).TotalDays + seizureType.t.TotalDays));
                    }
                    var g = szDay.GroupBy(i => i);
                    foreach (var bin in g)
                    {
                        if (bin.Key > 0)
                        {
                            binSeizures[(int)(bin.Key - 1)] = bin.Count();
                        }
                        else
                        {
                            binSeizures[(int)(bin.Key)] = bin.Count();
                        }
                    }
                    for (int i = 0; i < binSeizures.Count; i++)
                    {
                        sz += "," + binSeizures[i].ToString();
                    }
                    sw.WriteLine(sz);
                    
                }
            }
            F.Close();
            sw.Close();
        }

        //This function takes the data from the project file and loads it into memory. 
        void ParseLine(string L)
        {
            string[] data;
            string[] IDs;
            int Chans;
            data = L.Split(',');
            //Data format - Record Type - Record Start
            //Record types - An = Animal, Fl = File, Gp = Group, Lb = Label.
            if (data[0].IndexOf("Fl") != -1)
            {

                DateTime TempDate;
                try
                {
                    TempDate = ConvertFileToDT(data[1]);
                    //Get the ACQ info. 
                    int.TryParse(data[3],out Chans);
                    IDs = new string[Chans];
                    for (int i = 0; i < Chans; i++)
                    {
                        IDs[i] = data[4+i];
                    }
                    FileType F = new FileType(IDs, Chans, TempDate, data[2]);
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
                    FileType F = new FileType(IDs, Chans, TempDate, data[2]);
                    Pass=true;
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
                        SeizureType S = new SeizureType(data[3], data[4], data[5], data[6], data[7]);
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
