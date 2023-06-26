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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeBioPacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DISCONNECTBIOPAC = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SETTINGS = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SELECTCHANNELS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bioPacEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraAssociationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.automaticFeederToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animalSettingsTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPelletCountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feedersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feederTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ratRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feederAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feederStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feederMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.SpaceLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.IDT_BIOPACSTAT = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.CloneChannelPanel1 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel2 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel3 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel4 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel6 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel7 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel8 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel10 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel11 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel12 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel13 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel14 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel15 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel5 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel9 = new System.Windows.Forms.Panel();
            this.CloneChannelPanel16 = new System.Windows.Forms.Panel();
            this.UnitNameStrip = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.bioPacToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.automaticFeederToolStripMenuItem,
            this.feedersToolStripMenuItem});
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
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectDirectoryToolStripMenuItem
            // 
            this.selectDirectoryToolStripMenuItem.Name = "selectDirectoryToolStripMenuItem";
            this.selectDirectoryToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.selectDirectoryToolStripMenuItem.Text = "Select &Recording Directory";
            this.selectDirectoryToolStripMenuItem.Click += new System.EventHandler(this.selectDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
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
            this.bioPacToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.bioPacToolStripMenuItem.Text = "BioPac";
            // 
            // initializeBioPacToolStripMenuItem
            // 
            this.initializeBioPacToolStripMenuItem.Name = "initializeBioPacToolStripMenuItem";
            this.initializeBioPacToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.initializeBioPacToolStripMenuItem.Text = "Connect BioPac";
            this.initializeBioPacToolStripMenuItem.Click += new System.EventHandler(this.initializeBioPacToolStripMenuItem_Click);
            // 
            // IDM_DISCONNECTBIOPAC
            // 
            this.IDM_DISCONNECTBIOPAC.Name = "IDM_DISCONNECTBIOPAC";
            this.IDM_DISCONNECTBIOPAC.Size = new System.Drawing.Size(172, 22);
            this.IDM_DISCONNECTBIOPAC.Text = "Disconnect BioPac";
            this.IDM_DISCONNECTBIOPAC.Click += new System.EventHandler(this.disconnectBioPacToolStripMenuItem_Click);
            // 
            // IDM_SETTINGS
            // 
            this.IDM_SETTINGS.Name = "IDM_SETTINGS";
            this.IDM_SETTINGS.Size = new System.Drawing.Size(172, 22);
            this.IDM_SETTINGS.Text = "Settings";
            this.IDM_SETTINGS.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // IDM_SELECTCHANNELS
            // 
            this.IDM_SELECTCHANNELS.Name = "IDM_SELECTCHANNELS";
            this.IDM_SELECTCHANNELS.Size = new System.Drawing.Size(172, 22);
            this.IDM_SELECTCHANNELS.Text = "Select Channels";
            this.IDM_SELECTCHANNELS.Click += new System.EventHandler(this.selectChannelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // bioPacEnabledToolStripMenuItem
            // 
            this.bioPacEnabledToolStripMenuItem.Name = "bioPacEnabledToolStripMenuItem";
            this.bioPacEnabledToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.bioPacEnabledToolStripMenuItem.Text = "BioPac Enabled";
            this.bioPacEnabledToolStripMenuItem.Click += new System.EventHandler(this.bioPacEnabledToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraAssociationsToolStripMenuItem,
            this.videoSettingsToolStripMenuItem,
            this.toolStripSeparator3});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // cameraAssociationsToolStripMenuItem
            // 
            this.cameraAssociationsToolStripMenuItem.Name = "cameraAssociationsToolStripMenuItem";
            this.cameraAssociationsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.cameraAssociationsToolStripMenuItem.Text = "Camera Associations";
            this.cameraAssociationsToolStripMenuItem.Click += new System.EventHandler(this.cameraAssociationsToolStripMenuItem_Click);
            // 
            // videoSettingsToolStripMenuItem
            // 
            this.videoSettingsToolStripMenuItem.Name = "videoSettingsToolStripMenuItem";
            this.videoSettingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.videoSettingsToolStripMenuItem.Size = new System.Drawing.Size(224, 20);
            this.videoSettingsToolStripMenuItem.Text = "Video Settings";
            this.videoSettingsToolStripMenuItem.Click += new System.EventHandler(this.videoSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // automaticFeederToolStripMenuItem
            // 
            this.automaticFeederToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animalSettingsTestToolStripMenuItem,
            this.injectionManagerToolStripMenuItem,
            this.AddPelletCountMenuItem});
            this.automaticFeederToolStripMenuItem.Name = "automaticFeederToolStripMenuItem";
            this.automaticFeederToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.automaticFeederToolStripMenuItem.Text = "Animal Settings";
            // 
            // animalSettingsTestToolStripMenuItem
            // 
            this.animalSettingsTestToolStripMenuItem.Name = "animalSettingsTestToolStripMenuItem";
            this.animalSettingsTestToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.animalSettingsTestToolStripMenuItem.Text = "Animal Settings";
            this.animalSettingsTestToolStripMenuItem.Click += new System.EventHandler(this.animalSettingsTestToolStripMenuItem_Click);
            // 
            // injectionManagerToolStripMenuItem
            // 
            this.injectionManagerToolStripMenuItem.Name = "injectionManagerToolStripMenuItem";
            this.injectionManagerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.injectionManagerToolStripMenuItem.Text = "Injection Manager";
            this.injectionManagerToolStripMenuItem.Click += new System.EventHandler(this.injectionManagerToolStripMenuItem_Click);
            // 
            // AddPelletCountMenuItem
            // 
            this.AddPelletCountMenuItem.Name = "AddPelletCountMenuItem";
            this.AddPelletCountMenuItem.Size = new System.Drawing.Size(170, 22);
            this.AddPelletCountMenuItem.Text = "Add Pellet Count";
            this.AddPelletCountMenuItem.Click += new System.EventHandler(this.AddPelletCountMenuItem_Click);
            // 
            // feedersToolStripMenuItem
            // 
            this.feedersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feederTestToolStripMenuItem,
            this.feederAddressToolStripMenuItem,
            this.feederStatusToolStripMenuItem,
            this.feederMenuToolStripMenuItem});
            this.feedersToolStripMenuItem.Name = "feedersToolStripMenuItem";
            this.feedersToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.feedersToolStripMenuItem.Text = "Feeders";
            // 
            // feederTestToolStripMenuItem
            // 
            this.feederTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ratRoomToolStripMenuItem,
            this.mouseRoomToolStripMenuItem});
            this.feederTestToolStripMenuItem.Name = "feederTestToolStripMenuItem";
            this.feederTestToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.feederTestToolStripMenuItem.Text = "Feeder Test";
            // 
            // ratRoomToolStripMenuItem
            // 
            this.ratRoomToolStripMenuItem.Name = "ratRoomToolStripMenuItem";
            this.ratRoomToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.ratRoomToolStripMenuItem.Text = "Rat Room";
            this.ratRoomToolStripMenuItem.Click += new System.EventHandler(this.ratRoomToolStripMenuItem_Click);
            // 
            // mouseRoomToolStripMenuItem
            // 
            this.mouseRoomToolStripMenuItem.Name = "mouseRoomToolStripMenuItem";
            this.mouseRoomToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.mouseRoomToolStripMenuItem.Text = "Mouse Room";
            this.mouseRoomToolStripMenuItem.Click += new System.EventHandler(this.mouseRoomToolStripMenuItem_Click);
            // 
            // feederAddressToolStripMenuItem
            // 
            this.feederAddressToolStripMenuItem.Name = "feederAddressToolStripMenuItem";
            this.feederAddressToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.feederAddressToolStripMenuItem.Text = "Feeder Address";
            this.feederAddressToolStripMenuItem.Click += new System.EventHandler(this.feederAddressToolStripMenuItem_Click);
            // 
            // feederStatusToolStripMenuItem
            // 
            this.feederStatusToolStripMenuItem.Name = "feederStatusToolStripMenuItem";
            this.feederStatusToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.feederStatusToolStripMenuItem.Text = "Feeder Status";
            this.feederStatusToolStripMenuItem.Click += new System.EventHandler(this.feederStatusToolStripMenuItem_Click);
            // 
            // feederMenuToolStripMenuItem
            // 
            this.feederMenuToolStripMenuItem.Name = "feederMenuToolStripMenuItem";
            this.feederMenuToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.feederMenuToolStripMenuItem.Text = "Feeder Settings";
            this.feederMenuToolStripMenuItem.Click += new System.EventHandler(this.feederMenuToolStripMenuItem_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SpaceLeft,
            this.IDT_BIOPACSTAT,
            this.IDT_MPLASTMESSAGE,
            this.IDT_VIDEOSTATUS,
            this.IDT_VIDEORESULT,
            this.IDT_DEVICECOUNT,
            this.IDT_ENCODERSTATUS,
            this.IDT_ENCSTAT,
            this.IDT_FEEDST,
            this.UnitNameStrip});
            this.StatusBar.Location = new System.Drawing.Point(0, 859);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1286, 28);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            // 
            // SpaceLeft
            // 
            this.SpaceLeft.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.SpaceLeft.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.SpaceLeft.Font = new System.Drawing.Font("Tahoma", 12F);
            this.SpaceLeft.Name = "SpaceLeft";
            this.SpaceLeft.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.SpaceLeft.RightToLeftAutoMirrorImage = true;
            this.SpaceLeft.Size = new System.Drawing.Size(108, 23);
            this.SpaceLeft.Text = "HD Size";
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
            this.IDT_BIOPACSTAT.Size = new System.Drawing.Size(179, 23);
            this.IDT_BIOPACSTAT.Text = "BioPac Disconnected";
            // 
            // IDT_MPLASTMESSAGE
            // 
            this.IDT_MPLASTMESSAGE.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_MPLASTMESSAGE.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_MPLASTMESSAGE.Name = "IDT_MPLASTMESSAGE";
            this.IDT_MPLASTMESSAGE.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_MPLASTMESSAGE.Size = new System.Drawing.Size(110, 23);
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
            this.IDT_VIDEOSTATUS.Size = new System.Drawing.Size(125, 23);
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
            this.IDT_VIDEORESULT.Size = new System.Drawing.Size(125, 23);
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
            this.IDT_DEVICECOUNT.Size = new System.Drawing.Size(108, 23);
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
            this.IDT_ENCODERSTATUS.Size = new System.Drawing.Size(108, 23);
            this.IDT_ENCODERSTATUS.Text = "ENCR STAT";
            // 
            // IDT_ENCSTAT
            // 
            this.IDT_ENCSTAT.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_ENCSTAT.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_ENCSTAT.Name = "IDT_ENCSTAT";
            this.IDT_ENCSTAT.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_ENCSTAT.Size = new System.Drawing.Size(105, 23);
            this.IDT_ENCSTAT.Text = "ENCDR RS";
            // 
            // IDT_FEEDST
            // 
            this.IDT_FEEDST.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.IDT_FEEDST.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.IDT_FEEDST.Name = "IDT_FEEDST";
            this.IDT_FEEDST.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.IDT_FEEDST.Size = new System.Drawing.Size(106, 23);
            this.IDT_FEEDST.Text = "FEEDER";
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
            // CloneChannelPanel1
            // 
            this.CloneChannelPanel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel1.Location = new System.Drawing.Point(127, 37);
            this.CloneChannelPanel1.Name = "CloneChannelPanel1";
            this.CloneChannelPanel1.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel1.TabIndex = 9;
            // 
            // CloneChannelPanel2
            // 
            this.CloneChannelPanel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel2.Location = new System.Drawing.Point(269, 37);
            this.CloneChannelPanel2.Name = "CloneChannelPanel2";
            this.CloneChannelPanel2.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel2.TabIndex = 10;
            // 
            // CloneChannelPanel3
            // 
            this.CloneChannelPanel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel3.Location = new System.Drawing.Point(411, 37);
            this.CloneChannelPanel3.Name = "CloneChannelPanel3";
            this.CloneChannelPanel3.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel3.TabIndex = 10;
            // 
            // CloneChannelPanel4
            // 
            this.CloneChannelPanel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel4.Location = new System.Drawing.Point(553, 37);
            this.CloneChannelPanel4.Name = "CloneChannelPanel4";
            this.CloneChannelPanel4.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel4.TabIndex = 10;
            // 
            // CloneChannelPanel6
            // 
            this.CloneChannelPanel6.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel6.Location = new System.Drawing.Point(837, 37);
            this.CloneChannelPanel6.Name = "CloneChannelPanel6";
            this.CloneChannelPanel6.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel6.TabIndex = 10;
            // 
            // CloneChannelPanel7
            // 
            this.CloneChannelPanel7.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel7.Location = new System.Drawing.Point(979, 37);
            this.CloneChannelPanel7.Name = "CloneChannelPanel7";
            this.CloneChannelPanel7.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel7.TabIndex = 10;
            // 
            // CloneChannelPanel8
            // 
            this.CloneChannelPanel8.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel8.Location = new System.Drawing.Point(1121, 37);
            this.CloneChannelPanel8.Name = "CloneChannelPanel8";
            this.CloneChannelPanel8.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel8.TabIndex = 10;
            // 
            // CloneChannelPanel10
            // 
            this.CloneChannelPanel10.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel10.Location = new System.Drawing.Point(269, 158);
            this.CloneChannelPanel10.Name = "CloneChannelPanel10";
            this.CloneChannelPanel10.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel10.TabIndex = 11;
            // 
            // CloneChannelPanel11
            // 
            this.CloneChannelPanel11.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel11.Location = new System.Drawing.Point(411, 158);
            this.CloneChannelPanel11.Name = "CloneChannelPanel11";
            this.CloneChannelPanel11.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel11.TabIndex = 11;
            // 
            // CloneChannelPanel12
            // 
            this.CloneChannelPanel12.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel12.Location = new System.Drawing.Point(553, 158);
            this.CloneChannelPanel12.Name = "CloneChannelPanel12";
            this.CloneChannelPanel12.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel12.TabIndex = 11;
            // 
            // CloneChannelPanel13
            // 
            this.CloneChannelPanel13.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel13.Location = new System.Drawing.Point(695, 158);
            this.CloneChannelPanel13.Name = "CloneChannelPanel13";
            this.CloneChannelPanel13.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel13.TabIndex = 11;
            // 
            // CloneChannelPanel14
            // 
            this.CloneChannelPanel14.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel14.Location = new System.Drawing.Point(837, 158);
            this.CloneChannelPanel14.Name = "CloneChannelPanel14";
            this.CloneChannelPanel14.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel14.TabIndex = 11;
            // 
            // CloneChannelPanel15
            // 
            this.CloneChannelPanel15.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel15.Location = new System.Drawing.Point(979, 158);
            this.CloneChannelPanel15.Name = "CloneChannelPanel15";
            this.CloneChannelPanel15.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel15.TabIndex = 11;
            // 
            // CloneChannelPanel5
            // 
            this.CloneChannelPanel5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel5.Location = new System.Drawing.Point(695, 37);
            this.CloneChannelPanel5.Name = "CloneChannelPanel5";
            this.CloneChannelPanel5.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel5.TabIndex = 11;
            // 
            // CloneChannelPanel9
            // 
            this.CloneChannelPanel9.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel9.Location = new System.Drawing.Point(127, 158);
            this.CloneChannelPanel9.Name = "CloneChannelPanel9";
            this.CloneChannelPanel9.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel9.TabIndex = 11;
            // 
            // CloneChannelPanel16
            // 
            this.CloneChannelPanel16.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel16.Location = new System.Drawing.Point(1121, 158);
            this.CloneChannelPanel16.Name = "CloneChannelPanel16";
            this.CloneChannelPanel16.Size = new System.Drawing.Size(136, 115);
            this.CloneChannelPanel16.TabIndex = 12;
            // 
            // UnitNameStrip
            // 
            this.UnitNameStrip.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.UnitNameStrip.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.UnitNameStrip.Name = "UnitNameStrip";
            this.UnitNameStrip.Size = new System.Drawing.Size(73, 23);
            this.UnitNameStrip.Text = "UNIT LABEL";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1286, 887);
            this.Controls.Add(this.CloneChannelPanel16);
            this.Controls.Add(this.CloneChannelPanel9);
            this.Controls.Add(this.CloneChannelPanel5);
            this.Controls.Add(this.CloneChannelPanel15);
            this.Controls.Add(this.CloneChannelPanel14);
            this.Controls.Add(this.CloneChannelPanel13);
            this.Controls.Add(this.CloneChannelPanel12);
            this.Controls.Add(this.CloneChannelPanel11);
            this.Controls.Add(this.CloneChannelPanel10);
            this.Controls.Add(this.CloneChannelPanel8);
            this.Controls.Add(this.CloneChannelPanel7);
            this.Controls.Add(this.CloneChannelPanel6);
            this.Controls.Add(this.CloneChannelPanel4);
            this.Controls.Add(this.CloneChannelPanel3);
            this.Controls.Add(this.CloneChannelPanel2);
            this.Controls.Add(this.CloneChannelPanel1);
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
            this.Text = "BioPacVideo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.ToolStripMenuItem selectDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initializeBioPacToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IDM_DISCONNECTBIOPAC;
        private System.Windows.Forms.ToolStripMenuItem IDM_SELECTCHANNELS;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel IDT_BIOPACSTAT;
        private System.Windows.Forms.ToolStripMenuItem IDM_SETTINGS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_MPLASTMESSAGE;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEOSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_DEVICECOUNT;
        private System.Windows.Forms.ToolStripMenuItem videoSettingsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_FEEDST;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCODERSTATUS;
        private System.Windows.Forms.ToolStripStatusLabel IDT_VIDEORESULT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem bioPacEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraAssociationsToolStripMenuItem;
        private System.Windows.Forms.ComboBox VoltScale;
        private System.Windows.Forms.ComboBox TimeScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ToolStripStatusLabel IDT_ENCSTAT;
        private System.Windows.Forms.ToolStripMenuItem AddPelletCountMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel SpaceLeft;
        private System.Windows.Forms.ToolStripMenuItem injectionManagerToolStripMenuItem;
        private System.Windows.Forms.Panel CloneChannelPanel1;
        private System.Windows.Forms.Panel CloneChannelPanel2;
        private System.Windows.Forms.Panel CloneChannelPanel3;
        private System.Windows.Forms.Panel CloneChannelPanel4;
        private System.Windows.Forms.Panel CloneChannelPanel6;
        private System.Windows.Forms.Panel CloneChannelPanel7;
        private System.Windows.Forms.Panel CloneChannelPanel8;
        private System.Windows.Forms.Panel CloneChannelPanel10;
        private System.Windows.Forms.Panel CloneChannelPanel11;
        private System.Windows.Forms.Panel CloneChannelPanel12;
        private System.Windows.Forms.Panel CloneChannelPanel13;
        private System.Windows.Forms.Panel CloneChannelPanel14;
        private System.Windows.Forms.Panel CloneChannelPanel15;
        private System.Windows.Forms.Panel CloneChannelPanel5;
        private System.Windows.Forms.Panel CloneChannelPanel9;
        private System.Windows.Forms.Panel CloneChannelPanel16;
        private System.Windows.Forms.ToolStripMenuItem feedersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feederTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feederAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feederStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ratRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feederMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animalSettingsTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel UnitNameStrip;
    }
}

