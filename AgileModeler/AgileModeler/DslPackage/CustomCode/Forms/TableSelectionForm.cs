using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.Forms
{
    public partial class TableSelectionForm : Form
    {
        public TableSelectionForm()
        {
            InitializeComponent();
        }

        public void Initialize(List<Table> tables, List<string> tablesInStore)
        {
            dbTree.AfterCheck -= new TreeViewEventHandler(dbTree_AfterCheck);

            var tablesNode = dbTree.Nodes.Add("tableRoot", "Tables");
            tables.ForEach(table =>
            {
                var tNode = tablesNode.Nodes.Add(table.Name);
                tNode.Checked = tablesInStore.Contains(table.Name);
                tNode.Tag = table;
            });
            tablesNode.Checked = tablesNode.Nodes.Cast<TreeNode>().All(tn => tn.Checked);
            tablesNode.Expand();

            dbTree.AfterCheck += new TreeViewEventHandler(dbTree_AfterCheck);
        }

        private void dbTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            dbTree.AfterCheck -= new TreeViewEventHandler(dbTree_AfterCheck);
            var tablesNode = dbTree.Nodes.Find("tableRoot", false)[0];
            if (e.Node == tablesNode)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                    childNode.Checked = e.Node.Checked;
            }
            else
            {
                tablesNode.Checked = tablesNode.Nodes.Cast<TreeNode>().All(tn => tn.Checked);
            }
            dbTree.AfterCheck += new TreeViewEventHandler(dbTree_AfterCheck);
        }

        public List<Table> SelectedTables
        {
            get
            {
                var selTables = new List<Table>();
                foreach (TreeNode node in dbTree.Nodes.Find("tableRoot", false)[0].Nodes)
                {
                    if (node.Checked) selTables.Add(node.Tag as Table);
                }
                return selTables;
            }
        }

        public bool AutoDetectInheritance
        {
            get { return detectInheritanceCheckBox.Checked; }
        }

        public string TablePrefix
        {
            get { return tablePrefixTextBox.Text; }
        }
    }
}
