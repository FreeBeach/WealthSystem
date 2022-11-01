using UnityEngine;
using GestureEvents;
public class GestureTouches
{
    #region 固定化静态方法区
    static GestureEvent gestureEvent;
    public static GestureEvent GestureEvent
    {
        get
        {
            GestureOperation(true);
            return gestureEvent;
        }
    }
    public static void GestureOperation(bool isOpen)
    {
        if (gestureEvent == null)
            gestureEvent = new GestureEvent();
        if (isOpen)
        {
            RunUpdate();
        }
        else if(clockKey!=0)
        {
            StopUpdate();
        }
    }
    #endregion

    #region 自定义如何执行手势EventUpdate区域

    static int clockKey = 0;
    /// <summary>
    /// 运行Gesture
    /// </summary>
    public static void RunUpdate()
    {
        if (clockKey != 0) return;
        clockKey = ClockEvent.AddEventAlways(time=> { gestureEvent?.EventUpdate(); });
    }
    /// <summary>
    /// 关闭Gesture
    /// </summary>
    public static void StopUpdate()
    {
        ClockEvent.CloseClockEvent(clockKey);
        clockKey = 0;
    }
    #endregion
}
