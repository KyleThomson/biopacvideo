
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
            this.label1 = new System.Windows.Forms.Label();
            this.AnimalViewPanel = new System.Windows.Forms.Panel();
            this.aLComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TotalSLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.S1Label = new System.Windows.Forms.Label();
            this.S0Label = new System.Windows.Forms.Label();
            this.S2Label = new System.Windows.Forms.Label();
            this.S3Label = new System.Windows.Forms.Label();
            this.S4Label = new System.Windows.Forms.Label();
            this.S5Label = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeFrameBar)).BeginInit();
            this.TimeFramePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.AnimalViewPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZoomBar
            // 
            this.ZoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(47, 835);
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
            this.DefaultView.Size = new System.Drawing.Size(180, 22);
            this.DefaultView.Text = "Default";
            this.DefaultView.Click += new System.EventHandler(this.normalListToolStripMenuItem_Click);
            // 
            // galleryView
            // 
            this.galleryView.CheckOnClick = true;
            this.galleryView.Name = "galleryView";
            this.galleryView.Size = new System.Drawing.Size(180, 22);
            this.galleryView.Text = "Gallery";
            this.galleryView.Click += new System.EventHandler(this.normalListToolStripMenuItem_Click);
            // 
            // animalView
            // 
            this.animalView.CheckOnClick = true;
            this.animalView.Name = "animalView";
            this.animalView.Size = new System.Drawing.Size(180, 22);
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
            this.randomizedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.randomizedToolStripMenuItem.Text = "Randomized";
            // 
            // videoSizeToolStripMenuItem
            // 
            this.videoSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HighRes,
            this.LowRes});
            this.videoSizeToolStripMenuItem.Enabled = false;
            this.videoSizeToolStripMenuItem.Name = "videoSizeToolStripMenuItem";
            this.videoSizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.TimeLabel.Location = new System.Drawing.Point(1065, 747);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(87, 18);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "0:00 / 0:00";
            this.TimeLabel.Visible = false;
            // 
            // Next
            // 
            this.Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Next.Location = new System.Drawing.Point(132, 743);
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
            this.Previous.Location = new System.Drawing.Point(51, 743);
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
            this.ZoomLabel.Location = new System.Drawing.Point(106, 815);
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
            this.vScrollBar1.Location = new System.Drawing.Point(21, 724);
            this.vScrollBar1.Minimum = 4;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(13, 128);
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
            this.pg.Location = new System.Drawing.Point(132, 777);
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
            this.PauseButton.Location = new System.Drawing.Point(1109, 719);
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
            this.PlayButton.Location = new System.Drawing.Point(1061, 719);
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
            this.TimeFramePanel.Location = new System.Drawing.Point(226, 762);
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
            this.panel1.Location = new System.Drawing.Point(472, 717);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(83, 777);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Page:";
            // 
            // AnimalViewPanel
            // 
            this.AnimalViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimalViewPanel.Controls.Add(this.label6);
            this.AnimalViewPanel.Controls.Add(this.label3);
            this.AnimalViewPanel.Controls.Add(this.tableLayoutPanel1);
            this.AnimalViewPanel.Controls.Add(this.label2);
            this.AnimalViewPanel.Controls.Add(this.aLComboBox);
            this.AnimalViewPanel.Location = new System.Drawing.Point(1161, 719);
            this.AnimalViewPanel.Name = "AnimalViewPanel";
            this.AnimalViewPanel.Size = new System.Drawing.Size(417, 137);
            this.AnimalViewPanel.TabIndex = 34;
            this.AnimalViewPanel.Visible = false;
            // 
            // aLComboBox
            // 
            this.aLComboBox.FormattingEnabled = true;
            this.aLComboBox.Location = new System.Drawing.Point(0, 26);
            this.aLComboBox.Name = "aLComboBox";
            this.aLComboBox.Size = new System.Drawing.Size(121, 21);
            this.aLComboBox.TabIndex = 0;
            this.aLComboBox.SelectedIndexChanged += new System.EventHandler(this.aLComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 32;
            this.label2.Text = "Animal:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.96637F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.20176F));
            this.tableLayoutPanel1.Controls.Add(this.S1Label, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.TotalSLabel, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.S0Label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.S2Label, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.S3Label, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.S4Label, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.S5Label, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label18, 6, 1);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(185, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(232, 40);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(126, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Seizures";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(36, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(68, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 35;
            this.label5.Text = "2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(136, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 16);
            this.label6.TabIndex = 34;
            this.label6.Text = "Stage";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(100, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(132, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 37;
            this.label8.Text = "4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(164, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 16);
            this.label9.TabIndex = 38;
            this.label9.Text = "5";
            // 
            // TotalSLabel
            // 
            this.TotalSLabel.AutoSize = true;
            this.TotalSLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalSLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TotalSLabel.Location = new System.Drawing.Point(196, 1);
            this.TotalSLabel.Name = "TotalSLabel";
            this.TotalSLabel.Size = new System.Drawing.Size(31, 13);
            this.TotalSLabel.TabIndex = 39;
            this.TotalSLabel.Text = "Total";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(4, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 15);
            this.label11.TabIndex = 40;
            this.label11.Text = "NC";
            // 
            // S1Label
            // 
            this.S1Label.AutoSize = true;
            this.S1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S1Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S1Label.Location = new System.Drawing.Point(36, 20);
            this.S1Label.Name = "S1Label";
            this.S1Label.Size = new System.Drawing.Size(14, 16);
            this.S1Label.TabIndex = 41;
            this.S1Label.Text = "0";
            // 
            // S0Label
            // 
            this.S0Label.AutoSize = true;
            this.S0Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S0Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S0Label.Location = new System.Drawing.Point(4, 20);
            this.S0Label.Name = "S0Label";
            this.S0Label.Size = new System.Drawing.Size(14, 16);
            this.S0Label.TabIndex = 42;
            this.S0Label.Text = "0";
            // 
            // S2Label
            // 
            this.S2Label.AutoSize = true;
            this.S2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S2Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S2Label.Location = new System.Drawing.Point(68, 20);
            this.S2Label.Name = "S2Label";
            this.S2Label.Size = new System.Drawing.Size(14, 16);
            this.S2Label.TabIndex = 43;
            this.S2Label.Text = "0";
            // 
            // S3Label
            // 
            this.S3Label.AutoSize = true;
            this.S3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S3Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S3Label.Location = new System.Drawing.Point(100, 20);
            this.S3Label.Name = "S3Label";
            this.S3Label.Size = new System.Drawing.Size(14, 16);
            this.S3Label.TabIndex = 44;
            this.S3Label.Text = "0";
            // 
            // S4Label
            // 
            this.S4Label.AutoSize = true;
            this.S4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S4Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S4Label.Location = new System.Drawing.Point(132, 20);
            this.S4Label.Name = "S4Label";
            this.S4Label.Size = new System.Drawing.Size(14, 16);
            this.S4Label.TabIndex = 45;
            this.S4Label.Text = "0";
            // 
            // S5Label
            // 
            this.S5Label.AutoSize = true;
            this.S5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.S5Label.ForeColor = System.Drawing.SystemColors.Control;
            this.S5Label.Location = new System.Drawing.Point(164, 20);
            this.S5Label.Name = "S5Label";
            this.S5Label.Size = new System.Drawing.Size(14, 16);
            this.S5Label.TabIndex = 46;
            this.S5Label.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.Control;
            this.label18.Location = new System.Drawing.Point(196, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 16);
            this.label18.TabIndex = 47;
            this.label18.Text = "0";
            // 
            // PMEEGView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.AnimalViewPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
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
            this.AnimalViewPanel.ResumeLayout(false);
            this.AnimalViewPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel AnimalViewPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label S4Label;
        private System.Windows.Forms.Label S3Label;
        private System.Windows.Forms.Label S2Label;
        private System.Windows.Forms.Label S0Label;
        private System.Windows.Forms.Label S1Label;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label TotalSLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox aLComboBox;
        private System.Windows.Forms.Label S5Label;
        private System.Windows.Forms.Label label18;
    }
}