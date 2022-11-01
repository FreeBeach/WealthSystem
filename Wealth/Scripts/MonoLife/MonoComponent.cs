
namespace Lucifer.MonoLife
{
    class MonoComponent : UnityEngine.MonoBehaviour, IMonoBase
    {
        void Awake()
        {
            MonoLifeManager.Instance.ActionMonoAdd(this);
        }

        void OnEnable()
        {
            MonoLifeManager.Instance.ActionEnableUpdate(this, true);
            SelfOnEnable();
        }

        void OnDisable()
        {
            SelfOnDisable();
            MonoLifeManager.Instance.ActionEnableUpdate(this, false);
        }
        void OnDestroy()
        {
            SelfOnDestory();
            MonoLifeManager.Instance.ActionMonoRemove(this);
        }

        public virtual void SelfOnEnable()
        {

        }

        public virtual void SelfUpdate()
        {

        }

        public virtual void SelfOnDisable()
        {

        }

        public virtual void SelfOnDestory()
        {

        }

    }


}
