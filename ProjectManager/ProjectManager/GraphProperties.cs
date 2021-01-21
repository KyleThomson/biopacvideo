using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace ProjectManager
{
    class GraphProperties
    {
        public Bitmap mainPlot;
        public Graphics graphics;
        public Pen axisPen;
        public int xAxisLength;
        public int yAxisLength;
        public float xAxisStart;
        public float yAxisStart;
        public List<float> xTickPoints;
        public List<float> yTickPoints;
        public PictureBox picture = new PictureBox();
        public Form graphForm = new Form();
        public void InitGraph(int X, int Y)
        {
            mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            graphics = Graphics.FromImage(mainPlot);
            picture.ClientSize = new Size(X, Y);
            graphics.Clear(Color.White);
            graphForm.Size = new Size(X, Y);
            picture.Image = mainPlot;
            graphForm.Controls.Add(picture);
            xTickPoints = new List<float>();
            yTickPoints = new List<float>();
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
            graphics.DrawLine(axisPen, xAxisStartPoint, xAxisEndPoint);
            // Y Axis
            graphics.DrawLine(axisPen, yAxisStartPoint, yAxisEndPoint);
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
                xTickPoints.Add(xAxisStart + xTickSpacing * (i + 1));
                PointF xTickEnd = new PointF(xAxisStart + xTickSpacing * (i + 1), (float)((yAxisLength - yAxisStart) * 0.99));
                graphics.DrawLine(tickPen, xTickStart, xTickEnd);
                // x tick label
                SizeF xTickSize = new SizeF();
                xTickSize = graphics.MeasureString(xTickLabels[i], xFont);
                RectangleF xTickRect = new RectangleF(xAxisStart - (xTickSize.Width) / 2 + xTickSpacing * (i + 1), (float)(yAxisLength - yAxisStart * .975), xTickSize.Width, xTickSize.Height);
                graphics.DrawString(xTickLabels[i], xFont, drawBrush, xTickRect);


            }

            for (int i = 0; i < yTicks; i++)
            {
                PointF yTickStart = new PointF(xAxisStart, yAxisLength - yAxisStart - yTickSpacing * (i + 1));
                yTickPoints.Add(yAxisLength - yAxisStart - yTickSpacing * (i + 1));
                PointF yTickEnd = new PointF((float)(xAxisStart * 1.02), (float)((yAxisLength - yAxisStart - yTickSpacing * (i + 1))));
                graphics.DrawLine(tickPen, yTickStart, yTickEnd);
                // y tick label
                SizeF yTickSize = new SizeF();
                yTickSize = graphics.MeasureString(yTickLabels[i], xFont);
                RectangleF yTickRect = new RectangleF((float)(xAxisStart * 0.975) - yTickSize.Width, yAxisLength - yAxisStart - yTickSpacing * (i + 1) - yTickSize.Height / 2, yTickSize.Width, yTickSize.Height);
                graphics.DrawString(yTickLabels[i], xFont, drawBrush, yTickRect);
            }
        }
        public void WriteXLabel(string xLabel, Font font, int Xmax, int Ymax)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            RectangleF xLabelRect = new RectangleF((float)(Xmax / 2.5), (float)(Ymax - Ymax * 0.20), 200, 50);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            graphics.DrawString(xLabel, font, drawBrush, xLabelRect, drawFormat);
        }
        public void WriteYLabel(string yLabel, Font font, int Xmax, int Ymax)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat yLabelFormat = new StringFormat();
            yLabelFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            RectangleF yLabelRect = new RectangleF((float)(Xmax * 0.15), (float)(Ymax / 2.25), 50, 200);
            graphics.DrawString(yLabel, font, drawBrush, yLabelRect, yLabelFormat);
        }
        public void PlotPoints(float xCoord, float yCoord, int markerSize)
        {
            
            Pen dataPen = new Pen(Brushes.Black);
            graphics.DrawEllipse(dataPen, xCoord, yCoord, markerSize, markerSize);
        }
        public void DisplayGraph()
        {
            graphForm.Show();
        }
    }
}
