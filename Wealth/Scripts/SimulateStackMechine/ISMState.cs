

namespace Lucifer.SimulateStackMechine
{
    /// <summary>
    /// 状态机对外逻辑接口
    /// </summary>
    public abstract class ISMState : ISState, ISMechine
    {
        public override bool ExistPauseStage { get; } = false;
        public virtual SStateEnum StateEnumKey { get; } = SStateEnum.None;

        public virtual bool PermitTakeOver { get; } = true;
        public virtual MechineStage RealMStage { get; private set; } = MechineStage.MNone;
        public virtual void CreateMechine()
        {
            OnStateCreate();
            RealMStage = MechineStage.MAwake;
        }

        public virtual void LaunchMechine(params object[] datas)
        {
            OnStateLaunch(datas);
            RealMStage = MechineStage.MRunning;
        }

        public virtual void RefreshMechine(params object[] datas)
        {
            OnStateRefresh(datas);
        }

        public virtual void PauseMechine(params object[] datas)
        {
            if (!ExistPauseStage)
                return;
            OnStatePause(datas);
            RealMStage = MechineStage.MPause;
        }

        public virtual void DestoryMechine(params object[] datas)
        {
            OnStateDestroy(datas);
            RealMStage = MechineStage.MDestory;
        }

        public virtual void CompleteLogicProcess(MechineStage nextStage, params object[] datas)
        {
            if (nextStage == MechineStage.MAwake)
            {
                if (RealMStage != MechineStage.MNone)
                    return;
                CreateMechine();
                CompleteLogicProcess(MechineStage.MRunning, datas);
            }
            else if (nextStage == MechineStage.MRunning)
            {
                if (RealMStage == MechineStage.MNone)
                {
                    CompleteLogicProcess(MechineStage.MAwake);
                }
                else if (RealMStage == MechineStage.MAwake)
                {
                    LaunchMechine(datas);
                }
                else if (RealMStage == MechineStage.MRunning)
                {
                    RefreshMechine(datas);
                }
                else if (RealMStage == MechineStage.MPause)
                {
                    LaunchMechine(datas);
                }
            }
            else if (nextStage == MechineStage.MPause)
            {
                if (RealMStage == MechineStage.MRunning)
                {
                    PauseMechine(datas);
                    if (!ExistPauseStage)
                    {
                        DestoryMechine(datas);
                    }
                }
            }
            else if (nextStage == MechineStage.MDestory)
            {
                if (RealMStage != MechineStage.MDestory)
                    DestoryMechine(datas);
            }
        }
    }
}
