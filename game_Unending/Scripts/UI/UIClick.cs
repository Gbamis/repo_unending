using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UE
{
    public class UIClick : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler, IPointerExitHandler
    {
        public Image thumbnail;
        public Text id;
        public Text title;
        public Image locked;
        public GameObject selector;

        public void SetDetails(Sprite tb, int index, string titleText, bool isLocked = false)
        {
            thumbnail.sprite = tb;
            id.text = "0" + (index + 1).ToString();
            title.text = titleText;
            locked.enabled = isLocked;
        }
        public void OnPointerClick(PointerEventData ped)
        {
            GameManager.Instance.uIManager.eventSelection.SelectEvent(transform.GetSiblingIndex());
            GameManager.Instance.sfxManager.UIElementClickFX();
        }

        public void OnPointerMove(PointerEventData ped) { selector.SetActive(true); }

        public void OnPointerExit(PointerEventData ped) { selector.SetActive(false); }
    }
}
