using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Shell;
using System.ComponentModel.Design;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    public interface IDSLMenuCommandImpl
    {
        void StatusHandler(CommandSetState state);
        void InvokeHandler(CommandSetState state);
        System.ComponentModel.Design.CommandID GetCommandID();
    }

    public abstract class DSLMenuCommandImplBase : IDSLMenuCommandImpl
    {
        public DynamicStatusMenuCommand MenuCommand { get; set; }

        public abstract void StatusHandler(CommandSetState state);
        public abstract void InvokeHandler(CommandSetState state);
        public abstract CommandID GetCommandID();
    }

    public interface IDSLMenuCommand
    {
        IDSLMenuCommandImpl GetImplementation();
    }

    public class DSLMenuCommand<T> : DynamicStatusMenuCommand, IDSLMenuCommand
        where T : DSLMenuCommandImplBase, new()
    {
        T impl;

        private DSLMenuCommand(EventHandler statusHandler, EventHandler invokeHandler, T t)
            : base(statusHandler, invokeHandler, t.GetCommandID())
        {
            impl = t;
            impl.MenuCommand = this;
        }

        public DSLMenuCommand(EventHandler statusHandler, EventHandler invokeHandler) : this(statusHandler, invokeHandler, new T())
        { }

        public IDSLMenuCommandImpl GetImplementation()
        {
            return impl;
        }
    }
}
