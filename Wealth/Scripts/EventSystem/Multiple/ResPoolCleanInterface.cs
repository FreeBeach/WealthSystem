using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResPool
{
    /// <summary>
    /// 资源池管理工具基础接口
    /// </summary>
    public  interface ResPoolCleanInterface 
    {
        void CleanSomeRes();
        void AppendServe();
        void RemoveServe();

    }
}
