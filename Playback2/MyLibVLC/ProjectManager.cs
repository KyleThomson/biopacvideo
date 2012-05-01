using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SeizurePlayback
{
    public partial class ProjectManager : Form
    {
        Project pjt; 
        public ProjectManager()
        {
            InitializeComponent();
        }

        private void selectProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog F = new OpenFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "D:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt = new Project(F.FileName);
                pjt.Open();
            }             
        }

        private void importSeizureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog F = new OpenFileDialog();
            F.Filter = "*.txt";            
            F.InitialDirectory = "D:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                File.Copy(F.FileName, pjt.P + "\\Data\\" + Path.GetFileName(F.FileName));
                pjt.ImportSzFile(F.FileName);        
            }             
        }
    }
}
