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
            this.ID_SRATE.SelectedIndexChanged += new System.EventHandler(this.ID_SRATE_SelectedIndexChanged);
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
            this.ID_OK.Location = new System.Drawing.Point(210, 92);
            this.ID_OK.Name = "ID_OK";
            this.ID_OK.Size = new System.Drawing.Size(70, 25);
            this.ID_OK.TabIndex = 5;
            this.ID_OK.Text = " OK";
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
            this.label4.Location = new System.Drawing.Point(151, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gain";
            // 
            // ID_GAIN
            // 
            this.ID_GAIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_GAIN.FormattingEnabled = true;
            this.ID_GAIN.Location = new System.Drawing.Point(154, 65);
            this.ID_GAIN.Name = "ID_GAIN";
            this.ID_GAIN.Size = new System.Drawing.Size(121, 21);
            this.ID_GAIN.TabIndex = 8;
            this.ID_GAIN.SelectedIndexChanged += new System.EventHandler(this.ID_GAIN_SelectedIndexChanged);
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 124);
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

    }
}