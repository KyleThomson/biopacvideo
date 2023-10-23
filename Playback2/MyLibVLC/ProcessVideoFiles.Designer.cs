
namespace SeizurePlayback
{
    partial class ProcessVideoFiles
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoadVideoLabel = new System.Windows.Forms.Label();
            this.processProg = new System.Windows.Forms.ProgressBar();
            this.processVidLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Processing Video Files for Seizures";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Do not close the program until complete";
            // 
            // LoadVideoLabel
            // 
            this.LoadVideoLabel.AutoSize = true;
            this.LoadVideoLabel.Location = new System.Drawing.Point(73, 109);
            this.LoadVideoLabel.Name = "LoadVideoLabel";
            this.LoadVideoLabel.Size = new System.Drawing.Size(0, 13);
            this.LoadVideoLabel.TabIndex = 2;
            // 
            // processProg
            // 
            this.processProg.Location = new System.Drawing.Point(67, 109);
            this.processProg.Name = "processProg";
            this.processProg.Size = new System.Drawing.Size(233, 23);
            this.processProg.TabIndex = 3;
            // 
            // processVidLabel
            // 
            this.processVidLabel.AutoSize = true;
            this.processVidLabel.Location = new System.Drawing.Point(67, 93);
            this.processVidLabel.Name = "processVidLabel";
            this.processVidLabel.Size = new System.Drawing.Size(89, 13);
            this.processVidLabel.TabIndex = 4;
            this.processVidLabel.Text = "Processing Video";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(121, 151);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(110, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ProcessVideoFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 187);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.processVidLabel);
            this.Controls.Add(this.processProg);
            this.Controls.Add(this.LoadVideoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProcessVideoFiles";
            this.Text = "LoadVideoFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LoadVideoLabel;
        private System.Windows.Forms.ProgressBar processProg;
        private System.Windows.Forms.Label processVidLabel;
        private System.Windows.Forms.Button closeButton;
    }
}