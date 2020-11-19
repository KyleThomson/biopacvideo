using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace SeizureHeatmap
{
    public partial class mainUI : Form
    {
        MainData data;
        public mainUI()
        {
            InitializeComponent();
        }

        public void fileLoad_Click(object sender, EventArgs e)
        {
            string main_path;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.ShowDialog();
            main_path = openFileDialog1.FileName;
            data = new MainData(main_path);
            data.Open();
            data.GrpSeizures();
            data.ExtractData();
            for (int i = 0; i < data.Animals.Count; i++)
            {
                // data.Animals[i].animalID
                listBox1.Items.Insert(i, data.Animals[i].animalID);
            }

        }

        private void animalChecks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set chart properties

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Title = "Time (hours)";
            chart1.ChartAreas[0].AxisY.Title = "Seizure Stages";
            chart1.ChartAreas[0].Name = "Animal Seiure Stages";
            Legend animalLegend = new Legend();
            chart1.Legends.Add(animalLegend);
            // Generate chart series for each animal
            for (int i = 0; i < data.Animals.Count; i++)
            {
                Series series = new Series(data.Animals[i].animalID);
                chart1.Series.Add(series);
                if (data.Animals[i].seizureTimes != null)
                {
                    for (int x = 0; x < data.Animals[i].seizureTimes.Count; x++)
                    {
                        chart1.Series[i].Points.AddXY(Math.Floor(data.Animals[i].seizureTimes[x] / 24), data.Animals[i].seizureStages[x]);
                        chart1.Series[i].LegendText = data.Animals[i].animalID;
                        chart1.Series[i].ChartType = SeriesChartType.Point;
                        chart1.Series[i].MarkerStyle = (MarkerStyle)3;
                        chart1.Series[i].MarkerSize = (int)6;
                    }
                }
                else //null case
                {
                    chart1.Series[i].Points.AddXY("", "");
                    chart1.Series[i].ChartType = SeriesChartType.Point;
                }

            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // get selected animal
            string selectedAnimal = listBox1.SelectedItem.ToString();
            int animalIdx = listBox1.SelectedIndex;
            if (selectedAnimal != null)
            {
                // hide animal seizure data
                chart1.Series[animalIdx].Enabled = false;
            }

        }

        private void showAnimal_Click(object sender, EventArgs e)
        {
            // get selected animal
            string selectedAnimal = listBox1.SelectedItem.ToString();
            int animalIdx = listBox1.SelectedIndex;
            if (chart1.Series[animalIdx].Enabled == false)
            {
                chart1.Series[animalIdx].Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //newHM = new CreateHeatmap
            data.XlsWriteSrs();
        }

        private void DrawPoint_Click(object sender, EventArgs e)
        {
            int X = 1024;
            int Y = 1024;
            List<string> xTickString = new List<string>();
            List<string> yTickString = new List<string>();
            //Obtain basis for y and x axis labelling
            for (int i = 0; i < data.Animals[0].allDaySrs.Length; i++)
            {
                xTickString.Add((i+1).ToString());
            }

            for (int i = 0; i < data.Animals.Count; i++)
            {
                yTickString.Add(data.Animals[i].animalID);
            }
            //instantiate new instance of a graph
            GraphProperties srsGraph = new GraphProperties();
            srsGraph.InitGraph(X, Y);
            srsGraph.DrawAxes(4, X, Y);
            srsGraph.DrawTicks(data.Animals[0].allDaySrs.Length, data.Animals.Count, X, Y, 1.5F, xTickString, yTickString);
            Font aFont = new Font("Arial", 12);
            srsGraph.WriteXLabel("Time (days)", aFont, X, Y);
            srsGraph.WriteYLabel("Animals", aFont, X, Y);
            int markerSize = 4;
           
            for (int i = 0; i < data.Animals.Count; i++)
            {
                float yCoord = srsGraph.yTickPoints[i];
                int[] tempSRS = (int[])data.Animals[i].allDaySrs;
                for (int j = 0; j < data.Animals[i].allDaySrs.Length; j++)
                {
                    int oddCount = 0;
                    int evenCount = 0;
                    for (int k = 0; k < tempSRS[j]; k++)
                    {
                        if (k%2 == 0)
                        {
                            evenCount++;
                            float xCoord = srsGraph.xTickPoints[j] + (evenCount * markerSize / 3);
                            srsGraph.PlotPoints(xCoord, yCoord, markerSize);
                        }
                        else
                        {
                            oddCount++;
                            float xCoord = srsGraph.xTickPoints[j] - (evenCount * markerSize / 3);
                            srsGraph.PlotPoints(xCoord, yCoord, markerSize);
                        }
                        
                    }
                }
            }
            srsGraph.DisplayGraph();

        }

        private void mainUI_Load(object sender, EventArgs e)
        {

        }
    }
        

    }




