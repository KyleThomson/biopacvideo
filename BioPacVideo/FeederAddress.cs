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
            checkBoxAlternateAddress.Checked = Feeder.AlternateAddress;
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
        private void checkBoxAlternateAddress_CheckedChanged(object sender, EventArgs e)
        {            
            Feeder.AlternateAddress = checkBoxAlternateAddress.Checked;            
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            checkBoxAlternateAddress.Checked = false;
            AnimalBox.SelectedIndex = 0;
            Med.SelectedIndex = 0;
            FeederSelect.SelectedIndex = 0;

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

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            Feeder.AddressTable[((2*AnimalBox.SelectedIndex)+Med.SelectedIndex)] = FeederSelect.SelectedIndex;
            UpdateLabels();
            checkBoxAlternateAddress.Checked = true;
        }

        private void buttonMouseRoom_Click(object sender, EventArgs e)
        {
            checkBoxAlternateAddress.Checked = true;
            int[] feederPattern = { 0, 1, 2, 4, 5, 6, 8, 9, 10, 12, 13, 14, 16, 17, 18, 20 };
            for (int i = 0; i < feederPattern.Length; i++)
            {
                Feeder.AddressTable[2 * i] = feederPattern[i];
                Feeder.AddressTable[2 * i + 1] = feederPattern[i];
            }

            for (Int16 i = 0; i < 32; i++)
            {
                TempLabel = FeederLabels[i] as Label;
                TempLabel.Text = "Feeder " + Feeder.AddressTable[i].ToString();
            }
        }
    }
}
