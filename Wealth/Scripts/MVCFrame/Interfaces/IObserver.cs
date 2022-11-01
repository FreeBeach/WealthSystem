using System;

namespace FrameMVC.Interfaces
{
    public interface IObserver
    {
        object NotifyContent { get; }
        Action<INotification> NotifyMethod { set; }
        void NotifyObserver(INotification notification);
        bool CompareNotifyContext(object obj);

    }
}
