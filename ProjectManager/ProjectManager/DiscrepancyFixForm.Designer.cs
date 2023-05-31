
namespace ProjectManager
{
    partial class DiscrepancyFixForm
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
            this.ConflictingList = new System.Windows.Forms.ListView();
            this.conflictName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictBubble = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conflictNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.notesLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.myVLC = new LibVLCSharp.WinForms.VideoView();
            this.GalGBox = new System.Windows.Forms.GroupBox();
            this.ZoomBar = new System.Windows.Forms.TrackBar();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.scoreGroupBox = new System.Windows.Forms.GroupBox();
            this.submitChangeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.editNotesButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.telem = new System.Windows.Forms.CheckBox();
            this.showBuffer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.scoreGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConflictingList
            // 
            this.ConflictingList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ConflictingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.conflictName,
            this.conflictDate,
            this.conflictBubble,
            this.conflictNotes});
            this.ConflictingList.FullRowSelect = true;
            this.ConflictingList.HideSelection = false;
            this.ConflictingList.Location = new System.Drawing.Point(12, 12);
            this.ConflictingList.MultiSelect = false;
            this.ConflictingList.Name = "ConflictingList";
            this.ConflictingList.Size = new System.Drawing.Size(377, 311);
            this.ConflictingList.TabIndex = 34;
            this.ConflictingList.UseCompatibleStateImageBehavior = false;
            this.ConflictingList.View = System.Windows.Forms.View.Details;
            this.ConflictingList.SelectedIndexChanged += new System.EventHandler(this.ConflictingList_SelectedIndexChanged);
            // 
            // conflictName
            // 
            this.conflictName.Text = "Animal";
            this.conflictName.Width = 87;
            // 
            // conflictDate
            // 
            this.conflictDate.Text = "Date";
            this.conflictDate.Width = 112;
            // 
            // conflictBubble
            // 
            this.conflictBubble.Text = "Bubble Score";
            this.conflictBubble.Width = 91;
            // 
            // conflictNotes
            // 
            this.conflictNotes.Text = "Notes Score";
            this.conflictNotes.Width = 80;
            // 
            // notesLabel
            // 
            this.notesLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.notesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notesLabel.Location = new System.Drawing.Point(12, 366);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(377, 65);
            this.notesLabel.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Notes:";
            // 
            // myVLC
            // 
            this.myVLC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.myVLC.Enabled = false;
            this.myVLC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.myVLC.Location = new System.Drawing.Point(458, 12);
            this.myVLC.MediaPlayer = null;
            this.myVLC.Name = "myVLC";
            this.myVLC.Size = new System.Drawing.Size(422, 284);
            this.myVLC.TabIndex = 37;
            this.myVLC.Click += new System.EventHandler(this.myVLC_Click);
            // 
            // GalGBox
            // 
            this.GalGBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GalGBox.Location = new System.Drawing.Point(410, 302);
            this.GalGBox.Name = "GalGBox";
            this.GalGBox.Size = new System.Drawing.Size(518, 142);
            this.GalGBox.TabIndex = 38;
            this.GalGBox.TabStop = false;
            // 
            // ZoomBar
            // 
            this.ZoomBar.AutoSize = false;
            this.ZoomBar.Location = new System.Drawing.Point(755, 518);
            this.ZoomBar.Maximum = 20;
            this.ZoomBar.Name = "ZoomBar";
            this.ZoomBar.Size = new System.Drawing.Size(173, 21);
            this.ZoomBar.TabIndex = 39;
            this.ZoomBar.TabStop = false;
            this.ZoomBar.TickFrequency = 2;
            this.ZoomBar.Value = 10;
            this.ZoomBar.Scroll += new System.EventHandler(this.ZoomBar_Scroll);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Enabled = false;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.TimeLabel.Location = new System.Drawing.Point(407, 483);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(87, 18);
            this.TimeLabel.TabIndex = 40;
            this.TimeLabel.Text = "0:00 / 0:00";
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(410, 515);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(46, 23);
            this.PlayButton.TabIndex = 41;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(462, 515);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(46, 23);
            this.PauseButton.TabIndex = 42;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomLabel.AutoSize = true;
            this.ZoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.ZoomLabel.Location = new System.Drawing.Point(819, 497);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(48, 18);
            this.ZoomLabel.TabIndex = 43;
            this.ZoomLabel.Text = "Zoom";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(403, 449);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(525, 23);
            this.trackBar1.TabIndex = 44;
            // 
            // scoreGroupBox
            // 
            this.scoreGroupBox.Controls.Add(this.submitChangeButton);
            this.scoreGroupBox.Controls.Add(this.label4);
            this.scoreGroupBox.Controls.Add(this.label2);
            this.scoreGroupBox.Controls.Add(this.radioButton7);
            this.scoreGroupBox.Controls.Add(this.radioButton6);
            this.scoreGroupBox.Controls.Add(this.radioButton5);
            this.scoreGroupBox.Controls.Add(this.radioButton4);
            this.scoreGroupBox.Controls.Add(this.radioButton3);
            this.scoreGroupBox.Controls.Add(this.radioButton2);
            this.scoreGroupBox.Controls.Add(this.radioButton1);
            this.scoreGroupBox.Controls.Add(this.label3);
            this.scoreGroupBox.Controls.Add(this.panel1);
            this.scoreGroupBox.Enabled = false;
            this.scoreGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            this.scoreGroupBox.Location = new System.Drawing.Point(12, 439);
            this.scoreGroupBox.Name = "scoreGroupBox";
            this.scoreGroupBox.Size = new System.Drawing.Size(377, 110);
            this.scoreGroupBox.TabIndex = 45;
            this.scoreGroupBox.TabStop = false;
            this.scoreGroupBox.Text = "Update Score";
            // 
            // submitChangeButton
            // 
            this.submitChangeButton.AutoSize = true;
            this.submitChangeButton.ForeColor = System.Drawing.Color.Black;
            this.submitChangeButton.Location = new System.Drawing.Point(282, 81);
            this.submitChangeButton.Name = "submitChangeButton";
            this.submitChangeButton.Size = new System.Drawing.Size(89, 23);
            this.submitChangeButton.TabIndex = 12;
            this.submitChangeButton.Text = "Submit Change";
            this.submitChangeButton.UseVisualStyleBackColor = true;
            this.submitChangeButton.Click += new System.EventHandler(this.submitChangeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Other (Specify in Notes)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Noconvulsive";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton7.Location = new System.Drawing.Point(302, 53);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(40, 17);
            this.radioButton7.TabIndex = 8;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "NA";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton6.Location = new System.Drawing.Point(111, 54);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(31, 17);
            this.radioButton6.TabIndex = 7;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "0";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton5.Location = new System.Drawing.Point(319, 23);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(31, 17);
            this.radioButton5.TabIndex = 6;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "5";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton4.Location = new System.Drawing.Point(282, 23);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(31, 17);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton3.Location = new System.Drawing.Point(248, 23);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(31, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton2.Location = new System.Drawing.Point(211, 23);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(31, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButton1.Location = new System.Drawing.Point(174, 23);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(31, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(34, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Standard Racine Scale";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 28);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(157, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 28);
            this.panel2.TabIndex = 14;
            // 
            // editNotesButton
            // 
            this.editNotesButton.AutoSize = true;
            this.editNotesButton.ForeColor = System.Drawing.Color.Black;
            this.editNotesButton.Location = new System.Drawing.Point(323, 340);
            this.editNotesButton.Name = "editNotesButton";
            this.editNotesButton.Size = new System.Drawing.Size(66, 23);
            this.editNotesButton.TabIndex = 11;
            this.editNotesButton.Text = "Edit Notes";
            this.editNotesButton.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.telem);
            this.panel3.Controls.Add(this.showBuffer);
            this.panel3.Location = new System.Drawing.Point(395, -4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(545, 571);
            this.panel3.TabIndex = 46;
            // 
            // telem
            // 
            this.telem.AutoSize = true;
            this.telem.ForeColor = System.Drawing.SystemColors.Control;
            this.telem.Location = new System.Drawing.Point(254, 500);
            this.telem.Name = "telem";
            this.telem.Size = new System.Drawing.Size(72, 17);
            this.telem.TabIndex = 1;
            this.telem.Text = "Telemetry";
            this.telem.UseVisualStyleBackColor = true;
            this.telem.CheckedChanged += new System.EventHandler(this.telem_CheckedChanged);
            // 
            // showBuffer
            // 
            this.showBuffer.AutoSize = true;
            this.showBuffer.Checked = true;
            this.showBuffer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBuffer.ForeColor = System.Drawing.SystemColors.Control;
            this.showBuffer.Location = new System.Drawing.Point(254, 518);
            this.showBuffer.Name = "showBuffer";
            this.showBuffer.Size = new System.Drawing.Size(84, 17);
            this.showBuffer.TabIndex = 0;
            this.showBuffer.Text = "Show Buffer";
            this.showBuffer.UseVisualStyleBackColor = true;
            this.showBuffer.CheckedChanged += new System.EventHandler(this.showBuffer_CheckedChanged);
            // 
            // DiscrepancyFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(944, 561);
            this.Controls.Add(this.GalGBox);
            this.Controls.Add(this.scoreGroupBox);
            this.Controls.Add(this.editNotesButton);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.ZoomLabel);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.myVLC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.ConflictingList);
            this.Controls.Add(this.panel3);
            this.Name = "DiscrepancyFixForm";
            this.Text = "DiscrepancyFixForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DiscrepancyFixForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.scoreGroupBox.ResumeLayout(false);
            this.scoreGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ConflictingList;
        private System.Windows.Forms.ColumnHeader conflictName;
        private System.Windows.Forms.ColumnHeader conflictDate;
        private System.Windows.Forms.ColumnHeader conflictBubble;
        private System.Windows.Forms.ColumnHeader conflictNotes;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.Label label1;
        public LibVLCSharp.WinForms.VideoView myVLC;
        private System.Windows.Forms.GroupBox GalGBox;
        private System.Windows.Forms.TrackBar ZoomBar;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox scoreGroupBox;
        private System.Windows.Forms.Button submitChangeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button editNotesButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox showBuffer;
        private System.Windows.Forms.CheckBox telem;
    }
}