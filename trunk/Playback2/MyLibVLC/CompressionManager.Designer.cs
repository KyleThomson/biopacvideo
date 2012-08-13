namespace SeizurePlayback
{
    partial class CompressionManager
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
            this.label1 = new System.Windows.Forms.Label();
            this.AddDir = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.FailCountLbl = new System.Windows.Forms.Label();
            this.CurrentLabel = new System.Windows.Forms.Label();
            this.CurFileProg = new System.Windows.Forms.ProgressBar();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.TotProgress = new System.Windows.Forms.ProgressBar();
            this.ClrList = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory List";
            // 
            // AddDir
            // 
            this.AddDir.Location = new System.Drawing.Point(334, 27);
            this.AddDir.Name = "AddDir";
            this.AddDir.Size = new System.Drawing.Size(100, 26);
            this.AddDir.TabIndex = 2;
            this.AddDir.Text = "Add Directory";
            this.AddDir.UseVisualStyleBackColor = true;
            this.AddDir.Click += new System.EventHandler(this.AddDir_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(334, 59);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 26);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(334, 91);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(100, 26);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // FailCountLbl
            // 
            this.FailCountLbl.AutoSize = true;
            this.FailCountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailCountLbl.Location = new System.Drawing.Point(10, 394);
            this.FailCountLbl.Name = "FailCountLbl";
            this.FailCountLbl.Size = new System.Drawing.Size(48, 9);
            this.FailCountLbl.TabIndex = 9;
            this.FailCountLbl.Text = "Fail Count: 0";
            // 
            // CurrentLabel
            // 
            this.CurrentLabel.AutoSize = true;
            this.CurrentLabel.Location = new System.Drawing.Point(9, 352);
            this.CurrentLabel.Name = "CurrentLabel";
            this.CurrentLabel.Size = new System.Drawing.Size(66, 13);
            this.CurrentLabel.TabIndex = 8;
            this.CurrentLabel.Text = "Current File: ";
            // 
            // CurFileProg
            // 
            this.CurFileProg.Location = new System.Drawing.Point(12, 368);
            this.CurFileProg.Name = "CurFileProg";
            this.CurFileProg.Size = new System.Drawing.Size(419, 23);
            this.CurFileProg.TabIndex = 7;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(12, 310);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(78, 13);
            this.TotalLabel.TabIndex = 6;
            this.TotalLabel.Text = "Total Progress:";
            // 
            // TotProgress
            // 
            this.TotProgress.Location = new System.Drawing.Point(12, 326);
            this.TotProgress.Name = "TotProgress";
            this.TotProgress.Size = new System.Drawing.Size(419, 23);
            this.TotProgress.TabIndex = 5;
            // 
            // ClrList
            // 
            this.ClrList.Location = new System.Drawing.Point(334, 155);
            this.ClrList.Name = "ClrList";
            this.ClrList.Size = new System.Drawing.Size(100, 26);
            this.ClrList.TabIndex = 10;
            this.ClrList.Text = "Clear List";
            this.ClrList.UseVisualStyleBackColor = true;
            this.ClrList.Click += new System.EventHandler(this.ClrList_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(334, 123);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(100, 26);
            this.Remove.TabIndex = 11;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            // 
            // FileList
            // 
            this.FileList.FormattingEnabled = true;
            this.FileList.Location = new System.Drawing.Point(15, 27);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(313, 277);
            this.FileList.TabIndex = 12;
            // 
            // CompressionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 412);
            this.Controls.Add(this.FileList);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.ClrList);
            this.Controls.Add(this.FailCountLbl);
            this.Controls.Add(this.CurrentLabel);
            this.Controls.Add(this.CurFileProg);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.TotProgress);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.AddDir);
            this.Controls.Add(this.label1);
            this.Name = "CompressionManager";
            this.Text = "Compression Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddDir;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label FailCountLbl;
        private System.Windows.Forms.Label CurrentLabel;
        private System.Windows.Forms.ProgressBar CurFileProg;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.ProgressBar TotProgress;
        private System.Windows.Forms.Button ClrList;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.ListBox FileList;
    }
}