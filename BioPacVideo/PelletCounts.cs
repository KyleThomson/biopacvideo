using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BioPacVideo
{
    public partial class PelletCounts : Form
    {
        FeederTemplate Feeder;        
        ArrayList IDBoxes;
        private ArrayList PelletBoxes;
        private ArrayList BloodDraws;
        private ArrayList BloodDrawIDs;
        private ArrayList MedBoxes; 
        public PelletCounts()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;            
            IDBoxes = new ArrayList();
            PelletBoxes = new ArrayList();
            BloodDraws = new ArrayList();
            BloodDrawIDs = new ArrayList();
            MedBoxes = new ArrayList();
            TextBox IDB;
            TextBox PCB; 
            CreateArrays();
            for (int i = 0; i < 16; i++)
            {
                IDB = IDBoxes[i] as TextBox;
                IDB.Text = Feeder.Rats[i].ID.ToString();                
            }
        }
        private void CreateArrays()
        {
            IDBoxes.Add(IDBox1);
            IDBoxes.Add(IDBox2);
            IDBoxes.Add(IDBox3);
            IDBoxes.Add(IDBox4);
            IDBoxes.Add(IDBox5);
            IDBoxes.Add(IDBox6);
            IDBoxes.Add(IDBox7);
            IDBoxes.Add(IDBox8);
            IDBoxes.Add(IDBox9);
            IDBoxes.Add(IDBox10);
            IDBoxes.Add(IDBox11);
            IDBoxes.Add(IDBox12);
            IDBoxes.Add(IDBox13);
            IDBoxes.Add(IDBox14);
            IDBoxes.Add(IDBox15);
            IDBoxes.Add(IDBox16);
            PelletBoxes.Add(PCBox1);
            PelletBoxes.Add(PCBox2);
            PelletBoxes.Add(PCBox3);
            PelletBoxes.Add(PCBox4);
            PelletBoxes.Add(PCBox5);
            PelletBoxes.Add(PCBox6);
            PelletBoxes.Add(PCBox7);
            PelletBoxes.Add(PCBox8);
            PelletBoxes.Add(PCBox9);
            PelletBoxes.Add(PCBox10);
            PelletBoxes.Add(PCBox11);
            PelletBoxes.Add(PCBox12);
            PelletBoxes.Add(PCBox13);
            PelletBoxes.Add(PCBox14);
            PelletBoxes.Add(PCBox15);
            PelletBoxes.Add(PCBox16);
            BloodDraws.Add(BloodDraw1);
            BloodDraws.Add(BloodDraw2);
            BloodDraws.Add(BloodDraw3);
            BloodDraws.Add(BloodDraw4);
            BloodDraws.Add(BloodDraw5);
            BloodDraws.Add(BloodDraw6);
            BloodDraws.Add(BloodDraw7);
            BloodDraws.Add(BloodDraw8);
            BloodDraws.Add(BloodDraw9);
            BloodDraws.Add(BloodDraw10);
            BloodDraws.Add(BloodDraw11);
            BloodDraws.Add(BloodDraw12);
            BloodDraws.Add(BloodDraw13);
            BloodDraws.Add(BloodDraw14);
            BloodDraws.Add(BloodDraw15);
            BloodDraws.Add(BloodDraw16);
            BloodDrawIDs.Add(Comments1);
            BloodDrawIDs.Add(Comments2);
            BloodDrawIDs.Add(Comments3);
            BloodDrawIDs.Add(Comments4);
            BloodDrawIDs.Add(Comments5);
            BloodDrawIDs.Add(Comments6);
            BloodDrawIDs.Add(Comments7);
            BloodDrawIDs.Add(Comments8);
            BloodDrawIDs.Add(Comments9);
            BloodDrawIDs.Add(Comments10);
            BloodDrawIDs.Add(Comments11);
            BloodDrawIDs.Add(Comments12); 
            BloodDrawIDs.Add(Comments13);
            BloodDrawIDs.Add(Comments14);
            BloodDrawIDs.Add(Comments15);
            BloodDrawIDs.Add(Comments16);
            MedBoxes.Add(MedBox1);
            MedBoxes.Add(MedBox2);
            MedBoxes.Add(MedBox3);
            MedBoxes.Add(MedBox4);
            MedBoxes.Add(MedBox5);
            MedBoxes.Add(MedBox6);
            MedBoxes.Add(MedBox7);
            MedBoxes.Add(MedBox8);
            MedBoxes.Add(MedBox9);
            MedBoxes.Add(MedBox10);
            MedBoxes.Add(MedBox11);
            MedBoxes.Add(MedBox12);
            MedBoxes.Add(MedBox13);
            MedBoxes.Add(MedBox14);
            MedBoxes.Add(MedBox15);
            MedBoxes.Add(MedBox16); 
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            TextBox PCB;
            TextBox IDB;
            TextBox BDB;
            TextBox CMB;
            TextBox MDB;
            double PelletCount;
            TimeSpan TestTime;
            string LogText; 
            ArrayList LogList = new ArrayList(); 
            for (int i = 0; i < 16; i++)
            {
                PCB = PelletBoxes[i] as TextBox;
                IDB = IDBoxes[i] as TextBox;
                BDB = BloodDraws[i] as TextBox;
                CMB = BloodDrawIDs[i] as TextBox;
                MDB = MedBoxes[i] as TextBox; 
                if (!string.IsNullOrEmpty(PCB.Text))
                {
                    if (double.TryParse(PCB.Text, out PelletCount))
                    {
                        LogList.Add("Removal: " + (i + 1).ToString() + " " +  " TotP: " + PelletCount.ToString("D3"));
                    }
                    else
                    {
                        MessageBox.Show("Error Parsing Total Pellet Count: " + (i + 1).ToString());
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(MDB.Text))
                {
                    if (double.TryParse(MDB.Text, out PelletCount))
                    {
                        LogList.Add("Removal: " + (i + 1).ToString() +  " MedP: " + PelletCount.ToString("D3"));                        
                    }
                    else
                    {
                        MessageBox.Show("Error Parsing Med Pellet Count " + (i + 1).ToString());
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(BDB.Text))
                {
                    if (!(Char.IsLetter(CMB.Text[0]) && Char.IsNumber(CMB.Text[1]) && Char.IsNumber(CMB.Text[2]) && (CMB.Text.Length == 3))) 
                    {
                        MessageBox.Show("Error Parsing Blood Letting ID " + (i + 1).ToString());
                        return;
                    }
                    if (TimeSpan.TryParse(BDB.Text, out TestTime))
                    {
                        LogList.Add("BloodDraw: " + (i + 1).ToString() + " Time: " + TestTime.ToString() + " ID: " + CMB.Text);
                    }
                    else
                    {
                        MessageBox.Show("Error Parsing Blood Letting Time" + (i + 1).ToString());
                        return;
                    }
                }   
            }
            for (int i = 0; i < LogList.Count; i++) //We need to wait until everything is properly parsed before adding it to the log file!!
            {
                LogText = LogList[i] as String; //Grab a text to log 
                Feeder.Log(LogText); //Add it to the log
            }
            this.Close(); 
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PelletCounts_Load(object sender, EventArgs e)
        {

        }

        
    


       
    }
}
