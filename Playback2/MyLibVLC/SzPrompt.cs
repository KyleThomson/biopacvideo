using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SeizurePlayback
{

    public partial class SzPrompt : Form
    {
        public string Notes;
        public bool Ok;        
        public string Result;
        public bool VideoCapture; 
        public infopass Pass;
        Thread CT;        
        public SzPrompt()
        {            
            InitializeComponent();
            Notes = "";
            Unknown.Checked = true;
            Ok = false;
            
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            PleaseWait.Text = "Please Wait...";
            NotesBx.Enabled = false;
            OKBtn.Enabled = false;
            CancelBtn.Enabled = false;
            Pass.VideoCapture = VideoSave.Checked;
            VideoCapture = VideoSave.Checked; 
            CurFileProg.Maximum = Pass.length * 30;
            if (S1.Checked)
                Pass.Stage = 1;
            else if (S2.Checked)
                Pass.Stage = 2;
            else if (S3.Checked)
                Pass.Stage = 3;
            else if (S4.Checked)
                Pass.Stage = 4;
            else if (S5.Checked)
                Pass.Stage = 5;
            else if (NonConv.Checked)
                Pass.Stage = 0;
            else if (Unknown.Checked)
                Pass.Stage = -1;
            CT = new Thread(new ThreadStart(ExtractThread));
            CT.Start();
        }
        private void ExtractThread()
        {
            Ok = true;
            Result = Pass.Sz + Notes + "," + Pass.outfile;
            string outfile = Pass.FPath + "\\" + Pass.outfile;
            Pass.ACQ.DumpData(outfile + ".dat", Pass.ACQ.SelectedChan, Pass.StartTime, Pass.HighlightEnd - Pass.HighlightStart + 1);
            int StartTime;
            if (Pass.VideoCapture)
            { 
                if (Pass.AVIMode == "mp4")
                    {
                        StartTime = (int)(((float)Pass.StartTime * 1000F - Pass.Subtractor) / 1000F);
                    }
                    else
                    {
                        StartTime = (int)((((float)Pass.StartTime * 1000F * (1F + Pass.VideoOffset)) - Pass.Subtractor) / 1000F);
                    }
                Process p = new Process();
                string CmdString = " -y -ss " + StartTime.ToString() + " -t " + Pass.length.ToString();
                CmdString += " -i " + Pass.CurrentAVI;
                if (Pass.AVIMode == "mp4")
                {
                    CmdString += " -sameq " + outfile + ".mp4";
                }
                else
                {
                    CmdString += " -sameq " + outfile + ".avi";
                }

                p.StartInfo.Arguments = CmdString;
                p.StartInfo.FileName = Pass.X264path + "\\ffmpeg.exe";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.ErrorDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                p.Start();
                p.BeginErrorReadLine();
                while (!p.WaitForExit(1000))
                { };
            }
            Notes = Notes.Replace(",", string.Empty);                      
            Result = Pass.Sz + Notes + ", " + Pass.outfile + ", " + Pass.Stage.ToString();
            PleaseWait.Invoke((MethodInvoker)delegate { PleaseWait.Text = "Finished!"; });
            Thread.Sleep(300);
            this.Invoke((MethodInvoker)delegate { this.Close(); });
        }

        private void NotesBx_TextChanged(object sender, EventArgs e)
        {
            Notes = NotesBx.Text;
        }
        private void RadioButtonChanged(object sender, EventArgs e)
        {         
           

            
        }
        private void SzPrompt_Load(object sender, EventArgs e)
        {
            VideoSave.Checked = Pass.VideoCapture;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Ok = false;            
            this.Close();
        }
        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            int frames;
            string X;
            if (e.Data != null)
            {

                X = e.Data.ToString();             
                if (X.IndexOf("frame=") != -1)
                {                    
                    if (int.TryParse(X.Substring(X.IndexOf("frame=") + 6, 6), out frames))
                    {
                        CurFileProg.Invoke((MethodInvoker)delegate { CurFileProg.Increment(frames); });
                    }                    
                }         
            }
        }
       

    }
    public class infopass
    {
        public ACQReader ACQ;       
        public string outfile;
        public string Sz;
        public string FPath;
        public int HighlightStart;
        public int HighlightEnd;
        public float Subtractor;
        public string CurrentAVI;
        public float VideoOffset;
        public int length;
        public int StartTime;
        public int Stage;
        public double duration;
        public string AVIMode;
        public string X264path;
        public bool VideoCapture; 
        public infopass()
        { }
    }
}
