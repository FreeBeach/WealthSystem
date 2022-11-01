using FrameMVC.Interfaces;
using System;
using System.Collections.Generic;

namespace FrameMVC.Patterns
{
    public class CommandMacro : Notifier, ICommand
    {
        public readonly IList<Func<ICommand>> subcommands;
        public CommandMacro()
        {
            subcommands = new List<Func<ICommand>>();
            InitializeCommandMacro();
        }
        protected virtual void InitializeCommandMacro()
        {

        }
        protected void AddSubCommand(Func<ICommand> factory)
        {
            subcommands.Add(factory);
        }
        public virtual void Execute(INotification notification)
        {
            while (subcommands.Count > 0)
            {
                var factory = subcommands[0];
                var commandInstance = factory();
                commandInstance.InitializeNotifier(MultitonKey);
                commandInstance.Execute(notification);
                subcommands.RemoveAt(0);
            }
        }

    }
}
