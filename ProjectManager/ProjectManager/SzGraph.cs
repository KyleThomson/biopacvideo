using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using Accord;

namespace ProjectManager
{
    class SzGraph
    {
        // instance of graph for test 35 or test 36
        public GraphProperties graph;

        // string variables for header info for the pdf
        public TESTTYPES test;
        public string ETSP = "ETSP: ";
        public string batch = "Dose: ";
        public string dose = "Dose: ";
        public string frequency = "Frequency: ";
        public DateTime Earliest; public DateTime Latest;
        ToolBar toolBar1;
        public Project project;

        // bools for drawing graph
        private bool _treatment = true;
        private bool _sz = true;
        private bool _empty = true;
        private bool _export = true;

        
        public SzGraph(int X, int Y, Project pjt)
        {
            
            // Type of test
            test = pjt.analysis.test;
            project = pjt;
            // Find max day of project
            Earliest = project.Files[0].Start.Date;
            Latest = project.Files[project.Files.Count - 1].Start.Date;
            double totalHours = Latest.Subtract(Earliest).TotalHours;
            int tempMax = (int)Math.Round(totalHours / 24, 2);
            AlignToInjection();
            // x tick every 7 days           
            int xTickInterval = 7;

            // Get nearest multiple of 7 days
            int nearestMultiple = (int)Math.Round(tempMax / (double)xTickInterval, MidpointRounding.AwayFromZero) * xTickInterval;
            int numXTicks = (nearestMultiple / xTickInterval) + 1;

            // Initialize graph by drawing labels, tick points, inputting numbers into GraphProperties
            graph = new GraphProperties(X, Y, nearestMultiple, project.Animals.Count);
            graph.axes.xTicks = numXTicks;

            // Get axis tick labels for graph properties
            graph.axes.xTickLabels = GetXTickLabels(xTickInterval);
            graph.axes.yTickLabels = GetYTickLabels();

            // Set test descriptors
            SetTestDescriptors();

            // Add ToolBar
            AddButtons();
        }

