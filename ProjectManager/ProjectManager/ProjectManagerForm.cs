using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProjectManager
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
            F.InitialDirectory = "C:\\";
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
            F.InitialDirectory = "C:\\";
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
                if (SecondSelect.SelectedIndex == 0)
                {
                    SecondList.Items.Clear();
                    foreach (string A in pjt.Files[MainList.SelectedIndex].AnimalIDs) 
                    {
                        SecondList.Items.Add(A);
                    }
                }
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
            //This function probably should not be used. 
            if (pjt != null)
            {
                OpenFileDialog F = new OpenFileDialog();
                //F.Filter = "*.txt";            
                F.InitialDirectory = "C:\\";
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
                    if (!pjt.ImportDirectory(F.SelectedPath))
                    {
                        MessageBox.Show("File already imported", "ERROR");
                    }
                }                
                UpdateMainList();
                pjt.Save();
            }
        }

        private void MainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            UpdateSecondList();

            // Create an event handler if someone right clicks an item in main list.
            MainList.MouseDown += new MouseEventHandler(this.MainList_MouseDown);


        }
        private void MainList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // check if right mouse button was clicked
            {
                var item = MainList.IndexFromPoint(e.Location);

                if (item >= 0) // check if item selected is in range of MainList
                {
                    ContextMenuStrip rightClickMenu = new ContextMenuStrip(); // generate context menu
                    // Add menu options
                    var deleteAnimal = rightClickMenu.Items.Add("Delete");
                    MainList.ContextMenuStrip = rightClickMenu;
                    MainList.SelectedIndex = item;
                    rightClickMenu.Show(MainList, e.Location);
                    rightClickMenu.AutoClose = true;
                    // Call event handler if delete was selected
                    deleteAnimal.Click += new EventHandler(delete_Click);
                    
                }
            }
        }
        private void delete_Click(object sender, EventArgs e)
        {
            var index = MainList.SelectedIndex;
            MainList.Items.RemoveAt(index);
            if (MainSelect.SelectedIndex == 1) // operate on animals
            {
                
                pjt.Animals.RemoveAt(index);
            }
            else if(MainSelect.SelectedIndex == 0) // operate on files
            {
                pjt.Files.RemoveAt(index);
            }
                     
        }

        private void MainSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
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
            if (pjt == null)
                return;
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

        private void tempHumidityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            WeightAdd Frm = new WeightAdd();
            Frm.ShowDialog();
            if (Frm.OK)
            {
                for (int i = 0; i < Frm.Count; i++)
                {
                    pjt.AddWeight(Frm.ID[i], Frm.Weights[i], Frm.Pellets[i], Frm.Date);                
                }
            }
            Frm.Dispose();
        }

        private void addMultipleDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            int DuplicateDirectoryCount = 0; 
            MultiDirectoryAdd Frm = new MultiDirectoryAdd();
            Frm.ShowDialog();
            if (Frm.Pass)
            {
                for (int i = 0; i < Frm.DirReturn.Length; i++)
                {
                    //  File.Copy(F.FileName, pjt.P + "\\Data\\" + Path.GetFileName(F.FileName));
                    if (!pjt.ImportDirectory(Frm.DirReturn[i]))
                    {
                        DuplicateDirectoryCount++;
                    }
                }
            }
            UpdateMainList();
            pjt.Save();
            if (DuplicateDirectoryCount > 0)
                Info.Text = DuplicateDirectoryCount.ToString() + " duplicate directories skipped.";
        }

        private void mergeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            OpenFileDialog F = new OpenFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "C:\\";
            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt.MergeProject(F.FileName);                
            }
            UpdateMainList();            
        }

        private void calendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            //Calendar F = new Calendar(pjt);
            //F.ShowDialog();
        }

        private void importantDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            AddDate F = new AddDate(pjt);
            F.ShowDialog();
            pjt = F.pjt;
            pjt.Save();
            F.Dispose();
        }

        private void groupAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            AddGroup F = new AddGroup(pjt);
            F.ShowDialog();
            pjt = F.pjt;
            pjt.Save();
            F.Dispose();

        }

        private void testPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SzGraph Test = new SzGraph(4000, 4000, pjt, "T35");
            Test.GetXTickLabels(pjt,5); // pjt, tick label every 5 units
            Test.GetYTickLabels(pjt);
            Test.PlotSz(pjt);
            Test.PlotTrt(pjt);
            Test.Legend();
            Test.DisplayHeader();
            Test.DisplayStats(pjt);
            Test.graph.DisplayGraph();
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {

        }
    }
}
