namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class AddEntityForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.baseClassComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.entityNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keyPropertyGroupBox = new System.Windows.Forms.GroupBox();
            this.propertyTypeComboBox = new System.Windows.Forms.ComboBox();
            this.propertyNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.createKeyCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.keyPropertyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.baseClassComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.entityNameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // baseClassComboBox
            // 
            this.baseClassComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseClassComboBox.FormattingEnabled = true;
            this.baseClassComboBox.Location = new System.Drawing.Point(9, 92);
            this.baseClassComboBox.Name = "baseClassComboBox";
            this.baseClassComboBox.Size = new System.Drawing.Size(352, 21);
            this.baseClassComboBox.TabIndex = 3;
            this.baseClassComboBox.SelectedIndexChanged += new System.EventHandler(this.baseClassComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base Entity:";
            // 
            // entityNameTextBox
            // 
            this.entityNameTextBox.Location = new System.Drawing.Point(9, 42);
            this.entityNameTextBox.Name = "entityNameTextBox";
            this.entityNameTextBox.Size = new System.Drawing.Size(352, 20);
            this.entityNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Name:";
            // 
            // keyPropertyGroupBox
            // 
            this.keyPropertyGroupBox.Controls.Add(this.propertyTypeComboBox);
            this.keyPropertyGroupBox.Controls.Add(this.propertyNameTextBox);
            this.keyPropertyGroupBox.Controls.Add(this.label5);
            this.keyPropertyGroupBox.Controls.Add(this.createKeyCheckBox);
            this.keyPropertyGroupBox.Controls.Add(this.label4);
            this.keyPropertyGroupBox.Location = new System.Drawing.Point(12, 149);
            this.keyPropertyGroupBox.Name = "keyPropertyGroupBox";
            this.keyPropertyGroupBox.Size = new System.Drawing.Size(366, 143);
            this.keyPropertyGroupBox.TabIndex = 1;
            this.keyPropertyGroupBox.TabStop = false;
            this.keyPropertyGroupBox.Text = "Key Property";
            // 
            // propertyTypeComboBox
            // 
            this.propertyTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.propertyTypeComboBox.FormattingEnabled = true;
            this.propertyTypeComboBox.Location = new System.Drawing.Point(9, 114);
            this.propertyTypeComboBox.Name = "propertyTypeComboBox";
            this.propertyTypeComboBox.Size = new System.Drawing.Size(352, 21);
            this.propertyTypeComboBox.TabIndex = 7;
            // 
            // propertyNameTextBox
            // 
            this.propertyNameTextBox.Location = new System.Drawing.Point(9, 66);
            this.propertyNameTextBox.Name = "propertyNameTextBox";
            this.propertyNameTextBox.Size = new System.Drawing.Size(352, 20);
            this.propertyNameTextBox.TabIndex = 7;
            this.propertyNameTextBox.Text = "Id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Property Type:";
            // 
            // createKeyCheckBox
            // 
            this.createKeyCheckBox.AutoSize = true;
            this.createKeyCheckBox.Checked = true;
            this.createKeyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createKeyCheckBox.Location = new System.Drawing.Point(9, 20);
            this.createKeyCheckBox.Name = "createKeyCheckBox";
            this.createKeyCheckBox.Size = new System.Drawing.Size(118, 17);
            this.createKeyCheckBox.TabIndex = 0;
            this.createKeyCheckBox.Text = "Create key property";
            this.createKeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Property Name:";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(304, 301);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(223, 301);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AddEntityForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(392, 333);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.keyPropertyGroupBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEntityForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Entity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddEntityForm_FormClosing);
            this.Load += new System.EventHandler(this.AddEntityForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.keyPropertyGroupBox.ResumeLayout(false);
            this.keyPropertyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox keyPropertyGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox propertyTypeComboBox;
        private System.Windows.Forms.TextBox propertyNameTextBox;
        private System.Windows.Forms.CheckBox createKeyCheckBox;
        public System.Windows.Forms.ComboBox baseClassComboBox;
        public System.Windows.Forms.TextBox entityNameTextBox;
    }
}