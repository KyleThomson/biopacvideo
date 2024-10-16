﻿using System;
using System.IO;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class ProjectManager : Form
    {
        public Project pjt;
        public PMEEGView EEGFrm;
        bool _pjtOpened = false;
        public ProjectManager()
        {
            InitializeComponent();
            MainSelect.SelectedIndex = 0;
            Console.WriteLine(Properties.Settings.Default.DefaultDir);
            if (!Directory.Exists(Properties.Settings.Default.DefaultDir))
            {
                Properties.Settings.Default.DefaultDir = "C:\\";
            }

            //pjt = new Project("");
            // Handle event for form closing in case there are unsaved changes to project file.
            //FormClosing += (sender, e) => { ProjectManager_FormClosing(sender, e); };
        }
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog F = new SaveFileDialog();
            F.DefaultExt = ".pjt";
            F.Filter = "Text files (*.pjt)|*.pjt|All files (*.*)|*.*";
            F.InitialDirectory = Properties.Settings.Default.DefaultDir;
            if (F.ShowDialog() == DialogResult.OK)
            {


                char[] splitter = new char[] { '\\', '.' };
                string temp0 = F.FileName.Substring(0, (F.FileName.Length - 4));

                string[] temp = F.FileName.Split(splitter);

                Properties.Settings.Default.DefaultDir = temp[0] + "\\";
                Console.WriteLine(Properties.Settings.Default.DefaultDir);
                Directory.CreateDirectory(temp0);

                string FN = temp0 + "\\" + temp[temp.Length - 2];
                Console.WriteLine(FN);

                pjt = new Project(FN, true);
                pjt.pjtCreate();

                pjt.Open();
                _pjtOpened = true;
                if (pjt.CDatExists) eEGViewToolStripMenuItem.Enabled = true;
            }
            EnableFileTools();
            
        }
        private void ProjectManagerClosed(object sender, FormClosedEventArgs e)
        {

        }

        public void EnableFileTools()
        {
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            exportDataToolStripMenuItem.Enabled = true;
            mergeProjectToolStripMenuItem.Enabled = true;
            addMultipleDirectoriesToolStripMenuItem.Enabled = true;
            //importFileToolStripMenuItem.Enabled = true;
            importSeizureToolStripMenuItem.Enabled = true;

        }
        private void selectProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open file dialog to select project
            OpenFileDialog F = new OpenFileDialog();
            F.DefaultExt = ".pjt";
            F.Filter = "Project Files (*.pjt)|*.pjt";
            F.InitialDirectory = Properties.Settings.Default.DefaultDir;

            if (F.ShowDialog() == DialogResult.OK)
            {
                pjt = new Project(F.FileName, false);
                pjt.Open();
                ChangeTitleText(pjt.Filename);
                _pjtOpened = true;
                pjt.TrackAllAnimals();
                pjt.CompareStageConflicts(); // Find conflicts between bubble and notes
                pjt.analysis.DetermineTreatment(pjt.Animals);
                pjt.analysis.ParseGroups(pjt.Animals);
                if (pjt.BBigDAT != null) pjt.BBigDAT.Close();
                if (pjt.BigDAT != null) pjt.BigDAT.Close();
                UpdateMainList();
                EnableFileTools();
                if (pjt.CDatExists) eEGViewToolStripMenuItem.Enabled = true;
                checkDiscrepancy();
            }
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
            //MainSelect.SelectedIndex = 0;
        }
        private void UpdateSecondList()
        {
            if (pjt == null)
                return;
            SecondList.Items.Clear();
            if (MainList.SelectedIndex == -1) return; //No file selected

            if (MainSelect.SelectedIndex == 0) //Files
            {
                if (SecondSelect.SelectedIndex == 0)
                {
                    if (MainList.SelectedIndex >= 0)
                    {
                        SecondList.Items.Clear();
                        foreach (string A in pjt.Files[MainList.SelectedIndex].AnimalIDs)
                        {
                            SecondList.Items.Add(A);
                        }
                    }
                }
                else if (SecondSelect.SelectedIndex == 1)
                {
                    // reset list
                    SecondList.Items.Clear();

                    // call method to get array of strings for all seizures in selected file
                    string[] seizures = pjt.GetSeizuresInFile(pjt.Files[MainList.SelectedIndex]);

                    // populate secondlist with seizures from string array
                    for (int i = 0; i < seizures.Length; i++)
                        SecondList.Items.Add(seizures[i]);
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
                        string[] injections = pjt.Get_Injections((string)MainList.Items[MainList.SelectedIndex]);
                        for (int i = 0; i < injections.Length; i++)
                        {
                            SecondList.Items.Add(injections[i]);
                        }

                    }
                    else if (SecondSelect.SelectedIndex == 4)
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
            if (pjt == null)
            {
                pjt = new Project("", true);
            }
            //This function probably should not be used.
            if (pjt != null)
            {
                OpenFileDialog F = new OpenFileDialog();
                //F.Filter = "*.txt";            
                F.InitialDirectory = "C:\\";
                if (F.ShowDialog() == DialogResult.OK)
                {
                    string[] SZFile = Directory.GetFiles(F.FileName + "\\Seizure", "*.txt");
                    if (SZFile[0] != null)
                    {
                        //  File.Copy(F.FileName, pjt.P + "\\Data\\" + Path.GetFileName(F.FileName));
                        pjt.ImportSzFile(F.FileName, F.FileName + "\\Seizure\\", false);
                    }
                    else
                    {

                    }
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
            if (pjt == null)
            {
                pjt = new Project("", false);
            }
            if (pjt != null)
            {
                var moveVid = MessageBox.Show("You must import videos to view them, would you like to import videos?", "Video Importer", MessageBoxButtons.YesNo);
                FolderBrowserDialog F = new FolderBrowserDialog();
                if (F.ShowDialog(this) == DialogResult.OK)
                {
                    int result = pjt.ImportDirectory(F.SelectedPath, this.rejectUnreviewedFilesToolStripMenuItem.Checked, moveVid == DialogResult.Yes);
                    if (result == 2)
                    {
                        MessageBox.Show("File already imported", "ERROR");
                    }
                    pjt.FileChanged();
                    ChangeTitleText(pjt.Filename);
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
                    var edit = rightClickMenu.Items.Add("Edit");
                    SecondList.ContextMenuStrip = rightClickMenu;
                    SecondList.SelectedIndex = item;
                    rightClickMenu.Show(SecondList, e.Location);
                    rightClickMenu.AutoClose = true;

                    // call event handler to delete treatment
                    deleteTrt.Click += new EventHandler(delete_secondlist);
                    edit.Click += new EventHandler(edit_secondlist);
                }
            }
        }
        private void edit_secondlist(object sender, EventArgs e)
        {
            // Seizures
            if (SecondSelect.SelectedIndex == 0)
            {
                // create seizureedits dialog
                SeizureType currentSeizure =
                    pjt.Animals[MainList.SelectedIndex].Sz[SecondList.SelectedIndex];
                SeizureEdits szEditWindow = new SeizureEdits(currentSeizure);
                szEditWindow.ShowDialog();
                if (szEditWindow._seizureModified)
                {
                    // indicate file modified
                    pjt.FileChanged();
                    ChangeTitleText(pjt.Filename);
                }

            }
            // Weights
            else if (SecondSelect.SelectedIndex == 1)
            {

            }
            // Meal
            else if (SecondSelect.SelectedIndex == 2)
            {

            }
            // Injections
            else if (SecondSelect.SelectedIndex == 3)
            {
                // injectionsedits dialog
                InjectionType currentInection =
                    pjt.Animals[MainList.SelectedIndex].Injections[SecondList.SelectedIndex];
                InjectionEdits injectionEdits = new InjectionEdits(currentInection);
                injectionEdits.ShowDialog();
                if (injectionEdits._anyChanges)
                {
                    // indicate file modified
                    pjt.FileChanged();
                    ChangeTitleText(pjt.Filename);
                }
            }
            UpdateSecondList();
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
            else if (SecondSelect.SelectedIndex == 3) // Injections selected
            {
                // block of code to remove injection from file
                pjt.Animals[MainList.SelectedIndex].Injections.RemoveAt(SecondList.SelectedIndex);
                SecondList.Items.RemoveAt(SecondList.SelectedIndex);
            }
            // indicate file modified
            pjt.FileChanged();
            ChangeTitleText(pjt.Filename);
        }
        private void delete_Click(object sender, EventArgs e)
        {
            var index = MainList.SelectedIndex;
            MainList.Items.RemoveAt(index);
            if (MainSelect.SelectedIndex == 1) // operate on animals
            {
                pjt.Animals.RemoveAt(index);
            }
            else if (MainSelect.SelectedIndex == 0) // operate on files
            {
                pjt.Files.RemoveAt(index);
            }
            // indicate file modified
            pjt.FileChanged();
            ChangeTitleText(pjt.Filename);
        }
        private void MainSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pjt == null)
                return;
            if (MainSelect.SelectedIndex == 0)
            {
                SecondSelect.Items.Clear();
                SecondSelect.Items.Add("Animals");
                SecondSelect.Items.Add("Seizures");
                SecondSelect.SelectedIndex = 0;
            }
            else if (MainSelect.SelectedIndex == 1)
            {
                SecondSelect.Items.Clear();
                SecondSelect.Items.Add("Seizures");
                SecondSelect.Items.Add("Weights");
                SecondSelect.Items.Add("Meals");
                SecondSelect.Items.Add("Injections");
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
            {
                // passing empty string to GetPathName() returns empty string
                pjt = new Project("", true);

            }
            int DuplicateDirectoryCount = 0;
            int SuccessfullyImportedDirectory = 0;
            int NotImported = 0;
            MultiDirectoryAdd Frm = new MultiDirectoryAdd();
            Frm.ShowDialog();
            if (Frm.Pass)
            {
                DirectoryLoadBar.Visible = true;
                DirectoryLoadBar.Value = 0;
                DirectoryLoadBar.Maximum = Frm.DirReturn.Length * 2;
                DirectoryLoadText.Visible = true;
                DirectoryLoadText.Text = $"Loading: 1 / {Frm.DirReturn.Length} (Please Wait!)";
                
                var moveVid = MessageBox.Show("You must import videos to view them, would you like to import videos?", "Video Importer", MessageBoxButtons.YesNo);
                //BigDAT = new StreamReader
                for (int i = 0; i < Frm.DirReturn.Length; i++)
                {
                    DirectoryLoadText.Text = $"Loading: {i + 1} / {Frm.DirReturn.Length} (Please Wait!)";
                    if (DirectoryLoadBar.Value == DirectoryLoadBar.Maximum) DirectoryLoadBar.Value -= 2;
                    DirectoryLoadBar.Value++;
                    //  File.Copy(F.FileName, pjt.P + "\\Data\\" + Path.GetFileName(F.FileName));
                    int result = pjt.ImportDirectory(Frm.DirReturn[i], this.rejectUnreviewedFilesToolStripMenuItem.Checked, moveVid == DialogResult.Yes);
                    if (result == 2)
                    {
                        // User elected to not include file during import
                        NotImported++;
                    }
                    else if (result == 1)
                    {
                        // User tried adding directory that was already in .pjt file
                        DuplicateDirectoryCount++;
                    }
                    else if (result == 0)
                    {
                        // The file was imported into the current .pjt file
                        SuccessfullyImportedDirectory++;
                    }
                    DirectoryLoadBar.Value++;
                }
                DirectoryLoadBar.Visible = false;
                DirectoryLoadText.Visible = false;
                DirectoryLoadBar.Value = 0;
                DirectoryLoadText.Text = "";
                pjt.FileChanged();

                //show total number of DATS in BigDAT

                pjt.BBigDAT.Close();
                pjt.BigDAT.Close();

                FileStream tempF = new FileStream(pjt.CDatName, FileMode.Open, FileAccess.ReadWrite);
                BinaryWriter tempB = new BinaryWriter(tempF);

                tempF.Position = 0;

                Int32 tDat = pjt.DatCount;
                tempB.Write(tDat);
                tempB.Close();
                tempF.Close();
                pjt.Save(pjt.Filename);
                pjt.CompareStageConflicts();

                ChangeTitleText(pjt.Filename);
            }
            UpdateMainList();

            //MessageBox newDir = new MessageBox;

            // inform user of import results
            Info.Text = SuccessfullyImportedDirectory.ToString() + " directories imported. " +
                DuplicateDirectoryCount.ToString() + " duplicate directories skipped. " +
                NotImported.ToString() + " directories not imported."; ;
            if (pjt.CDatExists) eEGViewToolStripMenuItem.Enabled = true;

            checkDiscrepancy();
            

            


        }

        public bool checkDiscrepancy()
        {
            if (pjt.discrepancyList.Count > 0)
            {
                DialogResult result = MessageBox.Show("Discrepancies were detected between Racine Scores in one or more files, and must be fixed to maintain accuracy in data. \n \n \t Would you like to fix these now?", "PLEASE FIX SCORES", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (var DiscForm = new DiscrepancyFixForm(pjt.discrepancyList, pjt.Animals, Directory.GetParent(pjt.Filename).ToString() + "\\Videos\\", pjt.CDatName))
                    {
                        var temp = DiscForm.ShowDialog();
                        if (temp == DialogResult.OK)
                        {
                            Console.WriteLine(DiscForm.RetTest1);
                        }
                    }


                    return false;
                }
                else
                {
                    DiscrepButton.Show();

                    return true;
                }
            } else
            {
                return false;
            }
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
        private void ProjectManager_Load(object sender, EventArgs e)
        {

        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save .pjt file as -
            pjt.SaveAs();
            ChangeTitleText(pjt.Filename);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pjt is null) return;
            if (pjt.Filename != "")
            {
                // Save over current file
                pjt.Save(pjt.Filename); ChangeTitleText(pjt.Filename);
            }
            else
            {
                pjt.SaveAs();
                ChangeTitleText(pjt.Filename);
            }

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ProjectManager_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            SaveReminderDialog saveReminderDialog = new SaveReminderDialog();
            if (!_pjtOpened) return;
            // first check for default value
            if (pjt._fileChanged != default)
            {
                // Check file changed flag in project data
                if (pjt._fileChanged)
                {
                    // Make sound to annoy user
                    System.Media.SystemSounds.Exclamation.Play();

                    // Open dialog to remind user to save
                    saveReminderDialog.ShowDialog();

                    // handle options that user selected
                    if (saveReminderDialog.clickedOption == 0)
                    {
                        // User selected save
                        if (pjt.Filename != "")
                        // normal save if filename is not empty
                        { pjt.Save(pjt.Filename); }
                        else
                        // Save As
                        { pjt.SaveAs(); }

                    }
                    else if (saveReminderDialog.clickedOption == 2)
                    // User selected cancel. DON'T CLOSE FORM!
                    { e.Cancel = true; }
                    // else: selected don't save, do nothing so form closes
                }
            }
        }
        private void ChangeTitleText(string path)
        {
            // Change form title bar to match current file opened
            string title;
            // extract filename
            string filename = Path.GetFileName(path);
            if (filename != "")
                title = "Project Manager - " + filename;
            else
                title = "Project Manager - Untitled";

            // check if file has been changed
            if (pjt._fileChanged)
                // add asterik to indicate that file has unsaved changes
                title += "*";

            // set new title
            Text = title;
        }

        private void test35ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // user selected Test 35
            pjt.analysis.test = TESTTYPES.T35;
            pjt.TestSort();              // Sort test, return if test is undefined
            pjt.Analysis();
            GenerateTest();
        }

        private void test36ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // user selected Test 36
            pjt.analysis.test = TESTTYPES.T36;
            pjt.TestSort();              // Sort test, return if test is undefined
            pjt.Analysis();
            GenerateTest();
        }

        private void iAKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // user selected IAK Test
            pjt.analysis.test = TESTTYPES.IAK;
            pjt.TestSort();              // Sort test, return if test is undefined
            pjt.Analysis();
            GenerateTest();
        }
        private void GenerateTest()
        {
            SzGraph Test = new SzGraph(pjt);
            Test.DrawGraph();
        }

        private void rejectUnreviewedFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void successfulImport(object sender, EventArgs e)
        {

        }

        private void alreadyImport_Click(object sender, EventArgs e)
        {

        }

        private void notImported_Click(object sender, EventArgs e)
        {

        }

        private void eEGViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pjtOpened)
            {
                if (pjt.DatCount <= 0) return;
                EEGFrm = new PMEEGView(this);
                //EEGFrm.Show();
                //EEGFrm.UpdateDisplay();


            }
        }

        public void eEGViewDispose()
        {

            if (EEGFrm == null) return;

            EEGFrm.Dispose();
            

        }


    }
}
