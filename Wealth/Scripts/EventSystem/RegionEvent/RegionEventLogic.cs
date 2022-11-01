using System;
using System.Collections.Generic;
using EventKeyType = System.String;
namespace RegionEvent
{
    /// <summary>
    /// 区域事件
    /// </summary>
    public partial class RegionEvent
    {
        private Dictionary<EventKeyType, List<AppandRunMethods>> eventList;
        bool isEventCanClear = false;//事件执行中
        bool isSomeEventLoseActive = false;//是否有事件失效，需要随后删除
        /// <summary>
        /// 注册局部事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventKey"></param>
        /// <param name="actionOrFunc"></param>
        private void RegisterEventLogic<T>(EventKeyType eventKey, T actionOrFunc) where T : Delegate
        {
            if (IsKeyIllegal(eventKey)) return;
            if (eventList == null) eventList = new Dictionary<EventKeyType, List<AppandRunMethods>>();
            if (!eventList.ContainsKey(eventKey)) eventList[eventKey] = new List<AppandRunMethods>();
            eventList[eventKey].Add(new RegionEventFun<T>(actionOrFunc));
        }
        /// <summary>
        /// 删除局部事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventKey">事件ID</param>
        /// <param name="actionOrFunc">为空表示删除整个key值注册事件,否则表示某一个事件</param>
        private void RemoveEventLogic<T>(EventKeyType eventKey, T actionOrFunc = null) where T : Delegate
        {
            if (IsKeyIllegal(eventKey)) return;
            if (eventList == null) return;
            if (!eventList.ContainsKey(eventKey)) return;
            List<AppandRunMethods> methods = eventList[eventKey];
            int len = methods.Count;
            for (int i = len - 1; i > -1; i--)
            {
                if (actionOrFunc == null || methods[i].IsSameAction(actionOrFunc))
                {
                    methods[i].isMethodActive = false;
                    isSomeEventLoseActive = true;
                }
            }
            DestroySomeEvent();
        }
        /// <summary>
        /// 删除所有事件
        /// </summary>
        private void ClearAllEvent()
        {
            if (eventList == null) return;
            foreach(var kv in eventList)
            {
                if (kv.Value == null) continue;
                foreach(var mfun in kv.Value)
                {
                    mfun.isMethodActive = false;
                }
                isSomeEventLoseActive = true;
            }
            DestroySomeEvent();
        }
        /// <summary>
        /// 执行销毁操作
        /// </summary>
        private void DestroySomeEvent()
        {
            if(eventList == null) return;
            if ( !isSomeEventLoseActive || !isEventCanClear ) return;
            List<EventKeyType> keys = new List<EventKeyType>(eventList.Keys);
            List<AppandRunMethods> methods;
            for (int i = 0; i < keys.Count; i++)
            {
                methods = eventList[keys[i]];
                for (int j = methods.Count - 1; j > -1; j--)
                {
                    if (methods[j].isMethodActive) continue;
                    methods.RemoveAt(j);
                }
                if (methods.Count == 0)
                    eventList.Remove(keys[i]);
            }
            isSomeEventLoseActive = false;
            if (eventList.Count == 0)
                eventList = null;
        }


        /// <summary>
        /// 事件运行逻辑,
        /// 调用之前确保eventKey值存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventKey"></param>
        /// <param name="action"></param>
        private void RunTriggerEvent<T>(EventKeyType eventKey, Action<T> action) where T : Delegate
        {
            List<AppandRunMethods> methods = eventList[eventKey];
            isEventCanClear = false;
            int len = methods.Count;
            for (int i = 0; i < len; i++)
            {
                if (!methods[i].isMethodActive) continue;
                if (methods[i] is RegionEventFun<T> eventFun)
                {
                    action(eventFun.realFun);
                }
            }
            isEventCanClear = true;
            DestroySomeEvent();
        }

        /// <summary>
        /// 判断Key值非法，key是string
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private bool IsKeyIllegal(string eventKey)
        {
            return string.IsNullOrEmpty(eventKey);
        }
        /// <summary>
        /// 判断Key值非法，key是int
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private bool IsKeyIllegal(int eventKey)
        {
            return eventKey == 0;
        }
        /// <summary>
        /// 检查key值是否存在，触发事件调用
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private bool IsKeyExist(EventKeyType eventKey)
        {
            return !IsKeyIllegal(eventKey) && eventList != null && eventList.ContainsKey(eventKey);
        }

    }
}
