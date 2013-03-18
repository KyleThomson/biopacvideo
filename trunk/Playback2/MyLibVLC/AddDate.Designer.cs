namespace SeizurePlayback
{
    partial class AddDate
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
            this.AnimalBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelBox1 = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.DateList = new System.Windows.Forms.ListBox();
            this.Finish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnimalBox
            // 
            this.AnimalBox.FormattingEnabled = true;
            this.AnimalBox.Location = new System.Drawing.Point(12, 34);
            this.AnimalBox.Name = "AnimalBox";
            this.AnimalBox.Size = new System.Drawing.Size(121, 21);
            this.AnimalBox.TabIndex = 0;
            this.AnimalBox.SelectedIndexChanged += new System.EventHandler(this.AnimalBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Animal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date";
            // 
            // DateBox1
            // 
            this.DateBox1.Location = new System.Drawing.Point(142, 34);
            this.DateBox1.Name = "DateBox1";
            this.DateBox1.Size = new System.Drawing.Size(100, 20);
            this.DateBox1.TabIndex = 3;
            this.DateBox1.LostFocus += new System.EventHandler(this.DateBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Label";
            // 
            // LabelBox1
            // 
            this.LabelBox1.Location = new System.Drawing.Point(248, 34);
            this.LabelBox1.Name = "LabelBox1";
            this.LabelBox1.Size = new System.Drawing.Size(234, 20);
            this.LabelBox1.TabIndex = 5;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(326, 60);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 6;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(407, 60);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 7;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // DateList
            // 
            this.DateList.FormattingEnabled = true;
            this.DateList.Location = new System.Drawing.Point(12, 62);
            this.DateList.Name = "DateList";
            this.DateList.Size = new System.Drawing.Size(301, 199);
            this.DateList.TabIndex = 8;
            // 
            // Finish
            // 
            this.Finish.Location = new System.Drawing.Point(407, 238);
            this.Finish.Name = "Finish";
            this.Finish.Size = new System.Drawing.Size(75, 23);
            this.Finish.TabIndex = 9;
            this.Finish.Text = "Finish";
            this.Finish.UseVisualStyleBackColor = true;
            this.Finish.Click += new System.EventHandler(this.Finish_Click);
            // 
            // AddDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 272);
            this.Controls.Add(this.Finish);
            this.Controls.Add(this.DateList);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.LabelBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DateBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AnimalBox);
            this.Name = "AddDate";
            this.Text = "Add Important Date";            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox AnimalBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DateBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LabelBox1;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ListBox DateList;
        private System.Windows.Forms.Button Finish;
    }
}