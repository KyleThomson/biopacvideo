using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class CManage : Form
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
        DetectedSeizureFileType DSF;

        string[] AVIFiles;
        string AVIMode;
        bool SuppressChange;
        int[] ChanPos;
        CheckBox[] VisChecks;
        int[] SeizureCount;
        int HighlightStart, HighlightEnd;
        bool Highlighting;
        bool RealTime;
        bool Redraw;
        bool FastReviewState, FastReviewChange;
        int FastReviewPage;
        int[] CamerAssc;
        string[] SzInfo;
        int SzInfoIndex;
        IniFile BioINI;
        System.IO.StreamWriter SzTxt;

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
        bool VideoCapture;
        ReviewLogger RL; //Log of reviewer actions
        bool ignore_change;
        bool Reviewing;
        string CurrentAVI;
        public int MaxDispSize;
        string DefaultFolder;
        float Subtractor;
        string CurrentProject;
        string X264path;
        long[,] AVILengths;
        FolderBrowserDialog TempDiag;
        Mygraph graph;
        Mygraph FRgraph;
        float[] VideoOffset;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        int fastReviewCounter;
        int fastReviewLastPage = 0;
        int regularReviewReturn = 0;
        bool checkedChange = false;
        List<int> HCL;
        bool testMode = true;
        bool finsihedReview = false;
        bool isMP4 = false;
        string GlobalPath = null;
        string[,] AVINameList;
        bool[] VLCisLoaded = new bool[16];
        long totms;
        int[,] ButtonLoc;
        public int numPerPage;
        bool vLoading = false;
        TrackBar[] ChanZooms;
        int RewindVal = 30;
        public long[,] vidRanges = new long[16, 30];



        public CManage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ACQ = new ACQReader(); //Class to read from ACQ file
            graph = new Mygraph(); //Small Class for containing EEG area. 
            FRgraph = new Mygraph();
            DSF = new DetectedSeizureFileType();
            numPerPage = 24;
            HCL = DSF.HCL;
            FastReviewState = false;
            FastReviewChange = false;
            ButtonLoc = new int[3, 2] { { this.FastReview.Location.X, this.FastReview.Location.Y }, { this.button3.Location.X, this.button3.Location.Y }, { this.button2.Location.X, this.button2.Location.Y } };
            
            ChanZooms = new TrackBar[] { ZoomChan1, ZoomChan2, ZoomChan3, ZoomChan4, ZoomChan5, ZoomChan6, ZoomChan7, ZoomChan8, ZoomChan9, ZoomChan10, ZoomChan11, ZoomChan12 };




            comboBox1.Items.RemoveAt(8);
            comboBox1.Items.Insert(8, "Channel Zoom Control [   ]");
            comboBox1.Text = "More Options";

            VideoOffset = new float[16];
            string[] args = new string[] { "" };
            instance = new VlcInstance(args);
            INI = new IniFile(Directory.GetCurrentDirectory() + "\\SeizurePlayback.ini");
            ACQ.initDisplay(10, 10, 10);
            g = this.CreateGraphics(); //Graphics object for main form                      
            OffsetBox.Text = VideoOffset[0].ToString();
            //Create Instances                       
            CurrentAVI = ""; //No default AVI loaded            
            SeizureCount = new int[16]; //Create Array for Seizure Counts;             
            AVILengths = new long[16, 30];
            //Graphics area of the form to display the EEG. It would be better if these were dynamically resized. 
            //I don't have time for that shit. 
            // Haha but maybe i have time for that!

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
            RewindVal = MaxDispSize;
            Console.WriteLine(X264path + "\\ffmpeg.exe");
            if (!File.Exists(X264path + "\\ffmpeg.exe"))
            {
                TempDiag = new FolderBrowserDialog();
                TempDiag.Description = "FFMPEG Not Found. You will not be able to capture Seizures without this. Select Location.";
                TempDiag.ShowDialog();
                X264path = TempDiag.SelectedPath;
                if (!File.Exists(X264path + "\\ffmpeg.exe"))
                {
                    MessageBox.Show("FFMPEG not found at this location. You will not be able to capture seizures. ");
                }
                else
                {
                    INISave();
                }
            }
            //Add Mouse Handlers
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyMouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            ResizeBool = true;
            //Start up the display thread. 
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            OffsetBox.Enabled = false;
            graph.X1 = 5;
            graph.X2 = this.Size.Width - 10;
            graph.Y1 = 6;
            graph.Y2 = VideoPanel.Location.Y - 11; //this.Size.Height - VideoPanel.Location.Y;    
            FRgraph.X1 = 5;
            FRgraph.X2 = this.Size.Width - 10;
            FRgraph.Y1 = 6;
            FRgraph.Y2 = FRZoomBlock.Location.Y - 20;
            // Console.WriteLine(this.Size.Height);
            //Console.WriteLine(VideoPanel.Location.Y - 11);
            ACQ.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, FRZoomBlock.Location.Y - 20);    //Create the graphics box to display EEG. 
            this.BackColor = Color.Black;
            this.Opacity = 100;
            this.Refresh();

            this.VisChan1.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan1.TabIndex - 25), VisChan1.Checked); };
            this.VisChan2.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan2.TabIndex - 25), VisChan2.Checked); };
            this.VisChan3.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan3.TabIndex - 25), VisChan3.Checked); };
            this.VisChan4.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan4.TabIndex - 25), VisChan4.Checked); };
            this.VisChan5.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan5.TabIndex - 25), VisChan5.Checked); };
            this.VisChan6.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan6.TabIndex - 25), VisChan6.Checked); };
            this.VisChan7.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan7.TabIndex - 25), VisChan7.Checked); };
            this.VisChan8.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan8.TabIndex - 25), VisChan8.Checked); };
            this.VisChan16.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan16.TabIndex - 25), VisChan16.Checked); };
            this.VisChan15.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan15.TabIndex - 25), VisChan15.Checked); };
            this.VisChan14.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan14.TabIndex - 25), VisChan14.Checked); };
            this.VisChan13.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan13.TabIndex - 25), VisChan13.Checked); };
            this.VisChan12.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan12.TabIndex - 25), VisChan12.Checked); };
            this.VisChan11.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan11.TabIndex - 25), VisChan11.Checked); };
            this.VisChan10.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan10.TabIndex - 25), VisChan10.Checked); };
            this.VisChan9.CheckedChanged += delegate (object sender, System.EventArgs e) { VisChan_CheckedChanged_List(sender, e, (VisChan9.TabIndex - 25), VisChan9.Checked); };

            this.SwitchChan.CheckedChanged += delegate (object sender, System.EventArgs e) { SwitchChan_CheckedChanged(sender, e, this.SwitchChan.Checked); };
            this.Rewind_Dec.Click += delegate (object sender, System.EventArgs e) { Rewind_Change(sender, e, 0); };
            this.Rewind_Inc.Click += delegate (object sender, System.EventArgs e) { Rewind_Change(sender, e, 1); };


            this.numPerBox.SelectedItem = "24";




        }
        private void INIload()
        {
            DefaultFolder = INI.IniReadValue("General", "DefaultFolder", "C:\\");
            for (int i = 0; i < 16; i++)
            {
                VideoOffset[i] = INI.IniReadValue("General", "VideoOffset" + i, (float)0.009F);
            }
            CurrentProject = INI.IniReadValue("General", "CurrentProject", "");
            X264path = INI.IniReadValue("General", "X264path", "C:\\X264");
            VideoCapture = INI.IniReadValue("General", "SaveVideo", true);
            ACQ.Telemetry = INI.IniReadValue("General", "Telemetry", true);

            //ACQ.Telemetry = INI.IniReadValue("BioPac", "RecordingDevice_Channel0", true);
            TelemetryBox.Checked = ACQ.Telemetry;
        }
        private void INISave()
        {
            INI.IniWriteValue("General", "DefaultFolder", DefaultFolder);
            for (int i = 0; i < 16; i++)
            {
                INI.IniWriteValue("General", "VideoOffset" + i, VideoOffset[i]);
            }
            INI.IniWriteValue("General", "CurrentProject", CurrentProject);
            INI.IniWriteValue("General", "X264path", X264path);
            INI.IniWriteValue("General", "SaveVideo", VideoCapture);
            INI.IniWriteValue("General", "Telemetry", ACQ.Telemetry);
        }
        private void ReadReviewINI(IniFile F)
        {
            CamerAssc = new int[16];
            for (int cloop = 0; cloop < 16; cloop++)
            {
                CamerAssc[cloop] = F.IniReadValue("Video", "Camera" + cloop.ToString(), cloop);
            }

            CrashWarning = F.IniReadValue("General", "Crash", false);
            Compressed = F.IniReadValue("Review", "Compressed", false);




            PercentCompletion = F.IniReadValue("Review", "Complete", (double)0);
            if (PercentCompletion == 100) finsihedReview = true;
            if (PercentCompletion == 100) this.ColorClear.BackColor = Color.Green;
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
            // int h,m,s;
            Stopwatch st = new Stopwatch();
            while (true)
            {
                if (ACQ.Loaded)
                {
                    if (FastReviewState)
                    {

                        if (FastReviewChange)
                        {
                            int SeizureCount;
                            SeizureCount = FastReviewPage * numPerPage;
                            //DSF.SetSeizureNumber(SeizureCount);
                            DSF.SetSeizureNumber(SeizureCount);
                            DetectedSeizureType Sz;
                            ACQ.cleargraph();

                            for (int i = 0; i < numPerPage; i++)
                            {
                                Sz = DSF.GetCurrentSeizure();
                                if (!ACQ.DisplayDetection(Math.Max(Sz.TimeInSec - 15, 0), Sz.Channel, i % 2, i / 2, Sz.Display))
                                {
                                    MessageBox.Show("FAILED DRAWING");
                                }
                                if (!DSF.Inc()) break;
                            }
                            g.DrawImage(ACQ.frOffscreen, FRgraph.X1, FRgraph.Y1);
                            FastReviewChange = false;
                            Redraw = false;

                        }

                        if (Redraw) FastReviewChange = true;
                    }
                    else
                    {
                        if ((Step >= MaxDispSize) & Reviewing) //Update Review Info - avoiding redundancy.
                        {
                            LastReview = DateTime.Now;
                            PercentCompletion = Math.Max(PercentCompletion, ((double)ACQ.Position / (double)ACQ.TotFileTime) * 100);
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
                                //ACQ.DrawHL();
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
                                /*if (EOFReached)
                                     g.DrawString("End of File Reached", new Font("Arial", 20), new SolidBrush(Color.Red), new PointF(10,10));*/
                                ACQ.Position += 1;
                                Step += 1;
                                st.Stop();
                                if ((st.ElapsedMilliseconds + Delay) > 1000)
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
                                if (HighlightEnd - HighlightStart < 5)
                                {

                                }
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


                    }
                } //if ACQLoaded
                else
                {

                    g.DrawImage(ACQ.offscreen, graph.X1, graph.Y1);
                    Thread.Sleep(100);
                }

            }
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Previous_Click(null, null);
            }
            if (e.KeyCode == Keys.Right)
            {
                Next_Click(null, null);
            }
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (string.Compare(s, "r", true) == 0)
            {
                Rewind_Click(null, null);
            }
            if (string.Compare(s, "p", true) == 0)
            {
                if (Paused)
                {
                    Play_Click(null, null);
                }
                else
                {
                    Pause_Click(null, null);
                }
            }
            if (string.Compare(s, "s", true) == 0)
            {
                SpeedUp_Click(null, null);
            }
            if (string.Compare(s, "u", true) == 0)
            {
                Pause_Click(null, null);
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

            if (Paused == true) return;
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
            //Open.Enabled = false;
            TimeJump.Enabled = true;
            comboBox1.Enabled = true;
            for (int i = 0; i < 16; i++)
            {
                VLCisLoaded[i] = false;

            }

            if (ACQ.Loaded)
            {
 

                //DefaultFolder = INI.IniReadValue("General", "DefaultFolder", "C:\\");
                //Paused = true;
                //ACQ = new ACQReader();
                //ACQ.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, FRZoomBlock.Location.Y - 20);

                var ex = MessageBox.Show("In order to open a new EEG, you must restart the application. \n \t \t Would you like to exit?", "Restart", MessageBoxButtons.OKCancel);

                if (ex == DialogResult.OK) Application.Restart(); //for now, until I can figure out what's wrong with the load
                return;
                //player = null;
                //g.Clear(Color.Black);
            }

            //if (testMode)
            //{
            //    AutoLoadForTest();
            //    return;
            //}
            fastReviewCounter = 0;
            fastReviewLastPage = 0;

            DialogResult tempRes;

            FBD = new FolderBrowserDialog();
            FBD.SelectedPath = DefaultFolder;

            float hrs;
            OpenFrm frm;
            Paused = true;
            string[] IniFiles;
            string AVIname;
            tempRes = FBD.ShowDialog();
            totms = 0;
            if (tempRes == DialogResult.Cancel) return;
            if (FBD.SelectedPath != "")
            {
                Path = FBD.SelectedPath;

                string[] FName = Directory.GetFiles(Path, "*.acq");
                if (FName.Length == 0) return;
                DefaultFolder = Path.Substring(0, Path.LastIndexOf("\\"));
                Console.WriteLine(DefaultFolder);
                HighlightLabel.Text = "";
                for (int i = 0; i < 16; i++) SeizureCount[i] = 0; //Initialize to Zero. 
                SzInfo = new string[500];
                SzInfoIndex = 0;
                IniFiles = Directory.GetFiles(Path, "*_Settings.txt");
                BioINI = new IniFile(IniFiles[0]);
                ReadReviewINI(BioINI);

                AVIMode = "avi";
                AVIFiles = Directory.GetFiles(Path, "*.avi");
                LoadText.Visible = true;
                this.Refresh();
                //AVILoadBar.Visible = true;
                if (AVIFiles.Length == 0)
                {
                    AVIFiles = Directory.GetFiles(Path, "*.mp4");

                    if (AVIFiles.Length > 0)
                    {
                        AVIMode = "mp4";
                        isMP4 = true;
                        AVINameList = new string[16, 30];
                        //load all potential MP4 files
                        BaseName = AVIFiles[0].Substring(Path.Length + 1, 15);
                        OffsetBox.Visible = false;
                        OffsetLabel.Visible = false;
                        //AVILoadBar.Maximum = AVIFiles.Length;

                        //JOSH
                        for (int chanloop = 0; chanloop < 16; chanloop++)
                        {
                            totms = 0;
                            for (int fileloop = 0; fileloop < 30; fileloop++)
                            {
                                if (fileloop == 0)
                                {
                                    AVIname = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + ".mp4";
                                    AVINameList[chanloop, fileloop] = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + ".mp4";
                                }
                                else
                                {
                                    AVIname = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + "." + (fileloop).ToString() + ".mp4";
                                    AVINameList[chanloop, fileloop] = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + "." + (fileloop).ToString() + ".mp4";
                                }
                                //if (File.Exists(AVIname) && chanloop == 0)
                                //{
                                //    //Console.WriteLine(AVIname);
                                //    media = new VlcMedia(instance, AVIname);
                                //    if (player == null)
                                //    {
                                //        player = new VlcMediaPlayer(media);
                                //        player.Drawable = VideoPanel.Handle;
                                //    }
                                //    else player.Media = media;
                                //    player.Play();
                                //    while (player.GetLengthMs() == 0)
                                //    { }
                                //    AVILengths[chanloop, fileloop] = player.GetLengthMs();
                                //    totms += AVILengths[chanloop, fileloop];
                                //    player.Stop();
                                //    media.Dispose();
                                //    AVILoadBar.Increment(1);
                                //    VLCisLoaded[chanloop] = true;
                                //}
                                //else
                                //{
                                //    AVILengths[chanloop, fileloop] = 0;
                                //}
                            }
                            // hrs =(totms / 1000);
                            //Console.WriteLine("TOTAL LENGTH: " + hrs.ToString());
                        }
                    }
                }
                else
                {
                    BaseName = AVIFiles[0].Substring(Path.Length + 1, 15);
                    isMP4 = false;
                    AVINameList = new string[16, 10];
                    //Load all potential AVI files
                    for (int chanloop = 0; chanloop < 16; chanloop++)
                    {
                        totms = 0;
                        //AVILoadBar.Maximum = AVIFiles.Length;
                        for (int fileloop = 1; fileloop < 10; fileloop++)
                        {
                            AVIname = Path + "\\" + BaseName + string.Format("_{0:d2}", chanloop) + string.Format("_{0:d4}.avi", fileloop);
                            AVINameList[chanloop, fileloop - 1] = Path + "\\" + BaseName + string.Format("_{0:d2}", chanloop) + string.Format("_{0:d4}.avi", fileloop);

                            //if (File.Exists(AVIname) && chanloop == 0)
                            //{

                            //    media = new VlcMedia(instance, AVIname);
                            //    if (player == null)
                            //    {
                            //        player = new VlcMediaPlayer(media);
                            //        player.Drawable = VideoPanel.Handle;
                            //    }
                            //    else player.Media = media;
                            //    player.Play();
                            //    while (player.GetLengthMs() == 0)
                            //    { }
                            //    AVILengths[chanloop, fileloop - 1] = player.GetLengthMs();
                            //    totms += AVILengths[chanloop, fileloop - 1];
                            //    //Console.WriteLine(AVIname + "  " + AVILengths[chanloop, fileloop - 1].ToString());
                            //    player.Stop();
                            //    media.Dispose();
                            //    AVILoadBar.Increment(1);
                            //}
                            //else
                            //{
                            //    AVILengths[chanloop, fileloop - 1] = 0;
                            //}
                        }
                        for (int fileloop = 10; fileloop < 30; fileloop++)
                        {
                            AVILengths[chanloop, fileloop - 1] = 0;
                        }
                        //hrs = totms / (3600 * 1000);
                        //Console.WriteLine("TOTAL LENGTH: " + hrs.ToString());
                    }

                }
                LoadText.Visible = false;
                //AVILoadBar.Visible = false;
                ACQ.openACQ(FName[0]);
                Console.WriteLine(FName[0]);
                if (FName.Length > 1)
                {
                    ACQ.AppendACQ(FName[1]);
                }
                ACQ.VisibleChans = ACQ.Chans;
                ACQ.SetDispLength(MaxDispSize);

                ACQ.VisibleChans = ACQ.Chans;
                ACQ.SetDispLength(MaxDispSize);
                frm = new OpenFrm(BaseName, Reviewer, ReviewNotes, PercentCompletion, ACQ.TotFileTime, LastReview, LastOpen, CrashWarning, Compressed);
                frm.ShowDialog();
                Reviewer = frm.GetReviewer();
                Reviewing = frm.GetReviewing();
                if (Reviewing)
                {
                    //ReviewLogging = true;

                }
                frm.Dispose();
                CManage.ActiveForm.Text = "Seizure Playback - " + BaseName.Substring(4, 2) + "/" + BaseName.Substring(6, 2)
                    + "/" + BaseName.Substring(0, 4) + " - " + BaseName.Substring(9, 2) + ":" + BaseName.Substring(11, 2) + ":" + BaseName.Substring(13, 2);
                TimeBar.Minimum = 0;
                TimeBar.Maximum = ACQ.TotFileTime;
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

                        TimeSpan t;
                        int sT;
                        int tL;
                        int C;
                        TimeSpan.TryParse(TmpStr[3], out t);
                        sT = (int)t.TotalSeconds;
                        //Console.WriteLine(t);
                        //Console.WriteLine(sT);
                        int.TryParse(TmpStr[4], out tL);
                        int.TryParse(TmpStr[0], out C);
                        C = C - 1;
                        SeizureHighlight tempSH = new SeizureHighlight(C, sT, tL);
                        ACQ.SeizureHighlights.Add(tempSH);

                        SzInfoIndex++;
                    }
                    Console.WriteLine(ACQ.SeizureHighlights.Count);

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
                    ACQ.Position = (int)Math.Floor((PercentCompletion * (double)ACQ.TotFileTime) / (double)100);
                    Step = MaxDispSize;
                }

                string[] detName = Directory.GetFiles(Path, "*.det");           //this section autoloads a .det file if it exists within the directory.
                if (detName.Length != 0)
                {

                    DSF.OpenFile(detName[0]);

                    DetSezLabel.Text = ".det Loaded";
                    fastReviewCounter = 0;

                }
                else DetSezLabel.Text = ".det Not Found";

                if (PercentCompletion == 100)
                {
                    DetSezLabel.Text = "Finished!";
                    ColorClear.BackColor = Color.Green;
                }
            }
            if (ACQ.Loaded) ACQ.drawbuffer();
        }








        private void MouseDownHandler(object sender, MouseEventArgs e)
        {

            if (vLoading) return;

            if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && ((e.Y < graph.Y2) || (FastReviewState && e.Y < FRgraph.Y2)))
            {
                if (FastReviewState)
                {
                    int X = 1;
                    if (e.X < (((FRgraph.X2 - FRgraph.X1) / 2) + FRgraph.X1))
                    {
                        X = 0;
                    }
                    int Y = (e.Y - FRgraph.Y1);
                    Y = Y / ((FRgraph.Y2 - FRgraph.Y1) / (numPerPage / 2));
                    //if (DSF.ChangeDisplaySeizure((FastReviewPage * 16) + (Y * 2) + X))
                    //loadVid(DSF.GetChannelNumber((FastReviewPage * numPerPage) + (Y * 2) + X));
                    if (DSF.ChangeDisplaySeizure((FastReviewPage * numPerPage) + (Y * 2) + X))
                    {

                        FastReviewChange = true;
                    }

                }
                else
                {
                    //JOSH

                    Paused = true;
                    OffsetBox.Enabled = true;
                    int TempChan = (int)((float)ACQ.VisibleChans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                    if (ACQ.Randomized)
                    {
                        ACQ.SelectedChan = ACQ.RandomOrder[TempChan];
                        loadVid(ChanPos[TempChan]);
                    }
                    else
                    {
                        ACQ.SelectedChan = ChanPos[TempChan];
                        loadVid(ChanPos[TempChan]);

                    }
                    HighlightStart = (int)((float)MaxDispSize * (float)(e.X - graph.X1) / (graph.X2 - graph.X1));
                    Highlighting = true;
                    HighlightEnd = HighlightStart;
                    ACQ.sethighlight(HighlightStart, HighlightEnd);
                    if (player != null)
                        player.Pause();
                }
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
            //if (vLoading) return;
            if (FastReviewState) return;
            if (ACQ.Loaded)
                if ((e.X > graph.X1) && (e.X < graph.X2) && (e.Y > graph.Y1) && (e.Y < graph.Y2))
                {
                    if ((HighlightEnd - HighlightStart) < MaxDispSize / 30)
                    {
                        HighlightLabel.Text = "";
                        int TempChan = (int)((float)ACQ.VisibleChans * (float)(((float)e.Y - (float)graph.Y1) / (float)(graph.Y2 - graph.Y1)));
                        int XStart = (int)((float)MaxDispSize * (float)(e.X - graph.X1) / (graph.X2 - graph.X1));
                        if (ACQ.Randomized)
                        {
                            ACQ.SelectedChan = ACQ.RandomOrder[TempChan];
                        }
                        else
                        {
                            ACQ.SelectedChan = ChanPos[TempChan];
                        }
                        OffsetBox.Text = VideoOffset[ACQ.SelectedChan].ToString();
                        ACQ.Position = ACQ.Position - Step + XStart;
                        Step = XStart;
                        SeekToCurrentPos();
                        QuitHighlight();
                        Paused = false;
                        RealTime = true;
                        Redraw = true;
                    }
                    else
                    {
                        Highlighting = false;
                    }
                }
        }
        private void SeekToCurrentPos(bool AVIload = true)
        {
            int FNum = 1;
            string Fname;
            Redraw = true;
            //Frame rate is actually 30.3, but listed as 30 in the avi. To seek to the proper time, need to adjust for that factor.
            //Switch to float to do decimal math, switch back to integer for actual ms.             
            long TimeSeek;
            if (AVIMode == "mp4")
            {
                TimeSeek = (long)((float)ACQ.Position * 1000F);
            }
            else
            {
                TimeSeek = (long)((float)ACQ.Position * 1000F * (1F + VideoOffset[ACQ.SelectedChan]));
            }
            bool AVILoaded = false;
            bool pass = false;

            CurrentAVI = "";

            if (AVIMode == "avi")
            {

                while (TimeSeek > AVILengths[ACQ.SelectedChan, FNum - 1])
                {
                    //Console.WriteLine(TimeSeek);
                    TimeSeek -= AVILengths[ACQ.SelectedChan, FNum - 1];
                    FNum++;
                    if (AVILengths[ACQ.SelectedChan, FNum - 1] == 0)// no avi file
                    {
                        Console.WriteLine("No Avi File Found");
                        pass = true;
                        break;
                    }
                }
                Fname = Path + "\\" + BaseName + string.Format("_{0:d2}", ACQ.SelectedChan) + string.Format("_{0:d4}.avi", FNum);
            }
            else
            {
                if (this.VideoFix.Checked)
                {
                    int FixedChan = 0;
                    FixedChan = CamerAssc[CamerAssc[ACQ.SelectedChan]];
                    loadVid(FixedChan);
                    FNum = 0;
                    while (TimeSeek > AVILengths[FixedChan, FNum])
                    {
                        TimeSeek -= AVILengths[FixedChan, FNum];
                        FNum++;
                        if (AVILengths[FixedChan, FNum] == 0)// no avi file
                        {
                            pass = true;
                            break;
                        }
                    }
                    if (FNum == 0)
                    {
                        Fname = Path + "\\" + BaseName + string.Format("_{0:d3}", FixedChan) + ".mp4";
                    }
                    else
                    {
                        Fname = Path + "\\" + BaseName + string.Format("_{0:d3}", FixedChan) + "." + FNum.ToString() + ".mp4";
                    }
                }
                else
                {
                    FNum = 0;
                    while (TimeSeek > AVILengths[ACQ.SelectedChan, FNum])
                    {
                        TimeSeek -= AVILengths[ACQ.SelectedChan, FNum];
                        FNum++;
                        if (AVILengths[ACQ.SelectedChan, FNum] == 0)// no avi file
                        {
                            pass = true;
                            break;
                        }
                    }
                    
                    if (FNum == 0)
                    {
                        Fname = Path + "\\" + BaseName + string.Format("_{0:d3}", ACQ.SelectedChan) + ".mp4";
                    }
                    else
                    {
                        Fname = Path + "\\" + BaseName + string.Format("_{0:d3}", ACQ.SelectedChan) + "." + FNum.ToString() + ".mp4";
                    }
                }
            }


            if (!pass)
            {


                //if (player != null) player.Stop();
                //if (media !=null) media.Dispose();
                media = new VlcMedia(instance, Fname);
                Subtractor = TimeSeek;
                if (player == null)
                {
                    player = new VlcMediaPlayer(media);
                    player.Drawable = VideoPanel.Handle;
                }
                else player.Media = media;
                player.Play();
                AVILoaded = true;
                CurrentAVI = Fname;
                player.seek(TimeSeek);

            }
        }


        private void SpeedUp_Click(object sender, EventArgs e)
        {
            ACQ.SelectedChan = -1;
            OffsetBox.Enabled = false;
            RealTime = false;
            if (player != null)
                player.Stop();

        }

        private void Rewind_Click(object sender, EventArgs e)
        {


            //ACQ.Position = Math.Max(0, ACQ.Position - MaxDispSize);
            //if (player != null)
            //    player.seek(player.getpos() - MaxDispSize * 1000);
            //Step = MaxDispSize;

            ACQ.Position = Math.Max(0, ACQ.Position - RewindVal);
            if (player != null)
                player.seek(player.getpos() - RewindVal * 1000);
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
            int[] TimeScales = { 30, 60, 120, 300, 600, 1800, 3600 };
            MaxDispSize = TimeScales[TimeBox.SelectedIndex];
            RewindVal = MaxDispSize;

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
                P.AVIMode = AVIMode;
                P.VideoCapture = VideoCapture;
                if (AVIMode == "mp4")
                {
                    P.outfile = CurrentAVI.Substring(CurrentAVI.LastIndexOf("\\") + 1, 19);
                }
                else
                {
                    P.outfile = CurrentAVI.Substring(CurrentAVI.Length - 27, 18);
                }
                P.FPath = CurrentAVI.Substring(0, CurrentAVI.LastIndexOf("\\") + 1) + "Seizure";
                SeizureCount[ACQ.SelectedChan]++;
                P.length = (int)((float)(HighlightEnd - HighlightStart + 1) * 1.01F);
                TimeSpan t = TimeSpan.FromSeconds(P.StartTime);
                string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                P.outfile += "_S" + SeizureCount[ACQ.SelectedChan].ToString();
                P.Sz = (ACQ.SelectedChan + 1).ToString() + ", " + ACQ.ID[ACQ.SelectedChan] + ", " + SeizureCount[ACQ.SelectedChan].ToString() + ", "
                    + answer + ", " + (HighlightEnd - HighlightStart + 1).ToString() + " , ";
                P.CurrentAVI = CurrentAVI;
                P.Subtractor = Subtractor;
                //P.subVidOffset = DetermineRVT(ACQ.SelectedChan, P.StartTime * 1000);
                              
                P.X264path = X264path;
                P.ACQ = ACQ;
                P.VideoOffset = VideoOffset[ACQ.SelectedChan];
                SzPrompt Frm = new SzPrompt();
                Frm.Pass = P;
                Frm.CaptureLen.Text = "Capture Length: " + P.length + " Seconds";
                if (P.length < 10) Frm.ShortCapWarning.Show();
                Frm.ShowDialog(this);
                if (Frm.Ok)
                {
                    string Result = Frm.Result;
                    SzTxt.WriteLine(Result);
                    if (SRF != null) { SRF.Add(Result); }
                    SzInfo[SzInfoIndex] = Result;
                    SzInfoIndex++;
                    if (VideoCapture != Frm.VideoCapture) //Are we saving videos? 
                    {
                        VideoCapture = Frm.VideoCapture;
                        INISave();
                    }
                    SeizureHighlight tempSH = new SeizureHighlight(ACQ.SelectedChan, P.StartTime, P.length);
                    ACQ.SeizureHighlights.Add(tempSH);
                    //ACQ.AddSz(HighlightStart, HilightEnd); 
                }
                Frm.Dispose();
                Step = MaxDispSize;
                QuitHighlight();
            }
        }


        public long DetermineRVT(int chan, long ttms)
        {
            int FNum = 0;
            while (ttms > AVILengths[chan, FNum])
            {
                ttms -= AVILengths[chan, FNum];
                FNum++;
            }
            return ttms;
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
                //Console.WriteLine("checked!");
                VChan = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (VisChecks[i].Checked)
                    {
                        //if (HCL.Contains((i + 1)))
                        //{
                        //    HCL.Remove((i + 1));
                        //    checkedChange = true;
                        //}
                        ACQ.HideChan[i] = false;
                        ChanPos[VChan] = i;
                        VChan++;
                    }
                    else
                    {

                        ACQ.HideChan[i] = true;
                        //if (!HCL.Contains((i + 1)))
                        //{
                        //    HCL.Add((i + 1));
                        //    checkedChange = true;
                        //}
                    }
                }
                ACQ.VisibleChans = VChan;
                ACQ.ResetScale();
                Step = MaxDispSize;
            }
        }

        private void VisChan_CheckedChanged_List(object sender, EventArgs e, int chanPass, bool chanClicked)
        {

            if (!ACQ.Loaded) return;

            if (!SuppressChange)
            {
                if (!HCL.Contains(chanPass) && !chanClicked)
                {
                    ChanZooms[chanPass - 1].Enabled = false;
                    HCL.Add(chanPass);
                    checkedChange = true;
                    //Console.WriteLine("Channel " + chanPass + " Hidden");
                    VLCisLoaded[chanPass] = true;
                }
                if (HCL.Contains(chanPass) && chanClicked)
                {
                    ChanZooms[chanPass - 1].Enabled = true;
                    HCL.Remove(chanPass);
                    checkedChange = true;
                    //Console.WriteLine("Channel " + chanPass + " Visible");
                    VLCisLoaded[chanPass] = false;
                }

                DSF.HCLSync(HCL);


            }

        }

        private void ZoomScale_Scroll(object sender, EventArgs e)
        {

            if (!FastReviewState)
            {
                ACQ.Zoom = (float)ZoomScale.Value / 10;
                FRZoomBar.Value = ZoomScale.Value;
                Redraw = true;
            }
            if (FastReviewState)
            {
                ACQ.Zoom = (float)FRZoomBar.Value / 10;
                FastReviewChange = true;
                ZoomScale.Value = FRZoomBar.Value;
            }


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
            ACQ.Position = (int)Time.TotalSeconds; //doesn't draw rectangles in the right place
            Step = MaxDispSize;

            SeekToCurrentPos();
            //ACQ.drawbuffer();
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

            if (!ACQ.Loaded) return;
            OpenFileDialog FD = new OpenFileDialog();
            FD.InitialDirectory = AVIFiles[0].Substring(0, AVIFiles[0].LastIndexOf("\\"));
            FD.DefaultExt = "det";
            FD.Filter = "Detection Files (*.det)|*.det|All Files (*.*)|*.*";
            DialogResult Res = FD.ShowDialog();
            if (Res == DialogResult.OK)
            {
                DSF.OpenFile(FD.FileName);
            }
            DetSezLabel.Text = ".det Loaded";

            fastReviewCounter = 0;
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
            ACQ.UpdateIDs(); //Write the IDs to the ACQ file            

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

            if (CManage.ActiveForm != null)  //if (!(MainForm.ActiveForm.WindowState == FormWindowState.Minimized) && ResizeBool
            {
                Step = MaxDispSize;
                Redraw = true;
                graph.X2 = CManage.ActiveForm.Size.Width - 10; //Eat at joes
                graph.Y2 = VideoPanel.Location.Y - 11; //this does whatever it does 
                ACQ.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, FRZoomBlock.Location.Y - 20);    //Create the graphics box to display EEG.       
                VideoPanel.Location = new Point(VideoPanel.Location.X, CManage.ActiveForm.Height - 395);
                TimeBar.Size = new Size(CManage.ActiveForm.Size.Width - TimeBar.Location.X - 5, TimeBar.Size.Height);
                if (ACQ.Loaded) { ACQ.RefreshDisplay(); }
                CManage.ActiveForm.Refresh();
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

        private void VideoCreate_Click(object sender, EventArgs e)
        {
            if (CurrentAVI == "")
                return;
            int Start = (ACQ.Position - Step + HighlightStart);
            long Seek;
            Seek = (long)((float)Start * 1000F * (1F + VideoOffset[ACQ.SelectedChan]) - Subtractor);
            VideoCreator frm = new VideoCreator(ACQ.GetData(ACQ.SelectedChan, Start, HighlightEnd - HighlightStart + 1), HighlightEnd - HighlightStart + 1, CurrentAVI, Seek);
            frm.Show();
        }

        private void AddtoProj_Click(object sender, EventArgs e)
        {
            Project pjt;
            Paused = true;
            ACQ.Loaded = false;
            if (player != null)
                player.Stop();
            ACQ.closeACQ();
            SzTxt.Close();
            ACQ.cleargraph();
            this.Text = "Closing File...";
            Thread.Sleep(500);
            this.Text = "Seizure Video Playback";
            AddtoProject F = new AddtoProject(CurrentProject);
            F.ShowDialog();
            if (F.passed)
            {
                CurrentProject = F.returnstring;
                pjt = new Project(CurrentProject);
                pjt.Open();
                pjt.ImportDirectory(Path);
                pjt.Save();
                INISave();

            }
        }

        private void Download_ACQ_Click(object sender, EventArgs e)
        {
            GetACQ F = new GetACQ();
            F.ShowDialog();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (!DSF.isLoaded) return;

            if (FastReviewState)
            {
                if ((FastReviewPage * numPerPage) + numPerPage > DSF.Count) return;

                FastReviewPage++;

                //DetSezLabel.Text = ((FastReviewPage * 16) + 1).ToString() + " to " + ((FastReviewPage + 1) * 16 + " of " + DSFoCount).ToString();
                //DetSezLabel.Text = ((FastReviewPage * 16) + 1).ToString() + " to " + ((FastReviewPage + 1) * 16).ToString() + " of " + (DSF.Count).ToString();
                string displayPageMin = MinMaxPage("min");
                string displayPageMax = MinMaxPage("max");
                FRPageNum.Text = (displayPageMin + " to " + displayPageMax + " of " + (DSF.Count).ToString());
                FastReviewChange = true;
                fastReviewLastPage++;

            }
            else
            {
                bool pass = false;
                DetectedSeizureType Sz = new DetectedSeizureType(0, 0, true);
                while (!pass)
                {
                    if (!DSF.Inc())
                    {
                        DetSezLabel.Text = "Finished!";
                        PercentCompletion = 100;
                        ColorClear.BackColor = Color.Green;
                        UpdateReviewINI(BioINI);
                        Paused = true;
                        RealTime = false;
                        ACQ.Position = (int)ACQ.TotFileTime;
                        Step = MaxDispSize;
                        SeekToCurrentPos(false);
                        if (player != null) player.Pause();

                        return;
                    }
                    Sz = DSF.GetCurrentSeizure();
                    if ((!ACQ.HideChan[Sz.Channel - 1]) && Sz.Display)
                        pass = true;
                }
                //DetSezLabel.Text = (DSF.SeizureNumber + 1).ToString() + " of " + DSF.Count.ToString();
                DetSezLabel.Text = DSF.FRIndex() + " of " + (DSF.IsDisplayed()).ToString();
                regularReviewReturn = (Math.Min(regularReviewReturn + 1, DSF.Count - 1));
                ACQ.SelectedChan = Sz.Channel - 1;
                ACQ.Position = Math.Max(0, Sz.TimeInSec - 30);
                Step = MaxDispSize;
                loadVid(ACQ.SelectedChan);
                if (player != null)
                    player.Stop();
                if (player != null) SeekToCurrentPos();
                if (player != null) player.Play();
                QuitHighlight();
                Paused = false;
                RealTime = true;
            }

        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (!DSF.isLoaded) return;
            if (FastReviewState)
            {
                if (FastReviewPage == 0) return;
                FastReviewPage--;
                //DetSezLabel.Text = ((FastReviewPage * 16) + 1).ToString() + " to " + ((FastReviewPage + 1) * 16 + " of " + DSFoCount).ToString();
                //DetSezLabel.Text = ((FastReviewPage * 16) + 1).ToString() + " to " + ((FastReviewPage + 1) * 16).ToString() + " of " + (DSF.Count).ToString();
                string displayPageMin = MinMaxPage("min");
                string displayPageMax = MinMaxPage("max");
                FRPageNum.Text = (displayPageMin + " to " + displayPageMax + " of " + (DSF.Count).ToString());
                FastReviewChange = true;
                fastReviewLastPage--;
                return;
            }
            bool pass = false;
            DetectedSeizureType Sz = new DetectedSeizureType(0, 0, true);
            while (!pass)
            {
                //if (!DSF.Dec()) return;
                if (PercentCompletion == 100 && finsihedReview)
                {
                    ACQ.SelectedChan = 0;
                    ACQ.Position = 0;
                    Step = MaxDispSize;
                    if (player != null)
                        player.Stop();
                    SeekToCurrentPos(false);
                    QuitHighlight();
                    Paused = false;
                    RealTime = true;
                    finsihedReview = false;
                    //DetSezLabel.Text = (0 + " of " + DSF.Count.ToString());
                    DetSezLabel.Text = (0 + " of " + (DSF.IsDisplayed()).ToString());
                    return;
                }
                if (!DSF.Dec())
                {
                    ACQ.Position = 0;
                    Step = MaxDispSize;
                    if (player == null) return;
                    if (player != null)
                        player.Stop();
                    SeekToCurrentPos(false);
                    QuitHighlight();
                    Paused = false;
                    RealTime = true;
                    //DetSezLabel.Text = (0 + " of " + DSF.Count.ToString());
                    DetSezLabel.Text = (0 + " of " + (DSF.IsDisplayed()).ToString());
                    return;
                }
                Sz = DSF.GetCurrentSeizure();
                if (!ACQ.HideChan[Sz.Channel - 1] && Sz.Display)  //This adds the functionality that next has, skipping to the selected seizures. 
                    pass = true;

            }
            //DetSezLabel.Text = (DSF.SeizureNumber + 1).ToString() + " of " + DSF.Count.ToString();
            DetSezLabel.Text = DSF.FRIndex() + " of " + (DSF.IsDisplayed()).ToString();
            regularReviewReturn = (Math.Max(regularReviewReturn - 1, 0));
            ACQ.SelectedChan = Sz.Channel - 1;
            ACQ.Position = Math.Max(0, Sz.TimeInSec - 30);
            Step = MaxDispSize;
            if (player != null)
                player.Stop();
            SeekToCurrentPos(false);
            QuitHighlight();
            Paused = false;
            RealTime = true;
        }


        private void Randomization_CheckedChanged(object sender, EventArgs e)
        {
            if (!ACQ.Loaded)
            {
                Randomization.Checked = false;
                return;
            }
            if (Randomization.Checked)
            {
                for (int i = 0; i < 16; i++)
                {
                    VisChecks[i].Enabled = false; //Don't want user modifying visible channels after randomization
                }
                RvwSz.Enabled = false;
                ACQ.Randomized = true;
                ACQ.Randomizer(); //Randomize Channels           
            }
            else
            {
                ACQ.Randomized = false;
                RvwSz.Enabled = true;
                for (int i = 0; i < 16; i++)
                {
                    VisChecks[i].Enabled = true; //Re-enable ability to turn on and off channels 
                }
            }
        }

        private void FixChan_Click(object sender, EventArgs e)
        {
            /* FixChan F = new FixChan();
             F.ShowDialog();
             if (F.pass)
             {
                 ACQ.FixChans(F.FixNum); 
             }
             F.Dispose();
           */
        }

        private void TelemetryBox_CheckedChanged(object sender, EventArgs e)
        {
            ACQ.Telemetry = TelemetryBox.Checked;
            INISave();
        }



        private void FastReview_Click(object sender, EventArgs e)
        {


            DSF.HCLSync(HCL);



            if (!DSF.isLoaded) return; //Don't want to go into fastreview mode if file not loaded
            //if (!Paused) return; //If things are actively playing, it's gonna suck to deal with. 
            if (!Paused) //possible solution so it auto pauses? There may be more to add but this seems to work nicely
            {
                Paused = true;
                if (player != null)
                    player.Pause();
            }
            bool del;


            if (!FastReviewState)
            {
                TimeBox.SelectedItem = "60s";
                //MaxDispSize = 60;

                FRView(true);






                if (fastReviewCounter == 0)  //we do want to initially reset the displayed seizures so that all of them aren't highlighted, but this only has to happen when you open fast review for the first time.
                {

                    DSF.ResetDisplay();
                    fastReviewCounter = 1;
                }

                if (DSF.IsDisplayed() > 0)
                {
                    del = DeleteMessageBox();
                    if (del == true)
                    {
                        fastReviewLastPage = 0;
                        DSF.ResetDisplay();

                    }
                }
                //int tempHid = HCL.Count;
                //HCL = HiddenChannelList();
                //if (HCL.Count > tempHid)
                //{
                //    if (RemoveChannelMessage())
                //    {
                //        for (int i = 0; i < HCL.Count; i++)
                //        {
                //            DSF.RemoveChannel(HCL[i]);
                //        }
                //    }

                //}
                //if (HCL.Count < tempHid)
                //{
                //    string[] detName = Directory.GetFiles(Path, "*.det");           //this section autoloads a .det file if it exists within the directory.
                //    if (detName.Length != 0)
                //    {
                //        DSF.OpenFile(detName[0]);                      
                //    }
                //}

                if (checkedChange)
                {

                    DSF.ResetDFS();

                    checkedChange = false;
                    if (DSF.Count == 0)
                    {
                        Console.WriteLine("NO SEIZURES VISIBLE!");
                        return;
                    }

                }




                DSF.SetSeizureNumber(0);

                FastReviewPage = fastReviewLastPage;
                //DSF.VisCount();

                //below is code used to show page numbers in fast review state
                //DetSezLabel.Text = ((FastReviewPage * 16) + DSFoCount).ToString() + " to " + ((FastReviewPage + 1) * 16 + DSFoCount).ToString();
                //DetSezLabel.Text = ((FastReviewPage * 16) + 1).ToString() + " to " + ((FastReviewPage + 1) * 16).ToString() + " of " + (DSF.Count).ToString();
                string displayPageMin = MinMaxPage("min");
                string displayPageMax = MinMaxPage("max");
                FRPageNum.Text = (displayPageMin + " to " + displayPageMax + " of " + (DSF.Count).ToString());

                FastReviewState = true;
                FastReviewChange = true;
                //Disable Buttons 
                Play.Enabled = false;
                SpeedUp.Enabled = false;
                Rewind.Enabled = false;
            }
            else
            {


                FRView(false);
                g.Clear(Color.Black);



                DSF.SetSeizureNumber(0);


                FastReviewState = false;
                Play.Enabled = true;
                SpeedUp.Enabled = true;
                Rewind.Enabled = true;

                /*
                 * Experimental autoreturn to first selected seizure
               */
                DetectedSeizureType Sz;

                Sz = DSF.GetCurrentSeizure();
                bool pass = false;

                if ((!ACQ.HideChan[Sz.Channel - 1]) && Sz.Display) pass = true;
                if (DSF.IsDisplayed() == 1)
                {
                    DetSezLabel.Text = "Finished!";
                    PercentCompletion = 100;
                    UpdateReviewINI(BioINI);
                    ColorClear.BackColor = Color.Green;
                }
                if (DSF.IsDisplayed() == 0)
                {
                    bool MarkAs = MarkAsCompleteBox();
                    if (MarkAs)
                    {
                        DetSezLabel.Text = "Finished!";
                        PercentCompletion = 100;
                        ColorClear.BackColor = Color.Green;
                        UpdateReviewINI(BioINI);
                        Paused = true;
                        RealTime = false;
                        ACQ.Position = (int)ACQ.TotFileTime;
                        Step = MaxDispSize;

                        if (player != null) player.Pause();

                        return;
                    }
                }

                while (!pass)
                {

                    if (!DSF.Inc())
                    {
                        DetSezLabel.Text = "None Selected";
                        ACQ.Position = 0;
                        Step = MaxDispSize;
                        if (player != null)
                            player.Stop();
                        Paused = true;
                        RealTime = true;
                        if (player != null)
                            player.Stop();
                        return;
                    }
                    Sz = DSF.GetCurrentSeizure();
                    if ((!ACQ.HideChan[Sz.Channel - 1]) && Sz.Display)
                        pass = true;
                    // if (regularReviewReturn != 0) loadVid(Sz.Channel);
                }

                //DetSezLabel.Text = (DSF.SeizureNumber + 1).ToString() + " of " + DSF.Count.ToString();
                DetSezLabel.Text = DSF.FRIndex().ToString() + " of " + (DSF.IsDisplayed()).ToString();
                ACQ.SelectedChan = Sz.Channel - 1;
                ACQ.Position = Math.Max(0, Sz.TimeInSec - 30);
                loadVid(ACQ.SelectedChan);
                Step = MaxDispSize;
                if (player != null)
                    player.Stop();
                if (player != null) SeekToCurrentPos();
                if (player != null) player.Play();
                QuitHighlight();
                Paused = false;
                RealTime = true;


            }
        }


        /*
        deleteMessageBox is the prompt message for the user to delete fastreview save data.  
         
        */

        private bool MarkAsCompleteBox()
        {

            string message = "\t You Have Not Selected Any Seizures \n Would You Like to Finish and Mark this File as Complete?";
            string caption = "Seizure Review";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult answer;
            var answer = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                return true;
            }
            else return false;


        }

        private bool DeleteMessageBox()
        {

            string message = "Do you want to delete all previous progress?";
            string caption = "Fast Review Reset";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult answer;
            var answer = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                return true;
            }
            else return false;


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DetSezLabel_Click(object sender, EventArgs e)
        {
            SzDetPrompt F = new SzDetPrompt();
            F.ShowDialog();
            if (F.num != -1)
            {
                DSF.SeizureNumber = F.num - 2;
                Next_Click(null, null);
            }

        }

        private void SwitchChan_CheckedChanged(object sender, EventArgs e, bool check)
        {
            ACQ.SwapIDs(sender, e, check);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            switch (comboBox1.SelectedIndex)
            {
                case 0: //Compression Manager
                    CompressionManager frm = new CompressionManager();
                    frm.Show();
                    break;

                case 1: //Compress Directory 
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
                    break;

                case 2: //Enter Notes
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
                    break;
                case 3: //Fix Channel at Timepoint 
                    /* FixChan F = new FixChan();
                        F.ShowDialog();
                        if (F.pass)
                         {
                             ACQ.FixChans(F.FixNum); 
                             }
                             F.Dispose();
                            */
                    break;
                case 4: //Download ACQ 
                    GetACQ F = new GetACQ();
                    F.ShowDialog();
                    break;
                case 5: //Rename Channels 
                    RenameChans frm1 = new RenameChans(ACQ.Chans, ACQ.ID);
                    frm1.ShowDialog();
                    ACQ.UpdateIDs(); //Write the IDs to the ACQ file 
                    break;
                case 6: //Video Creator 
                    if (CurrentAVI == "")
                        return;
                    int Start = (ACQ.Position - Step + HighlightStart);
                    long Seek;
                    Seek = (long)((float)Start * 1000F * (1F + VideoOffset[ACQ.SelectedChan]) - Subtractor);
                    VideoCreator frm2 = new VideoCreator(ACQ.GetData(ACQ.SelectedChan, Start, HighlightEnd - HighlightStart + 1), HighlightEnd - HighlightStart + 1, CurrentAVI, Seek);
                    frm2.Show();
                    break;
                case 7:
                    bool a = true;
                    for (int i = 0; i < 16; i++)
                    {
                        if (!VLCisLoaded[i]) a = false;
                    }
                    if (a == false) loadVid(-1);
                    break;
                case 8: //Channel Zoom
                    if (ACQ.MasterZoom)
                    {
                        comboBox1.Items.RemoveAt(8);
                        comboBox1.Items.Insert(8, "Channel Zoom Control [\u2714]");
                        comboBox1.Text = "Channel Zoom Control [\u2714]";


                        ACQ.MasterZoom = false;
                        ZoomChanPanel.Show();
                        ZoomChanPanel.Enabled = true;

                        //ZoomChan1.Value = ZoomScale.Value;
                        //ZoomChan2.Value = ZoomScale.Value;
                        //ZoomChan3.Value = ZoomScale.Value;
                        //ZoomChan4.Value = ZoomScale.Value;
                        //ZoomChan5.Value = ZoomScale.Value;
                        //ZoomChan6.Value = ZoomScale.Value;
                        //ZoomChan7.Value = ZoomScale.Value;
                        //ZoomChan8.Value = ZoomScale.Value;
                        //ZoomChan9.Value = ZoomScale.Value;
                        //ZoomChan10.Value = ZoomScale.Value;
                        //ZoomChan11.Value = ZoomScale.Value;
                        //ZoomChan12.Value = ZoomScale.Value;

                        ZoomScale.Enabled = false;
                    }
                    else
                    {
                        comboBox1.Items.RemoveAt(8);
                        comboBox1.Items.Insert(8, "Channel Zoom Control [   ]");
                        comboBox1.Text = "Channel Zoom Control [   ]";
                        ACQ.MasterZoom = true;
                        ACQ.drawbuffer();
                        ZoomChanPanel.Hide();
                        ZoomChanPanel.Enabled = false;
                        ZoomScale.Enabled = true;

                    }
                    break;

            }
        }


        /*
        minMagePage is a string method that returns the lower bound and upper bound of the page you are on during fast review
        if you pass "min" it will give the lower bound and "max" will return what you think
        This method also checks if the numbers are over the total number of detections, in which case it will simply return the seizure count as a string
        This method is simply to make printing easier for me
         */
        public string MinMaxPage(string a) //must be "min" or "max"
        {

            if (a == "min")
            {
                if (((FastReviewPage * numPerPage) + 1) > DSF.Count) return (DSF.Count.ToString());
                return ((FastReviewPage * numPerPage) + 1).ToString();
            }
            if (a == "max")
            {
                if (((FastReviewPage + 1) * numPerPage) > DSF.Count) return (DSF.Count.ToString());
                return ((FastReviewPage + 1) * numPerPage).ToString();
            }
            else
            {
                return null;
            }
        }

        private void HighlightLabel_Click(object sender, EventArgs e)
        {

        }




        private void numPerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int temp = int.Parse(this.numPerBox.SelectedItem.ToString());

            int FRPtemp = numPerPage * FastReviewPage;
            FastReviewPage = FRPtemp / temp;

            numPerPage = temp;
            ACQ.numPerPage = temp;

            switch (numPerBox.SelectedIndex)
            {
                case 0:
                    ACQ.frepos = 26;
                    break;

                case 1:
                    ACQ.frepos = 32;
                    break;

                case 2:
                    ACQ.frepos = 42;
                    break;

                case 3:
                    ACQ.frepos = 52;
                    break;

                case 4:
                    ACQ.frepos = 64;
                    break;

                case 5:
                    ACQ.frepos = 86;
                    break;

                case 6:
                    ACQ.frepos = 120;
                    break;

            }

            FRPageNum.Text = (MinMaxPage("min") + " to " + MinMaxPage("max") + " of " + (DSF.Count).ToString());

            FastReviewChange = true;

        }

        //private void LoadAll_Click(object sender, EventArgs e)
        //{
        //    bool a= true;
        //    for (int i = 0; i < 16; i++)
        //    {
        //        if (!VLCisLoaded[i]) a = false;
        //    }
        //    if (a == false) loadVid(-1);
        //}

        private void VideoFix_CheckedChanged(object sender, EventArgs e)
        {
            //bool a = true;
            //for (int i = 0; i < 16; i++)
            //{
            //    if (!VLCisLoaded[i]) a = false;
            //}
            //if (a == false) loadVid(-1);

        }

        private void SwitchChan_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void ChanZoomCheck_CheckedChanged(object sender, EventArgs e)
        //{

        //    if (this.ChanZoomCheck.Checked)
        //    {
        //        ACQ.MasterZoom = false;
        //        ZoomChanPanel.Show();
        //        ZoomChanPanel.Enabled = true;

        //        ZoomChan1.Value = ZoomScale.Value;
        //        ZoomChan2.Value = ZoomScale.Value;
        //        ZoomChan3.Value = ZoomScale.Value;
        //        ZoomChan4.Value = ZoomScale.Value;
        //        ZoomChan5.Value = ZoomScale.Value;
        //        ZoomChan6.Value = ZoomScale.Value;
        //        ZoomChan7.Value = ZoomScale.Value;
        //        ZoomChan8.Value = ZoomScale.Value;
        //        ZoomChan9.Value = ZoomScale.Value;
        //        ZoomChan10.Value = ZoomScale.Value;
        //        ZoomChan11.Value = ZoomScale.Value;
        //        ZoomChan12.Value = ZoomScale.Value;

        //        ZoomScale.Enabled = false;
        //    } else
        //    {
        //        ACQ.MasterZoom = true;
        //        ZoomChanPanel.Hide();
        //        ZoomChanPanel.Enabled = false;
        //        ZoomScale.Enabled = true;
        //        ACQ.drawbuffer();
        //    }

        //}

        private void ZoomChan1_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[0] = (float)ZoomChan1.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }


        private void ZoomChan2_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[1] = (float)ZoomChan2.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }


        private void ZoomChan3_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[2] = (float)ZoomChan3.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan4_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[3] = (float)ZoomChan4.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan5_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[4] = (float)ZoomChan5.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan6_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[5] = (float)ZoomChan6.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan7_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[6] = (float)ZoomChan7.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan8_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[7] = (float)ZoomChan8.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan9_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[8] = (float)ZoomChan9.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan10_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[9] = (float)ZoomChan10.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan11_Scroll(object sender, EventArgs e)
        {
            ACQ.CurrentChannelZoom[10] = (float)ZoomChan11.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void ZoomChan12_Scroll(object sender, EventArgs e)

        {
            ACQ.CurrentChannelZoom[11] = (float)ZoomChan12.Value / 10;
            Redraw = true;
            if (FastReviewState) FastReviewChange = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ZoomScale.Value = 10;
            ACQ.Zoom = 1;
            Redraw = true;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        public void Rewind_Change(Object sender, EventArgs e, int dir) //1 for increase, 0 for decrease
        {
            if (dir == 1)
            {
                if (RewindVal * 2 >= MaxDispSize)
                {
                    RewindVal = MaxDispSize;
                    Rewind.Text = "Rewind " + MaxDispSize.ToString() + "s";
                    return;
                }
                if (RewindVal == 10) RewindVal = 15;
                RewindVal = (RewindVal - (RewindVal % 5));
                RewindVal *= 2;
                Rewind.Text = "Rewind " + RewindVal + "s";
            }
            else if (dir == 0)
            {
                if (RewindVal <= 5) return;
                if (RewindVal <= 10)
                {
                    RewindVal = 5;
                    Rewind.Text = "Rewind " + RewindVal + "s";
                    return;
                }
                if (RewindVal <= 15)
                {
                    RewindVal = 10;
                    Rewind.Text = "Rewind " + RewindVal + "s";
                    return;
                }

                RewindVal /= 2;
                RewindVal = RewindVal - RewindVal % 15;
                Rewind.Text = "Rewind " + RewindVal + "s";

            }
            else return;


        }





        public void loadVid(int chanloop)
        {
            if (vLoading) return;
            if (chanloop >= 0)
            {
                if (VLCisLoaded[chanloop]) return;
            }
            vLoading = true;





            if (!ACQ.Loaded) return;
            string AVIname;
            VLoadWait vLoad = new VLoadWait();
            vLoad.LT.Text = "LOADING";
            vLoad.Show();



            //AVIFiles = Directory.GetFiles(Path, "*.avi");

            LoadText.Show();
            LoadText.BringToFront();

            //this.Refresh();
            AVILoadBar.Value = 0;
            AVILoadBar.Maximum = 30;
            AVILoadBar.Visible = true;

            if (player == null) Console.WriteLine("Player does not exist yet");


            if (chanloop >= 0)
            {

                long tottemp = 0;

                LoadText.Text = "Loading Video for Channel: " + ((int)chanloop + (int)1);
                if (!isMP4)
                {
                    vLoad.VideoLoadProgressBar.Maximum = 10;

                    for (int fileloop = 1; fileloop < 10; fileloop++)
                    {

                        AVILoadBar.Increment(3);
                        vLoad.VideoLoadProgressBar.Increment(1);
                        AVIname = AVINameList[chanloop, fileloop - 1];
                        if (File.Exists(AVIname))
                        {
                            //Console.WriteLine(AVIname);
                            media = new VlcMedia(instance, AVIname);

                            if (player == null)
                            {

                                player = new VlcMediaPlayer(media);
                                player.Drawable = VideoPanel.Handle;
                            }

                            else player.Media = media;
                            player.Play();
                            while (player.GetLengthMs() == 0)
                            { }
                            AVILengths[chanloop, fileloop - 1] = player.GetLengthMs();
                            totms += AVILengths[chanloop, fileloop - 1];
                            tottemp += AVILengths[chanloop, fileloop - 1];
                            vidRanges[chanloop, fileloop - 1] = tottemp;
                            //Console.WriteLine(AVIname + "  " + AVILengths[chanloop, fileloop - 1].ToString());
                            player.Stop();
                            media.Dispose();
                            // vLoad.VideoLoadProgressBar.Increment(1);
                            //AVILoadBar.Increment(3);
                        }
                    }
                    for (int fileloop = 10; fileloop < 30; fileloop++)
                    {
                        AVILengths[chanloop, fileloop - 1] = 0;
                    }
                    VLCisLoaded[chanloop] = true;

                }
                else
                {
                    vLoad.VideoLoadProgressBar.Maximum = 30;
                    for (int fileloop = 0; fileloop < 30; fileloop++)
                    {
                        vLoad.VideoLoadProgressBar.Increment(1);
                        AVIname = AVINameList[chanloop, fileloop];
                        if (File.Exists(AVIname))
                        {
                            //Console.WriteLine(AVIname);
                            media = new VlcMedia(instance, AVIname);
                            if (player == null)
                            {
                                player = new VlcMediaPlayer(media);
                                player.Drawable = VideoPanel.Handle;
                            }
                            else player.Media = media;
                            player.Play();
                            while (player.GetLengthMs() == 0)
                            { }
                            AVILengths[chanloop, fileloop] = player.GetLengthMs();
                            totms += AVILengths[chanloop, fileloop];
                            tottemp += AVILengths[chanloop, fileloop];
                            vidRanges[chanloop, fileloop] = tottemp;
                            player.Stop();
                            media.Dispose();

                            VLCisLoaded[chanloop] = true;
                            //vLoad.VideoLoadProgressBar.Increment(1);
                        }
                        else
                        {
                            AVILengths[chanloop, fileloop] = 0;
                        }
                    }

                }
                VLCisLoaded[chanloop] = true;
            }
            else
            {
                LoadText.Text = "Loading All Video Files";
                if (player != null)
                {
                    player.Dispose();
                }
                vLoad.VideoLoadProgressBar.Maximum = 30;
                for (chanloop = 0; chanloop < 16; chanloop++)
                {
                    long tottemp = 0;
                    AVILoadBar.Value = 0;
                    if (!isMP4)
                    {

                        for (int fileloop = 0; fileloop < 10; fileloop++)
                        {
                            //AVILoadBar.Increment(3);
                            //vLoad.VideoLoadProgressBar.Maximum = 10;
                            AVILoadBar.Increment(3);
                            //vLoad.VideoLoadProgressBar.Increment(1);
                            AVIname = AVINameList[chanloop, fileloop];
                            if (File.Exists(AVIname))
                            {
                                //Console.WriteLine(AVIname);
                                media = new VlcMedia(instance, AVIname);

                                if (chanloop == 0)
                                {

                                    player = new VlcMediaPlayer(media);
                                    player.Drawable = VideoPanel.Handle;
                                }

                                else player.Media = media;
                                player.Play();
                                while (player.GetLengthMs() == 0)
                                { }
                                AVILengths[chanloop, fileloop] = player.GetLengthMs();
                                totms += AVILengths[chanloop, fileloop];
                                tottemp += AVILengths[chanloop, fileloop];
                                vidRanges[chanloop, fileloop] = tottemp;
                                //Console.WriteLine(AVIname + "  " + AVILengths[chanloop, fileloop - 1].ToString());
                                player.Stop();
                                media.Dispose();
                                // vLoad.VideoLoadProgressBar.Increment(1);
                                //AVILoadBar.Increment(3);
                            }
                        }
                        for (int fileloop = 10; fileloop < 30; fileloop++)
                        {
                            AVILengths[chanloop, fileloop - 1] = 0;
                        }
                        vLoad.VideoLoadProgressBar.Increment(3);
                    }
                    else
                    {
                        //vLoad.VideoLoadProgressBar.Maximum = 30;
                        for (int fileloop = 0; fileloop < 30; fileloop++)
                        {
                            AVILoadBar.Increment(1);
                            //vLoad.VideoLoadProgressBar.Increment(1);
                            AVIname = AVINameList[chanloop, fileloop];
                            if (File.Exists(AVIname))
                            {
                                //Console.WriteLine(AVIname);
                                media = new VlcMedia(instance, AVIname);
                                if (chanloop == 0)
                                {
                                    player = new VlcMediaPlayer(media);
                                    player.Drawable = VideoPanel.Handle;
                                }
                                else player.Media = media;
                                player.Play();
                                while (player.GetLengthMs() == 0)
                                { }
                                AVILengths[chanloop, fileloop] = player.GetLengthMs();
                                totms += AVILengths[chanloop, fileloop];
                                tottemp += AVILengths[chanloop, fileloop];
                                vidRanges[chanloop, fileloop - 1] = tottemp;
                                player.Stop();
                                media.Dispose();

                                VLCisLoaded[chanloop] = true;
                                //vLoad.VideoLoadProgressBar.Increment(1);
                            }
                            else
                            {
                                AVILengths[chanloop, fileloop] = 0;
                            }
                            vLoad.VideoLoadProgressBar.Increment(1);
                        }
                    }
                    VLCisLoaded[chanloop] = true;
                }
            }


            vLoad.Dispose();

            AVILoadBar.Visible = false;

            //Console.WriteLine("Loaded VLC for channel " + chanloop);
            LoadText.Visible = false;

            vLoading = false;


        }





        public void FRView(bool FRMode)
        {
            if (FRMode)
            {

                if (ZoomChanPanel.Visible)
                {
                    ZoomChanPanel.Hide();
                    comboBox1.SelectedIndex = 8;
                }
                this.Play.Hide();
                this.Play.Enabled = false;
                this.VideoPanel.Hide();
                this.VideoPanel.Enabled = false;
                this.Open.Hide();
                this.Open.Enabled = false;
                this.Pause.Hide();
                this.Pause.Enabled = false;
                this.TimeBar.Hide();
                this.TimeBar.Enabled = false;
                this.Rewind.Hide();
                this.Rewind.Enabled = false;
                this.SpeedUp.Hide();
                this.SpeedUp.Enabled = false;
                this.TimeLabel.Hide();
                this.TimeLabel.Enabled = false;
                //this.TimeBox.Hide();
                //this.TimeBox.Enabled = false;
                this.label1.Hide();
                this.label1.Enabled = false;
                this.SzCaptureButton.Hide();
                this.SzCaptureButton.Enabled = false;
                this.DetectionLoadButton.Hide();
                this.DetectionLoadButton.Enabled = false;

                this.FastReview.Hide();
                this.FastReview.Enabled = false;
                this.button2.Hide();
                this.button2.Enabled = false;
                this.button3.Hide();
                this.button3.Enabled = false;
                this.RvwSz.Hide();
                this.RvwSz.Enabled = false;
                this.TelemetryBox.Hide();
                this.TelemetryBox.Enabled = false;
                this.Randomization.Hide();
                this.Randomization.Enabled = false;
                this.VideoFix.Hide();
                this.VideoFix.Enabled = false;
                this.comboBox1.Hide();
                this.comboBox1.Enabled = false;
                this.OffsetBox.Hide();
                this.OffsetBox.Enabled = false;
                //this.LoadAll.Hide();
                //this.LoadAll.Enabled = false;
                this.Rewind_ChangeBox.Hide();
                this.Rewind_ChangeBox.Enabled = false;


                this.FRButtonGroup.Show();
                this.FRButtonGroup.Enabled = true;
                this.FRZoomBlock.Show();
                this.FRZoomBlock.Enabled = true;
                this.FRPageNum.Show();
                this.FRPageNum.Enabled = true;


                this.label2.Hide();
                this.label2.Enabled = false;
                this.TimeJump.Hide();
                this.TimeJump.Enabled = false;
                this.VisChan1.Hide();
                this.VisChan1.Enabled = false;
                this.VisChan2.Hide();
                this.VisChan2.Enabled = false;
                this.VisChan3.Hide();
                this.VisChan3.Enabled = false;
                this.VisChan4.Hide();
                this.VisChan4.Enabled = false;
                this.VisChan5.Hide();
                this.VisChan5.Enabled = false;
                this.VisChan6.Hide();
                this.VisChan6.Enabled = false;
                this.VisChan7.Hide();
                this.VisChan7.Enabled = false;
                this.VisChan8.Hide();
                this.VisChan8.Enabled = false;
                this.VisChan16.Hide();
                this.VisChan16.Enabled = false;
                this.VisChan15.Hide();
                this.VisChan15.Enabled = false;
                this.VisChan14.Hide();
                this.VisChan14.Enabled = false;
                this.VisChan13.Hide();
                this.VisChan13.Enabled = false;
                this.VisChan12.Hide();
                this.VisChan12.Enabled = false;
                this.VisChan11.Hide();
                this.VisChan11.Enabled = false;
                this.VisChan10.Hide();
                this.VisChan10.Enabled = false;
                this.VisChan9.Hide();
                this.VisChan9.Enabled = false;
                this.label3.Hide();
                this.label3.Enabled = false;
                this.SwitchChan.Hide();
                this.SwitchChan.Enabled = false;
                this.ZoomScale.Hide();
                this.ZoomScale.Enabled = false;
                this.DetSezLabel.Hide();
                this.DetSezLabel.Enabled = false;
                this.TimeBox.Hide();
                this.TimeBox.Enabled = false;
                this.HighlightLabel.Hide();
                this.HighlightLabel.Enabled = false;
                this.ColorClear.Hide();
                this.ColorClear.Enabled = false;
                this.MoreOp.Hide();
                this.OffsetLabel.Hide();






            }
            else
            {
                this.Play.Show();
                this.Play.Enabled = true;
                this.VideoPanel.Show();
                this.VideoPanel.Enabled = true;
                this.Open.Show();
                this.Open.Enabled = true;
                this.Pause.Show();
                this.Pause.Enabled = true;
                this.TimeBar.Show();
                this.TimeBar.Enabled = true;
                this.Rewind.Show();
                this.Rewind.Enabled = true;
                this.SpeedUp.Show();
                this.SpeedUp.Enabled = true;
                this.TimeLabel.Show();
                this.TimeLabel.Enabled = true;
                this.TimeBox.Show();
                this.TimeBox.Enabled = true;
                this.label1.Show();
                this.label1.Enabled = true;
                this.SzCaptureButton.Show();
                this.SzCaptureButton.Enabled = true;
                this.DetectionLoadButton.Show();
                this.DetectionLoadButton.Enabled = true;
                this.FastReview.Show();
                this.FastReview.Enabled = true;
                this.button2.Show();
                this.button2.Enabled = true;
                this.button3.Show();
                this.button3.Enabled = true;
                this.OffsetBox.Show();
                this.OffsetBox.Enabled = true;
                //this.LoadAll.Show();
                //this.LoadAll.Enabled = true;
                this.Rewind_ChangeBox.Show();
                this.Rewind_ChangeBox.Enabled = true;

                this.RvwSz.Show();
                this.RvwSz.Enabled = true;
                this.TelemetryBox.Show();
                this.TelemetryBox.Enabled = true;
                this.Randomization.Show();
                this.Randomization.Enabled = true;
                this.VideoFix.Show();
                this.VideoFix.Enabled = true;
                this.comboBox1.Show();
                this.comboBox1.Enabled = true;


                this.FRButtonGroup.Hide();
                this.FRButtonGroup.Enabled = false;
                this.FRZoomBlock.Hide();
                this.FRZoomBlock.Enabled = false;
                this.FRPageNum.Hide();
                this.FRPageNum.Enabled = false;


                this.label2.Show();
                this.label2.Enabled = true;
                this.TimeJump.Show();
                this.TimeJump.Enabled = true;
                this.VisChan1.Show();
                this.VisChan1.Enabled = true;
                this.VisChan2.Show();
                this.VisChan2.Enabled = true;
                this.VisChan3.Show();
                this.VisChan3.Enabled = true;
                this.VisChan4.Show();
                this.VisChan4.Enabled = true;
                this.VisChan5.Show();
                this.VisChan5.Enabled = true;
                this.VisChan6.Show();
                this.VisChan6.Enabled = true;
                this.VisChan7.Show();
                this.VisChan7.Enabled = true;
                this.VisChan8.Show();
                this.VisChan8.Enabled = true;
                this.VisChan16.Show();
                this.VisChan16.Enabled = true;
                this.VisChan15.Show();
                this.VisChan15.Enabled = true;
                this.VisChan14.Show();
                this.VisChan14.Enabled = true;
                this.VisChan13.Show();
                this.VisChan13.Enabled = true;
                this.VisChan12.Show();
                this.VisChan12.Enabled = true;
                this.VisChan11.Show();
                this.VisChan11.Enabled = true;
                this.VisChan10.Show();
                this.VisChan10.Enabled = true;
                this.VisChan9.Show();
                this.VisChan9.Enabled = true;
                this.label3.Show();
                this.label3.Enabled = true;
                this.SwitchChan.Show();
                this.SwitchChan.Enabled = true;
                this.ZoomScale.Show();
                this.ZoomScale.Enabled = true;
                this.DetSezLabel.Show();
                this.DetSezLabel.Enabled = true;
                this.TimeBox.Show();
                this.TimeBox.Enabled = true;
                this.HighlightLabel.Show();
                this.HighlightLabel.Enabled = true;
                this.ColorClear.Show();
                this.ColorClear.Enabled = true;
                this.MoreOp.Show();
                this.OffsetLabel.Show();
            }
        }





        //private bool RemoveChannelMessage()
        //{

        //    string message = "Do you want to remove unselected channels from Fast Review?";
        //    string caption = "Fast Review Channel Removal";
        //    //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        //    //DialogResult answer;
        //    var answer = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    if (answer == DialogResult.Yes)
        //    {
        //        return true;
        //    }
        //    else return false;


        //}

        //public List<int> HiddenChannelList()
        //{
        //    for (int i = 0; i < 16; i++)
        //    {
        //        if (ACQ.HideChan[i] && !HCL.Contains(i)) HCL.Add(i);
        //        if (!ACQ.HideChan[i] && HCL.Contains(i)) HCL.Remove(i);
        //    }

        //    return HCL;
        //}





        //public void AutoLoadForTest()
        //{

        //    if (!testMode) return;
        //    //D:\TestData\Withfastreview\20211128 - 214545;
        //    fastReviewCounter = 0;

        //    //DialogResult tempRes;
        //    //FBD = new FolderBrowserDialog();
        //    DefaultFolder = "D:\\TestData\\Withfastreview\\20211128 - 214545";
        //    //FBD.SelectedPath = DefaultFolder;
        //    long totms;
        //    float hrs;
        //    OpenFrm frm;
        //    Paused = true;
        //    string[] IniFiles;
        //    string AVIname;
        //    //tempRes = FBD.ShowDialog();

        //    if (true);
        //    {
        //        Path = "D:\\TestData\\Withfastreview\\20211128-214545";
        //        string[] FName = Directory.GetFiles(Path, "*.acq");
        //        if (FName.Length == 0) return;
        //        DefaultFolder = Path.Substring(0, Path.LastIndexOf("\\"));
        //        Console.WriteLine(DefaultFolder);
        //        HighlightLabel.Text = "";
        //        for (int i = 0; i < 16; i++) SeizureCount[i] = 0; //Initialize to Zero. 
        //        SzInfo = new string[500];
        //        SzInfoIndex = 0;
        //        IniFiles = Directory.GetFiles(Path, "*_Settings.txt");
        //        BioINI = new IniFile(IniFiles[0]);
        //        ReadReviewINI(BioINI);
        //        AVIMode = "avi";
        //        AVIFiles = Directory.GetFiles(Path, "*.avi");
        //        LoadText.Visible = true;
        //        this.Refresh();
        //        AVILoadBar.Visible = true;
        //        if (AVIFiles.Length == 0)
        //        {
        //            AVIFiles = Directory.GetFiles(Path, "*.mp4");
        //            if (AVIFiles.Length > 0)
        //            {
        //                AVIMode = "mp4";
        //                //load all potential MP4 files
        //                BaseName = AVIFiles[0].Substring(Path.Length + 1, 15);
        //                OffsetBox.Visible = false;
        //                OffsetLabel.Visible = false;
        //                AVILoadBar.Maximum = AVIFiles.Length;
        //                for (int chanloop = 0; chanloop < 16; chanloop++)
        //                {
        //                    totms = 0;
        //                    for (int fileloop = 0; fileloop < 30; fileloop++)
        //                    {
        //                        if (fileloop == 0)
        //                        {
        //                            AVIname = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + ".mp4";
        //                        }
        //                        else
        //                        {
        //                            AVIname = Path + "\\" + BaseName + string.Format("_{0:d3}", chanloop) + "." + (fileloop).ToString() + ".mp4";
        //                        }
        //                        if (File.Exists(AVIname))
        //                        {
        //                            //Console.WriteLine(AVIname);
        //                            media = new VlcMedia(instance, AVIname);
        //                            if (player == null)
        //                            {
        //                                player = new VlcMediaPlayer(media);
        //                                player.Drawable = VideoPanel.Handle;
        //                            }
        //                            else player.Media = media;
        //                            player.Play();
        //                            while (player.GetLengthMs() == 0)
        //                            { }
        //                            AVILengths[chanloop, fileloop] = player.GetLengthMs();
        //                            totms += AVILengths[chanloop, fileloop];
        //                            player.Stop();
        //                            media.Dispose();
        //                            AVILoadBar.Increment(1);
        //                        }
        //                        else
        //                        {
        //                            AVILengths[chanloop, fileloop] = 0;
        //                        }
        //                    }
        //                    // hrs =(totms / 1000);
        //                    //Console.WriteLine("TOTAL LENGTH: " + hrs.ToString());
        //                }
        //            }
        //        }
        //        else
        //        {
        //            BaseName = AVIFiles[0].Substring(Path.Length + 1, 15);
        //            //Load all potential AVI files                    
        //            for (int chanloop = 0; chanloop < 16; chanloop++)
        //            {
        //                totms = 0;
        //                AVILoadBar.Maximum = AVIFiles.Length;
        //                for (int fileloop = 1; fileloop < 10; fileloop++)
        //                {
        //                    AVIname = Path + "\\" + BaseName + string.Format("_{0:d2}", chanloop) + string.Format("_{0:d4}.avi", fileloop);

        //                    if (File.Exists(AVIname))
        //                    {

        //                        media = new VlcMedia(instance, AVIname);
        //                        if (player == null)
        //                        {
        //                            player = new VlcMediaPlayer(media);
        //                            player.Drawable = VideoPanel.Handle;
        //                        }
        //                        else player.Media = media;
        //                        player.Play();
        //                        while (player.GetLengthMs() == 0)
        //                        { }
        //                        AVILengths[chanloop, fileloop - 1] = player.GetLengthMs();
        //                        totms += AVILengths[chanloop, fileloop - 1];
        //                        //Console.WriteLine(AVIname + "  " + AVILengths[chanloop, fileloop - 1].ToString());
        //                        player.Stop();
        //                        media.Dispose();
        //                        AVILoadBar.Increment(1);
        //                    }
        //                    else
        //                    {
        //                        AVILengths[chanloop, fileloop - 1] = 0;
        //                    }
        //                }
        //                for (int fileloop = 10; fileloop < 30; fileloop++)
        //                {
        //                    AVILengths[chanloop, fileloop - 1] = 0;
        //                }
        //                //hrs = totms / (3600 * 1000);
        //                //Console.WriteLine("TOTAL LENGTH: " + hrs.ToString());
        //            }

        //        }
        //        LoadText.Visible = false;
        //        AVILoadBar.Visible = false;
        //        ACQ.openACQ(FName[0]);
        //        Console.WriteLine(FName[0]);
        //        if (FName.Length > 1)
        //        {
        //            ACQ.AppendACQ(FName[1]);
        //        }
        //        ACQ.VisibleChans = ACQ.Chans;
        //        ACQ.SetDispLength(MaxDispSize);

        //        ACQ.VisibleChans = ACQ.Chans;
        //        ACQ.SetDispLength(MaxDispSize);
        //        frm = new OpenFrm(BaseName, Reviewer, ReviewNotes, PercentCompletion, ACQ.TotFileTime, LastReview, LastOpen, CrashWarning, Compressed);
        //        frm.ShowDialog();
        //        Reviewer = frm.GetReviewer();
        //        Reviewing = frm.GetReviewing();
        //        if (Reviewing)
        //        {
        //            //ReviewLogging = true;

        //        }
        //        frm.Dispose();
        //        CManage.ActiveForm.Text = "Seizure Playback - " + BaseName.Substring(4, 2) + "/" + BaseName.Substring(6, 2)
        //            + "/" + BaseName.Substring(0, 4) + " - " + BaseName.Substring(9, 2) + ":" + BaseName.Substring(11, 2) + ":" + BaseName.Substring(13, 2);
        //        TimeBar.Minimum = 0;
        //        TimeBar.Maximum = ACQ.TotFileTime;
        //        SzInfoIndex = 0;
        //        string FPath = AVIFiles[0].Substring(0, AVIFiles[0].LastIndexOf("\\") + 1) + "Seizure";

        //        if (!Directory.Exists(FPath))
        //        {
        //            Directory.CreateDirectory(FPath);
        //        }
        //        if (File.Exists(FPath + "\\" + BaseName + ".txt"))
        //        {

        //            int Animal;
        //            int SzC;
        //            string[] TmpStr;
        //            StreamReader TmpTxt = new StreamReader(FPath + "\\" + BaseName + ".txt");
        //            while (!TmpTxt.EndOfStream)
        //            {
        //                SzInfo[SzInfoIndex] = TmpTxt.ReadLine();
        //                TmpStr = SzInfo[SzInfoIndex].Split(',');
        //                int.TryParse(TmpStr[0], out Animal);
        //                int.TryParse(TmpStr[2], out SzC);
        //                SeizureCount[Animal - 1] = Math.Max(SzC, SeizureCount[Animal - 1]);
        //                SzInfoIndex++;
        //            }
        //            TmpTxt.Dispose();
        //            SzTxt = new System.IO.StreamWriter(FPath + "\\" + BaseName + ".txt");
        //            for (int k = 0; k < SzInfoIndex; k++)
        //            {
        //                SzTxt.WriteLine(SzInfo[k]);
        //            }
        //        }
        //        else
        //        {
        //            SzTxt = new System.IO.StreamWriter(FPath + "\\" + BaseName + ".txt");
        //        }
        //        SzTxt.AutoFlush = true;
        //        SuppressChange = true;
        //        for (int i = 0; i < ACQ.Chans; i++)
        //        {
        //            VisChecks[i].Checked = true;
        //            ChanPos[i] = i;
        //        }
        //        for (int i = ACQ.Chans; i < 16; i++)
        //        {
        //            VisChecks[i].Visible = false;
        //        }
        //        SuppressChange = false;
        //        INISave();
        //        if (Reviewing)
        //        {
        //            ACQ.Position = (int)Math.Floor((PercentCompletion * (double)ACQ.TotFileTime) / (double)100);
        //            Step = MaxDispSize;
        //        }

        //        string[] detName = Directory.GetFiles(Path, "*.det");           //this section autoloads a .det file if it exists within the directory.
        //        if (detName.Length != 0)
        //        {

        //            DSF.OpenFile(detName[0]);

        //            DetSezLabel.Text = ".det Loaded";
        //            fastReviewCounter = 0;
        //        }
        //        else DetSezLabel.Text = ".det Not Found";
        //    }
        //}

    }
}
