using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class RenameChans : Form
    {
        private TextBox[] CB;
        public string[] Names;
        public bool OK;
        public RenameChans(int Chans, string[] Cnames)
        {
            InitializeComponent();
            OK = false;
            Names = new string[16];
            CB = new TextBox[16];
            CB[0] = CL1;
            CB[1] = CL2;
            CB[2] = CL3;
            CB[3] = CL4;
            CB[4] = CL5;
            CB[5] = CL6;
            CB[6] = CL7;
            CB[7] = CL8;
            CB[8] = CL9;
            CB[9] = CL10;
            CB[10] = CL11;
            CB[11] = CL12;
            CB[12] = CL13;
            CB[13] = CL14;
            CB[14] = CL15;
            CB[15] = CL16;
            for (int i = 0; i < 16; i++)
            {
                CB[i].Text = Cnames[i];
            }
            for (int i = Chans; i < 16; i++)
            {
                CB[i].Enabled = false;
            }
        }
        private void RenameChans_Load(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            OK = false; //redundant, but whatever. 
            this.Close();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                Names[i] = CB[i].Text;
            }
            OK = true;
            this.Close();
        }
    }
}
