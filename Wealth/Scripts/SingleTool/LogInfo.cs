using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SingleTool
{
    public class LogInfo
    {
        public static void Log(object msg)
        {
            Debug.Log(msg);
        }
        public static void LogError(object msg)
        {
            Debug.LogError(msg);
        }
        public static void LogWarning(object msg)
        {
            Debug.LogWarning(msg);
        }
    }
}
