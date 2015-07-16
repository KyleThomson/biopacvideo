using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SeizurePlayback
{
    public partial class VideoCreator : Form
    {
        Int32[] Data;
        Bitmap EEG;
        VlcMedia media;
        VlcMediaPlayer player;
        VlcInstance instance;
        Rectangle rect;
        Bitmap TempEEG;
        Bitmap VideoBMP;
        Graphics f;
        Graphics PanelGraph;
        Pen WavePen;
        float Zoom;      
        Bitmap CaptureBMP;
        MemoryRenderer Mem;
        IntPtr Handle;
        bool NewFrame;
        Thread CT;
        int cnt;
        bool Stop;
        int LiS;        
        public VideoCreator(Int32[] PassData, int LengthinSeconds, string CurrentAVI, long SeekTime)
        {
            InitializeComponent();
            Data = PassData;            
            Zoom = 0.15f;
            WavePen = new Pen(Color.Black);
            CaptureBMP = new Bitmap(CapPanel.Width, CapPanel.Height);
            VideoBMP = new Bitmap(640, 480);
            EEG = new Bitmap(950, 221);
            //Initalize Player instance;
            //string[] args = new string[] { "--no-directx-hw-yuv" }; //--no-overlay 
            string[] args = new string[] { "","--no-directx-hw-yuv" }; // 
            instance = new VlcInstance(args);     
            f = Graphics.FromImage(EEG);
            PanelGraph = CapPanel.CreateGraphics();            
            drawCapture(Data, f, 950, 221, 1);                                    
            LiS = LengthinSeconds;
            TempEEG = new Bitmap(EEG);
            EEGPanel.BackgroundImage = TempEEG;
            media = new VlcMedia(instance, CurrentAVI);
            rect = new Rectangle(0, 0, CapPanel.Width, CapPanel.Height);
            /*Rectangle rect = new Rectangle(0, 0, VideoBMP.Width, VideoBMP.Height);
            System.Drawing.Imaging.BitmapData bmpData = VideoBMP.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                VideoBMP.PixelFormat);
            IntPtr ptr = bmpData.Scan0;*/
            Handle = media.Handle;
            Mem = new MemoryRenderer(LibVlc.libvlc_media_player_new_from_media(media.Handle));
            Mem.SetFormat(new BitmapFormat(640,480, ChromaType.RV24));
            Mem.Play();
            Thread.Sleep(1000);
            Mem.seek(SeekTime);
            Mem.Pause();            
            //LibVlc.libvlc_media_player_pause(Handle);              
            
            

                
        }
        private void drawCapture(Int32[] TempData, Graphics f, int x, int y, int scalebar)
        {
            f.Clear(Color.White);
            PointF[] WaveC;
            WaveC = new PointF[TempData.Length];
            float InternalPointSpacing = (float)x / (float)TempData.Length;

            for (int i = 0; i < TempData.Length; i++)
            {

                PointF TempPoint = new PointF((float)i * InternalPointSpacing, 0.5F*y+ ScaleVoltsToPixel(Convert.ToSingle(TempData[i]), (float)y));
               WaveC[i] = TempPoint;
            }
            f.DrawLines(WavePen, WaveC);
        }
        private float ScaleVoltsToPixel(float volt, float pixelHeight)
        {
            float maxPixel = (pixelHeight * .15F);
            float minPixel = (pixelHeight * .95F);

            float m = (maxPixel - minPixel) / (65536);
            float b = 2 ^ 15;
            float result = ((m * volt) + b)*Zoom;
            //result = (result > maxPixel) ? maxPixel: result;
            //result = (result < minPixel) ? minPixel: result;
            return (result);
        }
        private void CreateVideo()
        {
            float Step = 950F/((float)LiS*30);            
            Pen LinePen = new Pen(Color.Red,3);
            for (int i = 1; i < LiS*30; i++)
            {
                
                TempEEG = new Bitmap(EEG);                
                f = Graphics.FromImage(TempEEG);
                f.DrawLine(LinePen, new Point((int)((float)i * Step), 0), new Point((int)((float)i * Step), 221));
                EEGPanel.BackgroundImage = TempEEG;
                VideoPanel.BackgroundImage = new Bitmap(string.Format("C:\\test\\a{0:D5}.png", i));
                //CapPanel.DrawToBitmap(CaptureBMP, rect);
                //CaptureBMP.Save(string.Format("C:\\TEST\\Img{0:D5}.png", i), System.Drawing.Imaging.ImageFormat.Png);
                CapPanel.Invoke((MethodInvoker)delegate {CapPanel.DrawToBitmap(CaptureBMP, rect);});
                CapPanel.Invoke((MethodInvoker)delegate { CaptureBMP.Save(string.Format("C:\\TEST\\Img{0:D5}.png", i), System.Drawing.Imaging.ImageFormat.Png); });
                Thread.Sleep(50);
                try {            
                    
                }
                catch 
                {
                    Thread.Sleep(300); 
                    try
                    {            
                       // CapPanel.DrawToBitmap(CaptureBMP, rect);
                       // CaptureBMP.Save(string.Format("C:\\TEST\\Img{0:D5}.png", i), System.Drawing.Imaging.ImageFormat.Png);
                    } 
                    catch
                     {}
                }
                
                ProgText.Invoke((MethodInvoker)delegate { ProgText.Text = "Step " + (i+1) + " of " + LiS * 30; });
                if (((i+1) % 10) == 0)
                   CurProg.Invoke((MethodInvoker)delegate { CurProg.Increment(1); });
                TempEEG.Dispose();
                VideoPanel.BackgroundImage.Dispose();
                EEGPanel.BackgroundImage.Dispose();
                ////    CaptureBMP.Dispose();
                if ((i % 30) == 0)
                {
                    GC.Collect();                                     
                }

            }
            ProgText.Invoke((MethodInvoker)delegate { ProgText.Text = "Creating Video..."; });
            Process p = new Process();
            string CmdString =" -i C:\\TEST\\Img%05d.png C:\\OUTPUT.avi -y";            
            p.StartInfo.FileName = "C:\\x264\\ffmpeg.exe";
            p.StartInfo.Arguments = CmdString;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;            
            p.Start();
            p.WaitForExit();        
        }
        private void StartCap_Click(object sender, EventArgs e)
        {
            //CreateVideo();

           if (!Stop)
            {
                StartCap.Text = "Stop Capture";
                CurProg.Value = 0;
                CurProg.Maximum = LiS * 3;
                CT = new Thread(new ThreadStart(CreateVideo));
                CT.Start();
                Stop = !Stop;
            }
            else
            {
                StartCap.Text = "Stop Capture";
                CT.Abort();
                Stop = !Stop;
            } /**/
        }

        
        private void ZoomScale_Scroll(object sender, EventArgs e)
        {
            EEGPanel.BackgroundImage.Dispose();
            Zoom = (float)ZoomScale.Value / 10;
            drawCapture(Data, f, 950, 221, 1);
            EEGPanel.BackgroundImage = EEG;
        }
        private void DumpFrames()
        {
           

            for (int i = 0; i < LiS * 30; i++)
            {
                ProgText.Invoke((MethodInvoker)delegate { ProgText.Text = "Starting Dump..."; }); 
                Mem.NextFrame();
                NewFrame = true;
                while (NewFrame) { };
                VideoBMP.Save(string.Format("C:\\TEST\\a{0:D5}.png", cnt++), System.Drawing.Imaging.ImageFormat.Png);
                VideoPanel.BackgroundImage = VideoBMP;
                Thread.Sleep(200);
                ProgText.Invoke((MethodInvoker)delegate { ProgText.Text = "Frame " + (i+1) + " of " + LiS * 30; });
                if (((i+1) % 10) == 0)
                {
                    CurProg.Invoke((MethodInvoker)delegate { CurProg.Increment(1); });
                    GC.Collect();
                    
                }
                                //Thread.Sleep(100);
                //VideoPanel.BackgroundImage = new Bitmap(string.Format("C:\\TEST\\a{0:D5}.png", cnt - 1));

            }
            Mem.SetCallback(null);
        }
        private void DumpFrames_Click(object sender, EventArgs e)
        {            
            Mem.SetCallback(delegate(Bitmap frame)
            {
                if (NewFrame)
                {
                    VideoBMP = (Bitmap)frame.Clone();
                    NewFrame = false;
                }

            });
            CurProg.Value = 0;
            CurProg.Maximum = LiS*3;
            CT = new Thread(new ThreadStart(DumpFrames));
            CT.Start();    
            //LibVlc.libvlc_media_player_next_frame(Handle)
            //VideoPanel.DrawToBitmap(VideoBMP, new Rectangle(0, 0, VideoPanel.Width, VideoPanel.Height));
            //VideoBMP = Mem.CurrentFrame;
            //VideoBMP
           
        }

        private void VideoCreator_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mem.SetCallback(delegate(Bitmap frame)
            {
                if (NewFrame)
                {
                    VideoBMP = (Bitmap)frame.Clone();
                    NewFrame = false;                   
                }                
            });
            Mem.NextFrame();
            NewFrame = true;
            Thread.Sleep(100);
            VideoPanel.BackgroundImage = VideoBMP;
        }        

    }
}
