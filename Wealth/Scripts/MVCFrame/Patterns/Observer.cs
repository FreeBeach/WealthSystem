using FrameMVC.Interfaces;
using System;

namespace FrameMVC.Patterns
{
    public class Observer : IObserver
    {
        public Observer(Action<INotification> method, object notifyContext)
        {
            NotifyContent = notifyContext;
            NotifyMethod = method;
        }
        public object NotifyContent { get; set; }

        public Action<INotification> NotifyMethod { get; set; }

        public virtual bool CompareNotifyContext(object obj)
        {
            return NotifyContent.Equals(obj);
        }

        public virtual void NotifyObserver(INotification notification)
        {
            NotifyMethod(notification);
        }
    }
}
