using UnityEngine;
using UnityEngine.EventSystems;

namespace GestureEvents
{
    /// <summary>
    /// ���ƻ��⣬�������Colider�Ϸ���ʱ��
    /// ���Ƽ���
    /// </summary>
    public class GestureOverEscape : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isObjOver = false;
        
        public void OnEnable()
        {
            GestureSettles.OperateFuncForException(GetEscapeSign, true);
        }
        public void OnDisable()
        {
            GestureSettles.OperateFuncForException(GetEscapeSign, false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            isObjOver=true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isObjOver = false;
        }
        private bool GetEscapeSign()
        {
            return isObjOver;
        }
    }

}
