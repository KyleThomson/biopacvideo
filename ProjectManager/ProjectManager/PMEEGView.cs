using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProjectManager
{


    public partial class PMEEGView : Form
    {
        int ViewMode = 0; //0 = normal, //1 = gallery, 2 = Animal View
        List<AnimalType> Animals;
        public bool paused = true;
        public int TimeFrame;
        DATR DAT;
        public int numDats;
        public int pageNum;
        public LibVLC vlc;
        public MediaPlayer player;
        public string vidDir;
        public bool vidEnable;
        public Thread ThreadDisplay;
        public List<OffsetName> Offset;
        public bool Redraw;
        public int numPerPage;
        public bool hiRes = true;
        public Graphics g;
        public Graphics Gg;
        public MyGraph graph;
        public MyGraph GalGraph;

        bool[] vidShow;
        int selected = -1;
        public int seconds = 0;

        float position = 0; //position in this program is equal to 500ms, rather than 1s
        Pen redPen;
        bool needsMainDraw = false; // this is to draw the initially selected seizure in gallery mode
        bool endOfClip = false;
        bool threadLock = true; //for locking the ThreadDisplay
        bool TDDone = false; //for waiting for threaddisplay to end
        int SelectedTotal = -1;
        bool mouseBusy = false; //used for both mouse clicks and key presses, in order to reject stacking clicks (causes thread issues)
        bool ButtonBusy = false; //used for button clicks, again for thread safety
        ProjectManager Prnt;
        int CurrentAnimal;
        
        EditNotesForm ENF;




        public PMEEGView(ProjectManager sent)
        {

            InitializeComponent();
            Prnt = sent;
            TimeFrame = 30;
            GalGBox.Visible = false;
            Animals = Prnt.pjt.Animals;
            numDats = Prnt.pjt.DatCount;


            DAT = new DATR(Prnt.pjt.CDatName);
            pageNum = 0;
            ViewMode = 0;
            vidDir = Directory.GetParent(sent.pjt.Filename).ToString() + "\\Videos\\";
            if (Directory.Exists(vidDir)) vidEnable = true;
            //TFSelect.SelectedIndex = 0;
            graph = new MyGraph();
            graph.X1 = 5;
            graph.X2 = this.Size.Width - 25;
            graph.Y1 = this.menuStrip1.Location.X + (this.menuStrip1.Height + 5);
            graph.Y2 = this.BottomLabel.Location.Y - 10;
            GalGraph = new MyGraph();
            GalGraph.X1 = this.GalGBox.Location.X + 2;
            GalGraph.X2 = this.GalGBox.Location.X + GalGBox.Width - 2;
            GalGraph.Y1 = this.GalGBox.Location.Y + 2;
            GalGraph.Y2 = this.GalGBox.Location.Y + GalGBox.Height;
            ListCreation();
            redPen = new Pen(Color.Red, 3);


            #region FORM EVENTS

            //this.Resize += new System.EventHandler(this.MainForm_Resize);

            SetFeatureToAllControls(this.Controls);
            this.KeyPreview = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);
            this.KeyDown += new KeyEventHandler(PMEEG_KeyDown);


            #endregion



            this.BackColor = Color.Black;
            this.Opacity = 100;
            //this.Refresh();
            g = this.CreateGraphics();
            Gg = GalGBox.CreateGraphics();


            DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, GalGBox.Width - 2, GalGBox.Height);
            numPerPage = 8;
            vidShow = new bool[numPerPage];
            for (int i = 0; i < numPerPage; i++)
            {
                vidShow[i] = false;
            }


            //do big eeg

            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
            Gg.DrawImage(DAT.GOffscreen, GalGraph.X1, GalGraph.Y1);

            
            this.Show();
            //ThreadDisplay = new Thread(new ThreadStart(CheckThread));
            //ThreadDisplay.Start();
            Redraw = true;

            pg.Text = (pageNum + 1) + " / " + pageCalc();

            UpdateDisplay();
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            threadLock = false;
            


        }

        private void MainForm_Resize(object sender, EventArgs e)
        {


        }

        public int pageCalc()
        {
            int r = numDats / numPerPage;
            if (numDats % numPerPage != 0) r++;
            return r;
        }

        #region Mouse and Key Handlers

        void PMEEG_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (mouseBusy) return;

            Console.WriteLine("Code: " + e.KeyCode + " | Value: " + e.KeyValue);

            switch (e.KeyCode)
            {

                case Keys.Left:
                case Keys.NumPad4:

                    Previous_Click(null, null);

                    break;

                case Keys.Right:
                case Keys.NumPad6:

                    Next_Click(null, null);

                    break;

            }

            mouseBusy = true;

            if (ViewMode == 0)
            {

            }
            else if (ViewMode == 1)
            {

                switch (e.KeyCode)
                {

                }

            }
            else if (ViewMode == 2)
            {

            }

            e.Handled = true;
            
            mouseBusy = false;
        }

        void PMEEG_KeyPress(object sender, KeyPressEventArgs e)
        {

            string s = e.KeyChar.ToString();

            switch (s)
            {
                case "p":

                    if (paused)
                    {
                        Play_Click(null, null);
                    } else
                    {
                        Pause_Click(null, null);
                    }

                    break;

                
            }

        }


        private void GalArea_MouseDown(object sender, MouseEventArgs e)
        {

            if (mouseBusy) return;
            mouseBusy = true;

            int tX1;
            int tX2;
            int tY1;
            int tY2;
            
            

            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }



            if (ViewMode == 1 || ViewMode == 2)
            {
                tX1 = GalArea.Location.X;
                tX2 = GalArea.Location.X + GalArea.Width;
                tY1 = GalArea.Location.Y;
                tY2 = GalArea.Location.Y + GalArea.Height;
            }
            else
            {
                tX1 = graph.X1;
                tX2 = graph.X2;
                tY1 = graph.Y1;
                tY2 = graph.Y2;
            }
            

            if ((e.X >= tX1) && (e.X < tX2) && (e.Y >= tY1) && ((e.Y < tY2)))
            {
                int Y = (e.Y - tY1);
                int temp;
                int X = 0;
                int XSplit = 1;
                if (ViewMode == 0)
                {
                    XSplit = 2;
                    if (e.X > (((tX2 - tX1) / 2) + tX1))
                    {
                        X = 1;
                    }
                }


                
                

                else if (ViewMode == 1)
                {
                    Y = Y / ((tY2 - tY1) / (numPerPage / XSplit));
                    temp = pageNum * numPerPage + Y + X;
                    position = 0;

                    if (Offset[temp].Selected)
                    {
                        Offset[temp].Selected = false;
                        selected = -1;
                        seconds = -1;
                        EditNotesButton.Enabled = false;
                        NotesSection.ResetText();
                        ANLabel.ResetText();
                        RacineLabel.ResetText();
                        CurrentAnimal = -1;
                    }
                    else
                    {
                        if (SelectedTotal != -1) Offset[SelectedTotal].Selected = false;
                        SelectedTotal = temp;
                        Offset[temp].Selected = true;
                        ListViewItem tempL = new ListViewItem();
                        ANLabel.Text = (Animals[Offset[temp].AnimalIndex].ID);
                        NotesSection.ResetText();
                        ANLabel.ResetText();
                        RacineLabel.ResetText();
                        RacineLabel.Text = (Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].Severity.ToString());
                        NotesSection.Text = Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].Notes;
                        EditNotesButton.Enabled = true;
                        CurrentAnimal = temp;
                        seconds = (Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].length);
                        
                        //if (ShowBuffer.Checked) seconds += 60;
                        selected = Y;
                    }

                    for (int i = 0; i < numPerPage; i++)
                    {
                        if (i == Y && selected != -1)
                        {
                            vidShow[i] = true;
                            VideoPlay(i);
                        }
                        else
                        {
                            vidShow[i] = false;
                        }

                    }

                }

            }
            Redraw = true;
            threadLock = false;
            UpdateDisplay();
            mouseBusy = false;

        }

        private void PMEEGView_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseBusy) return;
            mouseBusy = true;

            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            if (e.X >= GalGBox.Location.X && e.X < GalGBox.Location.X + GalGBox.Width && e.Y >= GalGBox.Location.Y && e.Y < GalGBox.Location.Y + GalGBox.Height && ViewMode == 1)
            {
                paused = true;
                int RelX = e.X - GalGBox.Location.X;
                int RelY = e.Y - GalGBox.Location.Y;

                if (selected != -1)
                {
                    threadLock = true;
                    while (!TDDone)
                    {
                        Thread.Sleep(10);
                    }


                    int SplitUp = GalGBox.Width / (seconds * 2);
                    int tempTimeSelect = RelX / SplitUp;

                    position = (float)tempTimeSelect / 2f;

                    //Console.WriteLine("X:" + e.X + " | Y:" + e.Y + $" | RelX:{RelX} | RelY:{RelY} | SplitUp:{SplitUp} | TempTime:{tempTimeSelect} + | Pos:{position}");

                    if (myVLC.MediaPlayer != null)
                    {
                        TimeSpan tempTime = TimeSpan.FromMilliseconds(position * 1000f);
                        Console.WriteLine("tempTime: " + tempTime.TotalMilliseconds);
                        if (tempTime.TotalMilliseconds >= myVLC.MediaPlayer.Length)
                        {
                            myVLC.MediaPlayer.SeekTo(TimeSpan.FromMilliseconds(myVLC.MediaPlayer.Length));

                            Console.WriteLine("Pos >= Media Length: " + (myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length));
                        }
                        else
                        {
                            myVLC.MediaPlayer.SeekTo(tempTime);

                            Console.WriteLine("Pos < Media Length: " + (myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length));
                        }

                        if (myVLC.MediaPlayer.IsPlaying)
                        {
                            Thread.Sleep(100);
                            myVLC.MediaPlayer.Pause();
                            Console.WriteLine("Needs Pause");
                        }

                    }

                    needsMainDraw = true;
                    threadLock = false;

                }
            }

            mouseBusy = false;

            threadLock = false;
        }

        private void SetFeatureToAllControls(Control.ControlCollection cc)
        {
            if (cc != null)
            {
                foreach (Control control in cc)
                {
                    control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
                    SetFeatureToAllControls(control.Controls);
                }
            }
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3)
            {
                e.IsInputKey = true;

            }
        }

        #endregion

        #region Button Handlers


        private void Next_Click(object sender, EventArgs e)
        {
            if (ButtonBusy) return;
            ButtonBusy = true;

            if (pageNum * numPerPage + numPerPage > numDats)
            {
                ButtonBusy = false;
                return;
            }
            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            ResetChosenVid();
            if (ViewMode == 1 && myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();


            pageNum += 1;
            Redraw = true;

            UpdateDisplay();
            pg.Text = (pageNum + 1) + " / " + pageCalc();
            if (ViewMode == 1) Gg.Clear(Color.White);
            threadLock = false;

            ButtonBusy = false;

        }

        private void Previous_Click(object sender, EventArgs e)
        {

            if (ButtonBusy) return;
            ButtonBusy = true;

            if (pageNum == 0)
            {
                ButtonBusy = false;
                return;
            }
            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            ResetChosenVid();
            if (ViewMode == 1 && myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            pageNum -= 1;
            Redraw = true;

            UpdateDisplay();
            pg.Text = (pageNum + 1) + " / " + pageCalc();
            ResetChosenVid();
            if (ViewMode == 1) Gg.Clear(Color.White);
            threadLock = false;

            ButtonBusy = false;
        }

        private void Play_Click(object sender, EventArgs e)
        {

            

            if (myVLC.MediaPlayer == null) return;
            if (ButtonBusy) return;

            ButtonBusy = true;


            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            if (paused)
            {

                if (endOfClip)
                {
                    myVLC.MediaPlayer.Position = 0;
                    position = 0;
                    endOfClip = false;
                }

                paused = false;
                myVLC.MediaPlayer.Play();
            }

            threadLock = false;

            ButtonBusy = false;
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            if (myVLC.MediaPlayer == null) return;

            if (ButtonBusy) return;

            ButtonBusy = true;

            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            if (myVLC.MediaPlayer == null) return;

            if (!paused)
            {
                paused = true;
                myVLC.MediaPlayer.Pause();

            }
            threadLock = false;

            ButtonBusy = false;
        }


        #endregion

        public void ListCreation()
        {
            Offset = new List<OffsetName>();
            OffsetName tempOff;
            int aCount = 0;
            int totalSZ = 0;


            foreach (AnimalType A in Animals)
            {
                int numSz = 0;
                foreach (SeizureType S in A.Sz)
                {
                    if (S.Offset >= 0)
                    {

                        tempOff = new OffsetName(aCount, numSz, false);

                        Offset.Add(tempOff);

                        totalSZ++;
                    }
                    numSz++;

                }
                aCount++;
            }
            numDats = totalSZ;


        }

        public void DisplayThread()
        {
            int tempMin = 0;
            int tempSec = 0;
            int totMin = 0;
            int totSec = 0;




            while (true)
            {


                if (!threadLock)
                {
                    TDDone = false;
                    if (selected != -1)
                    {
                        if (needsMainDraw && paused)
                        {
                            if (position <= 0)
                            {
                                Gg.Clear(Color.White);
                                Gg.DrawImage(DAT.GOffscreen, 0, 0);

                                PointF top = new PointF(0, 0);
                                PointF bottom = new PointF(0, GalGBox.Height);
                                Gg.DrawLine(redPen, top, bottom);
                                needsMainDraw = false;

                                totMin = seconds / 60;
                                totSec = seconds - (60 * totMin);

                                TimeLabel.Invoke((MethodInvoker)delegate
                                {
                                    TimeLabel.Text = $"0:00 / {totMin}" + string.Format(":{0:00}", totSec);
                                });
                            }
                            else
                            {
                                PointF top = new PointF((position / (float)seconds) * (GalGBox.Size.Width), 0);
                                PointF bottom = new PointF((position / (float)seconds) * (GalGBox.Size.Width), GalGBox.Height);
                                Gg.Clear(Color.White);
                                Gg.DrawImage(DAT.GOffscreen, 0, 0);
                                Gg.DrawLine(redPen, top, bottom);

                                if (position / 60 >= 1)
                                {
                                    tempMin = (int)position / 60;
                                    tempSec = (int)position % 60;
                                }
                                else
                                {
                                    tempMin = 0;
                                    tempSec = (int)position;
                                }
                                needsMainDraw = false;

                                TimeLabel.Invoke((MethodInvoker)delegate
                                {
                                    TimeLabel.Text = tempMin.ToString() + string.Format(":{0:00}", tempSec) + $" / {totMin}" + string.Format(":{0:00}", totSec);

                                });
                            }
                        }

                        if (!paused)
                        {
                            if (position >= seconds)
                            {
                                paused = true;
                                if (myVLC != null)
                                {
                                    myVLC.MediaPlayer.Pause();
                                }
                                endOfClip = true;



                            }
                            PointF top = new PointF((position / (float)seconds) * (GalGBox.Size.Width), 0);
                            PointF bottom = new PointF((position / (float)seconds) * (GalGBox.Size.Width), GalGBox.Height);
                            //Gg.Clear(Color.White);
                            Gg.DrawImage(DAT.GOffscreen, 0, 0);
                            Gg.DrawLine(redPen, top, bottom);

                            if (position / 60 >= 1)
                            {
                                tempMin = (int)position / 60;
                                tempSec = (int)position % 60;
                            }
                            else
                            {
                                tempMin = 0;
                                tempSec = (int)position;
                            }

                            TimeLabel.Invoke((MethodInvoker)delegate
                            {
                                TimeLabel.Text = tempMin.ToString() + string.Format(":{0:00}", tempSec) + $" / {totMin}" + string.Format(":{0:00}", totSec);

                            });



                            position += 0.5f;
                            Thread.Sleep(400);

                        }


                    }


                }
                Thread.Sleep(100);
                TDDone = true;

            }
        }



        public void UpdateDisplay()
        {

            if (!this.Visible)
            {
                return;
            }

            //Console.WriteLine("Thread Poked");

            //if (pauseDisplay)
            //{
            //    Thread.Sleep(1000);
            //    Redraw = false;
            //    pauseDisplay = false;
            //}
            if (DAT.Loaded)
            {
                //Thread.Sleep(1000);

                if (ViewMode == 1)  //gallery mode drawing
                {
                    if (Redraw)
                    {
                        DAT.SetDispLength(TimeFrame);
                        int count = pageNum * numPerPage;

                        DAT.cleargraph();
                        int tempIn;
                        tempIn = numPerPage / 2;

                        for (int i = 0; i < numPerPage; i++)
                        {
                            if (count >= Offset.Count) break;
                            DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, 0, i, Offset[count].Selected, vidShow[i], ShowBuffer.Checked);




                            count++;
                        }

                        //g.Clear(Color.White);
                        g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                        needsMainDraw = true;

                        Redraw = false;

                    }


                    //if (myVLC.MediaPlayer != null)
                    //{
                    //    //WhateverFormNamed.AttemptChange()
                    //    //PMEEGView.ActiveForm.change
                    //    TimeLabel.Invoke(new MethodInvoker(delegate
                    //    {

                    //        TimeLabel.Text = (Math.Ceiling((myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length) / 1000f)).ToString();
                    //        //if (!timeBarAccess) TimeBar.Value = (int)(myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length / 1000f);

                    //    }));

                    //    if (TimeBar.InvokeRequired) //Once again, need to do an invoke to handle from a separate thread
                    //    {
                    //        TimeBar.Invoke(new MethodInvoker(delegate
                    //        {

                    //            TimeBar.Value = (int)(myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length / 1000f);

                    //            TimeBar.AttemptChange(//Value I want);  

                    //        }));

                    //    }
                    //}

                }
                else if (ViewMode == 2) //animal mode drawing
                {

                }
                else //default mode drawing
                {

                    if (Redraw)
                    {
                        DAT.SetDispLength(TimeFrame);
                        int count = pageNum * numPerPage;
                        DAT.cleargraph();

                        for (int i = 0; i < numPerPage; i++)
                        {
                            if (count >= Offset.Count) break;
                            DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, i % 2, i / 2, Offset[count].Selected, false, ShowBuffer.Checked);

                            count++;
                        }
                        g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                        Redraw = false;

                    }









                }
            }



            //GC.Collect();

        }

        private void PMEEGView_Close(object sender, FormClosedEventArgs e)
        {
            ThreadDisplay.Abort();
            DAT.closeDAT();
            
            if (myVLC.MediaPlayer != null)
            {
                myVLC.MediaPlayer.Dispose();
                
            }

            Gg.Dispose();
            g.Dispose();
                       

            Prnt.eEGViewDispose();

        }







        private void normalListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < vidShow.Count(); i++)
            {
                vidShow[i] = false;
            }
            switch (sender.ToString())
            {
                case "Default":
                    if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
                    ViewMode = 0;
                    pageNum = 0;
                    Gg.Clear(Color.Black);
                    GalGBox.Visible = false;
                    DefaultView.Checked = true;
                    animalView.Checked = false;
                    galleryView.Checked = false;
                    VideoMode(false);
                    numPerPage = 8;
                    DAT.numPerPage = numPerPage;
                    DAT.drawMode = 0;
                    GraphResize(0);
                    Redraw = true;
                    UpdateDisplay();
                    videoSizeToolStripMenuItem.Enabled = false;
                    pg.Text = (pageNum + 1) + " / " + pageCalc();





                    break;
                case "Gallery":
                    ViewMode = 1;
                    g.Clear(Color.Black);
                    pageNum = 0;
                    Gg.Clear(Color.White);
                    GalGBox.Visible = true;
                    DAT.cleargraph();
                    DefaultView.Checked = false;
                    animalView.Checked = false;
                    galleryView.Checked = true;
                   

                    VideoMode(true);
                    GraphResize(1);
                    numPerPage = 4;
                    DAT.numPerPage = numPerPage * 2;
                    DAT.drawMode = 1;
                    Redraw = true;
                    UpdateDisplay();
                    videoSizeToolStripMenuItem.Enabled = true;
                    pg.Text = (pageNum + 1) + " / " + pageCalc();
                    AnimalViewPanel.Hide();
                    break;
                case "Animal":
                    ViewMode = 2;
                    DefaultView.Checked = false;
                    animalView.Checked = true;
                    galleryView.Checked = false;
                    GraphResize(2);
                    DAT.drawMode = 2;
                    pg.Text = (pageNum + 1) + " / " + pageCalc();
                    AnimalViewPanel.Show();
                    aLComboBox.Items.Clear();

                    foreach (AnimalType A in Animals)
                    {
                        aLComboBox.Items.Add(A.ID);
                    }
                    aLComboBox.SelectedIndex = 0;

                    break;
            }




        }

        public void GraphResize(int mode) // 0 = default, 1 = gallery, 2 = animal
        {
            if (mode == 0)
            {
                graph.X2 = this.Size.Width - 25;
                graph.Y2 = this.BottomLabel.Location.Y - 10;
                g.Dispose();
                g = this.CreateGraphics();
                DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1);


            }
            else if (mode == 1)
            {
                graph.X2 = this.GalArea.Width;
                graph.Y2 = this.GalArea.Height;

                if (g != null) g.Dispose();
                if (Gg != null) Gg.Dispose();

                g = this.CreateGraphics();
                Gg = GalGBox.CreateGraphics();
                DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, GalGBox.Size.Width, GalGBox.Size.Height);


            }
            else if (mode == 2)
            {

            }
        }

        public void VideoMode(bool show)
        {
            if (show)
            {
                myVLC.Enabled = true;
                myVLC.Show();

                TimeLabel.Enabled = true;
                TimeLabel.Show();
                PlayButton.Show();
                PauseButton.Show();
                GVGrouping.Show();
                
            }
            else
            {
                myVLC.Enabled = false;
                myVLC.Hide();
                PlayButton.Hide();
                PauseButton.Hide();
                TimeLabel.Enabled = false;
                TimeLabel.Hide();
                GVGrouping.Hide();
            }
        }



        private void GalleryBox_Enter(object sender, EventArgs e)
        {

        }










        //private void TFSelect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int[] TimeScales = { 10, 15, 30, 60, 120, 300, 600 };
        //    TimeFrame = TimeScales[TFSelect.SelectedIndex];

        //    DAT.SetDispLength(TimeFrame);
        //    if (TimeFrame > 30) DAT.oneCol = true;
        //    else DAT.oneCol = false;
        //    Redraw = true;
        //    UpdateDisplay();
        //}

        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            myVLC.MediaPlayer.Pause();
            paused = true;


            //myVLC.MediaPlayer.Position = temp / 100f;
            myVLC.MediaPlayer.Pause();

        }

        private void PMEEGView_Load(object sender, EventArgs e)
        {

        }

        private void LowRes_Click(object sender, EventArgs e)
        {
            //if (!hiRes) return;
            //this.MinimumSize = new Size(863, 500);
            //resChange(false);
            //this.Width = 1040;
            //this.Height = 600;


            //Point tempP = new Point(myVLC.Location.X - 5, this.Size.Height - 122);



            //PlayPauseButton.Location = tempP;

            //tempP = new Point(PlayPauseButton.Location.X + 43, PlayPauseButton.Location.Y + 9);
            //TimeLabel.Location = tempP;

            //tempP = new Point(myVLC.Location.X - 8, 24);
            //GVGrouping.Location = tempP;



        }











        private void HighRes_Click(object sender, EventArgs e)
        {
            //if (hiRes) return;
            //resChange(true);

            //this.MinimumSize = new Size(1111, 643);
            //this.Width = 1200;
            //this.Height = 800;

            //Point tempP = new Point(myVLC.Location.X - 8, this.Size.Height - 122);








            //tempP = new Point(myVLC.Location.X - 8, 24);
            //GVGrouping.Location = tempP;



        }

        public void resChange(bool high)//1 = high, 0 = low
        {
            if (high)
            {
                if (myVLC.Height == 480) return;
                else
                {
                    myVLC.Invoke(new MethodInvoker(delegate
                    {
                        myVLC.Width = 640;
                        myVLC.Height = 480;
                        Point tempX = new Point(this.Size.Width - 21 - 640, 29);
                        myVLC.Location = tempX;
                    }));


                    HighRes.Checked = true;
                    LowRes.Checked = false;
                    hiRes = true;
                }
            }
            else
            {
                if (myVLC.Height == 360) return;
                else
                {
                    myVLC.Invoke(new MethodInvoker(delegate
                    {
                        myVLC.Width = 480;
                        myVLC.Height = 360;
                        Point tempX = new Point(this.Size.Width - 21 - 480, 29);
                        myVLC.Location = tempX;

                    }));

                    HighRes.Checked = false;
                    LowRes.Checked = true;

                    hiRes = false;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


        }

        private void PMEEGView_ResizeBegin(object sender, EventArgs e)
        {

            threadLock = true;
            while (!TDDone)
            {

            }
        }

        private void PMEEGView_ResizeEnd(object sender, EventArgs e)
        {

            GraphResize(ViewMode);

            Redraw = true;
            UpdateDisplay();
            threadLock = false;
        }

        private void telemetryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (telemetryToolStripMenuItem1.Checked)
            {
                DAT.Telemetry = true;
            }
            else
            {
                DAT.Telemetry = false;
            }
            Redraw = true;
            UpdateDisplay();
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            DAT.Zoom = (float)ZoomBar.Value / 10f;
            Redraw = true;
            UpdateDisplay();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DAT.yOff = vScrollBar1.Value;
            Redraw = true;
            UpdateDisplay();
        }

 

        private void ResetChosenVid()
        {
            vidShow = new bool[numPerPage];
            for (int i = 0; i < vidShow.Count(); i++)
            {
                vidShow[i] = false;
            }
        }



        public void VideoPlay(int index)
        {
            int offset = numPerPage * pageNum + index;







            //if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            //params String[] options = new string[] { "--start-paused", "--no-playlist-autostart" };
            Media media = new Media(vlc, vidDir + Animals[Offset[offset].AnimalIndex].Sz[Offset[offset].SZNum].VidString);
            //media.AddOption("--start-paused");
            //media.AddOption("--no-playlist-autostart");

            MediaPlayer player = new MediaPlayer(media);

            



            myVLC.MediaPlayer = player;
            myVLC.MediaPlayer.Play();
            Thread.Sleep(100);

            if (myVLC.MediaPlayer.Length / 1000 > seconds) seconds += 60;


            myVLC.MediaPlayer.Pause();
            myVLC.MediaPlayer.Position = 0;
            paused = true;
            media.Dispose();
            myVLC.Show();

            Redraw = true;
            UpdateDisplay();
        }





        private void button1_Click(object sender, EventArgs e)
        {


        }


        private void GalGBox_Enter(object sender, EventArgs e)
        {

        }

        private void PMEEGView_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.X > GalGBox.Location.X && e.X < GalGBox.Location.X + GalGBox.Width && e.Y > GalGBox.Location.Y && e.Y < GalGBox.Location.Y + GalGBox.Height)
            //{
            //    Console.WriteLine("Yes!");
            //}
        }

        private void BigEEGPanel_MouseDown(object sender, MouseEventArgs e)
        {


        }



        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void PMEEGView_ResizeEnd_1(object sender, EventArgs e)
        {
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }

            graph.X1 = 5;
            graph.X2 = this.Size.Width - 25;
            graph.Y1 = this.menuStrip1.Location.X + (this.menuStrip1.Height + 5);
            graph.Y2 = this.BottomLabel.Location.Y - 10;
            
            GalGraph.X1 = this.GalGBox.Location.X + 2;
            GalGraph.X2 = this.GalGBox.Location.X + GalGBox.Width - 2;
            GalGraph.Y1 = this.GalGBox.Location.Y + 2;
            GalGraph.Y2 = this.GalGBox.Location.Y + GalGBox.Height;
            g.Dispose();
            Gg.Dispose();
            g = this.CreateGraphics();
            Gg = GalGBox.CreateGraphics();
            


            DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, GalGBox.Width - 2, GalGBox.Height);
            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
            Gg.DrawImage(DAT.GOffscreen, GalGraph.X1, GalGraph.Y1);

            Redraw = true;
            UpdateDisplay();
            threadLock = false;

        }

        private void TimeFrameBar_Scroll(object sender, EventArgs e)
        {
            if (TimeFrameBar.Value < 1)
            {
                TimeFrameBar.Value = 1;
            }
            TFLabel.Text = TimeFrameBar.Value.ToString();
            TimeFrame = TimeFrameBar.Value;

            DAT.SetDispLength(TimeFrame);
            if (TimeFrame > 30) DAT.oneCol = true;
            else DAT.oneCol = false;
            Redraw = true;
            UpdateDisplay();
        }

        private void GalArea_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TFs_Click(object sender, EventArgs e)
        {

            string[] s = sender.ToString().Split(':');
            int i;
            int.TryParse(s[1], out i);

            TimeFrameBar.Value = i;
            TimeFrameBar_Scroll(null, null);
            


        }

        private void GVGrouping_Click(object sender, EventArgs e)
        {

        }

        private void EditNotesButton_Click(object sender, EventArgs e)
        {

            
            int temp;
            ;
            if (!int.TryParse(RacineLabel.Text, out temp)) temp = 0;

            ENF = new EditNotesForm(this, temp, ANLabel.Text, NotesSection.Text);
            ENF.Show();
        }

        public void EditNotes(int r, string n)
        {
            if (CurrentAnimal < 0) return;

            Animals[Offset[CurrentAnimal].AnimalIndex].Sz[Offset[CurrentAnimal].SZNum].Severity = r;
            Animals[Offset[CurrentAnimal].AnimalIndex].Sz[Offset[CurrentAnimal].SZNum].Notes = n;
            ANLabel.Text = (Animals[Offset[CurrentAnimal].AnimalIndex].ID);
            RacineLabel.Text = (Animals[Offset[CurrentAnimal].AnimalIndex].Sz[Offset[CurrentAnimal].SZNum].Severity.ToString());
            NotesSection.Text = Animals[Offset[CurrentAnimal].AnimalIndex].Sz[Offset[CurrentAnimal].SZNum].Notes;

            Prnt.pjt.Animals = Animals;
            Prnt.pjt.Save(Prnt.pjt.Filename);

            if (ENF != null) ENF.Dispose();
           


        }

        private void aLComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int s0 = 0;
            int s1 = 0;
            int s2 = 0;
            int s3 = 0;
            int s4 = 0;
            int s5 = 0;
            int tot = 0;


            foreach (SeizureType s in Animals[aLComboBox.SelectedIndex].Sz)
            {
                switch (s.Severity)
                {
                    case 0:
                        s0++;
                        tot++;
                        break;
                    case 1:
                        s1++;
                        tot++;
                        break;
                    case 2:
                        s2++;
                        tot++;
                        break;
                    case 3:
                        s3++;
                        tot++;
                        break;
                    case 4:
                        s4++;
                        tot++;
                        break;
                    case 5:
                        s5++;
                        tot++;
                        break;

                }
            }

            S0Label.Text = s0.ToString();
            S1Label.Text = s1.ToString();
            S2Label.Text = s2.ToString();
            S3Label.Text = s3.ToString();
            S4Label.Text = s4.ToString();
            S5Label.Text = s5.ToString();
            TotalSLabel.Text = tot.ToString();

            Redraw = true;
            UpdateDisplay();



        }

        private void PMEEGView_ResizeEnd_2(object sender, EventArgs e)
        {
            GraphResize(ViewMode);
            Redraw = true;
            UpdateDisplay();
        }

        private void ShowBuffer_CheckedChanged(object sender, EventArgs e)
        {
            Redraw = true;
            UpdateDisplay();
        }


        //}



    }



    public class MyGraph
    {
        public int X1, Y1, X2, Y2;
    }
}


