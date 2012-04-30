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
    public partial class Compression : Form
    {
        string [] AVIFiles;
        string Path;
        Thread CT;                
        public Compression(string P)
        {
            InitializeComponent();
            Path = P;
            AVIFiles = Directory.GetFiles(Path, "*.avi");  
            
        }        
        private void CompThread()
        {
             Process Recomp;
            
            string Command;
            string SOut;
            int Dur = 0;            
                for (int i = 0; i < AVIFiles.Length; i++)
                {
                    Command = "-i " + AVIFiles[i] + " -vcodec libx264 -crf 33 -coder 0 -an ";
                    Command += Path + "\\temp.avi";
                    CurrentLabel.Invoke( (MethodInvoker) delegate{ CurrentLabel.Text = "Current File: " + AVIFiles[i];});
                    TotalLabel.Invoke((MethodInvoker)delegate { TotalLabel.Text = "Total Progress: " + (i + 1).ToString() + " of " + AVIFiles.Length.ToString();});                    
                    Recomp = new Process();
                    Recomp.StartInfo = new ProcessStartInfo("C:\\x264\\ffmpeg.exe", Command);
                    //Recomp.StartInfo.CreateNoWindow = true;
                    //Recomp.StartInfo.UseShellExecute = false;
                    //Recomp.StartInfo.RedirectStandardOutput = true;
                    Recomp.Start();
                    /*while (Dur == 0)
                    {
                        SOut = Recomp.StandardOutput.ReadLine();
                        if (SOut.IndexOf("Duration: ") != -1)
                        {
                            MessageBox.Show("OMG HI");
                            Dur = 3; 
                        }
                    }
                    while (!Recomp.HasExited)
                    {
                        //Recomp.StandardOutput
                    }*/
                    Recomp.WaitForExit();
                    if (File.Exists(Path + "\\temp.avi"))
                    {
                        File.Delete(AVIFiles[i]);
                        File.Move(Path + "\\temp.avi", AVIFiles[i]);
                    }                    
                    TotProgress.Invoke((MethodInvoker) delegate { TotProgress.Increment(1); } );
                }
                StartComp.Enabled = true;
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
