using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class FixChan : Form
    {
        public bool pass;
        public int FixNum; 
        public FixChan()
        {
            InitializeComponent();
            pass = false; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out FixNum))
            {
                return;
            }
            pass = true;
            this.Close(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pass = false;
            this.Close();
        }
    }
}
