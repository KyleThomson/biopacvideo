
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMEEGView));
            this.ZoomBar = new System.Windows.Forms.TrackBar();
            this.TFSelect = new System.Windows.Forms.ComboBox();
            this.NextIList = new System.Windows.Forms.ImageList(this.components);
            this.PrevIList = new System.Windows.Forms.ImageList(this.components);
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
            this.telemetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HighRes = new System.Windows.Forms.ToolStripMenuItem();
            this.LowRes = new System.Windows.Forms.ToolStripMenuItem();
            this.PausePlayList = new System.Windows.Forms.ImageList(this.components);
            this.PlayPauseButton = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.Next = new System.Windows.Forms.Button();
            this.Previous = new System.Windows.Forms.Button();
            this.TimeFrameLabel = new System.Windows.Forms.Label();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.BottomLabel = new System.Windows.Forms.Label();
            this.GVGrouping = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.pg = new System.Windows.Forms.Label();
            this.GalArea = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ZoomBar
            // 
            this.ZoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(45, 711);
            this.ZoomBar.Maximum = 20;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(128, 21);
            this.ZoomBar.TabIndex = 4;
            this.ZoomBar.TickFrequency = 2;
            this.ZoomBar.Value = 10;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // TFSelect
            // 
            this.TFSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TFSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TFSelect.FormattingEnabled = true;
            this.TFSelect.Items.AddRange(new object[] {
            "10s",
            "15s",
            "30s",
            "60s",
            "2m",
            "5m",
            "10m"});
            this.TFSelect.Location = new System.Drawing.Point(349, 710);
            this.TFSelect.Name = "TFSelect";
            this.TFSelect.Size = new System.Drawing.Size(128, 21);
            this.TFSelect.TabIndex = 2;
            this.TFSelect.SelectedIndexChanged += new System.EventHandler(this.TFSelect_SelectedIndexChanged);
            // 
            // NextIList
            // 
            this.NextIList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NextIList.ImageStream")));
            this.NextIList.TransparentColor = System.Drawing.Color.Black;
            this.NextIList.Images.SetKeyName(0, "NextFinal.png");
            this.NextIList.Images.SetKeyName(1, "nextclicked.png");
            this.NextIList.Images.SetKeyName(2, "NextMO.png");
            // 
            // PrevIList
            // 
            this.PrevIList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PrevIList.ImageStream")));
            this.PrevIList.TransparentColor = System.Drawing.Color.Black;
            this.PrevIList.Images.SetKeyName(0, "prevFinal.png");
            this.PrevIList.Images.SetKeyName(1, "prevclicked.png");
            this.PrevIList.Images.SetKeyName(2, "prevMO.png");
            // 
            // myVLC
            // 
            this.myVLC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myVLC.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.myVLC.Enabled = false;
            this.myVLC.Location = new System.Drawing.Point(539, 29);
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
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "PMEEGMenu";
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
            this.telemetryToolStripMenuItem,
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
            // telemetryToolStripMenuItem
            // 
            this.telemetryToolStripMenuItem.CheckOnClick = true;
            this.telemetryToolStripMenuItem.Name = "telemetryToolStripMenuItem";
            this.telemetryToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.telemetryToolStripMenuItem.Text = "Telemetry";
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
            // PausePlayList
            // 
            this.PausePlayList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PausePlayList.ImageStream")));
            this.PausePlayList.TransparentColor = System.Drawing.Color.Transparent;
            this.PausePlayList.Images.SetKeyName(0, "Play.png");
            this.PausePlayList.Images.SetKeyName(1, "PlayHover.png");
            this.PausePlayList.Images.SetKeyName(2, "PlayPressed.png");
            this.PausePlayList.Images.SetKeyName(3, "Pause.png");
            this.PausePlayList.Images.SetKeyName(4, "PauseHover.png");
            this.PausePlayList.Images.SetKeyName(5, "PauseClick.png");
            // 
            // PlayPauseButton
            // 
            this.PlayPauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayPauseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayPauseButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayPauseButton.BackgroundImage")));
            this.PlayPauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayPauseButton.Enabled = false;
            this.PlayPauseButton.FlatAppearance.BorderSize = 0;
            this.PlayPauseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayPauseButton.Location = new System.Drawing.Point(536, 712);
            this.PlayPauseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.Size = new System.Drawing.Size(40, 40);
            this.PlayPauseButton.TabIndex = 6;
            this.PlayPauseButton.UseVisualStyleBackColor = false;
            this.PlayPauseButton.Visible = false;
            this.PlayPauseButton.Click += new System.EventHandler(this.PlayPauseButton_Click);
            this.PlayPauseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayPauseButton_MouseDown);
            this.PlayPauseButton.MouseLeave += new System.EventHandler(this.PlayPauseButton_MouseLeave);
            this.PlayPauseButton.MouseHover += new System.EventHandler(this.PlayPauseButton_MouseHover);
            this.PlayPauseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlayPauseButton_MouseHover);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Enabled = false;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeLabel.Location = new System.Drawing.Point(579, 721);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(54, 20);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "00:00";
            this.TimeLabel.Visible = false;
            // 
            // TimeBar
            // 
            this.TimeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeBar.AutoSize = false;
            this.TimeBar.Enabled = false;
            this.TimeBar.Location = new System.Drawing.Point(533, 678);
            this.TimeBar.Maximum = 200;
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(650, 25);
            this.TimeBar.TabIndex = 8;
            this.TimeBar.TickFrequency = 5;
            this.TimeBar.Visible = false;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // Next
            // 
            this.Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Next.Location = new System.Drawing.Point(261, 690);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 9;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Previous
            // 
            this.Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Previous.Location = new System.Drawing.Point(180, 690);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(75, 23);
            this.Previous.TabIndex = 10;
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
            this.TimeFrameLabel.Location = new System.Drawing.Point(370, 689);
            this.TimeFrameLabel.Name = "TimeFrameLabel";
            this.TimeFrameLabel.Size = new System.Drawing.Size(88, 18);
            this.TimeFrameLabel.TabIndex = 11;
            this.TimeFrameLabel.Text = "Time Frame";
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomLabel.AutoSize = true;
            this.ZoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.ZoomLabel.Location = new System.Drawing.Point(86, 690);
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
            this.BottomLabel.Location = new System.Drawing.Point(-3, 672);
            this.BottomLabel.Name = "BottomLabel";
            this.BottomLabel.Size = new System.Drawing.Size(1195, 107);
            this.BottomLabel.TabIndex = 13;
            // 
            // GVGrouping
            // 
            this.GVGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVGrouping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GVGrouping.Location = new System.Drawing.Point(531, 24);
            this.GVGrouping.Name = "GVGrouping";
            this.GVGrouping.Size = new System.Drawing.Size(653, 755);
            this.GVGrouping.TabIndex = 15;
            this.GVGrouping.Text = "label2";
            this.GVGrouping.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1110, 548);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(9, 678);
            this.vScrollBar1.Minimum = 4;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 80);
            this.vScrollBar1.TabIndex = 17;
            this.vScrollBar1.Value = 100;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // pg
            // 
            this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pg.AutoSize = true;
            this.pg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pg.ForeColor = System.Drawing.Color.Cornsilk;
            this.pg.Location = new System.Drawing.Point(242, 716);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(32, 16);
            this.pg.TabIndex = 18;
            this.pg.Text = "0 / 0";
            // 
            // GalArea
            // 
            this.GalArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GalArea.Location = new System.Drawing.Point(0, 24);
            this.GalArea.Name = "GalArea";
            this.GalArea.Size = new System.Drawing.Size(533, 648);
            this.GalArea.TabIndex = 20;
            this.GalArea.Visible = false;
            this.GalArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GalArea_MouseDown);
            // 
            // PMEEGView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.pg);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.PlayPauseButton);
            this.Controls.Add(this.TimeBar);
            this.Controls.Add(this.ZoomLabel);
            this.Controls.Add(this.TimeFrameLabel);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.TFSelect);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.myVLC);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GVGrouping);
            this.Controls.Add(this.BottomLabel);
            this.Controls.Add(this.GalArea);
            this.MinimumSize = new System.Drawing.Size(1040, 600);
            this.Name = "PMEEGView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEG View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PMEEGView_Close);
            this.Load += new System.EventHandler(this.PMEEGView_Load);
            this.ResizeBegin += new System.EventHandler(this.PMEEGView_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.PMEEGView_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox TFSelect;
        public System.Windows.Forms.ImageList NextIList;
        public System.Windows.Forms.ImageList PrevIList;
        private System.Windows.Forms.TrackBar ZoomBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem galleryView;
        public System.Windows.Forms.ToolStripMenuItem animalView;
        private System.Windows.Forms.ToolStripMenuItem moreOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DefaultView;
        private System.Windows.Forms.ToolStripMenuItem telemetryToolStripMenuItem;
        public LibVLCSharp.WinForms.VideoView myVLC;
        private System.Windows.Forms.ImageList PausePlayList;
        private System.Windows.Forms.Button PlayPauseButton;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Previous;
        private System.Windows.Forms.Label TimeFrameLabel;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.Label BottomLabel;
        public System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.ToolStripMenuItem videoSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HighRes;
        private System.Windows.Forms.ToolStripMenuItem LowRes;
        private System.Windows.Forms.Label GVGrouping;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem telemetryToolStripMenuItem1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Label pg;
        private System.Windows.Forms.Panel GalArea;
    }
}