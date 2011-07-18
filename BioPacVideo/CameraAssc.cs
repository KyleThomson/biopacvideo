using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;

namespace BioPacVideo
{
    public partial class CameraAssc : Form
    {
        int Cam;
        IntPtr pDF;
        private VideoTemplate Video;
        private Bitmap Still;
        Thread ThreadDisplay;
        Graphics g;
        public CameraAssc()
        {
            
            InitializeComponent();
            Video = VideoTemplate.Instance;
            g = this.CreateGraphics();
            Cam = 0;
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            for (int i = 0; i < 16; i++)
                ChanSel.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < Video.Device_Count * 4; i++)
                CamSel.Items.Add("Camera " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                CABox.Text += "Channel " + (i + 1).ToString() + " - Camera " + (Video.CameraAssociation[i]+1).ToString() + Environment.NewLine;
            ChanSel.SelectedIndex = 0;
            CamSel.SelectedIndex = Video.CameraAssociation[0];

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cam = CamSel.SelectedIndex;
        }
        private void DisplayThread()
        {
            while (this.Visible)
            {
                Thread.Sleep(30);
                pDF = VideoWrapper.GetCurrentBuffer(Cam);                
                if (pDF != null)
                {
                    Still = new Bitmap(Video.XRes, Video.YRes, Video.XRes * 3, PixelFormat.Format24bppRgb, pDF);
                    Still.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }            
                if (this.Visible)
                    g.DrawImage(Still,200,20, 640,480);
                Still.Dispose();
            }
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            Video.CameraAssociation[ChanSel.SelectedIndex] = CamSel.SelectedIndex;
            CABox.Text = "";
            for (int i = 0; i < 16; i++)
                CABox.Text += "Channel " + (i + 1).ToString() + " - Camera " + (Video.CameraAssociation[i] + 1).ToString() + Environment.NewLine;
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