        private void AlignToInjection()
        {
            List<double> align = new List<double>();
            foreach (AnimalType animal in project.Animals)
            {
                var earliestIjt = animal.Injections[0].TimePoint.Subtract(Earliest).TotalDays;
                if (earliestIjt < 10)
                {
                    animal.alignBy7Days = earliestIjt - 7;
                }
            }


        }
        private List<string> GetXTickLabels(int xTickInterval)
        {
            List<string> xTickString = new List<string>();
            List<double> xTickLocations = new List<double>();
            //Obtain basis for y and x axis labeling
            for (int i = 0; i <= project.Files.Count; i += xTickInterval)
            {
                if (i % xTickInterval == 0)// && i != 0)
                {
                    xTickString.Add((i).ToString());
                    xTickLocations.Add(i);
                }

            }

            graph.axes.xTickLocations = xTickLocations;
            return xTickString;
        }
        private void SetTestDescriptors()
        {
            TestDescription description = new TestDescription();
            description.ShowDialog();
            // set description for the test - this will display in subheaders
            ETSP += description.ETSP; batch += description.Batch;
            dose += description.Dose; frequency += description.Frequency;

            // check if form was cancelled and stop plotting
            if (description._cancelled)
            { return; }
        }
        private List<string> GetYTickLabels()
        {
            List<string> yTickString = new List<string>();
            //Obtain basis for y and x axis labelling
            for (int i = 0; i < project.Animals.Count; i++)
            {
                yTickString.Add(project.Animals[i].ID);
            }
            return yTickString;
        }
        public void PlotSz()
        {
            // Plot seizures the same for both test 35 and test 36
            int markerSize = 6;
            Color szColor = Color.FromName("Black");
            for (int i = 0; i < project.Animals.Count; i++)
            {
                var align = project.Animals[i].alignBy7Days;
                float yCoord = i + 1;
                for (int j = 0; j < project.Animals[i].Sz.Count; j++)
                {
                    float xCoord = (float)(Math.Round((project.Animals[i].Sz[j].d.Date.Subtract(Earliest).TotalHours + project.Animals[i].Sz[j].t.TotalHours) / 24 - align, 2));
                    if (xCoord < 0)
                    { xCoord = 0; }
                    if (project.Animals[i].Sz[j].Severity > 0)
                    {
                        graph.PlotPoints((float)(xCoord), yCoord, markerSize, "o", szColor);
                    }
                    else if (project.Animals[i].Sz[j].Severity == 0)
                    {
                        graph.PlotPoints((float)(xCoord), (float)(yCoord - 0.05), markerSize, ".", szColor);
                    }

                }
            }
        }
        public void PlotTrt()
        {
            float lineWidth = 4;
            Color vehicleColor = Color.FromName("Teal");
            Color drugColor = Color.FromName("Red");

            // If Test 35, use injections to draw lines for treatment
            if (test == TESTTYPES.T35)
            {
                for (int i = 0; i < project.Animals.Count; i++)
                {
                    var align = project.Animals[i].alignBy7Days;
                    float yCoord = (float)(i + 0.5);

                    // Initialize vehicle and drug treatment times
                    List<float> vehicleTimes = new List<float>();
                    List<float> drugTimes = new List<float>();

                    for (int j = 0; j < project.Animals[i].Injections.Count; j++)
                    {
                        foreach (InjectionType I in project.Animals[i].Injections)
                        {
                            if (I.ADDID == "vehicle")
                            {
                                vehicleTimes.Add((float)Math.Round(I.TimePoint.Subtract(Earliest).TotalHours / 24 - align, 2));
                            }
                            else
                            {
                                drugTimes.Add((float)Math.Round(I.TimePoint.Subtract(Earliest).TotalHours / 24 - align, 2));
                            }
                        }
                        // Draw vehicle line
                        if (vehicleTimes.Count > 1)
                        {
                            if (vehicleTimes[0] < 0)
                            { vehicleTimes[0] = 0; }
                            graph.Line(vehicleTimes[0], yCoord, (float)(vehicleTimes[vehicleTimes.Count - 1] + 0.5), yCoord, lineWidth, vehicleColor);
                        }


                        // Draw drug line
                        if (drugTimes.Count > 1)
                        {
                            if (drugTimes[0] < 0)
                            { drugTimes[0] = 0; }
                            graph.Line(drugTimes[0], yCoord, (float)(drugTimes[drugTimes.Count - 1] + 0.5), yCoord, lineWidth, drugColor);
                        }


                    }
                }
            }
            else if (test == TESTTYPES.T36)
            {
                Color unmedicatedColor = Color.FromName("Blue");
                float animalCounter = 0.5F;
                foreach (AnimalType A in project.Animals)
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
            else if (test == TESTTYPES.IAK)
            {
                project.Animals = project.Animals.OrderBy(a => a.Group.Name).ToList();
                Color group1Color = Color.Red;
                Color group2Color = Color.Blue;
                Color groupColor = default;
                int i = 1;
                
                var yCoord = 0.6;
                int x = 0;
                foreach (AnimalType animal in project.Animals)
                {
                    yCoord = x + 0.5;
                    var align = animal.alignBy7Days;
                    var x1 = Math.Round(animal.Injections[0].TimePoint.Subtract(Earliest).TotalHours / 24 - align, 2);
                    var x2 = Math.Round(animal.Injections[animal.Injections.Count - 1].TimePoint.Subtract(Earliest).TotalHours / 24 - align, 2);
                    if (animal.Group.Name == project.Groups[0].Name) 
                    {
                        groupColor = group1Color;
                    }
                    if (animal.Group.Name == project.Groups[1].Name)
                    {
                        groupColor = group2Color;
                    }
                    graph.Line((float)x1, (float)yCoord, (float)x2, (float)yCoord, lineWidth, groupColor);
                    x++;

                    i++;
                }
            }
        }
        public void PlotEmpty()
        {
            Color lineColor = Color.FromName("Black");
            float lineWidth = 4;

            // get earliest and latest time in hours
            float time0 = (float)Earliest.Subtract(Earliest).TotalHours;
            float maxTime = (float)Math.Round(Latest.Subtract(Earliest).TotalHours / 24, 2);
            
            // Plot the missing time an animal has
            int i = 0;
            foreach (AnimalType animal in project.Animals)
            {
                var align = animal.alignBy7Days;
                float yCoord = (float)(i + 0.5);
                if (animal.earliestAppearance != default)
                {
                    
                    float x2 = (float)Math.Round(animal.earliestAppearance.Subtract(Earliest).TotalHours / 24 - align, 4);
                    if (x2 < 0)
                    { x2 = 0; }
                    if (x2 > align)
                    {
                        graph.Line((float)(time0), yCoord, (float)(x2), yCoord, lineWidth, lineColor);
                    }
                }
                if (animal.latestAppearance != default)
                {
                    float x2 = (float)Math.Round(animal.latestAppearance.Subtract(Earliest).TotalHours / 24 - align, 4);
                    if (x2 < 0)
                    { x2 = 0; }
                    graph.Line((float)(x2), yCoord, maxTime, yCoord, lineWidth, lineColor);
                }
                i++;
            }
        }
        public void Legend()
        {
            Legend testLegend = new Legend(new Context(graph.graphics));
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
            if (test == TESTTYPES.T35)
            {
                string drugString = "Drug Treatment";
                PointF drugStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.DrawLine(drugPen, drugStringPoint.X, drugStringPoint.Y + drugStringSize.Height, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height);

                // If Test 35
                // Placement for vehicle treatment
                string vehicleString = "Vehicle Treatment";
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.axes.xAxisLength - vehicleStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.DrawLine(vehiclePen, vehicleStringPoint.X, vehicleStringPoint.Y + vehicleStringSize.Height, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height);
            }
            else if (test == TESTTYPES.T36)
            {
                string drugString = "Medicated Meal:";
                SolidBrush drugBrush = new SolidBrush(Color.Red);
                PointF drugStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.FillEllipse(drugBrush, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);

                // If Test 36
                // Placement for vehicle treatment
                string vehicleString = "Unmedicated Meal:";
                SolidBrush unmedicatedBrush = new SolidBrush(Color.Blue);
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.axes.xAxisLength - vehicleStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.FillEllipse(unmedicatedBrush, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);
            }
            else if (test == TESTTYPES.IAK)
            {
                float lineWidth = 4 * graph.objectScale;
                int i = 1;
                foreach (GroupType group in project.Groups)
                {
                    string label = "Group " + group.Name + ":";
                    SizeF labelSize = graph.graphics.MeasureString(label, legendFont);
                    if (i == 1) // Group 1
                    {
                        Color lineColor = Color.FromName("Red");
                        PointF labelPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                        graph.graphics.DrawString(label, legendFont, legendBrush, labelPoint.X, labelPoint.Y);
                        graph.Line(labelPoint.X, labelPoint.Y + labelSize.Height,
                            labelPoint.X + labelSize.Width * 2, labelPoint.Y + labelSize.Height, lineWidth, lineColor);
                    }
                    else if (i == 2) // Group 2
                    {
                        Color lineColor = Color.FromName("Blue");
                        PointF labelPoint = new PointF(graph.axes.xAxisLength - labelSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));
                        graph.graphics.DrawString(label, legendFont, legendBrush, labelPoint.X, labelPoint.Y + labelSize.Height / 4);
                        graph.Line(labelPoint.X, labelPoint.Y + labelSize.Height,
                            labelPoint.X + labelSize.Width, labelPoint.Y + labelSize.Height, lineWidth, lineColor);
                    }
                    i++;
                }
            }

            // Placement for focal seizure
            string focalSzString = "Focal Seizure:";
            SizeF focalSzStringSize = graph.graphics.MeasureString(focalSzString, legendFont);
            PointF focalSzStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.15));
            graph.graphics.DrawString(focalSzString, legendFont, legendBrush, focalSzStringPoint.X, focalSzStringPoint.Y);
            graph.graphics.FillEllipse(legendBrush, focalSzStringPoint.X + focalSzStringSize.Width, focalSzStringPoint.Y + focalSzStringSize.Height / 4, markerSize / 2 * graph.objectScale, markerSize / 2 * graph.objectScale);

