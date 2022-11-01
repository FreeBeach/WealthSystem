using System.Collections;
using System.Collections.Generic;
namespace ResPool
{
    public abstract class ResourceTypePool<T, T1> : ResPoolCleanInterface where T : ICollection<T1>, new()
    {
        #region ��Դ�ع�������д
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
        /// ���ٲ��ֶ�������
        /// ��ֹ�ڴ泤ʱ��ռ��
        /// </summary>
        public void DestorySomeResIfNeed()
        {

        }
        /// <summary>
        /// ��ȡһ����Դ
        /// </summary>
        /// <returns></returns>
        public abstract T1 ResourceCreate();
        /// <summary>
        /// ������Դ
        /// </summary>
        public abstract void ResourceRecycle(T1 res);
        /// <summary>
        /// ��Դ������
        /// </summary>
        public virtual void ResourceSeeds()
        { }

        /// <summary>
        /// ��Դ��ʱ����
        /// </summary>
        public virtual void CleanSomeRes()
        {

        }



    }
}
