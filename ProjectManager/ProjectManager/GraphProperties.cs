using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace ProjectManager
{
    public class GraphProperties
    {
        public Bitmap mainPlot;
        public Graphics graphics;
        public Pen axisPen;
        public float xAxisLength;
        public float yAxisLength;
        public float xAxisStart;
        public float yAxisStart;
        public List<float> xTickPoints;
        public List<float> yTickPoints;
        public float maxXData;
        public float maxYData;
        public PictureBox picture = new PictureBox();
        public Form graphForm = new Form();
        public List<PointF> axes;
        public int X; public int Y;
        public void InitGraph()
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

            // Set smoothing mode for graphics in initialization. This will smooth out edges when drawing round objects.
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // High quality interpolation makes rescaling of image maintain resolution.
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        }
        public List<PointF> DrawAxes(float penWidth)
        {
            float xAxisLength = (float)(X * 0.75);
            float yAxisLength = (float)(Y * 0.75);
            float xAxisStart = (float)(X * 0.25);
            float yAxisStart = (float)(Y * 0.25);

            
            PointF xAxisStartPoint = new PointF(xAxisStart, Y - yAxisStart);
            PointF xAxisEndPoint = new PointF(xAxisLength, Y - yAxisStart);
            PointF yAxisStartPoint = new PointF(xAxisStart, yAxisStart);
            PointF yAxisEndPoint = new PointF(xAxisStart, yAxisLength);

            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth;
            // X Axis First
            graphics.DrawLine(axisPen, xAxisStartPoint, xAxisEndPoint);
            // Y Axis
            graphics.DrawLine(axisPen, yAxisStartPoint, yAxisEndPoint);
            List<PointF> axes = new List<PointF>();
            axes.Add(xAxisStartPoint);
            axes.Add(xAxisEndPoint);
            axes.Add(yAxisStartPoint);
            axes.Add(yAxisEndPoint);
            return axes;
        }
        public void BoundingBox(float penWidth)
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth;
            //Horizontal bounding line
            graphics.DrawLine(axisPen, new PointF(axes[0].X-2, (float)(axes[2].Y - axes[2].Y * 0.01)), new PointF(axes[1].X+2, (float)(axes[2].Y - axes[2].Y * 0.01)));
            //Vertical bounding line
            graphics.DrawLine(axisPen, new PointF(axes[1].X, axes[2].Y), new PointF(axes[1].X, axes[3].Y));

        }
        public List<float> DrawTicks(int xTicks, int yTicks, int xAxisLength, int yAxisLength, float tickWidth, List<string> xTickLabels, List<string> yTickLabels)
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
                float currentXPoint = (float)(Convert.ToDouble(xTickLabels[i])/maxXData)*(axes[1].X - axes[0].X);
                // create length of x tick
                PointF xTickStart = new PointF(xAxisStart + currentXPoint, yAxisLength - yAxisStart);
                xTickPoints.Add(xAxisStart + currentXPoint);
                PointF xTickEnd = new PointF(xAxisStart + currentXPoint, (float)((yAxisLength - yAxisStart) * 0.99));
                graphics.DrawLine(tickPen, xTickStart, xTickEnd);

                // x tick label
                SizeF xTickSize = new SizeF();
                xTickSize = graphics.MeasureString(xTickLabels[i], xFont);
                RectangleF xTickRect = new RectangleF(xAxisStart - (xTickSize.Width) / 2 + currentXPoint, (float)(yAxisLength - yAxisStart * .975), xTickSize.Width, xTickSize.Height);
                graphics.DrawString(xTickLabels[i], xFont, drawBrush, xTickRect);


            }

            for (int i = 0; i < yTicks; i++)
            {
                // create length of ticks
                PointF yTickStart = new PointF(xAxisStart, (float)(yAxisLength - yAxisStart - yTickSpacing * (i + 0.5)));
                yTickPoints.Add((float)(yAxisLength - yAxisStart - yTickSpacing * (i + 0.5)));
                PointF yTickEnd = new PointF((float)(xAxisStart * 1.02), (float)((yAxisLength - yAxisStart - yTickSpacing * (i + 0.5))));
                graphics.DrawLine(tickPen, yTickStart, yTickEnd);

                // y tick label
                SizeF yTickSize = new SizeF();
                yTickSize = graphics.MeasureString(yTickLabels[i], xFont);
                RectangleF yTickRect = new RectangleF((float)(xAxisStart * 0.975) - yTickSize.Width, (float)(yAxisLength - yAxisStart - yTickSpacing * (i + 0.5) - yTickSize.Height / 2), yTickSize.Width, yTickSize.Height);
                graphics.DrawString(yTickLabels[i], xFont, drawBrush, yTickRect);
            }
            List<float> axesStarts = new List<float>();
            axesStarts.Add(xAxisLength);
            axesStarts.Add(yAxisLength);
            return axesStarts;
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
        public void PlotPoints(float xCoord, float yCoord, int markerSize, string markerType)
        {          
            Pen dataPen = new Pen(Brushes.Black);
            SolidBrush dataBrush = new SolidBrush(Color.Black);

            // Scale input coordinates to pixels using where the axes were drawn and the maximum X and Y data
            float xAxisStart = (float)(xAxisLength * 0.25);
            float yAxisStart = (float)(yAxisLength * 0.75);
            float xStart = axes[0].X;
            float xEnd = axes[1].X;
            float yStart = axes[2].Y;
            float yEnd = axes[3].Y;
            float realXCoord = (xCoord / maxXData) * (xEnd - xStart);
            float realYCoord = (float)((yCoord / maxYData) * (yStart - yEnd));
            //float realYCoord = yCoord;

            if (markerType == "o")
            {
                graphics.DrawEllipse(dataPen, realXCoord + xAxisStart, realYCoord + yAxisStart, markerSize, markerSize);
            }
            else if(markerType == ".")
            {
                graphics.FillEllipse(dataBrush, realXCoord + xAxisStart, realYCoord + yAxisStart, markerSize, markerSize);
            }
            else if(markerType == "d")
            {
                //gotta do some math to draw a rhombus/diamond marker
                DrawDiamond(realXCoord + xAxisStart, realYCoord, markerSize);
                // If we lived in a perfect world this would draw a diamond.
            }
            else
            {
                //no marker shape selected?
            }
        }
        public void Line(float x1Coord, float y1Coord, float x2Coord, float y2Coord, float lineWidth, Color color)
        {
            // Adjust pen to user inputted width and color
            Pen dataPen = new Pen(Brushes.Black);
            dataPen.Color = color;
            dataPen.Width = lineWidth;

            // Scale input coordinates to pixels using where the axes were drawn and the maximum X and Y data
            float xAxisStart = (float)(xAxisLength * 0.25);
            float yAxisStart = (float)(yAxisLength * 0.75);
            float xStart = axes[0].X;
            float xEnd = axes[1].X;
            float yStart = axes[2].Y;
            float yEnd = axes[3].Y;

            float realX1Coord = (x1Coord / maxXData) * (xEnd - xStart);
            float realY1Coord = (y1Coord / maxYData) * (yStart - yEnd);
            float realX2Coord = (x2Coord / maxXData) * (xEnd - xStart);
            float realY2Coord = (y2Coord / maxYData) * (yStart - yEnd);

            PointF startPoint = new PointF(realX1Coord + xAxisStart, realY1Coord + yAxisStart);
            PointF endPoint = new PointF(realX2Coord + xAxisStart, realY2Coord + yAxisStart);
            graphics.DrawLine(dataPen, startPoint, endPoint);
        }
        public void DisplayGraph()
        {
            graphForm.Show();
        }
        public void DrawDiamond(float centerX, float centerY, float size)
        {
            Pen dataPen = new Pen(Brushes.Black);
            SolidBrush dataBrush = new SolidBrush(Color.Black);

            PointF left = new PointF(centerX - size, centerY);
            PointF right = new PointF(centerX + size, centerY);
            PointF top = new PointF(centerX, centerY + size);
            PointF bottom = new PointF(centerX, centerY - size);

            PointF[] dPoints = new PointF[] { left, right, top, bottom };

            graphics.DrawPolygon(dataPen, dPoints);
        }
        public void TextBox(string inputStr, Color color)
        {
            Pen dataPen = new Pen(Brushes.Black);
            SolidBrush dataBrush = new SolidBrush(Color.Black);
            graphics.DrawString(inputStr, new Font("Arial",12), dataBrush, X / 2, Y / 6);
        }
    }
}
