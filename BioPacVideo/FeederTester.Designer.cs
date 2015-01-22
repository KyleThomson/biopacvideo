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
            this.PelletsNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FeederNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IDC_RUNTEST
            // 
            this.IDC_RUNTEST.Location = new System.Drawing.Point(12, 77);
            this.IDC_RUNTEST.Name = "IDC_RUNTEST";
            this.IDC_RUNTEST.Size = new System.Drawing.Size(127, 33);
            this.IDC_RUNTEST.TabIndex = 0;
            this.IDC_RUNTEST.Text = "Run Test";
            this.IDC_RUNTEST.UseVisualStyleBackColor = true;
            this.IDC_RUNTEST.Click += new System.EventHandler(this.IDC_RUNTEST_Click);
            // 
            // PelletsNum
            // 
            this.PelletsNum.Location = new System.Drawing.Point(12, 51);
            this.PelletsNum.Name = "PelletsNum";
            this.PelletsNum.Size = new System.Drawing.Size(100, 20);
            this.PelletsNum.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pellets";
            // 
            // FeederNum
            // 
            this.FeederNum.AutoSize = true;
            this.FeederNum.Location = new System.Drawing.Point(12, 9);
            this.FeederNum.Name = "FeederNum";
            this.FeederNum.Size = new System.Drawing.Size(43, 13);
            this.FeederNum.TabIndex = 5;
            this.FeederNum.Text = "Feeder:";
            // 
            // FeederTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 449);
            this.Controls.Add(this.FeederNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PelletsNum);
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
        private System.Windows.Forms.TextBox PelletsNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FeederNum;
    }
}