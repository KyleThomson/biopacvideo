namespace SeizurePlayback
{
    partial class Compression
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
            this.TotProgress = new System.Windows.Forms.ProgressBar();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.CurFileProg = new System.Windows.Forms.ProgressBar();
            this.CurrentLabel = new System.Windows.Forms.Label();
            this.FailCountLbl = new System.Windows.Forms.Label();
            this.StartComp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TotProgress
            // 
            this.TotProgress.Location = new System.Drawing.Point(12, 25);
            this.TotProgress.Name = "TotProgress";
            this.TotProgress.Size = new System.Drawing.Size(419, 23);
            this.TotProgress.TabIndex = 0;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(12, 9);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(78, 13);
            this.TotalLabel.TabIndex = 1;
            this.TotalLabel.Text = "Total Progress:";
            // 
            // CurFileProg
            // 
            this.CurFileProg.Location = new System.Drawing.Point(12, 78);
            this.CurFileProg.Name = "CurFileProg";
            this.CurFileProg.Size = new System.Drawing.Size(419, 23);
            this.CurFileProg.TabIndex = 2;
            // 
            // CurrentLabel
            // 
            this.CurrentLabel.AutoSize = true;
            this.CurrentLabel.Location = new System.Drawing.Point(9, 62);
            this.CurrentLabel.Name = "CurrentLabel";
            this.CurrentLabel.Size = new System.Drawing.Size(66, 13);
            this.CurrentLabel.TabIndex = 3;
            this.CurrentLabel.Text = "Current File: ";
            // 
            // FailCountLbl
            // 
            this.FailCountLbl.AutoSize = true;
            this.FailCountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailCountLbl.Location = new System.Drawing.Point(9, 104);
            this.FailCountLbl.Name = "FailCountLbl";
            this.FailCountLbl.Size = new System.Drawing.Size(48, 9);
            this.FailCountLbl.TabIndex = 4;
            this.FailCountLbl.Text = "Fail Count: 0";
            // 
            // StartComp
            // 
            this.StartComp.Location = new System.Drawing.Point(437, 12);
            this.StartComp.Name = "StartComp";
            this.StartComp.Size = new System.Drawing.Size(103, 23);
            this.StartComp.TabIndex = 5;
            this.StartComp.Text = "Start Compression";
            this.StartComp.UseVisualStyleBackColor = true;
            this.StartComp.Click += new System.EventHandler(this.button1_Click);
            // 
            // Compression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 134);
            this.Controls.Add(this.StartComp);
            this.Controls.Add(this.FailCountLbl);
            this.Controls.Add(this.CurrentLabel);
            this.Controls.Add(this.CurFileProg);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.TotProgress);
            this.Name = "Compression";
            this.Text = "Compression Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar TotProgress;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.ProgressBar CurFileProg;
        private System.Windows.Forms.Label CurrentLabel;
        private System.Windows.Forms.Label FailCountLbl;
        private System.Windows.Forms.Button StartComp;

    }
}