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
    public partial class FeederTester : Form
    {

        MPTemplate MP;
        FeederTemplate Feeder;      
        Graphics g;
        Pen p; 
        int FeederPos = -1;
        int CagePos = -1;
        Bitmap RoomImage;
        public FeederTester()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
        
            Feeder = FeederTemplate.Instance;
            p = new Pen(Color.Red, 2);
            RoomImage = new Bitmap(Path.GetDirectoryName(Application.ExecutablePath) + "\\FeederTester.png");         
            g = this.CreateGraphics();            
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // If there is an image and it has a location,  
            // paint it when the Form is repainted. 
            base.OnPaint(e);
            e.Graphics.DrawImage(RoomImage,180,10);           
        }
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {

            if (e.Y < 150)
            {
                if ((e.X > 180) && (e.X < 233))
                {
                    FeederPos = 1;
                }
                if ((e.X > 233) && (e.X < 300))
                {
                    FeederPos = 0;
                }
                if ((e.X > 660) && (e.X < 733))
                {
                    FeederPos = 2;
                }
                if ((e.X > 733) && (e.X < 790))
                {
                    FeederPos = 3;
                }
            }
            else if (e.Y > 165) 
            {
                if (e.X < 480)
                {
                    if (e.Y < 237)
                        CagePos = 0;
                    else if (e.Y > 320)
                        CagePos = 4;
                    else
                        CagePos = 2;
                }
                else
                {
                    if (e.Y < 237)
                        CagePos = 1;
                    else if (e.Y > 320)
                        CagePos = 5;
                    else
                        CagePos = 3;
                }
                
            }
            g.DrawImage(RoomImage,180,10);
            switch (FeederPos)
            {
                case -1:
                    break;
                case 0:
                    g.DrawRectangle(p, 242, 25, 60, 120);
                    break;
                case 1:                    
                    g.DrawRectangle(p, 185, 11, 60, 120);
                    break;
                case 2:
                    g.DrawRectangle(p, 669, 25, 60, 120);
                    break;
                case 3:
                    g.DrawRectangle(p, 723, 11, 60, 120);
                    break;
            }
            switch (CagePos)
            {
                case -1:
                    break;
                case 0:
                    g.DrawRectangle(p, 193, 163, 270, 70);
                    break;
                case 1: 
                    g.DrawRectangle(p, 506, 163, 270, 70);
                    break;
                case 2: 
                    g.DrawRectangle(p, 193, 246, 270, 70);
                    break;
                case 3:
                    g.DrawRectangle(p, 508, 246, 270, 70);
                    break;
                case 4:
                    g.DrawRectangle(p, 193, 338, 270, 70);
                    break;
                case 5:
                    g.DrawRectangle(p, 508, 338, 270, 70);
                    break;
            }
            if ((FeederPos != -1) && (CagePos != -1))
            {
                FeederNum.Text = "Feeder: " + ((FeederPos + CagePos * 4)+1).ToString();
                return;
            }

        }
        private void IDC_RUNTEST_Click(object sender, EventArgs e)
        {
            if ((FeederPos == -1) || (CagePos == -1))
            {
                MessageBox.Show("No Feeder Selected");
                return;
            }
            int F, P;
            DateTime Start;
            Start = DateTime.Now;
            F = FeederPos + CagePos * 4; 
            if  (int.TryParse(PelletsNum.Text, out P))
            {
                Feeder.AddCommand((byte)F, (byte)P);
                Feeder.ExecuteAck();
            }
            else
            {
                MessageBox.Show("Please Input Number of Pellets");
                return;
            }

        }

        private void TestAll_Click(object sender, EventArgs e)
        {
            Feeder.ExecuteTest();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Feeder.ExecuteAck();
        }
    }
}
