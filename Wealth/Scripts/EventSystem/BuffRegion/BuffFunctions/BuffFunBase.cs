
namespace BuffRegion
{
    public abstract class BuffFunBase
    {
        /// <summary>
        /// Buff结束标志（true表示技能完成）
        /// </summary>
        public bool FunIsEnd = false;
        /// <summary>
        /// 单个参数的，看需要可直接做等值处理
        /// 多个同类型参数，可考虑数组
        /// 处理后的参数输出,使用Action<Delegalte>输入多个参数
        /// 尽量减少装箱拆箱操作
        /// </summary>
        public object BuffInParameters;

        /// <summary>
        /// Buff参数的输入
        /// 操作BuffInParameters在本地扩撒
        /// </summary>
        public virtual void FunInitParams()
        {

        }
        /// <summary>
        /// 填充方法流程,设置方法流程的参数
        /// </summary>
        public virtual void FunInitMethods()
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract void FunReset();
        /// <summary>
        /// 结构运行
        /// </summary>
        public abstract void FunRun();
    }
}


