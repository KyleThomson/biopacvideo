namespace ProjectManager
{
    partial class EditNotesForm
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
            this.RadioPanel = new System.Windows.Forms.Panel();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.NonCon = new System.Windows.Forms.RadioButton();
            this.S1 = new System.Windows.Forms.RadioButton();
            this.S2 = new System.Windows.Forms.RadioButton();
            this.S3 = new System.Windows.Forms.RadioButton();
            this.S4 = new System.Windows.Forms.RadioButton();
            this.S5 = new System.Windows.Forms.RadioButton();
            this.NotesBox = new System.Windows.Forms.TextBox();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectedLabel = new System.Windows.Forms.Label();
            this.CurrentScoreLabel = new System.Windows.Forms.Label();
            this.RadioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RadioPanel
            // 
            this.RadioPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RadioPanel.Controls.Add(this.S5);
            this.RadioPanel.Controls.Add(this.S4);
            this.RadioPanel.Controls.Add(this.S3);
            this.RadioPanel.Controls.Add(this.S2);
            this.RadioPanel.Controls.Add(this.S1);
            this.RadioPanel.Controls.Add(this.NonCon);
            this.RadioPanel.Location = new System.Drawing.Point(25, 51);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(106, 160);
            this.RadioPanel.TabIndex = 0;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.Location = new System.Drawing.Point(22, 21);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(48, 18);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "Score";
            // 
            // NonCon
            // 
            this.NonCon.Location = new System.Drawing.Point(3, 9);
            this.NonCon.Name = "NonCon";
            this.NonCon.Size = new System.Drawing.Size(104, 24);
            this.NonCon.TabIndex = 0;
            this.NonCon.TabStop = true;
            this.NonCon.Text = "Nonconvulsive";
            this.NonCon.UseVisualStyleBackColor = true;
            this.NonCon.CheckedChanged += new System.EventHandler(this.NonCon_CheckedChanged);
            // 
            // S1
            // 
            this.S1.AutoSize = true;
            this.S1.Location = new System.Drawing.Point(3, 36);
            this.S1.Name = "S1";
            this.S1.Size = new System.Drawing.Size(62, 17);
            this.S1.TabIndex = 1;
            this.S1.TabStop = true;
            this.S1.Text = "Stage 1";
            this.S1.UseVisualStyleBackColor = true;
            this.S1.CheckedChanged += new System.EventHandler(this.S1_CheckedChanged);
            // 
            // S2
            // 
            this.S2.AutoSize = true;
            this.S2.Location = new System.Drawing.Point(2, 60);
            this.S2.Name = "S2";
            this.S2.Size = new System.Drawing.Size(62, 17);
            this.S2.TabIndex = 2;
            this.S2.TabStop = true;
            this.S2.Text = "Stage 2";
            this.S2.UseVisualStyleBackColor = true;
            this.S2.CheckedChanged += new System.EventHandler(this.S2_CheckedChanged);
            // 
            // S3
            // 
            this.S3.AutoSize = true;
            this.S3.Location = new System.Drawing.Point(3, 84);
            this.S3.Name = "S3";
            this.S3.Size = new System.Drawing.Size(62, 17);
            this.S3.TabIndex = 3;
            this.S3.TabStop = true;
            this.S3.Text = "Stage 3";
            this.S3.UseVisualStyleBackColor = true;
            this.S3.CheckedChanged += new System.EventHandler(this.S3_CheckedChanged);
            // 
            // S4
            // 
            this.S4.AutoSize = true;
            this.S4.Location = new System.Drawing.Point(3, 108);
            this.S4.Name = "S4";
            this.S4.Size = new System.Drawing.Size(62, 17);
            this.S4.TabIndex = 4;
            this.S4.TabStop = true;
            this.S4.Text = "Stage 4";
            this.S4.UseVisualStyleBackColor = true;
            this.S4.CheckedChanged += new System.EventHandler(this.S4_CheckedChanged);
            // 
            // S5
            // 
            this.S5.AutoSize = true;
            this.S5.Location = new System.Drawing.Point(3, 131);
            this.S5.Name = "S5";
            this.S5.Size = new System.Drawing.Size(62, 17);
            this.S5.TabIndex = 5;
            this.S5.TabStop = true;
            this.S5.Text = "Stage 5";
            this.S5.UseVisualStyleBackColor = true;
            this.S5.CheckedChanged += new System.EventHandler(this.S5_CheckedChanged);
            // 
            // NotesBox
            // 
            this.NotesBox.Location = new System.Drawing.Point(152, 51);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(439, 182);
            this.NotesBox.TabIndex = 2;
            this.NotesBox.TextChanged += new System.EventHandler(this.NotesBox_TextChanged);
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesLabel.Location = new System.Drawing.Point(149, 21);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(48, 18);
            this.NotesLabel.TabIndex = 3;
            this.NotesLabel.Text = "Notes";
            // 
            // SaveButton
            // 
            this.SaveButton.AutoSize = true;
            this.SaveButton.Location = new System.Drawing.Point(232, 246);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(313, 246);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedLabel.Location = new System.Drawing.Point(23, 215);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(69, 18);
            this.SelectedLabel.TabIndex = 6;
            this.SelectedLabel.Text = "Selected:";
            // 
            // CurrentScoreLabel
            // 
            this.CurrentScoreLabel.AutoSize = true;
            this.CurrentScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentScoreLabel.Location = new System.Drawing.Point(93, 215);
            this.CurrentScoreLabel.Name = "CurrentScoreLabel";
            this.CurrentScoreLabel.Size = new System.Drawing.Size(32, 18);
            this.CurrentScoreLabel.TabIndex = 7;
            this.CurrentScoreLabel.Text = "N/A";
            // 
            // EditNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 281);
            this.Controls.Add(this.CurrentScoreLabel);
            this.Controls.Add(this.SelectedLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NotesLabel);
            this.Controls.Add(this.NotesBox);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.RadioPanel);
            this.MaximumSize = new System.Drawing.Size(620, 320);
            this.MinimumSize = new System.Drawing.Size(620, 320);
            this.Name = "EditNotesForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "EditNotesForm";
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel RadioPanel;
        private System.Windows.Forms.RadioButton S5;
        private System.Windows.Forms.RadioButton S4;
        private System.Windows.Forms.RadioButton S3;
        private System.Windows.Forms.RadioButton S2;
        private System.Windows.Forms.RadioButton S1;
        private System.Windows.Forms.RadioButton NonCon;
        private System.Windows.Forms.Label ScoreLabel;
        public System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Label NotesLabel;
        public System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label SelectedLabel;
        private System.Windows.Forms.Label CurrentScoreLabel;
    }
}