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
            this.cameraAssociationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.videoCaptureEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeBioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DISCONNECTBIOPAC = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SETTINGS = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SELECTCHANNELS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bioPacEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticFeederToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFeedingProtocolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFeedersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.IDT_BIOPACSTAT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_BIOPACRECORDINGSTAT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_MPLASTMESSAGE = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_VIDEOSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_VIDEORESULT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_DEVICECOUNT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_ENCODERSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_ENCSTAT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_FEEDST = new System.Windows.Forms.ToolStripStatusLabel();
            this.VoltScale = new System.Windows.Forms.ComboBox();
            this.TimeScale = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordingButton
            // 
            this.RecordingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordingButton.Location = new System.Drawing.Point(12, 37);
            this.RecordingButton.Name = "RecordingButton";
            this.RecordingButton.Size = new System.Drawing.Size(109, 59);
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
            this.menuStrip1.Size = new System.Drawing.Size(1286, 24);
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
            this.cameraAssociationsToolStripMenuItem,
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
            // cameraAssociationsToolStripMenuItem
            // 
            this.cameraAssociationsToolStripMenuItem.Name = "cameraAssociationsToolStripMenuItem";
            this.cameraAssociationsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.cameraAssociationsToolStripMenuItem.Text = "Camera Associations";
            this.cameraAssociationsToolStripMenuItem.Click += new System.EventHandler(this.cameraAssociationsToolStripMenuItem_Click);
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
            // automaticFeederToolStripMenuItem
            // 
            this.automaticFeederToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFeedingProtocolToolStripMenuItem,
            this.testFeedersToolStripMenuItem});
            this.automaticFeederToolStripMenuItem.Name = "automaticFeederToolStripMenuItem";
            this.automaticFeederToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.automaticFeederToolStripMenuItem.Text = "Animal Settings";
            // 
            // setFeedingProtocolToolStripMenuItem
            // 
            this.setFeedingProtocolToolStripMenuItem.Name = "setFeedingProtocolToolStripMenuItem";
            this.setFeedingProtocolToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.setFeedingProtocolToolStripMenuItem.Text = "Animal Settings";
            this.setFeedingProtocolToolStripMenuItem.Click += new System.EventHandler(this.setFeedingProtocolToolStripMenuItem_Click);
            // 
            // testFeedersToolStripMenuItem
            // 
            this.testFeedersToolStripMenuItem.Name = "testFeedersToolStripMenuItem";
            this.testFeedersToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.testFeedersToolStripMenuItem.Text = "Feeder Test";
            this.testFeedersToolStripMenuItem.Click += new System.EventHandler(this.testFeedersToolStripMenuItem_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDT_BIOPACSTAT,
            this.IDT_BIOPACRECORDINGSTAT,
            this.IDT_MPLASTMESSAGE,
            this.IDT_VIDEOSTATUS,
            this.IDT_VIDEORESULT,
            this.IDT_DEVICECOUNT,
            this.IDT_ENCODERSTATUS,
            this.IDT_ENCSTAT,
            this.IDT_FEEDST});
            this.StatusBar.Location = new System.Drawing.Point(0, 861);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1286, 26);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            this.StatusBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.StatusBar_ItemClicked);
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
            // IDT_ENCSTAT
            // 
            this.IDT_ENCSTAT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_ENCSTAT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_ENCSTAT.Name = "IDT_ENCSTAT";
            this.IDT_ENCSTAT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_ENCSTAT.Size = new System.Drawing.Size(139, 21);
            this.IDT_ENCSTAT.Text = "ENCODER RESULT";
            // 
            // IDT_FEEDST
            // 
            this.IDT_FEEDST.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_FEEDST.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_FEEDST.Name = "IDT_FEEDST";
            this.IDT_FEEDST.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_FEEDST.Size = new System.Drawing.Size(130, 21);
            this.IDT_FEEDST.Text = "FEEDER STATUS";
            // 
            // VoltScale
            // 
            this.VoltScale.FormattingEnabled = true;
            this.VoltScale.Location = new System.Drawing.Point(12, 115);
            this.VoltScale.Name = "VoltScale";
            this.VoltScale.Size = new System.Drawing.Size(109, 21);
            this.VoltScale.TabIndex = 4;
            this.VoltScale.SelectedIndexChanged += new System.EventHandler(this.VoltScale_SelectedIndexChanged);
            // 
            // TimeScale
            // 
            this.TimeScale.FormattingEnabled = true;
            this.TimeScale.Location = new System.Drawing.Point(12, 155);
            this.TimeScale.Name = "TimeScale";
            this.TimeScale.Size = new System.Drawing.Size(109, 21);
            this.TimeScale.TabIndex = 5;
            this.TimeScale.SelectedIndexChanged += new System.EventHandler(this.TimeScale_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Time Scale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Volt Scale";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1286, 887);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeScale);
            this.Controls.Add(this.VoltScale);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.RecordingButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "BioPac Video Recording";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem setFeedingProtocolToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEOSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_DEVICECOUNT;
        private System.Windows.Forms.ToolStripMenuItem videoSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorControlToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_FEEDST;
        private System.Windows.Forms.ToolStripMenuItem initializeVideoCardToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCODERSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEORESULT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem videoCaptureEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem bioPacEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraAssociationsToolStripMenuItem;
        private System.Windows.Forms.ComboBox VoltScale;
        private System.Windows.Forms.ComboBox TimeScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCSTAT;
    }
}

