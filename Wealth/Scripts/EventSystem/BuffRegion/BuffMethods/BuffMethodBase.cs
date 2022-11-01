using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BuffRegion
{
    /// <summary>
    /// 方法行为
    /// </summary>
    public abstract class BuffMethodBase
    {
        public BuffMethodBase(int methodType)
        {
            MethodType = methodType;
        }
        /// <summary>
        /// 方法类型，方便做方法池
        /// </summary>
        public int MethodType { protected set; get; }
        /// <summary>
        /// 方法终止标记
        /// </summary>
        protected bool IsMethodEnd = false;
        /// <summary>
        /// 方法终止后，需要删除或者回收当前脚本
        /// </summary>
        public virtual bool MethodIsEnd()
        {
            return IsMethodEnd;
        }
        /// <summary>
        /// 方法参数重置，方便重复使用
        /// </summary>
        public virtual void MethodReset()
        {
            IsMethodEnd = false;
        }
        /// <summary>
        /// 具体操作，流程
        /// </summary>
        public abstract void MethodRun();
    }
}
