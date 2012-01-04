using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class MainForm : Form
    {
        VlcInstance instance;
        VlcMediaPlayer player;
        FolderBrowserDialog FBD;
        ACQReader ACQ;
        double FrameRate;
        string Path;
        Graphics g;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        public MainForm()
        {
            InitializeComponent();
            ACQ = new ACQReader();
            ACQ.initDisplay(EEGPanel.Size.Width, EEGPanel.Size.Height);           
            string[] args = new string[] {""
                //,"--vout-filter=deinterlace", "--deinterlace-mode=blend"
            };

            instance = new VlcInstance(args);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player != null) player.Dispose();
                instance.Dispose();
            }
        }


        private void Play_Click(object sender, EventArgs e)
        {
            Play.Enabled = false;
            Pause.Enabled = true;
            Stop.Enabled = true;
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null) player[i].Play();
            }           
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Play.Enabled = true;
            Pause.Enabled = true;
            Stop.Enabled = true;
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null) player[i].Pause();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Play.Enabled = true;
            Pause.Enabled = false;
            Stop.Enabled = false;
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null) player[i].Stop();
            }
        }
      
        private VlcMediaPlayer LoadFile(VlcMediaPlayer player)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return null;

            using (VlcMedia media = new VlcMedia(instance, openFileDialog1.FileName))
            {
                if (player == null)
                    player = new VlcMediaPlayer(media);
                else
                    player.Media = media;
                player.FName = System.IO.Path.GetFileName(openFileDialog1.FileName);
                player.FPath = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);                
            }

            Play.Enabled = true;
            Pause.Enabled = false;
            Stop.Enabled = false;
            return player;
        }
        private void Open_Click(object sender, EventArgs e)
        {
            FBD = new FolderBrowserDialog();
            Path = FBD.SelectedPath;

            /*player[0] = LoadFile(player[0]);           
            * 
            if (player[0] != null)
                player[0].Drawable = panel1.Handle;            */
        }

        


        
 

        private void test_Click(object sender, EventArgs e)
        {
            TimeBar.Maximum = (int)(player[0].GetLengthMs()/1000);
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
    }
}
     