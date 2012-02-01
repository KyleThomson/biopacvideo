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
        int[] SeizureCount;
        int HighlightStart, HighlightEnd;
        bool Highlighting;
        bool RealTime;
        bool Redraw;
        long[] AVILengths;
        Thread ThreadDisplay;
        VlcMedia media;
        string Path;
        string BaseName;
        bool Paused;
        Graphics g;
        int Step;
        bool ignore_change;
        string CurrentAVI;
        int MaxDispSize;
        float Subtractor;
        Mygraph graph;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        public MainForm()
        {
            InitializeComponent();

            //Create Instances
            graph = new Mygraph(); //Small Class for containing EEG area. 
            ACQ = new ACQReader(); //Class to read from ACQ file
            g = this.CreateGraphics(); //Graphics object for main form
            string[] args = new string[] {""
                //,"--vout-filter=deinterlace", "--deinterlace-mode=blend"
            };
            instance = new VlcInstance(args);
            CurrentAVI = ""; //No default AVI loaded            
            SeizureCount = new int[16]; //Create Array for Seizure Counts; 
            for (int i = 0; i < 16; i++) SeizureCount[i] = 0; //Initialize to Zero. 

            //Graphics area of the form to display the EEG. It would be better if these were dynamically resized. 
            //I don't have time for that shit.
            graph.X1 = 5;
            graph.X2 = 1420;
            graph.Y1 = 6;
            graph.Y2 = 460;          
                           
            ACQ.initDisplay(graph.X2-graph.X1, graph.Y2-graph.Y1);    //Create the graphics box to display EEG.         
            TimeBox.SelectedIndex = 0; //Default Time Scale
            Step = MaxDispSize; //Setting Step to max display size makes sure the image refreshes. 

            //Add Mouse Handlers
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyMouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);

            //Start up the display thread. 
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();            
        }

        
        //Handles drawing the EEG to the screen. VLC handles the video independently. 
        private void DisplayThread()
        {
          
           int Delay = 0;
           // int h,m,s;
           Stopwatch st = new Stopwatch();
            while (true)
            {
                if (ACQ.Loaded)
                {
                    if (!Paused)
                    {                        
                        if (TimeLabel.InvokeRequired)
                        {
                            TimeLabel.Invoke(new MethodInvoker(delegate
                            {
                                int h, m, s;
                                h = ACQ.Position / 3600;
                                m = (ACQ.Position - (h * 3600)) / 60;
                                s = ACQ.Position - h * 3600 - m * 60;
                                //C# sucks at handling string formating.
                                TimeLabel.Text = string.Format("{0:00}:", h) + string.Format("{0:00}:", m) + string.Format("{0:00}", s);
                            }));
                        }

                        if (TimeBar.InvokeRequired)
                        {
                            TimeBar.Invoke(new MethodInvoker(delegate
                            {
                                ignore_change = true;
                                TimeBar.Value = Math.Min(ACQ.Position, TimeBar.Maximum);
                            }));
                        }
                        if (!RealTime)
                        {
                            if (Step >= MaxDispSize)
                            {
                                if (!ACQ.ReadData(ACQ.Position, MaxDispSize))
                                    Paused = true;                                
                                Step = 0;
                                Redraw = true;
                            }
                            else
                            {
                                Thread.Sleep(1500 / MaxDispSize);
                            }
                            if (Redraw)
                                ACQ.drawbuffer();
                            g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                            g.DrawLine(new Pen(Color.Red, 3), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y1), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y2));
                            ACQ.Position += 10;
                            Step += 10;
                        }
                        else
                        {
                            st.Start();
                            if (Step >= MaxDispSize)
                            {

                                if (!ACQ.ReadData(ACQ.Position, MaxDispSize))
                                    Paused = true;
                                Redraw = true;
                                Step = 0;
                            }
                            if (Redraw)
                                ACQ.drawbuffer();
                            g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                            g.DrawLine(new Pen(Color.Red, 3), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y1), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y2));
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
                    } //if !Paused
                    else
                    {
                        if (Highlighting)
                        {
                            int X = System.Windows.Forms.Cursor.Position.X;
                            HighlightEnd = (int)((float)MaxDispSize * (float)(X - graph.X1) / (graph.X2 - graph.X1));
                            ACQ.sethighlight(HighlightStart, HighlightEnd);
                            ACQ.drawbuffer();
                            g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                            Thread.Sleep(30);
                        }
                    }
                } //if ACQLoaded
                else
                {
                    g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                    Thread.Sleep(100);
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
            Paused = false;
            if (player != null & RealTime)
                player.Play();            
            QuitHighlight();
             
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Paused = true;
            if (player != null)
                player.Pause();
            
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
                        
            return player;
        }
        private void Open_Click(object sender, EventArgs e)
        {   
            FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            Path = FBD.SelectedPath;
            if (FBD.SelectedPath != "")
            {
            string[] FName = Directory.GetFiles(Path, "*.acq");
            AVIFiles = Directory.GetFiles(Path, "*.avi");            
            ACQ.openACQ(FName[0]);
            AVILengths = new long[AVIFiles.Length];
            BaseName = AVIFiles[0].Substring(Path.Length+1,15);
            TimeBar.Minimum = 0;
            TimeBar.Maximum = ACQ.FileTime;
            }
        }

        


        
 

           
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
            {                   
                Paused = true;                
                ACQ.SelectedChan = (int)((float)ACQ.Chans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2-graph.Y1)));
                HighlightStart = (int)((float)MaxDispSize * (float)(e.X-graph.X1)/(graph.X2 - graph.X1));
                Highlighting = true;
                HighlightEnd = HighlightStart;
                ACQ.sethighlight(HighlightStart, HighlightEnd);
                if (player != null)
                    player.Pause();
            }
        }
        private void QuitHighlight()
        {
            Highlighting = false;
            HighlightEnd = HighlightStart = 0;
            ACQ.EndHighlight();
        }
        private void MyMouseUp(Object sender, MouseEventArgs e)
        {            
            if (ACQ.Loaded)
                if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
                {
                    if ((HighlightEnd - HighlightStart) < 3)
                    {
                        QuitHighlight();
                        Paused = false;
                        int FNum = 1;
                        ACQ.SelectedChan = (int)((float)ACQ.Chans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                        int XStart = (int)((float)MaxDispSize * (float)(e.X - graph.X1) / (graph.X2 - graph.X1));
                        ACQ.Position = ACQ.Position - Step + XStart;
                        Step = XStart;
                        RealTime = true;
                        Redraw = true;
                        //Frame rate is actually 30.3, but listed as 30 in the avi. To seek to the proper time, need to adjust for that factor.
                        //Switch to float to do decimal math, switch back to integer for actual ms. 
                        long TimeSeek = (int)((float)ACQ.Position * 1000F * 1.01F);
                        bool AVILoaded = false;
                        bool pass = false;
                        Subtractor = 0;                                               
                        while (!AVILoaded)
                        {

                            CurrentAVI = "";   
                            string Fname = Path + "\\" + BaseName + string.Format("_{0:d2}", ACQ.SelectedChan) + string.Format("_{0:d4}.avi", FNum);
                            while (!File.Exists(Fname) && !pass)
                            {
                                FNum++;
                                if (FNum == 30)
                                    pass = true;
                            }
                            if (pass)
                            {
                                break;
                            }
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
                                Subtractor += player.GetLengthMs()/1000;
                                player.Stop();
                                FNum++;
                                media.Dispose();
                            }
                            else
                            {
                                AVILoaded = true;
                                CurrentAVI = Fname;
                                player.seek(TimeSeek);
                            }
                        }
                    }
                    else
                    {
                        Highlighting = false;
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ACQ.SelectedChan = -1;
            RealTime = false;
            if (player != null) 
                player.Stop();                
            
        }

        private void Rewind_Click(object sender, EventArgs e)
        {
            ACQ.Position -= MaxDispSize;
            if (player != null) 
                player.seek(player.getpos() - MaxDispSize * 1000);
            Step = MaxDispSize;
        }

        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            if (!ignore_change)
            {
                ACQ.Position = TimeBar.Value;
                Step = MaxDispSize;                
            }
            else
            {
                ignore_change = false;
            }
        }
        

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] TimeScales = {30, 60, 120, 300, 600};
            MaxDispSize = TimeScales[TimeBox.SelectedIndex];
            Rewind.Text = "Rewind " + MaxDispSize.ToString() + "s";
            Step = MaxDispSize;
            ACQ.SetDispLength(MaxDispSize);            
        }

        

        private void SzCaptureButton_Click(object sender, EventArgs e)
        {
            int StartTime;                
            if (CurrentAVI != "")
            {
                StartTime = (ACQ.Position - Step + HighlightStart);                
                string outfile = CurrentAVI.Substring(CurrentAVI.Length - 27, 18);
                string FPath = CurrentAVI.Substring(0, CurrentAVI.LastIndexOf("\\")+1) + "Seizure";
                SeizureCount[ACQ.SelectedChan]++;
                if (!Directory.Exists(FPath))
                {
                    Directory.CreateDirectory(FPath);
                }
                int length = (int)((float)(HighlightEnd - HighlightStart + 1) * 1.01F);
                outfile = FPath + "\\" + outfile + "_S" + SeizureCount[ACQ.SelectedChan].ToString();
                ACQ.DumpData(outfile + ".dat", ACQ.SelectedChan, StartTime, HighlightEnd - HighlightStart + 1);
                player.EncodeSeizure((int)((float)StartTime * 1.01F - Subtractor), length, CurrentAVI, outfile + ".avi");
            }
        }

        private void TimeJump_TextChanged(object sender, EventArgs e)
        {
             TimeSpan TestTime;
             if (TimeSpan.TryParse(TimeJump.Text, out TestTime))
             {
                 TimeJump.Text = TestTime.ToString();
                 ACQ.Position = TestTime.Hours * 3600 + TestTime.Minutes * 60 + TestTime.Seconds;
                 Step = MaxDispSize;
                 RealTime = true;
             }
        }
    }
}
        