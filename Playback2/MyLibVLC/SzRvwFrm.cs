using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SeizurePlayback
{
    public partial class SzRvwFrm : Form
    {
        CManage Prnt;
        bool SuppressChange;
        int lastSort = -1;
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

        public void Add(string[] TmpStr)
        {
            ListViewItem li = new ListViewItem(TmpStr);
            
            if (SzListView.Items.Count % 2 == 0)
            {
                li.BackColor = Color.LightGray;
            }
            else li.BackColor = Color.White;
            SzListView.Items.Add(li);
            

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

        private void SzRvwFrm_Load(object sender, EventArgs e)
        {

        }

        private void SzListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString() + " | " + e);
        }

        private void SzListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Console.WriteLine(sender.ToString() + " | " + e.Column);
            bool temp = false;
            if (lastSort == e.Column)
            {
                temp = true;
                lastSort = -1;
            } else lastSort = e.Column;
            
               
            this.SzListView.ListViewItemSorter = new ListViewItemComparer(e.Column, temp);
           
        }
    }

    class ListViewItemComparer : IComparer
    {
        private int col;
        private bool flip;
        public ListViewItemComparer()
        {
            col = 0;
            flip = false;
        }
        public ListViewItemComparer(int column, bool fl)
        {
            col = column;
            flip = fl;
        }
        public int Compare(object x, object y)
        {
            int r = 10;
            if (col == 0 || col == 2 || col == 4)
            {
                if (int.Parse(((ListViewItem)x).SubItems[col].Text) > int.Parse(((ListViewItem)y).SubItems[col].Text))
                {
                    r = 1;
                } else if (int.Parse(((ListViewItem)x).SubItems[col].Text) == int.Parse(((ListViewItem)y).SubItems[col].Text))
                {
                    r = 0;
                } else if (int.Parse(((ListViewItem)x).SubItems[col].Text) < int.Parse(((ListViewItem)y).SubItems[col].Text))
                {
                    r = -1;
                }
                
            } else 
            {
                r = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }

            if (flip) r *= -1;
            return r;
            //Console.WriteLine("R Compare: " + r);
            //Console.WriteLine("With String Compare: " + String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text));
            
        }
    }
}
