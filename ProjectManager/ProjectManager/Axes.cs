using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectManager
{
    // Axes is meant to be an object used in GraphProperties that will store properties about specific axes that can be passed to new graphs. 
    public class Axes
    {
        // whole axes properties
        public float xAxisLength;
        public float yAxisLength;
        public float xAxisStart;
        public float yAxisStart;
        public float maxXData;
        public float maxYData;
        public List<PointF> axesList;
        public string xLabel; public string yLabel;

        // properties related to tick marks
        public List<float> xTickPoints;
        public List<float> yTickPoints;
        public int xTicks; public int yTicks;
        public float tickWidth;
        public List<string> xTickLabels;
        public List<string> yTickLabels;
        public Axes(int X, int Y)
        {
            // Initialize tick points
            xTickPoints = new List<float>();
            yTickPoints = new List<float>();

            xAxisLength = (float)(X * 0.75);
            yAxisLength = (float)(Y * 0.75);
            xAxisStart = (float)(X * 0.25);
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
        public void YLabel(string label)
        {
            yLabel = label;
        }
        public void XLabel(string label)
        {
            xLabel = label;
        }
        public void AxisTicks(GraphProperties graph)
        {

        }
    }
}
