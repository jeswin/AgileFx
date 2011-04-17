using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class AddInheritanceForm : Form
    {
        IEnumerable<string> typeNames;
        private Store _Store = null;

        public AddInheritanceForm()
        {
            InitializeComponent();
        }

        public AddInheritanceForm(Store store)
            : this()
        {
            this._Store = store;
            this.typeNames = this._Store.ElementDirectory.FindElements<ModelClass>().Select(e => e.Name);
            LoadTypes();
        }

        private void LoadTypes()
        {
            baseClassComboBox.Items.Add("(None)");
            derivedClassComboBox.Items.Add("(None)");

            foreach (var name in typeNames)
            {
                baseClassComboBox.Items.Add(name);
                derivedClassComboBox.Items.Add(name);
            }

            baseClassComboBox.SelectedItem = "(None)";
            derivedClassComboBox.SelectedItem = "(None)";
        }

        private void AddInheritanceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                //? Add some validations in here..
                if (derivedClassComboBox.SelectedItem == baseClassComboBox.SelectedItem)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please select different entities.");
                    baseClassComboBox.SelectedIndex = (baseClassComboBox.Items[0] != derivedClassComboBox.SelectedItem) ? 0 : 1;
                }
            }
        }

        internal void Initialize(string derivedClassName)
        {
            derivedClassComboBox.SelectedItem = derivedClassName;
        }
    }
}
