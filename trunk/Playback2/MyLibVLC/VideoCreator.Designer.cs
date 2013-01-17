namespace SeizurePlayback
{
    partial class VideoCreator
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
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.CapPanel = new System.Windows.Forms.Panel();
            this.EEGPanel = new System.Windows.Forms.Panel();
            this.NotesBox = new System.Windows.Forms.TextBox();
            this.StartCap = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ZoomScale = new System.Windows.Forms.TrackBar();
            this.CurProg = new System.Windows.Forms.ProgressBar();
            this.ProgText = new System.Windows.Forms.Label();
            this.CapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomScale)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoPanel
            // 
            this.VideoPanel.BackColor = System.Drawing.SystemColors.Control;
            this.VideoPanel.Location = new System.Drawing.Point(5, 230);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(640, 480);
            this.VideoPanel.TabIndex = 0;
            // 
            // CapPanel
            // 
            this.CapPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CapPanel.Controls.Add(this.EEGPanel);
            this.CapPanel.Controls.Add(this.NotesBox);
            this.CapPanel.Controls.Add(this.VideoPanel);
            this.CapPanel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CapPanel.Location = new System.Drawing.Point(12, 12);
            this.CapPanel.Name = "CapPanel";
            this.CapPanel.Size = new System.Drawing.Size(960, 720);
            this.CapPanel.TabIndex = 1;
            // 
            // EEGPanel
            // 
            this.EEGPanel.Location = new System.Drawing.Point(3, 3);
            this.EEGPanel.Name = "EEGPanel";
            this.EEGPanel.Size = new System.Drawing.Size(950, 221);
            this.EEGPanel.TabIndex = 2;
            // 
            // NotesBox
            // 
            this.NotesBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.NotesBox.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesBox.ForeColor = System.Drawing.SystemColors.Info;
            this.NotesBox.Location = new System.Drawing.Point(651, 230);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(302, 480);
            this.NotesBox.TabIndex = 1;
            this.NotesBox.Text = "Enter notes here.";
            // 
            // StartCap
            // 
            this.StartCap.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.StartCap.Location = new System.Drawing.Point(12, 738);
            this.StartCap.Name = "StartCap";
            this.StartCap.Size = new System.Drawing.Size(125, 25);
            this.StartCap.TabIndex = 2;
            this.StartCap.Text = "Start Capture";
            this.StartCap.UseVisualStyleBackColor = true;
            this.StartCap.Click += new System.EventHandler(this.StartCap_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(143, 738);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "Dump Frames";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DumpFrames_Click);
            // 
            // ZoomScale
            // 
            this.ZoomScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomScale.Location = new System.Drawing.Point(274, 738);
            this.ZoomScale.Maximum = 20;
            this.ZoomScale.Minimum = 1;
            this.ZoomScale.Name = "ZoomScale";
            this.ZoomScale.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ZoomScale.Size = new System.Drawing.Size(171, 42);
            this.ZoomScale.TabIndex = 44;
            this.ZoomScale.TabStop = false;
            this.ZoomScale.Value = 10;
            this.ZoomScale.Scroll += new System.EventHandler(this.ZoomScale_Scroll);
            // 
            // CurProg
            // 
            this.CurProg.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CurProg.Location = new System.Drawing.Point(12, 769);
            this.CurProg.Name = "CurProg";
            this.CurProg.Size = new System.Drawing.Size(960, 23);
            this.CurProg.TabIndex = 45;
            // 
            // ProgText
            // 
            this.ProgText.AutoSize = true;
            this.ProgText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ProgText.Location = new System.Drawing.Point(821, 742);
            this.ProgText.Name = "ProgText";
            this.ProgText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ProgText.Size = new System.Drawing.Size(149, 16);
            this.ProgText.TabIndex = 46;
            this.ProgText.Text = "                                               ";
            this.ProgText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VideoCreator
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(988, 840);
            this.Controls.Add(this.ProgText);
            this.Controls.Add(this.CurProg);
            this.Controls.Add(this.ZoomScale);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartCap);
            this.Controls.Add(this.CapPanel);
            this.Name = "VideoCreator";
            this.Text = "Video Creator";
            this.Load += new System.EventHandler(this.VideoCreator_Load);
            this.CapPanel.ResumeLayout(false);
            this.CapPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel VideoPanel;
        private System.Windows.Forms.Panel CapPanel;
        private System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Button StartCap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar ZoomScale;
        private System.Windows.Forms.Panel EEGPanel;
        private System.Windows.Forms.ProgressBar CurProg;
        private System.Windows.Forms.Label ProgText;
    }
}