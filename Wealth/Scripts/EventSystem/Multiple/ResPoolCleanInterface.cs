using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResPool
{
    /// <summary>
    /// ��Դ�ع����߻����ӿ�
    /// </summary>
    public  interface ResPoolCleanInterface 
    {
        void CleanSomeRes();
        void AppendServe();
        void RemoveServe();

    }
}
