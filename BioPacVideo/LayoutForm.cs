using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class LayoutForm : Form
    {
        FeederTemplate Feeder;
        MPTemplate MP; 
        
        public LayoutForm(FeederTemplate passFeeder)
        {
            Feeder = passFeeder;        
            InitializeComponent();
            xInput.Text = Feeder.Cages_X.ToString();
            yInput.Text = Feeder.Cages_Y.ToString(); 
        }
        private void xInput_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if(int.TryParse(xInput.Text, out temp))
            {
                Feeder.Cages_X = temp; 
            }
            else
            {
                xInput.Text = Feeder.Cages_X.ToString();
            }
        }
        private void yInput_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if (int.TryParse(yInput.Text, out temp))
            {
                Feeder.Cages_Y = temp;
            }
            else
            {
                xInput.Text = Feeder.Cages_Y.ToString();
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            AnimalSettings frm = new AnimalSettings(Feeder, MP.Recording);
            frm.Height = (Feeder.Cages_Y * 216) + 138;
            frm.Width = (Feeder.Cages_X * 150) + 50;
            frm.ShowDialog(this);
            frm.Dispose(); 
        }
    }
}
