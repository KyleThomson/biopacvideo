using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace BioPacVideo
{
    public partial class FeederTester_Mouse : Form
    {
        #region Properties
        MPTemplate MP;
        FeederTemplate Feeder;
        Graphics g;
        Pen p;
        int FeederPos = -1;
        int CagePos = -1;
        Bitmap RoomImage;
        List<int> FeederLabel;
        #endregion

        #region Lifecycle
        public FeederTester_Mouse()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
            Feeder = FeederTemplate.Instance;
            p = new Pen(Color.Red, 2);
            RoomImage = new Bitmap(Path.GetDirectoryName(Application.ExecutablePath) + "\\FeederTester_Mouse_Large.png");
            g = this.CreateGraphics();
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            FeederLabel = new List<int>() { 1, 2, 3, -1, 4, 5, 6, -1, 7, 8, 9, -1, 10, 11, 12 };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // If there is an image and it has a location,  
            // paint it when the Form is repainted. 
            base.OnPaint(e);
            e.Graphics.DrawImage(RoomImage, 15, 40);
        }
        #endregion

        #region Input Handlers
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            g.DrawImage(RoomImage, 15, 40);
            if (e.Y < 198)
            {
                
                if ((e.X > 23) && (e.X < 95))
                {
                    g.DrawRectangle(p, 23, 45, 70, 135);
                    FeederPos = 1;
                }
                else if ((e.X > 173) && (e.X < 245))
                {
                    g.DrawRectangle(p, 173, 45, 70, 135);
                    FeederPos = 2;
                }
                else if ((e.X > 325) && (e.X < 395))
                {
                    g.DrawRectangle(p, 325, 45, 70, 135);
                    FeederPos = 3;
                }
                else if ((e.X > 485) && (e.X < 555))
                {
                    g.DrawRectangle(p, 482, 45, 70, 135);
                    FeederPos = 4;
                }
                else if ((e.X > 640) && (e.X < 705))
                {
                    g.DrawRectangle(p, 633, 45, 70, 135);
                    FeederPos = 5;
                }
                else if ((e.X > 785) && (e.X < 860))
                {
                    g.DrawRectangle(p, 788, 45, 70, 135);
                    FeederPos = 6;
                }
                else
                {
                    FeederPos = -1;
                }
            }
            else if (e.Y > 198 && e.Y < 350)
            {

                if ((e.X > 23) && (e.X < 95))
                {
                    g.DrawRectangle(p, 23, 205, 70, 135);
                    FeederPos = 7;
                }
                else if ((e.X > 173) && (e.X < 245))
                {
                    g.DrawRectangle(p, 173, 205, 70, 135);
                    FeederPos = 8;
                }
                else if ((e.X > 325) && (e.X < 395))
                {
                    g.DrawRectangle(p, 325, 205, 70, 135);
                    FeederPos = 9;
                }
                else if ((e.X > 485) && (e.X < 555))
                {
                    g.DrawRectangle(p, 482, 205, 70, 135);
                    FeederPos = 10;
                }
                else if ((e.X > 640) && (e.X < 705))
                {
                    g.DrawRectangle(p, 633, 205, 70, 135);
                    FeederPos = 11;
                }
                else if ((e.X > 785) && (e.X < 860))
                {
                    g.DrawRectangle(p, 788, 205, 70, 135);
                    FeederPos = 12;
                }
                else 
                {
                    FeederPos = -1;
                }
            }
            else
            {
                FeederPos = -1;
            }

            if ((FeederPos != -1))
            {

                feederLab.Text = "Feeder " + FeederPos.ToString() + " Selected";
                return;
            }
            else
            {
                feederLab.Text = "Please Select Feeder";
                return; 
            }

        }

        private void IDC_RUNTEST_Click(object sender, EventArgs e)
        {

            if ((FeederPos == -1))
            {
                MessageBox.Show("No Feeder Selected");
                return;
            }
            int P, F;
            DateTime Start;
            Start = DateTime.Now;
            if (int.TryParse(PelletsNum.Text, out P))
            {
                F = FeederLabel.IndexOf(FeederPos);
                Feeder.AddCommand((byte)F, (byte)P);
                Feeder.ExecuteAction();
            }
            else
            {
                MessageBox.Show("Please Input Number of Pellets");
                return;
            }

        }

        private void executeAck_Click(object sender, EventArgs e)
        {
            Feeder.ExecuteAction();
        }
        #endregion
    }
}