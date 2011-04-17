using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Configuration;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using System.Collections;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Diagrams.GraphObject;
using AgileFx.AgileModeler.CustomCode;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    abstract class ZoomCommandBase : DSLMenuCommandImplBase
    {
        protected Guid commandGuid;
        protected int commandID;
        protected float zoomFactor;

        public ZoomCommandBase(Guid commandGuid, int commandID, float zoomFactor)
        {
            this.commandGuid = commandGuid;
            this.commandID = commandID;
            this.zoomFactor = zoomFactor;
        }

        public override void StatusHandler(CommandSetState state)
        {
            MenuCommand.Visible = true;
            MenuCommand.Enabled = true;
            return;
        }

        public override void InvokeHandler(CommandSetState state)
        {
            new DiagramUtil().GetDiagramClientView(state.CurrentDocView.CurrentDiagram)
                .SetZoomFactor(zoomFactor, state.CurrentDocView.CurrentDiagram.Center, true);
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }

    class Zoom25Command : ZoomCommandBase
    {
        public Zoom25Command() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x850, 0.25f) { }
    }

    class Zoom50Command : ZoomCommandBase
    {
        public Zoom50Command() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x851, 0.5f) { }
    }

    class Zoom100Command : ZoomCommandBase
    {
        public Zoom100Command() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x852, 1f) { }
    }

    class Zoom150Command : ZoomCommandBase
    {
        public Zoom150Command() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x853, 1.5f) { }
    }

    class Zoom200Command : ZoomCommandBase
    {
        public Zoom200Command() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x854, 2f) { }
    }

    class ZoomToFitCommand : ZoomCommandBase
    {
        public ZoomToFitCommand() : base(new Guid("3c377eba-e82a-45af-9112-a339092d3d4a"), 0x855, 1f) { }

        public override void InvokeHandler(CommandSetState state)
        {
            new DiagramUtil().GetDiagramClientView(state.CurrentDocView.CurrentDiagram).ZoomToFit();
        }
    }
}
