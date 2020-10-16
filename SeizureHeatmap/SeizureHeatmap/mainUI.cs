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
            List<string> xTickString = new List<string>();
            List<string> yTickString = new List<string>();
            for (int i = 0; i < data.Animals[0].allDaySrs.Length; i++)
            {
                xTickString.Add((i+1).ToString());
            }

            for (int i = 0; i < data.Animals.Count; i++)
            {
                yTickString.Add(data.Animals[i].animalID);
            }
            Drawing newGraph = new Drawing(1024, 1024);
            int X = 1024;
            int Y = 1024;
            newGraph.DrawAxes(4, X, Y);
            newGraph.DisplayGraph();
            newGraph.DrawTicks(data.Animals[0].allDaySrs.Length, data.Animals.Count, X, Y, 1.5F, xTickString, yTickString);
            Font aFont = new Font("Arial",12);
            newGraph.WriteXLabel("Time (days)", aFont);
            newGraph.WriteYLabel("Animals", aFont);
            newGraph.DrawVector(data.Animals[0].allDaySrs.Length,data.Animals.Count);
            //Pen B = new Pen(Color.Black, width: (float)2.5);
            //PictureBox P = new PictureBox();
            //P.ClientSize = new Size(512, 512);
            //var bitmap = new Bitmap(512, 512);

            //try
            //{
            //// Calculate positioning of axes
            //int X = bitmap.Width;
            //int Y = bitmap.Height;
            //int yTickCount = data.Animals.Count;
            //int xTickCount = data.Animals[0].allDaySrs.Length;
            //float yTickSpacing = ((float)(Y / (yTickCount*1.25)));
            //float xTickSpacing = X / xTickCount;
            //// Axis labeling
            //string xLabel = "Time (days)";
            //string yLabel = "Animal IDs";
            //// Axis label bounding boxes
            //float width = 200.0F;
            //float height = 50.0F;
            //float yWidth = 100.0F;
            //float yHeight = 350.0F;
            //RectangleF xLabelRect = new RectangleF((X - X / 6) / 2, (float)(Y / 1.125), width, height);
            //RectangleF yLabelRect = new RectangleF(X/256, Y/4, yWidth, yHeight);
            //// Axis label formatting
            //Font drawFont = new Font("Arial", 12);
            //Font animalFont = new Font("Arial", 8);
            //SolidBrush drawBrush = new SolidBrush(Color.Black);
            //StringFormat drawFormat = new StringFormat();
            //drawFormat.Alignment = StringAlignment.Center;
            //StringFormat yLabelFormat = new StringFormat();
            //yLabelFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            //using (var graphics = Graphics.FromImage(bitmap))
            //{
            //    // Y-axis
            //    graphics.DrawLine(B, X / 6, 0, X / 6, (int)(Y / 1.2));
            //    graphics.DrawString(yLabel, drawFont, drawBrush, yLabelRect, yLabelFormat);
            //    for(int i = 0; i < data.Animals.Count; i++)
            //    {
            //        graphics.DrawLine(B, (float)(X / 6.5), yTickSpacing * (i+1), (float)(X / 5.5), yTickSpacing * (i+1));
            //        RectangleF yTickLabelRect = new RectangleF((float)(X / (X*16)), (float)(yTickSpacing * (i + 1/1.2)), width / 2, height / 2);
            //        graphics.DrawString(data.Animals[i].animalID, animalFont, drawBrush, yTickLabelRect, drawFormat);
            //        //Draw data points //ith animals seizure frequencies per day
            //        List<int> tempSrsList = new List<int>((IEnumerable<int>)data.Animals[i].allDaySrs);
            //        for (int k = 0; k < data.Animals[i].allDaySrs.Length; k++)
            //        {
            //            for (int j = 0; j < tempSrsList[k]; j++)
            //            {
            //                // generate point for each seizure
            //                PointF point1 = new PointF(X / 6 + xTickSpacing * (k + 1) + (j + 2), yTickSpacing * (i + 1) + (i / 2));
            //                PointF point2 = new PointF(X / 6 + xTickSpacing * (k + 1) + (k / 1) + (j + 2), yTickSpacing * (i + 1));
            //                PointF point3 = new PointF(X / 6 + xTickSpacing * (k + 1) + (j + 2), yTickSpacing * (i + 1) - (i / 2));
            //                PointF point4 = new PointF(X / 6 + xTickSpacing * (k + 1) - (k / 1) + (j + 2), yTickSpacing * (i + 1));
            //                PointF[] diamondMarker =
            //                {
            //                    point1,
            //                    point2,
            //                    point3,
            //                    point4
            //                };

            //                graphics.DrawPolygon(B, diamondMarker);
            //            }
            //        }
            //    }
            //    // X-axis
            //    graphics.DrawLine(B, X / 6, (int)(Y / 1.2), X, (int)(Y / 1.2));
            //    graphics.DrawString(xLabel, drawFont, drawBrush, xLabelRect, drawFormat);
            //    for(int i = 0; i < xTickCount; i++)
            //    {
            //        graphics.DrawLine(B, X / 6 + xTickSpacing * (i + 1), (float)(Y / 1.175), X / 6 + xTickSpacing * (i + 1), (float)(Y / 1.225));
            //        RectangleF xTickLabelRect = new RectangleF((float)(X/8 + xTickSpacing * (i)), (float)(Y / 1.150), width / 2, height / 2);
            //        graphics.DrawString((i+1).ToString(), animalFont, drawBrush, xTickLabelRect, drawFormat);
            //    }
            //}
            //var myForm = new Form1();
            //myForm.Size = new Size(512, 512);
            //P.Image = bitmap;
            //myForm.Controls.Add(P);
            //myForm.Show();
            //}
            //catch (NullReferenceException ex)
            //{
            //    MessageBox.Show(ex.ToString());
        }



}
                       
        //var myForm = new Form1();
        

    }
            //Drawing Circles = new Drawing();
            //Circles.InitDrawing(512, 512);
            //Circles.CreateDrawing(24, 24, 50, 50, "hello!");
            //Graphics newDrawing = Circles.gfx;




