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
        public string vidDir;
        public bool vidEnable;
        public Thread ThreadDisplay;
        public List<OffsetName> Offset;
        public bool Redraw;
        public int numPerPage;
        public bool hiRes = true;
        public Graphics g;
        public MyGraph graph;
        bool pauseDisplay = false;
        

        public PMEEGView(List<AnimalType> AL, int dc, string fn)
        {
            InitializeComponent();
            Animals = AL;
            numDats = dc;
            DAT = new DATR(fn);
            pageNum = 0;
            ViewMode = 0;
            vidDir = Directory.GetParent(fn).ToString() + "\\Videos";
            if (Directory.Exists(vidDir)) vidEnable = true;
            TFSelect.SelectedIndex = 0;
            graph = new MyGraph();
            graph.X1 = 5;
            graph.X2 = this.Size.Width - 25;
            graph.Y1 = this.menuStrip1.Location.X + (this.menuStrip1.Height + 5);
            graph.Y2 = this.BottomLabel.Location.Y - 10;
            ListCreation();
            #region FORM EVENTS

            this.Resize += new System.EventHandler(this.MainForm_Resize);





            #endregion



            this.BackColor = Color.Black;
            this.Opacity = 100;
            this.Refresh();
            g = this.CreateGraphics();
            DAT.initDisplay(graph.X2 - graph.X1, graph.Y2 - graph.Y1);
            numPerPage = 8;

            Redraw = false;


            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            ThreadDisplay.Start();
            Redraw = true;

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            

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

                            for (int i = 0; i < numPerPage; i++)
                            {
                                if (count >= Offset.Count) break;
                                DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, 0, i, i);
                                Console.WriteLine(Animals[Offset[count].AnimalIndex].ID);
                                count++;
                            }
                            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                            Redraw = false;

                        }
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
                                DAT.DrawSZ(Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].Offset, Animals[Offset[count].AnimalIndex].Sz[Offset[count].SZNum].length, i % 2, i / 2, i);
                                Console.WriteLine(Animals[Offset[count].AnimalIndex].ID);
                                count++;
                            }
                            g.DrawImage(DAT.offscreen, graph.X1, graph.Y1);
                            Redraw = false;

                        }


                        if (ViewMode == 1 && myVLC.MediaPlayer != null)
                        {
                            TimeLabel.Invoke(new MethodInvoker(delegate
                        {
                            TimeLabel.Text = (Math.Round((myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length) / 1000f)).ToString();
                            TimeBar.Value = (int)(myVLC.MediaPlayer.Position * myVLC.MediaPlayer.Length / 1000f);

                        }));
                            


                        
                        }
                        

                    }
                }
            }

            


        }



        private void PMEEGView_Close(object sender, FormClosedEventArgs e)
        {
            ThreadDisplay.Abort();

            if (vlc != null)
            {
                myVLC.MediaPlayer.Dispose();
                myVLC.Dispose();
                
                vlc.Dispose();
            }
            //ProjectManager.ActiveForm.Show();
           
            
        }



        

 

        private void normalListToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            if (paused)
            {
                paused = false;
                myVLC.MediaPlayer.Play();
            } else
            {
                paused = true;
                myVLC.MediaPlayer.Pause();
            }
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
            int[] TimeScales = { 30, 60, 120, 300, 600 };
            TimeFrame = TimeScales[TFSelect.SelectedIndex];           

            DAT.SetDispLength(TimeFrame);
            Redraw = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myVLC.Show();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            
            Media media = new Media(vlc, "D:\\TestData\\NewRatFR\\20220723-000024\\Seizure\\20220723-000024_00_S9.avi");
            MediaPlayer tempmp = new MediaPlayer(media);
            myVLC.MediaPlayer = tempmp;
            myVLC.MediaPlayer.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myVLC.Show();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            Media media = new Media(vlc, "D:\\TestData\\New Mouse noFR MP4\\20220725-000003\\Seizure\\20220725-000003_008_S1.mp4");
            MediaPlayer tempmp = new MediaPlayer(media);
            myVLC.MediaPlayer = tempmp;
            myVLC.MediaPlayer.Play();
            Thread.Sleep(50);
            this.TimeBar.Maximum = (int)Math.Round(myVLC.MediaPlayer.Length / 1000f);
            myVLC.MediaPlayer.Pause();
            paused = true;
            media.Dispose();
            
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
            DAT.yOff = 104 - vScrollBar1.Value;
            Redraw = true;
        }
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
 */