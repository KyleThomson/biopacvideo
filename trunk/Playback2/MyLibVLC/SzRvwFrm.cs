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
        MainForm Prnt;
        public SzRvwFrm(MainForm sent)
        {
            InitializeComponent();
            Prnt = sent;
        }
        public void Add(string s)
        {
            SzBox.Items.Add(s);   
        }
        private void SzBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeSpan t; 
            int C;
            string s = (string)SzBox.Items[SzBox.SelectedIndex];
            string [] SArray = s.Split(',');            
            TimeSpan.TryParse(SArray[3], out t);
            int.TryParse(SArray[0], out C);
            Prnt.ChildSend(t, C-1);
        }
    }
}
