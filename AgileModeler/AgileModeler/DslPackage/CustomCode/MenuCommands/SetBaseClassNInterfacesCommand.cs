using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.CustomCode;
using System.Windows.Forms;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    public class SetBaseClassNInterfacesCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x813;

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
            var dlg = new SetBaseClassAndInterfacesForm();
            var store = state.CurrentDocView.CurrentDiagram.Store;
            dlg.Initialize(store.ElementDirectory.FindElements<ModelClass>());
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var origCursor = Cursor.Current;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    using (Transaction tx = store.TransactionManager.BeginTransaction())
                    {
                        dlg.SelectedClasses.ForEach(c => c.DerivesOrImplements = dlg.InheritsOrImplements);

                        if (tx.HasPendingChanges) tx.Commit();
                    }
                }
                finally
                {
                    Cursor.Current = origCursor;
                }
            }
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
