namespace FrameMVC.Interfaces
{
    public interface INotifier
    {
        /// <summary>
        /// 通知初始化接口
        /// 没有初始化通知域内实例化标记Key值时，不能发送通知
        /// </summary>
        /// <param name="key">外观模式下通知域内实例化标记Key</param>
        void InitializeNotifier(string key);
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationkey">初始化标记值</param>
        /// <param name="body">通知内容</param>
        /// <param name="bodyType">通知内类型</param>
        void SendNotification(string notificationkey, object body = null, string bodyType = null);
    }
}
