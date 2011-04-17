namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class SetBaseClassAndInterfacesForm
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
            this.baseClassTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.interfacesTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dbTree = new System.Windows.Forms.TreeView();
            this.hideDerivedClassesCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Base Class";
            // 
            // baseClassTextBox
            // 
            this.baseClassTextBox.Location = new System.Drawing.Point(165, 8);
            this.baseClassTextBox.Name = "baseClassTextBox";
            this.baseClassTextBox.Size = new System.Drawing.Size(359, 20);
            this.baseClassTextBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(370, 403);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(451, 403);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // interfacesTextBox
            // 
            this.interfacesTextBox.Location = new System.Drawing.Point(165, 31);
            this.interfacesTextBox.Name = "interfacesTextBox";
            this.interfacesTextBox.Size = new System.Drawing.Size(359, 20);
            this.interfacesTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Interfaces (Comma separated)";
            // 
            // dbTree
            // 
            this.dbTree.CheckBoxes = true;
            this.dbTree.Location = new System.Drawing.Point(8, 72);
            this.dbTree.Name = "dbTree";
            this.dbTree.Size = new System.Drawing.Size(518, 327);
            this.dbTree.TabIndex = 7;
            this.dbTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.dbTree_AfterCheck);
            // 
            // hideDerivedClassesCheckBox
            // 
            this.hideDerivedClassesCheckBox.AutoSize = true;
            this.hideDerivedClassesCheckBox.Location = new System.Drawing.Point(14, 52);
            this.hideDerivedClassesCheckBox.Name = "hideDerivedClassesCheckBox";
            this.hideDerivedClassesCheckBox.Size = new System.Drawing.Size(127, 17);
            this.hideDerivedClassesCheckBox.TabIndex = 8;
            this.hideDerivedClassesCheckBox.Text = "Hide Derived Classes";
            this.hideDerivedClassesCheckBox.UseVisualStyleBackColor = true;
            this.hideDerivedClassesCheckBox.CheckedChanged += new System.EventHandler(this.hideDerivedClassesCheckBox_CheckedChanged);
            // 
            // SetBaseClassAndInterfacesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(536, 431);
            this.Controls.Add(this.hideDerivedClassesCheckBox);
            this.Controls.Add(this.dbTree);
            this.Controls.Add(this.interfacesTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.baseClassTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetBaseClassAndInterfacesForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Base class & Interfaces";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox baseClassTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox interfacesTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView dbTree;
        private System.Windows.Forms.CheckBox hideDerivedClassesCheckBox;
    }
}