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

        // some position properties
        private int headerX;
        private int headerY;
        private int headerLength;

        // bools for drawing graph
        private bool _treatment = true;
        private bool _sz = true;
        private bool _empty = true;
        private bool _export = true;

        
        public SzGraph(Project pjt)
        {
            
            // Type of test
            test = pjt.analysis.test;
            project = pjt;

            // Find max day of project
            Earliest = project.Files[0].Start.Date;
            Latest = project.Files[project.Files.Count - 1].Start.Date;
            double totalHours = Latest.Subtract(Earliest).TotalHours;
            int tempMax = (int)Math.Round(totalHours / 24, 2);
            // Line up first injections to 7 days
            AlignToInjection();

            // x tick every 7 days           
            int xTickInterval = 7;

            // Get nearest multiple of 7 days, for drawing axis ticks
            int nearestMultiple = (int)Math.Round(tempMax / (double)xTickInterval, MidpointRounding.AwayFromZero) * xTickInterval;
            int numXTicks = (nearestMultiple / xTickInterval) + 1;

            // Initialize graph by drawing labels, tick points, inputting numbers into GraphProperties
            graph = new GraphProperties(nearestMultiple, project.Animals.Count);
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
            // This function gets the earliest injection in days and subtracts 7 days by it. This value will align injections to start at day 7.

            // Iterate thru animals and get unique align time for each
            foreach (AnimalType animal in project.Animals)
            {
                // earliest injection time
                var earliestIjt = animal.Injections[0].TimePoint.Subtract(Earliest).TotalDays;
                if (earliestIjt < 10)
                {
                    // set align property for the animal
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
            // class has form with editable fields for etsp, dose, freq, batch
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

            // use animal ID for y axis tick labels
            for (int i = 0; i < project.Animals.Count; i++)
            {
                yTickString.Add(project.Animals[i].ID);
            }
            return yTickString;
        }
        public void PlotSz()
        {
            // Plot seizures
            int markerSize = 6;
            Color szColor = Color.FromName("Black");
            for (int i = 0; i < project.Animals.Count; i++)
            {
                // have to align seizures so that first injection is 7 days
                var align = project.Animals[i].alignBy7Days;

                // new y coord
                float yCoord = i + 1;
                
                // plot each seizure
                for (int j = 0; j < project.Animals[i].Sz.Count; j++)
                {
                    if (!project.Animals[i].Sz[j].keepInAnalysis)
                        continue;
                    // calculate x coordinate in days and align to first injection at 7 days
                    float xCoord = (float)(Math.Round((project.Animals[i].Sz[j].d.Date.Subtract(Earliest).TotalHours + project.Animals[i].Sz[j].t.TotalHours) / 24 - align, 2));
                    
                    // alignment might make seizure less than 0 days, set it back to zero? or exclude??
                    if (xCoord < 0)
                        xCoord = 0;

                    // logic for drawing open circle (generalized seizure) vs filled circle (focal seizure)
                    if (project.Animals[i].Sz[j].Severity > 0)
                    {
                        graph.PlotPoints(xCoord, yCoord, markerSize, "o", szColor);
                    }
                    else if (project.Animals[i].Sz[j].Severity == 0)
                    {
                        graph.PlotPoints(xCoord, (float)(yCoord - 0.05), markerSize, ".", szColor);
                    }

                }
            }
        }
        public void PlotTrt()
        {
            // line properties
            float lineWidth = 4;
            Color vehicleColor = Color.FromName("Teal");
            Color drugColor = Color.FromName("Red");
            
            // If Test 35, use injections to draw lines for treatment
            if (test == TESTTYPES.T35)
            {
                var yCoord = 0.5;
                // Iterate thru animals in project file
                foreach (AnimalType animal in project.Animals)
                {
                    var align = animal.alignBy7Days;

                    // Initialize vehicle and drug treatment times
                    List<double> vehicleTimes = new List<double>();
                    List<double> drugTimes = new List<double>();

                    // Iterate through each group found earlier in ParseGroups()
                    foreach (string group in project.analysis.groups)
                    {
                        // get times for vehicle injections
                        if (group == "vehicle")
                            vehicleTimes = animal.GetInjectionTimes(group, Earliest, align);
                        // get times for drug group injections
                        else if (group != "vehicle" && group != "Baseline")
                            drugTimes = animal.GetInjectionTimes(group, Earliest, align);
                    }
                    // plot vehicle
                    if (vehicleTimes.Count > 0)
                        graph.Line((float)vehicleTimes.Min(), (float)yCoord, (float)vehicleTimes.Max(), (float)yCoord, lineWidth, vehicleColor);

                    // plot drug
                    if (drugTimes.Count > 0)
                        graph.Line((float)drugTimes.Min(), (float)yCoord, (float)drugTimes.Max(), (float)yCoord, lineWidth, drugColor);

                    yCoord++;
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
                List<Color> groupColors = new List<Color>()
                {
                    Color.Red, 
                    Color.Blue
                };

                // initialize y coordinate
                var yCoord = 0.5;
                foreach (AnimalType animal in project.Animals)
                {
                    // get value to align injections to 7 days
                    var align = animal.alignBy7Days;
                    // determine which color to use with this counter
                    int colorCounter = 0;
                    foreach (string group in project.analysis.groups)
                    {
                        if (group == "Baseline") continue;
                        // get times for a specific group, we don't know need to know which group it is
                        var groupTimes = animal.GetInjectionTimes(group, Earliest, align);

                        // plot line
                        graph.Line((float)groupTimes.Min(), (float)yCoord, (float)groupTimes.Max(), (float)yCoord, 
                            lineWidth, groupColors[colorCounter]);

                        colorCounter++;
                    }
                    // step y coordinate
                    yCoord++;
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
            // Method that draws on legend for injection type and seizure type

            // Set drawing properties
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
                // string to draw and properties
                string drugString = "Drug Treatment";
                PointF drugStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);

                // Draw the string 
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.DrawLine(drugPen, drugStringPoint.X, drugStringPoint.Y + drugStringSize.Height, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height);

                // string to draw and properties
                string vehicleString = "Vehicle Treatment";
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.axes.xAxisLength - vehicleStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));

                // Draw the string 
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.DrawLine(vehiclePen, vehicleStringPoint.X, vehicleStringPoint.Y + vehicleStringSize.Height, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height);
            }
            else if (test == TESTTYPES.T36)
            {
                // string to draw and properties
                string drugString = "Medicated Meal:";
                SolidBrush drugBrush = new SolidBrush(Color.Red);
                PointF drugStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                SizeF drugStringSize = graph.graphics.MeasureString(drugString, legendFont);

                // Draw the string 
                graph.graphics.DrawString(drugString, legendFont, legendBrush, drugStringPoint.X, drugStringPoint.Y);
                graph.graphics.FillEllipse(drugBrush, drugStringPoint.X + drugStringSize.Width, drugStringPoint.Y + drugStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);

                // string to draw and properties
                string vehicleString = "Unmedicated Meal:";
                SolidBrush unmedicatedBrush = new SolidBrush(Color.Blue);
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleString, legendFont);
                PointF vehicleStringPoint = new PointF(graph.axes.xAxisLength - vehicleStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));

                // Draw the string
                graph.graphics.DrawString(vehicleString, legendFont, legendBrush, vehicleStringPoint.X, vehicleStringPoint.Y);
                graph.graphics.FillEllipse(unmedicatedBrush, vehicleStringPoint.X + vehicleStringSize.Width, vehicleStringPoint.Y + vehicleStringSize.Height / 4, markerSize * graph.objectScale, markerSize * graph.objectScale);
            }
            else if (test == TESTTYPES.IAK)
            {
                int i = 1;
                foreach (string group in project.analysis.groups)
                {
                    if (group == "Baseline") continue;

                    string label = "Group " + group;
                    SizeF labelSize = graph.graphics.MeasureString(label, legendFont);
                    if (i == 1) // Group 1
                    {
                        Pen label1Pen = new Pen(Brushes.Red);
                        label1Pen.Width = 4F * graph.objectScale;
                        PointF labelPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.1));
                        graph.graphics.DrawString(label, legendFont, legendBrush, labelPoint.X, labelPoint.Y);
                        graph.graphics.DrawLine(label1Pen, labelPoint.X, labelPoint.Y + labelSize.Height,
                            labelPoint.X + labelSize.Width, labelPoint.Y + labelSize.Height);
                    }
                    else if (i == 2) // Group 2
                    {
                        Pen label2Pen = new Pen(Brushes.Blue);
                        label2Pen.Width = 4F * graph.objectScale;
                        PointF labelPoint = new PointF(graph.axes.xAxisLength - labelSize.Width, (float)(graph.axes.axesList[0].Y * 1.1));
                        graph.graphics.DrawString(label, legendFont, legendBrush, labelPoint.X, labelPoint.Y);
                        graph.graphics.DrawLine(label2Pen, labelPoint.X, labelPoint.Y + labelSize.Height,
                            labelPoint.X + labelSize.Width, labelPoint.Y + labelSize.Height);
                    }
                    i++;
                }
            }

            // Placement for focal seizure
            string focalSzString = "Focal Seizure:";
            SizeF focalSzStringSize = graph.graphics.MeasureString(focalSzString, legendFont);
            PointF focalSzStringPoint = new PointF(graph.axes.xAxisStart, (float)(graph.axes.axesList[0].Y * 1.15));
            graph.graphics.DrawString(focalSzString, legendFont, legendBrush, focalSzStringPoint.X, focalSzStringPoint.Y);
            graph.graphics.FillEllipse(legendBrush, focalSzStringPoint.X + focalSzStringSize.Width, focalSzStringPoint.Y + focalSzStringSize.Height / 4, markerSize, markerSize);

            // Placement for generalized seizure
            string generalSzString = "Generalized Seizure:";
            SizeF generalSzStringSize = graph.graphics.MeasureString(generalSzString, legendFont);
            PointF generalSzStringPoint = new PointF(graph.axes.xAxisLength - generalSzStringSize.Width, (float)(graph.axes.axesList[0].Y * 1.15));
            graph.graphics.DrawString(generalSzString, legendFont, legendBrush, generalSzStringPoint.X, generalSzStringPoint.Y);
            graph.graphics.DrawEllipse(szPen, generalSzStringPoint.X + generalSzStringSize.Width, generalSzStringPoint.Y + generalSzStringSize.Height / 4, markerSize, markerSize);

        }
        public void DisplayHeader()
        {
            // Initialize header information for drawing text
            string headerString = "Epilepsy Therapy Screening Program";
            Font headerFont = new Font("Arial", 16F * graph.objectScale);
            string subheader = "";

            // Define header strings (main header and sub header)
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

            PointF headerPoint = new PointF(graph.mainPlot.Width / 2 - headerSize.Width / 2, (float)(graph.mainPlot.Height * 0.01));
            PointF subPoint = new PointF(graph.mainPlot.Width / 2 - subSize.Width / 2, (float)(graph.mainPlot.Height * 0.01 + headerSize.Height));
            
            // Rectangle properties
            RectangleF headerRect = new RectangleF((int)(graph.mainPlot.Width / 2 - subSize.Width / 2), (int)(graph.mainPlot.Height * 0.01), (int)subSize.Width, (int)(headerSize.Height + subSize.Height));
            headerX = (int)headerRect.X;
            headerY = (int)(graph.mainPlot.Height * 0.01);
            headerLength = (int)headerRect.Width + headerX;
            Pen headerPen = new Pen(Brushes.Black);
            headerPen.Width = 2.0F * graph.objectScale;
            SolidBrush solidBrush = new SolidBrush(Color.LightGray);

            // Draw rectangles
            graph.graphics.FillRectangle(solidBrush, headerRect);
            graph.graphics.DrawRectangle(headerPen, headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height);

            // Now draw strings over rectangles
            graph.graphics.DrawString(headerString, headerFont, headerBrush, headerPoint);
            graph.graphics.DrawString(subheader, subFont, headerBrush, subPoint);

            // Get properties for input strings
            SizeF etspSize = graph.graphics.MeasureString(ETSP, headerFont);
            SizeF batchSize = graph.graphics.MeasureString(batch, headerFont);
            SizeF doseSize = graph.graphics.MeasureString(dose, headerFont);
            SizeF freqSize = graph.graphics.MeasureString(frequency, headerFont);
            float etspAndbatchY = (float)((headerRect.Y + headerSize.Height + subSize.Height) * 1.1);

            RectangleF etspAndbatch = new RectangleF(headerRect.X, etspAndbatchY, headerRect.Width, Math.Max(etspSize.Height, batchSize.Height));
            float doseAndfreqY = (float)((etspAndbatch.Y + Math.Max(etspSize.Height, batchSize.Height)) * 1.075);
            RectangleF doseAndfreq = new RectangleF(headerRect.X, doseAndfreqY, headerRect.Width, Math.Max(doseSize.Height, freqSize.Height));

            // Header field rectangles
            graph.graphics.FillRectangle(solidBrush, etspAndbatch);
            graph.graphics.DrawRectangle(headerPen, etspAndbatch.X, etspAndbatch.Y, etspAndbatch.Width, etspAndbatch.Height);
            graph.graphics.FillRectangle(solidBrush, doseAndfreq);
            graph.graphics.DrawRectangle(headerPen, doseAndfreq.X, doseAndfreq.Y, doseAndfreq.Width, doseAndfreq.Height);

            // Draw strings for etsp, batch, dose, frequency
            graph.graphics.DrawString(ETSP, headerFont, headerBrush, headerRect.X, etspAndbatchY);
            graph.graphics.DrawString(batch, headerFont, headerBrush, (headerRect.Width + headerRect.X - batchSize.Width), etspAndbatchY);
            graph.graphics.DrawString(dose, headerFont, headerBrush, headerRect.X, doseAndfreqY);
            graph.graphics.DrawString(frequency, headerFont, headerBrush, (headerRect.Width + headerRect.X - freqSize.Width), doseAndfreqY);

        }

        private void StatsLabels(double L, double height, int numBoxes)
        {
            // set properties for text
            // fonts
            Font headerFont = new Font("Arial", 12F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);

            // Strings, string size, string placement
            string burdenString = "Daily Seizure Burden";
            SizeF burdenSize = graph.graphics.MeasureString(burdenString, headerFont);
            var x0 = headerX - burdenSize.Width / 2;
            var y0 = graph.axes.yAxisStart - height * 8;
            var stringPlacement = ((5.0 / 4.0) * L * numBoxes) / 2;
            PointF burdenPoint = new PointF((float)(x0 + stringPlacement), (float)y0);

            string freedomString = "Seizure Freedom";
            SizeF freedomSize = graph.graphics.MeasureString(freedomString, headerFont); 
            var x1 = headerLength - freedomSize.Width / 2;
            PointF freedomPoint = new PointF((float)(x1 - stringPlacement), (float)y0);

            // Draw
            graph.graphics.DrawString(burdenString, headerFont, headerBrush, burdenPoint);
            graph.graphics.DrawString(freedomString, headerFont, headerBrush, freedomPoint);
        }
        public void DisplayStats()
        {
            // Get data
            Dictionary<string, GroupedData> allData = project.analysis.groupedData;

            Font headerFont = new Font("Arial", 12F * graph.objectScale);

            if (test == TESTTYPES.T35)
            {
                // Find the drug group without knowing the name of the drug
                List<string> copyGroups = project.analysis.groups;
                copyGroups.Remove("Baseline");
                copyGroups.Remove("vehicle");
                string drugGroup = copyGroups[0];
                // if test 35, do baseline, vehicle, and drug
                Pen boundingPen = new Pen(Brushes.Black);
                boundingPen.Width = 1.25F * graph.objectScale;

                // sz burdens
                string baselineBurden = allData["Baseline"].szBurden.ToString("N1") + "\u00B1" + allData["Baseline"].burdenSEM.ToString("N1");
                string drugBurden = allData[drugGroup].szBurden.ToString("N1") + "\u00B1" + allData[drugGroup].burdenSEM.ToString("N1");
                string vehicleBurden = allData["vehicle"].szBurden.ToString("N1") + "\u00B1" + allData["vehicle"].burdenSEM.ToString("N1");

                // sz freedoms
                string vehicleFreedom = allData["vehicle"].szFreedom.ToString("D") + "/" + allData["vehicle"].numAnimals.ToString("D");
                string drugFreedom = allData[drugGroup].szFreedom.ToString("D") + "/" + allData[drugGroup].numAnimals.ToString("D");
                string baselineFreedom = allData["Baseline"].szFreedom.ToString("D") + "/" + allData["Baseline"].numAnimals.ToString("D");

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
                // Find the drug group without knowing the name of the drug
                List<string> copyGroups = project.analysis.groups;
                copyGroups.Remove("Baseline");
                copyGroups.Remove("vehicle");
                string drugGroup = copyGroups[0];

                string drugBurden = allData[drugGroup].szBurden.ToString("N1") + "\u00B1" + allData[drugGroup].burdenSEM.ToString("N1");
                // test 36 just baseline and drug
                string baselineWilcoxon;
                string vehicleWilcoxon;
                if (allData["Baseline"].burdenPValue < 0.05)
                {
                    drugBurden += "\xB†";
                    baselineWilcoxon = "\xB† p<0.05 vs. Baseline (Wilcoxon Rank Sum)"
                                       + "(p=" + allData["Baseline"].burdenPValue.ToString("G2") + ")";
                }
                else
                {
                    baselineWilcoxon = "n.s. vs. Baseline (Wilcoxon Rank Sum)"
                                       + "(p=" + allData["Baseline"].burdenPValue.ToString("G2") + ")";
                }

                // Seizure Freedom strings
                string drugFreedom = allData[drugGroup].szFreedom.ToString("D") + "/" + allData[drugGroup].numAnimals.ToString("D");
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
            // initialize properties for string placement
            int burdenStart = headerX;
            int freedomStart = headerLength;

            // if test 35, do baseline, vehicle, and drug
            Font headerFont = new Font("Arial", 12F * graph.objectScale);
            SolidBrush headerBrush = new SolidBrush(Color.Black);
            Font statsFont = new Font("Arial", 10F * graph.objectScale);
            Pen boundingPen = new Pen(Brushes.Black);
            boundingPen.Width = 1.25F * graph.objectScale;

            SizeF baselineS = graph.graphics.MeasureString("Baseline", headerFont);

            SizeF baselineStringSize = graph.graphics.MeasureString(baselineBurden, statsFont);
            SizeF drugStringSize = graph.graphics.MeasureString(drugBurden, statsFont);
            SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleBurden, statsFont);
            float boxLength = new List<float>() { baselineS.Width, drugStringSize.Width, vehicleStringSize.Width }.Max();
            float boxHeight = new List<float>() { baselineStringSize.Height, drugStringSize.Height, vehicleStringSize.Height }.Max();

            // Use box length and height to draw seizure burden and freedom labels
            StatsLabels(boxLength, boxHeight, 3);

            var boxLabelY = graph.axes.yAxisStart - boxHeight * (float)6.25;
            var boxY = graph.axes.yAxisStart - boxHeight * 5;
            var statsLineY1 = graph.axes.yAxisStart - boxHeight * (float)3.5;
            var statsLineY2 = graph.axes.yAxisStart - boxHeight * 2;

            // Baseline Burden
            graph.graphics.DrawString(baselineBurden, statsFont, headerBrush, burdenStart, boxY);
            graph.graphics.DrawRectangle(boundingPen, burdenStart, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Baseline", headerFont, headerBrush, burdenStart, boxLabelY);

            // Drug Burden       
            graph.graphics.DrawString(drugBurden, statsFont, headerBrush, burdenStart + boxLength + boxLength / 4, boxY);
            graph.graphics.DrawRectangle(boundingPen, burdenStart + boxLength + boxLength / 4, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Drug", headerFont, headerBrush, burdenStart + boxLength + boxLength / 4, boxLabelY);

            // Vehicle Burden       
            graph.graphics.DrawString(vehicleBurden, statsFont, headerBrush, burdenStart + boxLength * 2 + boxLength / 2, boxY);
            graph.graphics.DrawRectangle(boundingPen, burdenStart + boxLength * 2 + boxLength / 2, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Vehicle", headerFont, headerBrush, burdenStart + boxLength * 2 + boxLength / 2, boxLabelY);

            // Significance statements for Sz Burden:
            graph.graphics.DrawString(baselineWilcoxon, statsFont, headerBrush, burdenStart, statsLineY1);
            graph.graphics.DrawString(vehicleWilcoxon, statsFont, headerBrush, burdenStart, statsLineY2);

            // Vehicle freedom
            float boxStart = (float)(freedomStart - boxLength);
            graph.graphics.DrawString(vehicleFreedom, statsFont, headerBrush, boxStart, boxY);
            graph.graphics.DrawRectangle(boundingPen, boxStart, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Vehicle", headerFont, headerBrush, boxStart, boxLabelY);

            // Drug freedom
            graph.graphics.DrawString(drugFreedom, statsFont, headerBrush, boxStart - boxLength - boxLength / 4, boxY);
            graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength - boxLength / 4, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Drug", headerFont, headerBrush, boxStart - boxLength - boxLength / 4, boxLabelY);

            // Baseline freedom
            graph.graphics.DrawString(baselineFreedom, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, boxY);
            graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength * 2 - boxLength / 2, boxY, boxLength, boxHeight);
            graph.graphics.DrawString("Baseline", headerFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, boxLabelY);

            // Significance statements for Sz Freedom:
            graph.graphics.DrawString(baselineFisherExact, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, statsLineY1);
            graph.graphics.DrawString(vehicleFisherExact, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, statsLineY2);
        }
        public void ExportGraph()
        {
            if (_empty)
                _empty = false;

            graph.ClearGraph();
            DrawGraph();
            graph.SaveFig();
        }
        private void AddButtons()
        {
            // create control for tool bar. this will get added to top of graph
            toolBar1 = new ToolBar();

            ToolBarButton exportButton = new ToolBarButton
            {
                Text = "Export"
            };

            ToolBarButton hideEmptyButton = new ToolBarButton
            {
                Text = "Hide Empty"
            };

            ToolBarButton hideSzButton = new ToolBarButton
            {
                Text = "Hide Seizure"
            };

            ToolBarButton hideTrtButton = new ToolBarButton
            {
                Text = "Hide Treatment"
            };

            ToolBarButton clearGraph = new ToolBarButton
            {
                Text = "Clear"
            };

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
                case 0: 
                    // export button
                    ExportGraph();
                    break;

                case 1: 
                    // hide empty time
                    if (_empty)
                         _empty = false;
                    else
                        _empty = true;

                    graph.ClearGraph();
                    DrawGraph();
                    break;

                case 2: 
                    // hide seizures
                    if (_sz)
                        _sz = false;
                    else
                        _sz = true;

                    graph.ClearGraph();
                    DrawGraph();
                    break;

                case 3: 
                    // hide treatments
                    if (_treatment)
                        _treatment = false;
                    else
                        _treatment = true;

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
            // Draw axes
            graph.DrawAxes(4);
            graph.BoundingBox();
            Font aFont = new Font("Arial", 14 * graph.objectScale);
            graph.DrawTicks(graph.axes.xTicks, project.Animals.Count, 3.0F);
            graph.WriteXLabel("Time (days)", aFont);
            graph.WriteYLabel("Animals", aFont);

            // Draw data
            if (_sz)
                PlotSz();

            if (_treatment)
                PlotTrt();

            if (_empty)
                PlotEmpty();

            // Draw descriptors
            Legend();
            DisplayHeader();
            DisplayStats();

            // Display
            graph.DisplayGraph();
        }


    }

}
