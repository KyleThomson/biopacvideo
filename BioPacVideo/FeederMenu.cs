using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections; 
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class FeederMenu : Form
    {
        private FeederTemplate Feeder;
        private TimeSpan midnight;
        private TimeSpan mid_ten;
        private TimeSpan noon;
        private TimeSpan noon_ten;

        public FeederMenu(FeederTemplate Pass_feeder)
        {
            InitializeComponent();
            Feeder = Pass_feeder;
            midnight = new TimeSpan(0, 0, 0);
            mid_ten = new TimeSpan(0, 10, 0);
            noon = new TimeSpan(12, 00, 0);
            noon_ten = new TimeSpan(12, 10, 0);
            if (Feeder.Meal1 != TimeSpan.MaxValue)
                IDC_Meal1.Text = Feeder.Meal1.ToString();
            if (Feeder.Meal2 != TimeSpan.MaxValue)
                IDC_Meal2.Text = Feeder.Meal2.ToString();
            if (Feeder.Meal3 != TimeSpan.MaxValue)
                IDC_Meal3.Text = Feeder.Meal3.ToString();
            if (Feeder.Meal4 != TimeSpan.MaxValue)
                IDC_Meal4.Text = Feeder.Meal4.ToString();
            if (Feeder.Meal5 != TimeSpan.MaxValue)
                IDC_Meal5.Text = Feeder.Meal5.ToString();
            if (Feeder.Meal6 != TimeSpan.MaxValue)
                IDC_Meal6.Text = Feeder.Meal6.ToString();
            IDX_FEEDERENABLE.Checked = Feeder.Enabled;
            IDC_PPG.Text = string.Format("{0:0.000}", Feeder.PelletsPerGram);
        }
        public FeederTemplate ReturnFeeder()
        {
            return Feeder;
        }
        private void IDC_Meal1_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal1.Text, out TestTime))
            {
                if(TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal1.Text = Feeder.Meal1.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal1.Text = Feeder.Meal1.ToString();
                }
                else
                {
                    IDC_Meal1.Text = TestTime.ToString();
                    Feeder.Meal1 = TestTime;
                }    
                
            }
            else
                IDC_Meal1.Text = Feeder.Meal1.ToString();
        }
        private void IDC_Meal2_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal2.Text, out TestTime))
            {
                if (TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal2.Text = Feeder.Meal2.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal2.Text = Feeder.Meal2.ToString();
                }
                else
                {
                    IDC_Meal2.Text = TestTime.ToString();
                    Feeder.Meal2 = TestTime;
                }
                
            }
            else
                IDC_Meal2.Text = Feeder.Meal2.ToString();
        }

        private void IDC_Meal3_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal3.Text, out TestTime))
            {
                if (TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal3.Text = Feeder.Meal3.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal3.Text = Feeder.Meal3.ToString();
                }
                else
                {
                    IDC_Meal3.Text = TestTime.ToString();
                    Feeder.Meal3 = TestTime;
                }
                
            }
            else
                IDC_Meal3.Text = Feeder.Meal3.ToString();
        }

        private void IDX_FEEDERENABLE_CheckedChanged(object sender, EventArgs e)
        {
            Feeder.Enabled = IDX_FEEDERENABLE.Checked;
        }

        private void IDC_PPG_TextChanged(object sender, EventArgs e)
        {
            Double TestDouble;
            if (Double.TryParse(IDC_PPG.Text, out TestDouble))
            {
                IDC_PPG.Text = string.Format("{0:0.0000}", TestDouble);
                Feeder.PelletsPerGram = TestDouble;
            }
            else
            {
                IDC_PPG.Text = string.Format("{0:0.00}", Feeder.PelletsPerGram);
            }
        }
        private void IDC_Meal4_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal4.Text, out TestTime))
            {
                if (TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal4.Text = Feeder.Meal4.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal4.Text = Feeder.Meal4.ToString();
                }
                else
                {
                    IDC_Meal4.Text = TestTime.ToString();
                    Feeder.Meal4 = TestTime;
                }
                
            }
            else
                IDC_Meal4.Text = Feeder.Meal4.ToString();
        }

        private void IDC_Meal5_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal5.Text, out TestTime))
            {
                if (TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal5.Text = Feeder.Meal5.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal5.Text = Feeder.Meal5.ToString();
                }
                else
                {
                    IDC_Meal5.Text = TestTime.ToString();
                    Feeder.Meal5 = TestTime;
                }
                
            }
            else
                IDC_Meal5.Text = Feeder.Meal5.ToString();
        }

        private void IDC_Meal6_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal6.Text, out TestTime))
            {
                if (TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal6.Text = Feeder.Meal6.ToString();
                }
                else if (TestTime >= midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IDC_Meal6.Text = Feeder.Meal6.ToString();
                }
                else
                {
                    IDC_Meal6.Text = TestTime.ToString();
                    Feeder.Meal6 = TestTime;
                }
                
            }
            else
                IDC_Meal6.Text = Feeder.Meal6.ToString();
        }

        private void okayButton_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are all meal times input correctly?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                this.Dispose(true);
            }
        }

    }
}
