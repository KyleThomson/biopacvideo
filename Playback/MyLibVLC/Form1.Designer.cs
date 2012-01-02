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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Open2 = new System.Windows.Forms.Button();
            this.Open3 = new System.Windows.Forms.Button();
            this.Open4 = new System.Windows.Forms.Button();
            this.RateLabel = new System.Windows.Forms.Label();
            this.F1 = new System.Windows.Forms.Label();
            this.F2 = new System.Windows.Forms.Label();
            this.F3 = new System.Windows.Forms.Label();
            this.F4 = new System.Windows.Forms.Label();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.test = new System.Windows.Forms.Button();
            this.Enc1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SezLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Play
            // 
            this.Play.Enabled = false;
            this.Play.ForeColor = System.Drawing.SystemColors.Control;
            this.Play.Location = new System.Drawing.Point(664, 417);
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
            this.Stop.Location = new System.Drawing.Point(664, 475);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 41);
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
            this.Open.Location = new System.Drawing.Point(664, 12);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 3;
            this.Open.Text = "File 1";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Pause
            // 
            this.Pause.Enabled = false;
            this.Pause.ForeColor = System.Drawing.SystemColors.Control;
            this.Pause.Location = new System.Drawing.Point(664, 446);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 23);
            this.Pause.TabIndex = 0;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(698, 621);
            this.trackBar1.Maximum = 9;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(339, 42);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(338, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 240);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(12, 354);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(320, 240);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(338, 354);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(320, 240);
            this.panel4.TabIndex = 3;
            // 
            // Open2
            // 
            this.Open2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Open2.Location = new System.Drawing.Point(664, 41);
            this.Open2.Name = "Open2";
            this.Open2.Size = new System.Drawing.Size(75, 23);
            this.Open2.TabIndex = 5;
            this.Open2.Text = "File 2";
            this.Open2.UseVisualStyleBackColor = true;
            this.Open2.Click += new System.EventHandler(this.Open2_Click);
            // 
            // Open3
            // 
            this.Open3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Open3.Location = new System.Drawing.Point(664, 70);
            this.Open3.Name = "Open3";
            this.Open3.Size = new System.Drawing.Size(75, 23);
            this.Open3.TabIndex = 6;
            this.Open3.Text = "File 3";
            this.Open3.UseVisualStyleBackColor = true;
            this.Open3.Click += new System.EventHandler(this.Open3_Click);
            // 
            // Open4
            // 
            this.Open4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Open4.Location = new System.Drawing.Point(664, 99);
            this.Open4.Name = "Open4";
            this.Open4.Size = new System.Drawing.Size(75, 23);
            this.Open4.TabIndex = 7;
            this.Open4.Text = "File 4";
            this.Open4.UseVisualStyleBackColor = true;
            this.Open4.Click += new System.EventHandler(this.Open4_Click);
            // 
            // RateLabel
            // 
            this.RateLabel.AutoSize = true;
            this.RateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RateLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.RateLabel.Location = new System.Drawing.Point(941, 642);
            this.RateLabel.Name = "RateLabel";
            this.RateLabel.Size = new System.Drawing.Size(96, 24);
            this.RateLabel.TabIndex = 8;
            this.RateLabel.Text = "Speed: 1x";
            // 
            // F1
            // 
            this.F1.AutoSize = true;
            this.F1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.F1.Location = new System.Drawing.Point(12, 14);
            this.F1.Name = "F1";
            this.F1.Size = new System.Drawing.Size(56, 24);
            this.F1.TabIndex = 9;
            this.F1.Text = "File 1";
            // 
            // F2
            // 
            this.F2.AutoSize = true;
            this.F2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.F2.Location = new System.Drawing.Point(341, 14);
            this.F2.Name = "F2";
            this.F2.Size = new System.Drawing.Size(56, 24);
            this.F2.TabIndex = 10;
            this.F2.Text = "File 2";
            // 
            // F3
            // 
            this.F3.AutoSize = true;
            this.F3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.F3.Location = new System.Drawing.Point(12, 327);
            this.F3.Name = "F3";
            this.F3.Size = new System.Drawing.Size(56, 24);
            this.F3.TabIndex = 11;
            this.F3.Text = "File 3";
            // 
            // F4
            // 
            this.F4.AutoSize = true;
            this.F4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.F4.Location = new System.Drawing.Point(341, 327);
            this.F4.Name = "F4";
            this.F4.Size = new System.Drawing.Size(56, 24);
            this.F4.TabIndex = 12;
            this.F4.Text = "File 4";
            // 
            // TimeBar
            // 
            this.TimeBar.LargeChange = 1;
            this.TimeBar.Location = new System.Drawing.Point(12, 621);
            this.TimeBar.Maximum = 9;
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(646, 42);
            this.TimeBar.TabIndex = 13;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // test
            // 
            this.test.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.test.Location = new System.Drawing.Point(583, 645);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 14;
            this.test.Text = "SetBar";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // Enc1
            // 
            this.Enc1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Enc1.Location = new System.Drawing.Point(812, 17);
            this.Enc1.Name = "Enc1";
            this.Enc1.Size = new System.Drawing.Size(75, 23);
            this.Enc1.TabIndex = 15;
            this.Enc1.Text = "Seizure 1";
            this.Enc1.UseVisualStyleBackColor = true;
            this.Enc1.Click += new System.EventHandler(this.Enc1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Location = new System.Drawing.Point(812, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Seiure 2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button3.Location = new System.Drawing.Point(812, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Seizure 3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button4.Location = new System.Drawing.Point(812, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Seizure 4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // SezLength
            // 
            this.SezLength.Location = new System.Drawing.Point(812, 128);
            this.SezLength.Name = "SezLength";
            this.SezLength.Size = new System.Drawing.Size(75, 20);
            this.SezLength.TabIndex = 19;
            this.SezLength.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(893, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Video Length (in seconds)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1049, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SezLength);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Enc1);
            this.Controls.Add(this.test);
            this.Controls.Add(this.TimeBar);
            this.Controls.Add(this.F4);
            this.Controls.Add(this.F3);
            this.Controls.Add(this.F2);
            this.Controls.Add(this.F1);
            this.Controls.Add(this.RateLabel);
            this.Controls.Add(this.Open4);
            this.Controls.Add(this.Open3);
            this.Controls.Add(this.Open2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.trackBar1);
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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button Open2;
        private System.Windows.Forms.Button Open3;
        private System.Windows.Forms.Button Open4;
        private System.Windows.Forms.Label RateLabel;
        private System.Windows.Forms.Label F1;
        private System.Windows.Forms.Label F2;
        private System.Windows.Forms.Label F3;
        private System.Windows.Forms.Label F4;
        private System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Button Enc1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox SezLength;
        private System.Windows.Forms.Label label1;
    }
}

