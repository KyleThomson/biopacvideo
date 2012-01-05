using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SeizurePlayback
{
    public partial class MainForm : Form
    {
        VlcInstance instance;
        VlcMediaPlayer player;
        FolderBrowserDialog FBD;
        ACQReader ACQ;        
        string[] AVIFiles;
        int[] AVINums;
        bool RealTime;
        bool Redraw;
        long[] AVILengths;
        Thread ThreadDisplay;
        VlcMedia media;
        string Path;
        string BaseName;            
        Graphics g;
        int Step = 30;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        public MainForm()
        {
            InitializeComponent();
            ACQ = new ACQReader();
            g = this.CreateGraphics();
            ACQ.initDisplay(1180,500);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyMouseClick);
            string[] args = new string[] {""
                //,"--vout-filter=deinterlace", "--deinterlace-mode=blend"
            };

            instance = new VlcInstance(args);
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            
        }
 
        private void DisplayThread()
        {
          
           int Delay = 0;
           // int h,m,s;
           Stopwatch st = new Stopwatch();
            while (true)
            {
                if (ACQ.Loaded)
                {
                    
                    //TimeLabel.Text = string.Format("{0:00}:{0:00}:{0:00}", h, m, s);
                    if (TimeLabel.InvokeRequired)
                    {
                        TimeLabel.Invoke(new MethodInvoker(delegate {
                            int h, m, s;
                            h = ACQ.Position / 3600;
                            m = (ACQ.Position - (h * 3600)) / 60;
                            s = ACQ.Position - h * 3600 - m * 60;
                            TimeLabel.Text = string.Format("{0:00}:", h) + string.Format("{0:00}:", m) + string.Format("{0:00}", s);
                        }));
                    }

                    if (!RealTime)
                    {
                        if (Step >= 30)
                        {
                         
                            ACQ.ReadData(ACQ.Position, 30);                         
                            Step = 0;
                            Redraw = true;
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }
                        if (Redraw)
                            ACQ.drawbuffer();
                        g.DrawImage(ACQ.offscreen, 10, 20);
                        g.DrawLine(new Pen(Color.Red, 3), new Point(10+(1180 *Step) / 30, 20), new Point(10+(1180 * Step)/30, 520));
                        ACQ.Position += 10;
                        Step +=10;                        
                    }
                    else
                    {
                        st.Start();
                        if (Step >= 30)
                        {
                         
                            ACQ.ReadData(ACQ.Position, 30);
                            Redraw = true;
                            Step = 0;                                                        
                        }
                        else
                        {                            
                        }
                        if (Redraw)
                            ACQ.drawbuffer();
                        g.DrawImage(ACQ.offscreen, 10, 20);
                        g.DrawLine(new Pen(Color.Red, 3), new Point(10 + (1180 * Step) / 30, 20), new Point(10 + (1180 * Step) / 30, 520));
                        ACQ.Position += 1;
                        Step += 1;
                        st.Stop();
                        if (st.ElapsedMilliseconds > 1000)
                        {
                            Delay = (int)st.ElapsedMilliseconds - 1000;
                        }
                        else
                        {
                            Thread.Sleep(1000 - ((int)st.ElapsedMilliseconds + Delay));                                
                            Delay = 0;
                        }
                        st.Reset();
                    }
                }
                   
            } 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThreadDisplay.Abort();
            if (player != null) player.Dispose();
            instance.Dispose();            
        }


        private void Play_Click(object sender, EventArgs e)
        {
            Play.Enabled = false;
            Pause.Enabled = true;
            Stop.Enabled = true;
             
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Play.Enabled = true;
            Pause.Enabled = true;
            Stop.Enabled = true;
           
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Play.Enabled = true;
            Pause.Enabled = false;
            Stop.Enabled = false;      
        }
      
        private VlcMediaPlayer LoadFile(VlcMediaPlayer player, string FileName)
        {          
            using (VlcMedia media = new VlcMedia(instance, FileName))
            {
                if (player == null)
                    player = new VlcMediaPlayer(media);
                else
                    player.Media = media;
            }

            Play.Enabled = true;
            Pause.Enabled = false;
            Stop.Enabled = false;
            return player;
        }
        private void Open_Click(object sender, EventArgs e)
        {   
            FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            Path = FBD.SelectedPath;
            string[] FName = Directory.GetFiles(Path, "*.acq");
            AVIFiles = Directory.GetFiles(Path, "*.avi");            
            ACQ.openACQ(FName[0]);
            AVILengths = new long[AVIFiles.Length];
            BaseName = AVIFiles[0].Substring(Path.Length+1,15);
            TimeBar.Minimum = 0;
            TimeBar.Maximum = ACQ.FileTime;
        }

        


        
 

        private void test_Click(object sender, EventArgs e)
        {
            TimeBar.Maximum = (int)(player.GetLengthMs()/1000);
            TimeBar.SmallChange = (int)(TimeBar.Maximum * 0.01);
            TimeBar.LargeChange = (int)(TimeBar.Maximum * 0.1);
        }

        private void Enc1_Click(object sender, EventArgs e)
        {
            int Length;
            if (int.TryParse("60", out Length))                
            {                
                player.EncSeizureFromNow(Length);
            }
        }
        private void MyMouseClick(Object sender, MouseEventArgs e)
        {
            int FNum = 1;
            if (ACQ.Loaded)
                if ((e.X > 10) && (e.X < 1180) && (e.Y > 20) && (e.Y < 520))
                {
                    
                    ACQ.SelectedChan = (int)((float)ACQ.Chans * (float)(((float)e.Y - 30F) / 500F));
                    RealTime = true;
                    Redraw = true;
                    long TimeSeek = ACQ.Position * 1000;
                    bool AVILoaded = false;
                    while (!AVILoaded) 
                    {
                        string Fname = Path + "\\" + BaseName + string.Format("_{0:d2}", ACQ.SelectedChan) + string.Format("_{0:d4}.avi", FNum);                        
                        Console.WriteLine(Fname);
                         media = new VlcMedia(instance, Fname);                            
                        if (player == null)
                        {
                            player = new VlcMediaPlayer(media);
                            player.Drawable = VideoPanel.Handle;
                        }
                        else
                        player.Media = media;
                        player.Play();
                        while (player.GetLengthMs() == 0)
                        { }
                        if (player.GetLengthMs() < TimeSeek)
                        {
                            TimeSeek = TimeSeek - player.GetLengthMs();
                            player.Stop();
                            FNum++;
                            media.Dispose();
                        }
                        else
                        {
                            AVILoaded = true;
                            player.seek(TimeSeek);
                        }
                                                    
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ACQ.SelectedChan = -1;
            RealTime = false;
            
        }

        private void Rewind_Click(object sender, EventArgs e)
        {
            ACQ.Position -= 60;
            if (player.IsPlaying)
                player.seek(player.getpos() - 60 * 1000);
            Step = 99;
        }

        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            ACQ.Position = TimeBar.Value;
            Redraw = true;
        }
    }
}
        