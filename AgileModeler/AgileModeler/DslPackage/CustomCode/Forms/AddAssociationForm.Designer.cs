namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class AddAssociationForm
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
            this.associationNameTextBox = new System.Windows.Forms.TextBox();
            this.associationLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fromFKeyPropertyTextBox = new System.Windows.Forms.TextBox();
            this.fromFKeyCheckBox = new System.Windows.Forms.CheckBox();
            this.fromNavLabel = new System.Windows.Forms.Label();
            this.fromNavigationPropertyNameTextBox = new System.Windows.Forms.TextBox();
            this.fromMultiplicityComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fromEntityComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toFKeyPropertyTextBox = new System.Windows.Forms.TextBox();
            this.toNavLabel = new System.Windows.Forms.Label();
            this.toFKeyCheckBox = new System.Windows.Forms.CheckBox();
            this.toNavigationPropertyNameTextBox = new System.Windows.Forms.TextBox();
            this.toMultiplicityComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toEntityComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.manyToManyGroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.manyToManyToColumnTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.manyToManyFromColumnTextBox = new System.Windows.Forms.TextBox();
            this.manyToManyTableTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.manyToManyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // associationNameTextBox
            // 
            this.associationNameTextBox.Location = new System.Drawing.Point(15, 35);
            this.associationNameTextBox.Name = "associationNameTextBox";
            this.associationNameTextBox.Size = new System.Drawing.Size(365, 20);
            this.associationNameTextBox.TabIndex = 3;
            // 
            // associationLabel
            // 
            this.associationLabel.AutoSize = true;
            this.associationLabel.Location = new System.Drawing.Point(12, 18);
            this.associationLabel.Name = "associationLabel";
            this.associationLabel.Size = new System.Drawing.Size(95, 13);
            this.associationLabel.TabIndex = 2;
            this.associationLabel.Text = "Association Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fromFKeyPropertyTextBox);
            this.groupBox1.Controls.Add(this.fromFKeyCheckBox);
            this.groupBox1.Controls.Add(this.fromNavLabel);
            this.groupBox1.Controls.Add(this.fromNavigationPropertyNameTextBox);
            this.groupBox1.Controls.Add(this.fromMultiplicityComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.fromEntityComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 238);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End";
            // 
            // fromFKeyPropertyTextBox
            // 
            this.fromFKeyPropertyTextBox.Location = new System.Drawing.Point(8, 204);
            this.fromFKeyPropertyTextBox.Name = "fromFKeyPropertyTextBox";
            this.fromFKeyPropertyTextBox.Size = new System.Drawing.Size(165, 20);
            this.fromFKeyPropertyTextBox.TabIndex = 14;
            // 
            // fromFKeyCheckBox
            // 
            this.fromFKeyCheckBox.AutoSize = true;
            this.fromFKeyCheckBox.Location = new System.Drawing.Point(9, 181);
            this.fromFKeyCheckBox.Name = "fromFKeyCheckBox";
            this.fromFKeyCheckBox.Size = new System.Drawing.Size(124, 17);
            this.fromFKeyCheckBox.TabIndex = 13;
            this.fromFKeyCheckBox.Text = "Foreign Key Property";
            this.fromFKeyCheckBox.UseVisualStyleBackColor = true;
            this.fromFKeyCheckBox.Click += new System.EventHandler(this.fromFKeyCheckBox_Click);
            // 
            // fromNavLabel
            // 
            this.fromNavLabel.AutoSize = true;
            this.fromNavLabel.Location = new System.Drawing.Point(6, 125);
            this.fromNavLabel.Name = "fromNavLabel";
            this.fromNavLabel.Size = new System.Drawing.Size(111, 13);
            this.fromNavLabel.TabIndex = 12;
            this.fromNavLabel.Text = "Navigational Property:";
            // 
            // fromNavigationPropertyNameTextBox
            // 
            this.fromNavigationPropertyNameTextBox.Location = new System.Drawing.Point(8, 141);
            this.fromNavigationPropertyNameTextBox.Name = "fromNavigationPropertyNameTextBox";
            this.fromNavigationPropertyNameTextBox.Size = new System.Drawing.Size(165, 20);
            this.fromNavigationPropertyNameTextBox.TabIndex = 10;
            // 
            // fromMultiplicityComboBox
            // 
            this.fromMultiplicityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromMultiplicityComboBox.FormattingEnabled = true;
            this.fromMultiplicityComboBox.Location = new System.Drawing.Point(8, 91);
            this.fromMultiplicityComboBox.Name = "fromMultiplicityComboBox";
            this.fromMultiplicityComboBox.Size = new System.Drawing.Size(166, 21);
            this.fromMultiplicityComboBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Multiplicity";
            // 
            // fromEntityComboBox
            // 
            this.fromEntityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromEntityComboBox.FormattingEnabled = true;
            this.fromEntityComboBox.Location = new System.Drawing.Point(8, 41);
            this.fromEntityComboBox.Name = "fromEntityComboBox";
            this.fromEntityComboBox.Size = new System.Drawing.Size(166, 21);
            this.fromEntityComboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Entity:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.toFKeyPropertyTextBox);
            this.groupBox2.Controls.Add(this.toNavLabel);
            this.groupBox2.Controls.Add(this.toFKeyCheckBox);
            this.groupBox2.Controls.Add(this.toNavigationPropertyNameTextBox);
            this.groupBox2.Controls.Add(this.toMultiplicityComboBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.toEntityComboBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(200, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 238);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "End";
            // 
            // toFKeyPropertyTextBox
            // 
            this.toFKeyPropertyTextBox.Location = new System.Drawing.Point(8, 204);
            this.toFKeyPropertyTextBox.Name = "toFKeyPropertyTextBox";
            this.toFKeyPropertyTextBox.Size = new System.Drawing.Size(165, 20);
            this.toFKeyPropertyTextBox.TabIndex = 16;
            // 
            // toNavLabel
            // 
            this.toNavLabel.AutoSize = true;
            this.toNavLabel.Location = new System.Drawing.Point(6, 125);
            this.toNavLabel.Name = "toNavLabel";
            this.toNavLabel.Size = new System.Drawing.Size(111, 13);
            this.toNavLabel.TabIndex = 11;
            this.toNavLabel.Text = "Navigational Property:";
            // 
            // toFKeyCheckBox
            // 
            this.toFKeyCheckBox.AutoSize = true;
            this.toFKeyCheckBox.Location = new System.Drawing.Point(9, 181);
            this.toFKeyCheckBox.Name = "toFKeyCheckBox";
            this.toFKeyCheckBox.Size = new System.Drawing.Size(124, 17);
            this.toFKeyCheckBox.TabIndex = 15;
            this.toFKeyCheckBox.Text = "Foreign Key Property";
            this.toFKeyCheckBox.UseVisualStyleBackColor = true;
            this.toFKeyCheckBox.Click += new System.EventHandler(this.toFKeyCheckBox_Click);
            // 
            // toNavigationPropertyNameTextBox
            // 
            this.toNavigationPropertyNameTextBox.Location = new System.Drawing.Point(8, 141);
            this.toNavigationPropertyNameTextBox.Name = "toNavigationPropertyNameTextBox";
            this.toNavigationPropertyNameTextBox.Size = new System.Drawing.Size(165, 20);
            this.toNavigationPropertyNameTextBox.TabIndex = 10;
            // 
            // toMultiplicityComboBox
            // 
            this.toMultiplicityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toMultiplicityComboBox.FormattingEnabled = true;
            this.toMultiplicityComboBox.Location = new System.Drawing.Point(8, 91);
            this.toMultiplicityComboBox.Name = "toMultiplicityComboBox";
            this.toMultiplicityComboBox.Size = new System.Drawing.Size(166, 21);
            this.toMultiplicityComboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Multiplicity";
            // 
            // toEntityComboBox
            // 
            this.toEntityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toEntityComboBox.FormattingEnabled = true;
            this.toEntityComboBox.Location = new System.Drawing.Point(8, 41);
            this.toEntityComboBox.Name = "toEntityComboBox";
            this.toEntityComboBox.Size = new System.Drawing.Size(166, 21);
            this.toEntityComboBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Entity:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(225, 437);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(305, 437);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // manyToManyGroupBox
            // 
            this.manyToManyGroupBox.Controls.Add(this.label10);
            this.manyToManyGroupBox.Controls.Add(this.manyToManyToColumnTextBox);
            this.manyToManyGroupBox.Controls.Add(this.label9);
            this.manyToManyGroupBox.Controls.Add(this.manyToManyFromColumnTextBox);
            this.manyToManyGroupBox.Controls.Add(this.manyToManyTableTextBox);
            this.manyToManyGroupBox.Controls.Add(this.label8);
            this.manyToManyGroupBox.Location = new System.Drawing.Point(15, 315);
            this.manyToManyGroupBox.Name = "manyToManyGroupBox";
            this.manyToManyGroupBox.Size = new System.Drawing.Size(365, 112);
            this.manyToManyGroupBox.TabIndex = 16;
            this.manyToManyGroupBox.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(191, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Column name";
            // 
            // manyToManyToColumnTextBox
            // 
            this.manyToManyToColumnTextBox.Location = new System.Drawing.Point(193, 80);
            this.manyToManyToColumnTextBox.Name = "manyToManyToColumnTextBox";
            this.manyToManyToColumnTextBox.Size = new System.Drawing.Size(165, 20);
            this.manyToManyToColumnTextBox.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Column name";
            // 
            // manyToManyFromColumnTextBox
            // 
            this.manyToManyFromColumnTextBox.Location = new System.Drawing.Point(5, 80);
            this.manyToManyFromColumnTextBox.Name = "manyToManyFromColumnTextBox";
            this.manyToManyFromColumnTextBox.Size = new System.Drawing.Size(165, 20);
            this.manyToManyFromColumnTextBox.TabIndex = 24;
            // 
            // manyToManyTableTextBox
            // 
            this.manyToManyTableTextBox.Location = new System.Drawing.Point(5, 32);
            this.manyToManyTableTextBox.Name = "manyToManyTableTextBox";
            this.manyToManyTableTextBox.Size = new System.Drawing.Size(354, 20);
            this.manyToManyTableTextBox.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Many-to-Many Mapping Table Name";
            // 
            // AddAssociationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(392, 472);
            this.Controls.Add(this.manyToManyGroupBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.associationNameTextBox);
            this.Controls.Add(this.associationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAssociationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Association";
            this.Load += new System.EventHandler(this.AddAssociationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.manyToManyGroupBox.ResumeLayout(false);
            this.manyToManyGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label associationLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox associationNameTextBox;
        public System.Windows.Forms.TextBox fromNavigationPropertyNameTextBox;
        public System.Windows.Forms.ComboBox fromMultiplicityComboBox;
        public System.Windows.Forms.ComboBox fromEntityComboBox;
        public System.Windows.Forms.TextBox toNavigationPropertyNameTextBox;
        public System.Windows.Forms.ComboBox toMultiplicityComboBox;
        public System.Windows.Forms.ComboBox toEntityComboBox;
        private System.Windows.Forms.Label fromNavLabel;
        private System.Windows.Forms.Label toNavLabel;
        private System.Windows.Forms.CheckBox fromFKeyCheckBox;
        public System.Windows.Forms.TextBox fromFKeyPropertyTextBox;
        public System.Windows.Forms.TextBox toFKeyPropertyTextBox;
        private System.Windows.Forms.CheckBox toFKeyCheckBox;
        private System.Windows.Forms.GroupBox manyToManyGroupBox;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox manyToManyToColumnTextBox;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox manyToManyFromColumnTextBox;
        public System.Windows.Forms.TextBox manyToManyTableTextBox;
        private System.Windows.Forms.Label label8;
    }
}