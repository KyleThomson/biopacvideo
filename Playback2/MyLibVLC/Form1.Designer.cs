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
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Open = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.test = new System.Windows.Forms.Button();
            this.EEGPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.Enabled = false;
            this.Play.ForeColor = System.Drawing.SystemColors.Control;
            this.Play.Location = new System.Drawing.Point(360, 452);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 0;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.ForeColor = System.Drawing.SystemColors.Control;
            this.Stop.Location = new System.Drawing.Point(360, 510);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 240);
            this.panel1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Open
            // 
            this.Open.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Open.Location = new System.Drawing.Point(360, 423);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(127, 23);
            this.Open.TabIndex = 3;
            this.Open.Text = "Select Directory";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Pause
            // 
            this.Pause.Enabled = false;
            this.Pause.ForeColor = System.Drawing.SystemColors.Control;
            this.Pause.Location = new System.Drawing.Point(360, 481);
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
            this.TimeBar.Location = new System.Drawing.Point(391, 621);
            this.TimeBar.Maximum = 9;
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(646, 42);
            this.TimeBar.TabIndex = 13;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // test
            // 
            this.test.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.test.Location = new System.Drawing.Point(962, 645);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 14;
            this.test.Text = "SetBar";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // EEGPanel
            // 
            this.EEGPanel.BackColor = System.Drawing.Color.White;
            this.EEGPanel.Location = new System.Drawing.Point(12, 12);
            this.EEGPanel.Name = "EEGPanel";
            this.EEGPanel.Size = new System.Drawing.Size(1025, 392);
            this.EEGPanel.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1049, 675);
            this.Controls.Add(this.EEGPanel);
            this.Controls.Add(this.test);
            this.Controls.Add(this.TimeBar);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.panel1);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Panel EEGPanel;
    }
}
