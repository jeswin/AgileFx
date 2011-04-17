namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class GetOrCreateConnectionForm
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
            this.chooseDatabaseRadioButton = new System.Windows.Forms.RadioButton();
            this.createLocalRadioButton = new System.Windows.Forms.RadioButton();
            this.okButton = new System.Windows.Forms.Button();
            this.databaseNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.customConnectionRadioButton = new System.Windows.Forms.RadioButton();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chooseDatabaseRadioButton
            // 
            this.chooseDatabaseRadioButton.AutoSize = true;
            this.chooseDatabaseRadioButton.Checked = true;
            this.chooseDatabaseRadioButton.Location = new System.Drawing.Point(6, 12);
            this.chooseDatabaseRadioButton.Name = "chooseDatabaseRadioButton";
            this.chooseDatabaseRadioButton.Size = new System.Drawing.Size(151, 17);
            this.chooseDatabaseRadioButton.TabIndex = 0;
            this.chooseDatabaseRadioButton.TabStop = true;
            this.chooseDatabaseRadioButton.Text = "Let me choose a database";
            this.chooseDatabaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // createLocalRadioButton
            // 
            this.createLocalRadioButton.AutoSize = true;
            this.createLocalRadioButton.Location = new System.Drawing.Point(6, 44);
            this.createLocalRadioButton.Name = "createLocalRadioButton";
            this.createLocalRadioButton.Size = new System.Drawing.Size(147, 17);
            this.createLocalRadioButton.TabIndex = 1;
            this.createLocalRadioButton.Text = "Create a database named";
            this.createLocalRadioButton.UseVisualStyleBackColor = true;
            this.createLocalRadioButton.CheckedChanged += new System.EventHandler(this.createLocalRadioButton_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(253, 134);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // databaseNameTextBox
            // 
            this.databaseNameTextBox.Location = new System.Drawing.Point(155, 45);
            this.databaseNameTextBox.Name = "databaseNameTextBox";
            this.databaseNameTextBox.Size = new System.Drawing.Size(169, 20);
            this.databaseNameTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "on the local server.";
            // 
            // customConnectionRadioButton
            // 
            this.customConnectionRadioButton.AutoSize = true;
            this.customConnectionRadioButton.Location = new System.Drawing.Point(6, 88);
            this.customConnectionRadioButton.Name = "customConnectionRadioButton";
            this.customConnectionRadioButton.Size = new System.Drawing.Size(147, 17);
            this.customConnectionRadioButton.TabIndex = 5;
            this.customConnectionRadioButton.Text = "Use this connection string";
            this.customConnectionRadioButton.UseVisualStyleBackColor = true;
            this.customConnectionRadioButton.CheckedChanged += new System.EventHandler(this.customConnectionRadioButton_CheckedChanged);
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Enabled = false;
            this.connectionStringTextBox.Location = new System.Drawing.Point(26, 108);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.Size = new System.Drawing.Size(302, 20);
            this.connectionStringTextBox.TabIndex = 6;
            // 
            // GetOrCreateConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 164);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.customConnectionRadioButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.databaseNameTextBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.createLocalRadioButton);
            this.Controls.Add(this.chooseDatabaseRadioButton);
            this.Name = "GetOrCreateConnectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a Database to save models";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton chooseDatabaseRadioButton;
        private System.Windows.Forms.RadioButton createLocalRadioButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox databaseNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton customConnectionRadioButton;
        private System.Windows.Forms.TextBox connectionStringTextBox;
    }
}