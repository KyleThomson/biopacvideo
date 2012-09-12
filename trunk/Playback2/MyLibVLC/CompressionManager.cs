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
    public partial class CompressionManager : Form
    {
        IniFile CMINI;
        IniFile BioINI;        
        int CurrentFile;
        int CurrentDir;
        Stopwatch st;
        TimeSpan Length;
        Thread CT;
        int LastCount;
        bool Run;
        public CompressionManager()
        {
            InitializeComponent();
            string status; 
            CMINI = new IniFile(Directory.GetCurrentDirectory() + "\\CompressionManager.ini");
            string Dir = "!";
            int i = 0;
            while (Dir != "")
            {
                Dir = CMINI.IniReadValue("General", "File" + i.ToString(), "");
               
                if (Dir != "")
                {
                    status = CMINI.IniReadValue("General", "Status" + i.ToString(), "");
                    FileList.Items.Add(Dir);
                    StatusBox.Items.Add(status);
                    i++;
                }
            }
            CurrentDir = CMINI.IniReadValue("General", "CurrentDir", 0);
            CurrentFile = CMINI.IniReadValue("General", "CurrentFile", 0);
            if (FileList.Items.Count > 0)
                FileList.SelectedIndex = Math.Min(CurrentDir, FileList.Items.Count - 1);

            
        }
        public void process_OutputDataReceived(object sender, DataReceivedEventArgs e) //Need to make sure the compression hasn't frozen. 
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
            FileInfo FI;
            bool ResetFailCount = false;
            string Command;            
            bool fail = false;
            string[] IniFiles;
            int failcount = 0;
            st = new Stopwatch();
            string[] AVIFiles;
            string BaseName;
            string CurPath;
            this.Invoke((MethodInvoker)delegate { this.Text = "Compression Manager - Running"; });
            for (int j = CurrentDir; j < FileList.Items.Count; j++)
            {
                CurPath = FileList.Items[j].ToString();               
                AVIFiles = Directory.GetFiles(CurPath, "2*.avi");
                FileList.Invoke((MethodInvoker)delegate { FileList.SelectedIndex = j; });
                TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Maximum = AVIFiles.Length; });
                TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Value = CurrentFile; });
                BaseName = AVIFiles[0].Substring(CurPath.Length + 1, 15);
                for (int i = CurrentFile; i < AVIFiles.Length; i++)
                {
                    FI = new FileInfo(AVIFiles[i]);                    
                    CurrentFile = i;
                    UpdateINI();
                    Command = "-i " + AVIFiles[i] + " -y -vcodec libx264 -crf 28 -coder 0 -an ";
                    Command += CurPath + "\\temp.avi";
                    CurFileProg.Invoke((MethodInvoker)delegate { CurFileProg.Value = 0; });
                    CurrentLabel.Invoke((MethodInvoker)delegate { CurrentLabel.Text = "Current File: " + AVIFiles[i]; });
                    TotalLabel.Invoke((MethodInvoker)delegate { TotalLabel.Text = "Total Progress: " + (i + 1).ToString() + " of " + AVIFiles.Length.ToString(); });
                    Recomp = new Process();
                    Recomp.StartInfo = new ProcessStartInfo("C:\\x264\\ffmpeg.exe", Command);
                    Recomp.StartInfo.CreateNoWindow = true;
                    Recomp.StartInfo.UseShellExecute = false;
                    Recomp.StartInfo.RedirectStandardOutput = true;
                    Recomp.StartInfo.RedirectStandardError = true;                    
                    Recomp.ErrorDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                    Recomp.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                    Recomp.Start();
                    Recomp.BeginOutputReadLine();
                    Recomp.BeginErrorReadLine();
                    st.Start();
                    while (!Recomp.WaitForExit(1000))
                    {
                        if (!Run)
                        {
                            //Need to stop compression
                            Recomp.Kill();
                            Recomp.WaitForExit(); 
                            fail = true; //This shouldn't be needed... but...  
                            this.Invoke((MethodInvoker)delegate { this.Text = "Compression Manager - Stopped"; });
                            return; //This should hopefully kill the thread. 
                        }
                        if (st.ElapsedMilliseconds > 300000)
                        {
                       
                            if (!ResetFailCount)
                            {
                                Recomp.Kill();
                                Recomp.WaitForExit();
                                ResetFailCount = true;
                                i--;
                                fail = true;
                            }
                            else
                            {
                                Recomp.Kill();
                                Recomp.WaitForExit();
                                fail = true;
                                failcount++;
                                FailCountLbl.Invoke((MethodInvoker)delegate { FailCountLbl.Text = "Fail Count: " + failcount.ToString(); });
                                TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Increment(1); });
                                ResetFailCount = false;
                            }
                        }
                    }                    
                    if ((File.Exists(CurPath + "\\temp.avi")) & !fail)
                    {
                        File.Delete(AVIFiles[i]);
                        File.Move(CurPath + "\\temp.avi", AVIFiles[i]);
                        TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Increment(1); });
                        ResetFailCount = false;
                    }
                    fail = false;
                }
                IniFiles = Directory.GetFiles(CurPath, "*_Settings.txt");
                BioINI = new IniFile(IniFiles[0]);
                BioINI.IniWriteValue("Review", "Compressed", true);
                StatusBox.Invoke((MethodInvoker)delegate { StatusBox.Items[j] = "Compressed"; });
                CurrentFile = 0;
                CurrentDir++;
                UpdateINI();
            }
            this.Invoke((MethodInvoker)delegate { this.Text = "Compression Manager - Finished"; });
        }
        private void UpdateINI()
        {
            int i = 0;
            while (i < FileList.Items.Count) 
            {
                CMINI.IniWriteValue("General", "File" + i.ToString(), FileList.Items[i].ToString());
                CMINI.IniWriteValue("General", "Status" + i.ToString(), StatusBox.Items[i].ToString());
                i++;
            }
            CMINI.IniWriteValue("General", "File" + i.ToString(), "");
            CMINI.IniWriteValue("General", "CurrentDir", CurrentDir);
            CMINI.IniWriteValue("General", "CurrentFile", CurrentFile);

        }

        private void AddDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            string[] IniFiles;
            double PercentCompletion; 
            bool Compressed; 
            if (FBD.SelectedPath != "")
            {
                FileList.Items.Add(FBD.SelectedPath);
                IniFiles = Directory.GetFiles(FBD.SelectedPath, "*_Settings.txt");
                if (IniFiles.Length == 1)
                {
                    BioINI = new IniFile(IniFiles[0]);
                    PercentCompletion = BioINI.IniReadValue("Review", "Complete", (double)0);
                    Compressed = BioINI.IniReadValue("Review", "Compressed", false);
                    if (PercentCompletion > 99)
                    {
                        if (Compressed)
                        {
                            StatusBox.Items.Add("Compressed");
                        }
                        else
                        {
                            StatusBox.Items.Add("Uncompressed");
                        }

                    }
                    else
                    {
                        if (Compressed)
                        {
                            StatusBox.Items.Add("NR - Compressed");
                        }
                        else
                        {
                            StatusBox.Items.Add("NR - Uncompressed");
                        }

                    }
                }
                else
                {
                    StatusBox.Items.Add("Error no ini");
                }
            }
            UpdateINI();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (FileList.Items.Count == 0) //If there is nothing to do, do nothing. Very Zen
                return;
            Run = true; //Can't directly communicate with thread, so we use a flag
            CT = new Thread(new ThreadStart(CompThread)); 
            CT.Start();  //Start the Comp thread.       
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            this.Text = "Compression Manager - Stopping";
            Run = false; //Set the flag to stop - let the thread handle the specifics. 
        }

        private void ClrList_Click(object sender, EventArgs e)
        {
            FileList.Items.Clear();
            StatusBox.Items.Clear();
            CurrentDir = 0;
            CurrentFile = 0;
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            int i = FileList.SelectedIndex;
            if (!Run && i > -1)
            {                          
                    FileList.Items.RemoveAt(i);
                    StatusBox.Items.RemoveAt(i);             
            }
        }           
    }
}
