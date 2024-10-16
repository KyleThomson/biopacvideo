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
using System.Reflection;
using Ini;
using MPCLASS = Biopac.API.MPDevice.MPDevImports;
using MPCODE = Biopac.API.MPDevice.MPDevImports.MPRETURNCODE;
using Microsoft.VisualBasic;
using System.Collections;
using System.Security.AccessControl;

namespace BioPacVideo
{
    public partial class MainForm : Form
    {
        #region Properties
        IniFile BioIni; //Main Ini File
        MPTemplate MP;
        VideoWrapper Video; 
        FeederTemplate Feeder;
        string SyncName;
        Pen BoxPen;
        FolderBrowserDialog FBD;        
        Bitmap Still;                
        Thread ThreadDisplay;
        private Thread ThreadClock; 
        private Thread TimerThread;
        Graphics g;
        DriveInfo DI;
        bool RunDisplayThread;
        bool RunClockThread; 
        private ArrayList Panels; 
        public static int[] VoltageSettings = new int[] { 1, 10, 50, 100, 250, 500, 1000, 2000, 3000, 4000, 5000};
        public static int[] DisplayLengthSize = new int[] { 1, 5, 10, 30, 60 };
        private static ManualResetEvent mre = new ManualResetEvent(false);
        public string UnitLabel;
        #endregion

