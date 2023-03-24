
namespace ProjectManager
{
    partial class PMEEGView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ZoomBar = new System.Windows.Forms.TrackBar();
            this.myVLC = new LibVLCSharp.WinForms.VideoView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.telemetryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultView = new System.Windows.Forms.ToolStripMenuItem();
            this.galleryView = new System.Windows.Forms.ToolStripMenuItem();
            this.animalView = new System.Windows.Forms.ToolStripMenuItem();
            this.moreOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HighRes = new System.Windows.Forms.ToolStripMenuItem();
            this.LowRes = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.TimeFrameLabel = new System.Windows.Forms.Label();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.GVGrouping = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.GalArea = new System.Windows.Forms.Panel();
            this.ConflictingList = new System.Windows.Forms.ListView();
            this.conflictName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictBubble = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.EditNotesButton = new System.Windows.Forms.Button();
            this.NotesSection = new System.Windows.Forms.Label();
            this.animalListView = new System.Windows.Forms.ListView();
            this.LVAnimal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.szListView = new System.Windows.Forms.ListView();
            this.SZLVDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SZLVScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SZLVNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GalGBox = new System.Windows.Forms.GroupBox();
            this.PauseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.TimeFrameBar = new System.Windows.Forms.TrackBar();
            this.TF1s = new System.Windows.Forms.Label();
            this.TimeFramePanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TF90s = new System.Windows.Forms.Label();
            this.TF30s = new System.Windows.Forms.Label();
            this.TFLabel = new System.Windows.Forms.Label();
            this.TF120s = new System.Windows.Forms.Label();
            this.TF60s = new System.Windows.Forms.Label();
            this.ShowBuffer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.GalArea.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeFrameBar)).BeginInit();
            this.TimeFramePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZoomBar
            // 
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(1029, 326);
            this.ZoomBar.Maximum = 20;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(173, 21);
            this.ZoomBar.TabIndex = 4;
            this.ZoomBar.TabStop = false;
            this.ZoomBar.TickFrequency = 2;
            this.ZoomBar.Value = 10;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // myVLC
            // 
            this.myVLC.BackColor = System.Drawing.Color.White;
            this.myVLC.Enabled = false;
            this.myVLC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.myVLC.Location = new System.Drawing.Point(519, 29);
            this.myVLC.MediaPlayer = null;
            this.myVLC.Name = "myVLC";
            this.myVLC.Size = new System.Drawing.Size(467, 306);
            this.myVLC.TabIndex = 2;
            this.myVLC.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.moreOptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1257, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "PMEEGMenu";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.telemetryToolStripMenuItem1});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // telemetryToolStripMenuItem1
            // 
            this.telemetryToolStripMenuItem1.CheckOnClick = true;
            this.telemetryToolStripMenuItem1.Name = "telemetryToolStripMenuItem1";
            this.telemetryToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.telemetryToolStripMenuItem1.Text = "Telemetry";
            this.telemetryToolStripMenuItem1.Click += new System.EventHandler(this.telemetryToolStripMenuItem1_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.CheckOnClick = true;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DefaultView,
            this.galleryView,
            this.animalView});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // DefaultView
            // 
            this.DefaultView.Checked = true;
            this.DefaultView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefaultView.Name = "DefaultView";
            this.DefaultView.Size = new System.Drawing.Size(112, 22);
            this.DefaultView.Text = "Default";
            this.DefaultView.Click += new System.EventHandler(this.normalListToolStripMenuItem_Click);
            // 
            // galleryView
            // 
            this.galleryView.CheckOnClick = true;
            this.galleryView.Enabled = false;
            this.galleryView.Name = "galleryView";
            this.galleryView.Size = new System.Drawing.Size(112, 22);
            this.galleryView.Text = "Gallery";
            this.galleryView.Click += new System.EventHandler(this.normalListToolStripMenuItem_Click);
            // 
            // animalView
            // 
            this.animalView.CheckOnClick = true;
            this.animalView.Enabled = false;
            this.animalView.Name = "animalView";
            this.animalView.Size = new System.Drawing.Size(112, 22);
            this.animalView.Text = "Animal";
            this.animalView.Click += new System.EventHandler(this.normalListToolStripMenuItem_Click);
            // 
            // moreOptionsToolStripMenuItem
            // 
            this.moreOptionsToolStripMenuItem.CheckOnClick = true;
            this.moreOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomizedToolStripMenuItem,
            this.videoSizeToolStripMenuItem});
            this.moreOptionsToolStripMenuItem.Name = "moreOptionsToolStripMenuItem";
            this.moreOptionsToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.moreOptionsToolStripMenuItem.Text = "More Options";
            // 
            // randomizedToolStripMenuItem
            // 
            this.randomizedToolStripMenuItem.Name = "randomizedToolStripMenuItem";
            this.randomizedToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.randomizedToolStripMenuItem.Text = "Randomized";
            // 
            // videoSizeToolStripMenuItem
            // 
            this.videoSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HighRes,
            this.LowRes});
            this.videoSizeToolStripMenuItem.Enabled = false;
            this.videoSizeToolStripMenuItem.Name = "videoSizeToolStripMenuItem";
            this.videoSizeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.videoSizeToolStripMenuItem.Text = "Video Size";
            // 
            // HighRes
            // 
            this.HighRes.Checked = true;
            this.HighRes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HighRes.Name = "HighRes";
            this.HighRes.Size = new System.Drawing.Size(99, 22);
            this.HighRes.Text = "480p";
            this.HighRes.Click += new System.EventHandler(this.HighRes_Click);
            // 
            // LowRes
            // 
            this.LowRes.Name = "LowRes";
            this.LowRes.Size = new System.Drawing.Size(99, 22);
            this.LowRes.Text = "360p";
            this.LowRes.Click += new System.EventHandler(this.LowRes_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Enabled = false;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeLabel.Location = new System.Drawing.Point(620, 338);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(87, 18);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "0:00 / 0:00";
            this.TimeLabel.Visible = false;
            // 
            // TimeFrameLabel
            // 
            this.TimeFrameLabel.AutoSize = true;
            this.TimeFrameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeFrameLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeFrameLabel.Location = new System.Drawing.Point(34, 3);
            this.TimeFrameLabel.Name = "TimeFrameLabel";
            this.TimeFrameLabel.Size = new System.Drawing.Size(161, 18);
            this.TimeFrameLabel.TabIndex = 11;
            this.TimeFrameLabel.Text = "Time Frame (Seconds)";
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomLabel.AutoSize = true;
            this.ZoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.ZoomLabel.Location = new System.Drawing.Point(1089, 305);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(48, 18);
            this.ZoomLabel.TabIndex = 12;
            this.ZoomLabel.Text = "Zoom";
            // 
            // GVGrouping
            // 
            this.GVGrouping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GVGrouping.Enabled = false;
            this.GVGrouping.Location = new System.Drawing.Point(510, 22);
            this.GVGrouping.Name = "GVGrouping";
            this.GVGrouping.Size = new System.Drawing.Size(745, 514);
            this.GVGrouping.TabIndex = 15;
            this.GVGrouping.Text = "label2";
            this.GVGrouping.Visible = false;
            this.GVGrouping.Click += new System.EventHandler(this.GVGrouping_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(1237, 375);
            this.vScrollBar1.Minimum = 4;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 128);
            this.vScrollBar1.TabIndex = 17;
            this.vScrollBar1.Value = 50;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // GalArea
            // 
            this.GalArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GalArea.Controls.Add(this.ConflictingList);
            this.GalArea.Controls.Add(this.panel1);
            this.GalArea.Controls.Add(this.animalListView);
            this.GalArea.Controls.Add(this.szListView);
            this.GalArea.Location = new System.Drawing.Point(0, 24);
            this.GalArea.Name = "GalArea";
            this.GalArea.Size = new System.Drawing.Size(513, 509);
            this.GalArea.TabIndex = 20;
            this.GalArea.Visible = false;
            this.GalArea.Paint += new System.Windows.Forms.PaintEventHandler(this.GalArea_Paint);
            this.GalArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);
            // 
            // ConflictingList
            // 
            this.ConflictingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.conflictName,
            this.conflictDate,
            this.conflictBubble,
            this.conflictNotes});
            this.ConflictingList.HideSelection = false;
            this.ConflictingList.Location = new System.Drawing.Point(12, 5);
            this.ConflictingList.Name = "ConflictingList";
            this.ConflictingList.Size = new System.Drawing.Size(378, 358);
            this.ConflictingList.TabIndex = 33;
            this.ConflictingList.UseCompatibleStateImageBehavior = false;
            this.ConflictingList.View = System.Windows.Forms.View.Details;
            this.ConflictingList.Visible = false;
            // 
            // conflictName
            // 
            this.conflictName.Text = "Animal";
            this.conflictName.Width = 87;
            // 
            // conflictDate
            // 
            this.conflictDate.Text = "Date";
            this.conflictDate.Width = 112;
            // 
            // conflictBubble
            // 
            this.conflictBubble.Text = "Bubble Score";
            this.conflictBubble.Width = 91;
            // 
            // conflictNotes
            // 
            this.conflictNotes.Text = "Notes Score";
            this.conflictNotes.Width = 80;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.EditNotesButton);
            this.panel1.Controls.Add(this.NotesSection);
            this.panel1.Location = new System.Drawing.Point(9, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 138);
            this.panel1.TabIndex = 32;
            // 
            // EditNotesButton
            // 
            this.EditNotesButton.AutoSize = true;
            this.EditNotesButton.Enabled = false;
            this.EditNotesButton.Location = new System.Drawing.Point(445, 8);
            this.EditNotesButton.Name = "EditNotesButton";
            this.EditNotesButton.Size = new System.Drawing.Size(36, 23);
            this.EditNotesButton.TabIndex = 35;
            this.EditNotesButton.Text = "Edit";
            this.EditNotesButton.UseVisualStyleBackColor = true;
            this.EditNotesButton.Click += new System.EventHandler(this.EditNotesButton_Click);
            // 
            // NotesSection
            // 
            this.NotesSection.BackColor = System.Drawing.SystemColors.Control;
            this.NotesSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesSection.Location = new System.Drawing.Point(3, 8);
            this.NotesSection.Name = "NotesSection";
            this.NotesSection.Size = new System.Drawing.Size(438, 110);
            this.NotesSection.TabIndex = 30;
            this.NotesSection.Text = " ";
            // 
            // animalListView
            // 
            this.animalListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LVAnimal});
            this.animalListView.HideSelection = false;
            this.animalListView.Location = new System.Drawing.Point(9, 5);
            this.animalListView.Name = "animalListView";
            this.animalListView.Size = new System.Drawing.Size(110, 358);
            this.animalListView.TabIndex = 0;
            this.animalListView.UseCompatibleStateImageBehavior = false;
            this.animalListView.View = System.Windows.Forms.View.Details;
            this.animalListView.SelectedIndexChanged += new System.EventHandler(this.seizureListView_SelectedIndexChanged);
            // 
            // LVAnimal
            // 
            this.LVAnimal.Text = "Animal";
            this.LVAnimal.Width = 103;
            // 
            // szListView
            // 
            this.szListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SZLVDate,
            this.SZLVScore,
            this.SZLVNumber});
            this.szListView.HideSelection = false;
            this.szListView.Location = new System.Drawing.Point(125, 5);
            this.szListView.Name = "szListView";
            this.szListView.Size = new System.Drawing.Size(379, 358);
            this.szListView.TabIndex = 1;
            this.szListView.UseCompatibleStateImageBehavior = false;
            this.szListView.View = System.Windows.Forms.View.Details;
            this.szListView.Visible = false;
            // 
            // SZLVDate
            // 
            this.SZLVDate.Text = "Date";
            this.SZLVDate.Width = 217;
            // 
            // SZLVScore
            // 
            this.SZLVScore.Text = "Score";
            // 
            // SZLVNumber
            // 
            this.SZLVNumber.Text = "Seizure Number";
            this.SZLVNumber.Width = 91;
            // 
            // GalGBox
            // 
            this.GalGBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GalGBox.Enabled = false;
            this.GalGBox.Location = new System.Drawing.Point(519, 353);
            this.GalGBox.Name = "GalGBox";
            this.GalGBox.Size = new System.Drawing.Size(715, 178);
            this.GalGBox.TabIndex = 21;
            this.GalGBox.TabStop = false;
            this.GalGBox.Enter += new System.EventHandler(this.GalGBox_Enter);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(568, 336);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(46, 23);
            this.PauseButton.TabIndex = 25;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Visible = false;
            this.PauseButton.Click += new System.EventHandler(this.Pause_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(519, 336);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(46, 23);
            this.PlayButton.TabIndex = 26;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Visible = false;
            this.PlayButton.Click += new System.EventHandler(this.Play_Click);
            // 
            // TimeFrameBar
            // 
            this.TimeFrameBar.LargeChange = 15;
            this.TimeFrameBar.Location = new System.Drawing.Point(5, 24);
            this.TimeFrameBar.Maximum = 240;
            this.TimeFrameBar.Name = "TimeFrameBar";
            this.TimeFrameBar.Size = new System.Drawing.Size(191, 45);
            this.TimeFrameBar.TabIndex = 27;
            this.TimeFrameBar.TickFrequency = 30;
            this.TimeFrameBar.Value = 30;
            this.TimeFrameBar.Scroll += new System.EventHandler(this.TimeFrameBar_Scroll);
            // 
            // TF1s
            // 
            this.TF1s.AutoSize = true;
            this.TF1s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF1s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF1s.Location = new System.Drawing.Point(12, 54);
            this.TF1s.Name = "TF1s";
            this.TF1s.Size = new System.Drawing.Size(13, 13);
            this.TF1s.TabIndex = 28;
            this.TF1s.Text = "1";
            this.TF1s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // TimeFramePanel
            // 
            this.TimeFramePanel.Controls.Add(this.label4);
            this.TimeFramePanel.Controls.Add(this.label3);
            this.TimeFramePanel.Controls.Add(this.label2);
            this.TimeFramePanel.Controls.Add(this.label1);
            this.TimeFramePanel.Controls.Add(this.TF90s);
            this.TimeFramePanel.Controls.Add(this.TF30s);
            this.TimeFramePanel.Controls.Add(this.TFLabel);
            this.TimeFramePanel.Controls.Add(this.TF120s);
            this.TimeFramePanel.Controls.Add(this.TF60s);
            this.TimeFramePanel.Controls.Add(this.TimeFrameLabel);
            this.TimeFramePanel.Controls.Add(this.TF1s);
            this.TimeFramePanel.Controls.Add(this.TimeFrameBar);
            this.TimeFramePanel.Location = new System.Drawing.Point(1003, 230);
            this.TimeFramePanel.Name = "TimeFramePanel";
            this.TimeFramePanel.Size = new System.Drawing.Size(240, 72);
            this.TimeFramePanel.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(172, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "240";
            this.label4.Click += new System.EventHandler(this.TFs_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(151, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "210";
            this.label3.Click += new System.EventHandler(this.TFs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(130, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "180";
            this.label2.Click += new System.EventHandler(this.TFs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(109, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "150";
            this.label1.Click += new System.EventHandler(this.TFs_Click);
            // 
            // TF90s
            // 
            this.TF90s.AutoSize = true;
            this.TF90s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF90s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF90s.Location = new System.Drawing.Point(71, 52);
            this.TF90s.Name = "TF90s";
            this.TF90s.Size = new System.Drawing.Size(19, 13);
            this.TF90s.TabIndex = 33;
            this.TF90s.Tag = "testtag";
            this.TF90s.Text = "90";
            this.TF90s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // TF30s
            // 
            this.TF30s.AutoSize = true;
            this.TF30s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF30s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF30s.Location = new System.Drawing.Point(31, 53);
            this.TF30s.Name = "TF30s";
            this.TF30s.Size = new System.Drawing.Size(19, 13);
            this.TF30s.TabIndex = 32;
            this.TF30s.Text = "30";
            this.TF30s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // TFLabel
            // 
            this.TFLabel.AutoSize = true;
            this.TFLabel.BackColor = System.Drawing.SystemColors.Control;
            this.TFLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TFLabel.Location = new System.Drawing.Point(199, 26);
            this.TFLabel.Name = "TFLabel";
            this.TFLabel.Size = new System.Drawing.Size(22, 16);
            this.TFLabel.TabIndex = 31;
            this.TFLabel.Text = "30";
            // 
            // TF120s
            // 
            this.TF120s.AutoSize = true;
            this.TF120s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF120s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF120s.Location = new System.Drawing.Point(88, 52);
            this.TF120s.Name = "TF120s";
            this.TF120s.Size = new System.Drawing.Size(25, 13);
            this.TF120s.TabIndex = 30;
            this.TF120s.Text = "120";
            this.TF120s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // TF60s
            // 
            this.TF60s.AutoSize = true;
            this.TF60s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF60s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF60s.Location = new System.Drawing.Point(51, 53);
            this.TF60s.Name = "TF60s";
            this.TF60s.Size = new System.Drawing.Size(19, 13);
            this.TF60s.TabIndex = 29;
            this.TF60s.Text = "60";
            this.TF60s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // ShowBuffer
            // 
            this.ShowBuffer.AutoSize = true;
            this.ShowBuffer.Checked = true;
            this.ShowBuffer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowBuffer.ForeColor = System.Drawing.SystemColors.Control;
            this.ShowBuffer.Location = new System.Drawing.Point(241, 837);
            this.ShowBuffer.Name = "ShowBuffer";
            this.ShowBuffer.Size = new System.Drawing.Size(84, 17);
            this.ShowBuffer.TabIndex = 35;
            this.ShowBuffer.Text = "Show Buffer";
            this.ShowBuffer.UseVisualStyleBackColor = true;
            this.ShowBuffer.CheckedChanged += new System.EventHandler(this.ShowBuffer_CheckedChanged);
            // 
            // PMEEGView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1257, 548);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.ShowBuffer);
            this.Controls.Add(this.GalGBox);
            this.Controls.Add(this.TimeFramePanel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.ZoomLabel);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GalArea);
            this.Controls.Add(this.myVLC);
            this.Controls.Add(this.GVGrouping);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.Name = "PMEEGView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEG View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PMEEGView_Close);
            this.Load += new System.EventHandler(this.PMEEGView_Load);
            this.ResizeEnd += new System.EventHandler(this.PMEEGView_ResizeEnd_2);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PMEEGView_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GalArea.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeFrameBar)).EndInit();
            this.TimeFramePanel.ResumeLayout(false);
            this.TimeFramePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar ZoomBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem galleryView;
        public System.Windows.Forms.ToolStripMenuItem animalView;
        private System.Windows.Forms.ToolStripMenuItem moreOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DefaultView;
        public LibVLCSharp.WinForms.VideoView myVLC;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label TimeFrameLabel;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.ToolStripMenuItem videoSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HighRes;
        private System.Windows.Forms.ToolStripMenuItem LowRes;
        private System.Windows.Forms.Label GVGrouping;
        private System.Windows.Forms.ToolStripMenuItem telemetryToolStripMenuItem1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel GalArea;
        private System.Windows.Forms.GroupBox GalGBox;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.TrackBar TimeFrameBar;
        private System.Windows.Forms.Label TF1s;
        private System.Windows.Forms.Panel TimeFramePanel;
        private System.Windows.Forms.Label TFLabel;
        private System.Windows.Forms.Label TF120s;
        private System.Windows.Forms.Label TF60s;
        private System.Windows.Forms.Label TF90s;
        private System.Windows.Forms.Label TF30s;
        private System.Windows.Forms.Label NotesSection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button EditNotesButton;
        private System.Windows.Forms.CheckBox ShowBuffer;
        private System.Windows.Forms.ListView animalListView;
        private System.Windows.Forms.ColumnHeader LVAnimal;
        private System.Windows.Forms.ListView szListView;
        private System.Windows.Forms.ColumnHeader SZLVDate;
        private System.Windows.Forms.ColumnHeader SZLVScore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader SZLVNumber;
        private System.Windows.Forms.ListView ConflictingList;
        private System.Windows.Forms.ColumnHeader conflictName;
        private System.Windows.Forms.ColumnHeader conflictDate;
        private System.Windows.Forms.ColumnHeader conflictBubble;
        private System.Windows.Forms.ColumnHeader conflictNotes;
    }
}