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
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "D:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt.ExportData(F.FileName);
            }
        }

    }

}
