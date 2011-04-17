namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    partial class ItemDetailsForm
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
            this.contextNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.modelFirstRadioButton = new System.Windows.Forms.RadioButton();
            this.importFromDatabaseRadioButton = new System.Windows.Forms.RadioButton();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contextNameTextBox
            // 
            this.contextNameTextBox.Location = new System.Drawing.Point(12, 29);
            this.contextNameTextBox.Name = "contextNameTextBox";
            this.contextNameTextBox.Size = new System.Drawing.Size(423, 20);
            this.contextNameTextBox.TabIndex = 0;
            this.contextNameTextBox.Text = "Entities";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Context Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "How do you want to start designing your models?";
            // 
            // modelFirstRadioButton
            // 
            this.modelFirstRadioButton.AutoSize = true;
            this.modelFirstRadioButton.Checked = true;
            this.modelFirstRadioButton.Location = new System.Drawing.Point(12, 94);
            this.modelFirstRadioButton.Name = "modelFirstRadioButton";
            this.modelFirstRadioButton.Size = new System.Drawing.Size(313, 17);
            this.modelFirstRadioButton.TabIndex = 3;
            this.modelFirstRadioButton.TabStop = true;
            this.modelFirstRadioButton.Text = "Model First: Use a designer and export models to a database.";
            this.modelFirstRadioButton.UseVisualStyleBackColor = true;
            // 
            // importFromDatabaseRadioButton
            // 
            this.importFromDatabaseRadioButton.AutoSize = true;
            this.importFromDatabaseRadioButton.Location = new System.Drawing.Point(12, 117);
            this.importFromDatabaseRadioButton.Name = "importFromDatabaseRadioButton";
            this.importFromDatabaseRadioButton.Size = new System.Drawing.Size(395, 17);
            this.importFromDatabaseRadioButton.TabIndex = 4;
            this.importFromDatabaseRadioButton.TabStop = true;
            this.importFromDatabaseRadioButton.Text = "Import from database: You can import entities if you have an existing database.";
            this.importFromDatabaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(186, 140);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ItemDetailsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 171);
            this.ControlBox = false;
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.importFromDatabaseRadioButton);
            this.Controls.Add(this.modelFirstRadioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contextNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ItemDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Models";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox contextNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton modelFirstRadioButton;
        private System.Windows.Forms.RadioButton importFromDatabaseRadioButton;
        private System.Windows.Forms.Button okButton;
    }
}