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
    public partial class ShortGuide : Form
    {
        public ShortGuide()
        {
            InitializeComponent();
        }

        private void eEGManualReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eEGManualReviewToolStripMenuItem.Checked) return;
            
            eEGManualReviewToolStripMenuItem.Checked = false;
            eEGFastReviewToolStripMenuItem.Checked = true;
            MRSC.Show();
            FRSC.Hide();
            



        }

        public void switchGuide()
        {

        }

        private void ShortGuide_Load(object sender, EventArgs e)
        {

        }

        private void eEGFastReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (eEGFastReviewToolStripMenuItem.Checked) return;
            
                
                eEGFastReviewToolStripMenuItem.Checked = false;
                eEGManualReviewToolStripMenuItem.Checked = true;
            FRSC.Show();
            MRSC.Hide();
            
            
                
            
}
    }
}
