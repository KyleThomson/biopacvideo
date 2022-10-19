using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibVLCSharp;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

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
        bool pauseDisplay = false;
        bool[] vidShow;
        int selected = -1;
        public int seconds = 0;
        bool timeBarAccess = false;
        float position = 0; //position in this program is equal to 500ms, rather than 1s
        Pen redPen;
        bool needsMainDraw = false; // this is to draw the initially selected seizure in gallery mode
        bool endOfClip = false;
        bool threadLock = true; //for locking the ThreadDisplay
        bool TDDone = false; //for waiting for threaddisplay to end
        int SelectedTotal = -1;
        bool mouseBusy = false;
        
        
        

        public PMEEGView(List<AnimalType> AL, int dc, string fn)
        {
            InitializeComponent();
            GalGBox.Visible = false;
            Animals = AL;
            numDats = dc;
            DAT = new DATR(fn);
            pageNum = 0;
            ViewMode = 0;
            vidDir = Directory.GetParent(fn).ToString() + "\\Videos\\";
            if (Directory.Exists(vidDir)) vidEnable = true;
            TFSelect.SelectedIndex = 0;
            graph = new MyGraph();
            graph.X1 = 5;
            graph.X2 = this.Size.Width - 25;
            graph.Y1 = this.menuStrip1.Location.X + (this.menuStrip1.Height + 5);
            graph.Y2 = this.BottomLabel.Location.Y - 10;
            GalGraph = new MyGraph();
            GalGraph.X1 = this.GalGBox.Location.X + 2;
            GalGraph.X2 = this.GalGBox.Location.X + GalGBox.Width -2;
            GalGraph.Y1 = this.GalGBox.Location.Y + 2;
            GalGraph.Y2 = this.GalGBox.Location.Y + GalGBox.Height - 2;
            ListCreation();
            redPen = new Pen(Color.Red, 3);
            #region FORM EVENTS
            
            //this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);

            


            #endregion



            this.BackColor = Color.Black;
            this.Opacity = 100;
            this.Refresh();
            g = this.CreateGraphics();
            Gg = GalGBox.CreateGraphics();
            
            
            DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, GalGBox.Width - 2, GalGBox.Height - 2);
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
            int totTime = 0;
            
            

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
                            } else
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
                                PlayPauseButton_MouseLeave(null, null);


                            }
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
            int Delay = 0;
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
                                DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, 0, i, Offset[count].Selected, vidShow[i]);
                                



                                count++;
                            }
                            

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
                                DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, i % 2, i / 2, Offset[count].Selected, false);
                                
                                count++;
                            }
                            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                            Redraw = false;

                        }









                    }
                }
            




        }

        private void PMEEGView_Close(object sender, FormClosedEventArgs e)
        {
            ThreadDisplay.Abort();

            if (myVLC.MediaPlayer != null)
            {
                myVLC.MediaPlayer.Dispose();
                myVLC.Dispose();
                
                vlc.Dispose();
            }
            //ProjectManager.ActiveForm.Show();
           
            
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
                    ViewMode = 0;
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





                    break;
                case "Gallery":
                    ViewMode = 1;
                    g.Clear(Color.Black);

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
                    break;
                case "Animal":
                    ViewMode = 2;
                    DefaultView.Checked = false;
                    animalView.Checked = true;
                    galleryView.Checked = false;
                    GraphResize(2);
                    DAT.drawMode = 2;
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

                
            } else if (mode == 1)
            {
                graph.X2 = this.GVGrouping.Location.X - 20;
                graph.Y2 = this.BottomLabel.Location.Y - 10;

                
                g = this.CreateGraphics();
                DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1);
                
                
            } else if (mode == 2)
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
                PlayPauseButton.Enabled = true;
                PlayPauseButton.Show();
                GVGrouping.Show();
            } else
            {
                myVLC.Enabled = false;
                myVLC.Hide();
                
                TimeLabel.Enabled = false;
                TimeLabel.Hide();
                PlayPauseButton.Enabled = false;
                PlayPauseButton.Hide();
                GVGrouping.Hide();
            }
        }

 

        private void GalleryBox_Enter(object sender, EventArgs e)
        {

        }

        private void PlayPauseButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (paused)
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[2];
            }
            else
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[5];
            }
        }

        private void PlayPauseButton_Click(object sender, EventArgs e)
        {
            if (myVLC.MediaPlayer == null) return;
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
            } else
            {
                paused = true;
                myVLC.MediaPlayer.Pause();
            }
            PlayPauseButton_MouseLeave(sender, e);
        }

        private void PlayPauseButton_MouseHover(object sender, EventArgs e)
        {
            if (paused)
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[1];
            }
            else
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[4];
            }
        }

        private void PlayPauseButton_MouseLeave(object sender, EventArgs e)
        {
            if (paused)
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[0];
            }
            else
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[3];
            }
        }

        private void PlayPauseButton_MouseHover(object sender, MouseEventArgs e)
        {
            if (paused)
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[1];
            }
            else
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[4];
            }
        }

        private void TFSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] TimeScales = {10, 15, 30, 60, 120, 300, 600 };
            TimeFrame = TimeScales[TFSelect.SelectedIndex];           

            DAT.SetDispLength(TimeFrame);
            if (TimeFrame > 30) DAT.oneCol = true;
            else DAT.oneCol = false;
            Redraw = true;
            UpdateDisplay();
        }

        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            myVLC.MediaPlayer.Pause();
            paused = true;
            PlayPauseButton.BackgroundImage.Dispose();
            PlayPauseButton.BackgroundImage = PausePlayList.Images[0];
            
            //myVLC.MediaPlayer.Position = temp / 100f;
            myVLC.MediaPlayer.Pause();
            
        }

        private void PMEEGView_Load(object sender, EventArgs e)
        {

        }

        private void LowRes_Click(object sender, EventArgs e)
        {
            if (!hiRes) return;
            this.MinimumSize = new Size(863, 500);
            resChange(false);
            this.Width = 1040;
            this.Height = 600;

            
            Point tempP = new Point(myVLC.Location.X - 5, this.Size.Height - 122);
            

            
            PlayPauseButton.Location = tempP;

            tempP = new Point(PlayPauseButton.Location.X + 43, PlayPauseButton.Location.Y + 9);
            TimeLabel.Location = tempP;

            tempP = new Point(myVLC.Location.X - 8, 24);
            GVGrouping.Location = tempP;



        }
                   




                

            

                     

        private void HighRes_Click(object sender, EventArgs e)
        {
            if (hiRes) return;
            resChange(true);

            this.MinimumSize = new Size(1111, 643);
            this.Width = 1200;
            this.Height = 800;
            
                    Point tempP = new Point(myVLC.Location.X - 8, this.Size.Height - 122);
                    
               
                    
                    PlayPauseButton.Location = tempP;
                
                    tempP = new Point(PlayPauseButton.Location.X + 43, PlayPauseButton.Location.Y + 9);
                    TimeLabel.Location = tempP;

            tempP = new Point(myVLC.Location.X - 8, 24);
            GVGrouping.Location = tempP;



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
            } else
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
            } else
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

        private void Next_Click(object sender, EventArgs e)
        {
            if (pageNum * numPerPage + numPerPage > numDats) return;
            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }
            PlayPauseButton_MouseLeave(null, null);
            ResetChosenVid();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Stop();
            pageNum += 1;
            Redraw = true;
            
            UpdateDisplay();
            pg.Text = (pageNum + 1) + " / " + pageCalc();
            if (ViewMode == 1) Gg.Clear(Color.White);
            threadLock = false;
            


        }

        private void Previous_Click(object sender, EventArgs e)
        {
            
            if (pageNum == 0) return;
            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }
            PlayPauseButton_MouseLeave(null, null);
            ResetChosenVid();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Stop();
            pageNum -= 1;
            Redraw = true;
            
            UpdateDisplay();
            pg.Text = (pageNum + 1)+ " / " + pageCalc();
            ResetChosenVid();
            if (ViewMode == 1) Gg.Clear(Color.White);
            threadLock = false;
        }

        private void ResetChosenVid()
        {
            vidShow = new bool[numPerPage];
            for (int i = 0; i < vidShow.Count(); i++)
            {
                vidShow[i] = false;
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
            NotesShow.Items.Clear();

            paused = true;
            threadLock = true;
            while (!TDDone)
            {
                Thread.Sleep(10);
            }
            
            
            PlayPauseButton_MouseLeave(null, null);
            if (ViewMode == 1)
            {
                tX1 = GalArea.Location.X;
                tX2 = GalArea.Location.X + GalArea.Width;
                tY1 = GalArea.Location.Y;
                tY2 = GalArea.Location.Y + GalArea.Height;
            } else
            {
                tX1 = graph.X1;
                tX2 = graph.X2;
                tY1 = graph.Y1;
                tY2 = graph.Y2;
            }


            if ((e.X >= tX1) && (e.X < tX2) && (e.Y >= tY1) && ((e.Y < tY2)))
            {

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


                int Y = (e.Y - tY1);
                Y = Y / ((tY2 - tY1) / (numPerPage / XSplit));
                

                int temp = pageNum * numPerPage + (Y * 2) + X;

                if (ViewMode == 1)
                {
                    temp = pageNum * numPerPage + Y + X;
                    position = 0;

                    if (Offset[temp].Selected)
                    {
                        Offset[temp].Selected = false;
                        selected = -1;
                        seconds = -1;
                    }
                    else
                    {
                        if (SelectedTotal != -1) Offset[SelectedTotal].Selected = false;
                        SelectedTotal = temp;
                        Offset[temp].Selected = true;
                        ListViewItem tempL = new ListViewItem();
                        tempL.Text = (Animals[Offset[temp].AnimalIndex].ID);
                        tempL.SubItems.Add(Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].Severity.ToString());
                        tempL.SubItems.Add(Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].Notes);
                        NotesShow.Items.Add(tempL);
                        seconds = (Animals[Offset[temp].AnimalIndex].Sz[Offset[temp].SZNum].length);
                        selected = Y;
                        
                          
                    }
                    if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Stop();
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

        public void VideoPlay(int index)
        {
            int offset = numPerPage * pageNum + index;



           



            
            //if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            //params String[] options = new string[] { "--start-paused", "--no-playlist-autostart" };
            Media media = new Media(vlc, vidDir + Animals[Offset[offset].AnimalIndex].Sz[Offset[offset].SZNum].VidString);
            //media.AddOption("--start-paused");
            //media.AddOption("--no-playlist-autostart");
            
            MediaPlayer player = new MediaPlayer(media);

            


            myVLC.MediaPlayer = player;
            myVLC.MediaPlayer.Play();
            Thread.Sleep(100);
            
            
            myVLC.MediaPlayer.Pause();
            myVLC.MediaPlayer.Position = 0;
            paused = true;
            media.Dispose();
            myVLC.Show();

        }

        private void TimeBar_MouseDown(object sender, MouseEventArgs e)
        {
            timeBarAccess = true;
        }

        private void TimeBar_MouseUp(object sender, MouseEventArgs e)
        {
            timeBarAccess = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 1; i++)
            {

            }
        }

        private void ShowNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowNotes.Checked)
            {
                NotesShow.Visible = true;
            } else
            {
                NotesShow.Visible = false;
            }
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

        private void PMEEGView_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseBusy) return;
            mouseBusy = true;
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
                    
                    position = (float)tempTimeSelect / 2f ;

                    Console.WriteLine("X:" + e.X + " | Y:" + e.Y + $" | RelX:{RelX} | RelY:{RelY} | SplitUp:{SplitUp} | TempTime:{tempTimeSelect} + | Pos:{position}");

                    if (myVLC.MediaPlayer != null)
                    {
                        if (myVLC.MediaPlayer.IsPlaying) myVLC.MediaPlayer.Pause();
                        myVLC.MediaPlayer.Position = position * 1000;
                    }

                    needsMainDraw = true;
                    threadLock = false;

                }
            }

            mouseBusy = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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