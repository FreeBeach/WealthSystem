
using FrameMVC.Interfaces;
using FrameMVC.Patterns;
using System;
using System.Collections.Concurrent;

namespace FrameMVC.Core
{
    public class Controller : IController
    {
        protected IView view;
        protected readonly string multipleKey;
        protected readonly ConcurrentDictionary<string, Func<ICommand>> commandMap;
        protected static readonly ConcurrentDictionary<string, Lazy<IController>> InstanceMap = new ConcurrentDictionary<string, Lazy<IController>>();
        public Controller(string key)
        {
            multipleKey = key;
            InstanceMap.TryAdd(key, new Lazy<IController>(() => this));
            commandMap = new ConcurrentDictionary<string, Func<ICommand>>();
            InitializeController();
        }
        protected virtual void InitializeController()
        {
            view = View.GetInstance(multipleKey, key => new View(key));
        }
        public virtual void RegisterCommand(string notificationName, Func<ICommand> factory)
        {
            if (commandMap.TryGetValue(notificationName, out _) == false)
            {
                view.RegisterObserver(notificationName, new Observer(ExecuteCommand, this));
            }
            commandMap[notificationName] = factory;
        }

        public virtual void ExecuteCommand(INotification notification)
        {
            if (commandMap.TryGetValue(notification.Name, out var factory))
            {
                var commandInstance = factory();
                commandInstance.InitializeNotifier(multipleKey);
                commandInstance.Execute(notification);
            }
        }

        public void RemoveCommand(string notificationName)
        {
            if (commandMap.TryRemove(notificationName, out _))
            {
                view.RemoveObserver(notificationName, this);
            }
        }

        public bool HasCommand(string notificationName)
        {
            return commandMap.ContainsKey(notificationName);
        }
        public static void RemoveController(string key)
        {
            InstanceMap.TryRemove(key, out _);
        }
        public static IController GetInstance(string key, Func<string, IController> factory)
        {
            return InstanceMap.GetOrAdd(key, new Lazy<IController>(() => factory(key))).Value;
        }
    }
}
