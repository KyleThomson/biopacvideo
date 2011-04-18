using System.Drawing.Drawing2D;
using System.Drawing;

namespace BioPacVideo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing)
        {
     
            base.Dispose(disposing);
        } */

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecordingButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeVideoCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeBioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DISCONNECTBIOPAC = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SETTINGS = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SELECTCHANNELS = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticFeederToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFeedersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFeedingProtocolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_BIOPACSTAT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_BIOPACRECORDINGSTAT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_MPLASTMESSAGE = new System.Windows.Forms.ToolStripStatusLabel();
            this.TickCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_VIDEOSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_VIDEORESULT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_DEVICECOUNT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_ENCODERSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_ENCODERRESULT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDC_RATSELECT = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bioPacEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.videoCaptureEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordingButton
            // 
            this.RecordingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordingButton.Location = new System.Drawing.Point(12, 37);
            this.RecordingButton.Name = "RecordingButton";
            this.RecordingButton.Size = new System.Drawing.Size(240, 46);
            this.RecordingButton.TabIndex = 0;
            this.RecordingButton.Text = "Start Recording ";
            this.RecordingButton.UseVisualStyleBackColor = true;
            this.RecordingButton.Click += new System.EventHandler(this.RecordingButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.bioPacToolStripMenuItem,
            this.automaticFeederToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDirectoryToolStripMenuItem,
            this.openSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectDirectoryToolStripMenuItem
            // 
            this.selectDirectoryToolStripMenuItem.Name = "selectDirectoryToolStripMenuItem";
            this.selectDirectoryToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.selectDirectoryToolStripMenuItem.Text = "Select &Recording Directory";
            this.selectDirectoryToolStripMenuItem.Click += new System.EventHandler(this.selectDirectoryToolStripMenuItem_Click);
            // 
            // openSettingsToolStripMenuItem
            // 
            this.openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
            this.openSettingsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openSettingsToolStripMenuItem.Text = "&Open Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initializeVideoCardToolStripMenuItem,
            this.videoSettingsToolStripMenuItem,
            this.sensorControlToolStripMenuItem,
            this.toolStripSeparator3,
            this.videoCaptureEnabledToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // initializeVideoCardToolStripMenuItem
            // 
            this.initializeVideoCardToolStripMenuItem.Name = "initializeVideoCardToolStripMenuItem";
            this.initializeVideoCardToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.initializeVideoCardToolStripMenuItem.Text = "Initialize Video Card";
            this.initializeVideoCardToolStripMenuItem.Click += new System.EventHandler(this.initializeVideoCardToolStripMenuItem_Click);
            // 
            // videoSettingsToolStripMenuItem
            // 
            this.videoSettingsToolStripMenuItem.Name = "videoSettingsToolStripMenuItem";
            this.videoSettingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.videoSettingsToolStripMenuItem.Size = new System.Drawing.Size(223, 20);
            this.videoSettingsToolStripMenuItem.Text = "Video Settings";
            this.videoSettingsToolStripMenuItem.Click += new System.EventHandler(this.videoSettingsToolStripMenuItem_Click);
            // 
            // sensorControlToolStripMenuItem
            // 
            this.sensorControlToolStripMenuItem.Name = "sensorControlToolStripMenuItem";
            this.sensorControlToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.sensorControlToolStripMenuItem.Text = "Sensor Control";
            this.sensorControlToolStripMenuItem.Click += new System.EventHandler(this.sensorControlToolStripMenuItem_Click);
            // 
            // bioPacToolStripMenuItem
            // 
            this.bioPacToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initializeBioPacToolStripMenuItem,
            this.IDM_DISCONNECTBIOPAC,
            this.IDM_SETTINGS,
            this.IDM_SELECTCHANNELS,
            this.toolStripSeparator2,
            this.bioPacEnabledToolStripMenuItem});
            this.bioPacToolStripMenuItem.Name = "bioPacToolStripMenuItem";
            this.bioPacToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.bioPacToolStripMenuItem.Text = "BioPac";
            // 
            // initializeBioPacToolStripMenuItem
            // 
            this.initializeBioPacToolStripMenuItem.Name = "initializeBioPacToolStripMenuItem";
            this.initializeBioPacToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.initializeBioPacToolStripMenuItem.Text = "Connect BioPac";
            this.initializeBioPacToolStripMenuItem.Click += new System.EventHandler(this.initializeBioPacToolStripMenuItem_Click);
            // 
            // IDM_DISCONNECTBIOPAC
            // 
            this.IDM_DISCONNECTBIOPAC.Name = "IDM_DISCONNECTBIOPAC";
            this.IDM_DISCONNECTBIOPAC.Size = new System.Drawing.Size(160, 22);
            this.IDM_DISCONNECTBIOPAC.Text = "Disconnect BioPac";
            this.IDM_DISCONNECTBIOPAC.Click += new System.EventHandler(this.disconnectBioPacToolStripMenuItem_Click);
            // 
            // IDM_SETTINGS
            // 
            this.IDM_SETTINGS.Name = "IDM_SETTINGS";
            this.IDM_SETTINGS.Size = new System.Drawing.Size(160, 22);
            this.IDM_SETTINGS.Text = "Settings";
            this.IDM_SETTINGS.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // IDM_SELECTCHANNELS
            // 
            this.IDM_SELECTCHANNELS.Name = "IDM_SELECTCHANNELS";
            this.IDM_SELECTCHANNELS.Size = new System.Drawing.Size(160, 22);
            this.IDM_SELECTCHANNELS.Text = "Select Channels";
            this.IDM_SELECTCHANNELS.Click += new System.EventHandler(this.selectChannelsToolStripMenuItem_Click);
            // 
            // automaticFeederToolStripMenuItem
            // 
            this.automaticFeederToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testFeedersToolStripMenuItem,
            this.setFeedingProtocolToolStripMenuItem});
            this.automaticFeederToolStripMenuItem.Name = "automaticFeederToolStripMenuItem";
            this.automaticFeederToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.automaticFeederToolStripMenuItem.Text = "Automatic Feeder";
            // 
            // testFeedersToolStripMenuItem
            // 
            this.testFeedersToolStripMenuItem.Name = "testFeedersToolStripMenuItem";
            this.testFeedersToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.testFeedersToolStripMenuItem.Text = "Test Feeders";
            this.testFeedersToolStripMenuItem.Click += new System.EventHandler(this.testFeedersToolStripMenuItem_Click);
            // 
            // setFeedingProtocolToolStripMenuItem
            // 
            this.setFeedingProtocolToolStripMenuItem.Name = "setFeedingProtocolToolStripMenuItem";
            this.setFeedingProtocolToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.setFeedingProtocolToolStripMenuItem.Text = "Set Feeding Protocol";
            this.setFeedingProtocolToolStripMenuItem.Click += new System.EventHandler(this.setFeedingProtocolToolStripMenuItem_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.IDT_BIOPACSTAT,
            this.IDT_BIOPACRECORDINGSTAT,
            this.IDT_MPLASTMESSAGE,
            this.TickCountLabel,
            this.IDT_VIDEOSTATUS,
            this.IDT_VIDEORESULT,
            this.IDT_DEVICECOUNT,
            this.IDT_ENCODERSTATUS,
            this.IDT_ENCODERRESULT});
            this.StatusBar.Location = new System.Drawing.Point(0, 630);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1284, 26);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 21);
            this.toolStripStatusLabel1.Text = "MP150 Status:";
            // 
            // IDT_BIOPACSTAT
            // 
            this.IDT_BIOPACSTAT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_BIOPACSTAT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_BIOPACSTAT.Font = new System.Drawing.Font("Tahoma", 10F);
            this.IDT_BIOPACSTAT.Name = "IDT_BIOPACSTAT";
            this.IDT_BIOPACSTAT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_BIOPACSTAT.Size = new System.Drawing.Size(179, 21);
            this.IDT_BIOPACSTAT.Text = "BioPac Disconnected";
            // 
            // IDT_BIOPACRECORDINGSTAT
            // 
            this.IDT_BIOPACRECORDINGSTAT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_BIOPACRECORDINGSTAT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_BIOPACRECORDINGSTAT.Font = new System.Drawing.Font("Tahoma", 10F);
            this.IDT_BIOPACRECORDINGSTAT.Name = "IDT_BIOPACRECORDINGSTAT";
            this.IDT_BIOPACRECORDINGSTAT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_BIOPACRECORDINGSTAT.Size = new System.Drawing.Size(140, 21);
            this.IDT_BIOPACRECORDINGSTAT.Text = "Not Recording";
            // 
            // IDT_MPLASTMESSAGE
            // 
            this.IDT_MPLASTMESSAGE.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_MPLASTMESSAGE.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_MPLASTMESSAGE.Name = "IDT_MPLASTMESSAGE";
            this.IDT_MPLASTMESSAGE.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_MPLASTMESSAGE.Size = new System.Drawing.Size(107, 21);
            this.IDT_MPLASTMESSAGE.Text = "NO STATUS";
            // 
            // TickCountLabel
            // 
            this.TickCountLabel.Name = "TickCountLabel";
            this.TickCountLabel.Size = new System.Drawing.Size(97, 21);
            this.TickCountLabel.Text = "Video Card Status:";
            // 
            // IDT_VIDEOSTATUS
            // 
            this.IDT_VIDEOSTATUS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_VIDEOSTATUS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_VIDEOSTATUS.Name = "IDT_VIDEOSTATUS";
            this.IDT_VIDEOSTATUS.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_VIDEOSTATUS.Size = new System.Drawing.Size(123, 21);
            this.IDT_VIDEOSTATUS.Text = "VIDEO STATUS";
            // 
            // IDT_VIDEORESULT
            // 
            this.IDT_VIDEORESULT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_VIDEORESULT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_VIDEORESULT.Name = "IDT_VIDEORESULT";
            this.IDT_VIDEORESULT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_VIDEORESULT.Size = new System.Drawing.Size(122, 21);
            this.IDT_VIDEORESULT.Text = "VIDEO RESULT";
            // 
            // IDT_DEVICECOUNT
            // 
            this.IDT_DEVICECOUNT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_DEVICECOUNT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_DEVICECOUNT.Name = "IDT_DEVICECOUNT";
            this.IDT_DEVICECOUNT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_DEVICECOUNT.Size = new System.Drawing.Size(105, 21);
            this.IDT_DEVICECOUNT.Text = "Devices (0)";
            // 
            // IDT_ENCODERSTATUS
            // 
            this.IDT_ENCODERSTATUS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_ENCODERSTATUS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_ENCODERSTATUS.Name = "IDT_ENCODERSTATUS";
            this.IDT_ENCODERSTATUS.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_ENCODERSTATUS.Size = new System.Drawing.Size(140, 21);
            this.IDT_ENCODERSTATUS.Text = "ENCODER STATUS";
            // 
            // IDT_ENCODERRESULT
            // 
            this.IDT_ENCODERRESULT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_ENCODERRESULT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_ENCODERRESULT.Name = "IDT_ENCODERRESULT";
            this.IDT_ENCODERRESULT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_ENCODERRESULT.Size = new System.Drawing.Size(139, 21);
            this.IDT_ENCODERRESULT.Text = "ENCODER RESULT";
            // 
            // IDC_RATSELECT
            // 
            this.IDC_RATSELECT.FormattingEnabled = true;
            this.IDC_RATSELECT.Location = new System.Drawing.Point(12, 112);
            this.IDC_RATSELECT.Name = "IDC_RATSELECT";
            this.IDC_RATSELECT.Size = new System.Drawing.Size(154, 21);
            this.IDC_RATSELECT.TabIndex = 4;
            this.IDC_RATSELECT.SelectedIndexChanged += new System.EventHandler(this.IDC_RATSELECT_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Animal";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // bioPacEnabledToolStripMenuItem
            // 
            this.bioPacEnabledToolStripMenuItem.Name = "bioPacEnabledToolStripMenuItem";
            this.bioPacEnabledToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.bioPacEnabledToolStripMenuItem.Text = "BioPac Enabled";
            this.bioPacEnabledToolStripMenuItem.Click += new System.EventHandler(this.bioPacEnabledToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
            // 
            // videoCaptureEnabledToolStripMenuItem
            // 
            this.videoCaptureEnabledToolStripMenuItem.Name = "videoCaptureEnabledToolStripMenuItem";
            this.videoCaptureEnabledToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.videoCaptureEnabledToolStripMenuItem.Text = "Video Capture Enabled";
            this.videoCaptureEnabledToolStripMenuItem.Click += new System.EventHandler(this.videoCaptureEnabledToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1284, 656);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDC_RATSELECT);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.RecordingButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "BioPac Video Recording";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RecordingButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bioPacToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticFeederToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testFeedersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initializeBioPacToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IDM_DISCONNECTBIOPAC;
        private System.Windows.Forms.ToolStripMenuItem IDM_SELECTCHANNELS;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel IDT_BIOPACSTAT;
        private System.Windows.Forms.ToolStripStatusLabel IDT_BIOPACRECORDINGSTAT;
        private System.Windows.Forms.ToolStripMenuItem IDM_SETTINGS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_MPLASTMESSAGE;
        private System.Windows.Forms.ToolStripStatusLabel TickCountLabel;
        private System.Windows.Forms.ToolStripMenuItem setFeedingProtocolToolStripMenuItem;
        private System.Windows.Forms.ComboBox IDC_RATSELECT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEOSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_DEVICECOUNT;
        private System.Windows.Forms.ToolStripMenuItem videoSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorControlToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCODERRESULT;
        private System.Windows.Forms.ToolStripMenuItem initializeVideoCardToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCODERSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEORESULT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem videoCaptureEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem bioPacEnabledToolStripMenuItem;
    }
}

