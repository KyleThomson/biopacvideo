namespace BioPacVideo
{
    partial class SensorControl
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
            this.IDS_CONTRAST = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IDS_BRIGHTNESS = new System.Windows.Forms.TrackBar();
            this.IDS_SATURATION = new System.Windows.Forms.TrackBar();
            this.IDS_HUE = new System.Windows.Forms.TrackBar();
            this.IDB_OK = new System.Windows.Forms.Button();
            this.IDT_CONTRAST = new System.Windows.Forms.TextBox();
            this.IDT_BRIGHTNESS = new System.Windows.Forms.TextBox();
            this.IDT_SATURATION = new System.Windows.Forms.TextBox();
            this.IDT_HUE = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_CONTRAST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_BRIGHTNESS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_SATURATION)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_HUE)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contrast";
            // 
            // IDS_CONTRAST
            // 
            this.IDS_CONTRAST.Location = new System.Drawing.Point(89, 6);
            this.IDS_CONTRAST.Maximum = 100;
            this.IDS_CONTRAST.Name = "IDS_CONTRAST";
            this.IDS_CONTRAST.Size = new System.Drawing.Size(371, 42);
            this.IDS_CONTRAST.TabIndex = 2;
            this.IDS_CONTRAST.TickStyle = System.Windows.Forms.TickStyle.None;
            this.IDS_CONTRAST.Scroll += new System.EventHandler(this.IDS_CONTRAST_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Brightness";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Saturation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Hue";
            // 
            // IDS_BRIGHTNESS
            // 
            this.IDS_BRIGHTNESS.Location = new System.Drawing.Point(89, 32);
            this.IDS_BRIGHTNESS.Maximum = 100;
            this.IDS_BRIGHTNESS.Name = "IDS_BRIGHTNESS";
            this.IDS_BRIGHTNESS.Size = new System.Drawing.Size(371, 42);
            this.IDS_BRIGHTNESS.TabIndex = 6;
            this.IDS_BRIGHTNESS.TickStyle = System.Windows.Forms.TickStyle.None;
            this.IDS_BRIGHTNESS.Scroll += new System.EventHandler(this.IDS_BRIGHTNESS_Scroll);
            // 
            // IDS_SATURATION
            // 
            this.IDS_SATURATION.Location = new System.Drawing.Point(89, 58);
            this.IDS_SATURATION.Maximum = 100;
            this.IDS_SATURATION.Name = "IDS_SATURATION";
            this.IDS_SATURATION.Size = new System.Drawing.Size(371, 42);
            this.IDS_SATURATION.TabIndex = 7;
            this.IDS_SATURATION.TickStyle = System.Windows.Forms.TickStyle.None;
            this.IDS_SATURATION.Scroll += new System.EventHandler(this.IDS_SATURATION_Scroll);
            // 
            // IDS_HUE
            // 
            this.IDS_HUE.Location = new System.Drawing.Point(89, 84);
            this.IDS_HUE.Maximum = 100;
            this.IDS_HUE.Name = "IDS_HUE";
            this.IDS_HUE.Size = new System.Drawing.Size(371, 42);
            this.IDS_HUE.TabIndex = 8;
            this.IDS_HUE.TickStyle = System.Windows.Forms.TickStyle.None;
            this.IDS_HUE.Scroll += new System.EventHandler(this.IDS_HUE_Scroll);
            // 
            // IDB_OK
            // 
            this.IDB_OK.Location = new System.Drawing.Point(453, 110);
            this.IDB_OK.Name = "IDB_OK";
            this.IDB_OK.Size = new System.Drawing.Size(75, 23);
            this.IDB_OK.TabIndex = 9;
            this.IDB_OK.Text = "OK";
            this.IDB_OK.UseVisualStyleBackColor = true;
            this.IDB_OK.Click += new System.EventHandler(this.IDB_OK_Click);
            // 
            // IDT_CONTRAST
            // 
            this.IDT_CONTRAST.Enabled = false;
            this.IDT_CONTRAST.Location = new System.Drawing.Point(466, 6);
            this.IDT_CONTRAST.Name = "IDT_CONTRAST";
            this.IDT_CONTRAST.Size = new System.Drawing.Size(48, 20);
            this.IDT_CONTRAST.TabIndex = 10;
            // 
            // IDT_BRIGHTNESS
            // 
            this.IDT_BRIGHTNESS.Enabled = false;
            this.IDT_BRIGHTNESS.Location = new System.Drawing.Point(466, 32);
            this.IDT_BRIGHTNESS.Name = "IDT_BRIGHTNESS";
            this.IDT_BRIGHTNESS.Size = new System.Drawing.Size(48, 20);
            this.IDT_BRIGHTNESS.TabIndex = 11;
            // 
            // IDT_SATURATION
            // 
            this.IDT_SATURATION.Enabled = false;
            this.IDT_SATURATION.Location = new System.Drawing.Point(466, 58);
            this.IDT_SATURATION.Name = "IDT_SATURATION";
            this.IDT_SATURATION.Size = new System.Drawing.Size(48, 20);
            this.IDT_SATURATION.TabIndex = 12;
            // 
            // IDT_HUE
            // 
            this.IDT_HUE.Enabled = false;
            this.IDT_HUE.Location = new System.Drawing.Point(466, 84);
            this.IDT_HUE.Name = "IDT_HUE";
            this.IDT_HUE.Size = new System.Drawing.Size(48, 20);
            this.IDT_HUE.TabIndex = 13;
            // 
            // SensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 142);
            this.Controls.Add(this.IDT_HUE);
            this.Controls.Add(this.IDT_SATURATION);
            this.Controls.Add(this.IDT_BRIGHTNESS);
            this.Controls.Add(this.IDT_CONTRAST);
            this.Controls.Add(this.IDB_OK);
            this.Controls.Add(this.IDS_HUE);
            this.Controls.Add(this.IDS_SATURATION);
            this.Controls.Add(this.IDS_BRIGHTNESS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IDS_CONTRAST);
            this.Controls.Add(this.label1);
            this.Name = "SensorControl";
            this.Text = "SensorControl";
            ((System.ComponentModel.ISupportInitialize)(this.IDS_CONTRAST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_BRIGHTNESS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_SATURATION)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IDS_HUE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar IDS_CONTRAST;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar IDS_BRIGHTNESS;
        private System.Windows.Forms.TrackBar IDS_SATURATION;
        private System.Windows.Forms.TrackBar IDS_HUE;
        private System.Windows.Forms.Button IDB_OK;
        private System.Windows.Forms.TextBox IDT_CONTRAST;
        private System.Windows.Forms.TextBox IDT_BRIGHTNESS;
        private System.Windows.Forms.TextBox IDT_SATURATION;
        private System.Windows.Forms.TextBox IDT_HUE;
    }
}