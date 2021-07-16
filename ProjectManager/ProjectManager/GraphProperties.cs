using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace ProjectManager
{
    public class GraphProperties
    {
        public Bitmap mainPlot;
        public Bitmap resizedPlot;
        public Graphics graphics;
        public Axes axes;
        public PictureBox picture = new PictureBox();
        public Form graphForm = new Form();

        public float maxXData;
        public float maxYData;
        
        public List<PointF> axesPoints;
        public int X, Y;
        public float scale;
        public float xScale;
        public float yScale;
        public float objectScale;

        public GraphProperties(float maxX, float maxY)
        {
            // Set input arguments as max data
            maxXData = maxX; maxYData = maxY;

            // initial bitmap dimensions (intentionally big)
            // want the 8.5"x11" dimensions, DPI * inches = pixels, this should work at any screen resolution
            // windows defaults to 96 dpi so that's what we're gonna use
            X = (int)(96 * 8.5);
            Y = (int)(96 * 11.0);

            // bitmap initialization
            mainPlot = new Bitmap(Math.Max(X, 1), Math.Max(1, Y));
            graphics = Graphics.FromImage(mainPlot);
            graphics.Clear(Color.White);

            // create context object to pass to axes
            Context context = new Context(graphics);

            // Create axes
            axes = new Axes(X, Y, context);
            axesPoints = axes.axesList;
            AxisScale();

            // improve resolution
            ImproveResolution();
            
            // Calculate scale factor for resizing objects on bitmap
            ObjectScaling();
            
        }

        private void ImproveResolution()
        {
            // Set smoothing mode for graphics in initialization. This will smooth out edges when drawing round objects.
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // High quality interpolation makes rescaling of image maintain resolution.
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private void ObjectScaling()
        {
            // get resolution for 8.5" x 11"
            var xResolution = graphics.DpiX * 8.5;
            var yResolution = graphics.DpiY * 11.0;

            // calculate target scaling factor to maintain graph aspect ratio on any screen
            scale = (float)(mainPlot.Width / xResolution < mainPlot.Height / yResolution
                ? mainPlot.Width / xResolution : mainPlot.Height / yResolution);

            // set properties
            objectScale = scale;
            axes.objectScale = objectScale;
            axes.scale = scale;
        }
        private void AxisScale()
        {
            // scaling factor for plotting data on the 2D axes
            xScale = (float)((axes.axesList[1].X - axes.axesList[0].X) * 0.95/ maxXData);
            yScale = (axes.yAxisLength - axes.yAxisStart) / maxYData;
            axes.xScale = xScale;
            axes.yScale = yScale;
        }
        public void DrawAxes(float penWidth)
        {
            // call axes object
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
            axes.XLabel(xLabel, font);
        }
        public void WriteYLabel(string yLabel, Font font)
        {
            // Update Axes properties
            axes.YLabel(yLabel, font);
        }
        public void PlotPoints(float xCoord, float yCoord, int markerSize, string markerType, Color color)
        {
            Pen dataPen = new Pen(Brushes.Black);
            dataPen.Color = color;
            dataPen.Width = dataPen.Width * objectScale;
            SolidBrush dataBrush = new SolidBrush(Color.Black);
            dataBrush.Color = color;
            var centerPoint = markerSize / 2 * objectScale;

            // Convert input coordinate points
            float realXCoord = xCoord * xScale + axes.xTickPoints[0] - centerPoint;
            float realYCoord = (float)(axes.yAxisLength - yCoord * yScale) + centerPoint;

            // Marker type selection
            if (markerType == "o")
            {
                graphics.DrawEllipse(dataPen, realXCoord, realYCoord, markerSize * objectScale, markerSize * objectScale);
            }
            else if (markerType == ".")
            {
                graphics.FillEllipse(dataBrush, realXCoord, realYCoord - (markerSize * objectScale) / 2, markerSize * objectScale, markerSize * objectScale);
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

            // Convert input coordinate points
            float realX1Coord = x1Coord * xScale + axes.xTickPoints[0];
            float realY1Coord = axes.yAxisLength - y1Coord * yScale;
            float realX2Coord = x2Coord * xScale + axes.xTickPoints[0];
            float realY2Coord = axes.yAxisLength - y2Coord * yScale;

            PointF startPoint = new PointF(realX1Coord, realY1Coord);
            PointF endPoint = new PointF(realX2Coord, realY2Coord);
            graphics.DrawLine(dataPen, startPoint, endPoint);
        }
        public void DisplayGraph()
        {
            // Resize bitmap
            resizedPlot = Resize();

            // resize form
            graphForm.Size = new Size((int) (resizedPlot.Width * 1.00), (int) (resizedPlot.Width * 1.25));

            // Draw new image
            graphics.DrawImage(resizedPlot, 0, 0);

            // Add image to bitmap
            picture.Image = resizedPlot;
            picture.Size = new Size((int)(resizedPlot.Width * 1.00), (int)(resizedPlot.Width * 1.25));
            picture.Location = new Point(0, 50);
            graphForm.Controls.Add(picture);
            graphForm.Location = new Point(Screen.PrimaryScreen.Bounds.X, 0);
            graphForm.TopMost = true;
            graphForm.Show();

            // Save figure
            SaveFig();
        }
        public void ClearGraph()
        {
            // search form for controls that are pictures and remove them
            foreach (Control control in graphForm.Controls)
            {
                var currentPicture = control as PictureBox;

                if (currentPicture == null) continue;

                graphForm.Controls.Remove(picture);

                // Dispose of control to conserve memory
                control.Dispose();

                // Force Garbage Collection to work - seems to only cleanup unnecessary graph controls and not the project data
                // Using GC object, it seems, is not advisable but it doesn't interfere with other processes in this specific case.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public void SaveFig()
        {
            // Open new save file dialog and define extension etc
            SaveFileDialog graphSaveDialog = new SaveFileDialog();
            graphSaveDialog.DefaultExt = ".png";
            graphSaveDialog.Filter = "PNG files (*.png) |*.png";
            graphSaveDialog.Title = "Save Graph (.png) file";
            graphSaveDialog.InitialDirectory = "D:\\";

            if (graphSaveDialog.ShowDialog() == DialogResult.OK)
                resizedPlot.Save(graphSaveDialog.FileName);
        }

        private Bitmap Resize()
        {
            // get scaled dimensions
            int scaleWidth = (int)(X / scale);
            int scaleHeight = (int)(Y / scale);

            // create a resized image with new dimensions
            var resizedImage = new Rectangle((X - scaleWidth) / 2, (Y - scaleHeight) / 2, scaleWidth, scaleHeight);

            // make a new bitmap  - this will get drawn to with new dimensions
            var resizedBitmap = new Bitmap(mainPlot.Width, mainPlot.Height);

            // increase resolution? unsure if this is really necessary
            resizedBitmap.SetResolution(mainPlot.HorizontalResolution, mainPlot.VerticalResolution);

            // use temp graphics object to make image drawn higher quality
            using (var gfx = Graphics.FromImage(resizedBitmap))
            {
                gfx.CompositingMode = CompositingMode.SourceCopy;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    gfx.DrawImage(mainPlot, resizedImage, 0, 0, mainPlot.Width, mainPlot.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return resizedBitmap;
        }


    }
}
