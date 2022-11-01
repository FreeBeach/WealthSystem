
using System;
using System.Collections.Generic;

namespace Lucifer.SimulateStackMechine
{
    class MechineRunning
    {
        Dictionary<SStateEnum, Func<ISMechine>> registeredMechines;
        Dictionary<SStateEnum, ISMechine> aliveMechines = new Dictionary<SStateEnum, ISMechine>();
        /// <summary>
        /// 若在状态方法中通过MechineRunning切换下一状态，则下一状态延迟切换
        /// </summary>
        private Action changeStateActions;
        /// <summary>
        /// 状态机方法执行中标记
        /// </summary>
        private bool mechineStateChangeingSign = false;
        /// <summary>
        /// /上一个状态,受状态机影响
        /// </summary>
        public SStateEnum LastState { get; protected set; } = SStateEnum.None;
        /// <summary>
        /// 当前状态,受状态机影响
        /// </summary>
        public SStateEnum CurrentState { get; protected set; } = SStateEnum.None;
        /// <summary>
        /// 下一个状态,受状态机影响
        /// 仅存于mechineStateChangeingSign==true 的时间段内
        /// </summary>
        public SStateEnum NextState { get; protected set; } = SStateEnum.None;
        ISMechine localMechine;
        ISMechine nextMechine;
        /// <summary>
        /// 注册状态机
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="creatFun"></param>
        public void SStateRegister(SStateEnum stateKey, Func<ISMechine> creatFun)
        {
            if (registeredMechines.ContainsKey(stateKey))
            {
                Console.WriteLine("重复注册，key=" + stateKey);
                return;
            }
            registeredMechines.Add(stateKey, creatFun);
        }
        /// <summary>
        /// 切换，唤起，状态机
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="stage"></param>
        /// <param name="datas"></param>
        public void SwitchSState(SStateEnum stateKey, params object[] datas)
        {
            SwitchMStage(stateKey, MechineStage.MRunning, datas);
        }
        /// <summary>
        /// 更新状态机所处阶段
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="datas"></param>
        public void SwitchMStage(SStateEnum stateKey, MechineStage stage, params object[] datas)
        {
            if (mechineStateChangeingSign)
            {
                Action lastAction = changeStateActions;
                changeStateActions = null;
                changeStateActions = () =>
                {
                    lastAction?.Invoke();
                    SwitchMStage(stateKey, stage, datas);
                };
                return;
            }
            mechineStateChangeingSign = true;
            ChangeMechineState(stateKey, stage, datas);
            mechineStateChangeingSign = false;
            if (changeStateActions == null)
                return;
            Action usedAction = changeStateActions;
            changeStateActions = null;
            usedAction?.Invoke();
        }
        /// <summary>
        /// 获取状态机对象
        /// </summary>
        /// <param name="stateKey"></param>
        /// <returns></returns>
        public ISMState GetSMState(SStateEnum stateKey)
        {
            if (aliveMechines.ContainsKey(stateKey))
                return aliveMechines[stateKey] as ISMState;
            return null;
        }
        private void AddMechineState(SStateEnum stateKey)
        {
            if (registeredMechines.TryGetValue(stateKey, out Func<ISMechine> stateFun))
            {
                ISMechine mechine = stateFun();
                if (mechine.StateEnumKey == stateKey)
                {
                    aliveMechines.Add(stateKey, mechine);
                }
                else
                {
                    Console.WriteLine("状态标记SStateEnum值，与状态类内StateEnumKey标记不一致，请修改");
                }
            }
        }
        private ISMechine GetISMechine(SStateEnum stateKey)
        {
            if (stateKey == SStateEnum.None)
                return null;
            if (!aliveMechines.ContainsKey(stateKey))
            {
                AddMechineState(stateKey);
            }
            return aliveMechines[stateKey];
        }
        private void MechineStateRemove(SStateEnum stateKey)
        {
            aliveMechines.Remove(stateKey);
        }
        private void ChangeMechineState(SStateEnum stateKey, MechineStage stage, params object[] datas)
        {
            ///下一状态
            nextMechine = GetISMechine(stateKey);
            if (stateKey == SStateEnum.None || nextMechine == null)
                return;
            //检测是当前状态，或下一状态不允许托管，或下一状态显示阶段不MRunning
            if (stateKey == CurrentState || !nextMechine.PermitTakeOver || stage != MechineStage.MRunning)
            {
                nextMechine.CompleteLogicProcess(stage, datas);
                if (nextMechine.RealMStage == MechineStage.MDestory)
                {
                    MechineStateRemove(CurrentState);
                    if (nextMechine.PermitTakeOver)
                    {
                        LastState = CurrentState;
                        CurrentState = SStateEnum.None;
                    }
                }
                return;
            }
            //获取正在状态机内前置状态
            localMechine = GetISMechine(CurrentState);
            //当前状态不为空+状态以被托管+下一状态显示状态是MechineStage.MRunning
            if (localMechine != null)
            {
                NextState = stateKey;
                localMechine.CompleteLogicProcess(MechineStage.MPause);
                NextState = SStateEnum.None;
                if (localMechine.RealMStage == MechineStage.MDestory)
                {
                    MechineStateRemove(CurrentState);
                }
            }
            nextMechine.CompleteLogicProcess(stage, datas);
            LastState = CurrentState;
            CurrentState = stateKey;
            localMechine = null;
            nextMechine = null;
        }
    }
}
