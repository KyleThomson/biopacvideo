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
      private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)        
        {
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "D:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt = new Project(F.FileName);
                pjt.Open();
            }                    
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
            UpdateLists();
        }

        private void UpdateLists()
        {
            pjt.Sort();
            AnimalList.Items.Clear();
            string[] A = pjt.Get_Animals();
            for (int i = 0; i < A.Length; i++)
            {
                AnimalList.Items.Add(A[i]);
            }
        }

        private void importSeizureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt != null)
            {
                OpenFileDialog F = new OpenFileDialog();
                //F.Filter = "*.txt";            
                F.InitialDirectory = "D:\\";
                if (F.ShowDialog() == DialogResult.OK)
                {
                    //  File.Copy(F.FileName, pjt.P + "\\Data\\" + Path.GetFileName(F.FileName));
                    pjt.ImportSzFile(F.FileName);
                }
                UpdateLists();
                pjt.Save();
            }
        }

        private void AnimalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SzList.Items.Clear();
            string[] S = pjt.Get_Seizures((string)AnimalList.Items[AnimalList.SelectedIndex]);
            for (int i = 0; i < S.Length; i++)
            {
                SzList.Items.Add(S[i]);
            }
        }
    }
}
