﻿namespace SeizurePlayback
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
            this.SecondList = new System.Windows.Forms.ListBox();
            this.MainList = new System.Windows.Forms.ListBox();
            this.MainSelect = new System.Windows.Forms.ComboBox();
            this.SecondSelect = new System.Windows.Forms.ComboBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(409, 24);
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
            this.selectProjectToolStripMenuItem.Click += new System.EventHandler(this.selectProjectToolStripMenuItem_Click);
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.importFileToolStripMenuItem.Text = "Add Directory";
            this.importFileToolStripMenuItem.Click += new System.EventHandler(this.importFileToolStripMenuItem_Click);
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exportDataToolStripMenuItem.Text = "&Export Data";
            this.exportDataToolStripMenuItem.Click += new System.EventHandler(this.exportDataToolStripMenuItem_Click);
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
            this.tempHumidityToolStripMenuItem.Click += new System.EventHandler(this.tempHumidityToolStripMenuItem_Click);
            // 
            // SecondList
            // 
            this.SecondList.FormattingEnabled = true;
            this.SecondList.Location = new System.Drawing.Point(178, 66);
            this.SecondList.Name = "SecondList";
            this.SecondList.Size = new System.Drawing.Size(185, 264);
            this.SecondList.TabIndex = 1;
            this.SecondList.SelectedIndexChanged += new System.EventHandler(this.SecondList_SelectedIndexChanged);
            // 
            // MainList
            // 
            this.MainList.FormattingEnabled = true;
            this.MainList.Location = new System.Drawing.Point(12, 66);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(160, 264);
            this.MainList.TabIndex = 7;
            this.MainList.SelectedIndexChanged += new System.EventHandler(this.MainList_SelectedIndexChanged);
            // 
            // MainSelect
            // 
            this.MainSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainSelect.FormattingEnabled = true;
            this.MainSelect.Items.AddRange(new object[] {
            "Files",
            "Animals"});
            this.MainSelect.Location = new System.Drawing.Point(12, 39);
            this.MainSelect.Name = "MainSelect";
            this.MainSelect.Size = new System.Drawing.Size(160, 21);
            this.MainSelect.TabIndex = 8;
            this.MainSelect.SelectedIndexChanged += new System.EventHandler(this.MainSelect_SelectedIndexChanged);
            // 
            // SecondSelect
            // 
            this.SecondSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecondSelect.FormattingEnabled = true;
            this.SecondSelect.Location = new System.Drawing.Point(178, 39);
            this.SecondSelect.Name = "SecondSelect";
            this.SecondSelect.Size = new System.Drawing.Size(185, 21);
            this.SecondSelect.TabIndex = 9;
            this.SecondSelect.SelectedIndexChanged += new System.EventHandler(this.SecondSelect_SelectedIndexChanged);
            // 
            // ProjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 393);
            this.Controls.Add(this.SecondSelect);
            this.Controls.Add(this.MainSelect);
            this.Controls.Add(this.MainList);
            this.Controls.Add(this.SecondList);
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
        private System.Windows.Forms.ToolStripMenuItem importSeizureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measurementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tempHumidityToolStripMenuItem;
        private System.Windows.Forms.ListBox SecondList;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ListBox MainList;
        private System.Windows.Forms.ComboBox MainSelect;
        private System.Windows.Forms.ComboBox SecondSelect;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
    }
}