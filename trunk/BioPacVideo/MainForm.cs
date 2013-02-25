﻿using System;
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
using System.Management;

using Ini;

using MPCLASS = Biopac.API.MPDevice.MPDevImports;
using MPCODE = Biopac.API.MPDevice.MPDevImports.MPRETURNCODE;

namespace BioPacVideo
{
    public partial class MainForm : Form
    {
        IniFile BioIni; //Main Ini File
        MPTemplate MP; 
        VideoTemplate Video;
        FeederTemplate Feeder;
        string SyncName;
        StreamWriter SyncFile;
        Pen BoxPen;
        FolderBrowserDialog FBD;        
        Bitmap Still;                
        Thread ThreadDisplay;
        private Thread TimerThread;
        Graphics g;
        DriveInfo DI;
        bool RunDisplayThread; 

        public static int[] VoltageSettings = new int[] { 1, 10, 50, 100, 250, 500, 1000, 2000, 3000, 4000, 5000};
        public static int[] DisplayLengthSize = new int[] { 1, 5, 10, 30, 60 };




        public MainForm() //Form Constructior
        {
            //********** INIT VARIABLES ****************
            InitializeComponent(); //Default code                        
            MP = MPTemplate.Instance; //Pull Instance from MP Template - So we only have a single instance in all code
            Video = VideoTemplate.Instance; //Same for Video
            Feeder = FeederTemplate.Instance; //Same for Feeders
            BoxPen = new Pen(Brushes.Black, 4);            
            BioIni = new IniFile(Directory.GetCurrentDirectory() + "\\BioPacVideo.ini"); //Standard Ini Settings
            
            g = this.CreateGraphics();  //Plot window          
            
            //***************** LOAD SETTINGS *****************
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ReadINI(BioIni); //Read Presets from INI file                        

            //Start EEG and Video functions
            MP.InitializeDisplay(this.Width, this.Height);
            for (int i = 0; i < VoltageSettings.Length; i++)
            {
                if (VoltageSettings[i] < 1000)
                    VoltScale.Items.Add(string.Format("-{0} / {0} mV", VoltageSettings[i]));
                else
                    VoltScale.Items.Add(string.Format("-{0} / {0} V", VoltageSettings[i] / 1000));
            }
            for (int i = 0; i < DisplayLengthSize.Length; i++)
            {
                TimeScale.Items.Add(string.Format("{0} seconds", DisplayLengthSize[i]));
            }
            
            if (!File.Exists(@".\mpdev.dll"))
            {
                MessageBox.Show("mpdev.dll not found in " + Directory.GetCurrentDirectory() + "\nBioPac will not connect without this file!", "Missing DLL File",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MP.Enabled)
            {
                MP.isconnected = MP.Connect();
                if (!MP.isconnected)
                {
                    MessageBox.Show("BioPac failed to connect.\nError was " + MPTemplate.MPRET[(int)MP.MPReturn], "BioPac Comnmunication Error",
                 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MP.StartRecording();
                    IDT_BIOPACSTAT.Text = "BioPac Connected";
                }
                IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
            }
            bioPacEnabledToolStripMenuItem.Checked = MP.Enabled;
            //Still = new Bitmap("NoSignal.Bmp");
            MP.FileCount = 0;            
            RecordingButton.BackColor = Color.Green;                        
            Video.initVideo();
            Video.FileStart = 0;
            IDT_DEVICECOUNT.Text = string.Format("Device Count ({0})", Video.Device_Count);
            IDT_VIDEOSTATUS.Text = Video.GetResText();
            Console.WriteLine(Video.GetResText());
            if (Video.Enabled & (Video.Res == (AdvantechCodes.tagRes.SUCCEEDED)))
            {
                Video.CapSDKStatus = true;
            }
            else
            {                
                Video.Enabled = false;
            }
            videoCaptureEnabledToolStripMenuItem.Checked = Video.Enabled;
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            TimerThread = new Thread(new ThreadStart(TimerCheckThread));
            Video.UpdateCameraAssoc();
            RunDisplayThread = true;
            VoltScale.SelectedIndex = Array.IndexOf(VoltageSettings, MP.Voltage);
            TimeScale.SelectedIndex = Array.IndexOf(DisplayLengthSize, MP.DisplayLength); 
            ThreadDisplay.Start();
            TimerThread.Start();            
           
        }
     
        private void TimerCheckThread()
        {
                       
            while (true)
            {
                //If 12AM, restart recording. 
                if ((DateTime.Now.TimeOfDay.Hours == 0) & (DateTime.Now.TimeOfDay.Minutes == 0))
                {
                    if (MP.IsFileWriting)
                    {
                        StopRecording();
                        StartRecording();
                    }
                    Thread.Sleep(120000); //Always Skip the Meal;
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal1.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal1.Minutes))
                {
                    
                    Feeder.GoMeal(Feeder.GetDay()*Feeder.DailyMealCount);
                    Thread.Sleep(120000);
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal2.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal2.Minutes))
                {
                    Feeder.GoMeal(Feeder.GetDay()*4+1);
                    Thread.Sleep(120000);
                    //Lunch
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal3.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal3.Minutes))
                {
                    Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 2);
                    Thread.Sleep(120000);
                    //Dinner
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal4.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal4.Minutes))
                {
                    Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 3);
                    Thread.Sleep(120000);
                    //Brunch
                } 
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal5.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal5.Minutes))
                {
                    Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 4);
                    Thread.Sleep(120000);
                    //Midnight Snack 
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal6.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal6.Minutes))
                {
                    Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 5);
                    Thread.Sleep(120000);
                    //Hobbits 2nd Lunch
                    //PO - TA - TOES
                }
           
                Thread.Sleep(10000);
                
            }
      }
       
        private void DisplayThread()
        {
            int Cm; //To hold current camera. 
            int InnerSync = 0;
            int ChanSync = 0; 
            DI = new DriveInfo(MP.RecordingDirectory.Substring(0, 3));
            while (RunDisplayThread)
            {
                this.Invoke(new MethodInvoker(delegate { IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn]; }));
                this.Invoke(new MethodInvoker(delegate { IDT_VIDEOSTATUS.Text = Video.GetResText(); }));
                this.Invoke(new MethodInvoker(delegate { IDT_ENCSTAT.Text = Video.EncoderStatus(); }));
                this.Invoke(new MethodInvoker(delegate { IDT_FEEDST.Text = Feeder.StateText; }));
                this.Invoke(new MethodInvoker(delegate { IDT_ENCODERSTATUS.Text = VideoWrapper.GetEncRes().ToString(); }));                       
                MP._DisplayHandle.WaitOne();
                //if (Still != null)                    
                    g.DrawImage(MP.offscreen, 30, 280);
                Cm = 0;
                for (int i = 0; i < MP.TotChan(); i++)
                {
                    while (MP.RecordAC[Cm] == false)
                        Cm++;
                    Video.pDF = VideoWrapper.GetCurrentBuffer(Video.CameraAssociation[Cm]);
                    Cm++;
                    if (Video.pDF != null)
                    {
                        Still = new Bitmap(Video.XRes, Video.YRes, Video.XRes * 3, PixelFormat.Format24bppRgb, Video.pDF);
                        Still.RotateFlip(RotateFlipType.RotateNoneFlipY);
                     
                    }
                    else
                    {
                       Still = new Bitmap("NoSignal.Bmp");
                    }
                    g.DrawImage(Still, 132+(i%Video.LengthWise)*162, 32+(float)Math.Floor((decimal)(i/Video.LengthWise))*122, 160, 120);
                    Still.Dispose();
                }
                if (MP.IsFileWriting)
                {
                    int VS = Video.GetSampleCount();
                    if (VS != -1)
                    {
                        string Output = VS.ToString() + " " + Video.GetSyncInfo();
                        SyncFile.WriteLine(Output);                        
                    }
                }
                /*if ((long)(DI.AvailableFreeSpace / DI.TotalSize) < .01 && MP.IsFileWriting)
                {
                    MessageBox.Show((IWin32Window)null, "You are out of space. Recording Stopped.");
                    StopRecording();
                }*/
           
           }
        }

        //Read presets from INI file
        private void ReadINI(IniFile BioIni)
        {
            MP.RecordingDirectory = BioIni.IniReadValue("General", "RecDirectory", Directory.GetCurrentDirectory());
            MP.SampleRate = BioIni.IniReadValue("BioPac", "SampleRate", 1000);
            MP.SelectedChannel = BioIni.IniReadValue("BioPac", "Selected Channel", 1);
            MP.DisplayLength = BioIni.IniReadValue("BioPac", "DisplayLength", 10);
            MP.Voltage = BioIni.IniReadValue("BioPac", "Voltage(mV)", 500);
            MP.Gain = BioIni.IniReadValue("BioPac", "Gain", 20000);
            MP.Enabled = BioIni.IniReadValue("BioPac", "Enabled", true); 
            BioIni.IniReadValue("Feeder", "Meal1", out Feeder.Meal1);
            BioIni.IniReadValue("Feeder", "Meal2", out Feeder.Meal2);
            BioIni.IniReadValue("Feeder", "Meal3", out Feeder.Meal3);
            BioIni.IniReadValue("Feeder", "Meal4", out Feeder.Meal4);
            BioIni.IniReadValue("Feeder", "Meal5", out Feeder.Meal5);
            BioIni.IniReadValue("Feeder", "Meal6", out Feeder.Meal6);
            Feeder.PelletsPerGram = BioIni.IniReadValue("Feeder", "PelletsPerGram", 0.02);
            Feeder.Enabled = BioIni.IniReadValue("Feeder", "Enabled", true);
            Feeder.DailyMealCount = BioIni.IniReadValue("Feeder", "DailyMealCount", 4);
            for (int i = 0; i < 16; i++)
            {
                MP.RecordAC[i] = BioIni.IniReadValue("BioPac", string.Format("Channel{0}", i), true);
            }
            for (int i = 0; i < 16; i++)
            {
                Feeder.Rats[i].ID = BioIni.IniReadValue("Rats", string.Format("Rat{0} ID", i), string.Format("Rat{0}", i));
                Feeder.Rats[i].Weight = BioIni.IniReadValue("Rats", string.Format("Rat{0} (g)", i), (double)0);
                Feeder.Rats[i].Medication = BioIni.IniReadValue("Rats", string.Format("Rat{0}Medicate", i), 100);
                Feeder.Rats[i].Surgery = BioIni.IniReadValue("Rats", string.Format("Rat{0} Surgery", i));
                Feeder.Rats[i].Injection = BioIni.IniReadValue("Rats", string.Format("Rat{0} Injection", i));
                Feeder.Rats[i].FirstSeizure = BioIni.IniReadValue("Rats", string.Format("Rat{0} FirstSeizure", i));
                for (int j = 0; j < (7 * 6); j++)
                {
                    Feeder.Rats[i].Meals[j] = BioIni.IniReadValue("Rats", "Rat" + i + "Meal" + j, false);
                }
            }
            Video.Enabled = BioIni.IniReadValue("Video", "Enabled", true);
            Video.XRes = BioIni.IniReadValue("Video", "XRes", 320);
            Video.LengthWise = BioIni.IniReadValue("Video", "LengthWise", 8);
            Video.YRes = BioIni.IniReadValue("Video", "YRes", 240);
            Video.Quant = BioIni.IniReadValue("Video", "Quant", 4);
            Video.KeyFrames = BioIni.IniReadValue("Video", "KeyFrames", 100);
            for (int i = 0; i < 16; i++)
                Video.CameraAssociation[i] = BioIni.IniReadValue("Video", string.Format("Camera{0}", i), i);
            for (int i = 0; i < 32; i++)
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
            BioIni.IniWriteValue("BioPac", "Gain", MP.Gain.ToString());
            BioIni.IniWriteValue("BioPac", "Enabled", MP.Enabled);
            BioIni.IniWriteValue("Feeder", "Meal1", Feeder.Meal1.ToString());
            BioIni.IniWriteValue("Feeder", "Meal2", Feeder.Meal2.ToString());
            BioIni.IniWriteValue("Feeder", "Meal3", Feeder.Meal3.ToString());
            BioIni.IniWriteValue("Feeder", "Meal4", Feeder.Meal4.ToString());
            BioIni.IniWriteValue("Feeder", "Meal5", Feeder.Meal5.ToString());
            BioIni.IniWriteValue("Feeder", "Meal6", Feeder.Meal6.ToString());
            BioIni.IniWriteValue("Feeder", "PelletsPerGram", Feeder.PelletsPerGram.ToString());
            BioIni.IniWriteValue("Feeder", "Enabled", Feeder.Enabled);
            BioIni.IniWriteValue("Feeder", "DailyMealCount", Feeder.DailyMealCount);
            for (int i = 0; i < 16; i++)
            {
                BioIni.IniWriteValue("BioPac", string.Format("Channel{0}", i), MP.RecordAC[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} ID", i), Feeder.Rats[i].ID);
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} (g)", i), Feeder.Rats[i].Weight.ToString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0}Medicate", i), Feeder.Rats[i].Medication);
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} Surgery", i), Feeder.Rats[i].Surgery.ToShortDateString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} Injection", i), Feeder.Rats[i].Injection.ToShortDateString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} FirstSeizure", i), Feeder.Rats[i].FirstSeizure.ToShortDateString());
                for (int j = 0; j < (7 * 6); j++)
                {
                    BioIni.IniWriteValue("Rats", "Rat" + i + "Meal" + j, Feeder.Rats[i].Meals[j]);
                }
            }
            BioIni.IniWriteValue("Video", "Enabled", Video.Enabled);
            BioIni.IniWriteValue("Video", "LengthWise", Video.LengthWise);
            BioIni.IniWriteValue("Video", "XRes", Video.XRes);
            BioIni.IniWriteValue("Video", "YRes", Video.YRes);
            BioIni.IniWriteValue("Video", "Quant", Video.Quant);
            BioIni.IniWriteValue("Video", "KeyFrames", Video.KeyFrames);
            for (int i = 0; i < 16; i++)
                BioIni.IniWriteValue("Video", string.Format("Camera{0}", i), Video.CameraAssociation[i]);
            for (int i = 0; i < 32; i++)
            {

                BioIni.IniWriteValue("Video", string.Format("Bright{0}", i), Video.Brightness[i]);
                BioIni.IniWriteValue("Video", string.Format("Contrast{0}", i), Video.Contrast[i]);
                BioIni.IniWriteValue("Video", string.Format("Hue{0}", i), Video.Hue[i]);
                BioIni.IniWriteValue("Video", string.Format("Satur{0}", i), Video.Saturation[i]);
            }
        }
        private void StartRecording()
        {
            IniFile WriteOnce;
            string DateString, RecordingDir;   
            //Start Recording   
            if (!Video.EncoderStarted)
                Video.initEncoder();
            //Set up recording name based on date and time
            DateString = string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", DateTime.Now);
            RecordingDir = MP.RecordingDirectory + "\\" + DateString;
            Directory.CreateDirectory(RecordingDir);

            MP.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
            Feeder.SetLogName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString + "_Feeder.log");
            SyncName = (MP.RecordingDirectory + "\\" + DateString + "\\" + DateString + "_Sync.log"); 
            SyncFile = new StreamWriter(SyncName);
            SyncFile.AutoFlush = true;
            //Write INI file once, so we save all the settings                    
            WriteOnce = new IniFile(RecordingDir + "\\" + DateString + "_Settings.txt");
            UpdateINI(WriteOnce);
            //Video Stuff              
            Video.FileStart = 1;
            Video.FileName = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;            
            Video.SetFileName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString, Video.FileStart);
            Video.StartRecording();
            MP.isstreaming = MP.StartWriting();                                
        }

        private void StopRecording()
        {
            MP.StopWriting();
            Video.StopEncoding();
            while (MP.IsFileWriting) { };
            MP.StopRecording();
            MP.Disconnect();
            SyncFile.Close();
            Thread.Sleep(1000);
            MP.Connect();
            MP.StartRecording();
        }

        private void RecordingButton_Click(object sender, EventArgs e)
        {
               
            if (MP.isconnected && Video.CapSDKStatus)
            {
                if (!MP.IsFileWriting)
                {            
                    IDT_VIDEOSTATUS.Text = Video.GetResText();                      
                    //Visual Stuff, so we know we are recording. 
                    RecordingButton.Text = "Stop Recording";
                    IDT_BIOPACRECORDINGSTAT.Text = "Recording";
                    IDM_SELECTCHANNELS.Enabled = false;
                    IDM_SETTINGS.Enabled = false;
                    IDM_DISCONNECTBIOPAC.Enabled = false;
                    RecordingButton.BackColor = Color.Red;
                    //Start the actual recording                    
                    StartRecording();                                                                   
                }
                else
                {                                        
                    //End Recording
                    StopRecording();


                    IDT_BIOPACRECORDINGSTAT.Text = "Not Recording";                                           
                    IDM_SELECTCHANNELS.Enabled = true;
                    IDM_SETTINGS.Enabled = true;
                    IDM_DISCONNECTBIOPAC.Enabled = true;
                    IDT_ENCODERSTATUS.Text = Video.EncoderStatus();
                    IDT_FEEDST.Text = Video.EncoderResult();
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
                TimerThread.Abort();
                ThreadDisplay.Abort();      
                if (MP.isstreaming)
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
            ThreadDisplay.Abort();
            this.Dispose(true);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {                   
            this.Dispose(true);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordSettings frm = new RecordSettings();
            frm.ShowDialog(this);         
            frm.Dispose();
            UpdateINI(BioIni);
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }
        private void setFeedingProtocolToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            FeederForm frm = new FeederForm(Feeder.Rats, Feeder);
            frm.ShowDialog(this);
            Feeder.Rats = frm.ReturnRats();
            Feeder = frm.ReturnFeeder();
            int M = 0;
            if (!((Feeder.Meal1.Hours == 0) && (Feeder.Meal1.Minutes == 0))) 
                M++;
            if (!((Feeder.Meal2.Hours == 0) && (Feeder.Meal2.Minutes == 0)))
                M++;
            if (!((Feeder.Meal3.Hours == 0) && (Feeder.Meal3.Minutes == 0)))
                M++;
            if (!((Feeder.Meal4.Hours == 0) && (Feeder.Meal4.Minutes == 0)))
                M++;
            if (!((Feeder.Meal5.Hours == 0) && (Feeder.Meal5.Minutes == 0)))
                M++;
            if (!((Feeder.Meal6.Hours == 0) && (Feeder.Meal6.Minutes == 0)))
                M++;
            Feeder.DailyMealCount = M;
            frm.Dispose();
            UpdateINI(BioIni);           
        }

    
        private void testFeedersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeederTester frm = new FeederTester();
            frm.ShowDialog(this);
            frm.Kill();
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
            SensorControl frm = new SensorControl();
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
                Console.WriteLine(Video.GetResText());
                if (Video.Res == (AdvantechCodes.tagRes.SUCCEEDED))
                {
                    Video.CapSDKStatus = true;
                }                         
            }
            
            
        }

        private void bioPacEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bioPacEnabledToolStripMenuItem.Checked = !bioPacEnabledToolStripMenuItem.Checked;
            MP.Enabled = bioPacEnabledToolStripMenuItem.Checked;
            if (MP.Enabled)
            {
                if (MP.IsFileWriting)
                    MP.StopWriting();
                if (MP.isconnected)
                    MP.Disconnect();
            }
        }

        private void videoCaptureEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (Video.Enabled)
            {
                Video.StopEncoding();
                Video.StopRecording();
            }
            videoCaptureEnabledToolStripMenuItem.Checked = !videoCaptureEnabledToolStripMenuItem.Checked;
            Video.Enabled = videoCaptureEnabledToolStripMenuItem.Checked;
        }

        private void cameraAssociationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraAssc C = new CameraAssc();
            C.ShowDialog(this);
            UpdateINI(BioIni);
            Video.UpdateCameraAssoc();
            C.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void VoltScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            MP.Voltage = VoltageSettings[VoltScale.SelectedIndex];
        }

        private void TimeScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            MP.DisplayLength = DisplayLengthSize[TimeScale.SelectedIndex];
            MP.ResetDisplaySize();
        }

        private void StatusBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ZoomWindow X = new ZoomWindow();
            //X.ShowDialog(this);
        }
    }
}