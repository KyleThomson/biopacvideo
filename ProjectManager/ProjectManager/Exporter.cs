using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ProjectManager
{
    public partial class Exporter : Form
    {
        Project pjt;
        public Exporter(Project p)
        {
            InitializeComponent();
            pjt = p;
        }

        private void Export_Click(object sender, EventArgs e)
        {
            ExportType E = new ExportType();
            if (SzCount.Checked) E.Sz = true;
            if (PelletCount.Checked) E.Pellet = true;
            if (MedCount.Checked) E.Med = true;
            if (MealCheck.Checked) E.Meal = true;
            if (SzTimes.Checked) E.SzTime = true;
            if (DetailList.Checked) E.DetailList = true;
            if (Notes.Checked) E.Notes = true;
            if (SzSvBox.Checked) E.SeverityIndx = true;
            if (BloodDraw.Checked) E.BloodDraw = true;
            if (BloodDrawList.Checked) E.BloodDrawList = true;
            if (binSeizures.Checked)
            { 
                E.binSz = true;
                if (groupedSz.Checked)
                { 
                    E.grouped = true;
                    // Have user assign groups
                    if (pjt == null)
                        return;
                    AddGroup addGroup = new AddGroup(pjt);
                    addGroup.ShowDialog();
                    pjt = addGroup.pjt;
                    addGroup.Dispose();
                }
                else if (ungroupedSz.Checked)
                { E.ungrouped = true; }
            }
            
            
            // Create file dialog box for saving exported project file.
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".csv";
            F.Filter = "CSV files (*.csv) |*.csv";
            F.Title = "Save project (.csv) file";
            F.InitialDirectory = "D:\\";

            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt.ExportData(F.FileName, E);
            }
        }

        private void binSeizures_CheckedChanged(object sender, EventArgs e)
        {
            if (binSeizures.Checked)
            {
                groupedSz.Enabled = true;
                ungroupedSz.Enabled = true;
            }
            else if (!binSeizures.Checked)
            {
                groupedSz.Enabled = false;
                ungroupedSz.Enabled = false;
            }
        }

        private void Exporter_Load(object sender, EventArgs e)
        {

        }

        private void groupedSz_CheckedChanged(object sender, EventArgs e)
        {
            if (groupedSz.Checked)
            { ungroupedSz.Checked = false; }
        }

        private void ungroupedSz_CheckedChanged(object sender, EventArgs e)
        {
            if (ungroupedSz.Checked)
            { groupedSz.Checked = false; }
        }
    }

}
