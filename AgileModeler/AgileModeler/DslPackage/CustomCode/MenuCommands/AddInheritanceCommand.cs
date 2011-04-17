using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using System.Windows.Forms;
using AgileFx.AgileModeler.DslPackage.CustomCode.DomainUtils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    class AddInheritanceCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x812;

        public override void StatusHandler(CommandSetState state)
        {
            foreach (object selectedObject in state.CurrentSelection)
            {
                if (selectedObject is ClassShape)
                {
                    MenuCommand.Visible = true;
                    var store = state.CurrentDocView.CurrentDiagram.Store;
                    MenuCommand.Enabled = (store.ElementDirectory.FindElements<ModelClass>().Count > 1);
                    return;
                }
                else
                {
                    MenuCommand.Visible = false;
                    MenuCommand.Enabled = false;
                }
            }
        }

        public override void InvokeHandler(CommandSetState state)
        {
            var store = state.CurrentDocView.CurrentDiagram.Store;

            var addInheritanceForm = new AddInheritanceForm(store);
            if (addInheritanceForm.ShowDialog() == DialogResult.OK)
            {
                InheritanceUtil.AddInheritance(store, addInheritanceForm.baseClassComboBox.SelectedItem as string, addInheritanceForm.derivedClassComboBox.SelectedItem as string);
            }
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }

    }
}
