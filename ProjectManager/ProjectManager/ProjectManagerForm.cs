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
        SzGraph pjtGraph;
        string openedFilename;
        bool _doAnalysis; // flag on project open if we should do analysis
        public ProjectManager()
        {
            InitializeComponent();
            MainSelect.SelectedIndex = 0;
            FormClosed += new FormClosedEventHandler(ProjectManagerClosed);
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
        private void ProjectManagerClosed(object sender, FormClosedEventArgs e)
        {
            // Check file changed flag in project data
            if (pjt._fileChanged)
            {
                SaveReminderDialog saveReminderDialog = new SaveReminderDialog();
                saveReminderDialog.Show();
            }
        }
        private void selectProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open file dialog to select project
            OpenFileDialog F = new OpenFileDialog();
            F.DefaultExt = ".pjt";
            F.InitialDirectory = "C:\\";

            

            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt = new Project(F.FileName);
                openedFilename = F.FileName;
                pjt.Open();
                pjt.AreAnimalsDead();
                pjt.CompareStageConflicts(); // Find conflicts between bubble and notes

                // Create message box to prompt user if they want to perform analysis on project import
                string confirmationMessage = "Would you like to analyze the imported project file?";
                DialogResult confirmAnalysisResult = MessageBox.Show(confirmationMessage, "Project Analysis", MessageBoxButtons.YesNo);
                if (confirmAnalysisResult == DialogResult.Yes)
                { _doAnalysis = true; }
                else
                { _doAnalysis = false; }
                if (_doAnalysis)
                {
                    pjt.DetermineTest();         // Determine test to use
                    pjt.TestSort();              // Sort test, return if test is undefined
                    pjt.Analysis();              // Now perform analysis once/if any conflicts are resolved
                }
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
                    else if (SecondSelect.SelectedIndex == 3)
                    {
                        SecondList.Items.Clear();
                        if (pjt.test == TESTTYPES.T35)
                        {
                            SecondList.Items.Add("Baseline");
                            SecondList.Items.Add("Vehicle");
                            SecondList.Items.Add("Drug");
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
            }
        }

        private void SecondList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecondList.MouseDown += new MouseEventHandler(this.SecondList_MouseDown);
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
        private void SecondList_MouseDown(object sender, MouseEventArgs e)
        { 
            if (e.Button == MouseButtons.Right) // check if right mouse button was clicked
            {
                var item = SecondList.IndexFromPoint(e.Location);
                if (item >= 0) // check if item is in range
                {
                    ContextMenuStrip rightClickMenu = new ContextMenuStrip();
                    // add menu options
                    var deleteTrt = rightClickMenu.Items.Add("Delete");
                    SecondList.ContextMenuStrip = rightClickMenu;
                    SecondList.SelectedIndex = item;
                    rightClickMenu.Show(SecondList, e.Location);
                    rightClickMenu.AutoClose = true;

                    // call event handler to delete treatment
                    deleteTrt.Click += new EventHandler(delete_secondlist);
                }
            }
        }
        private void delete_secondlist(object sender, EventArgs e)
        {
            if (SecondSelect.SelectedIndex == 0) // Seizure box selected
            {
                if (MainList.SelectedIndex >= 0)
                {
                    // Remove selected seizure
                    pjt.Animals[MainList.SelectedIndex].Sz.RemoveAt(SecondList.SelectedIndex);
                    SecondList.Items.RemoveAt(SecondList.SelectedIndex);
                }
            }
            else if (SecondSelect.SelectedIndex == 2) // Meal box selected
            {
                if (MainList.SelectedIndex >= 0)
                {
                    // Remove selected meal
                    pjt.Animals[MainList.SelectedIndex].Meals.RemoveAt(SecondList.SelectedIndex);
                    SecondList.Items.RemoveAt(SecondList.SelectedIndex);
                }
            }
            else if (SecondSelect.SelectedIndex == 3) // Treatment box selected
            {
                // block of code to remove treatment from analysis
                if (MainList.SelectedIndex >= 0 )
                {
                    if (SecondList.Items[SecondList.SelectedIndex].ToString().ToUpper() == "BASELINE") // BASELINE CHOSEN FOR REMOVAL
                    {
                        pjt.Animals[MainList.SelectedIndex].metrics.RemoveAll(M => M.treatment == TRTTYPE.Baseline);
                        SecondList.Items.RemoveAt(SecondList.SelectedIndex);
                    }
                    else if (SecondList.Items[SecondList.SelectedIndex].ToString().ToUpper() == "VEHICLE") // VEHICLE CHOSEN FOR REMOVAL
                    {
                        pjt.Animals[MainList.SelectedIndex].metrics.RemoveAll(M => M.treatment == TRTTYPE.Vehicle);
                        SecondList.Items.RemoveAt(SecondList.SelectedIndex);
                    }
                    else if (SecondList.Items[SecondList.SelectedIndex].ToString().ToUpper() == "DRUG") // DRUG CHOSEN FOR REMOVAL
                    {
                        pjt.Animals[MainList.SelectedIndex].metrics.RemoveAll(M => M.treatment == TRTTYPE.Drug);
                        SecondList.Items.RemoveAt(SecondList.SelectedIndex);
                    }
                }
            }
            //Re-do analysis once some data has been removed
            if (_doAnalysis)
            { pjt.Analysis(); }
            // indicate file modified
            pjt.FileChanged();
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
            //Re-do analysis once some data has been removed
            if (_doAnalysis)
            { pjt.Analysis(); }
            // indicate file modified
            pjt.FileChanged();
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
                SecondSelect.Items.Add("Treatments");
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
            F.Dispose();
        }

        private void groupAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            AddGroup F = new AddGroup(pjt);
            F.ShowDialog();
            pjt = F.pjt;
            F.Dispose();

        }

        private void testPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SzGraph Test = new SzGraph(4000, 4000, pjt);
            Test.PlotSz(pjt);
            Test.PlotTrt(pjt);
            Test.Legend();
            Test.DisplayHeader();
            Test.DisplayStats(pjt);
            Test.graph.DisplayGraph();
            pjtGraph = Test;
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {

        }

        private void exportTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
            pdf.AddPage();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save .pjt file as -
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Filter = "PJT files (*.pjt) |*.pjt";
            saveAsDialog.DefaultExt = ".pjt";
            saveAsDialog.Title = "Save as project (.pjt) file";
            saveAsDialog.InitialDirectory = "D:\\";

            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveAsDialog.FileName != "")
                {
                    pjt.Save(saveAsDialog.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string confirmationMessage = "Are you sure you want to overwrite existing file?";
            DialogResult confirmSaveResult = MessageBox.Show(confirmationMessage, "Overwrite file", MessageBoxButtons.YesNo);

            if (confirmSaveResult == DialogResult.Yes)
            // Save over current file
            { pjt.Save(pjt.Filename); }
            else 
            { }
            
        }
    }
}
