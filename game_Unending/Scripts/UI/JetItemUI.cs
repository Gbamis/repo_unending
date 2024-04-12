using UnityEngine;
using UnityEngine.EventSystems;

namespace UE
{
    public class JetItemUI : MonoBehaviour, IPointerClickHandler
    {
        public UnityEngine.UI.Image thumbnail;
        public void OnPointerClick(PointerEventData ped)
        {
            GameManager.Instance.uIManager.jetSelection.SelectJet(transform.GetSiblingIndex());
            GameManager.Instance.sfxManager.UIElementClickFX();
        }
    }
}
