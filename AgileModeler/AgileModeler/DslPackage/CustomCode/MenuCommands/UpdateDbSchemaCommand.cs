using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Configuration;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.CustomCode;
using AgileFx.AgileModeler.DslPackage.Utils;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    public class UpdateDbSchemaCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x820;

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
            ModelerTransaction.Enter(() =>
                {
                    var diagram = state.CurrentDocView.CurrentDiagram;
                    var sync = new Utilities.DbSchemaImporter(diagram);
                    sync.ImportModels();
                });
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
