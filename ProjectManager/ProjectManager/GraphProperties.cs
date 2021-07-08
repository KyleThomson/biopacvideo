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
        public float xScale;
        public float yScale;
        public float objectScale;
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }

        public GraphProperties(int width, int height, float maxX, float maxY)
        {
            // Set input arguments as max data
            maxXData = maxX; maxYData = maxY;

            // First find resolution that graphics will be scaled to
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            //X = width; 
            //Y = height;

            X = screenWidth;
            Y = screenHeight;

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
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // High quality interpolation makes rescaling of image maintain resolution.
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        }

        private void ObjectScaling()
        {
            // calculate target scaling factor to maintain graph aspect ratio on any screen
            scale = Math.Min(screenWidth / (float)X, screenHeight / (float)Y);
            scale = 1;
            objectScale = 1 / scale;
            //objectScale = 4;
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
            // Calculate a scale factor that is in units of Pixels/unit

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
        public PictureBox ScaleGraph()
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
            newGfx.DrawImage(mainPlot, 0, 0, scaleWidth, scaleHeight);

            PictureBox resizedPicture = new PictureBox();
            resizedPicture.ClientSize = new Size(scaleWidth, scaleHeight);
            resizedPicture.Image = bmp;

            // Set size of form to fit monitor
            graphForm.Size = new Size(scaleWidth, scaleHeight);

            return resizedPicture;
        }
        public void DisplayGraph()
        {
            // re-size form and picture and show
            PictureBox resizedPicture = ScaleGraph();
            //graphForm.Controls.Add(resizedPicture);
            Bitmap resized = Resize();
            //graphForm.Controls.Add(resized);
            //graphForm.Show();
            graphics.DrawImage(resized, 0, 0);
            // Open new save file dialog and define extension etc
            SaveFileDialog graphSaveDialog = new SaveFileDialog();
            graphSaveDialog.DefaultExt = ".png";
            graphSaveDialog.Filter = "PNG files (*.png) |*.png";
            graphSaveDialog.Title = "Save Graph (.png) file";
            graphSaveDialog.InitialDirectory = "D:\\";

            if (graphSaveDialog.ShowDialog() == DialogResult.OK)
            {
                resized.Save(graphSaveDialog.FileName);
            }
        }

        private void AddBmpToForm(Bitmap bitmap)
        {
           
        }
        public void ClearGraph()
        {
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
            {
                // search controls for picture
                foreach (Control control in graphForm.Controls)
                {
                    // when picture is found, save if not null
                    var currentPicture = control as PictureBox;

                    if (currentPicture == null) continue;

                    currentPicture.Image.Save(graphSaveDialog.FileName);
                }
            }
        }

        private Bitmap Resize()
        {
            // get a scaled dimensions
            int scaleWidth = (int) (X * scale);
            int scaleHeight = (int) (Y * scale);

            // get resolution for 8.5" x 11"
            float xResolution = (float) (graphics.DpiX / 8.5);
            float yResolution = graphics.DpiY / 11;

            var resizedImage = new Rectangle((screenWidth - scaleWidth) / 2, (screenHeight - scaleHeight) / 2, scaleWidth, scaleHeight);
            var resizedBitmap = new Bitmap(scaleWidth, scaleHeight);
            resizedBitmap.SetResolution(xResolution, yResolution);

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
