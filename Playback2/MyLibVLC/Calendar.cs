using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class Calendar : Form
    {
        DateTime D;
        Bitmap B;
        int BoxWidth;
        int BoxHeight;
        Graphics g;
        public Calendar()
        {
            InitializeComponent();
            D = DateTime.Now;
            B = new Bitmap(panel1.Width,panel1.Height);
            g = Graphics.FromImage(B);
            
        }

        private void Calendar_Load(object sender, EventArgs e)
        {

        }
    }
}
