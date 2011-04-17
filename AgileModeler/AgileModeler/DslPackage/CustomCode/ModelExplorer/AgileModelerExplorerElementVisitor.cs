using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Shell;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace AgileFx.AgileModeler.DslPackage
{
    public class AgileModelerExplorerElementVisitor : ExplorerElementVisitor
    {
        private System.Windows.Forms.TreeView treeView;
        public AgileModelerExplorerElementVisitor(ModelExplorerTreeContainer treeContainer) : base(treeContainer)
        {
            this.treeView = treeContainer.ObjectModelBrowser;
        }

        //TODO: FIXME - This method is a royal mess! 
        public override  bool Visit(ElementWalker walker, ModelElement modelElement)
        {
            var result = base.Visit(walker, modelElement);
            if (walker.InternalElementList.Last() == modelElement)
            {
                var typesNode = this.treeView.Nodes[0].Nodes[0];
                typesNode.Text = "Models";
                foreach (ModelElementTreeNode classNode in typesNode.Nodes)
                {
                    var originalNodes = classNode.Nodes.OfType<RoleGroupTreeNode>().ToArray();
                    foreach (RoleGroupTreeNode roleGroupNode in originalNodes)
                    {
                        var toRemove = new List<ModelElementTreeNode>();
                        foreach (ModelElementTreeNode propNode in roleGroupNode.Nodes)
                        {
                            toRemove.Add(propNode);
                        }

                        foreach (var node in toRemove)
                        {
                            roleGroupNode.Nodes.Remove(node);
                            this.TreeContainer.InsertTreeNode(classNode.Nodes, node);
                        }
                    }
                    foreach (TreeNode node in originalNodes) classNode.Nodes.Remove(node);
                }
                typesNode.Expand();
            }
            return result;
        }
    }
}
