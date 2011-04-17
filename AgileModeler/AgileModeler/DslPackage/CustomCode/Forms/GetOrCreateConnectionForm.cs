using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AgileFx.AgileModeler.DslPackage.Utils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class GetOrCreateConnectionForm : Form
    {
        public GetOrCreateConnectionForm(string databaseName)
        {
            InitializeComponent();
            databaseNameTextBox.Text = databaseName;
        }

        public ConnectionConfigurationType ConfigurationType
        {
            get
            {
                if (createLocalRadioButton.Checked) return ConnectionConfigurationType.CreateLocal;
                else if (chooseDatabaseRadioButton.Checked) return ConnectionConfigurationType.ChooseDatabase;
                else if (customConnectionRadioButton.Checked) return ConnectionConfigurationType.UseCustomConnection;
                else throw new NotSupportedException();
            }
        }

        public string DatabaseName { get { return databaseNameTextBox.Text; } }

        public string ConnectionString { get { return connectionStringTextBox.Text; } }

        private void createLocalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            databaseNameTextBox.Enabled = createLocalRadioButton.Checked;
        }

        private void customConnectionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            connectionStringTextBox.Enabled = customConnectionRadioButton.Enabled;
        }
    }
}
