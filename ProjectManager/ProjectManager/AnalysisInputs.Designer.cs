namespace ProjectManager
{
    partial class AnalysisInputs
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
            this.roaList = new System.Windows.Forms.ComboBox();
            this.roaLabel = new System.Windows.Forms.Label();
            this.groupsLabel = new System.Windows.Forms.Label();
            this.group1 = new System.Windows.Forms.ComboBox();
            this.group2 = new System.Windows.Forms.ComboBox();
            this.group3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sigTests = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dataList = new System.Windows.Forms.ComboBox();
            this.dataLabel = new System.Windows.Forms.Label();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // roaList
            // 
            this.roaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roaList.FormattingEnabled = true;
            this.roaList.Items.AddRange(new object[] {
            "Injection",
            "Oral/meal"});
            this.roaList.Location = new System.Drawing.Point(12, 39);
            this.roaList.Name = "roaList";
            this.roaList.Size = new System.Drawing.Size(121, 21);
            this.roaList.TabIndex = 0;
            this.roaList.SelectedIndexChanged += new System.EventHandler(this.roaList_SelectedIndexChanged);
            // 
            // roaLabel
            // 
            this.roaLabel.AutoSize = true;
            this.roaLabel.Location = new System.Drawing.Point(12, 20);
            this.roaLabel.Name = "roaLabel";
            this.roaLabel.Size = new System.Drawing.Size(116, 13);
            this.roaLabel.TabIndex = 1;
            this.roaLabel.Text = "Route of Administration";
            // 
            // groupsLabel
            // 
            this.groupsLabel.AutoSize = true;
            this.groupsLabel.Location = new System.Drawing.Point(12, 199);
            this.groupsLabel.Name = "groupsLabel";
            this.groupsLabel.Size = new System.Drawing.Size(41, 13);
            this.groupsLabel.TabIndex = 2;
            this.groupsLabel.Text = "Groups";
            this.groupsLabel.Click += new System.EventHandler(this.groupsLabel_Click);
            // 
            // group1
            // 
            this.group1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group1.FormattingEnabled = true;
            this.group1.Items.AddRange(new object[] {
            "Baseline"});
            this.group1.Location = new System.Drawing.Point(12, 215);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(121, 21);
            this.group1.TabIndex = 3;
            this.group1.SelectedIndexChanged += new System.EventHandler(this.group1_SelectedIndexChanged);
            // 
            // group2
            // 
            this.group2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group2.FormattingEnabled = true;
            this.group2.Items.AddRange(new object[] {
            "Baseline"});
            this.group2.Location = new System.Drawing.Point(12, 254);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(121, 21);
            this.group2.TabIndex = 4;
            this.group2.SelectedIndexChanged += new System.EventHandler(this.group2_SelectedIndexChanged);
            // 
            // group3
            // 
            this.group3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group3.FormattingEnabled = true;
            this.group3.Items.AddRange(new object[] {
            "Baseline"});
            this.group3.Location = new System.Drawing.Point(12, 292);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(121, 21);
            this.group3.TabIndex = 5;
            this.group3.SelectedIndexChanged += new System.EventHandler(this.group3_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Significance Test";
            // 
            // sigTests
            // 
            this.sigTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sigTests.FormattingEnabled = true;
            this.sigTests.Items.AddRange(new object[] {
            "Fisher Exact",
            "Wilcoxon Rank Sum"});
            this.sigTests.Location = new System.Drawing.Point(12, 138);
            this.sigTests.Name = "sigTests";
            this.sigTests.Size = new System.Drawing.Size(121, 21);
            this.sigTests.TabIndex = 7;
            this.sigTests.SelectedIndexChanged += new System.EventHandler(this.sigTests_SelectedIndexChanged);
            // 
            // dataList
            // 
            this.dataList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataList.FormattingEnabled = true;
            this.dataList.Items.AddRange(new object[] {
            "Seizure Burden",
            "Seizure Freedom"});
            this.dataList.Location = new System.Drawing.Point(12, 88);
            this.dataList.Name = "dataList";
            this.dataList.Size = new System.Drawing.Size(121, 21);
            this.dataList.TabIndex = 9;
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(12, 72);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(30, 13);
            this.dataLabel.TabIndex = 8;
            this.dataLabel.Text = "Data";
            // 
            // analyzeButton
            // 
            this.analyzeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.analyzeButton.Location = new System.Drawing.Point(36, 322);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(75, 23);
            this.analyzeButton.TabIndex = 10;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = true;
            // 
            // AnalysisInputs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(146, 357);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.dataList);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.sigTests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.group1);
            this.Controls.Add(this.groupsLabel);
            this.Controls.Add(this.roaLabel);
            this.Controls.Add(this.roaList);
            this.Name = "AnalysisInputs";
            this.Text = "AnalysisInputs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox roaList;
        private System.Windows.Forms.Label roaLabel;
        private System.Windows.Forms.Label groupsLabel;
        private System.Windows.Forms.ComboBox group1;
        private System.Windows.Forms.ComboBox group2;
        private System.Windows.Forms.ComboBox group3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sigTests;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox dataList;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.Button analyzeButton;
    }
}