/* stuff for next/previous butons for customization. Hard to make non-tacky buttons but ill retry everntually
 *        private void NNext_MouseDown(object sender, MouseEventArgs e)
{

    NNext.BackgroundImage.Dispose();
    NNext.BackgroundImage = NextIList.Images[1];
}

private void NNext_MouseUp(object sender, MouseEventArgs e)
{
    NNext.BackgroundImage.Dispose();
    NNext.BackgroundImage = NextIList.Images[2];
}

private void NNext_MouseHover(object sender, EventArgs e)
{
    NNext.BackgroundImage.Dispose();
    NNext.BackgroundImage = NextIList.Images[2];
}

private void NNext_MouseLeave(object sender, EventArgs e)
{
    NNext.BackgroundImage.Dispose();
    NNext.BackgroundImage = NextIList.Images[0];
}

private void NPrev_MouseDown(object sender, MouseEventArgs e)
{
    NPrev.BackgroundImage.Dispose();
    NPrev.BackgroundImage = PrevIList.Images[1];
}

private void NPrev_MouseHover(object sender, EventArgs e)
{
    NPrev.BackgroundImage.Dispose();
    NPrev.BackgroundImage = PrevIList.Images[2];
}

private void NPrev_MouseLeave(object sender, EventArgs e)
{
    NPrev.BackgroundImage.Dispose();
    NPrev.BackgroundImage = PrevIList.Images[0];
}

private void NPrev_MouseUp(object sender, MouseEventArgs e)
{
    NPrev.BackgroundImage.Dispose();
    NPrev.BackgroundImage = PrevIList.Images[2];
}

temp for mouse down, may use later
 int galOff = GalArea.Location.Y;
                if (e.Y >= 0 + galOff && e.Y < (GalArea.Height / 4) + galOff)
                {
                    int temp = pageNum * numPerPage;
                    if (Offset[temp].Selected) Offset[temp].Selected = false;
                    
                    else Offset[temp].Selected = true;
                    Console.WriteLine("Mine: " + temp);

                }
                else if (e.Y >= (GalArea.Height / 4) + galOff && e.Y < (GalArea.Height / 2) + galOff)
                {
                    int temp = pageNum * numPerPage + 1;

                    if (Offset[temp].Selected) Offset[temp].Selected = false;

                    else Offset[temp].Selected = true;
                    Console.WriteLine("Mine: " + temp);

                }
                else if (e.Y >= (GalArea.Height / 2) + galOff && e.Y < (GalArea.Height - (GalArea.Height / 4)) + galOff)
                {
                    int temp = pageNum * numPerPage + 2;

                    if (Offset[temp].Selected) Offset[temp].Selected = false;

                    else Offset[temp].Selected = true;
                    Console.WriteLine("Mine: " + temp);

                }
                else if (e.Y >= (GalArea.Height - (GalArea.Height / 4)) + galOff && e.Y <= (GalArea.Height) + galOff)
                {
                    int temp = pageNum * numPerPage + 3;

                    if (Offset[temp].Selected) Offset[temp].Selected = false;

                    else Offset[temp].Selected = true;
                    Console.WriteLine("Mine: " + temp);

                }
 */