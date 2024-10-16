﻿namespace SeizurePlayback
{
    partial class SzPrompt
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
            this.NotesBx = new System.Windows.Forms.TextBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.CurFileProg = new System.Windows.Forms.ProgressBar();
            this.PleaseWait = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NonConv = new System.Windows.Forms.RadioButton();
            this.S1 = new System.Windows.Forms.RadioButton();
            this.S2 = new System.Windows.Forms.RadioButton();
            this.S3 = new System.Windows.Forms.RadioButton();
            this.S4 = new System.Windows.Forms.RadioButton();
            this.S5 = new System.Windows.Forms.RadioButton();
            this.RadioBox = new System.Windows.Forms.GroupBox();
            this.Unknown = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.VideoSave = new System.Windows.Forms.CheckBox();
            this.CaptureLen = new System.Windows.Forms.Label();
            this.ShortCapWarning = new System.Windows.Forms.Label();
            this.RadioBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotesBx
            // 
            this.NotesBx.Location = new System.Drawing.Point(12, 25);
            this.NotesBx.Name = "NotesBx";
            this.NotesBx.Size = new System.Drawing.Size(385, 20);
            this.NotesBx.TabIndex = 0;
            this.NotesBx.TextChanged += new System.EventHandler(this.NotesBx_TextChanged);
            // 
            // OKBtn
            // 
            this.OKBtn.Enabled = false;
            this.OKBtn.Location = new System.Drawing.Point(241, 51);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 1;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(322, 51);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CurFileProg
            // 
            this.CurFileProg.Location = new System.Drawing.Point(15, 103);
            this.CurFileProg.Name = "CurFileProg";
            this.CurFileProg.Size = new System.Drawing.Size(382, 25);
            this.CurFileProg.TabIndex = 3;
            this.CurFileProg.Click += new System.EventHandler(this.CurFileProg_Click);
            // 
            // PleaseWait
            // 
            this.PleaseWait.AutoSize = true;
            this.PleaseWait.Location = new System.Drawing.Point(12, 56);
            this.PleaseWait.Name = "PleaseWait";
            this.PleaseWait.Size = new System.Drawing.Size(0, 13);
            this.PleaseWait.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter Notes";
            // 
            // NonConv
            // 
            this.NonConv.AutoSize = true;
            this.NonConv.Location = new System.Drawing.Point(6, 19);
            this.NonConv.Name = "NonConv";
            this.NonConv.Size = new System.Drawing.Size(96, 17);
            this.NonConv.TabIndex = 6;
            this.NonConv.TabStop = true;
            this.NonConv.Text = "Nonconvulsive";
            this.NonConv.UseVisualStyleBackColor = true;
            // 
            // S1
            // 
            this.S1.AutoSize = true;
            this.S1.Location = new System.Drawing.Point(6, 42);
            this.S1.Name = "S1";
            this.S1.Size = new System.Drawing.Size(62, 17);
            this.S1.TabIndex = 7;
            this.S1.TabStop = true;
            this.S1.Text = "Stage 1";
            this.S1.UseVisualStyleBackColor = true;
            this.S1.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            // 
            // S2
            // 
            this.S2.AutoSize = true;
            this.S2.Location = new System.Drawing.Point(6, 65);
            this.S2.Name = "S2";
            this.S2.Size = new System.Drawing.Size(62, 17);
            this.S2.TabIndex = 8;
            this.S2.TabStop = true;
            this.S2.Text = "Stage 2";
            this.S2.UseVisualStyleBackColor = true;
            this.S2.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            // 
            // S3
            // 
            this.S3.AutoSize = true;
            this.S3.Location = new System.Drawing.Point(6, 88);
            this.S3.Name = "S3";
            this.S3.Size = new System.Drawing.Size(62, 17);
            this.S3.TabIndex = 9;
            this.S3.TabStop = true;
            this.S3.Text = "Stage 3";
            this.S3.UseVisualStyleBackColor = true;
            this.S3.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            // 
            // S4
            // 
            this.S4.AutoSize = true;
            this.S4.Location = new System.Drawing.Point(108, 48);
            this.S4.Name = "S4";
            this.S4.Size = new System.Drawing.Size(62, 17);
            this.S4.TabIndex = 10;
            this.S4.TabStop = true;
            this.S4.Text = "Stage 4";
            this.S4.UseVisualStyleBackColor = true;
            this.S4.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            // 
            // S5
            // 
            this.S5.AutoSize = true;
            this.S5.Location = new System.Drawing.Point(108, 71);
            this.S5.Name = "S5";
            this.S5.Size = new System.Drawing.Size(62, 17);
            this.S5.TabIndex = 11;
            this.S5.TabStop = true;
            this.S5.Text = "Stage 5";
            this.S5.UseVisualStyleBackColor = true;
            this.S5.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            // 
            // RadioBox
            // 
            this.RadioBox.Controls.Add(this.Unknown);
            this.RadioBox.Controls.Add(this.S5);
            this.RadioBox.Controls.Add(this.NonConv);
            this.RadioBox.Controls.Add(this.S4);
            this.RadioBox.Controls.Add(this.S1);
            this.RadioBox.Controls.Add(this.S3);
            this.RadioBox.Controls.Add(this.S2);
            this.RadioBox.Location = new System.Drawing.Point(416, 4);
            this.RadioBox.Name = "RadioBox";
            this.RadioBox.Size = new System.Drawing.Size(197, 113);
            this.RadioBox.TabIndex = 12;
            this.RadioBox.TabStop = false;
            this.RadioBox.Text = "Please Mark Seizure Stage";
            // 
            // Unknown
            // 
            this.Unknown.AutoSize = true;
            this.Unknown.Location = new System.Drawing.Point(108, 15);
            this.Unknown.Name = "Unknown";
            this.Unknown.Size = new System.Drawing.Size(80, 30);
            this.Unknown.TabIndex = 12;
            this.Unknown.TabStop = true;
            this.Unknown.Text = "Unknown/\r\nNonSeizure\r\n";
            this.Unknown.UseVisualStyleBackColor = true;
            this.Unknown.CheckedChanged += new System.EventHandler(this.RadioButtonChanged);
            this.Unknown.Click += new System.EventHandler(this.RadioButtonClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Enter Notes";
            // 
            // VideoSave
            // 
            this.VideoSave.AutoSize = true;
            this.VideoSave.Location = new System.Drawing.Point(416, 123);
            this.VideoSave.Name = "VideoSave";
            this.VideoSave.Size = new System.Drawing.Size(93, 17);
            this.VideoSave.TabIndex = 14;
            this.VideoSave.Text = "Capture Video";
            this.VideoSave.UseVisualStyleBackColor = true;
            // 
            // CaptureLen
            // 
            this.CaptureLen.AutoSize = true;
            this.CaptureLen.BackColor = System.Drawing.SystemColors.Menu;
            this.CaptureLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CaptureLen.Location = new System.Drawing.Point(12, 160);
            this.CaptureLen.Name = "CaptureLen";
            this.CaptureLen.Size = new System.Drawing.Size(2, 15);
            this.CaptureLen.TabIndex = 15;
            // 
            // ShortCapWarning
            // 
            this.ShortCapWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortCapWarning.AutoSize = true;
            this.ShortCapWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShortCapWarning.ForeColor = System.Drawing.Color.Red;
            this.ShortCapWarning.Location = new System.Drawing.Point(12, 197);
            this.ShortCapWarning.Name = "ShortCapWarning";
            this.ShortCapWarning.Size = new System.Drawing.Size(372, 16);
            this.ShortCapWarning.TabIndex = 16;
            this.ShortCapWarning.Text = "WARNING: Your Selected Seizure is Under 10 Seconds Long";
            this.ShortCapWarning.Visible = false;
            this.ShortCapWarning.Click += new System.EventHandler(this.ShortCapWarning_Click);
            // 
            // SzPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 233);
            this.Controls.Add(this.ShortCapWarning);
            this.Controls.Add(this.CaptureLen);
            this.Controls.Add(this.VideoSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PleaseWait);
            this.Controls.Add(this.CurFileProg);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.NotesBx);
            this.Controls.Add(this.RadioBox);
            this.Name = "SzPrompt";
            this.Text = "Capture Seizure";
            this.Load += new System.EventHandler(this.SzPrompt_Load);
            this.RadioBox.ResumeLayout(false);
            this.RadioBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NotesBx;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ProgressBar CurFileProg;
        private System.Windows.Forms.Label PleaseWait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton NonConv;
        private System.Windows.Forms.RadioButton S1;
        private System.Windows.Forms.RadioButton S2;
        private System.Windows.Forms.RadioButton S3;
        private System.Windows.Forms.RadioButton S4;
        private System.Windows.Forms.RadioButton S5;
        private System.Windows.Forms.GroupBox RadioBox;
        private System.Windows.Forms.RadioButton Unknown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox VideoSave;
        public System.Windows.Forms.Label CaptureLen;
        public System.Windows.Forms.Label ShortCapWarning;
    }
}