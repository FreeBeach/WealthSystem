using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GestureEvents
{
    /// <summary>
    /// 单指滑动，输出移动插值
    /// </summary>
    public class GestrureTouchOne: TouchBase
    {
        Vector2 vecTem;
        int fingleID = -1;
        Touch touch;
        /// <summary>
        /// resoultData=表示两个touch点之间的变化量
        /// </summary>

        public GestrureTouchOne()
        {
            resoultData = new float[2];
            gestureType = GestureTouchType.OneTouch;
        }
        public override void GestureOn()
        {
            base.GestureOn();
            InputDeltaValue();
        }
        public override void GestureOff()
        {
            base.GestureOff();
            fingleID = -1;
            resoultData[0] = 0;
            resoultData[1] = 0;
        }
        public override void GestureUpdate()
        {
            InputDeltaValue();
        }
        void InputDeltaValue()
        {
            touch = Input.GetTouch(0);
            CheckSameFingle(touch.fingerId);
            vecTem = touch.deltaPosition;
            resoultData[0] = vecTem.x;
            resoultData[1] = vecTem.y;
        }
        void CheckSameFingle(int fingerId)
        {
            if (fingleID == fingerId)
                return;
            fingleID = fingerId;
        }

        public override bool CheckTouchWorking()
        {
            if(isRunning && Input.touchCount != 1)
            {
                GestureOff();
            }
            else if(!isRunning && Input.touchCount == 1)
            {
                GestureOn();
            }
            return isRunning;
        }
    }
}
