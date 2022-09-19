
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
            this.NPrev = new System.Windows.Forms.Button();
            this.NNext = new System.Windows.Forms.Button();
            this.TFSelect = new System.Windows.Forms.ComboBox();
            this.NextIList = new System.Windows.Forms.ImageList(this.components);
            this.PrevIList = new System.Windows.Forms.ImageList(this.components);
            this.Video = new LibVLCSharp.WinForms.VideoView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultView = new System.Windows.Forms.ToolStripMenuItem();
            this.galleryView = new System.Windows.Forms.ToolStripMenuItem();
            this.animalView = new System.Windows.Forms.ToolStripMenuItem();
            this.moreOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.telemetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PausePlayList = new System.Windows.Forms.ImageList(this.components);
            this.PlayPauseButton = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ZoomBar
            // 
            this.ZoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(469, 549);
            this.ZoomBar.Maximum = 20;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(121, 21);
            this.ZoomBar.TabIndex = 4;
            this.ZoomBar.TickFrequency = 2;
            this.ZoomBar.Value = 10;
            // 
            // NPrev
            // 
            this.NPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NPrev.BackColor = System.Drawing.Color.Black;
            this.NPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NPrev.BackgroundImage")));
            this.NPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NPrev.FlatAppearance.BorderSize = 0;
            this.NPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.NPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.NPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NPrev.Location = new System.Drawing.Point(316, 520);
            this.NPrev.Margin = new System.Windows.Forms.Padding(0);
            this.NPrev.Name = "NPrev";
            this.NPrev.Size = new System.Drawing.Size(70, 49);
            this.NPrev.TabIndex = 3;
            this.NPrev.UseVisualStyleBackColor = false;
            this.NPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NPrev_MouseDown);
            this.NPrev.MouseLeave += new System.EventHandler(this.NPrev_MouseLeave);
            this.NPrev.MouseHover += new System.EventHandler(this.NPrev_MouseHover);
            this.NPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NPrev_MouseUp);
            // 
            // NNext
            // 
            this.NNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NNext.BackColor = System.Drawing.Color.Black;
            this.NNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NNext.BackgroundImage")));
            this.NNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NNext.FlatAppearance.BorderSize = 0;
            this.NNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.NNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.NNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NNext.Location = new System.Drawing.Point(393, 521);
            this.NNext.Margin = new System.Windows.Forms.Padding(0);
            this.NNext.Name = "NNext";
            this.NNext.Size = new System.Drawing.Size(70, 46);
            this.NNext.TabIndex = 2;
            this.NNext.UseVisualStyleBackColor = false;
            this.NNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NNext_MouseDown);
            this.NNext.MouseLeave += new System.EventHandler(this.NNext_MouseLeave);
            this.NNext.MouseHover += new System.EventHandler(this.NNext_MouseHover);
            this.NNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NNext_MouseUp);
            // 
            // TFSelect
            // 
            this.TFSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TFSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TFSelect.FormattingEnabled = true;
            this.TFSelect.Items.AddRange(new object[] {
            "30s",
            "60s",
            "2m",
            "5m",
            "10m"});
            this.TFSelect.Location = new System.Drawing.Point(183, 548);
            this.TFSelect.Name = "TFSelect";
            this.TFSelect.Size = new System.Drawing.Size(121, 21);
            this.TFSelect.TabIndex = 2;
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
            // Video
            // 
            this.Video.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Video.BackColor = System.Drawing.Color.White;
            this.Video.Enabled = false;
            this.Video.Location = new System.Drawing.Point(27, 27);
            this.Video.MediaPlayer = null;
            this.Video.Name = "Video";
            this.Video.Size = new System.Drawing.Size(458, 308);
            this.Video.TabIndex = 2;
            this.Video.Text = "videoView1";
            this.Video.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.moreOptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "PMEEGMenu";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
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
            this.telemetryToolStripMenuItem});
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
            // telemetryToolStripMenuItem
            // 
            this.telemetryToolStripMenuItem.CheckOnClick = true;
            this.telemetryToolStripMenuItem.Name = "telemetryToolStripMenuItem";
            this.telemetryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.telemetryToolStripMenuItem.Text = "Telemetry";
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
            this.PlayPauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayPauseButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayPauseButton.BackgroundImage")));
            this.PlayPauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayPauseButton.FlatAppearance.BorderSize = 0;
            this.PlayPauseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PlayPauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayPauseButton.Location = new System.Drawing.Point(36, 500);
            this.PlayPauseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.Size = new System.Drawing.Size(61, 52);
            this.PlayPauseButton.TabIndex = 6;
            this.PlayPauseButton.UseVisualStyleBackColor = false;
            this.PlayPauseButton.Click += new System.EventHandler(this.PlayPauseButton_Click);
            this.PlayPauseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayPauseButton_MouseDown);
            this.PlayPauseButton.MouseLeave += new System.EventHandler(this.PlayPauseButton_MouseLeave);
            this.PlayPauseButton.MouseHover += new System.EventHandler(this.PlayPauseButton_MouseHover);
            this.PlayPauseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlayPauseButton_MouseHover);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeLabel.Location = new System.Drawing.Point(100, 530);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(60, 24);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "00:00";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(27, 472);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(458, 25);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // PMEEGView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(817, 573);
            this.Controls.Add(this.PlayPauseButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.NNext);
            this.Controls.Add(this.NPrev);
            this.Controls.Add(this.TFSelect);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.Video);
            this.Controls.Add(this.menuStrip1);
            this.Name = "PMEEGView";
            this.Text = "EEG View";
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox TFSelect;
        private System.Windows.Forms.Button NNext;
        public System.Windows.Forms.ImageList NextIList;
        public System.Windows.Forms.ImageList PrevIList;
        private System.Windows.Forms.Button NPrev;
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
        public LibVLCSharp.WinForms.VideoView Video;
        private System.Windows.Forms.ImageList PausePlayList;
        private System.Windows.Forms.Button PlayPauseButton;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label TimeLabel;
    }
}