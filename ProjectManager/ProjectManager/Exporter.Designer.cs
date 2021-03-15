namespace ProjectManager
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
            this.MealCheck = new System.Windows.Forms.CheckBox();
            this.DetailList = new System.Windows.Forms.CheckBox();
            this.Notes = new System.Windows.Forms.CheckBox();
            this.SzSvBox = new System.Windows.Forms.CheckBox();
            this.BloodDraw = new System.Windows.Forms.CheckBox();
            this.BloodDrawList = new System.Windows.Forms.CheckBox();
            this.binSeizures = new System.Windows.Forms.CheckBox();
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
            this.PelletCount.Location = new System.Drawing.Point(125, 81);
            this.PelletCount.Name = "PelletCount";
            this.PelletCount.Size = new System.Drawing.Size(83, 17);
            this.PelletCount.TabIndex = 2;
            this.PelletCount.Text = "Pellet Count";
            this.PelletCount.UseVisualStyleBackColor = true;
            // 
            // MedCount
            // 
            this.MedCount.AutoSize = true;
            this.MedCount.Location = new System.Drawing.Point(12, 127);
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
            this.checkBox2.Location = new System.Drawing.Point(125, 104);
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
            this.SzTimes.Location = new System.Drawing.Point(12, 35);
            this.SzTimes.Name = "SzTimes";
            this.SzTimes.Size = new System.Drawing.Size(92, 17);
            this.SzTimes.TabIndex = 7;
            this.SzTimes.Text = "Seizure Times";
            this.SzTimes.UseVisualStyleBackColor = true;
            // 
            // MealCheck
            // 
            this.MealCheck.AutoSize = true;
            this.MealCheck.Location = new System.Drawing.Point(12, 104);
            this.MealCheck.Name = "MealCheck";
            this.MealCheck.Size = new System.Drawing.Size(49, 17);
            this.MealCheck.TabIndex = 8;
            this.MealCheck.Text = "Meal";
            this.MealCheck.UseVisualStyleBackColor = true;
            // 
            // DetailList
            // 
            this.DetailList.AutoSize = true;
            this.DetailList.Location = new System.Drawing.Point(125, 58);
            this.DetailList.Name = "DetailList";
            this.DetailList.Size = new System.Drawing.Size(84, 17);
            this.DetailList.TabIndex = 9;
            this.DetailList.Text = "Detailed List";
            this.DetailList.UseVisualStyleBackColor = true;
            // 
            // Notes
            // 
            this.Notes.AutoSize = true;
            this.Notes.Location = new System.Drawing.Point(12, 81);
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(69, 17);
            this.Notes.TabIndex = 10;
            this.Notes.Text = "Sz Notes";
            this.Notes.UseVisualStyleBackColor = true;
            // 
            // SzSvBox
            // 
            this.SzSvBox.AutoSize = true;
            this.SzSvBox.Location = new System.Drawing.Point(12, 58);
            this.SzSvBox.Name = "SzSvBox";
            this.SzSvBox.Size = new System.Drawing.Size(105, 17);
            this.SzSvBox.TabIndex = 11;
            this.SzSvBox.Text = "Seizure Severity ";
            this.SzSvBox.UseVisualStyleBackColor = true;
            // 
            // BloodDraw
            // 
            this.BloodDraw.AutoSize = true;
            this.BloodDraw.Location = new System.Drawing.Point(12, 150);
            this.BloodDraw.Name = "BloodDraw";
            this.BloodDraw.Size = new System.Drawing.Size(86, 17);
            this.BloodDraw.TabIndex = 12;
            this.BloodDraw.Text = "Blood Draws";
            this.BloodDraw.UseVisualStyleBackColor = true;
            // 
            // BloodDrawList
            // 
            this.BloodDrawList.AutoSize = true;
            this.BloodDrawList.Location = new System.Drawing.Point(12, 173);
            this.BloodDrawList.Name = "BloodDrawList";
            this.BloodDrawList.Size = new System.Drawing.Size(111, 17);
            this.BloodDrawList.TabIndex = 13;
            this.BloodDrawList.Text = "Blood Draws (List)";
            this.BloodDrawList.UseVisualStyleBackColor = true;
            // 
            // binSeizures
            // 
            this.binSeizures.AutoSize = true;
            this.binSeizures.Location = new System.Drawing.Point(125, 127);
            this.binSeizures.Name = "binSeizures";
            this.binSeizures.Size = new System.Drawing.Size(84, 17);
            this.binSeizures.TabIndex = 14;
            this.binSeizures.Text = "Bin Seizures";
            this.binSeizures.UseVisualStyleBackColor = true;
            // 
            // Exporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 233);
            this.Controls.Add(this.binSeizures);
            this.Controls.Add(this.BloodDrawList);
            this.Controls.Add(this.BloodDraw);
            this.Controls.Add(this.SzSvBox);
            this.Controls.Add(this.Notes);
            this.Controls.Add(this.DetailList);
            this.Controls.Add(this.MealCheck);
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
        private System.Windows.Forms.CheckBox MealCheck;
        private System.Windows.Forms.CheckBox DetailList;
        private System.Windows.Forms.CheckBox Notes;
        private System.Windows.Forms.CheckBox SzSvBox;
        private System.Windows.Forms.CheckBox BloodDraw;
        private System.Windows.Forms.CheckBox BloodDrawList;
        private System.Windows.Forms.CheckBox binSeizures;
    }
}