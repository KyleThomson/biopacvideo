using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class SzRvwFrm : Form
    {
        CManage Prnt;
        bool SuppressChange;
        public SzRvwFrm(CManage sent)
        {
            InitializeComponent();
            Prnt = sent;
            SuppressChange = false;
        }
        public void Add(string s)
        {
            SzBox.Items.Add(s);   
        }
        private void SzBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SuppressChange)
            {
                if (SzBox.SelectedIndex != -1)
                {
                    TimeSpan t;
                    int C;
                    string s = (string)SzBox.Items[SzBox.SelectedIndex];
                    string[] SArray = s.Split(',');
                    TimeSpan.TryParse(SArray[3], out t);
                    int.TryParse(SArray[0], out C);
                    Prnt.ChildSend(t, C - 1);
                }
            }
            else
            {
                SuppressChange = false; 
            }
        }
       public void DeleteSz(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Confirm Deletion of " + SzBox.Items[SzBox.SelectedIndex], "Are you sure?", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Prnt.DeleteSz(SzBox.SelectedIndex);
                SzBox.Items.RemoveAt(SzBox.SelectedIndex);
                SzBox.Refresh();
            }
        }
        private void EditNotes()
        {

        }
        private void SzBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int indexOfItem;
                indexOfItem = SzBox.IndexFromPoint(e.X, e.Y); //(2)
                if (indexOfItem < SzBox.Items.Count)
                {
                    SuppressChange = true;
                    SzBox.SelectedIndex = indexOfItem;
                    MenuItem[] Y = new MenuItem[2];
                    Y[0] = new MenuItem("Delete Seizure");
                    Y[0].Click += new System.EventHandler(DeleteSz);
                    Y[1] = new MenuItem("Edit Notes");
                    ContextMenu X = new ContextMenu(Y);
                    X.Show(SzBox, new Point(e.X, e.Y));
                } 
            }
        }
    }
}
