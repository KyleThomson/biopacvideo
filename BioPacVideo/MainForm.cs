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
        IniFile BioIni; //Main Ini File
        MPTemplate MP; 
        VideoTemplate Video;
        FeederTemplate Feeder;
        Pen BoxPen;
        FolderBrowserDialog FBD;        
        Bitmap Still;                
        Thread ThreadDisplay;
        private Thread TimerThread;
        Graphics g;
        bool RunDisplayThread;
        private DateTime Timing;

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
            IniFile WriteOnce;
            string DateString, RecordingDir;  
            //If 12AM, restart recording. 
            
            while (true)
            {
                if ((DateTime.Now.TimeOfDay.Hours == 0) & (DateTime.Now.TimeOfDay.Minutes == 0) & MP.IsFileWriting)
                {
                    MP.StopWriting();
                    Video.StopEncoding();
                    DateString = string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", DateTime.Now);
                    RecordingDir = MP.RecordingDirectory + "\\" + DateString;
                    MP.StopRecording();
                    MP.Disconnect();                    
                    Thread.Sleep(1000);
                    MP.Connect();
                    MP.StartRecording();
                    Directory.CreateDirectory(RecordingDir);
                    MP.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
                    //Write INI file once, so we save all the settings                    
                    WriteOnce = new IniFile(RecordingDir + "\\" + DateString + "_Settings.txt");
                    UpdateINI(WriteOnce);
                    //Video Stuff                    
                    Video.FileName = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
                    Feeder.SetLogName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString + "_Feeder.log");
                    Video.FileStart = 1;
                    Video.SetFileName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString, Video.FileStart);
                    MP.StartWriting();
                    Video.StartRecording();
                    Thread.Sleep(120000);
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Breakfast.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Breakfast.Minutes))
                {
                    Feeder.GoMeal();
                    Thread.Sleep(120000);
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Lunch.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Lunch.Minutes))
                {
                    Feeder.GoMeal();
                    Thread.Sleep(120000);
                    //Lunch
                }
                if ((DateTime.Now.TimeOfDay.Hours == Feeder.Dinner.Hours) & (DateTime.Now.TimeOfDay.Minutes == Feeder.Dinner.Minutes))
                {
                    Feeder.GoMeal();
                    Thread.Sleep(120000);
                    //YOU ARE A DINRAR
                }
                Thread.Sleep(10000);
                //If it is feed time,
            }
      }
       
        private void DisplayThread()
        {
            int Cm; //To hold current camera. 
            while (RunDisplayThread)
            {
                MP._DisplayHandle.WaitOne();
                IDT_MPLASTMESSAGE.Text = MPTemplate.MPRET[(int)MP.MPReturn];                
                if (Still != null)                    
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
                    g.DrawImage(Still, 132+(i%8)*162, 32+(float)Math.Floor((decimal)(i/8))*122, 160, 120);
                    Still.Dispose();
                }
            IDT_VIDEOSTATUS.Text = Video.GetResText();
            IDT_ENCSTAT.Text = Video.EncoderStatus();
            IDT_FEEDST.Text = Feeder.StateText;
            IDT_ENCODERSTATUS.Text = VideoWrapper.GetEncRes().ToString();            
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
            MP.Enabled = BioIni.IniReadValue("BioPac", "Enabled", true); BioIni.IniReadValue("Feeder", "Breakfast", out Feeder.Breakfast);
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
                Feeder.Rats[i].Weight = BioIni.IniReadValue("Rats", string.Format("Rat{0} (g)", i), (double)0);
                Feeder.Rats[i].Medication = BioIni.IniReadValue("Rats", string.Format("Rat{0}Medicate", i), 100);
                Feeder.Rats[i].Surgery = BioIni.IniReadValue("Rats", string.Format("Rat{0} Surgery", i));
                Feeder.Rats[i].Injection = BioIni.IniReadValue("Rats", string.Format("Rat{0} Injection", i));
                Feeder.Rats[i].FirstSeizure = BioIni.IniReadValue("Rats", string.Format("Rat{0} FirstSeizure", i));
            }
            Video.Enabled = BioIni.IniReadValue("Video", "Enabled", true);
            Video.XRes = BioIni.IniReadValue("Video", "XRes", 320);
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
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} (g)", i), Feeder.Rats[i].Weight.ToString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0}Medicate", i), Feeder.Rats[i].Medication);
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} Surgery", i), Feeder.Rats[i].Surgery.ToShortDateString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} Injection", i), Feeder.Rats[i].Injection.ToShortDateString());
                BioIni.IniWriteValue("Rats", string.Format("Rat{0} FirstSeizure", i), Feeder.Rats[i].FirstSeizure.ToShortDateString());
            }
            BioIni.IniWriteValue("Video", "Enabled", Video.Enabled);
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

        private void RecordingButton_Click(object sender, EventArgs e)
        {
            IniFile WriteOnce;
            string DateString, RecordingDir;         
            if (MP.isconnected && Video.CapSDKStatus)
            {
                if (!MP.IsFileWriting)
                {
                    //Start Recording   
                    if (!Video.EncoderStarted)
                        Video.initEncoder();                                    
                    //Set up recording name based on date and time
                    DateString = string.Format("{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}", DateTime.Now);
                    RecordingDir = MP.RecordingDirectory + "\\" + DateString;
                    Directory.CreateDirectory(RecordingDir);
                    
                    MP.Filename = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
                    Feeder.SetLogName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString + "_Feeder.log");
                    //Write INI file once, so we save all the settings                    
                    WriteOnce = new IniFile(RecordingDir + "\\" + DateString + "_Settings.txt");
                    UpdateINI(WriteOnce);

                    //Video Stuff                    
                    Video.FileName = MP.RecordingDirectory + "\\" + DateString + "\\" + DateString;
                    Video.FileStart = Video.FileStart+1;
                    Video.SetFileName(MP.RecordingDirectory + "\\" + DateString + "\\" + DateString, Video.FileStart);
                  //  Video.LoadSettings();                                                            
                    IDT_VIDEOSTATUS.Text = Video.GetResText();  

                    
                    //Visual Stuff, so we know we are recording. 
                    RecordingButton.Text = "Stop Recording";
                    IDT_BIOPACRECORDINGSTAT.Text = "Recording";
                    IDM_SELECTCHANNELS.Enabled = false;
                    IDM_SETTINGS.Enabled = false;
                    IDM_DISCONNECTBIOPAC.Enabled = false;
                    RecordingButton.BackColor = Color.Red;

                    //Start the actual recording
                    Video.StartRecording();
                    Timing = DateTime.Now;
                    Console.WriteLine(Timing.ToString());
                    MP.isstreaming = MP.StartWriting();                    
                }
                else
                {                                        
                    //End Recording
                    IDT_BIOPACRECORDINGSTAT.Text = "Not Recording";                    
                    MP.StopWriting();                    
                    Video.StopEncoding();
                    Console.WriteLine((DateTime.Now-Timing).ToString());
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
                RunDisplayThread = false;                
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
    }
}
