using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    
    public partial class SzDetPrompt : Form
    {
        public int num; 
        public SzDetPrompt()
        {
            InitializeComponent();
            num = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int.TryParse(NumBox.Text, out num);
            this.Close();
        }
    }
}
