using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager
{
    // This class is used to prompt user for some inputs for plotting
    public partial class TestDescription : Form
    {
        public string ETSP; 
        public string Batch;
        public string Dose; 
        public string Frequency;
        public bool _cancelled = false;
        public TestDescription()
        {
            InitializeComponent();
            ETSP = "----";
            Batch = "----";
            Dose = "----";
            Frequency = "----";
        }

        private void TestDescription_Load(object sender, EventArgs e)
        {

        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            // set values based on user input, keep default values if user didn't enter anything
            if (!String.IsNullOrEmpty(etspEntry.Text))
            { ETSP = etspEntry.Text; }
            if (!String.IsNullOrEmpty(doseEntry.Text))
            { Dose = doseEntry.Text; }
            if (!String.IsNullOrEmpty(batchEntry.Text))
            { Batch = batchEntry.Text; }
            if (!String.IsNullOrEmpty(frequencyEntry.Text))
            { Frequency = frequencyEntry.Text; }

            Close();
        }
        

        private void etspEntry_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _cancelled = true;
            Close();
        }
    }
}
