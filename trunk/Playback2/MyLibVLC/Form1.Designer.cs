namespace SeizurePlayback
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
            this.Stop = new System.Windows.Forms.Button();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Open = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.test = new System.Windows.Forms.Button();
            this.Rewind = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.Recompress = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.ForeColor = System.Drawing.SystemColors.Control;
            this.Play.Location = new System.Drawing.Point(338, 552);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 0;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Stop
            // 
            this.Stop.ForeColor = System.Drawing.SystemColors.Control;
            this.Stop.Location = new System.Drawing.Point(338, 610);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // VideoPanel
            // 
            this.VideoPanel.Location = new System.Drawing.Point(12, 523);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(320, 240);
            this.VideoPanel.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Open
            // 
            this.Open.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Open.Location = new System.Drawing.Point(338, 523);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(127, 23);
            this.Open.TabIndex = 3;
            this.Open.Text = "Select Directory";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Pause
            // 
            this.Pause.ForeColor = System.Drawing.SystemColors.Control;
            this.Pause.Location = new System.Drawing.Point(338, 581);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 23);
            this.Pause.TabIndex = 0;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // TimeBar
            // 
            this.TimeBar.LargeChange = 1;
            this.TimeBar.Location = new System.Drawing.Point(369, 721);
            this.TimeBar.Maximum = 9;
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(646, 42);
            this.TimeBar.TabIndex = 13;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // test
            // 
            this.test.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.test.Location = new System.Drawing.Point(940, 745);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 14;
            this.test.Text = "SetBar";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // Rewind
            // 
            this.Rewind.ForeColor = System.Drawing.SystemColors.Control;
            this.Rewind.Location = new System.Drawing.Point(419, 581);
            this.Rewind.Name = "Rewind";
            this.Rewind.Size = new System.Drawing.Size(75, 23);
            this.Rewind.TabIndex = 15;
            this.Rewind.Text = "Rewind 60s";
            this.Rewind.UseVisualStyleBackColor = true;
            this.Rewind.Click += new System.EventHandler(this.Rewind_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(419, 552);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Speed Up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimeLabel.Location = new System.Drawing.Point(12, 6);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(53, 24);
            this.TimeLabel.TabIndex = 17;
            this.TimeLabel.Text = "Time";
            // 
            // Recompress
            // 
            this.Recompress.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Recompress.Location = new System.Drawing.Point(338, 639);
            this.Recompress.Name = "Recompress";
            this.Recompress.Size = new System.Drawing.Size(75, 23);
            this.Recompress.TabIndex = 18;
            this.Recompress.Text = "Recompress";
            this.Recompress.UseVisualStyleBackColor = true;
            this.Recompress.Click += new System.EventHandler(this.Recompress_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1194, 775);
            this.Controls.Add(this.Recompress);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Rewind);
            this.Controls.Add(this.test);
            this.Controls.Add(this.TimeBar);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Play);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seizure Video Playback";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Button Rewind;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button Recompress;
    }
}