        #region Lifecycle
        public MainForm() //Form Constructior
        {
            //********** INIT VARIABLES ****************
            InitializeComponent(); //Default code
            Video = VideoWrapper.Instance; //Same for Video                        
            MP = MPTemplate.Instance; //Pull Instance from MP Template - So we only have a single instance in all code            
            Feeder = FeederTemplate.Instance; //Same for Feeders 
            BoxPen = new Pen(Brushes.Black, 4);
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string iniFilePath = Path.Combine(appDataFolder, "BioPacVideo", "BioPacVideo.ini");
            Directory.CreateDirectory(Path.GetDirectoryName(iniFilePath));
            BioIni = new IniFile(iniFilePath); //Standard Ini Settings
            
            Video.PanelHandles = new Int32[16];
            Video.PanelHandles[0] = CloneChannelPanel1.Handle.ToInt32();
            Video.PanelHandles[1] = CloneChannelPanel2.Handle.ToInt32();
            Video.PanelHandles[2] = CloneChannelPanel3.Handle.ToInt32();
            Video.PanelHandles[3] = CloneChannelPanel4.Handle.ToInt32();
            Video.PanelHandles[4] = CloneChannelPanel5.Handle.ToInt32();
            Video.PanelHandles[5] = CloneChannelPanel6.Handle.ToInt32();
            Video.PanelHandles[6] = CloneChannelPanel7.Handle.ToInt32();
            Video.PanelHandles[7] = CloneChannelPanel8.Handle.ToInt32();
            Video.PanelHandles[8] = CloneChannelPanel9.Handle.ToInt32();
            Video.PanelHandles[9] = CloneChannelPanel10.Handle.ToInt32();
            Video.PanelHandles[10] = CloneChannelPanel11.Handle.ToInt32();
            Video.PanelHandles[11] = CloneChannelPanel12.Handle.ToInt32();
            Video.PanelHandles[12] = CloneChannelPanel13.Handle.ToInt32();
            Video.PanelHandles[13] = CloneChannelPanel14.Handle.ToInt32();
            Video.PanelHandles[14] = CloneChannelPanel15.Handle.ToInt32();
           Video.PanelHandles[15] = CloneChannelPanel16.Handle.ToInt32();
            Panels = new ArrayList();
            Panels.Add(CloneChannelPanel1);
            Panels.Add(CloneChannelPanel2);
            Panels.Add(CloneChannelPanel3);
            Panels.Add(CloneChannelPanel4);
            Panels.Add(CloneChannelPanel5);
            Panels.Add(CloneChannelPanel6);
            Panels.Add(CloneChannelPanel7);
            Panels.Add(CloneChannelPanel8);
            Panels.Add(CloneChannelPanel9);
            Panels.Add(CloneChannelPanel10);
            Panels.Add(CloneChannelPanel11);
            Panels.Add(CloneChannelPanel12);
            Panels.Add(CloneChannelPanel13);
            Panels.Add(CloneChannelPanel14);
            Panels.Add(CloneChannelPanel15);
            Panels.Add(CloneChannelPanel16);
            g = this.CreateGraphics();  //Plot window          
            UnitLabel = Environment.MachineName.ToString(); // can't be more than 15 characters because microsoft is a dumb company 
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
                    MP.CommunicateBioPac();
                    IDT_BIOPACSTAT.Text = "BioPac Connected";
                }
                IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
            }
            Update_FreeSpace();
            UnitNameStrip.Text = UnitLabel; 
            bioPacEnabledToolStripMenuItem.Checked = MP.Enabled;
            //Still = new Bitmap("NoSignal.Bmp");
            MP.FileCount = 0;
            RecordingButton.BackColor = Color.Green;
            UpdateGUI(); 
            ThreadDisplay = new Thread(new ThreadStart(DisplayThread));
            TimerThread = new Thread(new ThreadStart(TimerCheckThread));
            ThreadClock = new Thread(new ThreadStart(ClockThread)); 
            RunDisplayThread = true;
            RunClockThread = true; 
            VoltScale.SelectedIndex = Array.IndexOf(VoltageSettings, MP.Voltage);
            TimeScale.SelectedIndex = Array.IndexOf(DisplayLengthSize, MP.DisplayLength); 
            ThreadDisplay.Start();
            ThreadClock.Start(); 
            TimerThread.Start();

           
        }

        ~MainForm()
        {
            ThreadDisplay.Abort();
            this.Dispose(true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MP.RecordingWanted && MessageBox.Show("Are you sure you want to close? The program is currently recording", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Establish new variable values
                MP.RecordingWanted = false;
                MP.Recording = false;

                //Actually Stop recording
                StopRecording();

                //Make Biopac Adjustments
                IDM_SELECTCHANNELS.Enabled = true;
                IDM_SETTINGS.Enabled = true;
                IDM_DISCONNECTBIOPAC.Enabled = true;

                //Make UI Adjustments
                RecordingButton.Text = "Start Recording";
                RecordingButton.BackColor = Color.Green;
            }
            else if (MP.RecordingWanted)
            {
                e.Cancel = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TimerThread.Abort();
                ThreadDisplay.Abort();
                ThreadClock.Abort();
                if (MP.isstreaming)
                {
                    MP.StopStreaming();
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
        #endregion

        #region Input Handlers
        #region File
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
            Update_FreeSpace();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            about.Location = new Point(this.Location.X + this.Size.Width/2 - about.Size.Width/2, this.Location.Y + this.Size.Height/2 - about.Size.Height/2);
        }
        #endregion

        #region BioPac
        private void connectBioPacToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordSettings frm = new RecordSettings();
            frm.ShowDialog(this);
            frm.Dispose();
            UpdateINI(BioIni); // this must be what is crashing the program 
            MessageBox.Show("Please restart the software \nbefore continuing.");
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
        }

        private void selectChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RecordSelect frm = new RecordSelect(MP.RecordAC, MP.RecordingDevice, MP.RecordingDeviceAll);
            frm.ShowDialog(this);
            MP.RecordAC = frm.AC();
            MP.RecordingDevice = frm.RC();
            MP.RecordingDeviceAll = frm.RD();
            frm.Dispose();
            UpdateINI(BioIni);
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
            UpdateGUI();
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
        #endregion

        #region Video
        private void cameraAssociationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraAssosciation frm = new CameraAssosciation();
            frm.ShowDialog(this);
            UpdateINI(BioIni);
            Video.UpdateCameraAssoc();
            MessageBox.Show("Please restart the software \nbefore continuing");
            frm.Dispose();

        }

        private void videoSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoSettings frm = new VideoSettings();
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK) UpdateINI(BioIni);
            frm.Dispose();
        }
        #endregion

        #region Animal Settings
        private void animalSettingsTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AnimalSettings frm = new AnimalSettings(Feeder, MP.Recording);
            frm.Height = 90;
            frm.Width = 300;
            frm.ShowDialog(this);
            UpdateINI(BioIni);
            frm.Dispose();
        }

        private void injectionManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InjectionManager Frm = new InjectionManager();
            Frm.ShowDialog(this);
            Frm.Dispose();
            UpdateINI(BioIni);
        }

        private void AddPelletCountMenuItem_Click(object sender, EventArgs e)
        {
            PelletCounts Frm = new PelletCounts();
            Frm.ShowDialog(this);
            Frm.Dispose();
        }
        #endregion

        #region Feeders
        #region Feeder Test
        private void ratRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeederTester frm = new FeederTester();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void mouseRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeederTester_Mouse frm = new FeederTester_Mouse();
            frm.ShowDialog(this);
            frm.Dispose();
        }
        #endregion

        private void feederAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeederAddress Frm = new FeederAddress(this.Feeder);
            Frm.ShowDialog(this);
            Frm.Dispose();
            UpdateINI(BioIni);
        }

        private void feederStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MP.ShowFeederStatus();
        }

        private void feederSettingsMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MealTimes frm = new MealTimes(Feeder);
            frm.ShowDialog(this);
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
            frm.Dispose();
        }
        #endregion

        private void RecordingButton_Click(object sender, EventArgs e)
        {

            if (MP.isconnected)
            {
                if (!MP.IsFileWriting)
                {
                    //IDT_VIDEOSTATUS.Text = Video.GetResText();                      
                    //Visual Stuff, so we know we are recording. 
                    if (MP.RecordingDirectory == "C:\\")
                    {
                        MessageBox.Show("Please Select a Recording Directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        RecordingButton.Text = "Stop Recording";
                        IDM_SELECTCHANNELS.Enabled = false;
                        IDM_SETTINGS.Enabled = false;
                        IDM_DISCONNECTBIOPAC.Enabled = false;
                        RecordingButton.BackColor = Color.Red;
                        RecordingButton.TabStop = false;
                        VoltScale.Focus();
                        //Start the actual recording
                        MP.RecordingWanted = true; //open the eyes to observe data transfer between Biopac and software
                                                   //Console.WriteLine("RecordingWanted = " + MP.RecordingWanted);
                        StartRecording();
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Are you sure you wish to stop recording?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        //End Recording
                        MP.RecordingWanted = false; // close the eyes and no longer observe data transfer between Biopac and software
                        MP.Recording = false;
                        //Console.WriteLine("RecordingWanted = " + MP.RecordingWanted);
                        StopRecording();
                        IDM_SELECTCHANNELS.Enabled = true;
                        IDM_SETTINGS.Enabled = true;
                        IDM_DISCONNECTBIOPAC.Enabled = true;
                        //IDT_ENCODERSTATUS.Text = Video.EncoderStatus();
                        // IDT_FEEDST.Text = Video.EncoderResult();
                        RecordingButton.Text = "Start Recording";
                        RecordingButton.BackColor = Color.Green;
                        RecordingButton.TabStop = true;
                        RecordingButton.Focus();
                    }
                    else
                    {
                        return;
                    }

                }
            }
            else
            {
                if (!MP.isconnected)
                {
                    MessageBox.Show("Please Connect BioPac " + MP.MPtype, "BioPac Not Connected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /* if (!Video.CapSDKStatus)
                 {
                     MessageBox.Show("Please Initialize Video Card.", "Video Card Not Initialized.",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }*/
            }
            IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];
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
        #endregion

        #region INI Functions
        /// <summary>
        /// Reads All INI Values from INI File on disk and populates them to the Program INI Variable
        /// </summary>
        /// <param name="BioIni">The INI File to populate the values of</param>
        private void ReadINI(IniFile BioIni)
        {
            MP.RecordingDirectory = BioIni.IniReadValue("General", "RecDirectory", Directory.GetCurrentDirectory());
            MP.MPtype = BioIni.IniReadValue("BioPac", "MPType", "MP160");
            MP.SampleRate = BioIni.IniReadValue("BioPac", "SampleRate", 500);
            MP.SelectedChannel = BioIni.IniReadValue("BioPac", "Selected Channel", 1);
            MP.DisplayLength = BioIni.IniReadValue("BioPac", "DisplayLength", 10);
            MP.Voltage = BioIni.IniReadValue("BioPac", "Voltage(mV)", 500);
            MP.Gain = BioIni.IniReadValue("BioPac", "Gain", 20000);
            MP.Enabled = BioIni.IniReadValue("BioPac", "Enabled", true);
            MP.Offset = BioIni.IniReadValue("BioPac", "Offset", 2.0);
            MP.FileSplit = BioIni.IniReadValue("BioPac", "FileSplit", true);
            BioIni.IniReadValue("Feeder", "Meal1", out Feeder.Meal1);
            BioIni.IniReadValue("Feeder", "Meal2", out Feeder.Meal2);
            BioIni.IniReadValue("Feeder", "Meal3", out Feeder.Meal3);
            BioIni.IniReadValue("Feeder", "Meal4", out Feeder.Meal4);
            BioIni.IniReadValue("Feeder", "Meal5", out Feeder.Meal5);
            BioIni.IniReadValue("Feeder", "Meal6", out Feeder.Meal6);
            Feeder.PelletsPerGram = BioIni.IniReadValue("Feeder", "PelletsPerGram", 0.02);
            Feeder.Enabled = BioIni.IniReadValue("Feeder", "Enabled", false);
            Feeder.DailyMealCount = BioIni.IniReadValue("Feeder", "DailyMealCount", 4);
            Feeder.ADDC1 = BioIni.IniReadValue("Feeder", "ADDCompound1", "");
            Feeder.Dose1 = BioIni.IniReadValue("Feeder", "ADDDose1", "");
            Feeder.Route1 = BioIni.IniReadValue("Feeder", "ADDRoute1", 1);
            Feeder.Solve1 = BioIni.IniReadValue("Feeder", "ADDSolve1", 1);
            Feeder.ADDC2 = BioIni.IniReadValue("Feeder", "ADDCompound2", "");
            Feeder.Dose2 = BioIni.IniReadValue("Feeder", "ADDDose2", "");
            Feeder.Route2 = BioIni.IniReadValue("Feeder", "ADDRoute2", 1);
            Feeder.Solve2 = BioIni.IniReadValue("Feeder", "ADDSolve2", 1);
            Feeder.AlternateAddress = BioIni.IniReadValue("Feeder", "AltEnabled", false);
            for (int i = 0; i < 32; i++)
            {
                Feeder.AddressTable[i] = BioIni.IniReadValue("Feeder", "Address" + i, i);
            }
            for (int i = 0; i < 16; i++)
            {
                MP.RecordAC[i] = BioIni.IniReadValue("BioPac", string.Format("Channel{0}", i), true);
            }
            for (int i = 0; i < 16; i++)
            {
                if (BioIni.IniReadValue("BioPac", string.Format("RecordingDevice_Channel{0}", i), false) == true)
                {
                    MP.RecordingDevice = Enumerable.Repeat(true, 16).ToArray();
                    break;
                }
                MP.RecordingDevice[i] = false;
            }
            MP.RecordingDeviceAll = BioIni.IniReadValue("Biopac", "RecordingDeviceAll", 2);
            MP.UpdateOffsets();
            for (int i = 0; i < 16; i++)
            {
                Feeder.Rats[i].ID = BioIni.IniReadValue("Rats", string.Format("Rat{0} ID", i), string.Format("Rat{0}", i));
                Feeder.Rats[i].Weight = BioIni.IniReadValue("Rats", string.Format("Rat{0} (g)", i), (double)0);
                Feeder.Rats[i].Medication = BioIni.IniReadValue("Rats", string.Format("Rat{0}Medicate", i), 100);
                Feeder.Rats[i].Surgery = BioIni.IniReadValue("Rats", string.Format("Rat{0} Surgery", i));
                Feeder.Rats[i].Injection = BioIni.IniReadValue("Rats", string.Format("Rat{0} Injection", i));
                Feeder.Rats[i].FirstSeizure = BioIni.IniReadValue("Rats", string.Format("Rat{0} FirstSeizure", i));
                Feeder.Rats[i].DoseID = BioIni.IniReadValue("Rats", string.Format("Rat{0} DoseID", i), 1);
                for (int j = 0; j < (7 * 6); j++)
                {
                    Feeder.Rats[i].Meals[j] = BioIni.IniReadValue("Rats", "Rat" + i + "Meal" + j, false);
                }
            }
            Feeder.Cages_X = BioIni.IniReadValue("Rats", "CagesX", 4);
            Feeder.Cages_Y = BioIni.IniReadValue("Rats", "CagesY", 4);

            Video.Enabled = BioIni.IniReadValue("Video", "Enabled", true);
            Video.XRes = BioIni.IniReadValue("Video", "XRes", 320);
            Video.LengthWise = BioIni.IniReadValue("Video", "LengthWise", 8);
            Video.YRes = BioIni.IniReadValue("Video", "YRes", 240);
            Video.Quality = BioIni.IniReadValue("Video", "Quality", 4000);
            Video.Bitrate = BioIni.IniReadValue("Video", "Bitrate", 2);
            Video.KeyFrames = BioIni.IniReadValue("Video", "KeyFrames", 100);
            for (int i = 0; i < 16; i++)
                Video.CameraAssociation[i] = BioIni.IniReadValue("Video", string.Format("Channel{0}", i), i);
            for (int i = 0; i < 32; i++)
            {
                Video.Brightness[i] = BioIni.IniReadValue("Video", string.Format("Bright{0}", i), 50);
                Video.Contrast[i] = BioIni.IniReadValue("Video", string.Format("Contrast{0}", i), 50);
                Video.Hue[i] = BioIni.IniReadValue("Video", string.Format("Hue{0}", i), 50);
                Video.Saturation[i] = BioIni.IniReadValue("Video", string.Format("Satur{0}", i), 50);
            }

        }

        /// <summary>
        /// Updates the values of the INI File both on the disk and for the variable passed in
        /// </summary>
        /// <param name="BioIni">The INI File to update the values of</param>
        private void UpdateINI(IniFile BioIni)
        {
            BioIni.IniWriteValue("General", "RecDirectory", MP.RecordingDirectory);
            BioIni.IniWriteValue("General", "Unit Name", UnitLabel);
            BioIni.IniWriteValue("General", "Version", "2.2.1");
            //videofix qualifier added
            BioIni.IniWriteValue("BioPac", "VideoFixDisabled", true);
            BioIni.IniWriteValue("BioPac", "MPType", MP.MPtype);
            BioIni.IniWriteValue("BioPac", "SelectedChannel", MP.SelectedChannel);
            BioIni.IniWriteValue("BioPac", "SampleRate", MP.SampleRate.ToString());
            BioIni.IniWriteValue("BioPac", "DisplayLength", MP.DisplayLength.ToString());
            BioIni.IniWriteValue("BioPac", "Voltage(mV)", MP.Voltage.ToString());
            BioIni.IniWriteValue("BioPac", "Gain", MP.Gain.ToString());
            BioIni.IniWriteValue("BioPac", "Enabled", MP.Enabled);
            BioIni.IniWriteValue("BioPac", "Offset", MP.Offset.ToString());
            BioIni.IniWriteValue("BioPac", "FileSplit", MP.FileSplit);
            BioIni.IniWriteValue("Feeder", "Meal1", Feeder.Meal1.ToString());
            BioIni.IniWriteValue("Feeder", "Meal2", Feeder.Meal2.ToString());
            BioIni.IniWriteValue("Feeder", "Meal3", Feeder.Meal3.ToString());
            BioIni.IniWriteValue("Feeder", "Meal4", Feeder.Meal4.ToString());
            BioIni.IniWriteValue("Feeder", "Meal5", Feeder.Meal5.ToString());
            BioIni.IniWriteValue("Feeder", "Meal6", Feeder.Meal6.ToString());
            BioIni.IniWriteValue("Feeder", "PelletsPerGram", Feeder.PelletsPerGram.ToString());
            BioIni.IniWriteValue("Feeder", "Enabled", Feeder.Enabled);
            BioIni.IniWriteValue("Feeder", "DailyMealCount", Feeder.DailyMealCount);
            BioIni.IniWriteValue("Feeder", "ADDCompound1", Feeder.ADDC1);
            BioIni.IniWriteValue("Feeder", "ADDDose1", Feeder.Dose1);
            BioIni.IniWriteValue("Feeder", "ADDRoute1", Feeder.Route1);
            BioIni.IniWriteValue("Feeder", "ADDSolve1", Feeder.Solve1);
            BioIni.IniWriteValue("Feeder", "ADDCompound2", Feeder.ADDC2);
            BioIni.IniWriteValue("Feeder", "ADDDose2", Feeder.Dose2);
            BioIni.IniWriteValue("Feeder", "ADDRoute2", Feeder.Route2);
            BioIni.IniWriteValue("Feeder", "ADDSolve2", Feeder.Solve2);
            BioIni.IniWriteValue("Feeder", "AltEnabled", Feeder.AlternateAddress);
            for (int i = 0; i < 32; i++)
            {
                BioIni.IniWriteValue("Feeder", "Address" + i, Feeder.AddressTable[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                BioIni.IniWriteValue("BioPac", string.Format("RecordingDevice_Channel{0}", i), MP.RecordingDevice[i]);
            }
            BioIni.IniWriteValue("BioPac", "RecordingDeviceAll", MP.RecordingDeviceAll);
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
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} DoseID", i), Feeder.Rats[i].DoseID.ToString());
                for (int j = 0; j < (7 * 6); j++)
                {
                    BioIni.IniWriteValue("Rats", "Rat" + i + "Meal" + j, Feeder.Rats[i].Meals[j]);
                }
            }
            BioIni.IniWriteValue("Rats", "CagesX", Feeder.Cages_X);
            BioIni.IniWriteValue("Rats", "CagesY", Feeder.Cages_Y);
            BioIni.IniWriteValue("Video", "Enabled", Video.Enabled);
            BioIni.IniWriteValue("Video", "LengthWise", Video.LengthWise);
            BioIni.IniWriteValue("Video", "XRes", Video.XRes);
            BioIni.IniWriteValue("Video", "YRes", Video.YRes);
            BioIni.IniWriteValue("Video", "Quality", Video.Quality);
            BioIni.IniWriteValue("Video", "Bitrate", Video.Bitrate);
            BioIni.IniWriteValue("Video", "KeyFrames", Video.KeyFrames);
            for (int i = 0; i < 16; i++)
                BioIni.IniWriteValue("Video", string.Format("Channel{0}", i), Video.CameraAssociation[i]);
            for (int i = 0; i < 32; i++)
            {
                BioIni.IniWriteValue("Video", string.Format("Bright{0}", i), Video.Brightness[i]);
                BioIni.IniWriteValue("Video", string.Format("Contrast{0}", i), Video.Contrast[i]);
                BioIni.IniWriteValue("Video", string.Format("Hue{0}", i), Video.Hue[i]);
                BioIni.IniWriteValue("Video", string.Format("Satur{0}", i), Video.Saturation[i]);
            }
        }
        #endregion

        #region Thread Functions
        /// <summary>
        /// A permanent loop that checks recording status, feeder meals, and recording filesplit 
        /// </summary>
        private void TimerCheckThread()
        {
            while (true)
            {
                if (MP.RecordingWanted && !MP.RecordingSuccess) // we want to be recording but the recording was unsuccessful
                {
                    if (MP.VideoOff)
                    {
                        Video.StopRecording();
                        this.RecordingButton.Invoke((MethodInvoker)delegate
                        {
                            RecordingButton.Enabled = false; RecordingButton.BackColor = Color.Gray; RecordingButton.Text = "Recording Error";
                        });
                        MP.VideoOff = false;
                    }
                    if (MP.Connect())
                    {
                        MP.RecordingSuccess = true;
                        if (MP.CommunicateBioPac())
                        {
                            StartRecording();
                            MP.VideoOff = false;
                            this.RecordingButton.Invoke((MethodInvoker)delegate
                            {
                                RecordingButton.Enabled = true; RecordingButton.BackColor = Color.Red; RecordingButton.Text = "Stop Recording";
                            });
                        }
                    }
                }
                //If 12AM, restart recording. 
                if ((((DateTime.Now.TimeOfDay.Hours == 0) & (DateTime.Now.TimeOfDay.Minutes == 0)))  || (((MP.FileSplit) & (DateTime.Now.TimeOfDay.Hours == 12) & (DateTime.Now.TimeOfDay.Minutes == 0))))
                {
                    if (MP.IsFileWriting)
                    {
                        StopRecording();
                        StartRecording();
                    }
                    else
                    {
                        MP.StopStreaming();
                        MP.Disconnect();                        
                        Thread.Sleep(1000);
                        MP.Connect();
                        MP.CommunicateBioPac(); 
                    }
                    //Update Hard Drive
                    Update_FreeSpace();
                    Thread.Sleep(120000); //Always Skip the Meal;
                }                
                else if (Feeder.Enabled)
                {
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal1.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal1.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount);
                        Thread.Sleep(120000);
                        //Breakfast
                    }
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal2.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal2.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 1);
                        Thread.Sleep(120000);
                        //Brunch
                    }
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal3.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal3.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 2);
                        Thread.Sleep(120000);
                        //Lunch
                    }
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal4.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal4.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 3);
                        Thread.Sleep(120000);
                        //Hobbits 2nd Lunch
                    }
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal5.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal5.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 4);
                        Thread.Sleep(120000);
                        //Dinner 
                    }
                    if ((DateTime.Now.TimeOfDay.Hours == Feeder.Meal6.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Meal6.Minutes))
                    {
                        Feeder.GoMeal(Feeder.GetDay() * Feeder.DailyMealCount + 5);
                        Thread.Sleep(120000);
                        //Midnight Snack
                        //PO - TA - TOES
                    }
                }
                Thread.Sleep(10000);
            }
      }
        
        /// <summary>
        /// A Loop dependent on the RunClockThread Variable that updates the time every second
        /// </summary>
        private void ClockThread()
        {
            while (RunClockThread)
            {
                TimeLabel.Text = DateAndTime.Now.ToString();
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// A function that loops based on the variable RunDisplayThread 
        /// that updates the Camera streams, EEG Data displays, Video Status text label, and Feeder State text label
        /// </summary>
        private void DisplayThread()
        {
            int Cm; //To hold current camera. 
            int InnerSync = 0;
            int ChanSync = 0;
            DI = new DriveInfo(MP.RecordingDirectory.Substring(0, 3));
            while (RunDisplayThread)
            {
                this.Invoke(new MethodInvoker(delegate { IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn]; }));
                //this.Invoke(new MethodInvoker(delegate { IDT_VIDEOSTATUS.Text = Video.GetResText(); }));
               // this.Invoke(new MethodInvoker(delegate { IDT_ENCSTAT.Text = Video.EncoderStatus(); }));
                this.Invoke(new MethodInvoker(delegate { IDT_FEEDST.Text = Feeder.StateText; }));
                //this.Invoke(new MethodInvoker(delegate { IDT_ENCODERSTATUS.Text = VideoWrapper.GetEncRes().ToString(); }));                       
                MP._DisplayHandle.WaitOne();
                //if (Still != null)                    
                    g.DrawImage(MP.offscreen, 30, 280);
                Cm = 0;
           } 
        }
        #endregion

        #region Recording Functions
        /// <summary>
        /// Creates a new Directory named YYYYMMDD-HHMMSS based on the current date, 
        /// then it establishes the same naming scheme or _Feeder.log, and _Settings.txt files. 
        /// the current MPTemplate is updated and ACQ, and Video recording are started to the current directory
        /// </summary>
        private void StartRecording()
        {
            IniFile WriteOnce;
            string DateString, RecordingDir;   
            //Start Recording   
            //if (!Video.EncoderStarted)
             //   Video.initEncoder();
            //Set up recording name based on date and time
            DateString = string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", DateTime.Now);
            RecordingDir = MP.RecordingDirectory + "\\" + DateString;
            Directory.CreateDirectory(RecordingDir);
            MP.Recording = true; 
            MP.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
            Feeder.SetLogName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString + "_Feeder.log");
            //Write INI file once, so we save all the settings                    
            WriteOnce = new IniFile(RecordingDir + "\\" + DateString + "_Settings.txt");
            UpdateINI(WriteOnce);
            //Video Stuff                          
            Video.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;            
            //Video.SetFileName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString, Video.FileStart);
            Video.StartRecording();
            MP.isstreaming = MP.StartWriting();
        }

        /// <summary>
        /// Stops recording, waits for the biopac to finish writing the ACQ file then reconnects to the biopac
        /// </summary>
        private void StopRecording()
        {
            MP.StopWriting();
            Video.StopRecording();
            while (MP.IsFileWriting) { };
            MP.StopStreaming();
            MP.Disconnect();
            Thread.Sleep(1000);
            MP.Connect();
            MP.CommunicateBioPac();
        }
        #endregion

        #region UI Functions
        /// <summary>
        /// Checks the available free space for the recording directory then updates the color and text of the UI to reflect the new free space
        /// </summary>
        private void Update_FreeSpace()
        {
            long DriveSpace;
            long FreeSpace;
            if (Directory.Exists(MP.RecordingDirectory))
            {
                DriveInfo Drive = new DriveInfo(Path.GetPathRoot(MP.RecordingDirectory));
                DriveSpace = Drive.TotalSize;
                FreeSpace = Drive.TotalFreeSpace;
            }
            else
            {
                MessageBox.Show("The Following Directory is Unavailable: " + Environment.NewLine + MP.RecordingDirectory.ToString() + Environment.NewLine + "Please Select New Directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MP.RecordingDirectory = String.Format("C:\\");
                DriveInfo Drive = new DriveInfo(Path.GetPathRoot(MP.RecordingDirectory));
                DriveSpace = Drive.TotalSize;
                FreeSpace = Drive.TotalFreeSpace;
            }
            double GBFree = (double)(FreeSpace / 1073741824);
            this.Invoke(new MethodInvoker(delegate { SpaceLeft.Text = GBFree.ToString() + "GB Free"; }));
            if (GBFree < 40)
                this.Invoke(new MethodInvoker(delegate { SpaceLeft.BackColor = Color.Red; }));
            else if (GBFree < 100)
                this.Invoke(new MethodInvoker(delegate { SpaceLeft.BackColor = Color.Yellow; }));
            else this.Invoke(new MethodInvoker(delegate { SpaceLeft.BackColor = Color.LightGreen; }));
        }

        /// <summary>
        /// Updates the Video on the camera placeholder elements
        /// </summary>
        public void UpdateGUI()
        {
            Panel TempPanel;
            Video.HwInitialize();
            Video.ShowCloneVideo(true);
            int halfChan = MP.TotChan() / 2;
            for (int i = 0; i < halfChan; i++)
            {
                TempPanel = Panels[i] as Panel;
                TempPanel.Location = new Point((i * 149) + 127, 37);
                TempPanel = Panels[i + halfChan] as Panel;
                TempPanel.Visible = true; 
                TempPanel.Location = new Point((i * 149) + 127, 158);
                TempPanel.Visible = true;
            }
            for (int i = MP.TotChan(); i < 16; i++)
            {
                TempPanel = Panels[i] as Panel;
                TempPanel.Visible = false;
            }
            Video.UpdateCameraAssoc();
        }
        #endregion

        private void advancedFeederControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Advanced = new AdvancedFeederControl();
            Advanced.Show();
        }
    }
}
