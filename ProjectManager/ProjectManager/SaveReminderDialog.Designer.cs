namespace ProjectManager
{
    partial class SaveReminderDialog
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
            this.Save = new System.Windows.Forms.Button();
            this.DontSave = new System.Windows.Forms.Button();
            this.CancelClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Do you want to save your changes?";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(26, 35);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(79, 21);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // DontSave
            // 
            this.DontSave.Location = new System.Drawing.Point(120, 35);
            this.DontSave.Name = "DontSave";
            this.DontSave.Size = new System.Drawing.Size(79, 21);
            this.DontSave.TabIndex = 2;
            this.DontSave.Text = "Don\'t Save";
            this.DontSave.UseVisualStyleBackColor = true;
            this.DontSave.Click += new System.EventHandler(this.DontSave_Click);
            // 
            // CancelClose
            // 
            this.CancelClose.Location = new System.Drawing.Point(215, 35);
            this.CancelClose.Name = "CancelClose";
            this.CancelClose.Size = new System.Drawing.Size(79, 21);
            this.CancelClose.TabIndex = 3;
            this.CancelClose.Text = "Cancel";
            this.CancelClose.UseVisualStyleBackColor = true;
            this.CancelClose.Click += new System.EventHandler(this.CancelClose_Click);
            // 
            // SaveReminderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 59);
            this.Controls.Add(this.CancelClose);
            this.Controls.Add(this.DontSave);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label1);
            this.Name = "SaveReminderDialog";
            this.Text = "SaveReminderDialog";
            this.Load += new System.EventHandler(this.SaveReminderDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button DontSave;
        private System.Windows.Forms.Button CancelClose;
    }
}