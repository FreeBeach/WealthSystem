using System.Collections;
using System.Collections.Generic;
namespace ResPool
{
    public abstract class ResourceTypePool<T, T1> : ResPoolCleanInterface where T : ICollection<T1>, new()
    {
        #region 资源池管理方法重写
        public void AppendServe()
        {
            ResourcePoolClean.AppendCleanServe(this);
        }

        public void RemoveServe()
        {
            ResourcePoolClean.RemoveCleanServe(this);
        }

        #endregion

        T resourceMap;
        public void Init()
        {
            resourceMap = new T();
            AppendServe();
        }
        public void UnInit()
        {
            resourceMap.Clear();
            RemoveServe();
        }
        /// <summary>
        /// 销毁部分多余数据
        /// 防止内存长时间占用
        /// </summary>
        public void DestorySomeResIfNeed()
        {

        }
        /// <summary>
        /// 获取一份资源
        /// </summary>
        /// <returns></returns>
        public abstract T1 ResourceCreate();
        /// <summary>
        /// 回收资源
        /// </summary>
        public abstract void ResourceRecycle(T1 res);
        /// <summary>
        /// 资源池种子
        /// </summary>
        public virtual void ResourceSeeds()
        { }

        /// <summary>
        /// 资源定时清理
        /// </summary>
        public virtual void CleanSomeRes()
        {

        }



    }
}
