using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Reflection;
using Microsoft.VisualStudio.Modeling.Diagrams.GraphObject;
using System.Collections;

namespace AgileFx.AgileModeler.CustomCode
{
    public class DiagramUtil
    {
        public DiagramClientView GetDiagramClientView(Diagram diagram)
        {
            if (diagram.ActiveDiagramView != null)
            {
                return diagram.ActiveDiagramView.DiagramClientView;
            }
            if (diagram.ClientViews.Count > 0)
            {
                return (diagram.ClientViews[0] as DiagramClientView);
            }
            return null;
        }

        public static void AutoLayout(IList shapes, Diagram diagram)
        {
            var store = diagram.Store;
            List<ShapeElement> shapeElementCollection = new List<ShapeElement>();
            List<ShapeElement> elements = new List<ShapeElement>();
            List<ShapeElement> list3 = new List<ShapeElement>();
            foreach (ShapeElement element in shapes)
            {
                var condition1 = (element is ClassShape) && ((element as ClassShape).ModelElement != null);

                bool condition2 = false;
                if (element is ClassShape)
                {
                    var modelClass = (element as ClassShape).ModelElement as ModelClass;
                    condition2 = modelClass.Baseclass != null || shapes.OfType<ClassShape>()
                        .Any(cs => (cs.ModelElement as ModelClass).Baseclass == modelClass);
                }
                if (condition1 && condition2)
                {
                    elements.Add(element);
                }
                else
                {
                    if (element is InheritanceConnector)
                    {
                        shapeElementCollection.Add(element);
                        continue;
                    }
                    list3.Add(element);
                }
            }
            using (Transaction transaction = store.TransactionManager.BeginTransaction("AutoLayout"))
            {
                using (new SaveLayoutFlags(elements, VGNodeFixedStates.PermeablePlace | VGNodeFixedStates.FixedPlace))
                {
                    diagram.AutoLayoutShapeElements(shapes, VGRoutingStyle.VGRouteNetwork, PlacementValueStyle.VGPlaceWE, false);
                }
                using (new SaveLayoutFlags(list3, VGNodeFixedStates.FixedPlace))
                {
                    diagram.AutoLayoutShapeElements(shapes, VGRoutingStyle.VGRouteOrgChartNS, PlacementValueStyle.VGPlaceSN, false);
                }
                using (new SaveLayoutFlags(shapes, VGNodeFixedStates.FixedPlace))
                {
                    diagram.AutoLayoutShapeElements(shapeElementCollection, VGRoutingStyle.VGRouteRightAngle, PlacementValueStyle.VGPlaceUndirected, false);
                }
                RunHandleLineRouting(diagram);
                transaction.Commit();
            }
        }

        private static void RunHandleLineRouting(Diagram diagram)
        {
            try
            {
                diagram.Reroute();
                MethodInfo method = typeof(Diagram).GetMethod("HandleLineRouting", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (method != null)
                {
                    method.Invoke(diagram, null);
                }
            }
            catch (Exception)
            {
            }
        }

        private sealed class SaveLayoutFlags : IDisposable
        {
            private IList Elements;
            private VGNodeFixedStates[] SavedFlags;

            public SaveLayoutFlags(IList elements)
            {
                this.Elements = elements;
                this.Save();
            }

            public SaveLayoutFlags(IList elements, VGNodeFixedStates flags)
                : this(elements)
            {
                this.SetFlags(flags);
            }

            public void Dispose()
            {
                this.Restore();
            }

            private void Restore()
            {
                for (int i = 0; i < this.Elements.Count; i++)
                {
                    NodeShape shape = this.Elements[i] as NodeShape;
                    if (shape != null)
                    {
                        shape.LayoutObjectFixedFlags = this.SavedFlags[i];
                    }
                }
            }

            private void Save()
            {
                this.SavedFlags = new VGNodeFixedStates[this.Elements.Count];
                for (int i = 0; i < this.Elements.Count; i++)
                {
                    NodeShape shape = this.Elements[i] as NodeShape;
                    if (shape != null)
                    {
                        this.SavedFlags[i] = shape.LayoutObjectFixedFlags;
                    }
                }
            }

            public void SetFlags(VGNodeFixedStates flags)
            {
                for (int i = 0; i < this.Elements.Count; i++)
                {
                    NodeShape shape = this.Elements[i] as NodeShape;
                    if (shape != null)
                    {
                        shape.LayoutObjectFixedFlags = flags;
                    }
                }
            }
        }
    }
}
