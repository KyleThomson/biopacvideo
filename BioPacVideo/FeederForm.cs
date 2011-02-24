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
    public partial class FeederForm : Form
    {
        private RatTemplate[] AllRats;
        private FeederTemplate Feeder;
        private bool Startup;
        ArrayList WeightBoxes;
        ArrayList MedicatedBoxes; 


        //CONSTRUCTOR
        public FeederForm(RatTemplate[] Pass_rats, FeederTemplate Pass_feeder)
        {
            Startup = true;
            InitializeComponent();
            AllRats = RatTemplate.NewInitArray(16);
            AllRats = Pass_rats;
            Feeder = Pass_feeder;
            WeightBoxes = new ArrayList();
            MedicatedBoxes = new ArrayList();
            MakeArrays();    
            TextBox TempBox;
            CheckBox TempCheck;
            if (Feeder.Breakfast != TimeSpan.MaxValue)
                IDC_BREAKFAST.Text = Feeder.Breakfast.ToString();
            if (Feeder.Lunch != TimeSpan.MaxValue)
                IDC_LUNCH.Text = Feeder.Lunch.ToString();
            if (Feeder.Dinner != TimeSpan.MaxValue)
                IDC_DINNER.Text = Feeder.Dinner.ToString();
            IDX_FEEDERENABLE.Checked = Feeder.Enabled;
            IDC_PPG.Text = string.Format("{0:0.00}", Feeder.PelletsPerGram);
            for (int i = 0; i < 16; i++)
            {      
                TempBox = WeightBoxes[i] as TextBox;
                TempCheck = MedicatedBoxes[i] as CheckBox;
                IDC_RATLIST.Items.Add(String.Format("Rat{0}", i + 1));
                if (AllRats[i].Weight > 0)
                {
                    TempBox.Text = string.Format("{0:0.0}", AllRats[i].Weight);
                    TempCheck.Checked = AllRats[i].Medication;
                                           
                    if (TempCheck.Checked)
                        TempCheck.Text = "Medicated";
                    else
                        TempCheck.Text = "Unmedicated";
                }
                else
                {
                    TempBox.Text = "";
                    TempCheck.Enabled = false;
                }                
            }
            Startup = false;
            IDC_RATLIST.SelectedIndex = 0;                     
        }


        private void updateBoxes()
        {
            if (!Startup)
            {
                TextBox TempBox;
                CheckBox TempCheck;
                Double Weight;
                for (int i = 0; i < 16; i++)
                {
                    TempBox = WeightBoxes[i] as TextBox;
                    TempCheck = MedicatedBoxes[i] as CheckBox;
                    if (Double.TryParse(TempBox.Text, out Weight))
                    {
                        AllRats[i].Weight = Weight;
                    }
                    if (AllRats[i].Weight > 0)
                    {
                        TempCheck.Enabled = true;
                        AllRats[i].Medication = TempCheck.Checked;
                        TempBox.Text = string.Format("{0:0.0}", AllRats[i].Weight);
                        if (TempCheck.Checked)
                            TempCheck.Text = "Medicated";
                        else
                            TempCheck.Text = "Unmedicated";
                    }
                    else
                    {
                        TempBox.Text = "";
                        TempCheck.Enabled = false;
                    }
                }
            }
        }

        public RatTemplate[] ReturnRats()
        {
            return AllRats;
        }
        public FeederTemplate ReturnFeeder()
        {
            return Feeder;
        }

        private void IDC_SURGERY_TextChanged(object sender, EventArgs e)
        {
            DateTime TestTime;
            if (DateTime.TryParse(IDC_SURGERY.Text, out TestTime))
            {
                IDC_SURGERY.Text = TestTime.Date.ToShortDateString();
                AllRats[IDC_RATLIST.SelectedIndex].Surgery = TestTime.Date;
            }
            else
                IDC_SURGERY.Text = AllRats[IDC_RATLIST.SelectedIndex].Surgery.ToShortDateString();
        }
        private void IDC_INJECTION_TextChanged(object sender, EventArgs e)
        {
            DateTime TestTime;
            if (DateTime.TryParse(IDC_INJECTION.Text, out TestTime))
            {
                IDC_INJECTION.Text = TestTime.Date.ToShortDateString();
                AllRats[IDC_RATLIST.SelectedIndex].Injection = TestTime.Date;
            }
            else
                IDC_INJECTION.Text = AllRats[IDC_RATLIST.SelectedIndex].Injection.ToShortDateString();
        }

        private void IDC_SEIZURE_TextChanged(object sender, EventArgs e)
        {
            DateTime TestTime;
            if (DateTime.TryParse(IDC_SEIZURE.Text, out TestTime))
            {
                IDC_SEIZURE.Text = TestTime.Date.ToShortDateString();
                AllRats[IDC_RATLIST.SelectedIndex].FirstSeizure = TestTime.Date;
            }
            else
                IDC_SEIZURE.Text = AllRats[IDC_RATLIST.SelectedIndex].FirstSeizure.ToShortDateString();
        }

        private void IDC_RATLIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AllRats[IDC_RATLIST.SelectedIndex].Surgery != DateTime.MinValue)
                IDC_SURGERY.Text = AllRats[IDC_RATLIST.SelectedIndex].Surgery.ToShortDateString();
            else
                IDC_SURGERY.Text = "";
            if (AllRats[IDC_RATLIST.SelectedIndex].FirstSeizure != DateTime.MinValue)
                IDC_SEIZURE.Text = AllRats[IDC_RATLIST.SelectedIndex].FirstSeizure.ToShortDateString();
            else
                IDC_SEIZURE.Text = "";
            if (AllRats[IDC_RATLIST.SelectedIndex].Injection != DateTime.MinValue)
                IDC_INJECTION.Text = AllRats[IDC_RATLIST.SelectedIndex].Injection.ToShortDateString();
            else
                IDC_INJECTION.Text = "";
        }
        private void IDC_BREAKFAST_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_BREAKFAST.Text, out TestTime))
            {
                IDC_BREAKFAST.Text = TestTime.ToString();
                Feeder.Breakfast = TestTime;
            }
            else
                IDC_BREAKFAST.Text = Feeder.Breakfast.ToString();

        }
        private void IDC_LUNCH_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_LUNCH.Text, out TestTime))
            {
                IDC_LUNCH.Text = TestTime.ToString();
                Feeder.Lunch = TestTime;
            }
            else
                IDC_LUNCH.Text = Feeder.Lunch.ToString();
        }

        private void IDC_DINNER_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_DINNER.Text, out TestTime))
            {
                IDC_DINNER.Text = TestTime.ToString();
                Feeder.Dinner = TestTime;
            }
            else
                IDC_DINNER.Text = Feeder.Dinner.ToString();
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
                IDC_PPG.Text = string.Format("{0:0.00}", TestDouble);
                Feeder.PelletsPerGram = TestDouble;
            }
            else
            {
                IDC_PPG.Text = string.Format("{0:0.00}", Feeder.PelletsPerGram);
            }
        }
        
      

        //START CRAPPY CODE
        private void MakeArrays()
        {
            WeightBoxes.Add(IDC_RAT1);
            WeightBoxes.Add(IDC_RAT2);
            WeightBoxes.Add(IDC_RAT3);
            WeightBoxes.Add(IDC_RAT4);
            WeightBoxes.Add(IDC_RAT5);
            WeightBoxes.Add(IDC_RAT6);
            WeightBoxes.Add(IDC_RAT7);
            WeightBoxes.Add(IDC_RAT8);
            WeightBoxes.Add(IDC_RAT9);
            WeightBoxes.Add(IDC_RAT10);
            WeightBoxes.Add(IDC_RAT11);
            WeightBoxes.Add(IDC_RAT12);
            WeightBoxes.Add(IDC_RAT13);
            WeightBoxes.Add(IDC_RAT14);
            WeightBoxes.Add(IDC_RAT15);
            WeightBoxes.Add(IDC_RAT16);
            MedicatedBoxes.Add(IDX_RAT1);
            MedicatedBoxes.Add(IDX_RAT2);
            MedicatedBoxes.Add(IDX_RAT3);
            MedicatedBoxes.Add(IDX_RAT4);
            MedicatedBoxes.Add(IDX_RAT5);
            MedicatedBoxes.Add(IDX_RAT6);
            MedicatedBoxes.Add(IDX_RAT7);
            MedicatedBoxes.Add(IDX_RAT8);
            MedicatedBoxes.Add(IDX_RAT9);
            MedicatedBoxes.Add(IDX_RAT10);
            MedicatedBoxes.Add(IDX_RAT11);
            MedicatedBoxes.Add(IDX_RAT12);
            MedicatedBoxes.Add(IDX_RAT13);
            MedicatedBoxes.Add(IDX_RAT14);
            MedicatedBoxes.Add(IDX_RAT15);
            MedicatedBoxes.Add(IDX_RAT16);
        }
        private void IDX_RAT1_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT2_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT3_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT4_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT5_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT6_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }

        private void IDX_RAT7_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT8_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT10_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT11_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT12_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT9_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT13_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT14_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT15_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDX_RAT16_CheckedChanged(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT1_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT2_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT3_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT4_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT5_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT6_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT7_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT8_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT9_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT10_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT11_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT12_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT13_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT14_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT15_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void IDC_RAT16_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}
