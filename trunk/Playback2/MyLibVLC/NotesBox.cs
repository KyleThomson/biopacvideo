using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class NotesBox : Form
    {
        public bool OK;
        public string Notes; 
        public NotesBox(string N)
        {
            InitializeComponent();
            OK = false;
            NotesTxtBox.Text = N;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OK = true;
            this.Close();
        }

        private void NotesTxtBox_TextChanged(object sender, EventArgs e)
        {
            Notes = NotesTxtBox.Text;
        }
    }
}
