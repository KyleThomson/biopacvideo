namespace ProjectManager
{
    partial class SeizureEdits
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
            this.severityLabel = new System.Windows.Forms.Label();
            this.notesLabel = new System.Windows.Forms.Label();
            this.timespanLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.severityField = new System.Windows.Forms.TextBox();
            this.notesField = new System.Windows.Forms.TextBox();
            this.timespanField = new System.Windows.Forms.TextBox();
            this.dateField = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // severityLabel
            // 
            this.severityLabel.AutoSize = true;
            this.severityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.severityLabel.Location = new System.Drawing.Point(12, 19);
            this.severityLabel.Name = "severityLabel";
            this.severityLabel.Size = new System.Drawing.Size(65, 20);
            this.severityLabel.TabIndex = 0;
            this.severityLabel.Text = "Severity";
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesLabel.Location = new System.Drawing.Point(12, 71);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(51, 20);
            this.notesLabel.TabIndex = 1;
            this.notesLabel.Text = "Notes";
            // 
            // timespanLabel
            // 
            this.timespanLabel.AutoSize = true;
            this.timespanLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timespanLabel.Location = new System.Drawing.Point(12, 133);
            this.timespanLabel.Name = "timespanLabel";
            this.timespanLabel.Size = new System.Drawing.Size(78, 20);
            this.timespanLabel.TabIndex = 2;
            this.timespanLabel.Text = "Timespan";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.Location = new System.Drawing.Point(12, 185);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(44, 20);
            this.dateLabel.TabIndex = 3;
            this.dateLabel.Text = "Date";
            // 
            // severityField
            // 
            this.severityField.Location = new System.Drawing.Point(127, 19);
            this.severityField.Name = "severityField";
            this.severityField.Size = new System.Drawing.Size(98, 20);
            this.severityField.TabIndex = 4;
            this.severityField.TextChanged += new System.EventHandler(this.severityField_TextChanged);
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(127, 73);
            this.notesField.Name = "notesField";
            this.notesField.Size = new System.Drawing.Size(98, 20);
            this.notesField.TabIndex = 5;
            this.notesField.TextChanged += new System.EventHandler(this.notesField_TextChanged);
            // 
            // timespanField
            // 
            this.timespanField.Location = new System.Drawing.Point(127, 133);
            this.timespanField.Name = "timespanField";
            this.timespanField.ReadOnly = true;
            this.timespanField.Size = new System.Drawing.Size(98, 20);
            this.timespanField.TabIndex = 6;
            // 
            // dateField
            // 
            this.dateField.Location = new System.Drawing.Point(127, 185);
            this.dateField.Name = "dateField";
            this.dateField.ReadOnly = true;
            this.dateField.Size = new System.Drawing.Size(98, 20);
            this.dateField.TabIndex = 7;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(110, 214);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(50, 24);
            this.ok.TabIndex = 8;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(199, 214);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(50, 24);
            this.cancel.TabIndex = 9;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SeizureEdits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 250);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.dateField);
            this.Controls.Add(this.timespanField);
            this.Controls.Add(this.notesField);
            this.Controls.Add(this.severityField);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.timespanLabel);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.severityLabel);
            this.Name = "SeizureEdits";
            this.Text = "SeizureEdits";
            this.Load += new System.EventHandler(this.SeizureEdits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label severityLabel;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.Label timespanLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TextBox severityField;
        private System.Windows.Forms.TextBox notesField;
        private System.Windows.Forms.TextBox timespanField;
        private System.Windows.Forms.TextBox dateField;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
    }
}