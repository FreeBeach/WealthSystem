using FrameMVC.Interfaces;

namespace FrameMVC.Patterns
{
    public class CommandSimple : Notifier, ICommand
    {
        public virtual void Execute(INotification notification)
        {

        }
    }
}
