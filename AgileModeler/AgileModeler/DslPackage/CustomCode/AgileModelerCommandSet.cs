using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.DslPackage.CustomCode;
using AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands;

namespace AgileFx.AgileModeler.DslPackage
{
    internal partial class AgileModelerCommandSet : AgileModelerCommandSetBase
    {
        protected override global::System.Collections.Generic.IList<global::System.ComponentModel.Design.MenuCommand> GetMenuCommands()
        {
            var commands = base.GetMenuCommands();

            commands.Add(new DSLMenuCommand<AddEntityCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<AddAssociationCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<AddInheritanceCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<UpdateDbSchemaCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<GenerateDbSchemaCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<BrowseRepositoryCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<Zoom25Command>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<Zoom50Command>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<Zoom100Command>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<Zoom150Command>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<Zoom200Command>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<ZoomToFitCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<LayoutDiagramCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<SetBaseClassNInterfacesCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            commands.Add(new DSLMenuCommand<ExportDiagramCommand>(new EventHandler(OnPopUpMenuDisplayAction), new EventHandler(OnCommandInvoke)));
            
            return commands;
        }

        CommandSetState GetDiagramState()
        {
            var state = new CommandSetState();
            state.CurrentSelection = this.CurrentSelection;
            state.CurrentDocView = this.CurrentDocView;
            return state;
        }

        internal void OnPopUpMenuDisplayAction(object sender, EventArgs e)
        {
            var command = sender as IDSLMenuCommand;
            var state = GetDiagramState();

            var impl = command.GetImplementation();
            impl.StatusHandler(state);
        }


        internal void OnCommandInvoke(object sender, EventArgs e)
        {
            var command = sender as IDSLMenuCommand;
            var state = GetDiagramState();

            var impl = command.GetImplementation();
            impl.InvokeHandler(state);
        }

    }
}