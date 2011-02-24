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
            this.SuspendLayout();
            // 
            // ID_SRATE
            // 
            this.ID_SRATE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_SRATE.FormattingEnabled = true;
            this.ID_SRATE.Location = new System.Drawing.Point(12, 46);
            this.ID_SRATE.Name = "ID_SRATE";
            this.ID_SRATE.Size = new System.Drawing.Size(121, 21);
            this.ID_SRATE.TabIndex = 0;        
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Display Window Size";
            // 
            // ID_DWS
            // 
            this.ID_DWS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ID_DWS.FormattingEnabled = true;
            this.ID_DWS.Location = new System.Drawing.Point(12, 100);
            this.ID_DWS.Name = "ID_DWS";
            this.ID_DWS.Size = new System.Drawing.Size(121, 21);
            this.ID_DWS.TabIndex = 2;
            // 
            // ID_OK
            // 
            this.ID_OK.Location = new System.Drawing.Point(247, 141);
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
            this.IDC_MINMAXV.Location = new System.Drawing.Point(154, 46);
            this.IDC_MINMAXV.Name = "IDC_MINMAXV";
            this.IDC_MINMAXV.Size = new System.Drawing.Size(121, 21);
            this.IDC_MINMAXV.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Min/Max Voltage";
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 178);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IDC_MINMAXV);
            this.Controls.Add(this.ID_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ID_DWS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ID_SRATE);
            this.Name = "RecordSettings";
            this.Text = "Biopac Record Settings";
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

    }
}