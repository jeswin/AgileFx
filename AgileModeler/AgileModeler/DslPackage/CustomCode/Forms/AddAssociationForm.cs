using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.CustomCode;
using AgileFx.AgileModeler.DslPackage.Utils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class AddAssociationForm : Form
    {
        public AddAssociationForm()
        {
            InitializeComponent();
        }

        private Store _Store = null;

        public AddAssociationForm(Store store)
            : this()
        {
            this._Store = store;

            LoadTypes();
            LoadMultiplicityValues();

            int fromEntityComboBoxSelectedIndex = fromEntityComboBox.SelectedIndex;
            fromEntityComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (fromEntityComboBoxSelectedIndex != fromEntityComboBox.SelectedIndex)
                {
                    fromEntityComboBoxSelectedIndex = fromEntityComboBox.SelectedIndex;
                    ResetEnd1NavigationPropertyNames(); ResetEnd2NavigationPropertyNames(); ResetCalculatedProperties();
                }
            };

            int toEntityComboBoxSelectedIndex = toEntityComboBox.SelectedIndex;
            toEntityComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (toEntityComboBoxSelectedIndex != toEntityComboBox.SelectedIndex)
                {
                    toEntityComboBoxSelectedIndex = toEntityComboBox.SelectedIndex;
                    ResetEnd1NavigationPropertyNames(); ResetEnd2NavigationPropertyNames(); ResetCalculatedProperties();
                }
            };

            int fromMultiplicityComboBoxSelectedIndex = fromMultiplicityComboBox.SelectedIndex;
            fromMultiplicityComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (fromMultiplicityComboBoxSelectedIndex != fromMultiplicityComboBox.SelectedIndex)
                {
                    fromMultiplicityComboBoxSelectedIndex = fromMultiplicityComboBox.SelectedIndex;
                    ResetEnd2NavigationPropertyNames(); ResetCalculatedProperties();
                }
            };

            int toMultiplicityComboBoxSelectedIndex = toMultiplicityComboBox.SelectedIndex;
            toMultiplicityComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (toMultiplicityComboBoxSelectedIndex != toMultiplicityComboBox.SelectedIndex)
                {
                    ResetEnd1NavigationPropertyNames(); ResetCalculatedProperties();
                    toMultiplicityComboBoxSelectedIndex = toMultiplicityComboBox.SelectedIndex;
                }
            };

            fromFKeyCheckBox.Click += (sender, e) => { ResetCalculatedProperties(); };
            toFKeyCheckBox.Click += (sender, e) => { ResetCalculatedProperties(); };
        }

        private void LoadTypes()
        {
            IEnumerable<string> types = DSLUtil.GetClasses(_Store);

            fromEntityComboBox.Items.Add("Select Entity");
            fromEntityComboBox.Items.AddRange(types.OrderBy(t => t).ToArray());
            fromEntityComboBox.SelectedItem = "Select Entity";

            toEntityComboBox.Items.Add("Select Entity");
            toEntityComboBox.Items.AddRange(types.OrderBy(t => t).ToArray());
            toEntityComboBox.SelectedItem = "Select Entity";

            fromEntityComboBox.Focus();
        }

        private void LoadMultiplicityValues()
        {
            fromMultiplicityComboBox.Items.AddRange(DSLUtil.GetMultiplicityTextValues().ToArray());
            toMultiplicityComboBox.Items.AddRange(DSLUtil.GetMultiplicityTextValues().ToArray());
            fromMultiplicityComboBox.SelectedItem = Multiplicity.One.ToText();
            toMultiplicityComboBox.SelectedItem = Multiplicity.ZeroMany.ToText();
        }

        public void SetSelectedClasses(params ModelClass[] modelClasses)
        {
            if (modelClasses.Length > 0)
            {
                foreach (var item in fromEntityComboBox.Items)
                {
                    if (item.ToString() == modelClasses[0].Name)
                    {
                        fromEntityComboBox.SelectedItem = item;
                    }
                }
                toEntityComboBox.Focus();
            }
            if (modelClasses.Length > 1)
            {
                foreach (var item in toEntityComboBox.Items)
                {
                    if (item.ToString() == modelClasses[1].Name)
                    {
                        toEntityComboBox.SelectedItem = item;
                    }
                }
                fromNavigationPropertyNameTextBox.Focus();
            }
        }


        ModelClass getClassFromStore(string name)
        {
            return _Store.ElementDirectory.FindElements<ModelClass>().FirstOrDefault(c => c.Name == name);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (fromEntityComboBox.Text == "Select Entity" || toEntityComboBox.Text == "Select Entity")
            {
                MessageBox.Show("Select both entities involved in the association.");
                return;
            }

            //If both entities are the same, navigation properties need to be different
            if (fromEntityComboBox.Text == toEntityComboBox.Text)
            {
                if (fromNavigationPropertyNameTextBox.Text == toNavigationPropertyNameTextBox.Text)
                {
                    MessageBox.Show("The " + fromEntityComboBox.Text + " class cannot have two properties of the name "
                        + fromNavigationPropertyNameTextBox.Text + ".");
                    return;
                }
            }

            //Many to many mapping column names have to be different
            if (fromMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText()
                    && toMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText())
            {
                if (manyToManyFromColumnTextBox.Text == manyToManyToColumnTextBox.Text)
                {
                    MessageBox.Show("Specify different names for columns in the Many-to-Many Map Table.");
                    return;
                }
            }

            var startClass = getClassFromStore(fromEntityComboBox.SelectedItem.ToString());
            var endClass = getClassFromStore(toEntityComboBox.SelectedItem.ToString());

            var assoc = new Association(startClass, endClass);
            assoc.Name = associationNameTextBox.Text;

            assoc.End1Multiplicity = DSLUtil.ToMultiplicity(fromMultiplicityComboBox.SelectedItem.ToString());
            assoc.End1RoleName = fromEntityComboBox.SelectedItem.ToString();
            assoc.End1NavigationProperty = fromNavigationPropertyNameTextBox.Text;

            assoc.End2Multiplicity = DSLUtil.ToMultiplicity(toMultiplicityComboBox.SelectedItem.ToString());
            assoc.End2RoleName = toEntityComboBox.SelectedItem.ToString();
            assoc.End2NavigationProperty = toNavigationPropertyNameTextBox.Text;

            if (assoc.End1Multiplicity == Multiplicity.ZeroMany && assoc.End1Multiplicity == Multiplicity.ZeroMany)
            {
                Func<string, string> getNavigationPropertyName = colName => colName.ToLower().EndsWith("id") ? colName.Substring(0, colName.Length - 2) : colName;
                Func<string, string> getFieldName = colName => colName.ToLower().EndsWith("id") ? colName : colName + "Id";
                assoc.ManyToManyMappingTable = manyToManyTableTextBox.Text;

                assoc.End1ManyToManyMappingColumn = manyToManyFromColumnTextBox.Text;
                assoc.End1ManyToManyFieldName = getFieldName(manyToManyFromColumnTextBox.Text);
                assoc.End1ManyToManyNavigationProperty = getNavigationPropertyName(manyToManyFromColumnTextBox.Text);

                assoc.End2ManyToManyMappingColumn = manyToManyToColumnTextBox.Text;
                assoc.End2ManyToManyFieldName = getFieldName(manyToManyToColumnTextBox.Text);
                assoc.End2ManyToManyNavigationProperty = getNavigationPropertyName(manyToManyToColumnTextBox.Text);
            }

            startClass.NavigationProperties.Add(new NavigationProperty(_Store)
            {
                Name = assoc.End1NavigationProperty,
                Association = assoc.Name,
                Multiplicity = assoc.End2Multiplicity,
                Type = (assoc.End2Multiplicity == Multiplicity.ZeroMany) ? "ICollection<" + assoc.End2RoleName + ">" : assoc.End2RoleName,
                IsForeignkey = fromFKeyCheckBox.Checked,
                ForeignkeyColumn = fromFKeyCheckBox.Checked ? fromNavigationPropertyNameTextBox.Text : string.Empty
            });

            endClass.NavigationProperties.Add(new NavigationProperty(_Store)
            {
                Name = assoc.End2NavigationProperty,
                Association = assoc.Name,
                Multiplicity = assoc.End1Multiplicity,
                Type = (assoc.End1Multiplicity == Multiplicity.ZeroMany) ? "ICollection<" + assoc.End1RoleName + ">" : assoc.End1RoleName,
                IsForeignkey = toFKeyCheckBox.Checked,
                ForeignkeyColumn = toFKeyCheckBox.Checked ? toNavigationPropertyNameTextBox.Text : string.Empty
            });

            if (fromFKeyCheckBox.Checked)
            {
                startClass.Fields.Add(new ModelField(_Store)
                {                     
                    Name = fromFKeyPropertyTextBox.Text,
                    ColumnName = fromNavigationPropertyNameTextBox.Text,
                    Type = GetPrimaryKeyFieldType(startClass),
                    Getter = AccessModifier.Public,
                    Setter = AccessModifier.Public,
                    Nullable = toMultiplicityComboBox.SelectedItem.ToString() != Multiplicity.One.ToText()
                });
            }

            if (toFKeyCheckBox.Checked)
            {
                endClass.Fields.Add(new ModelField(_Store)
                {
                    Name = toFKeyPropertyTextBox.Text,
                    ColumnName = toNavigationPropertyNameTextBox.Text,
                    Type = GetPrimaryKeyFieldType(startClass),
                    Getter = AccessModifier.Public,
                    Setter = AccessModifier.Public,
                    Nullable = fromMultiplicityComboBox.SelectedItem.ToString() != Multiplicity.One.ToText()
                });
            }

            this.DialogResult = DialogResult.OK;

        }

        private BuiltInTypes GetPrimaryKeyFieldType(ModelClass klass)
        {
            if (klass.Baseclass != null)
                return GetPrimaryKeyFieldType(klass.Baseclass);
            else
                return klass.Fields.Find(f => f.IsPrimaryKey == true).Type;
        }

        private void toFKeyCheckBox_Click(object sender, EventArgs e)
        {
            if (toFKeyCheckBox.Checked)
                fromFKeyCheckBox.Checked = false;
            else
            {
                //This means that the checkbox cannot be unchecked.
                if (fromMultiplicityComboBox.SelectedItem.ToString() != Multiplicity.ZeroMany.ToText())
                {
                    toFKeyCheckBox.Checked = true;
                    MessageBox.Show("This association needs one Foreign Key.");
                }
            }

            ResetCalculatedProperties();
        }

        private void fromFKeyCheckBox_Click(object sender, EventArgs e)
        {
            if (fromFKeyCheckBox.Checked)
                toFKeyCheckBox.Checked = false;
            else
            {
                //This means that the checkbox cannot be unchecked.
                if (toMultiplicityComboBox.SelectedItem.ToString() != Multiplicity.ZeroMany.ToText())
                {
                    fromFKeyCheckBox.Checked = true;
                    MessageBox.Show("This association needs one Foreign Key.");
                }
            }

            ResetCalculatedProperties();
        }

        private void AddAssociationForm_Load(object sender, EventArgs e)
        {
            ResetCalculatedProperties();
        }

        private void ResetEnd1NavigationPropertyNames()
        {
            var modelClass = getClassFromStore(fromEntityComboBox.SelectedItem.ToString());
            if (modelClass != null)
            {
                fromNavigationPropertyNameTextBox.Text = ModelUtil.GetMemberName(toEntityComboBox.SelectedItem.ToString(),
                    modelClass, toMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText());
            }
        }

        private void ResetEnd2NavigationPropertyNames()
        {
            var modelClass = getClassFromStore(toEntityComboBox.SelectedItem.ToString());
            if (modelClass != null)
            {
                toNavigationPropertyNameTextBox.Text = ModelUtil.GetMemberName(fromEntityComboBox.SelectedItem.ToString(),
                    modelClass, fromMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText());
            }
        }

        //If the (other side is one) or (other side is 0..1 and this side is not one), enable checkbox
        Func<string, string, bool> enabled = (thisEnd, otherEnd) => otherEnd == Multiplicity.One.ToText()
            || (otherEnd == Multiplicity.ZeroOne.ToText() && thisEnd != Multiplicity.One.ToText());
        private void ResetCalculatedProperties()
        {
            if (IsValid())
            {
                SetControlValidity(true);

                fromFKeyCheckBox.Enabled = enabled(fromMultiplicityComboBox.SelectedItem.ToString(), toMultiplicityComboBox.SelectedItem.ToString());
                toFKeyCheckBox.Enabled = enabled(toMultiplicityComboBox.SelectedItem.ToString(), fromMultiplicityComboBox.SelectedItem.ToString());

                fromFKeyCheckBox.Checked = fromFKeyCheckBox.Enabled && (fromFKeyCheckBox.Checked || !toFKeyCheckBox.Enabled);
                toFKeyCheckBox.Checked = toFKeyCheckBox.Enabled && !fromFKeyCheckBox.Checked; ;

                //If checked, then enable textbox. Otherwise clear everything and disable.
                if (fromFKeyCheckBox.Checked)
                {
                    fromFKeyPropertyTextBox.Enabled = true;
                    fromFKeyPropertyTextBox.Text = fromNavigationPropertyNameTextBox.Text + "Id";
                }
                else
                {
                    fromFKeyPropertyTextBox.Enabled = false;
                    fromFKeyPropertyTextBox.Text = "";
                }

                //If checked, then enable textbox. Otherwise clear everything and disable.
                if (toFKeyCheckBox.Checked)
                {
                    toFKeyPropertyTextBox.Enabled = true;
                    toFKeyPropertyTextBox.Text = toNavigationPropertyNameTextBox.Text + "Id";
                }
                else
                {
                    toFKeyPropertyTextBox.Text = "";
                    toFKeyPropertyTextBox.Enabled = false;
                }

                //See if the many-to-many area needs to be hidden/shown.
                //
                if (fromMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText()
                    && toMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText())
                {
                    this.Height = 510;
                    this.manyToManyGroupBox.Visible = true;
                    this.manyToManyGroupBox.Enabled = true;
                    manyToManyTableTextBox.Text = string.Format("{0}{1}Map", fromEntityComboBox.Text, toEntityComboBox.Text);

                    manyToManyFromColumnTextBox.Text = toEntityComboBox.Text;
                    manyToManyToColumnTextBox.Text = fromEntityComboBox.Text;
                }
                else
                {
                    this.manyToManyGroupBox.Visible = false;
                    this.manyToManyGroupBox.Enabled = false;
                    this.Height = 388;
                }

                //Set net association name

                //If many to many, then this is the mapname
                if (fromMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText()
                    && toMultiplicityComboBox.SelectedItem.ToString() == Multiplicity.ZeroMany.ToText())
                {
                    associationNameTextBox.Text = manyToManyTableTextBox.Text;
                }
                else
                {
                    //determine which side has the fkey.
                    if (fromFKeyCheckBox.Checked)
                    {
                        associationNameTextBox.Text = string.Format("FK_{0}_{1}_{2}_{3}", fromEntityComboBox.SelectedItem,
                            fromNavigationPropertyNameTextBox.Text, toEntityComboBox.SelectedItem, toNavigationPropertyNameTextBox.Text);
                    }
                    else
                    {
                        associationNameTextBox.Text = string.Format("FK_{0}_{1}_{2}_{3}", toEntityComboBox.SelectedItem,
                            toNavigationPropertyNameTextBox.Text, fromEntityComboBox.SelectedItem, fromNavigationPropertyNameTextBox.Text);
                    }
                }
            }
            else
            {
                SetControlValidity(false);

                //hide the many to many region
                this.Height = 388;
                manyToManyGroupBox.Visible = false;
                this.manyToManyGroupBox.Enabled = false;
            }
        }

        private bool IsValid()
        {
            return fromEntityComboBox.Text != "Select Entity" && toEntityComboBox.Text != "Select Entity"
                && !string.IsNullOrEmpty(fromMultiplicityComboBox.Text) && !string.IsNullOrEmpty(toMultiplicityComboBox.Text);
        }

        private void SetControlValidity(bool validity)
        {
            fromFKeyCheckBox.Enabled = toFKeyCheckBox.Enabled = validity;
            fromFKeyPropertyTextBox.Enabled = toFKeyPropertyTextBox.Enabled = validity;
            fromNavigationPropertyNameTextBox.Enabled = validity;
            toNavigationPropertyNameTextBox.Enabled = validity;
            associationNameTextBox.Enabled = validity;
            associationLabel.Enabled = validity;
            fromNavLabel.Enabled = validity;
            toNavLabel.Enabled = validity;
        }
    }
}
