using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Shell;
using System.Drawing;
using System.Reflection;

namespace AgileFx.AgileModeler.DslPackage
{
    partial class AgileModelerExplorerToolWindow
    {
        public override string WindowTitle
        {
            get
            {
                return "Model Browser";
            }
        }

        protected override ModelExplorerTreeContainer CreateTreeContainer()
        {
            var modelExplorer = new AgileModelerExplorer(this);
            var imageList = new System.Windows.Forms.ImageList();
            imageList.Images.AddRange(GetModelElemtIcons());
            modelExplorer.ObjectModelBrowser.ImageList = imageList;

            return modelExplorer;
        }

        private System.Drawing.Image[] GetModelElemtIcons()
        {
            Assembly a = typeof(AgileFx.AgileModeler.ModelClass).Assembly;
            Func<string, Image> getIconFromAssembly = icon => Image.FromStream(a.GetManifestResourceStream(icon), false);

            var images = new List<Image>();
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.ModelRoot.ico"));
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.Models.ico"));
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.ClassTool.bmp"));
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.KeyPropertyIcon.bmp"));
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.PropertyIcon.bmp"));
            images.Add(getIconFromAssembly("AgileFx.AgileModeler.Resources.NavigationPropertyIcon.bmp"));
            return images.ToArray();
        }
    }
}
