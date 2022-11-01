namespace FrameMVC.Interfaces
{
    public interface IModel
    {
        /// <summary>
        /// 注册代理
        /// </summary>
        /// <param name="proxy"></param>
        void RegisterProxy(IProxy proxy);
        /// <summary>
        /// 删除代理
        /// </summary>
        /// <param name="proxyNmae"></param>
        /// <returns></returns>
        IProxy RemoveProxy(string proxyNmae);
        /// <summary>
        /// 获取代理包
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        IProxy RetrieveProxy(string proxyName);
        /// <summary>
        /// 检查代理是否已经注册
        /// </summary>
        /// <param name="proxyName"></param>
        bool HasProxy(string proxyName);
    }
}
