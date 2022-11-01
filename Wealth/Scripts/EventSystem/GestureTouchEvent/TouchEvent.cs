using System;
using System.Collections.Generic;
using UnityEngine;

namespace GestureEvents
{
    public enum GestureTouchType
    {
        None, OneTouch, TwoTouch,LeftMouse, MiddleMouseScroll
    }

    public abstract class TouchBase
    {
        protected GestureTouchType gestureType = GestureTouchType.None;
        protected float[] resoultData;
        public float[] GetDatas => resoultData;
        public GestureTouchType GestureType => gestureType;
        public bool isRunning { get; protected set; }
        /// <summary>
        /// 检测是否可以开始,正在运行
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckTouchWorking();
        public virtual void GestureOn()
        {
            isRunning = true;
        }
        public abstract void GestureUpdate();

        public virtual void GestureOff()
        {
            isRunning = false;
        }
        public virtual void GestureDestory()
        {
            //Debug.Log("GestureDestory,type="+ gestureType);
        }

    }

    /// <summary>
    /// 单指，双指操作,更新数据
    /// </summary>
    public class TouchEvent
    {
        GestureTouchType gestureType = GestureTouchType.None;
        List<TouchBase> touchUnit;
        TouchBase runningTouch;
        ///最大数量                              
        /// <summary>                       
        /// 获取正在生效的                               
        /// </summary>
        public GestureTouchType GetActiveGesture => gestureType;
        public void TouchUpdate()
        {
            if (touchUnit == null || touchUnit.Count==0) return;
            RunningTouch();
            FindRunningTouch();
        }
        void FindRunningTouch()
        {
            if (runningTouch != null) return;
            foreach (var tBase in touchUnit)
            {
                if (tBase.CheckTouchWorking())
                {
                    RefreshRunningTouchData(tBase, tBase.GestureType);
                    break;
                }
            }
        }
        void RunningTouch()
        {
            if (runningTouch == null) return;
            if (runningTouch.CheckTouchWorking())
            {
                runningTouch.GestureUpdate();
            }
            if (runningTouch.isRunning) return;
            RefreshRunningTouchData();
        }
        void RefreshRunningTouchData(TouchBase touch=null, GestureTouchType gtType= GestureTouchType.None)
        {
            runningTouch = touch;
            gestureType = gtType;
        }
        
        /// <summary>
        /// 添加Touch解析事件
        /// </summary>
        /// <param name="gestureType"></param>
        public void AddTouchEvent(GestureTouchType gestureType)
        {
            if (touchUnit == null) touchUnit=new List<TouchBase>();
            foreach(var touch in touchUnit)
            {
                if (touch.GestureType == gestureType)
                    return;
            }
            if (gestureType == GestureTouchType.OneTouch)
                touchUnit.Add(new GestrureTouchOne());
            if (gestureType == GestureTouchType.LeftMouse)
                touchUnit.Add(new GestrureLeftMouse());
            if (gestureType == GestureTouchType.MiddleMouseScroll)
                touchUnit.Add(new GestrureMiddleMouseScroll());
            else if (gestureType == GestureTouchType.TwoTouch)
                touchUnit.Add(new GestrureTouchTwo());
        }
        /// <summary>
        /// 移除Touch解析事件
        /// </summary>
        /// <param name="gestureType"></param>
        public void RemoveTouchEvent(GestureTouchType gestureType)
        {
            if (touchUnit == null) return;
            for (int i = 0; i < touchUnit.Count; i++)
            {
                if (touchUnit[i].GestureType != gestureType) continue;
                if (runningTouch == touchUnit[i])
                    RefreshRunningTouchData();
                TouchBase touch = touchUnit[i];
                touchUnit.RemoveAt(i);
                touch.GestureDestory();
            }
        }
        public float[] GetTouchEventData()
        {
            if (runningTouch == null) return null;
            return runningTouch.GetDatas;
        }
    }
}
