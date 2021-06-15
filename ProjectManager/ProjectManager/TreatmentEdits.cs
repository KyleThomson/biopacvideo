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
    // This class generates a windows form that presents editable fields for each property of InjectionType in opened project
    // User is able to change injection fields
    public partial class TreatmentEdits : Form
    {
        private InjectionType currentInection;
        private bool _routeFlag;
        private bool _doseFlag;
        private bool _doseAmtFlag;
        private bool _addIdFlag;
        private bool _solventFlag;
        public bool _anyChanges;
        public TreatmentEdits(InjectionType injection)
        {
            InitializeComponent();
            currentInection = injection;

            // Set some formatting for textboxes
            routeTextBox.TextAlign = HorizontalAlignment.Left;
            doseTextBox.TextAlign = HorizontalAlignment.Left;
            doseAmtTextBox.TextAlign = HorizontalAlignment.Left;
            addIdTextBox.TextAlign = HorizontalAlignment.Left;
            solventTextBox.TextAlign = HorizontalAlignment.Left;

            // Set textbox text
            routeTextBox.Text = currentInection.Route;
            doseTextBox.Text = currentInection.Dose.ToString();
            doseAmtTextBox.Text = currentInection.DoseAmount.ToString();
            addIdTextBox.Text = currentInection.ADDID;
            solventTextBox.Text = currentInection.solvent;

            // Set flags
            _routeFlag = false;
            _doseFlag = false;
            _doseAmtFlag = false;
            _addIdFlag = false;
            _solventFlag = false;
            
            // main flag to detect changes
            _anyChanges = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void doseTextBox_TextChanged(object sender, EventArgs e)
        {
            _doseFlag = true;
        }

        private void doseAmtTextBox_TextChanged(object sender, EventArgs e)
        {
            _doseAmtFlag = true;
        }

        private void addIdTextBox_TextChanged(object sender, EventArgs e)
        {
            _addIdFlag = true;
        }

        private void solventTextBox_TextChanged(object sender, EventArgs e)
        {
            _solventFlag = true;
        }

        private void TreatmentEdits_Load(object sender, EventArgs e)
        {

        }

        private void routeTextBox_TextChanged(object sender, EventArgs e)
        {
            _routeFlag = true;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (_routeFlag)
            {
                currentInection.Route = routeTextBox.Text;
                _anyChanges = true;
            }

            if (_doseFlag)
            {
                currentInection.Dose = Convert.ToInt32(doseTextBox.Text);
                _anyChanges = true;
            }

            if (_doseAmtFlag)
            {
                currentInection.DoseAmount = Convert.ToDouble(doseAmtTextBox.Text);
                _anyChanges = true;
            }

            if (_addIdFlag)
            {
                currentInection.ADDID = addIdTextBox.Text;
                _anyChanges = true;
            }

            if (_solventFlag)
            {
                currentInection.solvent = solventTextBox.Text;
                _anyChanges = true;
            }
            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            // set flag to false just for good measure, dont modify
            _anyChanges = false;
            Close();
        }
    }
}
