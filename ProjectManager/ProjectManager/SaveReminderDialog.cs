using System;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class SaveReminderDialog : Form
    {
        public int clickedOption;
        public SaveReminderDialog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SaveReminderDialog_Load(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            clickedOption = 0;
            Close();
        }

        private void DontSave_Click(object sender, EventArgs e)
        {
            clickedOption = 1;
            Close();
        }

        private void CancelClose_Click(object sender, EventArgs e)
        {
            clickedOption = 2;
            Close();
        }
    }
}
