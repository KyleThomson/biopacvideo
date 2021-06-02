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

            // create context object to pass to axes
            Context context = new Context(graphics);

            // Create axes
            axes = new Axes(X, Y, context);
            axesPoints = axes.axesList;

            // Set smoothing mode for graphics in initialization. This will smooth out edges when drawing round objects.
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // High quality interpolation makes rescaling of image maintain resolution.
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // calculate target scaling factor to maintain graph aspect ratio on any screen
            scale = Math.Min(screenWidth / (float)width, screenHeight / (float)height);
            objectScale = 1 / scale;
            axes.objectScale = objectScale;
            axes.scale = scale;

            // Set input arguments as max data
            maxXData = maxX; maxYData = maxY;

        }
        public void DrawAxes(float penWidth)
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = penWidth * objectScale;
            axes.axesWidth = penWidth;
            axes.DrawXAxis();
            axes.DrawYAxis();
        }
        public void BoundingBox()
        {
            axes.DrawBoundingBox();
        }
        public void DrawTicks(int xTicks, int yTicks, float tickWidth)
        {
            Pen tickPen = new Pen(Brushes.Black);
            tickPen.Width = tickWidth * objectScale;
            axes.tickWidth = tickWidth;
            axes.xTicks = xTicks;
            axes.yTicks = yTicks;

            axes.AxisTicks();
        }
        public void WriteXLabel(string xLabel, Font font)
        {
            // Update Axes properties
            axes.XLabel(xLabel);

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
            // Update Axes properties
            axes.YLabel(yLabel);

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
            float xScale = (axes.xTickPoints[axes.xTickPoints.Count-1] - axes.xTickPoints[0]) / maxXData;
            float yScale = (axes.yAxisLength - axes.yAxisStart) / maxYData;

            // Convert input coordinate points
            float realXCoord = xCoord * xScale + axes.xTickPoints[0] - (markerSize * objectScale);
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
            float xScale = (axes.xTickPoints[axes.xTickPoints.Count - 1] - axes.xTickPoints[0]) / maxXData;
            float yScale = (axes.yAxisLength - axes.yAxisStart) / maxYData;

            // Convert input coordinate points
            float realX1Coord = x1Coord * xScale + axes.xTickPoints[0];
            float realY1Coord = axes.yAxisLength - y1Coord * yScale;
            float realX2Coord = x2Coord * xScale + axes.xTickPoints[0];
            float realY2Coord = axes.yAxisLength - y2Coord * yScale;

            PointF startPoint = new PointF(realX1Coord, realY1Coord);
            PointF endPoint = new PointF(realX2Coord, realY2Coord);
            graphics.DrawLine(dataPen, startPoint, endPoint);
        }
        public PictureBox ScaleGraph()
        {
            if (mainPlot == null)
            {
                // bitmap initialization
                mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
                graphics = Graphics.FromImage(mainPlot);
                graphics.Clear(Color.White);
            }
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
            newGfx.DrawImage(mainPlot, 0, 0, scaleWidth, scaleHeight);

            PictureBox resizedPicture = new PictureBox();
            resizedPicture.ClientSize = new Size(scaleWidth, scaleHeight);
            resizedPicture.Image = bmp;

            return resizedPicture;
        }
        public void DisplayGraph()
        {
            if (mainPlot == null)
            { 
                // bitmap initialization
                mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
                graphics = Graphics.FromImage(mainPlot);
                graphics.Clear(Color.White);
            }
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
            newGfx.DrawImage(mainPlot, 0, 0, scaleWidth, scaleHeight);

            PictureBox resizedPicture = new PictureBox();
            resizedPicture.ClientSize = new Size(scaleWidth, scaleHeight);
            resizedPicture.Image = bmp;

            graphForm.Size = new Size(scaleWidth, scaleHeight);
            graphForm.Controls.Add(resizedPicture);
            graphForm.Show();
        }
        public void ClearGraph()
        {
            foreach (Control control in graphForm.Controls)
            {
                PictureBox picture = control as PictureBox;
                if (picture != null)
                { graphForm.Controls.Remove(picture); }
            }
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
