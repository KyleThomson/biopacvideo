using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ProjectManager
{
    class SzGraph
    {
        // instance of graph for test 35 or test 36
        public GraphProperties graph;

        // string variables for header info for the pdf
        public TESTTYPES test;
        public string header;
        public string subHeader;
        public string ETSP = "ETSP: ";
        public string batch = "Dose: ";
        public string dose = "Dose: ";
        public string frequency = "Frequency: ";
        public DateTime Earliest; public DateTime Latest;
        public SzGraph(int X, int Y, Project pjt)
        {
            // Type of test
            test = pjt.test;

            // Find max day of project
            Earliest = pjt.Files[0].Start.Date;
            Latest = pjt.Files[pjt.Files.Count - 1].Start.Date;
            double totalHours = Latest.Subtract(Earliest).TotalHours;
            int tempMax = (int)Math.Round(totalHours / 24, 2);

            // x tick every 7 days           
            int xTickInterval = 7;
            
            // Get nearest multiple of 7 days
            int nearestMultiple = (int)Math.Round(tempMax / (double)xTickInterval, MidpointRounding.AwayFromZero) * xTickInterval;
            int numXTicks = (nearestMultiple / xTickInterval) + 1;

            // Initialize graph by drawing labels, tick points, inputting numbers into GraphProperties
            graph = new GraphProperties(X, Y, nearestMultiple, pjt.Animals.Count);
            graph.DrawAxes(4);
            graph.BoundingBox(4);

            // Get axis tick labels for graph properties
            List<string> xTickString = GetXTickLabels(pjt, xTickInterval);
            List<string> yTickString = GetYTickLabels(pjt);

            // Draw ticks and label axes           
            Font aFont = new Font("Arial", 12 * graph.objectScale);
            graph.DrawTicks(numXTicks, pjt.Animals.Count, 3.0F, xTickString, yTickString);
            graph.WriteXLabel("Time (days)", aFont);
            graph.WriteYLabel("Animals", aFont);

            // Set test descriptors
            SetTestDescriptors();
        }
        private List<string> GetXTickLabels(Project pjt, int xTickInterval)
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
        private List<string> GetYTickLabels(Project pjt)
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
            for (int i = 0; i < pjt.Animals.Count; i++)
            {
                float yCoord = i + 1;
                for (int j = 0; j < pjt.Animals[i].Sz.Count; j++)
                {
                    float xCoord = (float)(Math.Round((pjt.Animals[i].Sz[j].d.Date.Subtract(Earliest).TotalHours + pjt.Animals[i].Sz[j].t.TotalHours) / 24, 2));
                    if (pjt.Animals[i].Sz[j].Severity > 0)
                    {
                        graph.PlotPoints((float)(xCoord), yCoord, markerSize, "o", szColor);
                    }
                    else if (pjt.Animals[i].Sz[j].Severity == 0)
                    {
                        graph.PlotPoints((float)(xCoord), (float)(yCoord - 0.05), markerSize / 2, ".", szColor);
                    }

                }
            }
        }
        public void PlotTrt(Project pjt)
        {
            float lineWidth = 4;
            Color vehicleColor = Color.FromName("Teal");
            Color drugColor = Color.FromName("Red");

            // If Test 35, use injections to draw lines for treatment
            if (test == TESTTYPES.T35)
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
                            if (I.ADDID == "vehicle")
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
            else if (test == TESTTYPES.T36)
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
        public void PlotEmpty(Project pjt)
        {
            Color lineColor = Color.FromName("Black");
            float lineWidth = 4;
            float time0 = (float)Earliest.Subtract(Earliest).TotalHours;
            float maxTime = (float)Math.Round(Latest.Subtract(Earliest).TotalHours / 24, 2);
            // Plot the missing time an animal has
            int i = 0;
            foreach (AnimalType animal in pjt.Animals)
            {
                float yCoord = (float)(i + 0.5);
                if (animal.earliestAppearance != default)
                {
                    float x2 = (float)Math.Round(animal.earliestAppearance.Subtract(Earliest).TotalHours / 24, 2);
                    graph.Line(time0, yCoord, x2, yCoord, lineWidth, lineColor);
                }
                if (animal.latestAppearance != default)
                {
                    float x2 = (float)Math.Round(animal.latestAppearance.Subtract(Earliest).TotalHours / 24, 2);
                    graph.Line(x2, yCoord, maxTime, yCoord, lineWidth, lineColor);
                }
                i++;
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
            if (test == TESTTYPES.T35)
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
            else if (test == TESTTYPES.T36)
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
            if (test == TESTTYPES.T35)
            {
                subheader = "Test 35 - Chronic Post-SE (KA) Spontaneously Seizing Rats: Stage 1 (IP Administration)";
            }
            else if (test == TESTTYPES.T36)
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
            SolidBrush solidBrush = new SolidBrush(Color.Gray);
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

            if (test == TESTTYPES.T35)
            {
                // if test 35, do baseline, vehicle, and drug
                Font statsFont = new Font("Arial", 8F * graph.objectScale);
                Pen boundingPen = new Pen(Brushes.Black);
                boundingPen.Width = 1.25F * graph.objectScale;

                string baselineBurden = pjt.analysis.avgBaseBurden.ToString() + "\u00B1" + pjt.analysis.baselineSEM.ToString();
                string drugBurden = pjt.analysis.avgDrugBurden.ToString() + "\u00B1" + pjt.analysis.drugSEM.ToString();
                string vehicleBurden = pjt.analysis.avgVehBurden.ToString() + "\u00B1" + pjt.analysis.vehicleSEM.ToString();
                SizeF baselineS = graph.graphics.MeasureString("Baseline", headerFont);

                SizeF baselineStringSize = graph.graphics.MeasureString(baselineBurden, statsFont);
                SizeF drugStringSize = graph.graphics.MeasureString(drugBurden, statsFont);
                SizeF vehicleStringSize = graph.graphics.MeasureString(vehicleBurden, statsFont);
                float boxLength = new List<float>() { baselineS.Width, drugStringSize.Width, vehicleStringSize.Width }.Max();
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
                float boxStart = (float)(graph.mainPlot.Width * 0.75 - boxLength);
                string vehicleFreedom = pjt.analysis.vehFreedomSum.ToString() + "/" + pjt.vehicleAnimals.ToString();
                graph.graphics.DrawString(vehicleFreedom, statsFont, headerBrush, boxStart, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Vehicle", headerFont, headerBrush, boxStart, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Drug freedom
                string drugFreedom = pjt.analysis.drugFreedomSum.ToString() + "/" + pjt.drugAnimals.ToString();
                graph.graphics.DrawString(drugFreedom, statsFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Drug", headerFont, headerBrush, boxStart - boxLength - boxLength / 4, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));

                // Baseline freedom
                string baselineFreedom = pjt.analysis.baseFreedomSum.ToString() + "/" + pjt.baselineAnimals.ToString();
                graph.graphics.DrawString(baselineFreedom, statsFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8));
                graph.graphics.DrawRectangle(boundingPen, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8), boxLength, boxHeight);
                graph.graphics.DrawString("Baseline", headerFont, headerBrush, boxStart - boxLength * 2 - boxLength / 2, (float)(graph.yAxisStart * 0.8 - boxHeight * 1.25));
            }
            else if (test == TESTTYPES.T36)
            {
                // test 36 just baseline and drug
            }
        }
        public void ExportPDF()
        {

        }


    }

}
