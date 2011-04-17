using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using System.Windows.Forms;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    class BrowseRepositoryCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x840;

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
            MessageBox.Show("This feature will be available in the next version of AgileFx.");
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
