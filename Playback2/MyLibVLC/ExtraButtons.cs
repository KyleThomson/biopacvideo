using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class ExtraButtons : Form
    {
        private CManage mainFormContext;
        public ExtraButtons(CManage cManage)
        {
            mainFormContext = cManage;
            InitializeComponent();
        }

        private void ExtraButtons_Load(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            mainFormContext.NotesButton_Click();
        }

        private void compressionManager_Click(object sender, EventArgs e)
        {
            mainFormContext.CompressionManager();
        }

        private void VideoCreate_Click(object sender, EventArgs e)
        {
            mainFormContext.VideoCreate_Click();
        }

        private void Download_ACQ_Click(object sender, EventArgs e)
        {
            mainFormContext.Download_ACQ_Click();
        }

        private void Renamer_Click(object sender, EventArgs e)
        {
            mainFormContext.Renamer_Click();
        }

        private void FixChan_Click(object sender, EventArgs e)
        {
            mainFormContext.FixChan_Click();
        }

        private void CompressFinish_Click(object sender, EventArgs e)
        {
            mainFormContext.CompressFinish_Click();
        }
    }
}
