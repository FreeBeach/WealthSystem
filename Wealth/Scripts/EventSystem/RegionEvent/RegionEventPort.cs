using System;
using EventKeyType = System.String;
namespace RegionEvent
{
    /// <summary>
    /// 此处是对外接口
    /// </summary>
    public partial class RegionEvent
    {
        /// <summary>
        /// 注册事件入口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventKey"></param>
        /// <param name="actionOrFunc"></param>
        public void RegisterEvent<T>(EventKeyType eventKey, T actionOrFunc) where T : Delegate
        {
            RegisterEventLogic(eventKey, actionOrFunc);
        }
        /// <summary>
        /// 删除事件入口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventKey"></param>
        /// <param name="actionOrFunc"></param>
        public void RemoveEvent<T>(EventKeyType eventKey, T actionOrFunc = null) where T : Delegate
        {
            RemoveEventLogic(eventKey, actionOrFunc);
        }
        /// <summary>
        /// 移除所有接口
        /// </summary>
        public void RemoveEventAll()
        {
            ClearAllEvent();
        }
        public void TriggerEvent(EventKeyType eventKey)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action> run = (action) => { action?.Invoke(); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1>(EventKeyType eventKey, T1 param1)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1>> run = (action) => { action?.Invoke(param1); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1, T2>(EventKeyType eventKey, T1 param1, T2 param2)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1, T2>> run = (action) => { action?.Invoke(param1, param2); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1, T2, T3>(EventKeyType eventKey, T1 param1, T2 param2, T3 param3)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1, T2, T3>> run = (action) => { action?.Invoke(param1, param2, param3); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1, T2, T3, T4>(EventKeyType eventKey, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1, T2, T3, T4>> run = (action) => { action?.Invoke(param1, param2, param3, param4); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1, T2, T3, T4, T5>(EventKeyType eventKey, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1, T2, T3, T4, T5>> run = (action) => { action?.Invoke(param1, param2, param3, param4, param5); };
            RunTriggerEvent(eventKey, run);
        }
        public void TriggerEvent<T1, T2, T3, T4, T5, T6>(EventKeyType eventKey, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (!IsKeyExist(eventKey)) return;
            Action<Action<T1, T2, T3, T4, T5, T6>> run = (action) => { action?.Invoke(param1, param2, param3, param4, param5, param6); };
            RunTriggerEvent(eventKey, run);
        }
    }
}
