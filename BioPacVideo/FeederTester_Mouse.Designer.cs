
namespace BioPacVideo
{
    partial class FeederTester_Mouse
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
            this.runTestButton = new System.Windows.Forms.Button();
            this.executeAckButton = new System.Windows.Forms.Button();
            this.pelletLabel = new System.Windows.Forms.Label();
            this.PelletsNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.feederLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runTestButton
            // 
            this.runTestButton.Location = new System.Drawing.Point(296, 388);
            this.runTestButton.Name = "runTestButton";
            this.runTestButton.Size = new System.Drawing.Size(115, 39);
            this.runTestButton.TabIndex = 0;
            this.runTestButton.Text = "Run Test";
            this.runTestButton.UseVisualStyleBackColor = true;
            this.runTestButton.Click += new System.EventHandler(this.IDC_RUNTEST_Click);
            // 
            // executeAckButton
            // 
            this.executeAckButton.Location = new System.Drawing.Point(463, 388);
            this.executeAckButton.Name = "executeAckButton";
            this.executeAckButton.Size = new System.Drawing.Size(115, 39);
            this.executeAckButton.TabIndex = 2;
            this.executeAckButton.Text = "Execute Ack";
            this.executeAckButton.UseVisualStyleBackColor = true;
            this.executeAckButton.Click += new System.EventHandler(this.executeAck_Click);
            // 
            // pelletLabel
            // 
            this.pelletLabel.AutoSize = true;
            this.pelletLabel.Location = new System.Drawing.Point(150, 388);
            this.pelletLabel.Name = "pelletLabel";
            this.pelletLabel.Size = new System.Drawing.Size(93, 13);
            this.pelletLabel.TabIndex = 3;
            this.pelletLabel.Text = "Number of Pellets:";
            // 
            // PelletsNum
            // 
            this.PelletsNum.Location = new System.Drawing.Point(153, 407);
            this.PelletsNum.Name = "PelletsNum";
            this.PelletsNum.Size = new System.Drawing.Size(100, 20);
            this.PelletsNum.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select which feeder to test. Only one feeder can be selected at a time";
            // 
            // feederLab
            // 
            this.feederLab.AutoSize = true;
            this.feederLab.Location = new System.Drawing.Point(591, 9);
            this.feederLab.Name = "feederLab";
            this.feederLab.Size = new System.Drawing.Size(108, 13);
            this.feederLab.TabIndex = 6;
            this.feederLab.Text = "Please Select Feeder";
            // 
            // FeederTester_Mouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 450);
            this.Controls.Add(this.feederLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PelletsNum);
            this.Controls.Add(this.pelletLabel);
            this.Controls.Add(this.executeAckButton);
            this.Controls.Add(this.runTestButton);
            this.Name = "FeederTester_Mouse";
            this.Text = "FeederTester_Mouse";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runTestButton;
        private System.Windows.Forms.Button executeAckButton;
        private System.Windows.Forms.Label pelletLabel;
        private System.Windows.Forms.TextBox PelletsNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label feederLab;
    }
}