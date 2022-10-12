
namespace SeizurePlayback
{
    partial class ShortGuide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortGuide));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eEGManualReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eEGFastReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MRSC = new System.Windows.Forms.PictureBox();
            this.FRSC = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MRSC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRSC)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eEGManualReviewToolStripMenuItem,
            this.eEGFastReviewToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // eEGManualReviewToolStripMenuItem
            // 
            this.eEGManualReviewToolStripMenuItem.Checked = true;
            this.eEGManualReviewToolStripMenuItem.CheckOnClick = true;
            this.eEGManualReviewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eEGManualReviewToolStripMenuItem.Name = "eEGManualReviewToolStripMenuItem";
            resources.ApplyResources(this.eEGManualReviewToolStripMenuItem, "eEGManualReviewToolStripMenuItem");
            this.eEGManualReviewToolStripMenuItem.Click += new System.EventHandler(this.eEGManualReviewToolStripMenuItem_Click);
            // 
            // eEGFastReviewToolStripMenuItem
            // 
            this.eEGFastReviewToolStripMenuItem.CheckOnClick = true;
            this.eEGFastReviewToolStripMenuItem.Name = "eEGFastReviewToolStripMenuItem";
            resources.ApplyResources(this.eEGFastReviewToolStripMenuItem, "eEGFastReviewToolStripMenuItem");
            this.eEGFastReviewToolStripMenuItem.Click += new System.EventHandler(this.eEGFastReviewToolStripMenuItem_Click);
            // 
            // MRSC
            // 
            resources.ApplyResources(this.MRSC, "MRSC");
            this.MRSC.Name = "MRSC";
            this.MRSC.TabStop = false;
            // 
            // FRSC
            // 
            resources.ApplyResources(this.FRSC, "FRSC");
            this.FRSC.Name = "FRSC";
            this.FRSC.TabStop = false;
            // 
            // ShortGuide
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FRSC);
            this.Controls.Add(this.MRSC);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ShortGuide";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ShortGuide_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MRSC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRSC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eEGManualReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eEGFastReviewToolStripMenuItem;
        private System.Windows.Forms.PictureBox MRSC;
        private System.Windows.Forms.PictureBox FRSC;
    }
}