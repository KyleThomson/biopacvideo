namespace SeizurePlayback
{
    partial class SzRvwFrm
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
            this.SzBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // SzBox
            // 
            this.SzBox.FormattingEnabled = true;
            this.SzBox.Location = new System.Drawing.Point(12, 10);
            this.SzBox.Name = "SzBox";
            this.SzBox.Size = new System.Drawing.Size(304, 199);
            this.SzBox.TabIndex = 0;
            this.SzBox.SelectedIndexChanged += new System.EventHandler(this.SzBox_SelectedIndexChanged);
            this.SzBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SzBox_MouseDown);
            // 
            // SzRvwFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(328, 225);
            this.Controls.Add(this.SzBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SzRvwFrm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Seizure Review";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SzBox;
    }
}