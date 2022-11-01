using System;
using System.Collections.Generic;
using UnityEngine;

namespace GestureEvents
{
    public enum GestureFingleType
    {
        None, OneFingle, TwoFingle,MouseScroll
    }
    /// <summary>
    /// 单指，双指操作,更新数据，运行事件
    /// </summary>
    public class GestureEvent
    {
        public Dictionary<GestureFingleType, List<Action<float[]>>> registerEvents;
        public TouchEvent touchEvent { get; protected set; }
        /// <summary>
        /// 添加监听
        /// </summary>
        public void AddUpdateListener(GestureFingleType gestureType,Action<float[]> action)
        {
            if (action == null) return;
            if (touchEvent == null) touchEvent = new TouchEvent();
            touchEvent.AddTouchEvent(GetGestureTouchType(gestureType));
            if (registerEvents == null) registerEvents = new Dictionary<GestureFingleType, List<Action<float[]>>>();
            if (!registerEvents.ContainsKey(gestureType)) registerEvents.Add(gestureType, new List<Action<float[]>>());
            registerEvents[gestureType].Add(action);
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveUpdateListener(GestureFingleType gestureType, Action<float[]> action)
        {
            if (action == null || registerEvents==null) return;
            if (!registerEvents.ContainsKey(gestureType)) return;
            if (registerEvents[gestureType].Contains(action))
                registerEvents[gestureType].RemoveAll((fun) => { return fun == action; });
            if(registerEvents[gestureType].Count==0)
            {
                registerEvents.Remove(gestureType);
                if(touchEvent!=null)
                    touchEvent.RemoveTouchEvent(GetGestureTouchType(gestureType));
            }
        }
        /// <summary>
        /// 事件执行，包含Input输入事件,执行事件
        /// </summary>
        public void EventUpdate()
        {
            if (touchEvent == null) return;
            if (registerEvents == null) return;
            touchEvent.TouchUpdate();
            if (touchEvent.GetActiveGesture == GestureTouchType.None) return;
            if (!GestureSettles.CheckGesturesValid()) return;
            ActionRun(GetGestureFingleType(touchEvent.GetActiveGesture), touchEvent.GetTouchEventData());
        }
        void ActionRun(GestureFingleType gestureType,float[] data)
        {
            if (!registerEvents.ContainsKey(gestureType)) return;
            foreach (var action in registerEvents[gestureType])
            {
                action?.Invoke(data);
            }
        }
        GestureFingleType GetGestureFingleType(GestureTouchType gestureType)
        {
            switch (gestureType)
            {
                case GestureTouchType.OneTouch:
                    return GestureFingleType.OneFingle;
                case GestureTouchType.LeftMouse:
                    return GestureFingleType.OneFingle;
                case GestureTouchType.TwoTouch:
                    return GestureFingleType.TwoFingle;
                case GestureTouchType.MiddleMouseScroll:
                    return GestureFingleType.MouseScroll;
                default:
                    return GestureFingleType.None;
            }
        }
        GestureTouchType GetGestureTouchType(GestureFingleType gestureType)
        {
            switch (gestureType)
            {
#if UNITY_EDITOR
                case GestureFingleType.OneFingle:
                    return GestureTouchType.LeftMouse;
#else
                case GestureFingleType.OneFingle:
                    return GestureTouchType.OneTouch;
#endif
                case GestureFingleType.TwoFingle:
                    return GestureTouchType.TwoTouch;
                case GestureFingleType.MouseScroll:
                    return GestureTouchType.MiddleMouseScroll;
                default:
                    return GestureTouchType.None;
            }
        }
    }
}


