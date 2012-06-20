namespace SeizurePlayback
{
    partial class AdvanceSettings
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
            this.Retrybox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CRFBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartBox = new System.Windows.Forms.TextBox();
            this.Okbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Retrybox
            // 
            this.Retrybox.AutoSize = true;
            this.Retrybox.Location = new System.Drawing.Point(12, 49);
            this.Retrybox.Name = "Retrybox";
            this.Retrybox.Size = new System.Drawing.Size(121, 17);
            this.Retrybox.TabIndex = 0;
            this.Retrybox.Text = "Only > 1.7 GB (retry)";
            this.Retrybox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CRF";
            // 
            // CRFBox
            // 
            this.CRFBox.Location = new System.Drawing.Point(12, 23);
            this.CRFBox.Name = "CRFBox";
            this.CRFBox.Size = new System.Drawing.Size(100, 20);
            this.CRFBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start From";
            // 
            // StartBox
            // 
            this.StartBox.Location = new System.Drawing.Point(156, 25);
            this.StartBox.Name = "StartBox";
            this.StartBox.Size = new System.Drawing.Size(100, 20);
            this.StartBox.TabIndex = 4;
            // 
            // Okbutton
            // 
            this.Okbutton.Location = new System.Drawing.Point(181, 49);
            this.Okbutton.Name = "Okbutton";
            this.Okbutton.Size = new System.Drawing.Size(75, 23);
            this.Okbutton.TabIndex = 5;
            this.Okbutton.Text = "OK";
            this.Okbutton.UseVisualStyleBackColor = true;
            this.Okbutton.Click += new System.EventHandler(this.Okbutton_Click);
            // 
            // AdvanceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 85);
            this.Controls.Add(this.Okbutton);
            this.Controls.Add(this.StartBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CRFBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Retrybox);
            this.Name = "AdvanceSettings";
            this.Text = "Advanced Compression Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Retrybox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CRFBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StartBox;
        private System.Windows.Forms.Button Okbutton;
    }
}