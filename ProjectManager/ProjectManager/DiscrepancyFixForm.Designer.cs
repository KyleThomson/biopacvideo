﻿
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.scaleButtonDravet = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.scaleButton0 = new System.Windows.Forms.RadioButton();
            this.scaleButton5pop = new System.Windows.Forms.RadioButton();
            this.scaleButtonStatus = new System.Windows.Forms.RadioButton();
            this.scaleButtonNA = new System.Windows.Forms.RadioButton();
            this.submitChangeButton = new System.Windows.Forms.Button();
            this.scaleButton5 = new System.Windows.Forms.RadioButton();
            this.scaleButton4 = new System.Windows.Forms.RadioButton();
            this.scaleButton3 = new System.Windows.Forms.RadioButton();
            this.scaleButton2 = new System.Windows.Forms.RadioButton();
            this.scaleButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.editNotesButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.telem = new System.Windows.Forms.CheckBox();
            this.showBuffer = new System.Windows.Forms.CheckBox();
            this.notesBox = new System.Windows.Forms.TextBox();
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
            this.conflictDate.Width = 105;
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
            this.scoreGroupBox.Controls.Add(this.panel1);
            this.scoreGroupBox.Controls.Add(this.scaleButton5);
            this.scoreGroupBox.Controls.Add(this.scaleButton4);
            this.scoreGroupBox.Controls.Add(this.scaleButton3);
            this.scoreGroupBox.Controls.Add(this.scaleButton2);
            this.scoreGroupBox.Controls.Add(this.scaleButton1);
            this.scoreGroupBox.Controls.Add(this.label3);
            this.scoreGroupBox.Enabled = false;
            this.scoreGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            this.scoreGroupBox.Location = new System.Drawing.Point(12, 439);
            this.scoreGroupBox.Name = "scoreGroupBox";
            this.scoreGroupBox.Size = new System.Drawing.Size(377, 110);
            this.scoreGroupBox.TabIndex = 45;
            this.scoreGroupBox.TabStop = false;
            this.scoreGroupBox.Text = "Update Score";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.scaleButtonDravet);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.scaleButton0);
            this.panel1.Controls.Add(this.scaleButton5pop);
            this.panel1.Controls.Add(this.scaleButtonStatus);
            this.panel1.Controls.Add(this.scaleButtonNA);
            this.panel1.Controls.Add(this.submitChangeButton);
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 66);
            this.panel1.TabIndex = 22;
            // 
            // scaleButtonDravet
            // 
            this.scaleButtonDravet.AutoSize = true;
            this.scaleButtonDravet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButtonDravet.ForeColor = System.Drawing.SystemColors.Control;
            this.scaleButtonDravet.Location = new System.Drawing.Point(162, 9);
            this.scaleButtonDravet.Name = "scaleButtonDravet";
            this.scaleButtonDravet.Size = new System.Drawing.Size(57, 17);
            this.scaleButtonDravet.TabIndex = 27;
            this.scaleButtonDravet.TabStop = true;
            this.scaleButtonDravet.Text = "Dravet";
            this.scaleButtonDravet.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(21, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Other";
            // 
            // scaleButton0
            // 
            this.scaleButton0.AutoSize = true;
            this.scaleButton0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scaleButton0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton0.ForeColor = System.Drawing.SystemColors.Control;
            this.scaleButton0.Location = new System.Drawing.Point(240, 9);
            this.scaleButton0.Name = "scaleButton0";
            this.scaleButton0.Size = new System.Drawing.Size(111, 17);
            this.scaleButton0.TabIndex = 24;
            this.scaleButton0.TabStop = true;
            this.scaleButton0.Text = "0 (Nonconvulsive)";
            this.scaleButton0.UseVisualStyleBackColor = false;
            // 
            // scaleButton5pop
            // 
            this.scaleButton5pop.AutoSize = true;
            this.scaleButton5pop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton5pop.ForeColor = System.Drawing.SystemColors.Control;
            this.scaleButton5pop.Location = new System.Drawing.Point(60, 9);
            this.scaleButton5pop.Name = "scaleButton5pop";
            this.scaleButton5pop.Size = new System.Drawing.Size(89, 17);
            this.scaleButton5pop.TabIndex = 26;
            this.scaleButton5pop.TabStop = true;
            this.scaleButton5pop.Text = "5 w/ popcorn";
            this.scaleButton5pop.UseVisualStyleBackColor = true;
            // 
            // scaleButtonStatus
            // 
            this.scaleButtonStatus.AutoSize = true;
            this.scaleButtonStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButtonStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.scaleButtonStatus.Location = new System.Drawing.Point(25, 35);
            this.scaleButtonStatus.Name = "scaleButtonStatus";
            this.scaleButtonStatus.Size = new System.Drawing.Size(55, 17);
            this.scaleButtonStatus.TabIndex = 23;
            this.scaleButtonStatus.TabStop = true;
            this.scaleButtonStatus.Text = "Status";
            this.scaleButtonStatus.UseVisualStyleBackColor = true;
            // 
            // scaleButtonNA
            // 
            this.scaleButtonNA.AutoSize = true;
            this.scaleButtonNA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButtonNA.ForeColor = System.Drawing.SystemColors.Control;
            this.scaleButtonNA.Location = new System.Drawing.Point(99, 36);
            this.scaleButtonNA.Name = "scaleButtonNA";
            this.scaleButtonNA.Size = new System.Drawing.Size(131, 17);
            this.scaleButtonNA.TabIndex = 22;
            this.scaleButtonNA.TabStop = true;
            this.scaleButtonNA.Text = "N/A (Specify in Notes)";
            this.scaleButtonNA.UseVisualStyleBackColor = true;
            // 
            // submitChangeButton
            // 
            this.submitChangeButton.AutoSize = true;
            this.submitChangeButton.ForeColor = System.Drawing.Color.Black;
            this.submitChangeButton.Location = new System.Drawing.Point(246, 32);
            this.submitChangeButton.Name = "submitChangeButton";
            this.submitChangeButton.Size = new System.Drawing.Size(114, 27);
            this.submitChangeButton.TabIndex = 12;
            this.submitChangeButton.Text = "Submit Change";
            this.submitChangeButton.UseVisualStyleBackColor = true;
            this.submitChangeButton.Click += new System.EventHandler(this.submitChangeButton_Click);
            // 
            // scaleButton5
            // 
            this.scaleButton5.AutoSize = true;
            this.scaleButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton5.Location = new System.Drawing.Point(319, 21);
            this.scaleButton5.Name = "scaleButton5";
            this.scaleButton5.Size = new System.Drawing.Size(31, 17);
            this.scaleButton5.TabIndex = 6;
            this.scaleButton5.TabStop = true;
            this.scaleButton5.Text = "5";
            this.scaleButton5.UseVisualStyleBackColor = true;
            // 
            // scaleButton4
            // 
            this.scaleButton4.AutoSize = true;
            this.scaleButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton4.Location = new System.Drawing.Point(282, 21);
            this.scaleButton4.Name = "scaleButton4";
            this.scaleButton4.Size = new System.Drawing.Size(31, 17);
            this.scaleButton4.TabIndex = 5;
            this.scaleButton4.TabStop = true;
            this.scaleButton4.Text = "4";
            this.scaleButton4.UseVisualStyleBackColor = true;
            // 
            // scaleButton3
            // 
            this.scaleButton3.AutoSize = true;
            this.scaleButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton3.Location = new System.Drawing.Point(248, 21);
            this.scaleButton3.Name = "scaleButton3";
            this.scaleButton3.Size = new System.Drawing.Size(31, 17);
            this.scaleButton3.TabIndex = 4;
            this.scaleButton3.TabStop = true;
            this.scaleButton3.Text = "3";
            this.scaleButton3.UseVisualStyleBackColor = true;
            // 
            // scaleButton2
            // 
            this.scaleButton2.AutoSize = true;
            this.scaleButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton2.Location = new System.Drawing.Point(211, 21);
            this.scaleButton2.Name = "scaleButton2";
            this.scaleButton2.Size = new System.Drawing.Size(31, 17);
            this.scaleButton2.TabIndex = 3;
            this.scaleButton2.TabStop = true;
            this.scaleButton2.Text = "2";
            this.scaleButton2.UseVisualStyleBackColor = true;
            // 
            // scaleButton1
            // 
            this.scaleButton1.AutoSize = true;
            this.scaleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleButton1.Location = new System.Drawing.Point(174, 21);
            this.scaleButton1.Name = "scaleButton1";
            this.scaleButton1.Size = new System.Drawing.Size(31, 17);
            this.scaleButton1.TabIndex = 2;
            this.scaleButton1.TabStop = true;
            this.scaleButton1.Text = "1";
            this.scaleButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.scaleButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(20, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Standard Racine Scale";
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
            this.editNotesButton.Click += new System.EventHandler(this.editNotesButton_Click);
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
            // notesBox
            // 
            this.notesBox.AcceptsReturn = true;
            this.notesBox.Location = new System.Drawing.Point(12, 368);
            this.notesBox.Multiline = true;
            this.notesBox.Name = "notesBox";
            this.notesBox.Size = new System.Drawing.Size(377, 65);
            this.notesBox.TabIndex = 47;
            // 
            // DiscrepancyFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(944, 561);
            this.Controls.Add(this.notesBox);
            this.Controls.Add(this.GalGBox);
            this.Controls.Add(this.editNotesButton);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.ZoomLabel);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.ZoomBar);
            this.Controls.Add(this.myVLC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConflictingList);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.scoreGroupBox);
            this.Name = "DiscrepancyFixForm";
            this.Text = "DiscrepancyFixForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DiscrepancyFixForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.myVLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.scoreGroupBox.ResumeLayout(false);
            this.scoreGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.RadioButton scaleButton5;
        private System.Windows.Forms.RadioButton scaleButton4;
        private System.Windows.Forms.RadioButton scaleButton3;
        private System.Windows.Forms.RadioButton scaleButton2;
        private System.Windows.Forms.RadioButton scaleButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button editNotesButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox showBuffer;
        private System.Windows.Forms.CheckBox telem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton scaleButtonDravet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton scaleButton0;
        private System.Windows.Forms.RadioButton scaleButton5pop;
        private System.Windows.Forms.RadioButton scaleButtonStatus;
        private System.Windows.Forms.RadioButton scaleButtonNA;
        private System.Windows.Forms.TextBox notesBox;
    }
}