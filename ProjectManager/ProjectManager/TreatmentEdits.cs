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
    public partial class TreatmentEdits : Form
    {
        private InjectionType currentInection;
        private bool _routeFlag;
        private bool _doseFlag;
        private bool _doseAmtFlag;
        private bool _addIdFlag;
        private bool _solventFlag;
        public TreatmentEdits(InjectionType injection)
        {
            InitializeComponent();
            currentInection = injection;

            // Set textbox text
            

            // Set flags
            _routeFlag = false;
            _doseFlag = false;
            _doseAmtFlag = false;
            _addIdFlag = false;
            _solventFlag = false;

    }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void routeTextBox(object sender, EventArgs e)
        {

        }

        private void doseTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void doseAmtTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addIdTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void solventTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
