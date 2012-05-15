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
            MainSelect.SelectedIndex = 0;
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
            UpdateMainList();
        }

        private void UpdateMainList()
        {
            if (pjt == null)
                return;
            pjt.Sort();
            MainList.Items.Clear();
            if (MainSelect.SelectedIndex == 0) //Files
            {                    
                    string[] Fl = pjt.Get_Files();
                    for (int i = 0; i < Fl.Length; i++)
                    {
                        MainList.Items.Add(Fl[i]);
                    }                                       
            }
            else if (MainSelect.SelectedIndex == 1) //Animals
            {                
                string[] A = pjt.Get_Animals();
                for (int i = 0; i < A.Length; i++)
                {
                    MainList.Items.Add(A[i]);
                }
               
            }
        }
        private void UpdateSecondList()
        {
            if (pjt == null)
                return;            
            SecondList.Items.Clear();
            if (MainSelect.SelectedIndex == 0) //Files
            {
                
            }
            else if (MainSelect.SelectedIndex == 1) //Animals
            {            
                if (MainList.SelectedIndex != -1)
                {
                    if (SecondSelect.SelectedIndex == 0)
                    {
                        SecondList.Items.Clear();
                        string[] S = pjt.Get_Seizures((string)MainList.Items[MainList.SelectedIndex]);
                        for (int i = 0; i < S.Length; i++)
                        {
                            SecondList.Items.Add(S[i]);
                        }
                    }
                    else if (SecondSelect.SelectedIndex == 1)
                    {
                        //Get weights
                    }
                    else if (SecondSelect.SelectedIndex == 2)
                    {
                        SecondList.Items.Clear();
                        string[] S = pjt.Get_Meals((string)MainList.Items[MainList.SelectedIndex]);
                        for (int i = 0; i < S.Length; i++)
                        {
                            SecondList.Items.Add(S[i]);
                        }
                    }
                }
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
                UpdateMainList();
                pjt.Save();
            }
        }

        private void SecondList_SelectedIndexChanged(object sender, EventArgs e)
        {              
        }

        
        private void importFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt != null)
            {

                FolderBrowserDialog F = new FolderBrowserDialog();                
                if (F.ShowDialog(this) == DialogResult.OK)
                {                    
                    pjt.ImportDirectory(F.SelectedPath);
                }                
                UpdateMainList();
                pjt.Save();
            }
        }

        private void MainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSecondList();
        }

        private void MainSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainSelect.SelectedIndex == 0)
            {
                SecondSelect.Items.Clear();
                SecondSelect.Items.Add("Animals");
                SecondSelect.SelectedIndex = 0;
            }
            else if (MainSelect.SelectedIndex == 1)
            {
                SecondSelect.Items.Clear();
                SecondSelect.Items.Add("Seizures");
                SecondSelect.Items.Add("Weights");
                SecondSelect.Items.Add("Meals");
                SecondSelect.SelectedIndex = 0;
            }
            UpdateMainList();
        }

        private void SecondSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSecondList();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt != null)
            {
                Exporter F = new Exporter(pjt);
                F.ShowDialog(this);
                F.Dispose();
            }

        }
    }
}
