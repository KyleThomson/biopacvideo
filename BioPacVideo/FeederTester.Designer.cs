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
            this.TestAll = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IDC_RUNTEST
            // 
            this.IDC_RUNTEST.Location = new System.Drawing.Point(274, 591);
            this.IDC_RUNTEST.Name = "IDC_RUNTEST";
            this.IDC_RUNTEST.Size = new System.Drawing.Size(127, 33);
            this.IDC_RUNTEST.TabIndex = 0;
            this.IDC_RUNTEST.Text = "Run Test";
            this.IDC_RUNTEST.UseVisualStyleBackColor = true;
            this.IDC_RUNTEST.Click += new System.EventHandler(this.IDC_RUNTEST_Click);
            // 
            // PelletsNum
            // 
            this.PelletsNum.Location = new System.Drawing.Point(104, 604);
            this.PelletsNum.Name = "PelletsNum";
            this.PelletsNum.Size = new System.Drawing.Size(100, 20);
            this.PelletsNum.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 588);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of Pellets:";
            // 
            // FeederNum
            // 
            this.FeederNum.AutoSize = true;
            this.FeederNum.Location = new System.Drawing.Point(804, 9);
            this.FeederNum.Name = "FeederNum";
            this.FeederNum.Size = new System.Drawing.Size(102, 13);
            this.FeederNum.TabIndex = 5;
            this.FeederNum.Text = "No Feeder Selected";
            // 
            // TestAll
            // 
            this.TestAll.Location = new System.Drawing.Point(463, 591);
            this.TestAll.Name = "TestAll";
            this.TestAll.Size = new System.Drawing.Size(127, 33);
            this.TestAll.TabIndex = 6;
            this.TestAll.Text = "Test All (No Pellets)";
            this.TestAll.UseVisualStyleBackColor = true;
            this.TestAll.Click += new System.EventHandler(this.TestAll_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 591);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "ExecuteAck";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Click on which feeder you would like to test. Only one feeder can be selected at " +
    "a time";
            // 
            // FeederTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 636);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TestAll);
            this.Controls.Add(this.FeederNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PelletsNum);
            this.Controls.Add(this.IDC_RUNTEST);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeederTester";
            this.Text = "Feeder Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IDC_RUNTEST;
        private System.Windows.Forms.TextBox PelletsNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FeederNum;
        private System.Windows.Forms.Button TestAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}