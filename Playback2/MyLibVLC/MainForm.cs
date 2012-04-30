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
        bool SuppressChange;
        int[] ChanPos;
        CheckBox[] VisChecks; 
        int[] AVINums;
        int[] SeizureCount;
        int HighlightStart, HighlightEnd;
        bool Highlighting;
        bool RealTime;
        bool Redraw;
        string[] SzInfo;
        int SzInfoIndex;
        System.IO.StreamWriter SzTxt; 
        long[] AVILengths;
        Thread ThreadDisplay;
        VlcMedia media;
        string Path;
        string BaseName;
        bool Paused;
        Graphics g;
        bool doublesize;
        SzRvwFrm SRF; 
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
            ChanPos = new int[16];
            VisChecks = new CheckBox[16];
            VisChecks[0] = VisChan1;
            VisChecks[1] = VisChan2;
            VisChecks[2] = VisChan3;
            VisChecks[3] = VisChan4;
            VisChecks[4] = VisChan5;
            VisChecks[5] = VisChan6;
            VisChecks[6] = VisChan7;
            VisChecks[7] = VisChan8;
            VisChecks[8] = VisChan9;
            VisChecks[9] = VisChan10;
            VisChecks[10] = VisChan11;
            VisChecks[11] = VisChan12;
            VisChecks[12] = VisChan13;
            VisChecks[13] = VisChan14;
            VisChecks[14] = VisChan15;
            VisChecks[15] = VisChan16;
            
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
                        if (TimeLabel.InvokeRequired) //Need to invoke timer label to change it
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

                        if (TimeBar.InvokeRequired) //Once again, need to do an invoke to handle from a separate thread
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
                            if ((st.ElapsedMilliseconds+Delay) > 1000)
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
                        else
                        {
                            if (Step >= MaxDispSize)
                            {

                                if (!ACQ.ReadData(ACQ.Position, MaxDispSize))
                                    Paused = true;
                                ACQ.drawbuffer();
                                Step = 0;
                            }
                            g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                            Thread.Sleep(100);
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
            if (SzTxt != null) SzTxt.Close();
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
            Paused = true;
            FBD.ShowDialog();            
            Path = FBD.SelectedPath;
            if (FBD.SelectedPath != "")
            {
                string[] FName = Directory.GetFiles(Path, "*.acq");
                SzInfo = new string[500];
                SzInfoIndex = 0;
                AVIFiles = Directory.GetFiles(Path, "*.avi");            
                ACQ.openACQ(FName[0]);
                ACQ.VisibleChans = ACQ.Chans;
                ACQ.SetDispLength(MaxDispSize);  
                AVILengths = new long[AVIFiles.Length];
                BaseName = AVIFiles[0].Substring(Path.Length+1,15);                
                TimeBar.Minimum = 0;
                TimeBar.Maximum = ACQ.FileTime;
                SzInfoIndex = 0;
                string FPath = AVIFiles[0].Substring(0, AVIFiles[0].LastIndexOf("\\") + 1) + "Seizure";                

                if (!Directory.Exists(FPath))
                {
                    Directory.CreateDirectory(FPath);
                }
                if (File.Exists(FPath + "\\" + BaseName + ".txt"))
                {
                    
                    int j;
                    StreamReader TmpTxt = new StreamReader(FPath + "\\" + BaseName + ".txt");                         
                    while (!TmpTxt.EndOfStream)
                    {
                        SzInfo[SzInfoIndex] = TmpTxt.ReadLine();
                        int.TryParse(SzInfo[SzInfoIndex].Substring(0,2), out j);
                        SeizureCount[j]++;
                        SzInfoIndex++;
                    }
                    TmpTxt.Dispose();
                    SzTxt = new System.IO.StreamWriter(FPath + "\\" + BaseName + ".txt");
                    for (int k = 0; k < SzInfoIndex; k++)
                    {
                        SzTxt.WriteLine(SzInfo[k]);
                    }
                }
                else
                {
                    SzTxt = new System.IO.StreamWriter(FPath + "\\" + BaseName + ".txt");
                }
                SzTxt.AutoFlush = true;
                SuppressChange = true;
                for (int i = 0; i < ACQ.Chans; i++)
                {
                    VisChecks[i].Checked = true;
                    ChanPos[i] = i;
                }
                for (int i = ACQ.Chans; i < 16; i++)
                {
                    VisChecks[i].Visible = false;
                }
                SuppressChange = false;
            }
        }

        


        
 

           
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
            {                   
                Paused = true;
                int TempChan = (int)((float)ACQ.VisibleChans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                ACQ.SelectedChan = ChanPos[TempChan];                
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
                                                
                        int TempChan = (int)((float)ACQ.VisibleChans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                        int XStart = (int)((float)MaxDispSize * (float)(e.X - graph.X1) / (graph.X2 - graph.X1));
                        ACQ.SelectedChan = ChanPos[TempChan];
                        ACQ.Position = ACQ.Position - Step + XStart;
                        Step = XStart;
                        SeekToCurrentPos();
                        QuitHighlight();
                        Paused = false;
                        RealTime = true;
                    }
                    else
                    {
                        Highlighting = false;
                    }
                }
        }
        private void SeekToCurrentPos()
        {
            int FNum = 1;
            Redraw = true;
            //Frame rate is actually 30.3, but listed as 30 in the avi. To seek to the proper time, need to adjust for that factor.
            //Switch to float to do decimal math, switch back to integer for actual ms. 
            long TimeSeek = (int)((float)ACQ.Position * 1000F * 1.00957F);
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
                    Fname = Path + "\\" + BaseName + string.Format("_{0:d2}", ACQ.SelectedChan) + string.Format("_{0:d4}.avi", FNum);
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
                    Subtractor += player.GetLengthMs();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ACQ.SelectedChan = -1;
            RealTime = false;
            if (player != null) 
                player.Stop();                
            
        }

        private void Rewind_Click(object sender, EventArgs e)
        {
            ACQ.Position = Math.Max(0, ACQ.Position - MaxDispSize);
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
            string Notes; 
            if ((CurrentAVI != "") && (ACQ.SelectedChan != -1))
            {
                StartTime = (ACQ.Position - Step + HighlightStart);                
                string outfile = CurrentAVI.Substring(CurrentAVI.Length - 27, 18);
                string FPath = CurrentAVI.Substring(0, CurrentAVI.LastIndexOf("\\")+1) + "Seizure";                
                SeizureCount[ACQ.SelectedChan]++;                
                TimeSpan t = TimeSpan.FromSeconds( StartTime );
                SzPrompt Frm = new SzPrompt();
                Frm.ShowDialog();
                Notes = Frm.Notes;
                Frm.Dispose();
                string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours,t.Minutes, t.Seconds);
                outfile += "_S" + SeizureCount[ACQ.SelectedChan].ToString();
                string Sz = (ACQ.SelectedChan+1).ToString() + ", " + ACQ.ID[ACQ.SelectedChan] +  ", " + SeizureCount[ACQ.SelectedChan].ToString() + ", "
                    + answer + ", " + (HighlightEnd - HighlightStart + 1).ToString() + " , " + Notes + " , " + outfile;
                SzTxt.WriteLine(Sz);                
                if (SRF != null) { SRF.Add(Sz); }
                SzInfo[SzInfoIndex] = Sz;
                SzInfoIndex++;
                int length = (int)((float)(HighlightEnd - HighlightStart + 1) * 1.01F);                
                outfile = FPath + "\\" + outfile;
                ACQ.DumpData(outfile + ".dat", ACQ.SelectedChan, StartTime, HighlightEnd - HighlightStart + 1);
                player.EncodeSeizure((int)((((float)StartTime * 1000 * 1.0095F) - Subtractor)/1000), length, CurrentAVI, outfile + ".avi");
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

        private void VisChan_CheckedChanged(object sender, EventArgs e)
        {
            int VChan; 
            if (!SuppressChange)
            {
                VChan = 0; 
                for (int i = 0; i < 16; i++)
                {
                    if (VisChecks[i].Checked)
                    {
                        ACQ.HideChan[i] = false;
                        ChanPos[VChan] = i;
                        VChan++;                        
                    }
                    else
                    {
                        ACQ.HideChan[i] = true;
                    }                    
                }
                ACQ.VisibleChans = VChan;
                ACQ.ResetScale();
                Step = MaxDispSize;
            }
        }

        private void ZoomScale_Scroll(object sender, EventArgs e)
        {
            ACQ.Zoom = (float)ZoomScale.Value / 10;
        }

        private void VideoPanel_Click(object sender, EventArgs e)
        {
            if (!doublesize)
            {
                VideoPanel.Height = 720;
                VideoPanel.Width = 960;
                VideoPanel.Location = new Point(VideoPanel.Location.X, VideoPanel.Location.Y - 360);             
            }
            else
            {
                VideoPanel.Width = 480;
                VideoPanel.Height = 360;
                VideoPanel.Location = new Point(VideoPanel.Location.X, VideoPanel.Location.Y + 360);             
            }
            doublesize = !doublesize;
        }

        private void CompressFinish_Click(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
            }
            Compression Frm = new Compression(Path);
            Frm.ShowDialog(this);
            Frm.Dispose();
        }

        private void RvwSz_Click(object sender, EventArgs e)
        {
            SRF = new SzRvwFrm(this);
            for (int i = 0; i < SzInfoIndex; i++)
            {
                SRF.Add(SzInfo[i]);
            }
            SRF.Show();
        }
        public void ChildSend(TimeSpan Time, int Channel)
        {
            ACQ.SelectedChan = Channel;
            Paused = false;
            RealTime = true;
            ACQ.Position = (int)Time.TotalSeconds;
            Step = MaxDispSize;
            SeekToCurrentPos();                        
        }













    }
}
        