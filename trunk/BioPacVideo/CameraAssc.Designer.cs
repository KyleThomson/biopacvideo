namespace BioPacVideo
{
    partial class CameraAssc
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
            this.CamSel = new System.Windows.Forms.ComboBox();
            this.ChanSel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CABox = new System.Windows.Forms.TextBox();
            this.SetButton = new System.Windows.Forms.Button();
            this.FinishButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CamSel
            // 
            this.CamSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CamSel.FormattingEnabled = true;
            this.CamSel.Location = new System.Drawing.Point(12, 71);
            this.CamSel.Name = "CamSel";
            this.CamSel.Size = new System.Drawing.Size(121, 21);
            this.CamSel.TabIndex = 0;
            this.CamSel.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ChanSel
            // 
            this.ChanSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChanSel.FormattingEnabled = true;
            this.ChanSel.Location = new System.Drawing.Point(12, 31);
            this.ChanSel.Name = "ChanSel";
            this.ChanSel.Size = new System.Drawing.Size(121, 21);
            this.ChanSel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Camera Select";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Channel Select";
            // 
            // CABox
            // 
            this.CABox.Enabled = false;
            this.CABox.Location = new System.Drawing.Point(12, 127);
            this.CABox.Multiline = true;
            this.CABox.Name = "CABox";
            this.CABox.Size = new System.Drawing.Size(154, 263);
            this.CABox.TabIndex = 4;
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(13, 98);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(75, 23);
            this.SetButton.TabIndex = 5;
            this.SetButton.Text = "Set";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // FinishButton
            // 
            this.FinishButton.Location = new System.Drawing.Point(13, 396);
            this.FinishButton.Name = "FinishButton";
            this.FinishButton.Size = new System.Drawing.Size(75, 23);
            this.FinishButton.TabIndex = 7;
            this.FinishButton.Text = "Finish";
            this.FinishButton.UseVisualStyleBackColor = true;
            this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
            // 
            // CameraAssc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 482);
            this.Controls.Add(this.FinishButton);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.CABox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChanSel);
            this.Controls.Add(this.CamSel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CameraAssc";
            this.Text = "Camera Association";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CamSel;
        private System.Windows.Forms.ComboBox ChanSel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CABox;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Button FinishButton;
    }
}