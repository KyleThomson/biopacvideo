namespace SeizurePlayback
{
    partial class AddGroup
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
            this.AnimalList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupsList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Enroll = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.GroupNameBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Animal ID#";
            // 
            // AnimalList
            // 
            this.AnimalList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnimalList.FormattingEnabled = true;
            this.AnimalList.Location = new System.Drawing.Point(15, 80);
            this.AnimalList.Name = "AnimalList";
            this.AnimalList.Size = new System.Drawing.Size(121, 21);
            this.AnimalList.TabIndex = 1;
            this.AnimalList.SelectedIndexChanged += new System.EventHandler(this.AnimalList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Create Group:";
            // 
            // GroupsList
            // 
            this.GroupsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupsList.FormattingEnabled = true;
            this.GroupsList.Location = new System.Drawing.Point(142, 80);
            this.GroupsList.Name = "GroupsList";
            this.GroupsList.Size = new System.Drawing.Size(121, 21);
            this.GroupsList.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Group";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add Group";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // Enroll
            // 
            this.Enroll.Location = new System.Drawing.Point(269, 78);
            this.Enroll.Name = "Enroll";
            this.Enroll.Size = new System.Drawing.Size(75, 23);
            this.Enroll.TabIndex = 7;
            this.Enroll.Text = "Enroll";
            this.Enroll.UseVisualStyleBackColor = true;
            this.Enroll.Click += new System.EventHandler(this.Enroll_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(269, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(253, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Delete Group";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // GroupNameBox
            // 
            this.GroupNameBox.FormattingEnabled = true;
            this.GroupNameBox.Location = new System.Drawing.Point(12, 25);
            this.GroupNameBox.Name = "GroupNameBox";
            this.GroupNameBox.Size = new System.Drawing.Size(154, 21);
            this.GroupNameBox.TabIndex = 10;
            this.GroupNameBox.SelectedIndexChanged += new System.EventHandler(this.GroupNameBox_SelectedIndexChanged);
            // 
            // AddGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 142);
            this.Controls.Add(this.GroupNameBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Enroll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GroupsList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AnimalList);
            this.Controls.Add(this.label1);
            this.Name = "AddGroup";
            this.Text = "Group Enrollment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AnimalList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox GroupsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Enroll;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox GroupNameBox;
    }
}