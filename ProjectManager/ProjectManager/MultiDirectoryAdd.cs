using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ProjectManager
{

    public partial class MultiDirectoryAdd : Form
    {
        public bool Pass;
        public string[] DirReturn;
        public MultiDirectoryAdd()
        {
            InitializeComponent();
            DriveInfo[] F = DriveInfo.GetDrives();
            for (int i = 0; i < F.Length; i++)
            {
                Drives.Items.Add(F[i].Name);
            }
            Drives.SelectedIndex = 0;
            string[] MyDirs = Directory.GetDirectories(F[0].Name);
            for (int i = 0; i < MyDirs.Length; i++)
            {
                //DirListBox.Items.Add(MyDirs[i]);
                DirListView.Items.Add(MyDirs[i]);
                

            }

            
        }

        private void OKbttn_Click(object sender, EventArgs e)
        {
            
            //if (DirListBox.SelectedIndex == -1)
            //{
            //    Pass = false;
            //    this.Close();
            //}
            if (DirListView.SelectedIndices.Count <= 0)
            {
                Pass = false;
                this.Close();
            }
            Pass = true;
            //DirReturn = new string[DirListBox.SelectedItems.Count];
            DirReturn = new string[DirListView.SelectedItems.Count];
            //for (int i = 0; i < DirListBox.SelectedItems.Count; i++)
            //{
            //    DirReturn[i] = (string)DirListBox.SelectedItems[i];



            //}
            for (int i = 0; i < DirListView.SelectedItems.Count; i++)
            {
                DirReturn[i] = DirListView.SelectedItems[i].SubItems[0].Text;


            }

            this.Close();
        }

        private void DirListBox_DoubleClick(object sender, EventArgs e)
        {
            
            string Path = (string)DirListView.SelectedItems[0].SubItems[0].Text;
            string[] MyDirs = Directory.GetDirectories(Path);
            
            //DirListBox.Items.Clear();
            
            DirListView.Items.Clear();
            Font font = new Font("Microsoft Sans Seriff", 8);
            for (int i = 0; i < MyDirs.Length; i++)
            {
                int PerComp = -1;
                
                try
                {
                    string[] FName = Directory.GetFiles(MyDirs[i], "*.acq");
                    PerComp = Check_Completion(FName, MyDirs[i]);
                }
                catch (System.UnauthorizedAccessException a) { Console.WriteLine("Can't Access: " + Path); };

                if (PerComp == -1)
                {
                    //DirListBox.Items.Add(MyDirs[i]);
                    
                    

                    ListViewItem li = new ListViewItem();
                    
                    li.Text = MyDirs[i];
                    li.SubItems.Add(" ");
                    DirListView.Items.Add(li);

                } else
                {
                    //DirListBox.Items.Add(MyDirs[i]);
                    
                    

                    ListViewItem li = new ListViewItem();
                    //li.BackColor = Color.White;
                    li.Text = MyDirs[i];
                    li.SubItems.Add(PerComp.ToString());
                    
                    
                    DirListView.Items.Add(li);
                    DirListView.Items[i].UseItemStyleForSubItems = false;
                    if (PerComp >= 100) DirListView.Items[i].SubItems[1].BackColor = Color.LightGreen;
                    if (PerComp <= 60) DirListView.Items[i].SubItems[1].BackColor = Color.PaleVioletRed;

                    //DirListView.Items[i].BackColor = Color.White;


                    //DirListView.Items[i].BackColor = Color.Green;

                }
            }

        }

        private void CnclButton_Click(object sender, EventArgs e)
        {
            Pass = false;
            this.Close();
        }

        private void DirListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Drives_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] MyDirs;
            try
            {
                MyDirs = Directory.GetDirectories((string)Drives.Items[Drives.SelectedIndex]);
            }
            catch
            { return; }
            //DirListBox.Items.Clear();
            
            DirListView.Items.Clear();
            for (int i = 0; i < MyDirs.Length; i++)
            {
                int PerComp = -1;

                try
                {
                    string[] FName = Directory.GetFiles(MyDirs[i], "*.acq");
                    PerComp = Check_Completion(FName, MyDirs[i]);
                }
                catch (System.UnauthorizedAccessException a) { Console.WriteLine("Can't Access: "); };

                if (PerComp == -1)
                {
                    //DirListBox.Items.Add(MyDirs[i]);
                    


                    ListViewItem li = new ListViewItem();

                    li.Text = MyDirs[i];
                    li.SubItems.Add(" ");
                    DirListView.Items.Add(li);

                }
                else
                {
                    //DirListBox.Items.Add(MyDirs[i]);
                   


                    ListViewItem li = new ListViewItem();
                    //li.BackColor = Color.White;
                    li.Text = MyDirs[i];
                    li.SubItems.Add(PerComp.ToString());


                    DirListView.Items.Add(li);
                    DirListView.Items[i].UseItemStyleForSubItems = false;
                    if (PerComp >= 100) DirListView.Items[i].SubItems[1].BackColor = Color.LightGreen;
                    if (PerComp <= 60) DirListView.Items[i].SubItems[1].BackColor = Color.PaleVioletRed;

                    //DirListView.Items[i].BackColor = Color.White;


                    //DirListView.Items[i].BackColor = Color.Green;

                }
            }

        }

        public int Check_Completion(string[] FName, string MyDirs)
        {
            if (FName.Length > 0)
            {
                String[] IniFiles = Directory.GetFiles(MyDirs, "*_Settings.txt");
                IniFile BioINI = new IniFile(IniFiles[0]);
                double PercentCompletion = BioINI.IniReadValue("Review", "Complete", (double)0);
                Console.WriteLine(MyDirs + " is " + PercentCompletion);
                return (int)Math.Round(PercentCompletion);
            }
            else return -1;
        }

     
    }
}
