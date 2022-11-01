using FrameMVC.Interfaces;
using System;

namespace FrameMVC.Patterns
{
    public class Mediator : Notifier, IMediator
    {
        public const string NAME = "Meditor";
        public string MediatorName { get; protected set; }
        public object ViewComponent { get; set; }

        public virtual string[] ListNotificationInterests()
        {
            return new string[0];
        }
        public virtual void HandleNotification(INotification notification)
        {

        }
        public virtual void OnRegister()
        {

        }
        public virtual void OnRemove()
        {

        }
    }
}
