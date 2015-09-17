namespace ProjectManager
{
    partial class MultiDirectoryAdd
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
            this.DirListBox = new System.Windows.Forms.ListBox();
            this.CnclButton = new System.Windows.Forms.Button();
            this.OKbttn = new System.Windows.Forms.Button();
            this.Drives = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DirListBox
            // 
            this.DirListBox.FormattingEnabled = true;
            this.DirListBox.Location = new System.Drawing.Point(12, 51);
            this.DirListBox.Name = "DirListBox";
            this.DirListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DirListBox.Size = new System.Drawing.Size(311, 459);
            this.DirListBox.TabIndex = 0;
            this.DirListBox.SelectedIndexChanged += new System.EventHandler(this.DirListBox_SelectedIndexChanged);
            this.DirListBox.DoubleClick += new System.EventHandler(this.DirListBox_DoubleClick);
            // 
            // CnclButton
            // 
            this.CnclButton.Location = new System.Drawing.Point(248, 522);
            this.CnclButton.Name = "CnclButton";
            this.CnclButton.Size = new System.Drawing.Size(75, 23);
            this.CnclButton.TabIndex = 1;
            this.CnclButton.Text = "Cancel";
            this.CnclButton.UseVisualStyleBackColor = true;
            this.CnclButton.Click += new System.EventHandler(this.CnclButton_Click);
            // 
            // OKbttn
            // 
            this.OKbttn.Location = new System.Drawing.Point(167, 522);
            this.OKbttn.Name = "OKbttn";
            this.OKbttn.Size = new System.Drawing.Size(75, 23);
            this.OKbttn.TabIndex = 2;
            this.OKbttn.Text = "OK";
            this.OKbttn.UseVisualStyleBackColor = true;
            this.OKbttn.Click += new System.EventHandler(this.OKbttn_Click);
            // 
            // Drives
            // 
            this.Drives.FormattingEnabled = true;
            this.Drives.Location = new System.Drawing.Point(12, 24);
            this.Drives.Name = "Drives";
            this.Drives.Size = new System.Drawing.Size(311, 21);
            this.Drives.TabIndex = 3;
            this.Drives.SelectedIndexChanged += new System.EventHandler(this.Drives_SelectedIndexChanged);
            // 
            // MultiDirectoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 557);
            this.Controls.Add(this.Drives);
            this.Controls.Add(this.OKbttn);
            this.Controls.Add(this.CnclButton);
            this.Controls.Add(this.DirListBox);
            this.Name = "MultiDirectoryAdd";
            this.Text = "MultiDirectoryAdd";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox DirListBox;
        private System.Windows.Forms.Button CnclButton;
        private System.Windows.Forms.Button OKbttn;
        private System.Windows.Forms.ComboBox Drives;
    }
}