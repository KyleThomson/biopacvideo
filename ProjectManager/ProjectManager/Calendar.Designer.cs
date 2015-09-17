namespace ProjectManager
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.AnimalSel = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MMY = new System.Windows.Forms.Label();
            this.Up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(604, 595);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 57;
            this.lineShape1.X2 = 589;
            this.lineShape1.Y1 = 41;
            this.lineShape1.Y2 = 41;
            // 
            // AnimalSel
            // 
            this.AnimalSel.FormattingEnabled = true;
            this.AnimalSel.Location = new System.Drawing.Point(60, 12);
            this.AnimalSel.Name = "AnimalSel";
            this.AnimalSel.Size = new System.Drawing.Size(153, 21);
            this.AnimalSel.TabIndex = 2;
            this.AnimalSel.SelectedIndexChanged += new System.EventHandler(this.AnimalSel_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(60, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 534);
            this.panel1.TabIndex = 3;
            // 
            // MMY
            // 
            this.MMY.AutoSize = true;
            this.MMY.Font = new System.Drawing.Font("Arial", 14F);
            this.MMY.Location = new System.Drawing.Point(219, 12);
            this.MMY.Name = "MMY";
            this.MMY.Size = new System.Drawing.Size(371, 22);
            this.MMY.TabIndex = 4;
            this.MMY.Text = "                                          Month/Month Year";
            this.MMY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Up
            // 
            this.Up.Font = new System.Drawing.Font("Wingdings", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.Up.Location = new System.Drawing.Point(3, 50);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(52, 228);
            this.Up.TabIndex = 0;
            this.Up.Text = "⇧";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // down
            // 
            this.down.Font = new System.Drawing.Font("Wingdings", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.down.Location = new System.Drawing.Point(2, 360);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(52, 224);
            this.down.TabIndex = 5;
            this.down.Text = "⇩";
            this.down.UseVisualStyleBackColor = true;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 595);
            this.Controls.Add(this.down);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.MMY);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AnimalSel);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Calendar";
            this.Text = "Calendar";
            this.Load += new System.EventHandler(this.Calendar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.ComboBox AnimalSel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MMY;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button down;

    }
}