using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GestureEvents
{
    /// <summary>
    /// 鼠标滑动，输出插值
    /// </summary>
    public class GestrureLeftMouse : TouchBase
    {
        Vector2 vecTem;
        Vector3 oldMousePos;
        public GestrureLeftMouse()
        {
            resoultData = new float[2];
            gestureType = GestureTouchType.LeftMouse;
        }
        public override void GestureOn()
        {
            base.GestureOn();
            oldMousePos = Input.mousePosition;
            resoultData[0] = 0;
            resoultData[1] = 0;
        }
        public override void GestureOff()
        {
            base.GestureOff();
            oldMousePos = Vector2.zero;
            resoultData[0] = 0;
            resoultData[1] = 0;
        }

        public override bool CheckTouchWorking()
        {
            if (isRunning && !Input.GetMouseButton(0))
            {
                GestureOff();
            }
            else if (!isRunning && Input.GetMouseButton(0))
            {
                GestureOn();
            }
            return isRunning;
        }

        public override void GestureUpdate()
        {
            vecTem = Input.mousePosition - oldMousePos;
            resoultData[0] = vecTem.x;
            resoultData[1] = vecTem.y;
            oldMousePos = Input.mousePosition;
        }
    }
}
