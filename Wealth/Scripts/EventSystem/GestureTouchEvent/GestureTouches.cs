using UnityEngine;
using GestureEvents;
public class GestureTouches
{
    #region �̶�����̬������
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

    #region �Զ������ִ������EventUpdate����

    static int clockKey = 0;
    /// <summary>
    /// ����Gesture
    /// </summary>
    public static void RunUpdate()
    {
        if (clockKey != 0) return;
        clockKey = ClockEvent.AddEventAlways(time=> { gestureEvent?.EventUpdate(); });
    }
    /// <summary>
    /// �ر�Gesture
    /// </summary>
    public static void StopUpdate()
    {
        ClockEvent.CloseClockEvent(clockKey);
        clockKey = 0;
    }
    #endregion
}
