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
    // Windows form that presents editable fields for notes and seizure severity. 
    public partial class SeizureEdits : Form
    {
        private SeizureType currentSeizure;
        private bool _severityFlag;
        private bool _notesFlag;
        public bool _seizureModified;
        public SeizureEdits(SeizureType seizure)
        {
            InitializeComponent();
            currentSeizure = seizure;

            // Set some formatting for textboxes
            severityField.TextAlign = HorizontalAlignment.Left;
            notesField.TextAlign = HorizontalAlignment.Left;
            timespanField.TextAlign = HorizontalAlignment.Left;
            dateField.TextAlign = HorizontalAlignment.Left;

            // Set text for form fields
            severityField.Text = currentSeizure.Severity.ToString();
            notesField.Text = string.Concat(currentSeizure.Notes.Where(c => !char.IsWhiteSpace(c)));
            timespanField.Text = currentSeizure.t.ToString();
            dateField.Text = currentSeizure.d.ToString();

            // Set flags
            _severityFlag = false;
            _notesFlag = false;
            _seizureModified = false;
    }

        private void SeizureEdits_Load(object sender, EventArgs e)
        {

        }

        private void severityField_TextChanged(object sender, EventArgs e)
        {
            _severityFlag = true;
        }

        private void notesField_TextChanged(object sender, EventArgs e)
        {
            _notesFlag = true;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (_severityFlag)
            {
                if (int.TryParse(severityField.Text, out _))
                {
                    currentSeizure.Severity = Convert.ToInt32(severityField.Text);
                    _seizureModified = true;
                }
            }

            if (_notesFlag)
            {
                currentSeizure.Notes = notesField.Text;
                _seizureModified = true;
            }
            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
