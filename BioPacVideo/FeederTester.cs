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
        #region Properties
        MPTemplate MP;
        FeederTemplate Feeder;      
        Graphics g;
        Pen p; 
        int FeederPos = -1;
        Bitmap RoomImage;
        #endregion

        #region Lifecyle
        public FeederTester()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
        
            Feeder = FeederTemplate.Instance;
            p = new Pen(Color.Red, 2);
            RoomImage = new Bitmap(Path.GetDirectoryName(Application.ExecutablePath) + "\\FeederTester_Rat.png");         
            g = this.CreateGraphics();            
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // If there is an image and it has a location,  
            // paint it when the Form is repainted. 
            base.OnPaint(e);
            e.Graphics.DrawImage(RoomImage,15,25);           
        }
        #endregion

        #region Input Handlers
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            g.DrawImage(RoomImage, 15, 25);
            // feeders on the left side of the shelves 
            if (e.X < 465)
            {
                if (e.X < 220)
                {
                    if ((e.Y > 55) && (e.Y < 145) && (e.X < 83))
                    {
                        g.DrawRectangle(p, 28, 50, 55, 95);
                        FeederPos = 2;
                    }
                    else if ((e.Y > 85) && (e.Y < 185) && (e.X < 120))
                    {
                        g.DrawRectangle(p, 70, 90, 50, 95);
                        FeederPos = 1;
                    }
                    else if ((e.Y > 235) && (e.Y < 330) && (e.X < 83))
                    {
                        g.DrawRectangle(p, 28, 235, 50, 95);
                        FeederPos = 10;
                    }
                    else if ((e.Y > 270) && (e.Y < 370) && (e.X < 112))
                    {
                        g.DrawRectangle(p, 65, 274, 50, 95);
                        FeederPos = 9;
                    }
                    else if ((e.Y > 400) && (e.Y < 501) && (e.X < 80))
                    {
                        g.DrawRectangle(p, 32, 407, 50, 95);
                        FeederPos = 18;
                    }
                    else if ((e.Y > 440) && (e.Y < 541) && (e.X < 112))
                    {
                        g.DrawRectangle(p, 70, 445, 50, 95);
                        FeederPos = 17;
                    }
                    else
                    {
                        FeederPos = -1;
                    }
                }
                else if (e.X > 220)
                {
                    if ((e.Y > 55) && (e.Y < 145) && (e.X < 275))
                    {
                        g.DrawRectangle(p, 227, 50, 50, 95);
                        FeederPos = 4;
                    }
                    else if ((e.Y > 85) && (e.Y < 185) && (e.X < 310))
                    {
                        g.DrawRectangle(p, 265, 90, 50, 95);
                        FeederPos = 3;
                    }
                    else if ((e.Y > 235) && (e.Y < 330) && (e.X < 278))
                    {
                        g.DrawRectangle(p, 225, 235, 50, 95);
                        FeederPos = 12;
                    }
                    else if ((e.Y > 270) && (e.Y < 370) && (e.X < 315))
                    {
                        g.DrawRectangle(p, 265, 272, 50, 95);
                        FeederPos = 11;
                    }
                    else if ((e.Y > 400) && (e.Y < 501) && (e.X < 270))
                    {
                        g.DrawRectangle(p, 224, 408, 50, 95);
                        FeederPos = 20;
                    }
                    else if ((e.Y > 440) && (e.Y < 541) && (e.X < 305))
                    {
                        g.DrawRectangle(p, 260, 450, 50, 95);
                        FeederPos = 19;
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
            }
            // these are the feeders on the right side of the wall 
            else if (e.X > 465)
            {
                if (e.X < 675)
                {
                    if ((e.Y > 50) && (e.Y < 145) && (e.X < 535))
                    {
                        g.DrawRectangle(p, 487, 50, 50, 95);
                        FeederPos = 6;
                    }
                    else if ((e.Y > 90) && (e.Y < 185) && (e.X < 575))
                    {
                        g.DrawRectangle(p, 525, 90, 50, 95);
                        FeederPos = 5;
                    }
                    else if ((e.Y > 235) && (e.Y < 331) && (e.X < 530))
                    {
                        g.DrawRectangle(p, 480, 235, 50, 95);
                        FeederPos = 14;
                    }
                    else if ((e.Y > 275) && (e.Y < 371) && (e.X < 565))
                    {
                        g.DrawRectangle(p, 519, 273, 50, 95);
                        FeederPos = 13;
                    }
                    else if ((e.Y > 405) && (e.Y < 500) && (e.X < 535))
                    {
                        g.DrawRectangle(p, 486, 405, 50, 95);
                        FeederPos = 22;
                    }
                    else if ((e.Y > 445) && (e.Y < 535) && (e.X < 572))
                    {
                        g.DrawRectangle(p, 525, 445, 50, 95);
                        FeederPos = 21;
                    }
                    else
                    {
                        FeederPos = -1;
                    }
                }
                else if (e.X > 675)
                {
                    if ((e.Y > 50) && (e.Y < 144) && (e.X < 730))
                    {
                        g.DrawRectangle(p, 680, 50, 50, 95);
                        FeederPos = 8;
                    }
                    else if ((e.Y > 90) && (e.Y < 181) && (e.X < 765))
                    {
                        g.DrawRectangle(p, 720, 90, 50, 95);
                        FeederPos = 7;
                    }
                    else if ((e.Y > 238) && (e.Y < 330) && (e.X < 730))
                    {
                        g.DrawRectangle(p, 680, 235, 50, 95);
                        FeederPos = 16;
                    }
                    else if ((e.Y > 277) && (e.Y < 365) && (e.X < 765))
                    {
                        g.DrawRectangle(p, 718, 275, 50, 95);
                        FeederPos = 15;
                    }
                    else if ((e.Y > 408) && (e.Y < 502) && (e.X < 725))
                    {
                        g.DrawRectangle(p, 677, 408, 50, 95);
                        FeederPos = 24;
                    }
                    else if ((e.Y > 450) && (e.Y < 540) && (e.X < 760))
                    {
                        g.DrawRectangle(p, 715, 450, 50, 95);
                        FeederPos = 23;
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
            }
            else
            {
                FeederPos = -1; 
            }
            
            if ((FeederPos != -1))
            {
                FeederNum.Text = "Feeder " + (FeederPos).ToString() + " Selected";
                return;
            }
            else
            {
                FeederNum.Text = ("No Feeder Selected");
            }

        }

        private void IDC_RUNTEST_Click(object sender, EventArgs e)
        {
            if ((FeederPos == -1))
            {
                MessageBox.Show("No Feeder Selected");
                return;
            }
            int F, P;
            DateTime Start;
            Start = DateTime.Now;
            F = FeederPos - 1; 
            if  (int.TryParse(PelletsNum.Text, out P))
            {
                MealType M = new MealType(F, P, F, true);
                Feeder.AddCommand(M);
            }
            else
            {
                MessageBox.Show("Please Input Number of Pellets");
                return;
            }
        }

        private void executeAck_Click(object sender, EventArgs e)
        {
            Feeder.ArduinoAckowledge();
        }
        #endregion
    }
}
