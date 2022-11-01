
using System.Collections.Generic;

namespace BuffRegion
{
    /// <summary>
    /// 规则：Methods执行流程无序，根据配置完成若干个Methods即可，代表方法执行完毕
    /// 需要自己定义设置条件完成程度，达到条件后，需要设置:EventIsEnd=true
    /// 执行过程中，方法不会删除,
    /// 注意条件是否一定能达成
    /// </summary>
    public class BuffEventBaseCondition : BuffFunBase
    {
        protected List<BuffMethodBase> methodList;


        BuffMethodBase usedMethod;
        int methodNum;
        /// <summary>
        /// EventIsEnd==false阶段，所有条件已经执行了一次循环后，调用此函数一次
        /// </summary>
        public virtual void UpdateAllConditionsOnce()
        {

        }
        /// <summary>
        /// 结构运行,自行修改终止条件EventIsEnd=true
        /// </summary>
        public sealed override void FunRun()
        {
            if (FunIsEnd) return;
            if (methodList == null || methodList.Count == 0)
            {
                FunIsEnd = true;
                return;
            }
            methodNum = methodList.Count;
            for (int i = methodNum-1; i >-1; i--)
            {
                if (FunIsEnd) break;
                methodNum--;
                usedMethod = methodList[i];
                if (usedMethod == null || usedMethod.MethodIsEnd()) continue;
                usedMethod.MethodRun();
            }
            if(methodNum==0)
                UpdateAllConditionsOnce();
        }

        public override void FunReset()
        {
            if(methodList!=null)
            {
                foreach (var method in methodList)
                {
                    if (method == null) continue;
                    method.MethodReset();
                }
                methodList = null;
            }
            FunIsEnd = false;
        }
    }
}
