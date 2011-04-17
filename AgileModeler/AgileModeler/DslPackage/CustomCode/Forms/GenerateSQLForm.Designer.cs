namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class GenerateSQLForm
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
            this.cleanupDbSchemaCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.useNavPropNamesForFKeysCheckBox = new System.Windows.Forms.CheckBox();
            this.overwriteDatabaseCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cleanupDbSchemaCheckBox
            // 
            this.cleanupDbSchemaCheckBox.AutoSize = true;
            this.cleanupDbSchemaCheckBox.Checked = true;
            this.cleanupDbSchemaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cleanupDbSchemaCheckBox.Location = new System.Drawing.Point(15, 34);
            this.cleanupDbSchemaCheckBox.Name = "cleanupDbSchemaCheckBox";
            this.cleanupDbSchemaCheckBox.Size = new System.Drawing.Size(125, 17);
            this.cleanupDbSchemaCheckBox.TabIndex = 0;
            this.cleanupDbSchemaCheckBox.Text = "Cleanup DB Schema";
            this.cleanupDbSchemaCheckBox.UseVisualStyleBackColor = true;
            this.cleanupDbSchemaCheckBox.CheckedChanged += new System.EventHandler(this.cleanupDbSchemaCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select File";
            // 
            // fileTextBox
            // 
            this.fileTextBox.Location = new System.Drawing.Point(75, 7);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(215, 20);
            this.fileTextBox.TabIndex = 2;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(296, 6);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(215, 110);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(296, 110);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // useNavPropNamesForFKeysCheckBox
            // 
            this.useNavPropNamesForFKeysCheckBox.AutoSize = true;
            this.useNavPropNamesForFKeysCheckBox.Location = new System.Drawing.Point(15, 80);
            this.useNavPropNamesForFKeysCheckBox.Name = "useNavPropNamesForFKeysCheckBox";
            this.useNavPropNamesForFKeysCheckBox.Size = new System.Drawing.Size(246, 17);
            this.useNavPropNamesForFKeysCheckBox.TabIndex = 6;
            this.useNavPropNamesForFKeysCheckBox.Text = "Use NavigationProperty name for Foreign Keys";
            this.useNavPropNamesForFKeysCheckBox.UseVisualStyleBackColor = true;
            // 
            // overwriteDatabaseCheckBox
            // 
            this.overwriteDatabaseCheckBox.AutoSize = true;
            this.overwriteDatabaseCheckBox.Checked = true;
            this.overwriteDatabaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwriteDatabaseCheckBox.Location = new System.Drawing.Point(35, 57);
            this.overwriteDatabaseCheckBox.Name = "overwriteDatabaseCheckBox";
            this.overwriteDatabaseCheckBox.Size = new System.Drawing.Size(225, 17);
            this.overwriteDatabaseCheckBox.TabIndex = 7;
            this.overwriteDatabaseCheckBox.Text = "Overwrite Database (Recreates database)";
            this.overwriteDatabaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // GenerateSQLForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(381, 145);
            this.Controls.Add(this.overwriteDatabaseCheckBox);
            this.Controls.Add(this.useNavPropNamesForFKeysCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cleanupDbSchemaCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateSQLForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate SQL Scripts from Datamodel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cleanupDbSchemaCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox useNavPropNamesForFKeysCheckBox;
        private System.Windows.Forms.CheckBox overwriteDatabaseCheckBox;
    }
}