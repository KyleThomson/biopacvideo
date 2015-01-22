namespace BioPacVideo
{
    partial class FeederErrorBox
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
            this.ErrorList = new System.Windows.Forms.TextBox();
            this.Dismiss = new System.Windows.Forms.Button();
            this.StatusList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ErrorList
            // 
            this.ErrorList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ErrorList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorList.ForeColor = System.Drawing.Color.Black;
            this.ErrorList.Location = new System.Drawing.Point(5, 25);
            this.ErrorList.Multiline = true;
            this.ErrorList.Name = "ErrorList";
            this.ErrorList.ReadOnly = true;
            this.ErrorList.Size = new System.Drawing.Size(360, 391);
            this.ErrorList.TabIndex = 0;
            // 
            // Dismiss
            // 
            this.Dismiss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dismiss.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.Dismiss.Location = new System.Drawing.Point(291, 422);
            this.Dismiss.Name = "Dismiss";
            this.Dismiss.Size = new System.Drawing.Size(140, 28);
            this.Dismiss.TabIndex = 1;
            this.Dismiss.Text = "Dismiss";
            this.Dismiss.UseVisualStyleBackColor = true;
            this.Dismiss.Click += new System.EventHandler(this.Dismiss_Click);
            // 
            // StatusList
            // 
            this.StatusList.BackColor = System.Drawing.Color.White;
            this.StatusList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusList.ForeColor = System.Drawing.Color.Black;
            this.StatusList.Location = new System.Drawing.Point(371, 25);
            this.StatusList.Multiline = true;
            this.StatusList.Name = "StatusList";
            this.StatusList.ReadOnly = true;
            this.StatusList.Size = new System.Drawing.Size(360, 391);
            this.StatusList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "ERROR LIST";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(367, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "STATUS";
            // 
            // FeederErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 458);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusList);
            this.Controls.Add(this.Dismiss);
            this.Controls.Add(this.ErrorList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeederErrorBox";
            this.Text = "Feeder Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ErrorList;
        private System.Windows.Forms.Button Dismiss;
        private System.Windows.Forms.TextBox StatusList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}