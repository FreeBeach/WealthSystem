namespace FrameMVC.Interfaces
{
    public interface IProxy : INotifier
    {
        string ProxyName { get; }
        object Data { get; set; }
        /// <summary>
        /// 代理注册后，调用这个
        /// </summary>
        void OnRegister();
        /// <summary>
        /// 代理取消后，调用这个
        /// </summary>
        void OnRemove();
    }
}

