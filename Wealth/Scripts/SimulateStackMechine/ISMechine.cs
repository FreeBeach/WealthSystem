
using Lucifer.SimulateStackMechine;

namespace Lucifer.SimulateStackMechine
{
    /// <summary>
    /// 状态机内部逻辑接口
    /// </summary>
    interface ISMechine
    {
        /// <summary>
        /// 状态机标记key,一个状态一个，不可更改，不可重复
        /// 强制一个状态一个类
        /// 新状态类必须重写
        /// </summary>
        SStateEnum StateEnumKey { get; }
        /// <summary>
        /// true表示受状态机逻辑（前台最多只有一个有效状态）影响
        /// false表示不加入状态机逻辑
        /// </summary>
        bool PermitTakeOver { get; }
        /// <summary>
        /// 当前状态所处阶段
        /// </summary>
        MechineStage RealMStage { get; }
        /// <summary>
        /// 创建状态机
        /// 仅执行一次
        /// </summary>
        void CreateMechine();
        /// <summary>
        /// 启动当前状态机
        /// 挂起后可重复启动
        /// </summary>
        void LaunchMechine(params object[] datas);
        /// <summary>
        /// 运行状态机
        /// </summary>
        void RefreshMechine(params object[] datas);
        /// <summary>
        /// 挂起当前状态机
        /// 稍后可以再次启动
        /// </summary>
        void PauseMechine(params object[] datas);
        /// <summary>
        /// 销毁当前状态机
        /// </summary>
        void DestoryMechine(params object[] datas);
        /// <summary>
        /// 全流程
        /// </summary>
        /// <param name="datas"></param>
        void CompleteLogicProcess(MechineStage nextStage, params object[] datas);
    }
}
