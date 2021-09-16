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
    public partial class VideoSettings : Form
    {
        int BitRate;
        int Quality; 
        VideoWrapper Video = VideoWrapper.Instance; 
        public VideoSettings()
        {
            InitializeComponent();

            QualityBar.Value = Video.Quality; 
            trackBar1.Value = Video.Bitrate; 
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BitRate = trackBar1.Value;
            label4.Text = (BitRate * 1024 * 1024).ToString(); 
        }

        private void QualityBar_Scroll(object sender, EventArgs e)
        {
            Quality = QualityBar.Value;
            label2.Text = Quality.ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Video.Quality = Quality;
            Video.Bitrate = BitRate;
            this.Close();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
        }
    }
}
