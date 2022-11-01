
using FrameMVC.Interfaces;
using FrameMVC.Patterns;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FrameMVC.Core
{
    public class View : IView
    {
        protected readonly string multitionKey;
        protected readonly ConcurrentDictionary<string, IMediator> mediatorMap;
        protected readonly ConcurrentDictionary<string, IList<IObserver>> observerMap;
        protected static readonly ConcurrentDictionary<string, Lazy<IView>> InstanceMap = new ConcurrentDictionary<string, Lazy<IView>>();
        public View(string key)
        {
            multitionKey = key;
            InstanceMap.TryAdd(key, new Lazy<IView>(() => this));
            mediatorMap = new ConcurrentDictionary<string, IMediator>();
            observerMap = new ConcurrentDictionary<string, IList<IObserver>>();
            InitializeView();
        }
        protected virtual void InitializeView()
        {

        }
        public static IView GetInstance(string key, Func<string, IView> factory)
        {
            return InstanceMap.GetOrAdd(key, new Lazy<IView>(() => factory(key))).Value;
        }
        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            if (observerMap.TryGetValue(notificationName, out var observers))
            {
                observers.Add(observer);
            }
            else
            {
                observerMap.TryAdd(notificationName, new List<IObserver> { observer });
            }
        }
        public virtual void NotifyObservers(INotification notification)
        {
            if (observerMap.TryGetValue(notification.Name, out var observersRef))
            {
                var observers = new List<IObserver>(observersRef);
                foreach (var observer in observers)
                {
                    observer.NotifyObserver(notification);
                }
            }
        }
        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            if (observerMap.TryGetValue(notificationName, out var observers))
            {
                for (var i = 0; i < observers.Count; i++)
                {
                    if (observers[i].CompareNotifyContext(notifyContext))
                    {
                        observers.RemoveAt(i);
                        break;
                    }
                }
            }
            if (observers.Count == 0)
                observerMap.TryRemove(notificationName, out _);
        }
        public virtual void RegisterMediator(IMediator mediator)
        {
            if (mediatorMap.TryAdd(mediator.MediatorName, mediator))
            {
                mediator.InitializeNotifier(multitionKey);
                var interests = mediator.ListNotificationInterests();
                if (interests.Length > 0)
                {
                    IObserver observer = new Observer(mediator.HandleNotification, mediator);
                    foreach (var interest in interests)
                    {
                        RegisterObserver(interest, observer);
                    }
                }
                mediator.OnRegister();
            }
        }
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return mediatorMap.TryGetValue(mediatorName, out var mediator) ? mediator : null;
        }
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            if (mediatorMap.TryRemove(mediatorName, out var mediator))
            {
                var interests = mediator.ListNotificationInterests();
                foreach (var interest in interests)
                {
                    RemoveObserver(interest, mediator);
                }
                mediator.OnRemove();
            }
            return mediator;
        }
        public virtual bool HasMediator(string mediatorName)
        {
            return mediatorMap.ContainsKey(mediatorName);
        }
        public static void RemoveView(string key)
        {
            InstanceMap.TryRemove(key, out _);
        }
    }

}
