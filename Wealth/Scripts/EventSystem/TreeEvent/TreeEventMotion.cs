

using System;

namespace UsefulTool
{
    /// <summary>
    /// 当前节点可进行的操作，对象为自己
    /// 包含事件key,可为空
    /// </summary>
    public class TreeEventMotion
    {
        string motionKey = string.Empty;//空表示匹配所有事件，否则表示单个事件
        public TreeEventMotion(string motionKey)
        {
            this.motionKey = motionKey;
        }
        /// <summary>
        /// 检测键值是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameKey(string other)
        {
            return motionKey == other;
        }
        
    }
}
