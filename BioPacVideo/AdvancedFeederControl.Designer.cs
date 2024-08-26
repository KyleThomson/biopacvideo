namespace BioPacVideo
{
    partial class AdvancedFeederControl
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
            this.CommandValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CommandValue
            // 
            this.CommandValue.Location = new System.Drawing.Point(75, 43);
            this.CommandValue.Name = "CommandValue";
            this.CommandValue.Size = new System.Drawing.Size(100, 20);
            this.CommandValue.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 106);
            this.button1.TabIndex = 1;
            this.button1.Text = "Square Edge Button For Ben";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdvancedFeederControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 228);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CommandValue);
            this.Name = "AdvancedFeederControl";
            this.Text = "AdvancedFeederControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandValue;
        private System.Windows.Forms.Button button1;
    }
}