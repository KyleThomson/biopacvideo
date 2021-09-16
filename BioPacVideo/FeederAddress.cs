using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BioPacVideo
{
    public partial class FeederAddress : Form
    {
        private ArrayList FeederLabels;
        private FeederTemplate Feeder; 
        private Label TempLabel;
        public FeederAddress(FeederTemplate Pass_feeder)
        {
            Feeder = Pass_feeder;            
            InitializeComponent();
            FeederLabels = new ArrayList();
            FeederInit();
            AnimalBox.SelectedIndex = 0;
            Med.SelectedIndex = 0;
            FeederSelect.SelectedIndex = 0;
        }                
        private void FeederInit()
        {
            checkBox1.Checked = Feeder.AlternateAddress;
            FeederLabels.Add(FeederLabel0);
            FeederLabels.Add(FeederLabel1);
            FeederLabels.Add(FeederLabel2);
            FeederLabels.Add(FeederLabel3);
            FeederLabels.Add(FeederLabel4);
            FeederLabels.Add(FeederLabel5);
            FeederLabels.Add(FeederLabel6);
            FeederLabels.Add(FeederLabel7);
            FeederLabels.Add(FeederLabel8);
            FeederLabels.Add(FeederLabel9);
            FeederLabels.Add(FeederLabel10);
            FeederLabels.Add(FeederLabel11);
            FeederLabels.Add(FeederLabel12);
            FeederLabels.Add(FeederLabel13);
            FeederLabels.Add(FeederLabel14);
            FeederLabels.Add(FeederLabel15);
            FeederLabels.Add(FeederLabel16);
            FeederLabels.Add(FeederLabel17);
            FeederLabels.Add(FeederLabel18);
            FeederLabels.Add(FeederLabel19);
            FeederLabels.Add(FeederLabel20);
            FeederLabels.Add(FeederLabel21);
            FeederLabels.Add(FeederLabel22);
            FeederLabels.Add(FeederLabel23);
            FeederLabels.Add(FeederLabel24);
            FeederLabels.Add(FeederLabel25);
            FeederLabels.Add(FeederLabel26);
            FeederLabels.Add(FeederLabel27);
            FeederLabels.Add(FeederLabel28);
            FeederLabels.Add(FeederLabel29);
            FeederLabels.Add(FeederLabel30);
            FeederLabels.Add(FeederLabel31);
            UpdateLabels();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {            
            Feeder.AlternateAddress = checkBox1.Checked;            
        }
        private void FeederAddress_Load(object sender, EventArgs e)
        {
            
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            for (int i=0; i<32; i++)
            {
                Feeder.AddressTable[i] = i; 
            }
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            for (Int16 i = 0; i < 32; i++)
            {
                TempLabel = FeederLabels[i] as Label;
                TempLabel.Text = "Feeder " + Feeder.AddressTable[i].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Feeder.AddressTable[((2*AnimalBox.SelectedIndex)+Med.SelectedIndex)] = FeederSelect.SelectedIndex;
            UpdateLabels();
        }
    }
}
