using System;
using System.Collections.Generic;

namespace Lucifer.SingleTool
{
    public class ClockKey
    {
        private Func<float, bool> usedFun;
        public bool isEnd;
        public ClockKey(Func<float, bool> usedFun)
        {
            this.usedFun = usedFun;
            isEnd = false;
        }
        public int GetKey
        {
            get { return usedFun.GetHashCode(); }
        }
        public bool ExcuteOneTime(float deltatime)
        {
            if (isEnd)
                return isEnd;
            isEnd = true;
            isEnd = usedFun(deltatime);
            return isEnd;
        }
    }
    public class ClockEvent : SingleInstance<ClockEvent>
    {
        Dictionary<int, ClockKey> clockFunList = new Dictionary<int, ClockKey>();
        Dictionary<int, ClockKey> clockFunListTem = new Dictionary<int, ClockKey>();
        readonly List<int> deltaHashCods = new List<int>();
        bool isWorking;
        /// <summary>
        /// Clock运行逻辑
        /// </summary>
        /// <param name="deltaTime">时间间隔=Time.deltaTime</param>
        public void SelfUpdate(float deltaTime)
        {
            if (clockFunList.Count == 0)
                return;
            isWorking = true;
            foreach (int key in clockFunList.Keys)
            {
                if (deltaHashCods.Contains(key))
                {
                    //已经被删除
                }
                else
                {
                    if (clockFunList[key].ExcuteOneTime(deltaTime))
                    {
                        if (!deltaHashCods.Contains(key))
                        {
                            deltaHashCods.Add(key);
                        }
                    }
                }
            }
            isWorking = false;
            foreach (int key in clockFunListTem.Keys)
            {
                clockFunList[key] = clockFunListTem[key];
            }
            clockFunListTem.Clear();
            for (int i = 0; i < deltaHashCods.Count; i++)
            {
                if (clockFunList.ContainsKey(deltaHashCods[i]))
                    clockFunList.Remove(deltaHashCods[i]);
            }
            deltaHashCods.Clear();
        }
        /// <summary>
        /// 关闭计时器
        /// </summary>
        /// <param name="clockkey"></param>
        /// <returns></returns>
        public static bool CloseClockEvent(int clockkey)
        {
            if (!Instance.deltaHashCods.Contains(clockkey))
                Instance.deltaHashCods.Add(clockkey);
            return Instance.deltaHashCods.Contains(clockkey);
        }
        int AddClockFun(ClockKey clockkey)
        {
            if (isWorking)
                clockFunListTem.Add(clockkey.GetKey, clockkey);
            else
                clockFunList.Add(clockkey.GetKey, clockkey);
            return clockkey.GetKey;
        }
        /// <summary>
        /// totalTime时间完成后执行
        /// </summary>
        /// <param name="totalTime"></param>
        /// <param name="funCall"></param>
        /// <returns></returns>
        public static int AddEventTimeOut(float totalTime, Action funCall)
        {
            Func<float, bool> usedfun = deltaTime =>
            {
                totalTime -= deltaTime;
                if (totalTime <= 0)
                    funCall();
                return totalTime <= 0;
            };
            return Instance.AddClockFun(new ClockKey(usedfun));
        }
        /// <summary>
        /// 随Update刷新
        /// </summary>
        /// <param name="funCall"></param>
        /// <returns></returns>
        public static int AddEventAlways(Action<float> funCall)
        {
            Func<float, bool> usedfun = deltatime =>
            {
                funCall(deltatime);
                return false;
            };
            return Instance.AddClockFun(new ClockKey(usedfun));
        }
        /// <summary>
        /// funCall(totaltime)中时间是timelen累计时间
        /// </summary>
        /// <param name="timeLen"></param>
        /// <param name="funCall"></param>
        /// <param name="isParameterTotal">方法传参默认是累计时间</param>
        /// <returns></returns>
        public static int AddEventAlwaysToEnd(float timeLen, Action<float> funCall, bool isParameterTotal = true)
        {
            float totaltime = 0;
            Func<float, bool> usedfun = deltaTime =>
            {
                totaltime += deltaTime;
                totaltime = totaltime < timeLen ? totaltime : timeLen;
                funCall(isParameterTotal ? totaltime : deltaTime);
                return totaltime == timeLen;
            };
            return Instance.AddClockFun(new ClockKey(usedfun));
        }
        /// <summary>
        /// 推进次数，每次间隔多少秒
        /// 第一次触发需要timeStep秒后
        /// </summary>
        /// <param name="timeStep">时间步长</param>
        /// <param name="stepNum">总次数</param>
        /// <param name="timeToZeroAfterOneStep">前进一步是否需要重新计时</param>
        /// <param name="funCall"></param>
        /// <returns></returns>
        public static int AddEventAlwaysStepToEnd(float timeStep, int stepNum, Action funCall, bool timeToZeroAfterOneStep = false)
        {
            float stepTotalTime = 0;
            Func<float, bool> usedfun = deltatime =>
            {
                stepTotalTime += deltatime;
                while (stepTotalTime >= timeStep && stepNum > 0)
                {
                    stepTotalTime -= timeStep;
                    funCall();
                    stepNum -= 1;
                    if (timeToZeroAfterOneStep)
                        stepTotalTime = 0;
                }
                return stepNum <= 0;
            };
            return Instance.AddClockFun(new ClockKey(usedfun));
        }
    }
}
