namespace SeizurePlayback
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
            this.CurFileProg.Location = new System.Drawing.Point(15, 80);
            this.CurFileProg.Name = "CurFileProg";
            this.CurFileProg.Size = new System.Drawing.Size(382, 25);
            this.CurFileProg.TabIndex = 3;
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
            // SzPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 115);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PleaseWait);
            this.Controls.Add(this.CurFileProg);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.NotesBx);
            this.Name = "SzPrompt";
            this.Text = "Capture Seizure";
            this.Load += new System.EventHandler(this.SzPrompt_Load);
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
    }
}