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
    public partial class SetBaseClassAndInterfacesForm : Form
    {
        public SetBaseClassAndInterfacesForm()
        {
            InitializeComponent();
        }

        List<ModelClass> classesInStore = null;
        public void Initialize(IEnumerable<ModelClass> classes)
        {
            classesInStore = new List<ModelClass>(classes);
            var classesNode = dbTree.Nodes.Add("classRoot", "Classes");
            LoadClasses(false);
        }

        void LoadClasses(bool hideDerived)
        {
            var classesNode = dbTree.Nodes.Find("classRoot", false)[0];
            classesNode.Nodes.Clear();
            foreach (ModelClass c in classesInStore)
            {
                if (hideDerived && c.Baseclass != null) continue;
                var cNode = classesNode.Nodes.Add(c.Name);
                cNode.Tag = c;
            }
            classesNode.Expand();
        }

        private void dbTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null && e.Node.Name == "classRoot")
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                    childNode.Checked = e.Node.Checked;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.InheritsOrImplements))
            {
                MessageBox.Show("Please enter the details.");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public string InheritsOrImplements 
        { 
            get 
            {
                return (baseClassTextBox.Text + ", " + interfacesTextBox.Text).Trim(',', ' '); 
            }
        }

        public List<ModelClass> SelectedClasses
        {
            get
            {
                var selClasses = new List<ModelClass>();
                foreach (TreeNode node in dbTree.Nodes.Find("classRoot", false)[0].Nodes)
                {
                    if (node.Checked) selClasses.Add(node.Tag as ModelClass);
                }
                return selClasses;
            }
        }

        private void hideDerivedClassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadClasses(hideDerivedClassesCheckBox.Checked);
        }
    }
}
