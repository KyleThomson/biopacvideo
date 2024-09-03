using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace BioPacVideo
{
    public partial class About : Form
    {
        #region Lifecycle
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            pictureBoxBPVLogo.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BPVIcon.jpg"));
            labelVersionNumber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion
    }
}
