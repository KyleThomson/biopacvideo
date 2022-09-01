
namespace SeizurePlayback
{
    partial class VLoadWait
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
            this.VideoLoadProgressBar = new System.Windows.Forms.ProgressBar();
            this.LT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VideoLoadProgressBar
            // 
            this.VideoLoadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoLoadProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.VideoLoadProgressBar.Location = new System.Drawing.Point(100, 109);
            this.VideoLoadProgressBar.Name = "VideoLoadProgressBar";
            this.VideoLoadProgressBar.Size = new System.Drawing.Size(158, 23);
            this.VideoLoadProgressBar.TabIndex = 1;
            this.VideoLoadProgressBar.UseWaitCursor = true;
            // 
            // LT
            // 
            this.LT.AutoSize = true;
            this.LT.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LT.Location = new System.Drawing.Point(93, 36);
            this.LT.Name = "LT";
            this.LT.Size = new System.Drawing.Size(185, 42);
            this.LT.TabIndex = 2;
            this.LT.Text = "LOADING";
            // 
            // VLoadWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 178);
            this.Controls.Add(this.LT);
            this.Controls.Add(this.VideoLoadProgressBar);
            this.Name = "VLoadWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading Video";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ProgressBar VideoLoadProgressBar;
        public System.Windows.Forms.Label LT;
    }
}