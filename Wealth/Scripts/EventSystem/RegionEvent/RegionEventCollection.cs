using System;
namespace RegionEvent
{
    public class AppandRunMethods
    {
        public bool isMethodActive = true;
        public virtual bool IsSameAction<T2>(T2 param2) where T2 : Delegate { return false; }
    }

    public class RegionEventFun<T> : AppandRunMethods where T : Delegate
    {
        public T realFun;
        public RegionEventFun(T realFun_)
        {
            realFun = realFun_;
        }
        public override bool IsSameAction<T2>(T2 param2)
        {
            return param2 != null && param2 == realFun;
        }
    }
}
