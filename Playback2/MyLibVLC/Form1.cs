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
        private void drawbuffer()
        {
            PointF[][] WaveC;
            while (true)
            {
                _DrawHandle.WaitOne();
                if (ClearDisplay)
                {
                    lock (g)
                        g.Clear(Color.White);
                    CurPointPos = 0;
                    ClearDisplay = false;
                }
                int SamplesLeft;
                if (samplesize + CurPointPos > MaxDrawSize)
                {
                    SamplesLeft = (samplesize + CurPointPos - MaxDrawSize);
                    g.Clear(Color.White);
                    CurPointPos = 0;
                }
                else
                {
                    SamplesLeft = samplesize;
                }
                WaveC = new PointF[AcqChan][];
                for (int i = 0; i < AcqChan; i++)
                {
                    WaveC[i] = new PointF[SamplesLeft];
                }
                int SamplePos = 0;
                for (int i = 0; i < last_received; i++)
                {

                    if (SamplesLeft < samplesize)
                    {
                        if (i / AcqChan >= samplesize - SamplesLeft)
                        {
                            PointF TempPoint = new PointF(CurPointPos * PointSpacing, VoltageSpacing * ((i % AcqChan) + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax / (AcqChan)));
                            WaveC[i % AcqChan][SamplePos] = TempPoint;
                            if (i % AcqChan == AcqChan - 1)
                            {
                                SamplePos++;
                                CurPointPos++;
                            }
                        }
                    }
                    else
                    {
                        PointF TempPoint = new PointF(CurPointPos * PointSpacing, VoltageSpacing * ((i % AcqChan) + (float)0.5) + ScaleVoltsToPixel(Convert.ToSingle(draw_buffer[i]), Ymax / (AcqChan)));
                        WaveC[i % AcqChan][SamplePos] = TempPoint;
                        if (i % AcqChan == AcqChan - 1)
                        {
                            SamplePos++;
                            CurPointPos++;
                        }
                    }
                }
                if (SamplePos > 2)
                {
                    lock (g)
                        for (int i = 0; i < AcqChan; i++)
                            g.DrawLines(wavePen, WaveC[i]);
                    _DisplayHandle.Set();

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
            if (int.TryParse("60", out Length))                
            {                
                player.EncSeizureFromNow(Length);
            }
        }
    }
}
     