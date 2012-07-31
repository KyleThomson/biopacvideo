namespace SeizurePlayback
{
    partial class OpenFrm
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
            this.FileLabel = new System.Windows.Forms.Label();
            this.LastReviewLabel = new System.Windows.Forms.Label();
            this.LastOpenLabel = new System.Windows.Forms.Label();
            this.Reviewing = new System.Windows.Forms.CheckBox();
            this.OKbutton = new System.Windows.Forms.Button();
            this.Reviewer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PercentLabel = new System.Windows.Forms.Label();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.WarningLabel = new System.Windows.Forms.Label();
            this.CompLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileLabel.Location = new System.Drawing.Point(12, 9);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(88, 20);
            this.FileLabel.TabIndex = 0;
            this.FileLabel.Text = "File Name: ";
            // 
            // LastReviewLabel
            // 
            this.LastReviewLabel.AutoSize = true;
            this.LastReviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastReviewLabel.Location = new System.Drawing.Point(12, 75);
            this.LastReviewLabel.Name = "LastReviewLabel";
            this.LastReviewLabel.Size = new System.Drawing.Size(160, 20);
            this.LastReviewLabel.TabIndex = 1;
            this.LastReviewLabel.Text = "Review completed on";
            // 
            // LastOpenLabel
            // 
            this.LastOpenLabel.AutoSize = true;
            this.LastOpenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastOpenLabel.Location = new System.Drawing.Point(12, 95);
            this.LastOpenLabel.Name = "LastOpenLabel";
            this.LastOpenLabel.Size = new System.Drawing.Size(120, 20);
            this.LastOpenLabel.TabIndex = 2;
            this.LastOpenLabel.Text = "Last opened on";
            // 
            // Reviewing
            // 
            this.Reviewing.AutoSize = true;
            this.Reviewing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reviewing.Location = new System.Drawing.Point(12, 237);
            this.Reviewing.Name = "Reviewing";
            this.Reviewing.Size = new System.Drawing.Size(100, 24);
            this.Reviewing.TabIndex = 3;
            this.Reviewing.Text = "Reviewing";
            this.Reviewing.UseVisualStyleBackColor = true;
            // 
            // OKbutton
            // 
            this.OKbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKbutton.Location = new System.Drawing.Point(333, 237);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 34);
            this.OKbutton.TabIndex = 4;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // Reviewer
            // 
            this.Reviewer.Location = new System.Drawing.Point(12, 211);
            this.Reviewer.Name = "Reviewer";
            this.Reviewer.Size = new System.Drawing.Size(396, 20);
            this.Reviewer.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Reviewer(s)";
            // 
            // PercentLabel
            // 
            this.PercentLabel.AutoSize = true;
            this.PercentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PercentLabel.Location = new System.Drawing.Point(12, 55);
            this.PercentLabel.Name = "PercentLabel";
            this.PercentLabel.Size = new System.Drawing.Size(148, 20);
            this.PercentLabel.TabIndex = 7;
            this.PercentLabel.Text = "Percent Completion";
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesLabel.Location = new System.Drawing.Point(12, 115);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(51, 20);
            this.NotesLabel.TabIndex = 8;
            this.NotesLabel.Text = "Notes";
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WarningLabel.ForeColor = System.Drawing.Color.Red;
            this.WarningLabel.Location = new System.Drawing.Point(12, 168);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(68, 20);
            this.WarningLabel.TabIndex = 9;
            this.WarningLabel.Text = "Warning";
            // 
            // CompLabel
            // 
            this.CompLabel.AutoSize = true;
            this.CompLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompLabel.Location = new System.Drawing.Point(12, 148);
            this.CompLabel.Name = "CompLabel";
            this.CompLabel.Size = new System.Drawing.Size(99, 20);
            this.CompLabel.TabIndex = 10;
            this.CompLabel.Text = "Compressed";
            // 
            // OpenFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 280);
            this.Controls.Add(this.CompLabel);
            this.Controls.Add(this.WarningLabel);
            this.Controls.Add(this.NotesLabel);
            this.Controls.Add(this.PercentLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Reviewer);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.Reviewing);
            this.Controls.Add(this.LastOpenLabel);
            this.Controls.Add(this.LastReviewLabel);
            this.Controls.Add(this.FileLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFrm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Opening information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Label LastReviewLabel;
        private System.Windows.Forms.Label LastOpenLabel;
        private System.Windows.Forms.CheckBox Reviewing;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.TextBox Reviewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PercentLabel;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.Label WarningLabel;
        private System.Windows.Forms.Label CompLabel;
    }
}