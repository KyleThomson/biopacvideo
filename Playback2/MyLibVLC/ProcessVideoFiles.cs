using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class ProcessVideoFiles : Form
    {
        List<infopass> VidList;
        public bool close;
        private bool start;
        Thread CT; 
        public ProcessVideoFiles(List<infopass> VideoList)
        {
            InitializeComponent();
            VidList = VideoList;
            closeButton.Enabled = false;
            close = false;
            start = false;
            closeButton.Text = "Close";
            label1.Text = "Processing VIdeo Files";
            label2.Show(); 
            

            
        }
        private void StartProcess()
        {
            int i = 1; //start the seizure video processing count at 1 for the layman- SH
            //this.Show(); // maybe this opens the window? -SH
            processProg.Maximum = VidList.Count;
            processProg.Value = 0;
            closeButton.Enabled = false;
            foreach (infopass v in VidList) //process each video that has been assosciated with a seizure - SH
            {
                processProg.Value += 1;
                processVidLabel.Text = "Processing Video " + i.ToString() + "/" + VidList.Count.ToString();
                ProcessVideo(v);
                i++;
            }
            if (i == VidList.Count + 1)
            {
                processVidLabel.Text = "Complete!"; //once each video has been processed, label as complete - SH
                closeButton.Enabled = true; //allow the user to click the button to close the program - SH
                //this.Dispose(); //close the window upon completion - SH
            }
        }

        public void ProcessVideo(infopass Video)
        {
            if (Video.AVIMode == "mp4")
            {
                Video.StartTime = (int)(Video.Subtractor / 1000F);

            }
            else
            {
                Video.StartTime = (int)((Video.Subtractor - ((float)1000F * (1F + Video.VideoOffset))) / 1000F);
            }
            if (Video.StartTime - 30 < 0)
            {
                Video.StartTime = 0;
            }
            else Video.StartTime -= 30;

            Process p = new Process();
            string CmdString = " -y -ss " + Video.StartTime.ToString() + " -t " + Video.LengthBuff.ToString();
            CmdString += " -i \"" + Video.CurrentAVI + "\"";
            if (Video.AVIMode == "mp4")
            {
                CmdString += " -sameq \"" + Video.outfile + ".mp4\"";
            }
            else
            {
                CmdString += " -sameq \"" + Video.outfile + ".avi\"";
            }

            p.StartInfo.Arguments = CmdString;

            p.StartInfo.FileName = Video.X264path + "\\ffmpeg.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            //p.ErrorDataReceived += new DataReceivedEventHandler(process_OutputDataReceived); // I think this is just to make the progress bar work and we don't need that - SH
            p.Start();
            p.BeginErrorReadLine();
            while (!p.WaitForExit(1000)) //This is likely what is slowing down the program - SH
            { };
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
