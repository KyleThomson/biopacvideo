using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeizureHeatmap
{
    class SeizureGraphing
    {
        //public MainData data;
        public GraphProperties graph;
        public SeizureGraphing(int X, int Y, MainData data)
        {
            // create instance of a graph and initialize
            graph = new GraphProperties();
            graph.InitGraph(X, Y);
            graph.DrawAxes(4, X, Y);
            List<string> xTickString = GetXTickLabels(data);
            List<string> yTickString = GetYTickLabels(data);
            GetYTickLabels(data);
            graph.DrawTicks(data.Animals[0].allDaySrs.Length, data.Animals.Count, X, Y, 1.5F, xTickString, yTickString);
            Font aFont = new Font("Arial", 12);
            graph.WriteXLabel("Time (days)", aFont, X, Y);
            graph.WriteYLabel("Animals", aFont, X, Y);
        }
        public List<string> GetXTickLabels(MainData data)
        {
            List<string> xTickString = new List<string>();
            //Obtain basis for y and x axis labelling
            for (int i = 0; i < data.Animals[0].allDaySrs.Length; i++)
            {
                xTickString.Add((i + 1).ToString());
            }
            return xTickString;
        }
        public List<string> GetYTickLabels(MainData data)
        {
            List<string> yTickString = new List<string>();
            //Obtain basis for y and x axis labelling
            for (int i = 0; i < data.Animals.Count; i++)
            {
                yTickString.Add(data.Animals[i].animalID);
            }
            return yTickString;
        }
        public void PlotSeizures(MainData data)
        {
            int markerSize = 4;
            //plot a point for every seizure (this might be too cumbersome to look at)
            for (int i = 0; i < data.Animals.Count; i++)
            {
                float yCoord = graph.yTickPoints[i];
                int[] tempSRS = (int[])data.Animals[i].allDaySrs;
                for (int j = 0; j < data.Animals[i].allDaySrs.Length; j++)
                {
                    int oddCount = 0;
                    int evenCount = 0;
                    for (int k = 0; k < tempSRS[j]; k++)
                    {
                        if (k % 2 == 0)
                        {
                            evenCount++;
                            float xCoord = graph.xTickPoints[j] + (evenCount * markerSize / 3);
                            graph.PlotPoints(xCoord, yCoord, markerSize);
                        }
                        else
                        {
                            oddCount++;
                            float xCoord = graph.xTickPoints[j] - (oddCount * markerSize / 3);
                            graph.PlotPoints(xCoord, yCoord, markerSize);
                        }

                    }
                }
            }
            graph.DisplayGraph();
        }
    }
}
