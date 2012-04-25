using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class SzPrompt : Form
    {
        public string Notes;
        public SzPrompt()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotesBx_TextChanged(object sender, EventArgs e)
        {
            Notes = NotesBx.Text;
        }
    }
}
