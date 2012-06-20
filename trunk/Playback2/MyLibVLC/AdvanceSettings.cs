using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class AdvanceSettings : Form
    {
        public AdvanceSettings()
        {
            InitializeComponent();
        }
        public void GetInfo(out int CRF, out int Start, out bool Retry)
        {
            if (!int.TryParse(CRFBox.Text, out CRF))
                CRF = 33;
            if (!int.TryParse(StartBox.Text, out Start))
            { }
            Retry = false;
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
