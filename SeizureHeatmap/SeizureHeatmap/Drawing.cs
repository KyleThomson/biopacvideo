using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SeizureHeatmap
{
    class Drawing
    {
        public Graphics graphics;
        public int Xmax, Ymax;
        public Pen B = new Pen(Color.Black, width: (float)2.5);
        public PictureBox picture = new PictureBox();
        public Form1 myForm = new Form1();
        public int xAxisLength;
        public int yAxisLength;
        GraphProperties GRAPH;
        
        public Drawing(int X, int Y)
        {
            GRAPH = new GraphProperties();
            GRAPH.mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            GRAPH.graphics = Graphics.FromImage(GRAPH.mainPlot);
            GRAPH.graphForm = myForm;
            GRAPH.picture = picture;
            GRAPH.Xmax = X;
            GRAPH.Ymax = Y;
            int Xmax = X;
            int Ymax = Y;
            GRAPH.picture.ClientSize = new Size(Xmax, Ymax);
            GRAPH.graphics.Clear(Color.White);
            GRAPH.graphForm.Size = new Size(Xmax, Ymax);
            GRAPH.picture.Image = GRAPH.mainPlot;
            GRAPH.graphForm.Controls.Add(picture);
            GRAPH.xTickPoints = new List<float>();
            GRAPH.yTickPoints = new List<float>();
        }

        public void InitDrawing(int X, int Y)
        {
            //offScreen = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            //graphics = Graphics.FromImage(offScreen);
            //int Xmax = X;
            //int Ymax = Y;
            //picture.ClientSize = new Size(Xmax, Ymax);
            //try
            //{
            //    graphics.Clear(Color.White);
            //    var myForm = new Form1();
            //    graphics.Clear(Color.White);
            //    picture.Image = offScreen;
            //    myForm.Controls.Add(picture);
            //}
            //catch
            //{
            //}
        }
        public void DrawAxes(float penWidth, int Xmax, int Ymax)
        {
            float xAxisLength = (float)(Xmax * 0.75);
            float yAxisLength = (float)(Ymax * 0.75);
            float xAxisStart = (float)(Xmax * 0.25);
            float yAxisStart = (float)(Ymax * 0.25);

            PointF xAxisStartPoint = new PointF(xAxisStart, Ymax - yAxisStart);
            PointF xAxisEndPoint = new PointF(xAxisLength, Ymax - yAxisStart);
            PointF yAxisStartPoint = new PointF(xAxisStart, yAxisStart);
            PointF yAxisEndPoint = new PointF(xAxisStart, yAxisLength);

            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth;
            // X Axis First
            GRAPH.graphics.DrawLine(axisPen, xAxisStartPoint, xAxisEndPoint);
            // Y Axis
            GRAPH.graphics.DrawLine(axisPen, yAxisStartPoint, yAxisEndPoint);
        }
        public void DisplayGraph()
        {
            myForm.Show();
        }
        public void DrawTicks(int xTicks, int yTicks, int xAxisLength, int yAxisLength, float tickWidth, List<string> xTickLabels, List<string> yTickLabels)
        {
            Pen tickPen = new Pen(Brushes.Black);
            tickPen.Width = tickWidth;
            float xTickSpacing = (float)(xAxisLength / (xTicks * 2));
            float yTickSpacing = (float)(yAxisLength / (yTicks * 2));
            float xAxisStart = (float)(xAxisLength * 0.25);
            float yAxisStart = (float)(yAxisLength * 0.25);
            Font xFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            for (int i = 0; i < xTicks; i++)
            {
                PointF xTickStart = new PointF(xAxisStart + xTickSpacing * (i + 1), yAxisLength - yAxisStart);
                GRAPH.xTickPoints.Add(xAxisStart + xTickSpacing * (i + 1));
                PointF xTickEnd = new PointF(xAxisStart + xTickSpacing * (i + 1), (float)((yAxisLength - yAxisStart) * 0.99));
                PointF xTickLabelLoc = new PointF(xAxisStart + xTickSpacing * (i), (float)(yAxisLength - yAxisStart * 0.8));
                GRAPH.graphics.DrawLine(tickPen, xTickStart, xTickEnd);
                // x tick label
                SizeF xTickSize = new SizeF();
                xTickSize = GRAPH.graphics.MeasureString(xTickLabels[i], xFont);
                RectangleF xTickRect = new RectangleF(xAxisStart - (xTickSize.Width) / 2 + xTickSpacing * (i + 1), (float)(yAxisLength - yAxisStart*.975), xTickSize.Width, xTickSize.Height);
                GRAPH.graphics.DrawString(xTickLabels[i], xFont, drawBrush, xTickRect);


            }

            for (int i = 0; i < yTicks; i++)
            {
                PointF yTickStart = new PointF(xAxisStart, yAxisLength - yAxisStart - yTickSpacing * (i + 1));
                GRAPH.yTickPoints.Add(yAxisLength - yAxisStart - yTickSpacing * (i + 1));
                PointF yTickEnd = new PointF((float)(xAxisStart * 1.02), (float)((yAxisLength - yAxisStart - yTickSpacing * (i + 1))));
                GRAPH.graphics.DrawLine(tickPen, yTickStart, yTickEnd);
                // y tick label
                SizeF yTickSize = new SizeF();
                yTickSize = GRAPH.graphics.MeasureString(yTickLabels[i], xFont);
                RectangleF yTickRect = new RectangleF((float)(xAxisStart * 0.975) - yTickSize.Width, yAxisLength - yAxisStart - yTickSpacing * (i + 1) - yTickSize.Height/2, yTickSize.Width, yTickSize.Height);
                GRAPH.graphics.DrawString(yTickLabels[i], xFont, drawBrush, yTickRect);
            }
        }
        public void WriteXLabel(string xLabel, Font font)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            RectangleF xLabelRect = new RectangleF((float)(GRAPH.Xmax / 2.5), (float)(GRAPH.Ymax - GRAPH.Ymax * 0.20), 200, 50);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            GRAPH.graphics.DrawString(xLabel, font, drawBrush, xLabelRect, drawFormat);
        }
        public void WriteYLabel(string yLabel, Font font)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat yLabelFormat = new StringFormat();
            yLabelFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            RectangleF yLabelRect = new RectangleF((float)(GRAPH.Xmax * 0.15), (float)(GRAPH.Ymax / 2.25), 50, 200);
            GRAPH.graphics.DrawString(yLabel, font, drawBrush, yLabelRect, yLabelFormat);
        }
        public void WriteXTickLabel(int xTicks, string label)
        {
            
            StringFormat xLabelFormat = new StringFormat();
            float xAxisLength = (float)(GRAPH.Xmax * 0.75);
            float xAxisStart = (float)(GRAPH.Xmax * 0.25);
            float xCenter = (xAxisLength - xAxisStart) / 2;
            float yAxisLength = (float)(GRAPH.Ymax * 0.75);
            float yAxisStart = (float)(yAxisLength * 0.25);
            float yCenter = (float)((yAxisLength - yAxisStart) * 0.8);
        }

        public void DrawDataPoint(float xCoord, float yCoord)
        {

        }
        public void DrawVector(int xPoints, int yPoints)
        {
            Pen dataPen = new Pen(Brushes.Black);
            // Draw each point in paired list similarly to tick placement
                for (int i = 0; i < yPoints; i++)
                {
                    for (int k = 0; k < xPoints; k++)
                    {
                        GRAPH.graphics.DrawEllipse(dataPen, GRAPH.xTickPoints[k], GRAPH.yTickPoints[i], 4, 4);
                    }
                    // Find where to place each seizure
                }

        }
    }
}
