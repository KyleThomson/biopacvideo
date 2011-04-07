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
        VideoTemplate Video;
        public VideoSettings()
        {
            InitializeComponent();
            Video = VideoTemplate.Instance;
            if (Video.XRes == 640)
            {
                V1.Checked = true;
            }
            else if (Video.XRes == 320)
            {
                V2.Checked = true;
            }
            else if (Video.XRes == 160)
            {
                V3.Checked = true;
            }
            IDT_KEYINT.Text = Video.KeyFrames.ToString();
            IDT_QUANT.Text = Video.Quant.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            int test;
            if (int.TryParse(IDT_KEYINT.Text, out test))
                Video.KeyFrames = test;
            if (int.TryParse(IDT_QUANT.Text, out test))
                Video.Quant = test;
            if (V1.Checked)
            {
                Video.XRes = 640;
                Video.YRes = 480;
            }
            else if (V2.Checked)
            {
                Video.XRes = 320;
                Video.YRes = 240;
            }
            else
            {
                Video.XRes = 160;
                Video.YRes = 120;
            }
            VideoWrapper.SetCaptureRes(Video.XRes, Video.YRes);
            this.Close();
        }
    }
}
