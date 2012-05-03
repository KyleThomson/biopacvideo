namespace SeizurePlayback
{
    partial class ProjectManager
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSeizureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plottingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measurementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempHumidityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnimalList = new System.Windows.Forms.ListBox();
            this.SzList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WtMeasure = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.plottingToolStripMenuItem,
            this.measurementsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.selectProjectToolStripMenuItem,
            this.importFileToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.importSeizureToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.newProjectToolStripMenuItem.Text = "&New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // selectProjectToolStripMenuItem
            // 
            this.selectProjectToolStripMenuItem.Name = "selectProjectToolStripMenuItem";
            this.selectProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.selectProjectToolStripMenuItem.Text = "&Open Project";
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.importFileToolStripMenuItem.Text = "Add Directory";
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exportDataToolStripMenuItem.Text = "&Export Data";
            // 
            // importSeizureToolStripMenuItem
            // 
            this.importSeizureToolStripMenuItem.Name = "importSeizureToolStripMenuItem";
            this.importSeizureToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.importSeizureToolStripMenuItem.Text = "&Import Seizure File";
            // 
            // plottingToolStripMenuItem
            // 
            this.plottingToolStripMenuItem.Name = "plottingToolStripMenuItem";
            this.plottingToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.plottingToolStripMenuItem.Text = "Plot";
            // 
            // measurementsToolStripMenuItem
            // 
            this.measurementsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weightToolStripMenuItem,
            this.tempHumidityToolStripMenuItem});
            this.measurementsToolStripMenuItem.Name = "measurementsToolStripMenuItem";
            this.measurementsToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.measurementsToolStripMenuItem.Text = "&Measurements";
            // 
            // weightToolStripMenuItem
            // 
            this.weightToolStripMenuItem.Name = "weightToolStripMenuItem";
            this.weightToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.weightToolStripMenuItem.Text = "&Weight";
            // 
            // tempHumidityToolStripMenuItem
            // 
            this.tempHumidityToolStripMenuItem.Name = "tempHumidityToolStripMenuItem";
            this.tempHumidityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tempHumidityToolStripMenuItem.Text = "Temp/Humidity";
            // 
            // AnimalList
            // 
            this.AnimalList.FormattingEnabled = true;
            this.AnimalList.Location = new System.Drawing.Point(12, 60);
            this.AnimalList.Name = "AnimalList";
            this.AnimalList.Size = new System.Drawing.Size(120, 264);
            this.AnimalList.TabIndex = 1;
            // 
            // SzList
            // 
            this.SzList.FormattingEnabled = true;
            this.SzList.Location = new System.Drawing.Point(138, 60);
            this.SzList.Name = "SzList";
            this.SzList.Size = new System.Drawing.Size(145, 264);
            this.SzList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Animal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Seizures";
            // 
            // WtMeasure
            // 
            this.WtMeasure.FormattingEnabled = true;
            this.WtMeasure.Location = new System.Drawing.Point(289, 60);
            this.WtMeasure.Name = "WtMeasure";
            this.WtMeasure.Size = new System.Drawing.Size(145, 264);
            this.WtMeasure.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Weight";
            // 
            // ProjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 393);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WtMeasure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SzList);
            this.Controls.Add(this.AnimalList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProjectManager";
            this.Text = "Project Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plottingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSeizureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measurementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tempHumidityToolStripMenuItem;
        private System.Windows.Forms.ListBox AnimalList;
        private System.Windows.Forms.ListBox SzList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox WtMeasure;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}