namespace BioPacVideo
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.labelBioPacVideo = new System.Windows.Forms.Label();
            this.pictureBoxBPVLogo = new System.Windows.Forms.PictureBox();
            this.labelCopyRight = new System.Windows.Forms.Label();
            this.textBoxAbout = new System.Windows.Forms.TextBox();
            this.labelVersionNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBPVLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBioPacVideo
            // 
            this.labelBioPacVideo.AutoSize = true;
            this.labelBioPacVideo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBioPacVideo.Location = new System.Drawing.Point(75, 10);
            this.labelBioPacVideo.Name = "labelBioPacVideo";
            this.labelBioPacVideo.Size = new System.Drawing.Size(112, 22);
            this.labelBioPacVideo.TabIndex = 0;
            this.labelBioPacVideo.Text = "BioPacVideo";
            // 
            // pictureBoxBPVLogo
            // 
            this.pictureBoxBPVLogo.ImageLocation = "";
            this.pictureBoxBPVLogo.InitialImage = null;
            this.pictureBoxBPVLogo.Location = new System.Drawing.Point(12, 10);
            this.pictureBoxBPVLogo.Name = "pictureBoxBPVLogo";
            this.pictureBoxBPVLogo.Size = new System.Drawing.Size(57, 92);
            this.pictureBoxBPVLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBPVLogo.TabIndex = 1;
            this.pictureBoxBPVLogo.TabStop = false;
            // 
            // labelCopyRight
            // 
            this.labelCopyRight.AutoSize = true;
            this.labelCopyRight.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyRight.Location = new System.Drawing.Point(76, 33);
            this.labelCopyRight.Name = "labelCopyRight";
            this.labelCopyRight.Size = new System.Drawing.Size(122, 16);
            this.labelCopyRight.TabIndex = 2;
            this.labelCopyRight.Text = "© 2024 Kyle Thomson";
            // 
            // textBoxAbout
            // 
            this.textBoxAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAbout.Enabled = false;
            this.textBoxAbout.Location = new System.Drawing.Point(12, 108);
            this.textBoxAbout.Multiline = true;
            this.textBoxAbout.Name = "textBoxAbout";
            this.textBoxAbout.ReadOnly = true;
            this.textBoxAbout.Size = new System.Drawing.Size(260, 313);
            this.textBoxAbout.TabIndex = 3;
            this.textBoxAbout.Text = resources.GetString("textBoxAbout.Text");
            // 
            // labelVersionNumber
            // 
            this.labelVersionNumber.AutoSize = true;
            this.labelVersionNumber.Location = new System.Drawing.Point(76, 49);
            this.labelVersionNumber.Name = "labelVersionNumber";
            this.labelVersionNumber.Size = new System.Drawing.Size(87, 14);
            this.labelVersionNumber.TabIndex = 4;
            this.labelVersionNumber.Text = "VersionNumber";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 436);
            this.Controls.Add(this.labelVersionNumber);
            this.Controls.Add(this.textBoxAbout);
            this.Controls.Add(this.labelCopyRight);
            this.Controls.Add(this.pictureBoxBPVLogo);
            this.Controls.Add(this.labelBioPacVideo);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(300, 475);
            this.MinimumSize = new System.Drawing.Size(300, 475);
            this.Name = "About";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBPVLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBioPacVideo;
        private System.Windows.Forms.PictureBox pictureBoxBPVLogo;
        private System.Windows.Forms.Label labelCopyRight;
        private System.Windows.Forms.TextBox textBoxAbout;
        private System.Windows.Forms.Label labelVersionNumber;
    }
}