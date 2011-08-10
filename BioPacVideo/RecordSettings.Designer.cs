namespace BioPacVideo
{
    partial class RecordSettings
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
            this.ID_SRATE = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ID_DWS = new System.Windows.Forms.ComboBox();
            this.ID_OK = new System.Windows.Forms.Button();
            this.IDC_MINMAXV = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ID_GAIN = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ID_SRATE
            // 
            this.ID_SRATE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_SRATE.FormattingEnabled = true;
            this.ID_SRATE.Location = new System.Drawing.Point(15, 65);
            this.ID_SRATE.Name = "ID_SRATE";
            this.ID_SRATE.Size = new System.Drawing.Size(121, 21);
            this.ID_SRATE.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Display Window Size";
            // 
            // ID_DWS
            // 
            this.ID_DWS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_DWS.FormattingEnabled = true;
            this.ID_DWS.Location = new System.Drawing.Point(15, 25);
            this.ID_DWS.Name = "ID_DWS";
            this.ID_DWS.Size = new System.Drawing.Size(121, 21);
            this.ID_DWS.TabIndex = 2;
            // 
            // ID_OK
            // 
            this.ID_OK.Location = new System.Drawing.Point(210, 182);
            this.ID_OK.Name = "ID_OK";
            this.ID_OK.Size = new System.Drawing.Size(70, 25);
            this.ID_OK.TabIndex = 5;
            this.ID_OK.Text = "OK";
            this.ID_OK.UseVisualStyleBackColor = true;
            this.ID_OK.Click += new System.EventHandler(this.ID_OK_Click);
            // 
            // IDC_MINMAXV
            // 
            this.IDC_MINMAXV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IDC_MINMAXV.FormattingEnabled = true;
            this.IDC_MINMAXV.Location = new System.Drawing.Point(154, 25);
            this.IDC_MINMAXV.Name = "IDC_MINMAXV";
            this.IDC_MINMAXV.Size = new System.Drawing.Size(121, 21);
            this.IDC_MINMAXV.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Min/Max Voltage";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gain";
            // 
            // ID_GAIN
            // 
            this.ID_GAIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_GAIN.FormattingEnabled = true;
            this.ID_GAIN.Location = new System.Drawing.Point(15, 105);
            this.ID_GAIN.Name = "ID_GAIN";
            this.ID_GAIN.Size = new System.Drawing.Size(121, 21);
            this.ID_GAIN.TabIndex = 8;
            this.ID_GAIN.SelectedIndexChanged += new System.EventHandler(this.ID_GAIN_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(154, 105);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "High Pass Filter";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(15, 145);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Low Pass Filter";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(154, 145);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 15;
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 219);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ID_GAIN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IDC_MINMAXV);
            this.Controls.Add(this.ID_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ID_DWS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ID_SRATE);
            this.Name = "RecordSettings";
            this.Text = " ";
            this.Load += new System.EventHandler(this.RecordSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ID_DWS;
        public System.Windows.Forms.ComboBox ID_SRATE;
        private System.Windows.Forms.Button ID_OK;
        public System.Windows.Forms.ComboBox IDC_MINMAXV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox ID_GAIN;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox comboBox3;

    }
}