            // Placement for generalized seizure
            string generalSzString = "Generalized Seizure:";
            SizeF generalSzStringSize = graph.graphics.MeasureString(generalSzString, legendFont);
            PointF generalSzStringPoint = new PointF(graph.axes.xAxisLength - generalSzStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.15));
            graph.graphics.DrawString(generalSzString, legendFont, legendBrush, generalSzStringPoint.X, generalSzStringPoint.Y);
            graph.graphics.DrawEllipse(szPen, generalSzStringPoint.X + generalSzStringSize.Width, generalSzStringPoint.Y + generalSzStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);

        }
        public void DisplayHeader()
        {
            // Initialize header information for drawing text
            string headerString = "Epilepsy Therapy Screening Program";
            Font headerFont = new Font("Arial", 16F * graph.objectScale);
            string subheader = "";
            if (test == TESTTYPES.T35)
            {
                subheader = "Test 35 - Chronic Post-SE (KA) Spontaneously Seizing Rats: Stage 1 (IP Administration)";
            }
            else if (test == TESTTYPES.T36)
            {
                subheader = "Test 36 - Chronic Post-SE (KA) Spontaneously Seizing Rats: Stage 2 (Oral Administration - Drug in Food)";
            }
            else if (test == TESTTYPES.IAK)
            {
                subheader =
                    "IAK Test - This is a test. I don't know what to put here yet. So I'm going to ramble for a few more characters.";
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
            SolidBrush solidBrush = new SolidBrush(Color.LightGray);
            graph.graphics.FillRectangle(solidBrush, headerRect);
            graph.graphics.DrawRectangle(headerPen, headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height);

            // Now draw strings over rectangles
            graph.graphics.DrawString(headerString, headerFont, headerBrush, headerPoint);
            graph.graphics.DrawString(subheader, subFont, headerBrush, subPoint);

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
            graph.graphics.DrawString(batch, headerFont, headerBrush, (float)(headerRect.Width + headerRect.X - batchSize.Width), etspAndbatchY);
            graph.graphics.DrawString(dose, headerFont, headerBrush, headerRect.X, doseAndfreqY);
            graph.graphics.DrawString(frequency, headerFont, headerBrush, (float)(headerRect.Width + headerRect.X - freqSize.Width), doseAndfreqY);

        }
        public void DisplayStats()
        {
            // Get data
            Dictionary<string, GroupedData> allData = project.analysis.groupedData;

            Font headerFont = new Font("Arial", 12F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);

            // Draw Daily Seizure Burden header
            string burdenString = "Daily Seizure Burden";
            SizeF burdenSize = graph.graphics.MeasureString(burdenString, headerFont);
            PointF burdenPoint = new PointF((float)(graph.mainPlot.Width / 2 - burdenSize.Width / 0.65), (float)(graph.axes.yAxisStart * 0.65));
            graph.graphics.DrawString(burdenString, headerFont, headerBrush, burdenPoint);

            // Draw Seizure Freedom header
            string freedomString = "Seizure Freedom";
            SizeF freedomSize = graph.graphics.MeasureString(freedomString, headerFont);
            PointF freedomPoint = new PointF((float)(graph.mainPlot.Width / 2 + freedomSize.Width / 1.25), (float)(graph.axes.yAxisStart * 0.65));
            graph.graphics.DrawString(freedomString, headerFont, headerBrush, freedomPoint);

            if (test == TESTTYPES.T35)
            {
                // Find the drug group without knowing the name of the drug
                List<string> copyGroups = project.analysis.groups;
                copyGroups.Remove("Baseline");
                copyGroups.Remove("vehicle");
                string drugGroup = copyGroups[0];
                // if test 35, do baseline, vehicle, and drug
                Font statsFont = new Font("Arial", 8F * graph.objectScale);
                Pen boundingPen = new Pen(Brushes.Black);
                boundingPen.Width = 1.25F * graph.objectScale;

                // sz burdens
                string baselineBurden = allData["Baseline"].szBurden.ToString() + "\u00B1" + allData["Baseline"].burdenSEM.ToString();
                string drugBurden = allData[drugGroup].szBurden.ToString() + "\u00B1" + allData[drugGroup].burdenSEM.ToString();
                string vehicleBurden = allData["vehicle"].szBurden.ToString() + "\u00B1" + allData["vehicle"].burdenSEM.ToString();
                SizeF baselineS = graph.graphics.MeasureString("Baseline", headerFont);

                // sz freedoms
                string vehicleFreedom = allData["vehicle"].szFreedom.ToString() + "/" + allData["vehicle"].numAnimals.ToString();
                string drugFreedom = allData[drugGroup].szFreedom.ToString() + "/" + allData[drugGroup].numAnimals.ToString();
                string baselineFreedom = allData["Baseline"].szFreedom.ToString() + "/" + allData["Baseline"].numAnimals.ToString();

                // Seizure Burden strings
                string baselineWilcoxon;
                string vehicleWilcoxon;
                if (allData["Baseline"].burdenPValue < 0.05)
                {
                    drugBurden += "\xB†";
                    baselineWilcoxon = "\xB† p<0.05 vs. Baseline (Wilcoxon Rank Sum)" 
                                       + "(p="+ allData["Baseline"].burdenPValue.ToString("G2") + ")";
                }
                else
                { baselineWilcoxon = "n.s. vs. Baseline (Wilcoxon Rank Sum)"
                                     + "(p=" + allData["Baseline"].burdenPValue.ToString("G2") + ")"; }
                if (allData["vehicle"].burdenPValue < 0.05)
                {
                    drugBurden += "\xB*";
                    vehicleWilcoxon = "\xB* p<0.05 vs. Vehicle (Wilcoxon Rank Sum)"
                                      + "(p=" + allData["vehicle"].burdenPValue.ToString("G2") + ")";
                }
                else
                {
                    vehicleWilcoxon = "n.s. vs. Vehicle (Wilcoxon Rank Sum)"
                                      + "(p=" + allData["vehicle"].burdenPValue.ToString("G2") + ")"; 

                }

                // Seizure Freedom strings
                string baselineFisherExact;
                string vehicleFisherExact;
                if (allData["Baseline"].freedomPValue < 0.05)
                {
                    drugFreedom += "\xB†";
                    baselineFisherExact = "\xB† p<0.05 vs. Baseline (Fisher Exact)"
                                          + "(p=" + allData["Baseline"].freedomPValue.ToString("G2") + ")";
                }
                else
                {
                    baselineFisherExact = "n.s. vs. Baseline (Fisher Exact)"
                                          + "(p=" + allData["Baseline"].freedomPValue.ToString("G2") + ")";
                }
                if (allData["vehicle"].freedomPValue < 0.05)
                {
                    drugFreedom += "\xB*";
                    vehicleFisherExact = "\xB* p<0.05 vs. Vehicle (Fisher Exact)"
                                         + "(p=" + allData["vehicle"].freedomPValue.ToString("G2") + ")";
                }
                else
                {
                    vehicleFisherExact = "n.s. vs. Vehicle (Fisher Exact)"
                                         + "(p=" + allData["vehicle"].freedomPValue.ToString("G2") + ")";
                }

                // Draw to graph
                ThreeGroups(baselineBurden, drugBurden, vehicleBurden, baselineWilcoxon, vehicleWilcoxon, baselineFisherExact, vehicleFisherExact, drugFreedom, vehicleFreedom, baselineFreedom);
            }
            else if (test == TESTTYPES.T36)
            {
                // test 36 just baseline and drug
            }
            else if (test == TESTTYPES.IAK)
            {
                // IAK compare to baseline for groups A and B
                Font statsFont = new Font("Arial", 8F * graph.objectScale);
                Pen boundingPen = new Pen(Brushes.Black);
                boundingPen.Width = 1.25F * graph.objectScale;
                foreach (KeyValuePair<string, GroupedData> group in project.analysis.groupedData)
                {
                    if (group.Key != "Baseline")
                    {
                        string baselineBurden = project.analysis.groupedData["Baseline"].szBurden.ToString() +
                                                "\u00B1" + project.analysis.groupedData["Baseline"].burdenSEM.ToString();
                        string groupBurden = group.Value.szBurden.ToString() + "\u00B1" +
                                             group.Value.burdenSEM.ToString();
                        string groupFreedom =
                            group.Value.szFreedom.ToString() + "/" + group.Value.numAnimals.ToString();
                    }
                }

                // sz burdens
                SizeF baselineS = graph.graphics.MeasureString("Baseline", headerFont);

                // sz freedoms

                // Seizure Burden strings
                string groupAWilcoxon;
                string groupBWilcoxon;

            }
        }
        private void TwoGroups()
        {

        }
        private void ThreeGroups(string baselineBurden, string drugBurden, string vehicleBurden, string baselineWilcoxon, string vehicleWilcoxon, string baselineFisherExact, string vehicleFisherExact, string drugFreedom, string vehicleFreedom, string baselineFreedom)
        {
            // if test 35, do baseline, vehicle, and drug
            Font headerFont = new Font("Arial", 12F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);
            Font statsFont = new Font("Arial", 8F * graph.objectScale);
            Pen boundingPen = new Pen(Brushes.Black);
            boundingPen.Width = 1.25F * graph.objectScale;

            SizeF baselineS = graph.graphics.MeasureString("Baseline", headerFont);

            SizeF baselineStringSize = graph.graphics.MeasureString(baselineBurden, statsFont);
            SizeF drugStringSize = graph.graphics.MeasureString(drugBurden, statsFont);
            SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleBurden, statsFont);
            float boxLength = new List<float>() { baselineS.Width, drugStringSize.Width, vehicleStringSize.Width }.Max();
            float boxHeight = new List<float>() { baselineStringSize.Height, drugStringSize.Height, vehicleStringSize.Height }.Max();

            // Baseline Burden
            graph.graphics.DrawString(baselineBurden, statsFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Baseline", headerFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Drug Burden       
            graph.graphics.DrawString(drugBurden, statsFont, headerBrush, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Drug", headerFont, headerBrush, graph.mainPlot.Width / 4 + boxLength + boxLength / 4, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Vehicle Burden       
            graph.graphics.DrawString(vehicleBurden, statsFont, headerBrush, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Vehicle", headerFont, headerBrush, graph.mainPlot.Width / 4 + boxLength * 2 + boxLength / 2, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Significance statements for Sz Burden:
            graph.graphics.DrawString(baselineWilcoxon, statsFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.axes.yAxisStart * 0.875));
            graph.graphics.DrawString(vehicleWilcoxon, statsFont, headerBrush, graph.mainPlot.Width / 4, (float)(graph.axes.yAxisStart * 0.925));

            // Vehicle freedom
            float boxStart = (float)(graph.mainPlot.Width * 0.75 - boxLength);
            graph.graphics.DrawString(vehicleFreedom, statsFont, headerBrush, boxStart, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, boxStart, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Vehicle", headerFont, headerBrush, boxStart, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Drug freedom
            graph.graphics.DrawString(drugFreedom, statsFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength - boxLength / 4, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Drug", headerFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Baseline freedom
            graph.graphics.DrawString(baselineFreedom, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.axes.yAxisStart * 0.8));
            graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.axes.yAxisStart * 0.8), boxLength, boxHeight);
            graph.graphics.DrawString("Baseline", headerFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.axes.yAxisStart * 0.8 - boxHeight * 1.50));

            // Significance statements for Sz Freedom:
            graph.graphics.DrawString(baselineFisherExact, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.axes.yAxisStart * 0.875));
            graph.graphics.DrawString(vehicleFisherExact, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.axes.yAxisStart * 0.925));
        }
        public void ExportGraph()
        {
            if (_empty)
            { _empty = false; }
            graph.ClearGraph();
            DrawGraph();
            graph.SaveFig();
        }
        private void AddButtons()
        {
            // create control for tool bar. this will get added to top of graph
            toolBar1 = new ToolBar();
            ToolBarButton exportButton = new ToolBarButton();
            exportButton.Text = "Export";

            ToolBarButton hideEmptyButton = new ToolBarButton
            {
                Text = "Hide Empty"
            };

            ToolBarButton hideSzButton = new ToolBarButton();
            hideSzButton.Text = "Hide Seizure";

            ToolBarButton hideTrtButton = new ToolBarButton();
            hideTrtButton.Text = "Hide Treatment";

            ToolBarButton clearGraph = new ToolBarButton();
            clearGraph.Text = "Clear";

            // Add button to toolbar controls
            toolBar1.Buttons.Add(exportButton);
            toolBar1.Buttons.Add(hideEmptyButton);
            toolBar1.Buttons.Add(hideSzButton);
            toolBar1.Buttons.Add(hideTrtButton);
            toolBar1.Buttons.Add(clearGraph);

            // Add event handler
            toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);

            // Add toolbar to form
            graph.graphForm.Controls.Add(toolBar1);
        }
        private void toolBar1_ButtonClick(Object sender, ToolBarButtonClickEventArgs e)
        {
            // Figure out which button is hit
            switch (toolBar1.Buttons.IndexOf(e.Button))
            {
                case 0: // export button
                    ExportGraph();
                    break;

                case 1: // hide empty time
                    if (_empty)
                    { _empty = false; }
                    else
                    { _empty = true; }
                    graph.ClearGraph();
                    DrawGraph();
                    break;

                case 2: // hide seizures
                    if (_sz)
                    {
                        _sz = false;
                    }
                    else
                    {
                        _sz = true;
                    }
                    graph.ClearGraph();
                    DrawGraph();
                    break;

                case 3: // hide treatments
                    if (_treatment)
                    { _treatment = false; }
                    else
                    { _treatment = true; }
                    graph.ClearGraph();
                    DrawGraph();
                    break;

                case 4:
                    // Clear graphics
                    graph.ClearGraph();
                    break;

            }
        }
        public void DrawGraph()
        {
            // Clear graphics
            //Color bg = Color.FromName("White");
            //graph.graphics.Clear(bg);

            // Draw axes
            graph.DrawAxes(4);
            graph.BoundingBox();
            Font aFont = new Font("Arial", 12 * graph.objectScale);
            graph.DrawTicks(graph.axes.xTicks, project.Animals.Count, 3.0F);
            graph.WriteXLabel("Time (days)", aFont);
            graph.WriteYLabel("Animals", aFont);

            // Draw data
            if (_sz)
            { PlotSz(); }

            if (_treatment)
            { PlotTrt(); }

            if (_empty)
            { PlotEmpty(); }

            // Draw descriptors
            Legend();
            DisplayHeader();
            DisplayStats();

            // Display
            graph.DisplayGraph();
        }


    }

}
