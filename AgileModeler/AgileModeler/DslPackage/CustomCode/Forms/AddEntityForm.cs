using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;

using AgileFx.AgileModeler.DslPackage.Lib;
using AgileFx.AgileModeler.DslPackage.CustomCode.DomainUtils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class AddEntityForm : Form
    {
        IEnumerable<string> typeNames;

        public AddEntityForm()
        {
            InitializeComponent();
        }

        Store _Store = null;
        public AddEntityForm(Store store)
            : this()
        {
            this._Store = store;
            this.typeNames = _Store.ElementDirectory.FindElements<ModelClass>().Select(e => e.Name);
            entityNameTextBox.Text = GetNewEntityName();
        }

        private void AddEntityForm_Load(object sender, EventArgs e)
        {
            baseClassComboBox.Items.Add("(None)");
            foreach (var name in typeNames)
                baseClassComboBox.Items.Add(name);
            baseClassComboBox.SelectedItem = "(None)";

            foreach (var type in TypeUtil.GetBuiltInTypes())
                propertyTypeComboBox.Items.Add(type);

            propertyTypeComboBox.SelectedItem = "Int64";
        }

        private void AddEntityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(entityNameTextBox.Text))
                {
                    MessageBox.Show("The EntityType name is not valid.");
                    e.Cancel = true;
                }
            }
        }

        private void baseClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyPropertyGroupBox.Enabled = ((string)baseClassComboBox.SelectedItem == "(None)");
        }

        private string GetNewEntityName()
        {
            var matchingEntities = this.typeNames.Where(e => Regex.Match(e, "Entity[0-9]+").Success);
            var maxIndex = (matchingEntities.Count() > 0) ? matchingEntities.Select(e => int.Parse(e.Substring(6))).Max() : 0;
            return string.Format("Entity{0}", maxIndex + 1);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ModelerTransaction.Enter(() =>
                {
                    using (Transaction tx = _Store.TransactionManager.BeginTransaction())
                    {
                        var entity = new ModelClass(_Store)
                        {
                            Name = entityNameTextBox.Text,
                            TableName = entityNameTextBox.Text
                        };
                        _Store.ElementDirectory.FindElements<ModelRoot>().Single().Types.Add(entity);

                        if (baseClassComboBox.Text != "(None)")
                        {
                            var baseClass = _Store.ElementDirectory.FindElements<ModelClass>()
                                .First(m => m.Name == baseClassComboBox.Text);

                            InheritanceUtil.AddInheritance(baseClass, entity);
                        }
                        else if (createKeyCheckBox.Checked)
                        {
                            var propType = (BuiltInTypes)Enum.Parse(typeof(BuiltInTypes), propertyTypeComboBox.Text);
                            entity.Fields.Add(new ModelField(_Store)
                            {
                                IsPrimaryKey = true,
                                Name = propertyNameTextBox.Text,
                                Type = (BuiltInTypes)Enum.Parse(typeof(BuiltInTypes), propertyTypeComboBox.Text),
                                IsIdentity = true,
                                IsDbGenerated = true,
                                ColumnName = propertyNameTextBox.Text
                            });
                        }

                        tx.Commit();
                    }
                });

        }
    }
}
