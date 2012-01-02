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
        VlcMediaPlayer[] player;
        float[] Rates = { 0.25F, 0.5F, 1, 2, 5, 10, 20, 30, 50, 100 };
        public MainForm()
        {
            InitializeComponent();
            player = new VlcMediaPlayer[4];
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "MPEG, AVI|*.mpg;*.avi|All|*.*";
            string[] args = new string[] {""
                //,"--vout-filter=deinterlace", "--deinterlace-mode=blend"
            };

            instance = new VlcInstance(args);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null) player[i].Dispose();
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null) player[i].Speed(Rates[trackBar1.Value]);
            }
            RateLabel.Text = "Speed: " + Rates[trackBar1.Value].ToString() + "x";
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
            player[0] = LoadFile(player[0]);           
            if (player[0] != null)
                player[0].Drawable = panel1.Handle;
            F1.Text = player[0].FName;            
        }

        private void Open2_Click(object sender, EventArgs e)
        {
            player[1] = LoadFile(player[1]);
            if (player[1] != null)
                player[1].Drawable = panel2.Handle;
            F2.Text = player[1].FName;
        }

        private void Open3_Click(object sender, EventArgs e)
        {

            player[2] = LoadFile(player[2]);
            if (player[2] != null)
                player[2].Drawable = panel3.Handle;
            F3.Text = player[2].FName;
        }

        private void Open4_Click(object sender, EventArgs e)
        {
            player[3] = LoadFile(player[3]);
            if (player[3] != null)
                player[3].Drawable = panel4.Handle;
            F4.Text = player[3].FName;
        }


        private void TimeBar_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player[i] != null)
                {
                    player[i].seek(TimeBar.Value);
                }
            }
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
            if (int.TryParse(SezLength.Text, out Length))                
            {                
                player[0].EncSeizureFromNow(Length);
            }
        }
    }
}
     