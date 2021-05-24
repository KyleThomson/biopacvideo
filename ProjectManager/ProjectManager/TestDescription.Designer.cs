namespace ProjectManager
{
    partial class TestDescription
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
            this.etsp = new System.Windows.Forms.Label();
            this.etspEntry = new System.Windows.Forms.TextBox();
            this.batchEntry = new System.Windows.Forms.TextBox();
            this.batch = new System.Windows.Forms.Label();
            this.doseEntry = new System.Windows.Forms.TextBox();
            this.dose = new System.Windows.Forms.Label();
            this.frequencyEntry = new System.Windows.Forms.TextBox();
            this.frequency = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // etsp
            // 
            this.etsp.AutoSize = true;
            this.etsp.Location = new System.Drawing.Point(24, 18);
            this.etsp.Name = "etsp";
            this.etsp.Size = new System.Drawing.Size(35, 13);
            this.etsp.TabIndex = 0;
            this.etsp.Text = "ETSP";
            // 
            // etspEntry
            // 
            this.etspEntry.Location = new System.Drawing.Point(27, 34);
            this.etspEntry.Name = "etspEntry";
            this.etspEntry.Size = new System.Drawing.Size(100, 20);
            this.etspEntry.TabIndex = 1;
            this.etspEntry.TextChanged += new System.EventHandler(this.etspEntry_TextChanged);
            // 
            // batchEntry
            // 
            this.batchEntry.Location = new System.Drawing.Point(183, 34);
            this.batchEntry.Name = "batchEntry";
            this.batchEntry.Size = new System.Drawing.Size(100, 20);
            this.batchEntry.TabIndex = 3;
            // 
            // batch
            // 
            this.batch.AutoSize = true;
            this.batch.Location = new System.Drawing.Point(180, 18);
            this.batch.Name = "batch";
            this.batch.Size = new System.Drawing.Size(35, 13);
            this.batch.TabIndex = 2;
            this.batch.Text = "Batch";
            // 
            // doseEntry
            // 
            this.doseEntry.Location = new System.Drawing.Point(27, 92);
            this.doseEntry.Name = "doseEntry";
            this.doseEntry.Size = new System.Drawing.Size(100, 20);
            this.doseEntry.TabIndex = 5;
            // 
            // dose
            // 
            this.dose.AutoSize = true;
            this.dose.Location = new System.Drawing.Point(24, 76);
            this.dose.Name = "dose";
            this.dose.Size = new System.Drawing.Size(32, 13);
            this.dose.TabIndex = 4;
            this.dose.Text = "Dose";
            // 
            // frequencyEntry
            // 
            this.frequencyEntry.Location = new System.Drawing.Point(183, 92);
            this.frequencyEntry.Name = "frequencyEntry";
            this.frequencyEntry.Size = new System.Drawing.Size(100, 20);
            this.frequencyEntry.TabIndex = 7;
            // 
            // frequency
            // 
            this.frequency.AutoSize = true;
            this.frequency.Location = new System.Drawing.Point(180, 76);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(57, 13);
            this.frequency.TabIndex = 6;
            this.frequency.Text = "Frequency";
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(64, 126);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 8;
            this.confirmButton.Text = "Ok";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(162, 126);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // TestDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 161);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.frequencyEntry);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.doseEntry);
            this.Controls.Add(this.dose);
            this.Controls.Add(this.batchEntry);
            this.Controls.Add(this.batch);
            this.Controls.Add(this.etspEntry);
            this.Controls.Add(this.etsp);
            this.Name = "TestDescription";
            this.Text = "TestDescription";
            this.Load += new System.EventHandler(this.TestDescription_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label etsp;
        private System.Windows.Forms.TextBox etspEntry;
        private System.Windows.Forms.TextBox batchEntry;
        private System.Windows.Forms.Label batch;
        private System.Windows.Forms.TextBox doseEntry;
        private System.Windows.Forms.Label dose;
        private System.Windows.Forms.TextBox frequencyEntry;
        private System.Windows.Forms.Label frequency;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
    }
}