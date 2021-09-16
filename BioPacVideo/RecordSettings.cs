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
    public partial class RecordSettings : Form
    {
        MPTemplate MP;
        public RecordSettings()
        {
            InitializeComponent();
            this.ControlBox = false; 
            MP = MPTemplate.Instance;
            if (String.Equals(MP.MPtype, "MP160"))
            {
                MPTypeBox.SelectedIndex = 1; 
            }
            else
            {
                MPTypeBox.SelectedIndex = 0;
            }
            if (MP.FileSplit)
            {
                MidnightNoon.Checked = true;

            }
            else
            {
                Midnight.Checked = true;
            }
        }

        private void MPTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MPTypeBox.SelectedIndex==0)
            {
                MP.MPtype = "MP150";
            }
            else
            {
                MP.MPtype = "MP160";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MP.FileSplit = MidnightNoon.Checked;
            this.Close();
        }
    }
}
