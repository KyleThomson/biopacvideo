namespace BioPacVideo
{
    partial class RecordSettings
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
            this.MPTypeBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BreakBox = new System.Windows.Forms.GroupBox();
            this.Midnight = new System.Windows.Forms.RadioButton();
            this.MidnightNoon = new System.Windows.Forms.RadioButton();
            this.sampleRateBar = new System.Windows.Forms.TrackBar();
            this.sampleRateLabel = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BreakBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleRateBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MPTypeBox
            // 
            this.MPTypeBox.FormattingEnabled = true;
            this.MPTypeBox.Items.AddRange(new object[] {
            "MP150",
            "MP160"});
            this.MPTypeBox.Location = new System.Drawing.Point(29, 37);
            this.MPTypeBox.MaxDropDownItems = 2;
            this.MPTypeBox.Name = "MPTypeBox";
            this.MPTypeBox.Size = new System.Drawing.Size(121, 21);
            this.MPTypeBox.TabIndex = 0;
            this.MPTypeBox.SelectedIndexChanged += new System.EventHandler(this.MPTypeBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MP Type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(529, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sample Rate";
            // 
            // BreakBox
            // 
            this.BreakBox.Controls.Add(this.Midnight);
            this.BreakBox.Controls.Add(this.MidnightNoon);
            this.BreakBox.Location = new System.Drawing.Point(170, 21);
            this.BreakBox.Name = "BreakBox";
            this.BreakBox.Size = new System.Drawing.Size(145, 76);
            this.BreakBox.TabIndex = 4;
            this.BreakBox.TabStop = false;
            this.BreakBox.Text = "Break File? ";
            // 
            // Midnight
            // 
            this.Midnight.AutoSize = true;
            this.Midnight.Location = new System.Drawing.Point(6, 43);
            this.Midnight.Name = "Midnight";
            this.Midnight.Size = new System.Drawing.Size(87, 17);
            this.Midnight.TabIndex = 6;
            this.Midnight.TabStop = true;
            this.Midnight.Text = "Midnight only";
            this.Midnight.UseVisualStyleBackColor = true;
            // 
            // MidnightNoon
            // 
            this.MidnightNoon.AutoSize = true;
            this.MidnightNoon.Location = new System.Drawing.Point(6, 20);
            this.MidnightNoon.Name = "MidnightNoon";
            this.MidnightNoon.Size = new System.Drawing.Size(115, 17);
            this.MidnightNoon.TabIndex = 5;
            this.MidnightNoon.TabStop = true;
            this.MidnightNoon.Text = "Midnight and Noon";
            this.MidnightNoon.UseVisualStyleBackColor = true;
            // 
            // sampleRateBar
            // 
            this.sampleRateBar.LargeChange = 100;
            this.sampleRateBar.Location = new System.Drawing.Point(29, 137);
            this.sampleRateBar.Maximum = 1000;
            this.sampleRateBar.Name = "sampleRateBar";
            this.sampleRateBar.Size = new System.Drawing.Size(286, 45);
            this.sampleRateBar.SmallChange = 10;
            this.sampleRateBar.TabIndex = 10;
            this.sampleRateBar.TickFrequency = 100;
            this.sampleRateBar.Value = 500;
            this.sampleRateBar.ValueChanged += new System.EventHandler(this.sampleRateBar_ValueChanged);
            // 
            // sampleRateLabel
            // 
            this.sampleRateLabel.AutoSize = true;
            this.sampleRateLabel.Location = new System.Drawing.Point(115, 108);
            this.sampleRateLabel.Name = "sampleRateLabel";
            this.sampleRateLabel.Size = new System.Drawing.Size(25, 13);
            this.sampleRateLabel.TabIndex = 6;
            this.sampleRateLabel.Text = "500";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(29, 199);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 20);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Default";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "500";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "1000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "ms/sample";
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 272);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.sampleRateLabel);
            this.Controls.Add(this.sampleRateBar);
            this.Controls.Add(this.BreakBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MPTypeBox);
            this.Name = "RecordSettings";
            this.Text = "Record Settings";
            this.BreakBox.ResumeLayout(false);
            this.BreakBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleRateBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox MPTypeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox BreakBox;
        private System.Windows.Forms.RadioButton Midnight;
        private System.Windows.Forms.RadioButton MidnightNoon;
        private System.Windows.Forms.TrackBar sampleRateBar;
        private System.Windows.Forms.Label sampleRateLabel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}