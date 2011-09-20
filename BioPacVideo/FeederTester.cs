using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class FeederTester : Form
    {
        ArrayList TestBoxes;
        MPTemplate MP;
        FeederTemplate Feeder;
        VideoTemplate Video;        
        Graphics g;
        IntPtr pDF;
        int Chan = 0;
        private Bitmap Still;
        Thread ThreadDisplay;
        public FeederTester()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
            Video = VideoTemplate.Instance;
            Feeder = FeederTemplate.Instance;
            g = this.CreateGraphics();
            TestBoxes = new ArrayList();
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
           
        }
        private void DisplayThread()
        {  
            while (true)
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

        private void IDC_RUNTEST_Click(object sender, EventArgs e)
        {
            Feeder.AddCommand(Convert.ToInt32(FeederNum.Text), Convert.ToInt32(PelletsNum.Text));
            Feeder.Execute();
        }
        public void Kill()
        {
            ThreadDisplay.Abort();
        }
        private void FeederTester_Load(object sender, EventArgs e)
        {

        }
    }
}
