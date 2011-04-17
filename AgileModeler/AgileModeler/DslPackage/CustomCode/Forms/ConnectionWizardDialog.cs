using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class ConnectionWizardDialog : Form
    {
        private SqlConnectionProperties cp;
        public ConnectionWizardDialog()
        {
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            cp = new SqlConnectionProperties();
            cp["Timeout"] = 10;
            cp.Remove("User Instance");
            uic.Initialize(cp);
        }

        public string ConnectionString
        {
            get { return cp.ConnectionStringBuilder.ConnectionString; }
            set { cp.ConnectionStringBuilder.ConnectionString = value; }
        }

        private void ConnectionDialog_Load(object sender, EventArgs e)
        {
            this.Padding = new Padding(5);           
        }

        private void Advanced_Click(object sender, EventArgs e)
        {
            //Set up a form to display the advanced connection properties
            Form frm = new Form();
            PropertyGrid pg = new PropertyGrid();
            pg.SelectedObject = cp;
            pg.Dock = DockStyle.Fill;
            pg.Parent = frm;
            frm.ShowDialog();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                MessageBox.Show("Test Connection Succeeded.", "Sql Connection Wizard", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        bool TestConnection()
        {
            cp.ConnectionStringBuilder["User Instance"] = false;
            //Test the connection
            using (SqlConnection conn = new SqlConnection(cp.ConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    MessageBox.Show("Test Connection Failed.", "Sql Connection Wizard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
