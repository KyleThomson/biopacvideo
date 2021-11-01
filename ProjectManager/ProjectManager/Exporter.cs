using System;
using System.Windows.Forms;

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
            if (seizureDuration.Checked) E.seizureDuration = true;
            if (DetailList.Checked) E.DetailList = true;
            if (Notes.Checked) E.Notes = true;
            if (SzSvBox.Checked) E.SeverityIndx = true;
            if (BloodDraw.Checked) E.BloodDraw = true;
            if (BloodDrawList.Checked) E.BloodDrawList = true;
            if (injections.Checked) E.Injections = true;
            if (binSeizures.Checked)
            {
                E.binSz = true;
                if (groupedSz.Checked)
                {
                    pjt.analysis.test = TESTTYPES.IAK;
                    E.grouped = true;
                    
                    if (pjt == null)
                        return;
                }
                else if (ungroupedSz.Checked)
                { E.ungrouped = true; }

                if (align.Checked)
                    E.align = true;
            }
            // Check if data should be exported
            E.IsExport();

            // Create file dialog box for saving exported project file.
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".csv";
            F.Filter = "CSV files (*.csv) |*.csv";
            F.Title = "Save project (.csv) file";
            F.InitialDirectory = "D:\\";

            // Only open file dialog if user wanted to export project data
            if (E.exportData)
            {
                if (F.ShowDialog() == DialogResult.OK)
                {
                    pjt.ExportData(F.FileName, E);
                }
            }
            // Only open file dialog if user selected binned seizures
            if (E.binSz)
                pjt.ExportBinnedSz(E);

        }

        private void binSeizures_CheckedChanged(object sender, EventArgs e)
        {
            if (binSeizures.Checked)
            {
                groupedSz.Enabled = true;
                ungroupedSz.Enabled = true;
                align.Enabled = true;
            }
            else if (!binSeizures.Checked)
            {
                groupedSz.Enabled = false;
                ungroupedSz.Enabled = false;
                align.Enabled = false;
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

        private void align_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void DetailList_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
