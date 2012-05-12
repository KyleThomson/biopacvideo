﻿using System;
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
    public partial class Compression : Form
    {
        string [] AVIFiles;
        string Path;
        Thread CT;
        Stopwatch st;
        TimeSpan Length;
        int LastCount;
        public Compression(string P)
        {
            InitializeComponent();
            Path = P;
            AVIFiles = Directory.GetFiles(Path, "*.avi");  
            
        }

        public void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            TimeSpan tempspan;
            int maths; 
            string X;
            if (e.Data != null)
            {

                X = e.Data.ToString();
                if (X.IndexOf("Duration: ") != -1)
                {
                    TimeSpan.TryParse(X.Substring(X.IndexOf("Duration: ") + 10, 10), out Length);
                }
                if (X.IndexOf("time=") != -1)
                {
                    TimeSpan.TryParse(X.Substring(X.IndexOf("time=") + 5, 10), out tempspan);
                    maths = (int)(100 * tempspan.TotalSeconds / Length.TotalSeconds);
                    if (maths > LastCount)
                    {
                        LastCount++;
                        CurFileProg.Invoke((MethodInvoker)delegate { CurFileProg.Increment(1); });

                    }
                }
                st.Reset();
                st.Start();
            }
        }
        private void CompThread()
        {   
             Process Recomp;
          
            string Command;
            bool fail = false;
            int failcount = 0;
            st = new Stopwatch();
                for (int i = 0; i < AVIFiles.Length; i++)
                {
                    LastCount = 0;
                    Command = "-i " + AVIFiles[i] + " -y -vcodec libx264 -crf 33 -coder 0 -an ";
                    Command += Path + "\\temp.avi";
                    CurFileProg.Invoke((MethodInvoker)delegate { CurFileProg.Value = 0; });
                    CurrentLabel.Invoke( (MethodInvoker) delegate{ CurrentLabel.Text = "Current File: " + AVIFiles[i];});
                    TotalLabel.Invoke((MethodInvoker)delegate { TotalLabel.Text = "Total Progress: " + (i + 1).ToString() + " of " + AVIFiles.Length.ToString(); });                    
                    Recomp = new Process();
                    Recomp.StartInfo = new ProcessStartInfo("C:\\x264\\ffmpeg.exe", Command);
                    Recomp.StartInfo.CreateNoWindow = true;
                    Recomp.StartInfo.UseShellExecute = false;
                    Recomp.StartInfo.RedirectStandardOutput = true;
                    Recomp.StartInfo.RedirectStandardError = true;                    
                    Recomp.ErrorDataReceived += new DataReceivedEventHandler(process_OutputDataReceived); 
                    Recomp.Start();
                    Recomp.BeginErrorReadLine();                    
                    st.Start();
                    while (!Recomp.WaitForExit(1000))
                    {                                            
                        if (st.ElapsedMilliseconds > 300000)
                        {
                            
                            Recomp.Kill();
                            fail = true;
                            failcount++;
                            FailCountLbl.Invoke((MethodInvoker)delegate { FailCountLbl.Text = "Fail Count: " + failcount.ToString(); });                    
                        }
                    }       
                    if ((File.Exists(Path + "\\temp.avi")) & !fail)
                    {
                        File.Delete(AVIFiles[i]);
                        File.Move(Path + "\\temp.avi", AVIFiles[i]);
                    } 
                    TotProgress.Invoke((MethodInvoker) delegate { TotProgress.Increment(1); } );
                    fail = false; 
                }
                //StartComp.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
           
            result = MessageBox.Show("Warning: This process takes 10-20 hours and is irreversable. Are you sure you want to begin?", 
                "Compression Start", MessageBoxButtons.YesNo);            
            if (result == DialogResult.Yes)
            {

                TotProgress.Maximum = AVIFiles.Length;
                StartComp.Enabled = false;
                CT = new Thread(new ThreadStart(CompThread));
                CT.Start();

            }
        }
    }
}
