using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class FeederErrorBox : Form
    {
        public FeederErrorBox()
        {
            InitializeComponent();
        }

        public void Add_Error(string ErrorText)
        {
            if (this.Visible == false)
                this.Show();             
            string value = DateTime.Now.ToString("HH:mm");
            ErrorList.Text += ErrorText + value + "\r\n";            
        }
        public void Add_Status(string StatusText)
        {
            string value = DateTime.Now.ToString("HH:mm");
            StatusList.Text += StatusText + value + "\r\n";
        }
        private void Dismiss_Click(object sender, EventArgs e)
        {
            ErrorList.Text = "";
            StatusList.Text = "";
            this.Hide();
        }
    }
}
