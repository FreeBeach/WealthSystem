using System.Collections.Generic;
using Lucifer.SingleTool;

namespace Lucifer.MonoLife
{
    /// <summary>
    /// 默认生命周期不销毁
    /// </summary>
    public class MonoLifeManager : SingleInstance<MonoLifeManager>
    {
        public enum MonoState
        {
            Die, //删除
            Alive, //运行
            Pause,//暂停，针对Update
        }
        private Dictionary<IMonoBase, MonoState> updateLifeActions = new Dictionary<IMonoBase, MonoState>();
        List<IMonoBase> dieList = new List<IMonoBase>();
        public void ActionMonoAdd(IMonoBase mono)
        {
            ActionDealWithAdd(mono);
        }
        public void ActionMonoRemove(IMonoBase mono)
        {
            ActionDealWithRemove(mono);
        }
        /// <summary>
        /// Mono中Update临时停用或启用
        /// </summary>
        /// <param name="updateMono"></param>
        /// <param name="isEnable"></param>
        public void ActionEnableUpdate(IMonoBase updateMono, bool isEnable)
        {
            if (updateMono == null || !updateLifeActions.ContainsKey(updateMono)) return;
            if (updateLifeActions[updateMono] == MonoState.Alive && !isEnable)
            {
                updateLifeActions[updateMono] = MonoState.Pause;
            }
            else if (updateLifeActions[updateMono] == MonoState.Pause && isEnable)
            {
                updateLifeActions[updateMono] = MonoState.Alive;
            }
        }

        void Update()
        {
            if (updateLifeActions.Count == 0) return;
            foreach (IMonoBase monobase in updateLifeActions.Keys)
            {
                if (monobase == null)
                {
                    dieList.Add(monobase);
                }
                else if (updateLifeActions[monobase] == MonoState.Alive)
                {
                    monobase.SelfUpdate();
                }
                else if (updateLifeActions[monobase] == MonoState.Die)
                {
                    dieList.Add(monobase);
                }
            }
            for (int i = 0; i < dieList.Count; i++)
            {
                if (updateLifeActions.ContainsKey(dieList[i]) && updateLifeActions[dieList[i]] == MonoState.Die)
                    updateLifeActions.Remove(dieList[i]);
            }
            dieList.Clear();
        }
        private void ActionDealWithAdd(IMonoBase mono)
        {
            if (mono == null) return;
            if (updateLifeActions.ContainsKey(mono)) return;
            updateLifeActions[mono] = MonoState.Alive;
        }
        private void ActionDealWithRemove(IMonoBase mono)
        {
            if (mono == null) return;
            if (updateLifeActions.ContainsKey(mono))
                updateLifeActions[mono] = MonoState.Die;
        }
    }
}
