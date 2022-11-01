using FrameMVC.Interfaces;
using System;
using System.Collections.Concurrent;

namespace FrameMVC.Core
{
    public class Model : IModel
    {
        /// <summary>
        /// 多例模式下的关键字标记
        /// </summary>
        protected readonly string multitonKey;
        /// <summary>
        /// 代理实例化集合
        /// </summary>
        protected readonly ConcurrentDictionary<string, IProxy> proxyMap;
        /// <summary>
        /// 数据模型集合
        /// </summary>
        protected static readonly ConcurrentDictionary<string, Lazy<IModel>> InstanceMap = new ConcurrentDictionary<string, Lazy<IModel>>();

        public Model(string multitlonKey)
        {
            multitonKey = multitlonKey;
            InstanceMap.TryAdd(multitlonKey, new Lazy<IModel>(() => this));
            proxyMap = new ConcurrentDictionary<string, IProxy>();
            InitializeModel();
        }
        /// <summary>
        /// model初始化
        /// </summary>
        protected virtual void InitializeModel()
        {

        }
        public virtual bool HasProxy(string proxyName)
        {
            return proxyMap.ContainsKey(proxyName);
        }

        public virtual void RegisterProxy(IProxy proxy)
        {
            proxy.InitializeNotifier(multitonKey);
            proxyMap[proxy.ProxyName] = proxy;
            proxy.OnRegister();
        }

        public virtual IProxy RemoveProxy(string proxyName)
        {
            if (proxyMap.TryRemove(proxyName, out var proxy))
            {
                proxy.OnRemove();
            }
            return proxy;
        }

        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return proxyMap.TryGetValue(proxyName, out var proxy) ? proxy : null;
        }
        /// <summary>
        /// Model添加入口
        /// </summary>
        /// <param name="key"></param>
        /// <param name="createrFun"></param>
        /// <returns></returns>
        public static IModel GetInstance(string key, Func<string, IModel> createrFun)
        {
            return InstanceMap.GetOrAdd(key, new Lazy<IModel>(() => createrFun(key))).Value;
        }
        /// <summary>
        /// 移除模板key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveModel(string key)
        {
            return InstanceMap.TryRemove(key, out _);
        }
    }
}
