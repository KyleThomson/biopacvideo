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
        ArrayList PelletBoxes; 
        public PelletCounts()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;            
            IDBoxes = new ArrayList();
            PelletBoxes = new ArrayList();
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
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            TextBox PCB; 
            int PelletCount; 
            for (int i = 0; i < 16; i++)
            {
                PCB = PelletBoxes[i] as TextBox;
                if (!string.IsNullOrEmpty(PCB.Text))
                {
                    if (int.TryParse(PCB.Text, out PelletCount))
                    {
                        Feeder.Log("Removal - Cage: " + (i + 1).ToString() + "Pellets: " + PelletCount.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Error Parsing Box " + (i + 1).ToString());
                        return;
                    }
                }

            }
        }

    


       
    }
}
