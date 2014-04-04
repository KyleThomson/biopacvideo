using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
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
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "D:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt.ExportData(F.FileName, E);
            }
        }

    }

}
