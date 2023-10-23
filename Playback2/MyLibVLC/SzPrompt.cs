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
        public List<infopass> VideoList;
        //public List<infopass> VideoList;
        Thread CT;
        public SzPrompt(List<infopass> PassList)
        {
            InitializeComponent();
            Notes = "";
            Unknown.Checked = true;
            OKBtn.Enabled = false;
            Ok = false;
            VideoList = PassList; 
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
            else if (S5Pop.Checked) // added new seizure classifications - SH
                Pass.Stage = 6;
            else if (Dravet.Checked)
                Pass.Stage = 7;
            else if (Status.Checked)
                Pass.Stage = 8;
            CT = new Thread(new ThreadStart(ExtractThread));
            CT.Start();
            if (Pass.VideoCapture)
            {
                VideoList.Add(Pass); //add the seizure information to a list for video processing purposes -SH
            }
        }

        public List<infopass> GetVidList()
        {
            return VideoList; //to pass it back to the mainform - SH
        }
        private void ExtractThread()
        {
            Ok = true;

            int BuffDiff = 0;
            Result = Pass.Sz + Notes + "," + Pass.outfile;
            string outfile = Pass.FPath + "\\" + Pass.outfile;
            int StartTime;
            int LengthBuff = Pass.length + 60;
            int StartTimeBuff = Pass.StartTime - 30;
            if (StartTimeBuff < 0)
            {
                //LengthBuff = LengthBuff - (Math.Abs(StartTimeBuff));
                BuffDiff = -(30 - Math.Abs(StartTimeBuff));
                StartTimeBuff = 0;
            }
            if (StartTimeBuff + LengthBuff > Pass.ACQ.FileTime)
            {
                BuffDiff = (StartTimeBuff + LengthBuff) - Pass.ACQ.FileTime;
                //LengthBuff = LengthBuff - SizeDiff;
            }
            //Pass.ACQ.DumpData(outfile + ".dat", Pass.ACQ.SelectedChan, Pass.StartTime, Pass.HighlightEnd - Pass.HighlightStart + 1);
            Pass.ACQ.DumpData(outfile + ".dat", Pass.ACQ.SelectedChan, StartTimeBuff, LengthBuff, BuffDiff); //BuffDiff Serves the edge case of a seizure buffer that extends before or after the seizure. If 
                                                                                                             // it is negative, it indicates the extension is at the start, positive is at the end. 0 is no difference
                                                                                                             //work on fixing video capture
            if (Pass.VideoCapture) // add the video information to a list to be processed at the end - SH
            {
                Pass.LengthBuff = LengthBuff;
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
            OKBtn.Enabled = true;


        }
        private void RadioButtonClicked(object sender, EventArgs e)
        {
            OKBtn.Enabled = true;


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

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e) // what does this do? - SH
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
                        CurFileProg.Invoke((MethodInvoker)delegate { CurFileProg.Increment(frames); }); // need to figure this out - SH
                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShortCapWarning_Click(object sender, EventArgs e)
        {

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
        public int EndTime;
        public int Stage;
        public double duration;
        public string AVIMode;
        public string X264path;
        public bool VideoCapture;
        public int LengthBuff;
        public long subVidOffset;
        public infopass()
        { }
    }

}
