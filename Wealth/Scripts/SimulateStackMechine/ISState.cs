

namespace Lucifer.SimulateStackMechine
{
    /// <summary>
    /// 状态机对外逻辑接口
    /// 前台最多只有一个有效状态机
    /// </summary>
    public abstract class ISState
    {
        /// <summary>
        /// true 表示有挂起状态，被其他状态挤掉会自动切换到此状态
        /// false 表示被其他状态挤掉时会跳过OnStatePause，执行OnStateDestroy
        /// </summary>
        public abstract bool ExistPauseStage { get; }

        /// <summary>
        /// 创建状态机调用
        /// 仅生效一次
        /// </summary>
        public abstract void OnStateCreate();
        /// <summary>
        /// 启动状态机
        /// 可循环启动
        /// </summary>
        public abstract void OnStateLaunch(params object[] datas);
        /// <summary>
        /// 状态机内部刷新
        /// </summary>
        public abstract void OnStateRefresh(params object[] datas);
        /// <summary>
        /// 挂起状态机
        /// 可循环挂起
        /// </summary>
        public abstract void OnStatePause(params object[] datas);
        /// 状态机销毁
        /// 调用一次
        /// </summary>
        public abstract void OnStateDestroy(params object[] datas);

    }
}
