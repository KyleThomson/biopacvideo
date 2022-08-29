
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
            this.LoadVideoLabel = new System.Windows.Forms.Label();
            this.VideoLoadProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadVideoLabel
            // 
            this.LoadVideoLabel.AutoSize = true;
            this.LoadVideoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadVideoLabel.ForeColor = System.Drawing.Color.Red;
            this.LoadVideoLabel.Location = new System.Drawing.Point(149, 32);
            this.LoadVideoLabel.Name = "LoadVideoLabel";
            this.LoadVideoLabel.Size = new System.Drawing.Size(70, 25);
            this.LoadVideoLabel.TabIndex = 0;
            this.LoadVideoLabel.Text = "WAIT!";
            // 
            // VideoLoadProgressBar
            // 
            this.VideoLoadProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.VideoLoadProgressBar.Location = new System.Drawing.Point(100, 109);
            this.VideoLoadProgressBar.Name = "VideoLoadProgressBar";
            this.VideoLoadProgressBar.Size = new System.Drawing.Size(158, 23);
            this.VideoLoadProgressBar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Do Not Click Your Mouse Until This Prompt Has Closed";
            // 
            // VLoadWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 178);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VideoLoadProgressBar);
            this.Controls.Add(this.LoadVideoLabel);
            this.Name = "VLoadWait";
            this.Text = "Loading Video";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoadVideoLabel;
        public System.Windows.Forms.ProgressBar VideoLoadProgressBar;
        private System.Windows.Forms.Label label1;
    }
}