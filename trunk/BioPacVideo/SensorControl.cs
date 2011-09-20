using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class SensorControl : Form
    {
        VideoTemplate Video;
        private int Chan = 0;
        Graphics g;
        IntPtr pDF;        
        private Bitmap Still;
        Thread ThreadDisplay;
        public SensorControl()
        {
            InitializeComponent();
            Video = VideoTemplate.Instance;
            g = this.CreateGraphics();
            for (int i = 0; i < 32; i++)
            {
                ChanSelect.Items.Add("Camera " + i + 1.ToString());
            }
            ChanSelect.SelectedIndex = 0;
            IDS_CONTRAST.Value = Video.Contrast[Chan];
            IDS_BRIGHTNESS.Value = Video.Brightness[Chan];
            IDS_HUE.Value = Video.Hue[Chan];
            IDS_SATURATION.Value = Video.Saturation[Chan];
            IDT_HUE.Text = IDS_HUE.Value.ToString();
            IDT_CONTRAST.Text = IDS_CONTRAST.Value.ToString();
            IDT_BRIGHTNESS.Text = IDS_BRIGHTNESS.Value.ToString();
            IDT_SATURATION.Text = IDS_SATURATION.Value.ToString();
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
        }
        private void DisplayThread()
        {
            while (this.Visible)
            {
                Thread.Sleep(30);
                pDF = VideoWrapper.GetCurrentBuffer(Chan);
                if (pDF != null)
                {
                    Still = new Bitmap(Video.XRes, Video.YRes, Video.XRes * 3, PixelFormat.Format24bppRgb, pDF);
                    Still.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                if (this.Visible)
                    g.DrawImage(Still, 550, 20, 320, 240);
                Still.Dispose();
            }
        }
        private void IDS_CONTRAST_Scroll(object sender, EventArgs e)
        {
            IDT_CONTRAST.Text = IDS_CONTRAST.Value.ToString();
            Video.Contrast[Chan] = IDS_CONTRAST.Value;
            VideoWrapper.SetContrast(Chan, IDS_CONTRAST.Value);
        }

        private void IDS_BRIGHTNESS_Scroll(object sender, EventArgs e)
        {
            IDT_BRIGHTNESS.Text = IDS_BRIGHTNESS.Value.ToString();
            Video.Brightness[Chan] = IDS_BRIGHTNESS.Value;
            VideoWrapper.SetBrightness(Chan, IDS_BRIGHTNESS.Value);
        }

        private void IDS_SATURATION_Scroll(object sender, EventArgs e)
        {
            IDT_SATURATION.Text = IDS_SATURATION.Value.ToString();
            Video.Saturation[Chan] = IDS_SATURATION.Value;
            VideoWrapper.SetSaturation(Chan, IDS_SATURATION.Value);
        }

        private void IDS_HUE_Scroll(object sender, EventArgs e)
        {
            IDT_HUE.Text = IDS_HUE.Value.ToString();
            Video.Hue[Chan] = IDS_HUE.Value;
            VideoWrapper.SetHue(Chan, IDS_HUE.Value);
        }

        private void IDB_OK_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chansel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chan = ChanSelect.SelectedIndex;
            IDS_CONTRAST.Value = Video.Contrast[Chan];
            IDS_BRIGHTNESS.Value = Video.Brightness[Chan];
            IDS_HUE.Value = Video.Hue[Chan];
            IDS_SATURATION.Value = Video.Saturation[Chan];
            IDT_HUE.Text = IDS_HUE.Value.ToString();
            IDT_CONTRAST.Text = IDS_CONTRAST.Value.ToString();
            IDT_BRIGHTNESS.Text = IDS_BRIGHTNESS.Value.ToString();
            IDT_SATURATION.Text = IDS_SATURATION.Value.ToString();
        }

        private void SensorControl_Load(object sender, EventArgs e)
        {

        }
        
    }
}
