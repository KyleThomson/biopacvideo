namespace SeizurePlayback
{
    partial class Exporter
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
            this.SzCount = new System.Windows.Forms.CheckBox();
            this.ExportBtn = new System.Windows.Forms.Button();
            this.PelletCount = new System.Windows.Forms.CheckBox();
            this.MedCount = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SzTimes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SzCount
            // 
            this.SzCount.AutoSize = true;
            this.SzCount.Location = new System.Drawing.Point(12, 12);
            this.SzCount.Name = "SzCount";
            this.SzCount.Size = new System.Drawing.Size(92, 17);
            this.SzCount.TabIndex = 0;
            this.SzCount.Text = "Seizure Count";
            this.SzCount.UseVisualStyleBackColor = true;
            // 
            // ExportBtn
            // 
            this.ExportBtn.Location = new System.Drawing.Point(146, 198);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(75, 23);
            this.ExportBtn.TabIndex = 1;
            this.ExportBtn.Text = "Export Data";
            this.ExportBtn.UseVisualStyleBackColor = true;
            this.ExportBtn.Click += new System.EventHandler(this.Export_Click);
            // 
            // PelletCount
            // 
            this.PelletCount.AutoSize = true;
            this.PelletCount.Location = new System.Drawing.Point(12, 55);
            this.PelletCount.Name = "PelletCount";
            this.PelletCount.Size = new System.Drawing.Size(83, 17);
            this.PelletCount.TabIndex = 2;
            this.PelletCount.Text = "Pellet Count";
            this.PelletCount.UseVisualStyleBackColor = true;
            // 
            // MedCount
            // 
            this.MedCount.AutoSize = true;
            this.MedCount.Location = new System.Drawing.Point(12, 78);
            this.MedCount.Name = "MedCount";
            this.MedCount.Size = new System.Drawing.Size(73, 17);
            this.MedCount.TabIndex = 3;
            this.MedCount.Text = "Med Rate";
            this.MedCount.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(125, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Humidity";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 101);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "Weight";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(125, 35);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(86, 17);
            this.checkBox3.TabIndex = 6;
            this.checkBox3.Text = "Temperature";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // SzTimes
            // 
            this.SzTimes.AutoSize = true;
            this.SzTimes.Location = new System.Drawing.Point(12, 32);
            this.SzTimes.Name = "SzTimes";
            this.SzTimes.Size = new System.Drawing.Size(92, 17);
            this.SzTimes.TabIndex = 7;
            this.SzTimes.Text = "Seizure Times";
            this.SzTimes.UseVisualStyleBackColor = true;
            // 
            // Exporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 233);
            this.Controls.Add(this.SzTimes);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.MedCount);
            this.Controls.Add(this.PelletCount);
            this.Controls.Add(this.ExportBtn);
            this.Controls.Add(this.SzCount);
            this.Name = "Exporter";
            this.Text = "Export Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SzCount;
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.CheckBox PelletCount;
        private System.Windows.Forms.CheckBox MedCount;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox SzTimes;
    }
}