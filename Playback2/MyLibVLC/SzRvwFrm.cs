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

        public void Add(string[] TmpStr, int i)
        {
            ListViewItem li = new ListViewItem(TmpStr);
            li.SubItems.Add(i.ToString());
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
                //ListView.SelectedIndexCollection index = this.SzListView.SelectedIndices;
                //ListView.SelectedListViewItemCollection col = SzListView.SelectedItems;
                //Console.WriteLine(col[0].ToString());
                //Console.WriteLine(index[0].ToString());
               

                
                //if (SzBox.SelectedIndex != -1)
                if (SzListView.SelectedItems.Count > 0)
                {
                    TimeSpan t;
                    int C;
                    //string s = (string)SzBox.Items[SzBox.SelectedIndex];

                    //string[] SArray = s.Split(',');
                    //string[] SArray = col.ToString();

                    string s;

                    
                    
                    //s = SzListView.Items[index[0]].SubItems[3].Text;
                    s = SzListView.Items[SzListView.SelectedIndices[0]].SubItems[3].Text;
                    
                    
                    //TimeSpan.TryParse(SArray[3], out t);
                    TimeSpan.TryParse(s, out t);
                    Console.WriteLine("String: " + s + "\n TimeSpan: " + t);
                    s = SzListView.Items[SzListView.SelectedIndices[0]].SubItems[0].Text;
                    //int.TryParse(SArray[0], out C);
                    int.TryParse(s, out C);
                    Console.WriteLine("String: " + s + "\n Int: " + C);
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
            string s = "";
            foreach (ListViewItem.ListViewSubItem temp in SzListView.SelectedItems[0].SubItems)
            {
                s += temp.Text + ", ";
            }
            //DialogResult result = MessageBox.Show("Confirm Deletion of " + SzBox.Items[SzBox.SelectedIndex], "Are you sure?", MessageBoxButtons.OKCancel);
            DialogResult result = MessageBox.Show("Confirm Deletion of " + s, "Are you sure?", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //Prnt.DeleteSz(SzBox.SelectedIndex);
                //SzBox.Items.RemoveAt(SzBox.SelectedIndex);
                //SzBox.Refresh();
                Prnt.DeleteSz(int.Parse(SzListView.SelectedItems[0].SubItems[8].Text));
                Console.WriteLine(int.Parse(SzListView.SelectedItems[0].SubItems[8].Text));
                Console.WriteLine(SzListView.SelectedIndices[0]);

                SzListView.Items.RemoveAt(SzListView.SelectedIndices[0]);
                SzListView.Refresh();
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

        private void SzView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                

               
                    SuppressChange = true;
                    MenuItem[] Y = new MenuItem[2];
                    Y[0] = new MenuItem("Delete Seizure");
                    Y[0].Click += new System.EventHandler(DeleteSz);
                    Y[1] = new MenuItem("Edit Notes");
                    ContextMenu X = new ContextMenu(Y);
                    X.Show(SzListView, new Point(e.X, e.Y));
                
            }
        }

        private void SzRvwFrm_Load(object sender, EventArgs e)
        {

        }

        private void SzListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString() + " | " + e.ToString());
        }

        private void SzListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //Console.WriteLine(sender.ToString() + " | " + e.Column);
            bool temp = false;
            if (lastSort == e.Column)
            {
                temp = true;
                lastSort = -1;
            } else lastSort = e.Column;
            
               
            this.SzListView.ListViewItemSorter = new ListViewItemComparer(e.Column, temp);
            for (int i = 0; i < SzListView.Items.Count; i ++)
            {
                if (i % 2 == 0)
                {
                    SzListView.Items[i].BackColor = Color.LightGray;
                } else SzListView.Items[i].BackColor = Color.White;
            }
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
            int secSort = col;
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


            if (r == 0)
            {
                switch (col)
                {
                    case 0:
                        secSort = 2;
                        break;
                    case 1:
                        secSort = 2;
                        break;


                }
                if (col >= 2)
                {
                    secSort = 0;
                }

                if (int.Parse(((ListViewItem)x).SubItems[secSort].Text) == int.Parse(((ListViewItem)y).SubItems[secSort].Text))
                {
                    r = 0;
                }
                else if (int.Parse(((ListViewItem)x).SubItems[secSort].Text) > int.Parse(((ListViewItem)y).SubItems[secSort].Text))
                {
                    r = 1;
                } else
                {
                    r = -1;
                }
            }

            if (flip) r *= -1;
            return r;
            //Console.WriteLine("R Compare: " + r);
            //Console.WriteLine("With String Compare: " + String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text));
            
        }
    }
}
