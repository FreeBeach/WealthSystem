using FrameMVC.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameMVC.Interfaces
{
    public interface IView
    {
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, object notifyContext);
        void NotifyObservers(INotification notification);
        void RegisterMediator(IMediator meditor);
        IMediator RetrieveMediator(string mediatorName);
        IMediator RemoveMediator(string mediatorName);
        bool HasMediator(string mediatorName);
    }
}
