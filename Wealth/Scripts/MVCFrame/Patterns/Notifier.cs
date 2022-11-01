using FrameMVC.Interfaces;

namespace FrameMVC.Patterns
{
    public class Notifier : INotifier
    {
        public string MultitonKey { get; protected set; }
        protected string Multiton_UnRegister_MsgView = "无法通知，没有注册用于通知的MultitonKey";
        public void InitializeNotifier(string key)
        {
            MultitonKey = key;
        }

        public virtual void SendNotification(string notificationkey, object body = null, string bodyType = null)
        {
            Facade.SendNotification(notificationkey, body, bodyType);
        }
        protected IFacade Facade
        {
            get
            {
                if (MultitonKey == null) throw new System.Exception(Multiton_UnRegister_MsgView);
                return Patterns.Facade.GetInstance(MultitonKey, key => new Facade(key));
            }
        }
    }
}

