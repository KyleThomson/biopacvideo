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
            this.DirListView = new System.Windows.Forms.ListView();
            this.Files = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PerVals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // DirListBox
            // 
            this.DirListBox.Enabled = false;
            this.DirListBox.FormattingEnabled = true;
            this.DirListBox.Location = new System.Drawing.Point(390, 51);
            this.DirListBox.Name = "DirListBox";
            this.DirListBox.ScrollAlwaysVisible = true;
            this.DirListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DirListBox.Size = new System.Drawing.Size(311, 459);
            this.DirListBox.TabIndex = 0;
            this.DirListBox.Visible = false;
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
            // DirListView
            // 
            this.DirListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Files,
            this.PerVals});
            this.DirListView.GridLines = true;
            this.DirListView.HideSelection = false;
            this.DirListView.Location = new System.Drawing.Point(12, 51);
            this.DirListView.MinimumSize = new System.Drawing.Size(318, 459);
            this.DirListView.Name = "DirListView";
            this.DirListView.Size = new System.Drawing.Size(372, 459);
            this.DirListView.TabIndex = 5;
            this.DirListView.UseCompatibleStateImageBehavior = false;
            this.DirListView.View = System.Windows.Forms.View.Details;
            this.DirListView.DoubleClick += new System.EventHandler(this.DirListBox_DoubleClick);
            // 
            // Files
            // 
            this.Files.Text = "Files";
            this.Files.Width = 320;
            // 
            // PerVals
            // 
            this.PerVals.Text = "%";
            this.PerVals.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PerVals.Width = 45;
            // 
            // MultiDirectoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 557);
            this.Controls.Add(this.DirListView);
            this.Controls.Add(this.Drives);
            this.Controls.Add(this.OKbttn);
            this.Controls.Add(this.CnclButton);
            this.Controls.Add(this.DirListBox);
            this.MinimumSize = new System.Drawing.Size(418, 596);
            this.Name = "MultiDirectoryAdd";
            this.Text = "MultiDirectoryAdd";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox DirListBox;
        private System.Windows.Forms.Button CnclButton;
        private System.Windows.Forms.Button OKbttn;
        private System.Windows.Forms.ComboBox Drives;
        private System.Windows.Forms.ColumnHeader Files;
        public System.Windows.Forms.ListView DirListView;
        public System.Windows.Forms.ColumnHeader PerVals;
    }
}