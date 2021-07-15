using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectManager
{
    // Axes is meant to be an object used in GraphProperties that will store properties about specific axes that can be passed to new graphs. 
    public class Axes
    {
        // Pass graph reference
        public Context graphContext;

        // whole axes properties
        public float xAxisLength;
        public float yAxisLength;
        public float xAxisStart;
        public float yAxisStart;
        public List<PointF> axesList;
        public string xLabel; public string yLabel;
        public float axesWidth;

        // properties related to tick marks
        public List<float> xTickPoints;
        public List<float> yTickPoints;
        public int xTicks; public int yTicks;
        public float tickWidth;
        public List<string> xTickLabels;
        public List<string> yTickLabels;
        public List<double> xTickLocations;

        // Drawing
        public float scale;
        public float objectScale;
        public float xScale;
        public float yScale;
        public Axes(int X, int Y, Context context)
        {
            // Construct wrapped graph object w/ context
            graphContext = context;

            // Initialize tick points
            xTickPoints = new List<float>();
            yTickPoints = new List<float>();

            xAxisLength = (float)(X * 0.85);
            yAxisLength = (float)(Y * 0.75);
            xAxisStart = (float)(X * 0.15);
            yAxisStart = (float)(Y * 0.25);

            PointF xAxisStartPoint = new PointF(xAxisStart, Y - yAxisStart);
            PointF xAxisEndPoint = new PointF(xAxisLength, Y - yAxisStart);
            PointF yAxisStartPoint = new PointF(xAxisStart, yAxisStart);
            PointF yAxisEndPoint = new PointF(xAxisStart, yAxisLength);

            axesList = new List<PointF>();
            axesList.Add(xAxisStartPoint);
            axesList.Add(xAxisEndPoint);
            axesList.Add(yAxisStartPoint);
            axesList.Add(yAxisEndPoint);
        }
        public void YLabel(string label, Font font)
        {
            // get current graphics state
            GraphicsState state = graphContext.graphics.Save();
            graphContext.graphics.ResetTransform();

            yLabel = label;
            // Format string
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat yLabelFormat = new StringFormat();
            yLabelFormat.Alignment = StringAlignment.Center;

            // Get size of the string and bounds
            SizeF yLabelSize = graphContext.graphics.MeasureString(yLabel, font);
            SizeF bounds = graphContext.graphics.VisibleClipBounds.Size;

            // Use size of string and length of axis to center the label
            float xPoint = axesList[0].X;
            float yPoint = (yAxisLength + yAxisStart) / 2 - yLabelSize.Width / 2;
            //RectangleF yLabelRect = new RectangleF((float)(xPoint * 0.70), yPoint, yLabelSize.Width, yLabelSize.Height);
            RectangleF yLabelRect = new RectangleF(bounds.ToPointF().X, bounds.ToPointF().Y, bounds.Width, bounds.Height);

            // Rotate graphics first
            graphContext.graphics.TranslateTransform(bounds.Width, 0);
            graphContext.graphics.RotateTransform(90);

            graphContext.graphics.DrawString(yLabel, font, drawBrush, yLabelRect, yLabelFormat);
            graphContext.graphics.ResetTransform();
        }
        public void XLabel(string label, Font font)
        {
            xLabel = label;

            // Format string
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            // Get size of the string
            SizeF xLabelSize = graphContext.graphics.MeasureString(xLabel, font);

            // Use size of string and length of axis to center the label
            float xPoint = (xAxisLength + xAxisStart) / 2 - xLabelSize.Width / 2;
            float yPoint = axesList[0].Y;
            RectangleF xLabelRect = new RectangleF((xPoint), (float)(yPoint * 1.05), xLabelSize.Width, xLabelSize.Height);

            graphContext.graphics.DrawString(xLabel, font, drawBrush, xLabelRect, drawFormat);
        }
        public void AxisTicks()
        {
            Pen tickPen = new Pen(Brushes.Black);
            tickPen.Width = tickWidth * objectScale;
            float xTickSpacing = (float)(xAxisLength / (xTicks * 2));
            float yTickSpacing = (float)(yAxisLength / (yTicks * 1.5));
            Font xFont = new Font("Arial", 12 * objectScale);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float maxXTick = (float)xTickLocations[xTicks - 1];

            for (int i = 0; i < xTicks; i++)
            {
                float currentXPoint = (float)(xTickLocations[i] * xScale + 0.50 * xScale);
                // create length of x tick
                PointF xTickStart = new PointF(xAxisStart + currentXPoint, yAxisLength);
                xTickPoints.Add(xAxisStart + currentXPoint);
                PointF xTickEnd = new PointF(xAxisStart + currentXPoint, (float)((yAxisLength) * 0.99));
                graphContext.graphics.DrawLine(tickPen, xTickStart, xTickEnd);

                // x tick label
                SizeF xTickSize = graphContext.graphics.MeasureString(xTickLabels[i], xFont);
                RectangleF xTickRect = new RectangleF(xAxisStart - (xTickSize.Width) / 2 + currentXPoint, (float)(yAxisLength * 1.025), xTickSize.Width, xTickSize.Height);
                graphContext.graphics.DrawString(xTickLabels[i], xFont, drawBrush, xTickRect);
            }

            for (int i = 0; i < yTicks; i++)
            {
                // create length of ticks
                PointF yTickStart = new PointF(xAxisStart, (float)(yAxisLength - yTickSpacing * (i + 0.5)));
                yTickPoints.Add((float)(yAxisLength - yTickSpacing * (i + 0.5)));
                PointF yTickEnd = new PointF((float)(xAxisStart * 1.02), (float)((yAxisLength - yTickSpacing * (i + 0.5))));
                graphContext.graphics.DrawLine(tickPen, yTickStart, yTickEnd);

                // y tick label
                SizeF yTickSize = graphContext.graphics.MeasureString(yTickLabels[i], xFont);
                RectangleF yTickRect = new RectangleF((float)(xAxisStart * 0.975) - yTickSize.Width, (float)(yAxisLength - yTickSpacing * (i + 0.5) - yTickSize.Height / 2), yTickSize.Width, yTickSize.Height);
                graphContext.graphics.DrawString(yTickLabels[i], xFont, drawBrush, yTickRect);
            }
        }
        public void DrawXAxis()
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = axesWidth * objectScale;
            graphContext.graphics.DrawLine(axisPen, axesList[0], axesList[1]);
        }
        public void DrawYAxis()
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = axesWidth * objectScale;
            graphContext.graphics.DrawLine(axisPen, axesList[2], axesList[3]);
        }
        public void DrawBoundingBox()
        {
            Pen axisPen = new Pen(Brushes.Black);
            axisPen.Width = axesWidth * objectScale;
            //Horizontal bounding line
            graphContext.graphics.DrawLine(axisPen, new PointF((float)(axesList[0].X * 0.995), (float)(axesList[2].Y - axesList[2].Y * 0.011)), new PointF((float)(axesList[1].X * 1.0025), (float)(axesList[2].Y - axesList[2].Y * 0.011)));
            //Vertical bounding line
            graphContext.graphics.DrawLine(axisPen, new PointF(axesList[1].X, axesList[2].Y), new PointF(axesList[1].X, axesList[3].Y));

        }
    }
}
