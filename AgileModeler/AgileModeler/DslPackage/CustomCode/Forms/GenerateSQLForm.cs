using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class GenerateSQLForm : Form
    {
        SaveFileDialog saveDlg = new SaveFileDialog();
        public GenerateSQLForm()
        {
            InitializeComponent();

            //custom initializations
            saveDlg.DefaultExt = ".sql";
            saveDlg.AddExtension = true;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (saveDlg.ShowDialog() == DialogResult.OK) fileTextBox.Text = saveDlg.FileName;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (fileTextBox.Text == string.Empty || !Directory.Exists(Path.GetDirectoryName(fileTextBox.Text)))
            {
                MessageBox.Show("Please select a location");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public string Filename { get { return saveDlg.FileName; } }
        public bool CleanupDbSchema { get { return cleanupDbSchemaCheckBox.Checked; } }
        public bool OverwriteDatabase { get { return overwriteDatabaseCheckBox.Checked; } }
        public bool UseNavigationPropertyNameForFKeys { get { return useNavPropNamesForFKeysCheckBox.Checked; } }

        private void cleanupDbSchemaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cleanupDbSchemaCheckBox.Checked)
                overwriteDatabaseCheckBox.Enabled = overwriteDatabaseCheckBox.Checked = true;
            else
                overwriteDatabaseCheckBox.Enabled = overwriteDatabaseCheckBox.Checked = false;
        }
    }
}
