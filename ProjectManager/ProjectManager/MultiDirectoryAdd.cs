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

    public partial class MultiDirectoryAdd : Form
    {        
        public bool Pass;
        public string[] DirReturn;
        public MultiDirectoryAdd()
        {
            InitializeComponent();
            DriveInfo[]  F = DriveInfo.GetDrives();
            for (int i = 0; i < F.Length; i++)
            {
                Drives.Items.Add(F[i].Name);
            }
            Drives.SelectedIndex = 0;
            string[] MyDirs = Directory.GetDirectories(F[0].Name);
            for (int i = 0; i < MyDirs.Length; i++)
            {
                DirListBox.Items.Add(MyDirs[i]);
            }
        }

        private void OKbttn_Click(object sender, EventArgs e)
        {
            if (DirListBox.SelectedIndex == -1)
            {
                Pass = false;
                this.Close();
            }
            Pass = true;
            DirReturn = new string[DirListBox.SelectedItems.Count];
            for (int i = 0; i < DirListBox.SelectedItems.Count; i++)
            {
                DirReturn[i] = (string)DirListBox.SelectedItems[i];
            }
            this.Close();
        }

        private void DirListBox_DoubleClick(object sender, EventArgs e)
        {
            string Path = (string)DirListBox.Items[DirListBox.SelectedIndex];
            string[] MyDirs = Directory.GetDirectories(Path);
            DirListBox.Items.Clear();
            for (int i = 0; i < MyDirs.Length; i++)
            {
                DirListBox.Items.Add(MyDirs[i]);
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
            { return;  }
            DirListBox.Items.Clear();
            for (int i = 0; i < MyDirs.Length; i++)
            {
                DirListBox.Items.Add(MyDirs[i]);
            }

        }
    }
}
