using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GestureEvents
{
    /// <summary>
    /// 鼠标滑动，输出插值
    /// </summary>
    public class GestrureMiddleMouseScroll : TouchBase
    {
        public GestrureMiddleMouseScroll()
        {
            resoultData = new float[1];
            gestureType = GestureTouchType.MiddleMouseScroll;
        }
        public override void GestureOn()
        {
            base.GestureOn();
            resoultData[0] = Input.mouseScrollDelta.y;
        }
        public override void GestureOff()
        {
            base.GestureOff();
            resoultData[0] = 0;
        }

        public override bool CheckTouchWorking()
        {
            if (isRunning && Input.mouseScrollDelta.y==0)
            {
                GestureOff();
            }
            else if (!isRunning && Input.mouseScrollDelta.y!=0)
            {
                GestureOn();
            }
            return isRunning;
        }

        public override void GestureUpdate()
        {
            resoultData[0] = Input.mouseScrollDelta.y;
        }
    }
}
