namespace SeizurePlayback
{
    partial class CManage
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
            this.Play = new System.Windows.Forms.Button();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.Open = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.Rewind = new System.Windows.Forms.Button();
            this.SpeedUp = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.TimeBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SzCaptureButton = new System.Windows.Forms.Button();
            this.DetectionLoadButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeJump = new System.Windows.Forms.TextBox();
            this.VisChan1 = new System.Windows.Forms.CheckBox();
            this.VisChan2 = new System.Windows.Forms.CheckBox();
            this.VisChan3 = new System.Windows.Forms.CheckBox();
            this.VisChan4 = new System.Windows.Forms.CheckBox();
            this.VisChan5 = new System.Windows.Forms.CheckBox();
            this.VisChan6 = new System.Windows.Forms.CheckBox();
            this.VisChan7 = new System.Windows.Forms.CheckBox();
            this.VisChan8 = new System.Windows.Forms.CheckBox();
            this.VisChan16 = new System.Windows.Forms.CheckBox();
            this.VisChan15 = new System.Windows.Forms.CheckBox();
            this.VisChan14 = new System.Windows.Forms.CheckBox();
            this.VisChan13 = new System.Windows.Forms.CheckBox();
            this.VisChan12 = new System.Windows.Forms.CheckBox();
            this.VisChan11 = new System.Windows.Forms.CheckBox();
            this.VisChan10 = new System.Windows.Forms.CheckBox();
            this.VisChan9 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ZoomScale = new System.Windows.Forms.TrackBar();
            this.RvwSz = new System.Windows.Forms.Button();
            this.OffsetLabel = new System.Windows.Forms.Label();
            this.OffsetBox = new System.Windows.Forms.TextBox();
            this.HighlightLabel = new System.Windows.Forms.Label();
            this.DetSezLabel = new System.Windows.Forms.Label();
            this.Randomization = new System.Windows.Forms.CheckBox();
            this.AVILoadBar = new System.Windows.Forms.ProgressBar();
            this.LoadText = new System.Windows.Forms.Label();
            this.FastReview = new System.Windows.Forms.Button();
            this.TelemetryBox = new System.Windows.Forms.CheckBox();
            this.VideoFix = new System.Windows.Forms.CheckBox();
            this.SwitchChan = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FastReviewFR = new System.Windows.Forms.Button();
            this.PreviousFR = new System.Windows.Forms.Button();
            this.NextFR = new System.Windows.Forms.Button();
            this.FRButtonGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.numPerBox = new System.Windows.Forms.ComboBox();
            this.FRZoomBar = new System.Windows.Forms.TrackBar();
            this.FRZoomBlock = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.FRPageNum = new System.Windows.Forms.Label();
            this.LoadAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomScale)).BeginInit();
            this.FRButtonGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FRZoomBar)).BeginInit();
            this.FRZoomBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Play.BackColor = System.Drawing.Color.DarkGray;
            this.Play.ForeColor = System.Drawing.Color.Black;
            this.Play.Location = new System.Drawing.Point(832, 536);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 0;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // VideoPanel
            // 
            this.VideoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VideoPanel.BackColor = System.Drawing.Color.DarkGray;
            this.VideoPanel.Location = new System.Drawing.Point(12, 473);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(480, 360);
            this.VideoPanel.TabIndex = 2;
            this.VideoPanel.Click += new System.EventHandler(this.VideoPanel_Click);
            // 
            // Open
            // 
            this.Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Open.BackColor = System.Drawing.Color.DarkGray;
            this.Open.ForeColor = System.Drawing.Color.Black;
            this.Open.Location = new System.Drawing.Point(670, 507);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(156, 23);
            this.Open.TabIndex = 3;
            this.Open.Text = "Select Directory";
            this.Open.UseVisualStyleBackColor = false;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Pause
            // 
            this.Pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Pause.BackColor = System.Drawing.Color.DarkGray;
            this.Pause.ForeColor = System.Drawing.Color.Black;
            this.Pause.Location = new System.Drawing.Point(832, 507);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 23);
            this.Pause.TabIndex = 0;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = false;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // TimeBar
            // 
            this.TimeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeBar.LargeChange = 1;
            this.TimeBar.Location = new System.Drawing.Point(498, 695);
            this.TimeBar.Maximum = 9;
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(924, 45);
            this.TimeBar.TabIndex = 13;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // Rewind
            // 
            this.Rewind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Rewind.BackColor = System.Drawing.Color.DarkGray;
            this.Rewind.ForeColor = System.Drawing.Color.Black;
            this.Rewind.Location = new System.Drawing.Point(913, 507);
            this.Rewind.Name = "Rewind";
            this.Rewind.Size = new System.Drawing.Size(75, 23);
            this.Rewind.TabIndex = 15;
            this.Rewind.Text = "Rewind 30s";
            this.Rewind.UseVisualStyleBackColor = false;
            this.Rewind.Click += new System.EventHandler(this.Rewind_Click);
            // 
            // SpeedUp
            // 
            this.SpeedUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpeedUp.BackColor = System.Drawing.Color.DarkGray;
            this.SpeedUp.ForeColor = System.Drawing.Color.Black;
            this.SpeedUp.Location = new System.Drawing.Point(913, 536);
            this.SpeedUp.Name = "SpeedUp";
            this.SpeedUp.Size = new System.Drawing.Size(75, 23);
            this.SpeedUp.TabIndex = 16;
            this.SpeedUp.Text = "Speed Up";
            this.SpeedUp.UseVisualStyleBackColor = false;
            this.SpeedUp.Click += new System.EventHandler(this.SpeedUp_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLabel.Location = new System.Drawing.Point(532, 476);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TimeLabel.Size = new System.Drawing.Size(127, 24);
            this.TimeLabel.TabIndex = 17;
            this.TimeLabel.Text = "        Time";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TimeBox
            // 
            this.TimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeBox.FormattingEnabled = true;
            this.TimeBox.Items.AddRange(new object[] {
            "30s",
            "60s",
            "2m",
            "5m",
            "10m",
            "30m ",
            "60m"});
            this.TimeBox.Location = new System.Drawing.Point(498, 652);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(161, 21);
            this.TimeBox.TabIndex = 18;
            this.TimeBox.SelectedIndexChanged += new System.EventHandler(this.TimeBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(500, 625);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "Time Scale";
            // 
            // SzCaptureButton
            // 
            this.SzCaptureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SzCaptureButton.BackColor = System.Drawing.Color.DarkGray;
            this.SzCaptureButton.ForeColor = System.Drawing.Color.Black;
            this.SzCaptureButton.Location = new System.Drawing.Point(669, 566);
            this.SzCaptureButton.Name = "SzCaptureButton";
            this.SzCaptureButton.Size = new System.Drawing.Size(156, 23);
            this.SzCaptureButton.TabIndex = 20;
            this.SzCaptureButton.Text = "Capture Seizure";
            this.SzCaptureButton.UseVisualStyleBackColor = false;
            this.SzCaptureButton.Click += new System.EventHandler(this.SzCaptureButton_Click);
            // 
            // DetectionLoadButton
            // 
            this.DetectionLoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DetectionLoadButton.BackColor = System.Drawing.Color.DarkGray;
            this.DetectionLoadButton.ForeColor = System.Drawing.Color.Black;
            this.DetectionLoadButton.Location = new System.Drawing.Point(669, 536);
            this.DetectionLoadButton.Name = "DetectionLoadButton";
            this.DetectionLoadButton.Size = new System.Drawing.Size(156, 23);
            this.DetectionLoadButton.TabIndex = 21;
            this.DetectionLoadButton.Text = "Load Detection File";
            this.DetectionLoadButton.UseVisualStyleBackColor = false;
            this.DetectionLoadButton.Click += new System.EventHandler(this.DetectionLoadButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(913, 595);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Next_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.Color.DarkGray;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(832, 595);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Previous";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Previous_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(672, 625);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "Time Jump";
            // 
            // TimeJump
            // 
            this.TimeJump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeJump.Location = new System.Drawing.Point(669, 652);
            this.TimeJump.Name = "TimeJump";
            this.TimeJump.Size = new System.Drawing.Size(161, 20);
            this.TimeJump.TabIndex = 25;
            this.TimeJump.LostFocus += new System.EventHandler(this.TimeJump_TextChanged);
            // 
            // VisChan1
            // 
            this.VisChan1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan1.AutoSize = true;
            this.VisChan1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan1.Location = new System.Drawing.Point(502, 743);
            this.VisChan1.Name = "VisChan1";
            this.VisChan1.Size = new System.Drawing.Size(74, 17);
            this.VisChan1.TabIndex = 26;
            this.VisChan1.Text = "Channel 1";
            this.VisChan1.UseVisualStyleBackColor = true;
            this.VisChan1.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan2
            // 
            this.VisChan2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan2.AutoSize = true;
            this.VisChan2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan2.Location = new System.Drawing.Point(503, 766);
            this.VisChan2.Name = "VisChan2";
            this.VisChan2.Size = new System.Drawing.Size(74, 17);
            this.VisChan2.TabIndex = 27;
            this.VisChan2.Text = "Channel 2";
            this.VisChan2.UseVisualStyleBackColor = true;
            this.VisChan2.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan3
            // 
            this.VisChan3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan3.AutoSize = true;
            this.VisChan3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan3.Location = new System.Drawing.Point(503, 789);
            this.VisChan3.Name = "VisChan3";
            this.VisChan3.Size = new System.Drawing.Size(74, 17);
            this.VisChan3.TabIndex = 28;
            this.VisChan3.Text = "Channel 3";
            this.VisChan3.UseVisualStyleBackColor = true;
            this.VisChan3.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan4
            // 
            this.VisChan4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan4.AutoSize = true;
            this.VisChan4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan4.Location = new System.Drawing.Point(503, 812);
            this.VisChan4.Name = "VisChan4";
            this.VisChan4.Size = new System.Drawing.Size(74, 17);
            this.VisChan4.TabIndex = 29;
            this.VisChan4.Text = "Channel 4";
            this.VisChan4.UseVisualStyleBackColor = true;
            this.VisChan4.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan5
            // 
            this.VisChan5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan5.AutoSize = true;
            this.VisChan5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan5.Location = new System.Drawing.Point(584, 743);
            this.VisChan5.Name = "VisChan5";
            this.VisChan5.Size = new System.Drawing.Size(74, 17);
            this.VisChan5.TabIndex = 30;
            this.VisChan5.Text = "Channel 5";
            this.VisChan5.UseVisualStyleBackColor = true;
            this.VisChan5.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan6
            // 
            this.VisChan6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan6.AutoSize = true;
            this.VisChan6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan6.Location = new System.Drawing.Point(583, 766);
            this.VisChan6.Name = "VisChan6";
            this.VisChan6.Size = new System.Drawing.Size(74, 17);
            this.VisChan6.TabIndex = 31;
            this.VisChan6.Text = "Channel 6";
            this.VisChan6.UseVisualStyleBackColor = true;
            this.VisChan6.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan7
            // 
            this.VisChan7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan7.AutoSize = true;
            this.VisChan7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan7.Location = new System.Drawing.Point(583, 789);
            this.VisChan7.Name = "VisChan7";
            this.VisChan7.Size = new System.Drawing.Size(74, 17);
            this.VisChan7.TabIndex = 32;
            this.VisChan7.Text = "Channel 7";
            this.VisChan7.UseVisualStyleBackColor = true;
            this.VisChan7.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan8
            // 
            this.VisChan8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan8.AutoSize = true;
            this.VisChan8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan8.Location = new System.Drawing.Point(583, 812);
            this.VisChan8.Name = "VisChan8";
            this.VisChan8.Size = new System.Drawing.Size(74, 17);
            this.VisChan8.TabIndex = 33;
            this.VisChan8.Text = "Channel 8";
            this.VisChan8.UseVisualStyleBackColor = true;
            this.VisChan8.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan16
            // 
            this.VisChan16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan16.AutoSize = true;
            this.VisChan16.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan16.Location = new System.Drawing.Point(745, 812);
            this.VisChan16.Name = "VisChan16";
            this.VisChan16.Size = new System.Drawing.Size(80, 17);
            this.VisChan16.TabIndex = 41;
            this.VisChan16.Text = "Channel 16";
            this.VisChan16.UseVisualStyleBackColor = true;
            this.VisChan16.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan15
            // 
            this.VisChan15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan15.AutoSize = true;
            this.VisChan15.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan15.Location = new System.Drawing.Point(745, 789);
            this.VisChan15.Name = "VisChan15";
            this.VisChan15.Size = new System.Drawing.Size(80, 17);
            this.VisChan15.TabIndex = 40;
            this.VisChan15.Text = "Channel 15";
            this.VisChan15.UseVisualStyleBackColor = true;
            this.VisChan15.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan14
            // 
            this.VisChan14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan14.AutoSize = true;
            this.VisChan14.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan14.Location = new System.Drawing.Point(745, 766);
            this.VisChan14.Name = "VisChan14";
            this.VisChan14.Size = new System.Drawing.Size(80, 17);
            this.VisChan14.TabIndex = 39;
            this.VisChan14.Text = "Channel 14";
            this.VisChan14.UseVisualStyleBackColor = true;
            this.VisChan14.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan13
            // 
            this.VisChan13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan13.AutoSize = true;
            this.VisChan13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan13.Location = new System.Drawing.Point(746, 743);
            this.VisChan13.Name = "VisChan13";
            this.VisChan13.Size = new System.Drawing.Size(80, 17);
            this.VisChan13.TabIndex = 38;
            this.VisChan13.Text = "Channel 13";
            this.VisChan13.UseVisualStyleBackColor = true;
            this.VisChan13.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan12
            // 
            this.VisChan12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan12.AutoSize = true;
            this.VisChan12.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan12.Location = new System.Drawing.Point(665, 812);
            this.VisChan12.Name = "VisChan12";
            this.VisChan12.Size = new System.Drawing.Size(80, 17);
            this.VisChan12.TabIndex = 37;
            this.VisChan12.Text = "Channel 12";
            this.VisChan12.UseVisualStyleBackColor = true;
            this.VisChan12.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan11
            // 
            this.VisChan11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan11.AutoSize = true;
            this.VisChan11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan11.Location = new System.Drawing.Point(665, 789);
            this.VisChan11.Name = "VisChan11";
            this.VisChan11.Size = new System.Drawing.Size(80, 17);
            this.VisChan11.TabIndex = 36;
            this.VisChan11.Text = "Channel 11";
            this.VisChan11.UseVisualStyleBackColor = true;
            this.VisChan11.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan10
            // 
            this.VisChan10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan10.AutoSize = true;
            this.VisChan10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan10.Location = new System.Drawing.Point(665, 766);
            this.VisChan10.Name = "VisChan10";
            this.VisChan10.Size = new System.Drawing.Size(80, 17);
            this.VisChan10.TabIndex = 35;
            this.VisChan10.Text = "Channel 10";
            this.VisChan10.UseVisualStyleBackColor = true;
            this.VisChan10.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // VisChan9
            // 
            this.VisChan9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisChan9.AutoSize = true;
            this.VisChan9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VisChan9.Location = new System.Drawing.Point(664, 743);
            this.VisChan9.Name = "VisChan9";
            this.VisChan9.Size = new System.Drawing.Size(74, 17);
            this.VisChan9.TabIndex = 34;
            this.VisChan9.Text = "Channel 9";
            this.VisChan9.UseVisualStyleBackColor = true;
            this.VisChan9.CheckedChanged += new System.EventHandler(this.VisChan_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(501, 562);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 24);
            this.label3.TabIndex = 42;
            this.label3.Text = "Zoom level";
            // 
            // ZoomScale
            // 
            this.ZoomScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomScale.Location = new System.Drawing.Point(499, 595);
            this.ZoomScale.Maximum = 20;
            this.ZoomScale.Minimum = 1;
            this.ZoomScale.Name = "ZoomScale";
            this.ZoomScale.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ZoomScale.Size = new System.Drawing.Size(171, 45);
            this.ZoomScale.TabIndex = 43;
            this.ZoomScale.TabStop = false;
            this.ZoomScale.Value = 10;
            this.ZoomScale.Scroll += new System.EventHandler(this.ZoomScale_Scroll);
            // 
            // RvwSz
            // 
            this.RvwSz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RvwSz.BackColor = System.Drawing.Color.DarkGray;
            this.RvwSz.ForeColor = System.Drawing.Color.Black;
            this.RvwSz.Location = new System.Drawing.Point(669, 595);
            this.RvwSz.Name = "RvwSz";
            this.RvwSz.Size = new System.Drawing.Size(156, 23);
            this.RvwSz.TabIndex = 45;
            this.RvwSz.Text = "Review Seizures";
            this.RvwSz.UseVisualStyleBackColor = false;
            this.RvwSz.Click += new System.EventHandler(this.RvwSz_Click);
            // 
            // OffsetLabel
            // 
            this.OffsetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OffsetLabel.AutoSize = true;
            this.OffsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OffsetLabel.ForeColor = System.Drawing.Color.White;
            this.OffsetLabel.Location = new System.Drawing.Point(847, 625);
            this.OffsetLabel.Name = "OffsetLabel";
            this.OffsetLabel.Size = new System.Drawing.Size(57, 24);
            this.OffsetLabel.TabIndex = 47;
            this.OffsetLabel.Text = "Offset";
            this.OffsetLabel.Visible = false;
            // 
            // OffsetBox
            // 
            this.OffsetBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OffsetBox.Location = new System.Drawing.Point(841, 652);
            this.OffsetBox.Name = "OffsetBox";
            this.OffsetBox.Size = new System.Drawing.Size(161, 20);
            this.OffsetBox.TabIndex = 48;
            this.OffsetBox.Visible = false;
            this.OffsetBox.TextChanged += new System.EventHandler(this.OffsetBox_TextChanged_1);
            this.OffsetBox.LostFocus += new System.EventHandler(this.OffsetBox_TextChanged);
            // 
            // HighlightLabel
            // 
            this.HighlightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HighlightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighlightLabel.ForeColor = System.Drawing.Color.White;
            this.HighlightLabel.Location = new System.Drawing.Point(530, 508);
            this.HighlightLabel.Name = "HighlightLabel";
            this.HighlightLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HighlightLabel.Size = new System.Drawing.Size(127, 24);
            this.HighlightLabel.TabIndex = 50;
            this.HighlightLabel.Text = "        HL";
            this.HighlightLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HighlightLabel.Click += new System.EventHandler(this.HighlightLabel_Click);
            // 
            // DetSezLabel
            // 
            this.DetSezLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DetSezLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetSezLabel.ForeColor = System.Drawing.Color.White;
            this.DetSezLabel.Location = new System.Drawing.Point(499, 536);
            this.DetSezLabel.Name = "DetSezLabel";
            this.DetSezLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DetSezLabel.Size = new System.Drawing.Size(159, 24);
            this.DetSezLabel.TabIndex = 56;
            this.DetSezLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.DetSezLabel.Click += new System.EventHandler(this.DetSezLabel_Click);
            // 
            // Randomization
            // 
            this.Randomization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Randomization.AutoSize = true;
            this.Randomization.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Randomization.Location = new System.Drawing.Point(1085, 656);
            this.Randomization.Name = "Randomization";
            this.Randomization.Size = new System.Drawing.Size(79, 17);
            this.Randomization.TabIndex = 57;
            this.Randomization.Text = "Randomize";
            this.Randomization.UseVisualStyleBackColor = true;
            this.Randomization.CheckedChanged += new System.EventHandler(this.Randomization_CheckedChanged);
            // 
            // AVILoadBar
            // 
            this.AVILoadBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AVILoadBar.Location = new System.Drawing.Point(861, 789);
            this.AVILoadBar.Maximum = 30;
            this.AVILoadBar.Name = "AVILoadBar";
            this.AVILoadBar.Size = new System.Drawing.Size(445, 25);
            this.AVILoadBar.Step = 1;
            this.AVILoadBar.TabIndex = 58;
            this.AVILoadBar.Visible = false;
            // 
            // LoadText
            // 
            this.LoadText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadText.ForeColor = System.Drawing.Color.White;
            this.LoadText.Location = new System.Drawing.Point(867, 762);
            this.LoadText.Name = "LoadText";
            this.LoadText.Size = new System.Drawing.Size(439, 24);
            this.LoadText.TabIndex = 59;
            this.LoadText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadText.Visible = false;
            // 
            // FastReview
            // 
            this.FastReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FastReview.BackColor = System.Drawing.Color.DarkGray;
            this.FastReview.ForeColor = System.Drawing.Color.Black;
            this.FastReview.Location = new System.Drawing.Point(832, 566);
            this.FastReview.Name = "FastReview";
            this.FastReview.Size = new System.Drawing.Size(156, 23);
            this.FastReview.TabIndex = 60;
            this.FastReview.Text = "Fast Review";
            this.FastReview.UseVisualStyleBackColor = false;
            this.FastReview.Click += new System.EventHandler(this.FastReview_Click);
            // 
            // TelemetryBox
            // 
            this.TelemetryBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TelemetryBox.AutoSize = true;
            this.TelemetryBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TelemetryBox.Location = new System.Drawing.Point(1008, 656);
            this.TelemetryBox.Name = "TelemetryBox";
            this.TelemetryBox.Size = new System.Drawing.Size(78, 17);
            this.TelemetryBox.TabIndex = 61;
            this.TelemetryBox.Text = "Telementry";
            this.TelemetryBox.UseVisualStyleBackColor = true;
            this.TelemetryBox.CheckedChanged += new System.EventHandler(this.TelemetryBox_CheckedChanged);
            // 
            // VideoFix
            // 
            this.VideoFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VideoFix.AutoSize = true;
            this.VideoFix.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VideoFix.Location = new System.Drawing.Point(1170, 656);
            this.VideoFix.Name = "VideoFix";
            this.VideoFix.Size = new System.Drawing.Size(72, 17);
            this.VideoFix.TabIndex = 62;
            this.VideoFix.Text = "Video Fix ";
            this.VideoFix.UseVisualStyleBackColor = true;
            this.VideoFix.CheckedChanged += new System.EventHandler(this.VideoFix_CheckedChanged);
            // 
            // SwitchChan
            // 
            this.SwitchChan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SwitchChan.AutoSize = true;
            this.SwitchChan.ForeColor = System.Drawing.Color.Transparent;
            this.SwitchChan.Location = new System.Drawing.Point(861, 743);
            this.SwitchChan.Name = "SwitchChan";
            this.SwitchChan.Size = new System.Drawing.Size(131, 17);
            this.SwitchChan.TabIndex = 63;
            this.SwitchChan.Text = "Channel Name Switch";
            this.SwitchChan.UseVisualStyleBackColor = true;
            this.SwitchChan.CheckedChanged += new System.EventHandler(this.SwitchChan_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Compression Manager",
            "Compress Directory",
            "Enter Notes",
            "Fix Channel at Timepoint",
            "Download ACQ",
            "Rename Channels",
            "Video Creator"});
            this.comboBox1.Location = new System.Drawing.Point(994, 597);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 64;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FastReviewFR
            // 
            this.FastReviewFR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FastReviewFR.BackColor = System.Drawing.Color.DarkGray;
            this.FastReviewFR.ForeColor = System.Drawing.Color.Black;
            this.FastReviewFR.Location = new System.Drawing.Point(84, 3);
            this.FastReviewFR.Name = "FastReviewFR";
            this.FastReviewFR.Size = new System.Drawing.Size(156, 23);
            this.FastReviewFR.TabIndex = 65;
            this.FastReviewFR.Text = "Fast Review";
            this.FastReviewFR.UseVisualStyleBackColor = false;
            this.FastReviewFR.Click += new System.EventHandler(this.FastReview_Click);
            // 
            // PreviousFR
            // 
            this.PreviousFR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PreviousFR.BackColor = System.Drawing.Color.DarkGray;
            this.PreviousFR.ForeColor = System.Drawing.Color.Black;
            this.PreviousFR.Location = new System.Drawing.Point(3, 3);
            this.PreviousFR.Name = "PreviousFR";
            this.PreviousFR.Size = new System.Drawing.Size(75, 23);
            this.PreviousFR.TabIndex = 66;
            this.PreviousFR.Text = "Previous";
            this.PreviousFR.UseVisualStyleBackColor = false;
            this.PreviousFR.Click += new System.EventHandler(this.Previous_Click);
            // 
            // NextFR
            // 
            this.NextFR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NextFR.BackColor = System.Drawing.Color.DarkGray;
            this.NextFR.ForeColor = System.Drawing.Color.Black;
            this.NextFR.Location = new System.Drawing.Point(246, 3);
            this.NextFR.Name = "NextFR";
            this.NextFR.Size = new System.Drawing.Size(75, 23);
            this.NextFR.TabIndex = 67;
            this.NextFR.Text = "Next";
            this.NextFR.UseVisualStyleBackColor = false;
            this.NextFR.Click += new System.EventHandler(this.Next_Click);
            // 
            // FRButtonGroup
            // 
            this.FRButtonGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FRButtonGroup.AutoSize = true;
            this.FRButtonGroup.Controls.Add(this.PreviousFR);
            this.FRButtonGroup.Controls.Add(this.FastReviewFR);
            this.FRButtonGroup.Controls.Add(this.NextFR);
            this.FRButtonGroup.Controls.Add(this.numPerBox);
            this.FRButtonGroup.Enabled = false;
            this.FRButtonGroup.Location = new System.Drawing.Point(676, 789);
            this.FRButtonGroup.Name = "FRButtonGroup";
            this.FRButtonGroup.Size = new System.Drawing.Size(452, 29);
            this.FRButtonGroup.TabIndex = 68;
            this.FRButtonGroup.Visible = false;
            // 
            // numPerBox
            // 
            this.numPerBox.FormattingEnabled = true;
            this.numPerBox.Items.AddRange(new object[] {
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28"});
            this.numPerBox.Location = new System.Drawing.Point(327, 3);
            this.numPerBox.Name = "numPerBox";
            this.numPerBox.Size = new System.Drawing.Size(121, 21);
            this.numPerBox.TabIndex = 72;
            this.numPerBox.SelectedIndexChanged += new System.EventHandler(this.numPerBox_SelectedIndexChanged);
            // 
            // FRZoomBar
            // 
            this.FRZoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FRZoomBar.Location = new System.Drawing.Point(3, 27);
            this.FRZoomBar.Maximum = 20;
            this.FRZoomBar.Minimum = 1;
            this.FRZoomBar.Name = "FRZoomBar";
            this.FRZoomBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FRZoomBar.Size = new System.Drawing.Size(171, 45);
            this.FRZoomBar.TabIndex = 69;
            this.FRZoomBar.TabStop = false;
            this.FRZoomBar.Value = 10;
            this.FRZoomBar.Scroll += new System.EventHandler(this.ZoomScale_Scroll);
            // 
            // FRZoomBlock
            // 
            this.FRZoomBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FRZoomBlock.Controls.Add(this.label4);
            this.FRZoomBlock.Controls.Add(this.FRZoomBar);
            this.FRZoomBlock.Enabled = false;
            this.FRZoomBlock.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FRZoomBlock.Location = new System.Drawing.Point(496, 766);
            this.FRZoomBlock.Name = "FRZoomBlock";
            this.FRZoomBlock.Size = new System.Drawing.Size(174, 77);
            this.FRZoomBlock.TabIndex = 70;
            this.FRZoomBlock.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 71;
            this.label4.Text = "Zoom level";
            // 
            // FRPageNum
            // 
            this.FRPageNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FRPageNum.AutoSize = true;
            this.FRPageNum.Enabled = false;
            this.FRPageNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FRPageNum.ForeColor = System.Drawing.Color.White;
            this.FRPageNum.Location = new System.Drawing.Point(758, 766);
            this.FRPageNum.Name = "FRPageNum";
            this.FRPageNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FRPageNum.Size = new System.Drawing.Size(0, 24);
            this.FRPageNum.TabIndex = 71;
            this.FRPageNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.FRPageNum.Visible = false;
            // 
            // LoadAll
            // 
            this.LoadAll.Location = new System.Drawing.Point(1008, 739);
            this.LoadAll.Name = "LoadAll";
            this.LoadAll.Size = new System.Drawing.Size(91, 23);
            this.LoadAll.TabIndex = 72;
            this.LoadAll.Text = "Load All Videos";
            this.LoadAll.UseVisualStyleBackColor = true;
            this.LoadAll.Click += new System.EventHandler(this.LoadAll_Click);
            // 
            // CManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1444, 845);
            this.Controls.Add(this.LoadAll);
            this.Controls.Add(this.FRPageNum);
            this.Controls.Add(this.FRZoomBlock);
            this.Controls.Add(this.FRButtonGroup);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SwitchChan);
            this.Controls.Add(this.VideoFix);
            this.Controls.Add(this.TelemetryBox);
            this.Controls.Add(this.FastReview);
            this.Controls.Add(this.LoadText);
            this.Controls.Add(this.AVILoadBar);
            this.Controls.Add(this.Randomization);
            this.Controls.Add(this.DetSezLabel);
            this.Controls.Add(this.HighlightLabel);
            this.Controls.Add(this.OffsetBox);
            this.Controls.Add(this.OffsetLabel);
            this.Controls.Add(this.RvwSz);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VisChan16);
            this.Controls.Add(this.VisChan15);
            this.Controls.Add(this.VisChan14);
            this.Controls.Add(this.VisChan13);
            this.Controls.Add(this.VisChan12);
            this.Controls.Add(this.VisChan11);
            this.Controls.Add(this.VisChan10);
            this.Controls.Add(this.VisChan9);
            this.Controls.Add(this.VisChan8);
            this.Controls.Add(this.VisChan7);
            this.Controls.Add(this.VisChan6);
            this.Controls.Add(this.VisChan5);
            this.Controls.Add(this.VisChan4);
            this.Controls.Add(this.VisChan3);
            this.Controls.Add(this.VisChan2);
            this.Controls.Add(this.VisChan1);
            this.Controls.Add(this.TimeJump);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DetectionLoadButton);
            this.Controls.Add(this.SzCaptureButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.SpeedUp);
            this.Controls.Add(this.Rewind);
            this.Controls.Add(this.TimeBar);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.ZoomScale);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seizure Video Playback";
            this.TransparencyKey = System.Drawing.Color.Purple;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomScale)).EndInit();
            this.FRButtonGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FRZoomBar)).EndInit();
            this.FRZoomBlock.ResumeLayout(false);
            this.FRZoomBlock.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.Button Rewind;
        private System.Windows.Forms.Button SpeedUp;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.ComboBox TimeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SzCaptureButton;
        private System.Windows.Forms.Button DetectionLoadButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TimeJump;
        private System.Windows.Forms.CheckBox VisChan1;
        private System.Windows.Forms.CheckBox VisChan2;
        private System.Windows.Forms.CheckBox VisChan3;
        private System.Windows.Forms.CheckBox VisChan4;
        private System.Windows.Forms.CheckBox VisChan5;
        private System.Windows.Forms.CheckBox VisChan6;
        private System.Windows.Forms.CheckBox VisChan7;
        private System.Windows.Forms.CheckBox VisChan8;
        private System.Windows.Forms.CheckBox VisChan16;
        private System.Windows.Forms.CheckBox VisChan15;
        private System.Windows.Forms.CheckBox VisChan14;
        private System.Windows.Forms.CheckBox VisChan13;
        private System.Windows.Forms.CheckBox VisChan12;
        private System.Windows.Forms.CheckBox VisChan11;
        private System.Windows.Forms.CheckBox VisChan10;
        private System.Windows.Forms.CheckBox VisChan9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar ZoomScale;
        private System.Windows.Forms.Button RvwSz;
        private System.Windows.Forms.Label OffsetLabel;
        private System.Windows.Forms.TextBox OffsetBox;
        private System.Windows.Forms.Label HighlightLabel;
        private System.Windows.Forms.Label DetSezLabel;

        private System.Windows.Forms.CheckBox Randomization;
        private System.Windows.Forms.ProgressBar AVILoadBar;
        private System.Windows.Forms.Button FastReview;
        private System.Windows.Forms.CheckBox TelemetryBox;
        private System.Windows.Forms.CheckBox VideoFix;
        private System.Windows.Forms.CheckBox SwitchChan;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button FastReviewFR;
        private System.Windows.Forms.Button PreviousFR;
        private System.Windows.Forms.Button NextFR;
        private System.Windows.Forms.FlowLayoutPanel FRButtonGroup;
        private System.Windows.Forms.TrackBar FRZoomBar;
        private System.Windows.Forms.FlowLayoutPanel FRZoomBlock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label FRPageNum;
        public System.Windows.Forms.ComboBox numPerBox;
        public System.Windows.Forms.Label LoadText;
        private System.Windows.Forms.Button LoadAll;
    }
}

