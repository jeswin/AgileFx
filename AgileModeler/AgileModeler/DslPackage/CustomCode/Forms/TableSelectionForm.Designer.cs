namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class TableSelectionForm
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
            this.dbTree = new System.Windows.Forms.TreeView();
            this.detectInheritanceCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tablePrefixTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dbTree
            // 
            this.dbTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dbTree.CheckBoxes = true;
            this.dbTree.Location = new System.Drawing.Point(12, 33);
            this.dbTree.Name = "dbTree";
            this.dbTree.Size = new System.Drawing.Size(518, 327);
            this.dbTree.TabIndex = 0;
            this.dbTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.dbTree_AfterCheck);
            // 
            // detectInheritanceCheckBox
            // 
            this.detectInheritanceCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detectInheritanceCheckBox.AutoSize = true;
            this.detectInheritanceCheckBox.Checked = true;
            this.detectInheritanceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.detectInheritanceCheckBox.Location = new System.Drawing.Point(13, 367);
            this.detectInheritanceCheckBox.Name = "detectInheritanceCheckBox";
            this.detectInheritanceCheckBox.Size = new System.Drawing.Size(139, 17);
            this.detectInheritanceCheckBox.TabIndex = 1;
            this.detectInheritanceCheckBox.Text = "Auto Detect Inheritance";
            this.detectInheritanceCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(455, 384);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(377, 384);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ignore Table prefix";
            // 
            // tablePrefixTextBox
            // 
            this.tablePrefixTextBox.Location = new System.Drawing.Point(136, 5);
            this.tablePrefixTextBox.Name = "tablePrefixTextBox";
            this.tablePrefixTextBox.Size = new System.Drawing.Size(147, 20);
            this.tablePrefixTextBox.TabIndex = 6;
            // 
            // TableSelectionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(542, 410);
            this.Controls.Add(this.tablePrefixTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.detectInheritanceCheckBox);
            this.Controls.Add(this.dbTree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableSelectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Table Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView dbTree;
        private System.Windows.Forms.CheckBox detectInheritanceCheckBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tablePrefixTextBox;
    }
}