
namespace BuffRegion
{
    /// <summary>
    /// 规则，使用单个method
    /// method完成，event就完成
    /// </summary>
    public class BuffEventBaseSingle : BuffFunBase
    {
        protected BuffMethodBase method;
        public sealed override void FunRun()
        {
            if (FunIsEnd) return;
            if (method == null)
            {
                FunIsEnd = true;
                return;
            }
            method.MethodRun();
            if (!method.MethodIsEnd()) return;
            method.MethodReset();
            FunIsEnd = true;
            method = null;
        }
        public override void FunReset()
        {
            method=null;
            FunIsEnd = false;
        }
    }
}
