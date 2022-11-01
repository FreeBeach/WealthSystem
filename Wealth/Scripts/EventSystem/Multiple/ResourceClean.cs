using System.Collections.Generic;

namespace ResPool
{
    public class ResourcePoolClean
    {
        static List<ResPoolCleanInterface> cleanPoolsList;
        /// <summary>
        /// �̶�ʱ������
        /// </summary>
        public static void DoSomeClean()
        {
            if (cleanPoolsList == null) return;
            foreach(var serve in cleanPoolsList)
            {
                serve.CleanSomeRes();
            }
        }
        /// <summary>
        /// ĳ��Դ�ؼ��������
        /// </summary>
        /// <param name="serve"></param>
        public static void AppendCleanServe(ResPoolCleanInterface serve)
        {
            if (serve == null) return;
            if (cleanPoolsList == null) cleanPoolsList = new List<ResPoolCleanInterface>();
            if (cleanPoolsList.Contains(serve)) return;
            cleanPoolsList.Add(serve);
        }
        /// <summary>
        /// ĳ��Դ���Ƴ�������
        /// </summary>
        /// <param name="serve"></param>
        public static void RemoveCleanServe(ResPoolCleanInterface serve)
        {
            if (serve == null) return;
            if (cleanPoolsList == null) return;
            if (!cleanPoolsList.Contains(serve)) return;
            cleanPoolsList.Remove(serve);
            if (cleanPoolsList.Count == 0) cleanPoolsList = null;
        }
    }
}

