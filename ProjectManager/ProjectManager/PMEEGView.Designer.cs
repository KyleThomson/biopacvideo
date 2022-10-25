
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
            this.Next = new System.Windows.Forms.Button();
            this.Previous = new System.Windows.Forms.Button();
            this.TimeFrameLabel = new System.Windows.Forms.Label();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.BottomLabel = new System.Windows.Forms.Label();
            this.GVGrouping = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.pg = new System.Windows.Forms.Label();
            this.GalArea = new System.Windows.Forms.Panel();
            this.GalGBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.TimeFrameBar = new System.Windows.Forms.TrackBar();
            this.TF1s = new System.Windows.Forms.Label();
            this.TimeFramePanel = new System.Windows.Forms.Panel();
            this.TF90s = new System.Windows.Forms.Label();
            this.TF30s = new System.Windows.Forms.Label();
            this.TFLabel = new System.Windows.Forms.Label();
            this.TF120s = new System.Windows.Forms.Label();
            this.TF60s = new System.Windows.Forms.Label();
            this.NotesSection = new System.Windows.Forms.Label();
            this.ANL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EditNotesButton = new System.Windows.Forms.Button();
            this.RacineLabel = new System.Windows.Forms.Label();
            this.RSL = new System.Windows.Forms.Label();
            this.ANLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeFrameBar)).BeginInit();
            this.TimeFramePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZoomBar
            // 
            this.ZoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(45, 811);
            this.ZoomBar.Maximum = 20;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(128, 21);
            this.ZoomBar.TabIndex = 4;
            this.ZoomBar.TabStop = false;
            this.ZoomBar.TickFrequency = 2;
            this.ZoomBar.Value = 10;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // myVLC
            // 
            this.myVLC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.myVLC.BackColor = System.Drawing.Color.Black;
            this.myVLC.Enabled = false;
            this.myVLC.Location = new System.Drawing.Point(939, 29);
            this.myVLC.MediaPlayer = null;
            this.myVLC.Name = "myVLC";
            this.myVLC.Size = new System.Drawing.Size(640, 480);
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
            this.menuStrip1.Size = new System.Drawing.Size(1584, 24);
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
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Enabled = false;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeLabel.Location = new System.Drawing.Point(1435, 748);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(87, 18);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "0:00 / 0:00";
            this.TimeLabel.Visible = false;
            // 
            // Next
            // 
            this.Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Next.Location = new System.Drawing.Point(261, 790);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 9;
            this.Next.TabStop = false;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Previous
            // 
            this.Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Previous.Location = new System.Drawing.Point(180, 790);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(75, 23);
            this.Previous.TabIndex = 10;
            this.Previous.TabStop = false;
            this.Previous.Text = "Previous";
            this.Previous.UseVisualStyleBackColor = true;
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // TimeFrameLabel
            // 
            this.TimeFrameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeFrameLabel.AutoSize = true;
            this.TimeFrameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeFrameLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeFrameLabel.Location = new System.Drawing.Point(21, 3);
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
            this.ZoomLabel.Location = new System.Drawing.Point(86, 790);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(48, 18);
            this.ZoomLabel.TabIndex = 12;
            this.ZoomLabel.Text = "Zoom";
            // 
            // BottomLabel
            // 
            this.BottomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BottomLabel.Location = new System.Drawing.Point(12, 717);
            this.BottomLabel.Name = "BottomLabel";
            this.BottomLabel.Size = new System.Drawing.Size(1595, 162);
            this.BottomLabel.TabIndex = 13;
            // 
            // GVGrouping
            // 
            this.GVGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVGrouping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GVGrouping.Enabled = false;
            this.GVGrouping.Location = new System.Drawing.Point(931, 24);
            this.GVGrouping.Name = "GVGrouping";
            this.GVGrouping.Size = new System.Drawing.Size(653, 855);
            this.GVGrouping.TabIndex = 15;
            this.GVGrouping.Text = "label2";
            this.GVGrouping.Visible = false;
            this.GVGrouping.Click += new System.EventHandler(this.GVGrouping_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar1.Location = new System.Drawing.Point(25, 778);
            this.vScrollBar1.Minimum = 4;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 80);
            this.vScrollBar1.TabIndex = 17;
            this.vScrollBar1.Value = 50;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // pg
            // 
            this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pg.AutoSize = true;
            this.pg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pg.ForeColor = System.Drawing.Color.Cornsilk;
            this.pg.Location = new System.Drawing.Point(242, 816);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(31, 16);
            this.pg.TabIndex = 18;
            this.pg.Text = "0 / 0";
            // 
            // GalArea
            // 
            this.GalArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GalArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GalArea.Location = new System.Drawing.Point(0, 24);
            this.GalArea.Name = "GalArea";
            this.GalArea.Size = new System.Drawing.Size(533, 690);
            this.GalArea.TabIndex = 20;
            this.GalArea.Visible = false;
            this.GalArea.Paint += new System.Windows.Forms.PaintEventHandler(this.GalArea_Paint);
            this.GalArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);
            // 
            // GalGBox
            // 
            this.GalGBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.GalGBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GalGBox.Enabled = false;
            this.GalGBox.Location = new System.Drawing.Point(936, 538);
            this.GalGBox.Name = "GalGBox";
            this.GalGBox.Size = new System.Drawing.Size(642, 176);
            this.GalGBox.TabIndex = 21;
            this.GalGBox.TabStop = false;
            this.GalGBox.Enter += new System.EventHandler(this.GalGBox_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(672, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PauseButton.Location = new System.Drawing.Point(1480, 719);
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
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.Location = new System.Drawing.Point(1428, 720);
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
            this.TimeFrameBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeFrameBar.LargeChange = 15;
            this.TimeFrameBar.Location = new System.Drawing.Point(5, 24);
            this.TimeFrameBar.Maximum = 120;
            this.TimeFrameBar.Name = "TimeFrameBar";
            this.TimeFrameBar.Size = new System.Drawing.Size(191, 45);
            this.TimeFrameBar.TabIndex = 27;
            this.TimeFrameBar.TickFrequency = 15;
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
            this.TimeFramePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeFramePanel.Controls.Add(this.TF90s);
            this.TimeFramePanel.Controls.Add(this.TF30s);
            this.TimeFramePanel.Controls.Add(this.TFLabel);
            this.TimeFramePanel.Controls.Add(this.TF120s);
            this.TimeFramePanel.Controls.Add(this.TF60s);
            this.TimeFramePanel.Controls.Add(this.TimeFrameLabel);
            this.TimeFramePanel.Controls.Add(this.TF1s);
            this.TimeFramePanel.Controls.Add(this.TimeFrameBar);
            this.TimeFramePanel.Location = new System.Drawing.Point(342, 780);
            this.TimeFramePanel.Name = "TimeFramePanel";
            this.TimeFramePanel.Size = new System.Drawing.Size(240, 72);
            this.TimeFramePanel.TabIndex = 29;
            // 
            // TF90s
            // 
            this.TF90s.AutoSize = true;
            this.TF90s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF90s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF90s.Location = new System.Drawing.Point(132, 52);
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
            this.TF30s.Location = new System.Drawing.Point(50, 52);
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
            this.TFLabel.Size = new System.Drawing.Size(21, 16);
            this.TFLabel.TabIndex = 31;
            this.TFLabel.Text = "30";
            // 
            // TF120s
            // 
            this.TF120s.AutoSize = true;
            this.TF120s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TF120s.ForeColor = System.Drawing.SystemColors.Control;
            this.TF120s.Location = new System.Drawing.Point(170, 53);
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
            this.TF60s.Location = new System.Drawing.Point(92, 52);
            this.TF60s.Name = "TF60s";
            this.TF60s.Size = new System.Drawing.Size(19, 13);
            this.TF60s.TabIndex = 29;
            this.TF60s.Text = "60";
            this.TF60s.Click += new System.EventHandler(this.TFs_Click);
            // 
            // NotesSection
            // 
            this.NotesSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesSection.BackColor = System.Drawing.SystemColors.Control;
            this.NotesSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesSection.Location = new System.Drawing.Point(87, 8);
            this.NotesSection.Name = "NotesSection";
            this.NotesSection.Size = new System.Drawing.Size(439, 125);
            this.NotesSection.TabIndex = 30;
            this.NotesSection.Text = " ";
            // 
            // ANL
            // 
            this.ANL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ANL.AutoSize = true;
            this.ANL.BackColor = System.Drawing.SystemColors.Control;
            this.ANL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ANL.Location = new System.Drawing.Point(3, 11);
            this.ANL.Name = "ANL";
            this.ANL.Size = new System.Drawing.Size(52, 18);
            this.ANL.TabIndex = 31;
            this.ANL.Text = "Name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.EditNotesButton);
            this.panel1.Controls.Add(this.RacineLabel);
            this.panel1.Controls.Add(this.RSL);
            this.panel1.Controls.Add(this.NotesSection);
            this.panel1.Controls.Add(this.ANLabel);
            this.panel1.Controls.Add(this.ANL);
            this.panel1.Location = new System.Drawing.Point(691, 717);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 153);
            this.panel1.TabIndex = 32;
            // 
            // EditNotesButton
            // 
            this.EditNotesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditNotesButton.AutoSize = true;
            this.EditNotesButton.Enabled = false;
            this.EditNotesButton.Location = new System.Drawing.Point(532, 11);
            this.EditNotesButton.Name = "EditNotesButton";
            this.EditNotesButton.Size = new System.Drawing.Size(36, 23);
            this.EditNotesButton.TabIndex = 35;
            this.EditNotesButton.Text = "Edit";
            this.EditNotesButton.UseVisualStyleBackColor = true;
            this.EditNotesButton.Click += new System.EventHandler(this.EditNotesButton_Click);
            // 
            // RacineLabel
            // 
            this.RacineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RacineLabel.BackColor = System.Drawing.SystemColors.Control;
            this.RacineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RacineLabel.Location = new System.Drawing.Point(3, 97);
            this.RacineLabel.Name = "RacineLabel";
            this.RacineLabel.Size = new System.Drawing.Size(66, 18);
            this.RacineLabel.TabIndex = 34;
            // 
            // RSL
            // 
            this.RSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RSL.AutoSize = true;
            this.RSL.BackColor = System.Drawing.SystemColors.Control;
            this.RSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RSL.Location = new System.Drawing.Point(3, 71);
            this.RSL.Name = "RSL";
            this.RSL.Size = new System.Drawing.Size(58, 18);
            this.RSL.TabIndex = 33;
            this.RSL.Text = "Racine:";
            // 
            // ANLabel
            // 
            this.ANLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ANLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ANLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ANLabel.Location = new System.Drawing.Point(3, 37);
            this.ANLabel.Name = "ANLabel";
            this.ANLabel.Size = new System.Drawing.Size(66, 18);
            this.ANLabel.TabIndex = 32;
            // 
            // PMEEGView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TimeFramePanel);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pg);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.ZoomLabel);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.myVLC);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GalArea);
            this.Controls.Add(this.GalGBox);
            this.Controls.Add(this.GVGrouping);
            this.Controls.Add(this.BottomLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1600, 900);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "PMEEGView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEG View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PMEEGView_Close);
            this.Load += new System.EventHandler(this.PMEEGView_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PMEEGView_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeFrameBar)).EndInit();
            this.TimeFramePanel.ResumeLayout(false);
            this.TimeFramePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Previous;
        private System.Windows.Forms.Label TimeFrameLabel;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.Label BottomLabel;
        private System.Windows.Forms.ToolStripMenuItem videoSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HighRes;
        private System.Windows.Forms.ToolStripMenuItem LowRes;
        private System.Windows.Forms.Label GVGrouping;
        private System.Windows.Forms.ToolStripMenuItem telemetryToolStripMenuItem1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Label pg;
        private System.Windows.Forms.Panel GalArea;
        private System.Windows.Forms.GroupBox GalGBox;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.Label ANL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ANLabel;
        private System.Windows.Forms.Label RacineLabel;
        private System.Windows.Forms.Label RSL;
        private System.Windows.Forms.Button EditNotesButton;
    }
}