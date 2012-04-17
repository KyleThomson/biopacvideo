namespace BioPacVideo
{
    partial class VideoSettings
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
            this.V1 = new System.Windows.Forms.RadioButton();
            this.V2 = new System.Windows.Forms.RadioButton();
            this.V3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.IDT_KEYINT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IDT_QUANT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // V1
            // 
            this.V1.AutoSize = true;
            this.V1.Location = new System.Drawing.Point(12, 34);
            this.V1.Name = "V1";
            this.V1.Size = new System.Drawing.Size(97, 17);
            this.V1.TabIndex = 0;
            this.V1.TabStop = true;
            this.V1.Text = "VGA (640x480)";
            this.V1.UseVisualStyleBackColor = true;
            // 
            // V2
            // 
            this.V2.AutoSize = true;
            this.V2.Location = new System.Drawing.Point(12, 57);
            this.V2.Name = "V2";
            this.V2.Size = new System.Drawing.Size(105, 17);
            this.V2.TabIndex = 1;
            this.V2.TabStop = true;
            this.V2.Text = "QVGA (320x240)";
            this.V2.UseVisualStyleBackColor = true;
            // 
            // V3
            // 
            this.V3.AutoSize = true;
            this.V3.Location = new System.Drawing.Point(12, 80);
            this.V3.Name = "V3";
            this.V3.Size = new System.Drawing.Size(127, 17);
            this.V3.TabIndex = 2;
            this.V3.TabStop = true;
            this.V3.Text = "SUBQVGA (160x120)";
            this.V3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Recording Resolution";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Key Interval";
            // 
            // IDT_KEYINT
            // 
            this.IDT_KEYINT.Location = new System.Drawing.Point(9, 169);
            this.IDT_KEYINT.Name = "IDT_KEYINT";
            this.IDT_KEYINT.Size = new System.Drawing.Size(100, 20);
            this.IDT_KEYINT.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Quant";
            // 
            // IDT_QUANT
            // 
            this.IDT_QUANT.Location = new System.Drawing.Point(9, 211);
            this.IDT_QUANT.Name = "IDT_QUANT";
            this.IDT_QUANT.Size = new System.Drawing.Size(100, 20);
            this.IDT_QUANT.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lengthwise Display";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "4",
            "8"});
            this.comboBox1.Location = new System.Drawing.Point(9, 126);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // VideoSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(156, 265);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IDT_QUANT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IDT_KEYINT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.V3);
            this.Controls.Add(this.V2);
            this.Controls.Add(this.V1);
            this.Name = "VideoSettings";
            this.Text = "Video Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton V1;
        private System.Windows.Forms.RadioButton V2;
        private System.Windows.Forms.RadioButton V3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IDT_KEYINT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IDT_QUANT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}