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
            for (int i = 0; i < 32; i++)
            {
                ChanSel.Items.Add("Camera " + i + 1.ToString());
            }
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
            int F, P;
            if (int.TryParse(FeederNum.Text, out F) & int.TryParse(PelletsNum.Text, out P))
            {
                Feeder.AddCommand((byte)F, (byte)P);
                Feeder.Execute();
            }
        }
        public void Kill()
        {
            ThreadDisplay.Abort();
        }
        private void FeederTester_Load(object sender, EventArgs e)
        {

        }

        private void ChanSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chan = ChanSel.SelectedIndex;
        }
    }
}
