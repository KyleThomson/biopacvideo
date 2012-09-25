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
        ArrayList IDBoxes;


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
            IDBoxes = new ArrayList();
            MakeArrays();
            TextBox TempBox;
            TextBox TempCheck;
            TextBox TempID;
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
            for (int i = 0; i < 16; i++)
            {
                TempBox = WeightBoxes[i] as TextBox;
                TempCheck = MedicatedBoxes[i] as TextBox;
                TempID = IDBoxes[i] as TextBox;
                TempID.Text = AllRats[i].ID;
                IDC_RATLIST.Items.Add(String.Format("Rat{0}", i + 1));
                if (AllRats[i].Weight > 0)
                {
                    TempBox.Text = string.Format("{0:0.0}", AllRats[i].Weight);
                    TempCheck.Text = AllRats[i].Medication.ToString();

                }
                else
                {
                    TempBox.Text = "";
                    //TempCheck.Enabled = false;
                }
            }
            Startup = false;
            updateBoxes();
            IDC_RATLIST.SelectedIndex = 0;
        }


        private void updateBoxes()
        {
            if (!Startup)
            {
                TextBox TempBox;
                TextBox TempCheck;
                TextBox TempID;
                Double Weight;
                int Percent;
                for (int i = 0; i < 16; i++)
                {
                    TempBox = WeightBoxes[i] as TextBox;
                    TempCheck = MedicatedBoxes[i] as TextBox;
                    TempID = IDBoxes[i] as TextBox;
                    AllRats[i].ID = TempID.Text;
                    if (Double.TryParse(TempBox.Text, out Weight))
                    {
                        if ((AllRats[i].Weight <= 0) && (Weight > 0))
                        {
                            Feeder.GenMeals(i, true);
                        }
                        AllRats[i].Weight = Weight;
                    }
                    if (AllRats[i].Weight > 0)
                    {
                        TempCheck.Enabled = true;
                        if (int.TryParse(TempCheck.Text, out Percent))
                        {
                            if (Percent != AllRats[i].Medication)
                            {                                
                                AllRats[i].Medication = Percent; //Save the percentage
                                Feeder.GenMeals(i, true); 
                            }
                        }
                        TempBox.Text = string.Format("{0:0.0}", AllRats[i].Weight);

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
        private void IDC_Meal1_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal1.Text, out TestTime))
            {
                IDC_Meal1.Text = TestTime.ToString();
                Feeder.Meal1 = TestTime;
            }
            else
                IDC_Meal1.Text = Feeder.Meal1.ToString();

        }
        private void IDC_Meal2_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal2.Text, out TestTime))
            {
                IDC_Meal2.Text = TestTime.ToString();
                Feeder.Meal2 = TestTime;
            }
            else
                IDC_Meal2.Text = Feeder.Meal2.ToString();
        }

        private void IDC_Meal3_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal3.Text, out TestTime))
            {
                IDC_Meal3.Text = TestTime.ToString();
                Feeder.Meal3 = TestTime;
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
            IDBoxes.Add(RATID1);
            IDBoxes.Add(RATID2);
            IDBoxes.Add(RATID3);
            IDBoxes.Add(RATID4);
            IDBoxes.Add(RATID5);
            IDBoxes.Add(RATID6);
            IDBoxes.Add(RATID7);
            IDBoxes.Add(RATID8);
            IDBoxes.Add(RATID9);
            IDBoxes.Add(RATID10);
            IDBoxes.Add(RATID11);
            IDBoxes.Add(RATID12);
            IDBoxes.Add(RATID13);
            IDBoxes.Add(RATID14);
            IDBoxes.Add(RATID15);
            IDBoxes.Add(RATID16);

        }
        private void IDX_RAT_FocusLeave(object sender, EventArgs e)
        {
            updateBoxes();
        }
        private void button1_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void IDC_Meal4_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal4.Text, out TestTime))
            {
                IDC_Meal4.Text = TestTime.ToString();
                Feeder.Meal4 = TestTime;
            }
            else
                IDC_Meal4.Text = Feeder.Meal4.ToString();

        }

        private void IDC_Meal5_TextChanged(object sender, EventArgs e)
        {

            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal5.Text, out TestTime))
            {
                IDC_Meal5.Text = TestTime.ToString();
                Feeder.Meal5 = TestTime;
            }
            else
                IDC_Meal5.Text = Feeder.Meal5.ToString();

        }

        private void IDC_Meal6_TextChanged(object sender, EventArgs e)
        {
            TimeSpan TestTime;
            if (TimeSpan.TryParse(IDC_Meal6.Text, out TestTime))
            {
                IDC_Meal6.Text = TestTime.ToString();
                Feeder.Meal6 = TestTime;
            }
            else
                IDC_Meal6.Text = Feeder.Meal6.ToString();
        }


    }         
}
