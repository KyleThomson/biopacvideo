namespace SeizureHeatmap
{
    partial class mainUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.fileLoad = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PlotSeizure = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.showAnimal = new System.Windows.Forms.Button();
            this.GenerateExcel = new System.Windows.Forms.Button();
            this.DrawPoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileLoad
            // 
            this.fileLoad.Location = new System.Drawing.Point(36, 25);
            this.fileLoad.Name = "fileLoad";
            this.fileLoad.Size = new System.Drawing.Size(142, 52);
            this.fileLoad.TabIndex = 0;
            this.fileLoad.Text = "Load file";
            this.fileLoad.UseVisualStyleBackColor = true;
            this.fileLoad.Click += new System.EventHandler(this.fileLoad_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(241, 25);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(660, 463);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // PlotSeizure
            // 
            this.PlotSeizure.Location = new System.Drawing.Point(36, 326);
            this.PlotSeizure.Name = "PlotSeizure";
            this.PlotSeizure.Size = new System.Drawing.Size(142, 29);
            this.PlotSeizure.TabIndex = 4;
            this.PlotSeizure.Text = "Plot seizures";
            this.PlotSeizure.UseVisualStyleBackColor = true;
            this.PlotSeizure.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(36, 94);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(142, 160);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Hide animal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // showAnimal
            // 
            this.showAnimal.Location = new System.Drawing.Point(36, 289);
            this.showAnimal.Name = "showAnimal";
            this.showAnimal.Size = new System.Drawing.Size(142, 23);
            this.showAnimal.TabIndex = 7;
            this.showAnimal.Text = "show animal";
            this.showAnimal.UseVisualStyleBackColor = true;
            this.showAnimal.Click += new System.EventHandler(this.showAnimal_Click);
            // 
            // GenerateExcel
            // 
            this.GenerateExcel.Location = new System.Drawing.Point(36, 426);
            this.GenerateExcel.Name = "GenerateExcel";
            this.GenerateExcel.Size = new System.Drawing.Size(142, 43);
            this.GenerateExcel.TabIndex = 8;
            this.GenerateExcel.Text = "Write seizures to Excel";
            this.GenerateExcel.UseVisualStyleBackColor = true;
            this.GenerateExcel.Click += new System.EventHandler(this.button2_Click);
            // 
            // DrawPoint
            // 
            this.DrawPoint.Location = new System.Drawing.Point(36, 493);
            this.DrawPoint.Name = "DrawPoint";
            this.DrawPoint.Size = new System.Drawing.Size(142, 43);
            this.DrawPoint.TabIndex = 9;
            this.DrawPoint.Text = "Draw some points";
            this.DrawPoint.UseVisualStyleBackColor = true;
            this.DrawPoint.Click += new System.EventHandler(this.DrawPoint_Click);
            // 
            // mainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 591);
            this.Controls.Add(this.DrawPoint);
            this.Controls.Add(this.GenerateExcel);
            this.Controls.Add(this.showAnimal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.PlotSeizure);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.fileLoad);
            this.Name = "mainUI";
            this.Text = "Main_Interface";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button fileLoad;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button PlotSeizure;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button showAnimal;
        private System.Windows.Forms.Button GenerateExcel;
        private System.Windows.Forms.Button DrawPoint;
    }
}

