using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    class AddAssociationCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x811;

        public override void StatusHandler(CommandSetState state)
        {
            foreach (object selectedObject in state.CurrentSelection)
            {
                if (selectedObject is ClassDiagram)
                {
                    MenuCommand.Visible = MenuCommand.Enabled = true;
                    MenuCommand.Enabled = true;
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

            ModelerTransaction.Enter(() =>
            {
                using (Transaction tx = store.TransactionManager.BeginTransaction())
                {
                    var addAssociationForm = new AddAssociationForm(state.CurrentDocView.CurrentDiagram.Store);
                    if (state.CurrentSelection.Count == 1)
                    {
                        var selection = state.CurrentSelection.Cast<object>().First();
                        if (selection is ClassShape)
                        {
                            var modelClass = ((ClassShape)selection).ModelElement as ModelClass;
                            addAssociationForm.SetSelectedClasses(modelClass);
                        }
                    }
                    addAssociationForm.ShowDialog();

                    tx.Commit();
                }
            });

            /*
            MenuCommand command = sender as MenuCommand;

            StringBuilder sb = new StringBuilder();
            sb.Append("Command: " + Commands.Values.First(x => x.CommandGuid == command.CommandID.Guid).Type.ToString() + "\n");
            foreach (object selectedObject in this.CurrentSelection)
            {
                sb.AppendLine("Selected Shape: " + selectedObject.ToString());

                if (selectedObject is ClassShape)
                {
                    ModelClass modelClass = (ModelClass)(selectedObject as ClassShape).ModelElement;
                    sb.AppendLine("*** Related Domain Class: " + modelClass.ToString());
                }

                if (selectedObject is ClassDiagram)
                {
                }
            }


            */
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
