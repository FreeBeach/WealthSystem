
namespace Lucifer.SingleTool
{
    /// <summary>
    /// 单例类接口
    /// </summary>
    public class SingleInstance<T> where T : new()
    {
        private static T Instance_;
        public static T Instance
        {
            get
            {
                if (Instance_ == null)
                    Instance_ = new T();
                return Instance_;
            }
        }
        //public SingleInstance() { }
    }
}
