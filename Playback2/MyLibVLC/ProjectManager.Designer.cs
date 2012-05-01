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
            this.selectProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plottingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSeizureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measurementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempHumidityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.selectProjectToolStripMenuItem,
            this.importFileToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.importSeizureToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectProjectToolStripMenuItem
            // 
            this.selectProjectToolStripMenuItem.Name = "selectProjectToolStripMenuItem";
            this.selectProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectProjectToolStripMenuItem.Text = "Select Project";
            this.selectProjectToolStripMenuItem.Click += new System.EventHandler(this.selectProjectToolStripMenuItem_Click);
            // 
            // plottingToolStripMenuItem
            // 
            this.plottingToolStripMenuItem.Name = "plottingToolStripMenuItem";
            this.plottingToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.plottingToolStripMenuItem.Text = "Plot";
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importFileToolStripMenuItem.Text = "Add Directory";
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            // 
            // importSeizureToolStripMenuItem
            // 
            this.importSeizureToolStripMenuItem.Name = "importSeizureToolStripMenuItem";
            this.importSeizureToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.importSeizureToolStripMenuItem.Text = "Import Seizure File";
            this.importSeizureToolStripMenuItem.Click += new System.EventHandler(this.importSeizureToolStripMenuItem_Click);
            // 
            // measurementsToolStripMenuItem
            // 
            this.measurementsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weightToolStripMenuItem,
            this.tempHumidityToolStripMenuItem});
            this.measurementsToolStripMenuItem.Name = "measurementsToolStripMenuItem";
            this.measurementsToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.measurementsToolStripMenuItem.Text = "Measurements";
            // 
            // weightToolStripMenuItem
            // 
            this.weightToolStripMenuItem.Name = "weightToolStripMenuItem";
            this.weightToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.weightToolStripMenuItem.Text = "Weight";
            // 
            // tempHumidityToolStripMenuItem
            // 
            this.tempHumidityToolStripMenuItem.Name = "tempHumidityToolStripMenuItem";
            this.tempHumidityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tempHumidityToolStripMenuItem.Text = "Temp/Humidity";
            // 
            // ProjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 393);
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
    }
}