using System;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class AddtoProject : Form
    {
        public bool passed;
        public string returnstring;
        public AddtoProject(string Filename)
        {
            InitializeComponent();
            passed = false;
            textBox1.Text = Filename;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.CheckFileExists = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

            returnstring = textBox1.Text;
            passed = true;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            passed = false;
            this.Close();
        }





    }
}
