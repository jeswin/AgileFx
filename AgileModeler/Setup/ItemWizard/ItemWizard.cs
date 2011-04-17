using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using EnvDTE;

namespace AgileFx.AgileModeler.ItemWizard
{
    public class AddDomainModelWizard : IWizard
    {
        // This method is called before opening any item that 
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
#if VS_2008
            projectItem.Properties.Item("CustomTool").Value = "AgileModelerCodeGenerator";
#endif
#if VS_2010
            projectItem.Properties.Item("CustomTool").Value = "AgileModelerCodeGeneratorVS2010";
#endif
            var defaultNS = projectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString();
            var namespaces = new List<string>();
            ProjectItem parent = projectItem.Collection.Parent as ProjectItem;
            while (parent != null)
            {
                if (parent.Kind != EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    namespaces.Insert(0, parent.Name.Replace(" ", string.Empty));
                }

                parent = parent.Collection.Parent as ProjectItem;
            }
            namespaces.Insert(0, defaultNS);
            projectItem.Properties.Item("CustomToolNamespace").Value = string.Join(".", namespaces.ToArray());
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
