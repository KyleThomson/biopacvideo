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
        List<TextBox> MealTime;
        List<TimeSpan> FeederTime; 

        public FeederMenu(FeederTemplate Pass_feeder)
        {
            InitializeComponent();
            Feeder = Pass_feeder;
            midnight = new TimeSpan(0, 0, 0);
            mid_ten = new TimeSpan(0, 10, 0);
            noon = new TimeSpan(12, 00, 0);
            noon_ten = new TimeSpan(12, 10, 0);
            if (Feeder.Meal1 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal1 == midnight) { IDC_Meal1.Text = "No Meal"; }
                else { IDC_Meal1.Text = Feeder.Meal1.ToString(); }
            }
            if (Feeder.Meal2 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal2 == midnight) { IDC_Meal2.Text = "No Meal"; }
                else { IDC_Meal2.Text = Feeder.Meal2.ToString(); }
            }
            if (Feeder.Meal3 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal3 == midnight) { IDC_Meal3.Text = "No Meal"; }
                else { IDC_Meal3.Text = Feeder.Meal3.ToString(); }
            }
            if (Feeder.Meal4 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal4 == midnight) { IDC_Meal4.Text = "No Meal"; }
                else { IDC_Meal4.Text = Feeder.Meal4.ToString(); }
            }
            if (Feeder.Meal5 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal5 == midnight) { IDC_Meal5.Text = "No Meal"; }
                else { IDC_Meal5.Text = Feeder.Meal5.ToString(); }
            }
            if (Feeder.Meal6 != TimeSpan.MaxValue)
            {
                if (Feeder.Meal6 == midnight) { IDC_Meal6.Text = "No Meal"; }
                else { IDC_Meal6.Text = Feeder.Meal6.ToString(); }
            }
            IDX_FEEDERENABLE.Checked = Feeder.Enabled;
            IDC_PPG.Text = string.Format("{0:0.000}", Feeder.PelletsPerGram);
            FeederTime = new List<TimeSpan> { Feeder.Meal1, Feeder.Meal2, Feeder.Meal3, Feeder.Meal4, Feeder.Meal5, Feeder.Meal6 }; //I ended up doing case switches instead of this 
            MealTime = new List<TextBox> { IDC_Meal1, IDC_Meal2, IDC_Meal3, IDC_Meal4, IDC_Meal5, IDC_Meal6 }; 
            IDC_Meal1.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal1); };
            IDC_Meal2.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal2); };
            IDC_Meal3.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal3); };
            IDC_Meal4.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal4); };
            IDC_Meal5.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal5); };
            IDC_Meal6.LostFocus += delegate (object sender, System.EventArgs e) { IDC_Meal_TextChanged(sender, e, IDC_Meal6); };

        }
        public FeederTemplate ReturnFeeder()
        {
            return Feeder;
        }
        private void IDC_Meal_TextChanged(object sender, EventArgs e, TextBox tb)
        {
            TimeSpan TestTime;
            int i = MealTime.IndexOf(tb); 
            if (TimeSpan.TryParse(MealTime[i].Text, out TestTime))
            {
                if(TestTime >= noon && TestTime <= noon_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MealTime[i].Text = FeederTime[i].ToString(); 
                }
                else if (TestTime > midnight && TestTime <= mid_ten)
                {
                    MessageBox.Show("Feeders Cannot be Set to the Following Times:\n12:00-12:10\n0:00-0:10", "Feeder Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MealTime[i].Text = FeederTime[i].ToString(); 
                }
                else
                {
                    MealTime[i].Text = TestTime.ToString();
                    switch (i)
                    {
                        case 0: Feeder.Meal1 = TestTime; break;
                        case 1: Feeder.Meal2 = TestTime; break;
                        case 2: Feeder.Meal3 = TestTime; break;
                        case 3: Feeder.Meal4 = TestTime; break;
                        case 4: Feeder.Meal5 = TestTime; break;
                        case 5: Feeder.Meal6 = TestTime; break;
                    }
                }    
                
            }
            else if (MealTime[i].Text == "" || MealTime[i].Text == " " || MealTime[i].Text == "0")
            {
                MealTime[i].Text = "No Meal";
                FeederTime[i] = midnight;
                switch (i)
                {
                    case 0: Feeder.Meal1 = midnight; break; 
                    case 1: Feeder.Meal2 = midnight; break; 
                    case 2: Feeder.Meal3 = midnight; break;
                    case 3: Feeder.Meal4 = midnight; break;
                    case 4: Feeder.Meal5 = midnight; break;
                    case 5: Feeder.Meal6 = midnight; break;
                }
            }
            else
                MealTime[i].Text = FeederTime[i].ToString();
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
       
        private void okayButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i<6; i++)
            {
                if (MealTime[i].Text == "")
                {
                    FeederTime[i] = midnight;
                    MealTime[i].Text = "No Meal";
                    switch (i)
                    {
                        case 0: Feeder.Meal1 = midnight; break;
                        case 1: Feeder.Meal2 = midnight; break;
                        case 2: Feeder.Meal3 = midnight; break;
                        case 3: Feeder.Meal4 = midnight; break;
                        case 4: Feeder.Meal5 = midnight; break;
                        case 5: Feeder.Meal6 = midnight; break;
                    }
                }
            }
            DialogResult dr = MessageBox.Show("Are all meal times input correctly?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                this.Dispose(true);
            }
        }

    }
}
