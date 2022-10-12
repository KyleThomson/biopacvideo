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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SzRvwFrm));
            this.SzBox = new System.Windows.Forms.ListBox();
            this.SzListView = new System.Windows.Forms.ListView();
            this.Channel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.szNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Notes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Output = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Score = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IndexSZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // SzBox
            // 
            this.SzBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SzBox.Enabled = false;
            this.SzBox.FormattingEnabled = true;
            this.SzBox.HorizontalScrollbar = true;
            this.SzBox.Location = new System.Drawing.Point(12, 10);
            this.SzBox.Name = "SzBox";
            this.SzBox.Size = new System.Drawing.Size(192, 199);
            this.SzBox.TabIndex = 0;
            this.SzBox.Visible = false;
            this.SzBox.SelectedIndexChanged += new System.EventHandler(this.SzBox_SelectedIndexChanged);
            this.SzBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SzBox_MouseDown);
            // 
            // SzListView
            // 
            this.SzListView.AllowColumnReorder = true;
            this.SzListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Channel,
            this.AName,
            this.szNum,
            this.TimeStamp,
            this.Length,
            this.Notes,
            this.Output,
            this.Score,
            this.IndexSZ});
            this.SzListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SzListView.FullRowSelect = true;
            this.SzListView.HideSelection = false;
            this.SzListView.Location = new System.Drawing.Point(12, 10);
            this.SzListView.MinimumSize = new System.Drawing.Size(438, 186);
            this.SzListView.MultiSelect = false;
            this.SzListView.Name = "SzListView";
            this.SzListView.Size = new System.Drawing.Size(1092, 213);
            this.SzListView.TabIndex = 2;
            this.SzListView.UseCompatibleStateImageBehavior = false;
            this.SzListView.View = System.Windows.Forms.View.Details;
            this.SzListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SzListView_ColumnClick);
            this.SzListView.SelectedIndexChanged += new System.EventHandler(this.SzBox_SelectedIndexChanged);
            this.SzListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SzView_MouseDown);
            // 
            // Channel
            // 
            this.Channel.Text = "Ch";
            this.Channel.Width = 34;
            // 
            // AName
            // 
            this.AName.Text = "Animal";
            this.AName.Width = 113;
            // 
            // szNum
            // 
            this.szNum.Text = "Sz#";
            this.szNum.Width = 38;
            // 
            // TimeStamp
            // 
            this.TimeStamp.Text = "Sz Time";
            this.TimeStamp.Width = 87;
            // 
            // Length
            // 
            this.Length.Text = "(s)";
            this.Length.Width = 35;
            // 
            // Notes
            // 
            this.Notes.Text = "Notes";
            this.Notes.Width = 254;
            // 
            // Output
            // 
            this.Output.Text = "File";
            this.Output.Width = 0;
            // 
            // Score
            // 
            this.Score.Text = "Score";
            this.Score.Width = 57;
            // 
            // IndexSZ
            // 
            this.IndexSZ.Text = "Index";
            // 
            // SzRvwFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1130, 235);
            this.Controls.Add(this.SzListView);
            this.Controls.Add(this.SzBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(482, 254);
            this.Name = "SzRvwFrm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Seizure Review";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SzRvwFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SzBox;
        private System.Windows.Forms.ColumnHeader Channel;
        private System.Windows.Forms.ColumnHeader AName;
        private System.Windows.Forms.ColumnHeader szNum;
        private System.Windows.Forms.ColumnHeader TimeStamp;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.ColumnHeader Notes;
        private System.Windows.Forms.ColumnHeader Output;
        private System.Windows.Forms.ColumnHeader Score;
        public System.Windows.Forms.ListView SzListView;
        private System.Windows.Forms.ColumnHeader IndexSZ;
    }
}