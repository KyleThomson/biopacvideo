using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class Calendar : Form
    {
        DateTime D;
        Bitmap B;        
        Graphics g;
        int DW;
        int DaysShown;
        DateTime N;
        Project pjt; 
        public Calendar(Project P)
        {
            InitializeComponent();
            D = DateTime.Now;
            B = new Bitmap(panel1.Width,panel1.Height);
            g = Graphics.FromImage(B);
            panel1.BackgroundImage = B;
            DW = panel1.Width / 7;
            DaysShown = panel1.Height / DW;
            N = DateTime.Now;
            N = N.AddDays(-1 * (int)N.DayOfWeek);
            pjt = P;
            string[] A = pjt.Get_Animals();
            for (int i = 0; i < A.Length; i++)
            {
                AnimalSel.Items.Add(A[i]);
            }
            AnimalSel.SelectedIndex = 0;             
            
        }
        private void DrawCalendar()
        {            
            Pen Day = new Pen(Color.Black);
            Day.Width = 3;
            Font F = new Font("Arial",14);
            Size S = new Size(DW, DW);
            Pen Filled = new Pen(Color.Blue);
            SolidBrush BlueFill = new SolidBrush(Color.LightBlue);
            SolidBrush BT = new SolidBrush(Color.Black);
            Font Fs = new Font("Arial", 11);
            SolidBrush Bs = new SolidBrush(Color.Red);
            Pen DayHilight = new Pen(Color.Blue);           
            DayHilight.Width = 3;
            Pen ImpDay = new Pen(Color.Red);
            ImpDay.Width = 3;
            int Percent; 
            String St;            
            g.Clear(Color.White);
            MMY.Text = N.ToString("MMMM") + "/" + N.AddMonths(1).ToString("MMMM") + " " + N.AddMonths(1).Year.ToString();
            for (int w = 0; w < DaysShown; w++)                
            {
                for (int d = 0; d < 7; d++)
                {
                    //Draw inital box
                    //g.FillRectangle(WhiteFill, new Rectangle(new Point(DW * d, DW * w), S));
                    g.DrawRectangle(Day, new Rectangle(new Point(DW * d, DW * w), S));
                    //Draw Data box    
                }
            }
            for (int w = 0; w < DaysShown; w++)
            {
                for (int d = 0; d < 7; d++)
                {
                    Percent = pjt.GetAnimalRecordingInfo(AnimalSel.Items[AnimalSel.SelectedIndex].ToString(), N.AddDays(d + w * 7));                    
                    if (Percent > 0)
                    {
                        g.FillRectangle(BlueFill,new Rectangle(new Point(DW * d, DW * w), new Size(DW, (int)((float)DW * ((float)Percent / 100F)))));
                        g.DrawRectangle(DayHilight, new Rectangle(new Point(DW * d, DW * w), new Size(DW, (int)((float)DW * ((float)Percent / 100F)))));
                    }
                    //Write text
                    St = N.AddDays(d+w*7).Day.ToString(); 
                    g.DrawString(St, F, BT, new PointF(DW * d + DW - (St.Length*12) - 6, DW * w + 4));
                }
            }
            for (int w = 0; w < DaysShown; w++)
            {
                for (int d = 0; d < 7; d++)
                {
                    ImportantDateType I = pjt.CheckImportantDate(AnimalSel.Items[AnimalSel.SelectedIndex].ToString(), N.AddDays(d + w * 7));
                    if (I != null)
                    {
                        string s = "";
                        foreach (LabelType Lb in pjt.Labels)
                        {
                            s = Lb.LabelMatch(I.LabelID, s); 
                        }
                        g.DrawRectangle(ImpDay, new Rectangle(new Point(DW * d, DW * w), S));
                        g.DrawString(s, Fs, Bs, new PointF(DW * d + 2, DW * w + DW - 17));
                    }
                }
            }            
            panel1.Refresh();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {

        }

        private void Up_Click(object sender, EventArgs e)
        {
            N = N.AddDays(-7);
            DrawCalendar();
        }

        private void down_Click(object sender, EventArgs e)
        {
            N= N.AddDays(7);
            DrawCalendar();
        }

        private void AnimalSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawCalendar();
        }
    
    }
}
