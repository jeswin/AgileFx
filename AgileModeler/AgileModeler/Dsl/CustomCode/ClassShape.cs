using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;
using Microsoft.VisualStudio.Modeling;

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace AgileFx.AgileModeler
{
    public partial class ClassShape : ClassShapeBase
    {

        protected override DslDiagrams::CompartmentMapping[] GetCompartmentMappings(global::System.Type melType)
        {
            if (melType == null) throw new global::System.ArgumentNullException("melType");

            CompartmentMapping[] mappings = base.GetCompartmentMappings(melType);

            foreach (CompartmentMapping mapping in mappings)
            {
                ElementListCompartmentMapping elemMap = mapping as ElementListCompartmentMapping;

                if (elemMap != null)
                {
                    if (elemMap.CompartmentId == "PropertiesCompartment")
                    { elemMap.ImageGetter = e => GetCompartmentItemIcon(e, "PropertiesCompartment"); }
                    if (elemMap.CompartmentId == "NavigationPropertiesCompartment")
                    { elemMap.ImageGetter = e => GetCompartmentItemIcon(e, "NavigationPropertiesCompartment"); }
                }
            }

            return mappings;
        }

        //This will deliver custom images to operations
        public Image GetCompartmentItemIcon(ModelElement element, string type)
        {
            string icon = "";

            if (type == "PropertiesCompartment")
                if (((ModelField)element).IsPrimaryKey)
                    icon = "AgileFx.AgileModeler.Resources.KeyPropertyIcon.bmp";
                else
                    icon = "AgileFx.AgileModeler.Resources.PropertyIcon.bmp";
            else if (type == "NavigationPropertiesCompartment")
                icon = "AgileFx.AgileModeler.Resources.NavigationPropertyIcon.bmp";

            Assembly a = Assembly.GetExecutingAssembly();
            Stream imgStream = a.GetManifestResourceStream(icon);
            return Image.FromStream(imgStream);
        }
    }
}
