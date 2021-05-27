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
        public Axes axes;
        public List<float> xTickPoints;
        public List<float> yTickPoints;
        public float maxXData;
        public float maxYData;
        public PictureBox picture = new PictureBox();
        public Form graphForm = new Form();
        public List<PointF> axesPoints;
        public int X; public int Y;
        public float scale;
        public float objectScale;
        public int screenWidth { get; set; } public int screenHeight { get; set; }

        public GraphProperties(int width, int height, float maxX, float maxY)
        {
            // First find resolution that graphics will be scaled to
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            X = width; Y = height;

            // bitmap initialization
            mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            graphics = Graphics.FromImage(mainPlot);
            graphics.Clear(Color.White);

            // Create axes
            axes = new Axes(X, Y);
            axesPoints = axes.axesList;

            xTickPoints = new List<float>();
            yTickPoints = new List<float>();


            // Set smoothing mode for graphics in initialization. This will smooth out edges when drawing round objects.
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // High quality interpolation makes rescaling of image maintain resolution.
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // calculate target scaling factor to maintain graph aspect ratio on any screen
            scale = Math.Min(screenWidth / (float)width, screenHeight / (float)height);
            objectScale = 1 / scale;

            // Set input arguments as max data
            maxXData = maxX; maxYData = maxY;

        }
        public void DrawAxes(float penWidth)
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth * objectScale;
            // X Axis First
            graphics.DrawLine(axisPen, axesPoints[0], axesPoints[1]);
            // Y Axis
            graphics.DrawLine(axisPen, axesPoints[2], axesPoints[3]);
        }
        public void BoundingBox(float penWidth)
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth * objectScale;
            //Horizontal bounding line
            graphics.DrawLine(axisPen, new PointF((float)(axesPoints[0].X*0.995), (float)(axesPoints[2].Y - axesPoints[2].Y * 0.011)), new PointF((float)(axesPoints[1].X*1.0025), (float)(axesPoints[2].Y - axesPoints[2].Y * 0.011)));
            //Vertical bounding line
            graphics.DrawLine(axisPen, new PointF(axesPoints[1].X, axesPoints[2].Y), new PointF(axesPoints[1].X, axesPoints[3].Y));

        }
        public void DrawTicks(int xTicks, int yTicks, float tickWidth, List<string> xTickLabels, List<string> yTickLabels)
        {
            Pen tickPen = new Pen(Brushes.Black);
            tickPen.Width = tickWidth * objectScale;
            axes.tickWidth = tickWidth;
            axes.xTicks = xTicks;
            axes.yTicks = yTicks;
            float xTickSpacing = (float)(axes.xAxisLength / (xTicks * 2));
            float yTickSpacing = (float)(axes.yAxisLength / (yTicks * 1.5));
            Font xFont = new Font("Arial", 10 * objectScale);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float maxXTick = (float)Convert.ToDouble(xTickLabels[xTicks - 1]);
            for (int i = 0; i < xTicks; i++)
            {
                float currentXPoint = (float)(((Convert.ToDouble(xTickLabels[i]) + 0.5) / maxXTick) * (axesPoints[1].X - axesPoints[0].X) * 0.95);
                // create length of x tick
                PointF xTickStart = new PointF(axes.xAxisStart + currentXPoint, axes.yAxisLength);
                xTickPoints.Add(axes.xAxisStart + currentXPoint);
                PointF xTickEnd = new PointF(axes.xAxisStart + currentXPoint, (float)((axes.yAxisLength) * 0.99));
                graphics.DrawLine(tickPen, xTickStart, xTickEnd);

                // x tick label
                SizeF xTickSize = graphics.MeasureString(xTickLabels[i], xFont);
                RectangleF xTickRect = new RectangleF(axes.xAxisStart - (xTickSize.Width) / 2 + currentXPoint, (float)(axes.yAxisLength * 1.025), xTickSize.Width, xTickSize.Height);
                graphics.DrawString(xTickLabels[i], xFont, drawBrush, xTickRect);


            }

            for (int i = 0; i < yTicks; i++)
            {
                // create length of ticks
                PointF yTickStart = new PointF(axes.xAxisStart, (float)(axes.yAxisLength - yTickSpacing * (i + 0.5)));
                yTickPoints.Add((float)(axes.yAxisLength - yTickSpacing * (i + 0.5)));
                PointF yTickEnd = new PointF((float)(axes.xAxisStart * 1.02), (float)((axes.yAxisLength - yTickSpacing * (i + 0.5))));
                graphics.DrawLine(tickPen, yTickStart, yTickEnd);

                // y tick label
                SizeF yTickSize = graphics.MeasureString(yTickLabels[i], xFont);
                RectangleF yTickRect = new RectangleF((float)(axes.xAxisStart * 0.975) - yTickSize.Width, (float)(axes.yAxisLength - yTickSpacing * (i + 0.5) - yTickSize.Height / 2), yTickSize.Width, yTickSize.Height);
                graphics.DrawString(yTickLabels[i], xFont, drawBrush, yTickRect);
            }
        }
        public void WriteXLabel(string xLabel, Font font)
        {   
            // Format string
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            // Get size of the string
            SizeF xLabelSize = graphics.MeasureString(xLabel, font);

            // Use size of string and length of axis to center the label
            float xPoint = (axes.xAxisLength + axes.xAxisStart) / 2 - xLabelSize.Width / 2;
            float yPoint = axesPoints[0].Y;
            RectangleF xLabelRect = new RectangleF((xPoint), (float)(yPoint * 1.05), xLabelSize.Width, xLabelSize.Height);
            graphics.DrawString(xLabel, font, drawBrush, xLabelRect, drawFormat);
        }
        public void WriteYLabel(string yLabel, Font font)
        {
            // Format string
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat yLabelFormat = new StringFormat();
            yLabelFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            // Get size of the string
            SizeF yLabelSize = graphics.MeasureString(yLabel, font);

            // Use size of string and length of axis to center the label
            float xPoint = axesPoints[0].X;
            float yPoint = (axes.yAxisLength + axes.yAxisStart) / 2 - yLabelSize.Width / 2;
            RectangleF yLabelRect = new RectangleF((float)(xPoint * 0.70), yPoint, yLabelSize.Height, yLabelSize.Width);
            graphics.DrawString(yLabel, font, drawBrush, yLabelRect, yLabelFormat);
        }
        public void PlotPoints(float xCoord, float yCoord, int markerSize, string markerType, Color color)
        {          
            Pen dataPen = new Pen(Brushes.Black);
            dataPen.Color = color;
            dataPen.Width = dataPen.Width * objectScale;
            SolidBrush dataBrush = new SolidBrush(Color.Black);
            dataBrush.Color = color;

            // Calculate a scale factor that is in units of Pixels/unit
            float xScale = (xTickPoints[xTickPoints.Count-1] - xTickPoints[0]) / maxXData;
            float yScale = (axes.yAxisLength - axes.yAxisStart) / maxYData;

            // Convert input coordinate points
            float realXCoord = xCoord * xScale + xTickPoints[0] - (markerSize * objectScale);
            float realYCoord = (float)(axes.yAxisLength - yCoord * yScale) + (markerSize * objectScale);

            // Marker type selection
            if (markerType == "o")
            {
                graphics.DrawEllipse(dataPen, realXCoord , realYCoord, markerSize * objectScale, markerSize * objectScale);
            }
            else if(markerType == ".")
            {
                graphics.FillEllipse(dataBrush, realXCoord, realYCoord - (markerSize * objectScale) / 2, markerSize * objectScale, markerSize * objectScale);
            }
            else if(markerType == "d")
            {
                //gotta do some math to draw a rhombus/diamond marker
                DrawDiamond(realXCoord + axes.xAxisStart, realYCoord, markerSize * objectScale);
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
            dataPen.Width = lineWidth * objectScale;

            // Calculate a scale factor that is in units of Pixels/unit
            float xScale = (xTickPoints[xTickPoints.Count - 1] - xTickPoints[0]) / maxXData;
            float yScale = (axes.yAxisLength - axes.yAxisStart) / maxYData;

            // Convert input coordinate points
            float realX1Coord = x1Coord * xScale + xTickPoints[0];
            float realY1Coord = axes.yAxisLength - y1Coord * yScale;
            float realX2Coord = x2Coord * xScale + xTickPoints[0];
            float realY2Coord = axes.yAxisLength - y2Coord * yScale;

            PointF startPoint = new PointF(realX1Coord, realY1Coord);
            PointF endPoint = new PointF(realX2Coord, realY2Coord);
            graphics.DrawLine(dataPen, startPoint, endPoint);
        }
        public void DisplayGraph()
        {
            // Scaled dimensions of new graph
            var scaleWidth = (int)(mainPlot.Width * scale);
            var scaleHeight = (int)(mainPlot.Height * scale);

            // Create new bitmap and graphics to fit graph to monitor
            //Bitmap bmp = new Bitmap(mainPlot, new Size(screenWidth, screenHeight));
            Bitmap bmp = new Bitmap(mainPlot, new Size(scaleWidth, scaleHeight));
            var newGfx = Graphics.FromImage(bmp);
            newGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            newGfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Re-draw the graphics we already created
            var brush = new SolidBrush(Color.Black);
            //newGfx.FillRectangle(brush, new RectangleF(0, 0, mainPlot.Width, mainPlot.Height));
            //newGfx.DrawImage(mainPlot, (screenWidth - scaleWidth) / 2, (screenHeight - scaleHeight) / 2, scaleWidth, scaleHeight);
            newGfx.DrawImage(mainPlot, 0, 0, scaleWidth, scaleHeight);

            PictureBox resizedPicture = new PictureBox();
            resizedPicture.ClientSize = new Size(scaleWidth, scaleHeight);
            resizedPicture.Image = bmp;

            graphForm.Size = new Size(scaleWidth, scaleHeight);
            //graphForm.Size = new Size(scaleWidth, scaleHeight);
            graphForm.Controls.Add(resizedPicture);
            graphForm.Show();
            bmp.Save(@"E:\Box Sync\LABWORK\ADD_lab\testest.pdf");
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
        public void TextBox(string inputStr, Color color, Font font)
        {
            Pen dataPen = new Pen(Brushes.Black);
            SolidBrush dataBrush = new SolidBrush(Color.Black);
            graphics.DrawString(inputStr, new Font("Arial",12), dataBrush, X / 2, Y / 6);
        }
             
        
    }
}
