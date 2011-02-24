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
    public partial class SensorControl : Form
    {
        VideoTemplate Video;
        private int ChanNum;
        public SensorControl(int Chan)
        {
            InitializeComponent();
            Video = VideoTemplate.Instance;
            ChanNum = Chan;
            IDS_CONTRAST.Value = Video.Contrast[Chan];
            IDS_BRIGHTNESS.Value = Video.Brightness[Chan];
            IDS_HUE.Value = Video.Hue[Chan];
            IDS_SATURATION.Value = Video.Saturation[Chan];
            IDT_HUE.Text = IDS_HUE.Value.ToString();
            IDT_CONTRAST.Text = IDS_CONTRAST.Value.ToString();
            IDT_BRIGHTNESS.Text = IDS_BRIGHTNESS.Value.ToString();
            IDT_SATURATION.Text = IDS_SATURATION.Value.ToString();
        }        
        private void IDS_CONTRAST_Scroll(object sender, EventArgs e)
        {
            IDT_CONTRAST.Text = IDS_CONTRAST.Value.ToString();
        }

        private void IDS_BRIGHTNESS_Scroll(object sender, EventArgs e)
        {
            IDT_BRIGHTNESS.Text = IDS_BRIGHTNESS.Value.ToString();
        }

        private void IDS_SATURATION_Scroll(object sender, EventArgs e)
        {
            IDT_SATURATION.Text = IDS_SATURATION.Value.ToString();
        }

        private void IDS_HUE_Scroll(object sender, EventArgs e)
        {
            IDT_HUE.Text = IDS_HUE.Value.ToString();
        }
    }
}
