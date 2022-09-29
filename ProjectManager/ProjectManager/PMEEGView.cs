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
        int currentSel = -1;
        int seconds = 0;
        bool timeBarAccess = false;
        

        public PMEEGView(List<AnimalType> AL, int dc, string fn)
        {
            InitializeComponent();
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
            #region FORM EVENTS

            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);




            #endregion



            this.BackColor = Color.Black;
            this.Opacity = 100;
            this.Refresh();
            g = this.CreateGraphics();
            Gg = this.CreateGraphics();
            DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1, GalGBox.Width - 2, GalGBox.Height - 2);
            numPerPage = 8;
            vidShow = new bool[numPerPage];
            for (int i = 0; i < numPerPage; i++)
            {
                vidShow[i] = false;
            }

            Redraw = false;
            //do big eeg
            
            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
            Gg.DrawImage(DAT.GOffscreen, GalGraph.X1, GalGraph.Y1);
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            Redraw = true;
            pg.Text = (pageNum + 1) + " / " + pageCalc();

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
            int Delay = 0;
            Stopwatch st = new Stopwatch();
            
            //Console.WriteLine("Thread Poked");
            while (true)
            {
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
                            Gg.DrawImage(DAT.GOffscreen, GalGraph.X1, GalGraph.Y1);
                            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                            
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
                        Thread.Sleep(500);
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
                                Console.WriteLine(Animals[Offset[count].AnimalIndex].ID);
                                count++;
                            }
                            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                            Redraw = false;

                        }






                    
                        

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
                    DefaultView.Checked = true;
                    animalView.Checked = false;
                    galleryView.Checked = false;
                    VideoMode(false);
                    numPerPage = 8;
                    DAT.numPerPage = numPerPage;
                    DAT.drawMode = 0;
                    GraphResize(0);
                    Redraw = true;
                    videoSizeToolStripMenuItem.Enabled = false;





                    break;
                case "Gallery":
                    ViewMode = 1;
                    g.Clear(Color.Black);

                    Gg.Clear(Color.White);
                    
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
                TimeBar.Enabled = true;
                TimeBar.Show();
                TimeLabel.Enabled = true;
                TimeLabel.Show();
                PlayPauseButton.Enabled = true;
                PlayPauseButton.Show();
                GVGrouping.Show();
            } else
            {
                myVLC.Enabled = false;
                myVLC.Hide();
                TimeBar.Enabled = false;
                TimeBar.Hide();
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
        }

        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            myVLC.MediaPlayer.Pause();
            paused = true;
            PlayPauseButton.BackgroundImage.Dispose();
            PlayPauseButton.BackgroundImage = PausePlayList.Images[0];
            Single temp = TimeBar.Value;
            myVLC.MediaPlayer.Position = temp / 100f;
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

            TimeBar.Width = 488;
            Point tempP = new Point(myVLC.Location.X - 5, this.Size.Height - 122);
            TimeBar.Location = tempP;

            tempP = new Point(myVLC.Location.X, TimeBar.Location.Y + 34);
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
            TimeBar.Width = 650;
                    Point tempP = new Point(myVLC.Location.X - 8, this.Size.Height - 122);
                    TimeBar.Location = tempP;
               
                    tempP = new Point(myVLC.Location.X, TimeBar.Location.Y + 34);
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
            
            ThreadDisplay.Suspend();
        }

        private void PMEEGView_ResizeEnd(object sender, EventArgs e)
        {
          
            GraphResize(ViewMode);
            ThreadDisplay.Resume();
            Redraw = true;
        }

        private void telemetryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DAT.Telemetry = true;
            Redraw = true;
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            DAT.Zoom = (float)ZoomBar.Value / 10f;
            Redraw = true;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DAT.yOff = vScrollBar1.Value;
            Redraw = true;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (pageNum * numPerPage + numPerPage > numDats) return;
            pageNum += 1;
            Redraw = true;
            pg.Text = (pageNum + 1) + " / " + pageCalc();
            ResetChosenVid();

        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (pageNum == 0) return;
            pageNum -= 1;
            Redraw = true;
            pg.Text = (pageNum + 1)+ " / " + pageCalc();
            ResetChosenVid();
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

            int tX1;
            int tX2;
            int tY1;
            int tY2;

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


            if ((e.X > tX1) && (e.X < tX2) && (e.Y > tY1) && ((e.Y < tY2)))
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
                Console.WriteLine("X: " + X);
                Console.WriteLine("Y: " + Y);

                int temp = pageNum * numPerPage + (Y * 2) + X;

                if (ViewMode == 1)
                {
                    temp = pageNum * numPerPage + Y + X;


                    if (Offset[temp].Selected) Offset[temp].Selected = false;
                    else Offset[temp].Selected = true;
                    for (int i = 0; i < numPerPage; i++)
                    {
                        if (i == Y)
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

        }

        public void VideoPlay(int index)
        {
            int offset = numPerPage * pageNum + index;



            Console.WriteLine("Chosen: " + vidDir + Animals[Offset[offset].AnimalIndex].Sz[Offset[offset].SZNum].VidString);
            Console.WriteLine("Hard:   D:\\PMTests\\fvt\\Videos\\JH_060221_16-1.avi");




            myVLC.Show();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            Media media = new Media(vlc, vidDir + Animals[Offset[offset].AnimalIndex].Sz[Offset[offset].SZNum].VidString);
            
            MediaPlayer player = new MediaPlayer(media);

            


            myVLC.MediaPlayer = player;
            myVLC.MediaPlayer.Play();
            Thread.Sleep(50);
            this.TimeBar.Maximum = (int)Math.Round(myVLC.MediaPlayer.Length / 1000f);
            
            myVLC.MediaPlayer.Pause();
            paused = true;
            media.Dispose();


        }

        private void TimeBar_MouseDown(object sender, MouseEventArgs e)
        {
            timeBarAccess = true;
        }

        private void TimeBar_MouseUp(object sender, MouseEventArgs e)
        {
            timeBarAccess = false;
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