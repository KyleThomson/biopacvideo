using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SeizurePlayback
{
    public partial class GetACQ : Form
    {
        string Src;
        string Dest;
        Thread CT;
        public GetACQ()
        {
            InitializeComponent();
        }
        private void CopyThread()
        {
            DirectoryInfo di = new DirectoryInfo(Src);
            DirectoryInfo[] dirs;      
            DirectoryInfo TempDir;
            FileInfo[] Temp;
            dirs = di.GetDirectories("*.");
            TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Maximum = dirs.Length; });
            TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Value = 0; }); 
            foreach (DirectoryInfo d in dirs)
            {
                Temp = d.GetFiles("*.acq");
                if (Temp.Length > 0) File.Copy(Temp[0].FullName, Dest +"\\" + Temp[0].Name);                
                Temp = d.GetFiles("*Feeder.log");
                if (Temp.Length > 0) File.Copy(Temp[0].FullName, Dest + "\\" + Temp[0].Name);
                Temp = d.GetFiles("*Settings.txt");
                if (Temp.Length > 0) File.Copy(Temp[0].FullName, Dest + "\\" + Temp[0].Name);
                if (Directory.Exists(d.FullName + "\\Seizure")) 
                {
                    TempDir = new DirectoryInfo(d.FullName + "\\Seizure");
                    Temp = TempDir.GetFiles("*.txt");
                    if (Temp.Length > 0) File.Copy(Temp[0].FullName, Dest + "\\" + Temp[0].Name);
                }
                else
                {
                    
                }
                TotProgress.Invoke((MethodInvoker)delegate { TotProgress.Increment(1); }); 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                Source.Text = FBD.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
               Destination.Text = FBD.SelectedPath;
            }
        }

        private void StartComp_Click(object sender, EventArgs e)
        {
            Src = Source.Text;
            Dest = Destination.Text;
            StartComp.Enabled = false;
            CT = new Thread(new ThreadStart(CopyThread));
            CT.Start();                
        }
    }
}
