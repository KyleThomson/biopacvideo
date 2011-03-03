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
            this.BioPacStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.RecordingStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.MPLastMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.TickCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_VIDEOSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_DEVICECOUNT = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDC_RATSELECT = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IDB_TESTVIDEO = new System.Windows.Forms.Button();
            this.IDS_ENCODERSTATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordingButton
            // 
            this.RecordingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordingButton.Location = new System.Drawing.Point(1144, 27);
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
            this.menuStrip1.Size = new System.Drawing.Size(1400, 24);
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
            this.selectDirectoryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.selectDirectoryToolStripMenuItem.Text = "Select &Recording Directory";
            this.selectDirectoryToolStripMenuItem.Click += new System.EventHandler(this.selectDirectoryToolStripMenuItem_Click);
            // 
            // openSettingsToolStripMenuItem
            // 
            this.openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
            this.openSettingsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openSettingsToolStripMenuItem.Text = "&Open Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoSettingsToolStripMenuItem,
            this.sensorControlToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // videoSettingsToolStripMenuItem
            // 
            this.videoSettingsToolStripMenuItem.Name = "videoSettingsToolStripMenuItem";
            this.videoSettingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.videoSettingsToolStripMenuItem.Size = new System.Drawing.Size(196, 20);
            this.videoSettingsToolStripMenuItem.Text = "Video Settings";
            this.videoSettingsToolStripMenuItem.Click += new System.EventHandler(this.videoSettingsToolStripMenuItem_Click);
            // 
            // sensorControlToolStripMenuItem
            // 
            this.sensorControlToolStripMenuItem.Name = "sensorControlToolStripMenuItem";
            this.sensorControlToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.sensorControlToolStripMenuItem.Text = "Sensor Control";
            this.sensorControlToolStripMenuItem.Click += new System.EventHandler(this.sensorControlToolStripMenuItem_Click);
            // 
            // bioPacToolStripMenuItem
            // 
            this.bioPacToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initializeBioPacToolStripMenuItem,
            this.IDM_DISCONNECTBIOPAC,
            this.IDM_SETTINGS,
            this.IDM_SELECTCHANNELS});
            this.bioPacToolStripMenuItem.Name = "bioPacToolStripMenuItem";
            this.bioPacToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.bioPacToolStripMenuItem.Text = "BioPac";
            // 
            // initializeBioPacToolStripMenuItem
            // 
            this.initializeBioPacToolStripMenuItem.Name = "initializeBioPacToolStripMenuItem";
            this.initializeBioPacToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.initializeBioPacToolStripMenuItem.Text = "Connect BioPac";
            this.initializeBioPacToolStripMenuItem.Click += new System.EventHandler(this.initializeBioPacToolStripMenuItem_Click);
            // 
            // IDM_DISCONNECTBIOPAC
            // 
            this.IDM_DISCONNECTBIOPAC.Name = "IDM_DISCONNECTBIOPAC";
            this.IDM_DISCONNECTBIOPAC.Size = new System.Drawing.Size(171, 22);
            this.IDM_DISCONNECTBIOPAC.Text = "Disconnect BioPac";
            this.IDM_DISCONNECTBIOPAC.Click += new System.EventHandler(this.disconnectBioPacToolStripMenuItem_Click);
            // 
            // IDM_SETTINGS
            // 
            this.IDM_SETTINGS.Name = "IDM_SETTINGS";
            this.IDM_SETTINGS.Size = new System.Drawing.Size(171, 22);
            this.IDM_SETTINGS.Text = "Settings";
            this.IDM_SETTINGS.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // IDM_SELECTCHANNELS
            // 
            this.IDM_SELECTCHANNELS.Name = "IDM_SELECTCHANNELS";
            this.IDM_SELECTCHANNELS.Size = new System.Drawing.Size(171, 22);
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
            this.testFeedersToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.testFeedersToolStripMenuItem.Text = "Test Feeders";
            this.testFeedersToolStripMenuItem.Click += new System.EventHandler(this.testFeedersToolStripMenuItem_Click);
            // 
            // setFeedingProtocolToolStripMenuItem
            // 
            this.setFeedingProtocolToolStripMenuItem.Name = "setFeedingProtocolToolStripMenuItem";
            this.setFeedingProtocolToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.setFeedingProtocolToolStripMenuItem.Text = "Set Feeding Protocol";
            this.setFeedingProtocolToolStripMenuItem.Click += new System.EventHandler(this.setFeedingProtocolToolStripMenuItem_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BioPacStat,
            this.RecordingStatus,
            this.MPLastMessage,
            this.TickCountLabel,
            this.IDT_VIDEOSTATUS,
            this.IDT_DEVICECOUNT,
            this.IDS_ENCODERSTATUS});
            this.StatusBar.Location = new System.Drawing.Point(0, 734);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1400, 26);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            // 
            // BioPacStat
            // 
            this.BioPacStat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.BioPacStat.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.BioPacStat.Font = new System.Drawing.Font("Tahoma", 10F);
            this.BioPacStat.Name = "BioPacStat";
            this.BioPacStat.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.BioPacStat.Size = new System.Drawing.Size(179, 21);
            this.BioPacStat.Text = "BioPac Disconnected";
            // 
            // RecordingStatus
            // 
            this.RecordingStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.RecordingStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.RecordingStatus.Font = new System.Drawing.Font("Tahoma", 10F);
            this.RecordingStatus.Name = "RecordingStatus";
            this.RecordingStatus.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.RecordingStatus.Size = new System.Drawing.Size(140, 21);
            this.RecordingStatus.Text = "Not Recording";
            // 
            // MPLastMessage
            // 
            this.MPLastMessage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.MPLastMessage.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.MPLastMessage.Name = "MPLastMessage";
            this.MPLastMessage.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.MPLastMessage.Size = new System.Drawing.Size(107, 21);
            this.MPLastMessage.Text = "NO STATUS";
            // 
            // TickCountLabel
            // 
            this.TickCountLabel.Name = "TickCountLabel";
            this.TickCountLabel.Size = new System.Drawing.Size(64, 21);
            this.TickCountLabel.Text = "Tick Count: ";
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
            // IDT_DEVICECOUNT
            // 
            this.IDT_DEVICECOUNT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_DEVICECOUNT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_DEVICECOUNT.Name = "IDT_DEVICECOUNT";
            this.IDT_DEVICECOUNT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_DEVICECOUNT.Size = new System.Drawing.Size(134, 21);
            this.IDT_DEVICECOUNT.Text = "Video Devices (0)";
            // 
            // IDC_RATSELECT
            // 
            this.IDC_RATSELECT.FormattingEnabled = true;
            this.IDC_RATSELECT.Location = new System.Drawing.Point(1144, 112);
            this.IDC_RATSELECT.Name = "IDC_RATSELECT";
            this.IDC_RATSELECT.Size = new System.Drawing.Size(154, 21);
            this.IDC_RATSELECT.TabIndex = 4;
            this.IDC_RATSELECT.SelectedIndexChanged += new System.EventHandler(this.IDC_RATSELECT_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1141, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Animal";
            // 
            // IDB_TESTVIDEO
            // 
            this.IDB_TESTVIDEO.Location = new System.Drawing.Point(921, 38);
            this.IDB_TESTVIDEO.Name = "IDB_TESTVIDEO";
            this.IDB_TESTVIDEO.Size = new System.Drawing.Size(75, 23);
            this.IDB_TESTVIDEO.TabIndex = 6;
            this.IDB_TESTVIDEO.Text = "TestVideo";
            this.IDB_TESTVIDEO.UseVisualStyleBackColor = true;
            this.IDB_TESTVIDEO.Click += new System.EventHandler(this.IDB_TESTVIDEO_Click);
            // 
            // IDS_ENCODERSTATUS
            // 
            this.IDS_ENCODERSTATUS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDS_ENCODERSTATUS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDS_ENCODERSTATUS.Name = "IDS_ENCODERSTATUS";
            this.IDS_ENCODERSTATUS.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDS_ENCODERSTATUS.Size = new System.Drawing.Size(140, 21);
            this.IDS_ENCODERSTATUS.Text = "ENCODER STATUS";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1400, 760);
            this.Controls.Add(this.IDB_TESTVIDEO);
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
        private System.Windows.Forms.ToolStripStatusLabel BioPacStat;
        private System.Windows.Forms.ToolStripStatusLabel RecordingStatus;
        private System.Windows.Forms.ToolStripMenuItem IDM_SETTINGS;
        private System.Windows.Forms.ToolStripStatusLabel MPLastMessage;
        private System.Windows.Forms.ToolStripStatusLabel TickCountLabel;
        private System.Windows.Forms.ToolStripMenuItem setFeedingProtocolToolStripMenuItem;
        private System.Windows.Forms.ComboBox IDC_RATSELECT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button IDB_TESTVIDEO;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEOSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_DEVICECOUNT;
        private System.Windows.Forms.ToolStripMenuItem videoSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorControlToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDS_ENCODERSTATUS;
    }
}

