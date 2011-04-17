using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class ItemDetailsForm : Form
    {
        public ItemDetailsForm()
        {
            InitializeComponent();
        }

        public DiagramInitializeData GetInitializeData()
        {
            return new DiagramInitializeData
            {
                ContextName = contextNameTextBox.Text,
                ImportFromDatabase = importFromDatabaseRadioButton.Checked
            };
        }
    }

    public class DiagramInitializeData
    {
        public string ContextName { get; set; }
        public bool ImportFromDatabase { get; set; }
    }
}
