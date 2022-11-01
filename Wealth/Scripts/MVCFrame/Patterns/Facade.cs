using FrameMVC.Core;
using FrameMVC.Interfaces;
using System;
using System.Collections.Concurrent;

namespace FrameMVC.Patterns
{
    public class Facade : IFacade
    {
        protected IController controller;
        protected IModel model;
        protected IView view;
        /// <summary>
        /// 通知域内实例化标记Key
        /// </summary>
        protected string multitonKey;

        protected static readonly ConcurrentDictionary<string, Lazy<IFacade>> InstanceMap = new ConcurrentDictionary<string, Lazy<IFacade>>();


        public Facade(string key)
        {
            InitializeNotifier(key);
            InstanceMap.TryAdd(key, new Lazy<IFacade>(() => this));
            InitializeFacade();
        }
        /// <summary>
        /// 初始化外观模式下，各个子系统
        /// </summary>
        protected virtual void InitializeFacade()
        {
            InitializeModel();
            InitializeView();
            InitializeController();
        }
        #region Notifier
        public virtual void InitializeNotifier(string key)
        {
            multitonKey = key;
        }

        public virtual void SendNotification(string notificationkey, object body = null, string bodyType = null)
        {
            NotifyObservers(new Notification(notificationkey, body, bodyType));
        }
        public virtual void NotifyObservers(INotification notification)
        {
            view.NotifyObservers(notification);
        }
        #endregion

        #region Model
        protected virtual void InitializeModel()
        {
            model = Model.GetInstance(multitonKey, key => new Model(key));
        }
        public virtual void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy(proxy);
        }
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy(proxyName);
        }
        public virtual IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy(proxyName);
        }
        public virtual bool HasProxy(string proxyName)
        {
            return model.HasProxy(proxyName);
        }
        #endregion

        #region View
        protected virtual void InitializeView()
        {
            view = View.GetInstance(multitonKey, key => new View(key));
        }
        public virtual void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator(mediator);
        }
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator(mediatorName);
        }
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator(mediatorName);
        }
        public virtual bool HasMediator(string mediatorName)
        {
            return view.HasMediator(mediatorName);
        }
        #endregion

        #region Controller
        protected virtual void InitializeController()
        {
            controller = Controller.GetInstance(multitonKey, key => new Controller(key));
        }
        public virtual void RegisterCommand(string notificationName, Func<ICommand> commandFunc)
        {
            controller.RegisterCommand(notificationName, commandFunc);
        }
        public virtual void RemoveCommand(string notificationName)
        {
            controller.RemoveCommand(notificationName);
        }
        public virtual bool HasCommand(string notificationName)
        {
            return controller.HasCommand(notificationName);
        }
        #endregion

        public static bool HasCore(string key)
        {
            return InstanceMap.TryGetValue(key, out _);
        }
        public static void RemoveCore(string key)
        {
            if (!InstanceMap.TryGetValue(key, out _)) return;
            Model.RemoveModel(key);
            View.RemoveView(key);
            Controller.RemoveController(key);
            InstanceMap.TryRemove(key, out _);
        }
        public static IFacade GetInstance(string key, Func<string, IFacade> factory)
        {
            return InstanceMap.GetOrAdd(key, new Lazy<IFacade>(() => factory(key))).Value;
        }


    }
}


