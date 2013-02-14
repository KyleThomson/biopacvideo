namespace SeizurePlayback
{
    partial class Calendar
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
            this.Tracker = new System.Windows.Forms.TrackBar();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.AnimalSel = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Tracker)).BeginInit();
            this.SuspendLayout();
            // 
            // Tracker
            // 
            this.Tracker.Location = new System.Drawing.Point(12, 50);
            this.Tracker.Name = "Tracker";
            this.Tracker.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Tracker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Tracker.Size = new System.Drawing.Size(42, 415);
            this.Tracker.TabIndex = 0;
            this.Tracker.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(498, 477);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 12;
            this.lineShape1.X2 = 488;
            this.lineShape1.Y1 = 40;
            this.lineShape1.Y2 = 40;
            // 
            // AnimalSel
            // 
            this.AnimalSel.FormattingEnabled = true;
            this.AnimalSel.Location = new System.Drawing.Point(12, 12);
            this.AnimalSel.Name = "AnimalSel";
            this.AnimalSel.Size = new System.Drawing.Size(153, 21);
            this.AnimalSel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(60, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 415);
            this.panel1.TabIndex = 3;
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 477);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AnimalSel);
            this.Controls.Add(this.Tracker);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "Calendar";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Calendar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tracker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar Tracker;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.ComboBox AnimalSel;
        private System.Windows.Forms.Panel panel1;

    }
}