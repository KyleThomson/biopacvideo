namespace SeizurePlayback
{
    partial class GetACQ
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
            this.StartComp = new System.Windows.Forms.Button();
            this.CurrentLabel = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.TotProgress = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.Destination = new System.Windows.Forms.TextBox();
            this.Source = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartComp
            // 
            this.StartComp.Location = new System.Drawing.Point(437, 77);
            this.StartComp.Name = "StartComp";
            this.StartComp.Size = new System.Drawing.Size(103, 23);
            this.StartComp.TabIndex = 9;
            this.StartComp.Text = "Start Copy";
            this.StartComp.UseVisualStyleBackColor = true;
            this.StartComp.Click += new System.EventHandler(this.StartComp_Click);
            // 
            // CurrentLabel
            // 
            this.CurrentLabel.AutoSize = true;
            this.CurrentLabel.Location = new System.Drawing.Point(12, 103);
            this.CurrentLabel.Name = "CurrentLabel";
            this.CurrentLabel.Size = new System.Drawing.Size(66, 13);
            this.CurrentLabel.TabIndex = 8;
            this.CurrentLabel.Text = "Current File: ";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(12, 61);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(78, 13);
            this.TotalLabel.TabIndex = 7;
            this.TotalLabel.Text = "Total Progress:";
            // 
            // TotProgress
            // 
            this.TotProgress.Location = new System.Drawing.Point(12, 77);
            this.TotProgress.Name = "TotProgress";
            this.TotProgress.Size = new System.Drawing.Size(419, 23);
            this.TotProgress.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Set Destination";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Destination
            // 
            this.Destination.Location = new System.Drawing.Point(12, 38);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(419, 20);
            this.Destination.TabIndex = 11;
            // 
            // Source
            // 
            this.Source.Location = new System.Drawing.Point(12, 12);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(419, 20);
            this.Source.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(437, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Set Source";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GetACQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 125);
            this.Controls.Add(this.Source);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartComp);
            this.Controls.Add(this.CurrentLabel);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.TotProgress);
            this.Name = "GetACQ";
            this.Text = "Download ACQ files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartComp;
        private System.Windows.Forms.Label CurrentLabel;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.ProgressBar TotProgress;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Destination;
        private System.Windows.Forms.TextBox Source;
        private System.Windows.Forms.Button button2;
    }
}