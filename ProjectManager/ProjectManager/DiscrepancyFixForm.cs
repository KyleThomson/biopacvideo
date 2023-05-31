using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using LibVLCSharp.Shared;


namespace ProjectManager
{
    
    public partial class DiscrepancyFixForm : Form
    {
        public string RetTest1;
        public bool listCleared = false;
        public List<DiscrepancyItem> discList;
        public List<AnimalType> animals;
        public DATR DAT;

        public bool paused = true;
        public Graphics g;
        public Graphics gg;
        int seconds = 0;
        float pos = 0;
        Pen RedPen;
        bool endOfClip = false;
        public string vidDir;
        public MediaPlayer player;
        public LibVLC vlc;
        public DiscrepancyItem currentSZ;
        public bool buttonBusy = false;
        public bool vidExists;
        
        


        public DiscrepancyFixForm(List<DiscrepancyItem> l, List<AnimalType> a, string vD, string CDatName)
        {
            InitializeComponent();
            discList = l;
            animals = a;
            vidDir = vD;

            DAT = new DATR(CDatName);

            g = GalGBox.CreateGraphics();
            gg = this.CreateGraphics();

            DAT.initDisplay(GalGBox.Width - 2, GalGBox.Height - 2);

            foreach(DiscrepancyItem di in discList)
            {
                ListViewItem tempLVI = new ListViewItem();
                tempLVI.Text = di.an.ID;
                tempLVI.SubItems.Add(di.sz.d.ToShortDateString().ToString());
                tempLVI.SubItems.Add(di.sz.Severity.ToString());
                tempLVI.SubItems.Add(di.notes.ToString());
                

                ConflictingList.Items.Add(tempLVI);

            }

           
        }




        private void closeForm(object sender, EventArgs e)
        {
            this.RetTest1 = "It Works!";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DiscrepancyFixForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void myVLC_Click(object sender, EventArgs e)
        {

        }

        private void ConflictingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.scoreGroupBox.Enabled = true;

            if (ConflictingList.SelectedIndices.Count < 1)
            {
                return;
            }

            currentSZ = discList[ConflictingList.SelectedIndices[0]];

            notesLabel.Text = currentSZ.sz.Notes;

            if (currentSZ.sz.VidString == null)
            {
                DialogResult errorBox = MessageBox.Show("NO VIDEO DATA FOUND \n \n \t To view video, you must use Seizure Playback to view this seizure", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                vidExists = false;
            }
            else
            {
                vidExists = true;

                

            }

            UpdateDisplay();
        }




        //Section to draw the seizure in the GalGBox

        public void UpdateDisplay()
        {


            if (currentSZ == null) return;

            if (DAT.Loaded)
            {

                DAT.SetDispLength(currentSZ.sz.length);
                DAT.cleargraph();

                DAT.DrawOneSZ(currentSZ.sz, 0, 0, showBuffer.Checked);

                g.Clear(Color.White);
                g.DrawImage(DAT.offscreen, 0, 0);
                

            }


        }



        private void telem_CheckedChanged(object sender, EventArgs e)
        {
            if (telem.Checked)
            {
                DAT.Telemetry = true;
            } else
            {
                DAT.Telemetry = false;
            }

            UpdateDisplay();
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            DAT.Zoom = (float)ZoomBar.Value / 10f;
            UpdateDisplay();
        }

        private void showBuffer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void submitChangeButton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked || radioButton5.Checked || radioButton6.Checked || radioButton7.Checked)
            {

                if (radioButton1.Checked)
                {
                    currentSZ.sz.Severity = 1;
                } else if (radioButton2.Checked)
                {
                    currentSZ.sz.Severity = 2;
                }
                else if (radioButton3.Checked)
                {
                    currentSZ.sz.Severity = 3;
                }
                else if (radioButton4.Checked)
                {
                    currentSZ.sz.Severity = 4;
                }
                else if (radioButton5.Checked)
                {
                    currentSZ.sz.Severity = 5;
                }
                else if (radioButton6.Checked)
                {
                    currentSZ.sz.Severity = 0;
                }
                else if (radioButton7.Checked)
                {
                    currentSZ.sz.Severity = -1;
                }

                currentSZ.sz.Notes = notesLabel.Text;

                animals[currentSZ.anIndex].Sz[currentSZ.szIndex] = currentSZ.sz;



            } else
            {
                DialogResult errorBox = MessageBox.Show("Please Select New Rating", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }


        //Video Handling Code


        public void VideoPlay(int index)
        {
            if (!vidExists)
            {
                return;
            }

            //if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            if (myVLC.MediaPlayer != null) myVLC.MediaPlayer.Dispose();
            vlc = new LibVLC();
            //params String[] options = new string[] { "--start-paused", "--no-playlist-autostart" };
            Media media = new Media(vlc, vidDir + currentSZ.sz.VidString);
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
            UpdateDisplay();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (myVLC.MediaPlayer == null) return;
            if (buttonBusy) return;

            buttonBusy = true;

            if (paused)
            {

                if (endOfClip)
                {
                    myVLC.MediaPlayer.Position = 0;
                    pos = 0;
                    endOfClip = false;
                }

                paused = false;
                myVLC.MediaPlayer.Play();
            }

           

            buttonBusy = false;
        
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (myVLC.MediaPlayer == null) return;

            if (buttonBusy) return;

            buttonBusy = true;

            if (myVLC.MediaPlayer == null) return;

            if (!paused)
            {
                paused = true;
                myVLC.MediaPlayer.Pause();

            }

            buttonBusy = false;
        }
    }
}
