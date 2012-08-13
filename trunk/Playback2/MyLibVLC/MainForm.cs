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
        IniFile INI;
        VlcInstance instance;
        VlcMediaPlayer player;
        FolderBrowserDialog FBD;
        string ReviewNotes;
        DateTime LastReview;
        String Reviewer;
        DateTime LastOpen;
        ACQReader ACQ;        
        string[] AVIFiles;
        bool SuppressChange;
        int[] ChanPos;
        CheckBox[] VisChecks;         
        int[] SeizureCount;
        int HighlightStart, HighlightEnd;
        bool Highlighting;
        bool RealTime;
        bool Redraw;
        string[] SzInfo;
        int SzInfoIndex;
        IniFile BioINI;
        System.IO.StreamWriter SzTxt; 
        long[] AVILengths;
        Thread ThreadDisplay;
        VlcMedia media;
        string Path;
        double PercentCompletion;
        string BaseName;
        bool Compressed;
        bool Paused;
        Graphics g;
        bool ResizeBool = false;
        bool doublesize;
        SzRvwFrm SRF; 
        int Step;
        bool CrashWarning;
        bool ignore_change;
        bool Reviewing;        
        string CurrentAVI;
        int MaxDispSize;
        string DefaultFolder; 
        float Subtractor;
        Mygraph graph;
        float[] VideoOffset;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        public MainForm()
        {
            ACQ = new ACQReader(); //Class to read from ACQ file
            graph = new Mygraph(); //Small Class for containing EEG area. 
            VideoOffset = new float[16];
            string[] args = new string[] { "" };
            instance = new VlcInstance(args);            
            graph.X1 = 5;
            graph.X2 = 1420;
            graph.Y1 = 6;
            graph.Y2 = 460;
            ACQ.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1);    //Create the graphics box to display EEG.     
            InitializeComponent();
            INI = new IniFile(Directory.GetCurrentDirectory() + "\\SeizurePlayback.ini"); 

            g = this.CreateGraphics(); //Graphics object for main form                      
            OffsetBox.Text = VideoOffset[0].ToString();
            //Create Instances                       
            CurrentAVI = ""; //No default AVI loaded            
            SeizureCount = new int[16]; //Create Array for Seizure Counts;             
               
            //Graphics area of the form to display the EEG. It would be better if these were dynamically resized. 
            //I don't have time for that shit.
            
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

            INIload();
            TimeBox.SelectedIndex = 1; //Default Time Scale
            Step = MaxDispSize; //Setting Step to max display size makes sure the image refreshes. 

            //Add Mouse Handlers
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyMouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            ResizeBool = true;
            //Start up the display thread. 
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            OffsetBox.Enabled = false;
        }
        private void INIload()
        {
            DefaultFolder = INI.IniReadValue("General", "DefaultFolder", "C:\\");
            for (int i = 0; i < 16; i++)
            {
                VideoOffset[i] = INI.IniReadValue("General", "VideoOffset" + i, (float)0.009F);
            }
        }
        private void INISave()
        {
            INI.IniWriteValue("General", "DefaultFolder", DefaultFolder);
            for (int i = 0; i < 16; i++)
            {
                INI.IniWriteValue("General", "VideoOffset" + i, VideoOffset[i]);
            }
        }
        private void ReadReviewINI(IniFile F)
        {
            CrashWarning = F.IniReadValue("General", "Crash",false);
            Compressed = F.IniReadValue("Review", "Compressed", false);
            PercentCompletion = F.IniReadValue("Review", "Complete", (double)0);
            if (PercentCompletion > 0)
            {
                Reviewer = F.IniReadValue("Review", "Reviewer", Reviewer);
                DateTime.TryParse(F.IniReadValue("Review", "LastReviewed", ""), out LastReview);
                DateTime.TryParse(F.IniReadValue("Review", "LastOpen", ""), out LastOpen);
                ReviewNotes = F.IniReadValue("Review", "Notes", "");
            }
        }
        private void UpdateReviewINI(IniFile F)
        {
            F.IniWriteValue("Review", "Complete", PercentCompletion);
            if (!Reviewing)
            {
                F.IniWriteValue("Review", "LastReviewed", LastReview.ToLongDateString());
            }
            else
            {
                F.IniWriteValue("Review", "LastReviewed", DateTime.Now.ToLongDateString());
            }
            F.IniWriteValue("Review", "LastOpen", DateTime.Now.ToLongDateString());
            F.IniWriteValue("Review", "Reviewer", Reviewer);
            F.IniWriteValue("Review", "Notes", ReviewNotes);
        }
        
        //Handles drawing the EEG to the screen. VLC handles the video independently. 
        private void DisplayThread()
        {
          
           int Delay = 0;
           bool EOFReached = false;
           // int h,m,s;
           Stopwatch st = new Stopwatch();
            while (true)
            {
                if (ACQ.Loaded)
                {
                    if ((Step >= MaxDispSize) & Reviewing) //Update Review Info - avoiding redundancy.
                    {
                        LastReview = DateTime.Now;
                        PercentCompletion = Math.Max(PercentCompletion, ((double)ACQ.Position / (double)ACQ.FileTime)*100);
                        UpdateReviewINI(BioINI);
                    }
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
                    if (!Paused)
                    {                        
                        
                        if (!RealTime)
                        {
                            if (Step >= MaxDispSize)
                            {
                                if (!ACQ.ReadData(ACQ.Position, MaxDispSize))
                                {
                                    EOFReached = true;
                                    Paused = true;
                                    PercentCompletion = 100;
                                }
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
                            if (EOFReached)
                                g.DrawString("End of File Reached", new Font("Arial", 20), new SolidBrush(Color.Red), new PointF(10, 10));
                            ACQ.Position += 10;
                            Step += 10;
                        }
                        else
                        {
                            st.Start();
                            if (Step >= MaxDispSize)
                            {

                                if (!ACQ.ReadData(ACQ.Position, MaxDispSize))
                                {
                                    EOFReached = true;
                                    Paused = true;
                                    PercentCompletion = 100;
                                }
                                Redraw = true;
                                Step = 0;
                            }
                            if (Redraw)
                                ACQ.drawbuffer();
                            g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                            g.DrawLine(new Pen(Color.Red, 3), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y1), new Point(graph.X1 + (graph.X2 * Step) / MaxDispSize, graph.Y2));
                            if (EOFReached)
                                g.DrawString("End of File Reached", new Font("Arial", 20), new SolidBrush(Color.Red), new PointF(10,10));
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
                            string s = ((int)((HighlightEnd - HighlightStart) / 60)).ToString() + ":" + string.Format("{0:00}", ((HighlightEnd - HighlightStart) % 60));
                            HighlightLabel.Invoke(new MethodInvoker(delegate { HighlightLabel.Text = s; }));
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
                                Redraw = true;
                                Step = 0;
                            }
                            if (Redraw)
                            {
                                ACQ.drawbuffer();
                                Redraw = false;
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
            FBD.SelectedPath = DefaultFolder;
            OpenFrm frm;
            Paused = true;
            string[] IniFiles; 
            FBD.ShowDialog();                        
            if (FBD.SelectedPath != "")
            {
                Path = FBD.SelectedPath;
                string[] FName = Directory.GetFiles(Path, "*.acq");
                if (FName.Length == 0) return;
                DefaultFolder = Path.Substring(0,Path.LastIndexOf("\\"));
                Console.WriteLine(DefaultFolder);                    
                HighlightLabel.Text = "";
                for (int i = 0; i < 16; i++) SeizureCount[i] = 0; //Initialize to Zero. 
                SzInfo = new string[500];
                SzInfoIndex = 0;
                IniFiles = Directory.GetFiles(Path, "*_Settings.txt");
                BioINI = new IniFile(IniFiles[0]);
                ReadReviewINI(BioINI);
                AVIFiles = Directory.GetFiles(Path, "*.avi");            
                ACQ.openACQ(FName[0]);
                ACQ.VisibleChans = ACQ.Chans;
                ACQ.SetDispLength(MaxDispSize);  
                AVILengths = new long[AVIFiles.Length];
                BaseName = AVIFiles[0].Substring(Path.Length+1,15);
                frm = new OpenFrm(BaseName, Reviewer, ReviewNotes, PercentCompletion, ACQ.FileTime, LastReview, LastOpen, CrashWarning, Compressed);
                frm.ShowDialog();
                Reviewer = frm.GetReviewer();
                Reviewing = frm.GetReviewing();
                frm.Dispose();
                MainForm.ActiveForm.Text = "Seizure Playback - " + BaseName.Substring(4, 2) + "/" + BaseName.Substring(6, 2)
                    + "/" + BaseName.Substring(0, 4) + " - " + BaseName.Substring(9, 2) + ":" + BaseName.Substring(11, 2) + ":" + BaseName.Substring(13, 2);
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
                    
                    int Animal;
                    int SzC;
                    string[] TmpStr;
                    StreamReader TmpTxt = new StreamReader(FPath + "\\" + BaseName + ".txt");                         
                    while (!TmpTxt.EndOfStream)
                    {
                        SzInfo[SzInfoIndex] = TmpTxt.ReadLine();
                        TmpStr = SzInfo[SzInfoIndex].Split(',');
                        int.TryParse(TmpStr[0], out Animal);
                        int.TryParse(TmpStr[2], out SzC);
                        SeizureCount[Animal - 1] = Math.Max(SzC, SeizureCount[Animal - 1]);
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
                INISave();
                if (Reviewing)
                {
                    ACQ.Position = (int)Math.Floor((PercentCompletion * (double)ACQ.FileTime) / (double)100);
                    Step = MaxDispSize;
                }
            }
        }

        


        
 

           
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
            {                   
                Paused = true;
                OffsetBox.Enabled = true;
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
            HighlightLabel.Text = "";
            ACQ.EndHighlight();
        }
        private void MyMouseUp(Object sender, MouseEventArgs e)
        {            
            if (ACQ.Loaded)
                if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
                {                    
                    if ((HighlightEnd - HighlightStart) < MaxDispSize/40)
                    {
                        HighlightLabel.Text = "";                        
                        int TempChan = (int)((float)ACQ.VisibleChans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                        int XStart = (int)((float)MaxDispSize * (float)(e.X - graph.X1) / (graph.X2 - graph.X1));
                        ACQ.SelectedChan = ChanPos[TempChan];
                        OffsetBox.Text = VideoOffset[ACQ.SelectedChan].ToString();
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
            long TimeSeek = (int)((float)ACQ.Position * 1000F * (1F+VideoOffset[ACQ.SelectedChan]));
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
            OffsetBox.Enabled = false;
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

            infopass P;            
            P = new infopass();
            if ((CurrentAVI != "") && (ACQ.SelectedChan != -1))
            {
                P.StartTime = (ACQ.Position - Step + HighlightStart);
                P.HighlightEnd = HighlightEnd;
                P.HighlightStart = HighlightStart;
                P.outfile = CurrentAVI.Substring(CurrentAVI.Length - 27, 18);
                P.FPath = CurrentAVI.Substring(0, CurrentAVI.LastIndexOf("\\")+1) + "Seizure";                
                SeizureCount[ACQ.SelectedChan]++;
                P.length = (int)((float)(HighlightEnd - HighlightStart + 1) * 1.01F);  
                TimeSpan t = TimeSpan.FromSeconds( P.StartTime );
                string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours,t.Minutes, t.Seconds);
                P.outfile += "_S" + SeizureCount[ACQ.SelectedChan].ToString();
                P.Sz = (ACQ.SelectedChan + 1).ToString() + ", " + ACQ.ID[ACQ.SelectedChan] + ", " + SeizureCount[ACQ.SelectedChan].ToString() + ", "
                    + answer + ", " + (HighlightEnd - HighlightStart + 1).ToString() + " , ";
                P.CurrentAVI = CurrentAVI;
                P.Subtractor = Subtractor;
                P.ACQ = ACQ;
                P.VideoOffset = VideoOffset[ACQ.SelectedChan];                
                SzPrompt Frm = new SzPrompt();
                Frm.Pass = P;
                Frm.ShowDialog(this);                
                if (Frm.Ok)
                {
                    string Result = Frm.Result; 
                    SzTxt.WriteLine(Result);
                    if (SRF != null) { SRF.Add(Result); }
                    SzInfo[SzInfoIndex] = Result;
                    SzInfoIndex++;                    
                }
                Frm.Dispose();
                Step = MaxDispSize;
                QuitHighlight();
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
            Redraw = true;
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
            if (ACQ.Loaded)
            {
                if (player != null)
                {
                    player.Stop();
                    player.Dispose();
                }
                Compression Frm = new Compression(Path);
                Frm.ShowDialog();
                if (Frm.HitStart)
                {
                    BioINI.IniWriteValue("Review", "Compressed", true);
                }
            }
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
        public void DeleteSz(int Index)
        {
            //Remove File/Count
            string FPath = AVIFiles[0].Substring(0, AVIFiles[0].LastIndexOf("\\") + 1) + "Seizure";
            string[] TmpStr = SzInfo[Index].Split(',');
            File.Delete(TmpStr[6] + ".dat");
            File.Delete(TmpStr[6] + ".avi");                        
            for (int i = Index; i < SzInfo.Length - 1; i++)
            {
                SzInfo[i] = SzInfo[i + 1];
            }
            SzInfoIndex--;
            SzTxt.Close();            
            SzTxt = new System.IO.StreamWriter(FPath + "\\" + BaseName + ".txt");
            SzTxt.AutoFlush = true;
            for (int k = 0; k < SzInfoIndex; k++)
            {
                SzTxt.WriteLine(SzInfo[k]);
            }

        }

        private void DetectionLoadButton_Click(object sender, EventArgs e)
        {

        }

        private void PMButton_Click(object sender, EventArgs e)
        {
            ProjectManager PM = new ProjectManager();
            PM.Show(this);
        }

        private void OffsetBox_TextChanged(object sender, EventArgs e)
        {
            if (ACQ.SelectedChan != -1)
            {
                float.TryParse(OffsetBox.Text, out VideoOffset[ACQ.SelectedChan]);
            }
        }

        private void Renamer_Click(object sender, EventArgs e)
        {
            RenameChans frm = new RenameChans(ACQ.Chans, ACQ.ID);
            frm.ShowDialog();
            if (frm.OK)
            {
                ACQ.ID = frm.Names; //Grab the names
                ACQ.UpdateIDs(); //Write the IDs to the ACQ file
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

            if (MainForm.ActiveForm != null)  //if (!(MainForm.ActiveForm.WindowState == FormWindowState.Minimized) && ResizeBool
            {
                Step = MaxDispSize;
                Redraw = true;
                graph.X2 = MainForm.ActiveForm.Size.Width-5; //Eat at joes
                graph.Y2 = MainForm.ActiveForm.Size.Height - 400; //this does whatever it does 
                ACQ.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1);    //Create the graphics box to display EEG.       
                VideoPanel.Location = new Point(VideoPanel.Location.X, MainForm.ActiveForm.Height - 395);
                TimeBar.Size = new Size(MainForm.ActiveForm.Size.Width - TimeBar.Location.X - 5, TimeBar.Size.Height);
                if (ACQ.Loaded) { ACQ.RefreshDisplay(); }
                MainForm.ActiveForm.Refresh();
            }
       }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            if (ACQ.Loaded)
            {
                NotesBox N = new NotesBox(ReviewNotes);
                N.ShowDialog();
                if (N.OK)
                {
                    ReviewNotes = N.Notes;
                    UpdateReviewINI(BioINI);
                }
            }
        }

        private void OffsetBox_TextChanged_1(object sender, EventArgs e)
        {
            if (ACQ.SelectedChan != -1)
            {
                float.TryParse(OffsetBox.Text, out VideoOffset[ACQ.SelectedChan]);
                INISave();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CompressionManager frm = new CompressionManager();
            frm.Show();
        }



    }
}
        