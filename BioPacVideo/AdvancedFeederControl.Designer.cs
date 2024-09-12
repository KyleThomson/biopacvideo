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
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxHistory = new System.Windows.Forms.TextBox();
            this.comboBoxCommonCommands = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CommandValue
            // 
            this.CommandValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommandValue.Location = new System.Drawing.Point(182, 429);
            this.CommandValue.Name = "CommandValue";
            this.CommandValue.Size = new System.Drawing.Size(476, 20);
            this.CommandValue.TabIndex = 0;
            this.CommandValue.TextChanged += new System.EventHandler(this.CommandValue_TextChanged);
            this.CommandValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandValue_KeyPressed);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(664, 424);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(110, 29);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxHistory
            // 
            this.textBoxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHistory.Enabled = false;
            this.textBoxHistory.Location = new System.Drawing.Point(12, 12);
            this.textBoxHistory.Multiline = true;
            this.textBoxHistory.Name = "textBoxHistory";
            this.textBoxHistory.ReadOnly = true;
            this.textBoxHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHistory.Size = new System.Drawing.Size(762, 406);
            this.textBoxHistory.TabIndex = 2;
            // 
            // comboBoxCommonCommands
            // 
            this.comboBoxCommonCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxCommonCommands.FormattingEnabled = true;
            this.comboBoxCommonCommands.Items.AddRange(new object[] {
            "",
            "24 - Run All Feeders",
            "25 - Feeder",
            "26 - Pellet",
            "27 - Test States",
            "28 - Acknowledge",
            "29 - Execute",
            "30 - Blink LED",
            "31 - Null Command"});
            this.comboBoxCommonCommands.Location = new System.Drawing.Point(12, 429);
            this.comboBoxCommonCommands.Name = "comboBoxCommonCommands";
            this.comboBoxCommonCommands.Size = new System.Drawing.Size(164, 21);
            this.comboBoxCommonCommands.TabIndex = 3;
            this.comboBoxCommonCommands.SelectedIndexChanged += new System.EventHandler(this.comboBoxCommonCommands_SelectedIndexChanged);
            // 
            // AdvancedFeederControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 461);
            this.Controls.Add(this.comboBoxCommonCommands);
            this.Controls.Add(this.textBoxHistory);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.CommandValue);
            this.Name = "AdvancedFeederControl";
            this.Text = "AdvancedFeederControl";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandValue;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxHistory;
        private System.Windows.Forms.ComboBox comboBoxCommonCommands;
    }
}