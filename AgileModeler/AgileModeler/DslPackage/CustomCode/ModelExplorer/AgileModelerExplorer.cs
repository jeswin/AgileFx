using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections.ObjectModel;

namespace AgileFx.AgileModeler.DslPackage
{
    partial class AgileModelerExplorer
    {
        public override RoleGroupTreeNode CreateRoleGroupTreeNode(DomainRoleInfo targetRoleInfo)
        {
            var roleGroupNode = base.CreateRoleGroupTreeNode(targetRoleInfo);
            roleGroupNode.ImageIndex = 1;
            return roleGroupNode;
        }

        public override RoleTreeNode CreateRoleTreeNode(DomainRoleInfo targetRoleInfo)
        {
            var roleNode = base.CreateRoleTreeNode(targetRoleInfo);
            roleNode.ImageIndex = -1;
            return roleNode;
        }

        public override ModelElementTreeNode CreateModelElementTreeNode(ModelElement modelElement)
        {
            var modelElementNode = base.CreateModelElementTreeNode(modelElement);
            modelElementNode.ShowDomainClass = false;

            if (modelElement is ModelRoot) modelElementNode.ImageIndex = 0;
            else if (modelElement is ModelRootHasTypes) modelElementNode.ImageIndex = 1;
            else if (modelElement is ModelClass && modelElement.Store.ElementDirectory.FindElements<ModelClass>().Any(c => c.Id == modelElement.Id)) modelElementNode.ImageIndex = 2;
            else if (modelElement is ModelField) modelElementNode.ImageIndex = (modelElement as ModelField).IsPrimaryKey ? 3 : 4;
            else if (modelElement is NavigationProperty) modelElementNode.ImageIndex = 5;
            else modelElementNode.ImageIndex = -1;
            modelElementNode.SelectedImageIndex = modelElementNode.ImageIndex;

            return modelElementNode;
        }

        /// <summary>
        /// Executed when the model explorer creates the tree element visitor.
        /// </summary>
        /// <returns>The tree element visitor.</returns>
        protected override IElementVisitor CreateElementVisitor()
        {
            // Subscribe double click in the tree view
            this.ObjectModelBrowser.DoubleClick += new EventHandler(this.ObjectModelBrowser_DoubleClick);

            // Default behavior

            //return base.CreateElementVisitor();
            return new AgileModelerExplorerElementVisitor(this);
        }

        #region Private DoubleClick Implementation
        private void ObjectModelBrowser_DoubleClick(object sender, EventArgs e)
        {
            // Get the node selected in the model explorer tree

            ModelElementTreeNode node = (this.ObjectModelBrowser.SelectedNode as ModelElementTreeNode);
            if (node != null)
            {
                // Get the corresponding model element

                ModelElement element = node.ModelElement;
                if (element != null)
                {
                    // Get the corresponding shape
                    // If the model element is in a compartment the result will be null

                    ShapeElement shape = GetModelElementFirstShape(element);
                    if (shape == null)
                    {
                        // If the element is in a compartment, try to get the parent model element to select that

                        ModelElement parentElement = GetCompartmentElementFirstParent(element);
                        if (parentElement != null)
                        {
                            // Get the corresponding shape

                            shape = GetModelElementFirstShape(parentElement);
                        }
                    }

                    // Select the shape

                    if (shape != null)
                    {
                        SelectShape(shape, this.ModelingDocData);
                    }
                }
            }
        }
        /// <summary>
        /// Selects the specified shape using the given modeling document data.
        /// </summary>
        /// <param name="shapeElement">The shape element that should be selected.</param>
        /// <param name="docData">The modeling document data.</param>
        private static void SelectShape(ShapeElement shapeElement, DocData docData)
        {
            // Validation

            if (shapeElement == null)
            {
                throw new ArgumentNullException("shapeElement");
            }

            if (docData == null)
            {
                throw new ArgumentNullException("docData");
            }

            // Select the shape

            ModelingDocView docView = docData.DocViews[0];
            if (docView != null)
            {
                docView.SelectObjects(1, new object[] { shapeElement }, 0);
            }
        }

        /// <summary>
        /// Gets the first shape the represents the specified model element.
        /// </summary>
        /// <param name="modelElement">The model element whose shape will be returned.</param>
        /// <returns>The first shape the represents the specified model element.</returns>
        private static ShapeElement GetModelElementFirstShape(ModelElement modelElement)
        {
            // Presentation elements

            LinkedElementCollection<PresentationElement> presentations = PresentationViewsSubject.GetPresentation(modelElement);
            foreach (ModelElement element in presentations)
            {
                ShapeElement shapeElement = (element as ShapeElement);
                if (shapeElement != null)
                {
                    return shapeElement;
                }
            }

            // Default result

            return null;
        }

        /// <summary>
        /// Return the model element which is the parent of the specified model element considering that
        /// this element is placed in a compartment.
        /// </summary>
        /// <param name="modelElement">The model element whose parent will be returned.</param>
        /// <returns>
        /// The model element which is the parent of the specified model element considering that
        /// this element is placed in a compartment.
        /// </returns>
        private static ModelElement GetCompartmentElementFirstParent(ModelElement modelElement)
        {
            // Get the domain class associated with model element.

            DomainClassInfo domainClass = modelElement.GetDomainClass();
            if (domainClass != null)
            {
                // A element is only considered to be in a compartment if it participates in only 1 embedding relationship
                // This might be wrong for some models

                if (domainClass.AllEmbeddedByDomainRoles.Count == 1)
                {
                    // Get a collection of all the links to this model element
                    // Since this is in a compartment there will only be one

                    ReadOnlyCollection<ElementLink> links = DomainRoleInfo.GetAllElementLinks(modelElement);
                    if (links.Count == 1)
                    {
                        // Get the model element participating in the link that isn't the current one
                        // That will be the parent
                        // Probably there is a better way to achieve the same result

                        foreach (ModelElement linkedElement in links[0].LinkedElements)
                        {
                            if (!modelElement.Equals(linkedElement))
                            {
                                return linkedElement;
                            }
                        }
                    }
                }
            }

            // Default result

            return null;
        }
        #endregion
    }
}
