using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using Ini;

using MPCLASS = Biopac.API.MPDevice.MPDevImports;
using MPCODE = Biopac.API.MPDevice.MPDevImports.MPRETURNCODE;

namespace BioPacVideo
{
    public partial class MainForm : Form
    {
        IniFile BioIni;
        MPTemplate MP;
        VideoTemplate Video;
        FeederTemplate Feeder;
        Pen BoxPen;
        int TickCount = 0;
        FolderBrowserDialog FBD;
        RatTemplate[] Rats;
        Bitmap Still;                
        Thread ThreadDisplay;        
        Graphics g;
        bool RunDisplayThread; 
        public MainForm()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
            Video = VideoTemplate.Instance;
            Feeder = new FeederTemplate();
            BoxPen = new Pen(Brushes.Black, 4);            
            BioIni = new IniFile(Directory.GetCurrentDirectory() + "\\BioPacVideo.ini"); 
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            for (int i = 0; i < 16; i++)
            {
                IDC_RATSELECT.Items.Add(string.Format("Rat {0}", i + 1));
            }
            //Load mpdev
            if (!File.Exists(@".\mpdev.dll"))
            {
                MessageBox.Show("mpdev.dll not found in " + Directory.GetCurrentDirectory() + "\nBioPac will not connect without this file!", "Missing DLL File",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MP.isconnected = MP.Connect();
                if (!MP.isconnected)
                {
                    MessageBox.Show("BioPac failed to connect.\nError was " + MPTemplate.MPRET[(int)MP.MPReturn], "BioPac Comnmunication Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    IDT_BIOPACSTAT.Text = "BioPac Connected";
                }
                IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
            }                           
            Rats = RatTemplate.NewInitArray(16);
            g = this.CreateGraphics();
            Still = new Bitmap("NoSignal.Bmp");
            MP.FileCount = 0;            
            RecordingButton.BackColor = Color.Green;
            ReadINI(BioIni); //Read Presets from INI file
            IDC_RATSELECT.SelectedIndex = MP.SelectedChannel-1;            
        }
        

        //Read INI presets 
        private void ReadINI(IniFile BioIni)
        {
            MP.RecordingDirectory = BioIni.IniReadValue("General", "RecDirectory", Directory.GetCurrentDirectory());
            MP.SampleRate = BioIni.IniReadValue("BioPac", "SampleRate", 1000);
            MP.SelectedChannel = BioIni.IniReadValue("BioPac", "Selected Channel", 1);            
            MP.DisplayLength = BioIni.IniReadValue("BioPac", "DisplayLength", 10);
            MP.Voltage = BioIni.IniReadValue("BioPac", "Voltage(mV)", 500);
            BioIni.IniReadValue("Feeder", "Breakfast", out Feeder.Breakfast);
            BioIni.IniReadValue("Feeder", "Lunch", out Feeder.Lunch);
            BioIni.IniReadValue("Feeder", "Dinner", out Feeder.Dinner);
            Feeder.PelletsPerGram = BioIni.IniReadValue("Feeder", "PelletsPerGram", 0);
            Feeder.Enabled = BioIni.IniReadValue("Feeder", "Enabled", true);
            for (int i = 0; i < 16; i++)
            {
                MP.RecordAC[i] = BioIni.IniReadValue("BioPac", string.Format("Channel{0}", i), true);
            }
            for (int i = 0; i < 16; i++)
            {
                Rats[i].Weight = BioIni.IniReadValue("Rats", string.Format("Rat{0} (g)", i), (double)0);
                Rats[i].Medication = BioIni.IniReadValue("Rats", string.Format("Rat{0}Medicate", i), true);
                Rats[i].Surgery = BioIni.IniReadValue("Rats", string.Format("Rat{0} Surgery", i));
                Rats[i].Injection = BioIni.IniReadValue("Rats", string.Format("Rat{0} Injection", i));
                Rats[i].FirstSeizure = BioIni.IniReadValue("Rats", string.Format("Rat{0} FirstSeizure", i));
            }
            Video.XRes = BioIni.IniReadValue("Video", "XRes", 320);
            Video.YRes = BioIni.IniReadValue("Video", "YRes", 240);
            Video.Quant = BioIni.IniReadValue("Video", "Quant", 4);
            Video.KeyFrames = BioIni.IniReadValue("Video", "KeyFrames", 100);
            for (int i = 0; i < 16; i++)
            {
                Video.Brightness[i] = BioIni.IniReadValue("Video", string.Format("Bright{0}", i), 50);
                Video.Contrast[i] = BioIni.IniReadValue("Video", string.Format("Contrast{0}", i), 50);
                Video.Hue[i] = BioIni.IniReadValue("Video", string.Format("Hue{0}", i), 50);
                Video.Saturation[i] = BioIni.IniReadValue("Video", string.Format("Satur{0}", i), 50);
            }
            
        }
        //Write INI file - used for settings and saving recording settings in recording directory
        private void UpdateINI(IniFile BioIni)
        {
            BioIni.IniWriteValue("General", "RecDirectory", MP.RecordingDirectory);
            BioIni.IniWriteValue("BioPac", "SelectedChannel", MP.SelectedChannel);
            BioIni.IniWriteValue("BioPac", "SampleRate", MP.SampleRate.ToString());
            BioIni.IniWriteValue("BioPac", "DisplayLength", MP.DisplayLength.ToString());
            BioIni.IniWriteValue("BioPac", "Voltage(mV)", MP.Voltage.ToString());
            BioIni.IniWriteValue("Feeder", "Breakfast", Feeder.Breakfast.ToString());
            BioIni.IniWriteValue("Feeder", "Lunch", Feeder.Lunch.ToString());
            BioIni.IniWriteValue("Feeder", "Dinner", Feeder.Dinner.ToString());
            BioIni.IniWriteValue("Feeder", "PelletsPerGram", Feeder.PelletsPerGram.ToString());
            BioIni.IniWriteValue("Feeder", "Enabled", Feeder.Enabled);
            for (int i = 0; i < 16; i++)
            {
                BioIni.IniWriteValue("BioPac", string.Format("Channel{0}", i), MP.RecordAC[i]);
            }
            for (int i = 0; i < 16; i++)
            {
               BioIni.IniWriteValue("Rats", string.Format("Rat{0} (g)", i), Rats[i].Weight.ToString());
               BioIni.IniWriteValue("Rats", string.Format("Rat{0}Medicate", i), Rats[i].Medication);
               BioIni.IniWriteValue("Rats", string.Format("Rat{0} Surgery", i), Rats[i].Surgery.ToShortDateString());
               BioIni.IniWriteValue("Rats", string.Format("Rat{0} Injection", i), Rats[i].Injection.ToShortDateString());
               BioIni.IniWriteValue("Rats", string.Format("Rat{0} FirstSeizure", i), Rats[i].FirstSeizure.ToShortDateString());
            }
            BioIni.IniWriteValue("Video", "XRes", Video.XRes);
            BioIni.IniWriteValue("Video", "YRes", Video.YRes);
            BioIni.IniWriteValue("Video", "Quant", Video.Quant);
            BioIni.IniWriteValue("Video", "KeyFrames", Video.KeyFrames);
            for (int i = 0; i < 16; i++)
            {
                BioIni.IniWriteValue("Video", string.Format("Bright{0}", i), Video.Brightness[i]);
                BioIni.IniWriteValue("Video", string.Format("Contrast{0}", i), Video.Contrast[i]);
                BioIni.IniWriteValue("Video", string.Format("Hue{0}", i), Video.Hue[i]);
                BioIni.IniWriteValue("Video", string.Format("Satur{0}", i), Video.Saturation[i]);
            }
        }


       
        private void DisplayThread()
        {            
            while (RunDisplayThread)
            {
                Thread.Sleep(30);
                IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
                Still.Dispose();       
                g.DrawImage(MP.offscreen, 50, this.Height-300);
                Video.pDF = VideoWrapper.GetCurrentBuffer();
            if (Video.pDF != null)
            {
                Still = new Bitmap(Video.XRes, Video.YRes, Video.XRes * 3, PixelFormat.Format24bppRgb, Video.pDF);
                Still.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            else
            {
                Still = new Bitmap("NoSignal.Bmp");
            }            
            g.DrawImage(Still, 322, 52, 320, 240);
            IDT_VIDEOSTATUS.Text = Video.GetResText();
            IDT_ENCODERRESULT.Text = Video.EncoderStatus();
            IDT_ENCODERSTATUS.Text = VideoWrapper.GetEncRes().ToString();
                TickCount++;
                TickCountLabel.Text = string.Format("Buffer: {0}%", MP.buffull);         
           }
        }
       

        private void RecordingButton_Click(object sender, EventArgs e)
        {
            IniFile WriteOnce;
            if (MP.isconnected && Video.CapSDKStatus)
            {
                if (!MP.isrecording)
                {
                    //Start Recording   
                    MP.InitializeDisplay(this.Width, this.Height);
                    ThreadDisplay = new Thread(new ThreadStart(DisplayThread)); 
                    string DateString, RecordingDir;
                    DateString = string.Format("{0:yyyy}{0:MM}{0:dd}", DateTime.Now);
                    RecordingDir = MP.RecordingDirectory + "\\" + DateString;
                    Directory.CreateDirectory(RecordingDir);
                    MP.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;                  
                    WriteOnce = new IniFile(RecordingDir + "\\" + DateString + "_Settings.txt");
                    UpdateINI(WriteOnce);
                    //Video Stuff
                    
                    Video.FileName = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
                    Video.FileStart = 1;
                    Video.SetFileName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString);
                    Video.LoadSettings();                    
                    Video.StartRecording();
                    //IDS_ENCODERSTATUS.Text = Video.EncoderStatus();
                    IDT_VIDEOSTATUS.Text = Video.GetResText();
                    RecordingButton.Text = "Stop Recording";
                    IDT_BIOPACRECORDINGSTAT.Text = "Recording";
                    IDM_SELECTCHANNELS.Enabled = false;
                    IDM_SETTINGS.Enabled = false;
                    IDM_DISCONNECTBIOPAC.Enabled = false;
                    RecordingButton.BackColor = Color.Red;                    
                    MP.isrecording = MP.StartRecording();
                    RunDisplayThread = true;
                    ThreadDisplay.Start();
                }
                else
                {                    
                    RunDisplayThread = false;
                    IDT_BIOPACRECORDINGSTAT.Text = "Not Recording";
                    MP.isrecording = false;
                    MP.StopRecording();
                    Video.StopRecording();
                    IDM_SELECTCHANNELS.Enabled = true;
                    IDM_SETTINGS.Enabled = true;
                    IDM_DISCONNECTBIOPAC.Enabled = true;
                    //IDS_ENCODERSTATUS.Text = Video.EncoderStatus();
                    RecordingButton.Text = "Start Recording";                    
                    RecordingButton.BackColor = Color.Green;
                }
            }
            else
            {
                if (!MP.isconnected)
                {
                    MessageBox.Show("Please Connect BioPac MP150.", "BioPac Not Connected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!Video.CapSDKStatus)
                {
                    MessageBox.Show("Please Initialize Video Card.", "Video Card Not Initialized.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        private void selectDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FBD = new FolderBrowserDialog();
            this.FBD.SelectedPath = MP.RecordingDirectory;
            DialogResult result = FBD.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                MP.RecordingDirectory = FBD.SelectedPath;
            }
            UpdateINI(BioIni);
        }
        
        private void initializeBioPacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MP.isconnected)
            {
                MP.isconnected = MP.Connect();
            }
            if (!MP.isconnected)
            {
                MessageBox.Show("BioPac failed to connect.\nError was " + MPTemplate.MPRET[(int)MP.MPReturn], "BioPac Comnmunication Error",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                IDT_BIOPACSTAT.Text = "BioPac Connected";
            }
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }


        private void selectChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            RecordSelect frm = new RecordSelect(MP.RecordAC);
            frm.ShowDialog(this);                
            MP.RecordAC = frm.AC();
            frm.Dispose();
            UpdateINI(BioIni);
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }


        private void disconnectBioPacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MP.isconnected)
            {
                MP.isconnected = MP.Disconnect();
            }
            if (MP.isconnected)
            {
                MessageBox.Show("BioPac failed to disconnect.\nError was " + MPTemplate.MPRET[(int)MP.MPReturn], "BioPac Comnmunication Error",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                IDT_BIOPACSTAT.Text = "BioPac Not Connected";
            }
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RunDisplayThread = false;                
                if (MP.isrecording)
                {
                    MP.StopRecording();
                }
                if (MP.isconnected)
                {
                    MP.Disconnect();
                }
                UpdateINI(BioIni);              
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
        }    
        
        ~MainForm()
        {                
            this.Dispose();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {                   
            this.Dispose();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordSettings frm = new RecordSettings(MP.SampleRate, MP.DisplayLength, MP.Voltage);
            frm.ShowDialog(this);
            MP.SampleRate = frm.SampleRate();
            MP.DisplayLength = frm.DisplayLength();
            MP.Voltage = frm.Voltage();
            frm.Dispose();
            UpdateINI(BioIni);
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }
        private void setFeedingProtocolToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            FeederForm frm = new FeederForm(Rats, Feeder);
            frm.ShowDialog(this);
            Rats = frm.ReturnRats();
            Feeder = frm.ReturnFeeder();
            frm.Dispose();
            UpdateINI(BioIni);           
        }

        private void IDC_RATSELECT_SelectedIndexChanged(object sender, EventArgs e)
        {
            MP.SelectedChannel = IDC_RATSELECT.SelectedIndex + 1;
            Video.SelectChannel(IDC_RATSELECT.SelectedIndex);
            MP.ClearDisplay = true;
        }

        private void testFeedersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeederTester frm = new FeederTester();
            frm.ShowDialog(this);
            frm.Dispose();
        }
    
        

        private void videoSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoSettings frm = new VideoSettings();         
            frm.ShowDialog(this);
            UpdateINI(BioIni);
            frm.Dispose();
        }

        private void sensorControlToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            SensorControl frm = new SensorControl(IDC_RATSELECT.SelectedIndex);
            frm.ShowDialog(this);
            UpdateINI(BioIni);
            frm.Dispose();
        }


        private void initializeVideoCardToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            if (!Video.CapSDKStatus)
            {
                Video.initVideo();
                IDT_DEVICECOUNT.Text = string.Format("Device Count ({0})", Video.Device_Count);
                IDT_VIDEOSTATUS.Text = Video.GetResText();
                if (Video.Res == (AdvantechCodes.tagRes.SUCCEEDED))
                {
                    Video.CapSDKStatus = true;
                }
                Video.initEncoder();
            }
            
            
        }
    }
}
