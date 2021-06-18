namespace ProjectManager
{
    partial class InjectionEdits
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
            this.routeLabel = new System.Windows.Forms.Label();
            this.doseLabel = new System.Windows.Forms.Label();
            this.doseAmtLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.solventLabel = new System.Windows.Forms.Label();
            this.doseTextBox = new System.Windows.Forms.TextBox();
            this.doseAmtTextBox = new System.Windows.Forms.TextBox();
            this.addIdTextBox = new System.Windows.Forms.TextBox();
            this.solventTextBox = new System.Windows.Forms.TextBox();
            this.routeTextBox = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // routeLabel
            // 
            this.routeLabel.AutoSize = true;
            this.routeLabel.Location = new System.Drawing.Point(26, 28);
            this.routeLabel.Name = "routeLabel";
            this.routeLabel.Size = new System.Drawing.Size(36, 13);
            this.routeLabel.TabIndex = 0;
            this.routeLabel.Text = "Route";
            // 
            // doseLabel
            // 
            this.doseLabel.AutoSize = true;
            this.doseLabel.Location = new System.Drawing.Point(26, 64);
            this.doseLabel.Name = "doseLabel";
            this.doseLabel.Size = new System.Drawing.Size(32, 13);
            this.doseLabel.TabIndex = 1;
            this.doseLabel.Text = "Dose";
            // 
            // doseAmtLabel
            // 
            this.doseAmtLabel.AutoSize = true;
            this.doseAmtLabel.Location = new System.Drawing.Point(23, 99);
            this.doseAmtLabel.Name = "doseAmtLabel";
            this.doseAmtLabel.Size = new System.Drawing.Size(74, 13);
            this.doseAmtLabel.TabIndex = 2;
            this.doseAmtLabel.Text = " Dose Amount";
            this.doseAmtLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(26, 137);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(44, 13);
            this.idLabel.TabIndex = 3;
            this.idLabel.Text = "ADD ID";
            // 
            // solventLabel
            // 
            this.solventLabel.AutoSize = true;
            this.solventLabel.Location = new System.Drawing.Point(26, 173);
            this.solventLabel.Name = "solventLabel";
            this.solventLabel.Size = new System.Drawing.Size(43, 13);
            this.solventLabel.TabIndex = 4;
            this.solventLabel.Text = "Solvent";
            // 
            // doseTextBox
            // 
            this.doseTextBox.Location = new System.Drawing.Point(114, 61);
            this.doseTextBox.Name = "doseTextBox";
            this.doseTextBox.Size = new System.Drawing.Size(100, 20);
            this.doseTextBox.TabIndex = 6;
            this.doseTextBox.TextChanged += new System.EventHandler(this.doseTextBox_TextChanged);
            // 
            // doseAmtTextBox
            // 
            this.doseAmtTextBox.Location = new System.Drawing.Point(114, 96);
            this.doseAmtTextBox.Name = "doseAmtTextBox";
            this.doseAmtTextBox.Size = new System.Drawing.Size(100, 20);
            this.doseAmtTextBox.TabIndex = 7;
            this.doseAmtTextBox.TextChanged += new System.EventHandler(this.doseAmtTextBox_TextChanged);
            // 
            // addIdTextBox
            // 
            this.addIdTextBox.Location = new System.Drawing.Point(114, 134);
            this.addIdTextBox.Name = "addIdTextBox";
            this.addIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.addIdTextBox.TabIndex = 8;
            this.addIdTextBox.TextChanged += new System.EventHandler(this.addIdTextBox_TextChanged);
            // 
            // solventTextBox
            // 
            this.solventTextBox.Location = new System.Drawing.Point(114, 170);
            this.solventTextBox.Name = "solventTextBox";
            this.solventTextBox.Size = new System.Drawing.Size(100, 20);
            this.solventTextBox.TabIndex = 9;
            this.solventTextBox.TextChanged += new System.EventHandler(this.solventTextBox_TextChanged);
            // 
            // routeTextBox
            // 
            this.routeTextBox.Location = new System.Drawing.Point(114, 25);
            this.routeTextBox.Name = "routeTextBox";
            this.routeTextBox.Size = new System.Drawing.Size(100, 20);
            this.routeTextBox.TabIndex = 10;
            this.routeTextBox.TextChanged += new System.EventHandler(this.routeTextBox_TextChanged);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(69, 211);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(50, 24);
            this.ok.TabIndex = 11;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(146, 211);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(50, 24);
            this.cancel.TabIndex = 12;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InjectionEdits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 247);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.routeTextBox);
            this.Controls.Add(this.solventTextBox);
            this.Controls.Add(this.addIdTextBox);
            this.Controls.Add(this.doseAmtTextBox);
            this.Controls.Add(this.doseTextBox);
            this.Controls.Add(this.solventLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.doseAmtLabel);
            this.Controls.Add(this.doseLabel);
            this.Controls.Add(this.routeLabel);
            this.Name = "InjectionEdits";
            this.Text = "InjectionEdits";
            this.Load += new System.EventHandler(this.TreatmentEdits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label routeLabel;
        private System.Windows.Forms.Label doseLabel;
        private System.Windows.Forms.Label doseAmtLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label solventLabel;
        private System.Windows.Forms.TextBox doseTextBox;
        private System.Windows.Forms.TextBox doseAmtTextBox;
        private System.Windows.Forms.TextBox addIdTextBox;
        private System.Windows.Forms.TextBox solventTextBox;
        private System.Windows.Forms.TextBox routeTextBox;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
    }
}