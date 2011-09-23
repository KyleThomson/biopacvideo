namespace BioPacVideo
{
    partial class FeederTester
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
            this.IDC_RUNTEST = new System.Windows.Forms.Button();
            this.FeederNum = new System.Windows.Forms.TextBox();
            this.PelletsNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ChanSel = new System.Windows.Forms.ComboBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // IDC_RUNTEST
            // 
            this.IDC_RUNTEST.Location = new System.Drawing.Point(12, 137);
            this.IDC_RUNTEST.Name = "IDC_RUNTEST";
            this.IDC_RUNTEST.Size = new System.Drawing.Size(127, 33);
            this.IDC_RUNTEST.TabIndex = 0;
            this.IDC_RUNTEST.Text = "Run Test";
            this.IDC_RUNTEST.UseVisualStyleBackColor = true;
            this.IDC_RUNTEST.Click += new System.EventHandler(this.IDC_RUNTEST_Click);
            // 
            // FeederNum
            // 
            this.FeederNum.Location = new System.Drawing.Point(12, 67);
            this.FeederNum.Name = "FeederNum";
            this.FeederNum.Size = new System.Drawing.Size(100, 20);
            this.FeederNum.TabIndex = 1;
            // 
            // PelletsNum
            // 
            this.PelletsNum.Location = new System.Drawing.Point(12, 111);
            this.PelletsNum.Name = "PelletsNum";
            this.PelletsNum.Size = new System.Drawing.Size(100, 20);
            this.PelletsNum.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pellets";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Camera";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Feeder";
            // 
            // ChanSel
            // 
            this.ChanSel.FormattingEnabled = true;
            this.ChanSel.Location = new System.Drawing.Point(12, 22);
            this.ChanSel.Name = "ChanSel";
            this.ChanSel.Size = new System.Drawing.Size(121, 21);
            this.ChanSel.TabIndex = 5;
            this.ChanSel.SelectedIndexChanged += new System.EventHandler(this.ChanSel_SelectedIndexChanged);
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(12, 176);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(190, 63);
            this.StatusBox.TabIndex = 7;
            // 
            // FeederTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 325);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChanSel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PelletsNum);
            this.Controls.Add(this.FeederNum);
            this.Controls.Add(this.IDC_RUNTEST);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeederTester";
            this.Text = "Feeder Test";
            this.Load += new System.EventHandler(this.FeederTester_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IDC_RUNTEST;
        private System.Windows.Forms.TextBox FeederNum;
        private System.Windows.Forms.TextBox PelletsNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ChanSel;
        private System.Windows.Forms.TextBox StatusBox;
    }
}