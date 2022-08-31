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
    public partial class VLoadWait : Form
    {
        public bool closed = false;

        public VLoadWait()
        {
            
            InitializeComponent();
            
    }

        private void VidLoadClose_Click(object sender, EventArgs e)
        {
            closed = true;
            Close();
        }
    }
}
