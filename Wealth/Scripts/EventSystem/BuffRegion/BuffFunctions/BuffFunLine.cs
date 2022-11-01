using System.Collections.Generic;

namespace BuffRegion
{
    /// <summary>
    /// 规则，多个method组成链，执行顺序从前向后
    /// 一个执行完成，才能执行下一个
    /// 所有方法执行完成，才能算结束
    /// </summary>
    public class BuffEventBaseLine: BuffFunBase
    {
        protected Queue<BuffMethodBase> methodQueue;
        BuffMethodBase usedMethod;
        int methodNum;

        /// <summary>
        /// 结构运行
        /// </summary>
        public sealed override void FunRun()
        {
            if (FunIsEnd) return;
            if (methodQueue == null || methodQueue.Count == 0)
            {
                FunIsEnd = true;
                return;
            }
            methodNum = methodQueue.Count;
            while (methodNum > 0)
            {
                if (FunIsEnd) break;
                methodNum--;
                usedMethod = methodQueue.Peek();
                if (usedMethod == null)
                {
                    methodQueue.Dequeue();
                    FunIsEnd = methodNum == 0;
                    continue;
                }
                usedMethod.MethodRun();
                if (!usedMethod.MethodIsEnd()) break;
                methodQueue.Dequeue();
                usedMethod.MethodReset();
                FunIsEnd = methodNum == 0;
            }
        }
        public override void FunReset()
        {
            if (methodQueue != null)
            {
                foreach (var method in methodQueue)
                {
                    if (method != null) method.MethodReset();
                }
                methodQueue = null;
            }
            FunIsEnd = false;
        }
    }
}


