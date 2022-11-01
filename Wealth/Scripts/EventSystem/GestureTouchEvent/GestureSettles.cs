using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GestureEvents
{
    /// <summary>
    /// 检测到手势事件的情况下，检测手势合法性，逻辑如下
    /// 启动在UI上边的Touch,生命周期内都不属于触发序列内
    /// </summary>
    class GestureSettles
    {
        static List<Func<bool>> gestureExceptFuncs;
        /// <summary>
        /// 检测是否合法,ture表示数据可用
        /// </summary>
        /// <returns></returns>
        public static bool CheckGesturesValid()
        {
            if (EventSystem.current.currentSelectedGameObject != null) return false;//
            if (CheckMouseValid() || CheckTouchValid()) return true;
            if (gestureExceptFuncs != null)
                foreach(var fun in gestureExceptFuncs)
                    if (fun()) return true;
            return false;
        }
        static bool CheckMouseValid()
        {
            if (!Input.GetMouseButton(0) && Input.mouseScrollDelta.y==0) return false;
            return !EventSystem.current.IsPointerOverGameObject();
        }
        static bool CheckTouchValid()
        {
            if (Input.touchCount == 0) return false;
            foreach(var touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 添加Over豁免事件,true表示在有touch的情况下，允许touch事件
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="isAppend"></param>
        public static void OperateFuncForException(Func<bool> fun,bool isAppend)
        {
            if (fun == null) return;
            if(isAppend)
            {
                if (gestureExceptFuncs == null) gestureExceptFuncs = new List<Func<bool>>();
                gestureExceptFuncs.Add(fun);
                return;
            }
            if (gestureExceptFuncs != null)
            {
                gestureExceptFuncs.RemoveAll(fun_ => { return fun_ == fun; });
                if (gestureExceptFuncs.Count == 0) gestureExceptFuncs = null;
            }
        }
    }
}
