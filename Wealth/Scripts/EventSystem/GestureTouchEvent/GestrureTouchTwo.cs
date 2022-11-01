using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GestureEvents
{
    /// <summary>
    /// 输出两点向量（1指向2）
    /// </summary>
    public class GestrureTouchTwo : TouchBase
    {
        Vector2 vecTem;
        int fingleID1;
        int fingleID2;
        Touch[] touches;
        /// <summary>
        /// resoultData表示两个点之间的向量
        /// </summary>
        public GestrureTouchTwo()
        {
            resoultData = new float[2];
            gestureType = GestureTouchType.TwoTouch;
        }
        public override void GestureOn()
        {
            base.GestureOn();
            InputDeltaValue();
        }
        public override void GestureOff()
        {
            base.GestureOff();
            ResetData(-1,-1,0,0);
        }
        public override void GestureUpdate()
        {
            InputDeltaValue();
        }
        void ResetData(int f1,int f2,float d1,float d2)
        {
            fingleID1 = f1;
            fingleID2 = f2;
            resoultData[0] = d1;
            resoultData[1] = d2;
        }
        void InputDeltaValue()
        {
            touches = Input.touches;
            if (!CheckSameFingle(touches[0].fingerId, touches[1].fingerId)) return;
            vecTem = touches[1].position - touches[0].position;
            resoultData[0] = vecTem.x;
            resoultData[1] = vecTem.y;
        }
        bool CheckSameFingle(int fingerId1, int fingerId2)
        {
            if (fingleID1 == fingerId1 && fingleID2 == fingerId2)
                return true;
            ResetData(fingerId1, fingerId2,0,0);
            return false;
        }
        public override bool CheckTouchWorking()
        {
            if (isRunning && Input.touchCount != 2)
            {
                GestureOff();
            }
            else if (!isRunning && Input.touchCount == 2)
            {
                GestureOn();
            }
            return isRunning;
        }
    }
}
