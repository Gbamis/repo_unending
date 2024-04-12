
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UE
{
    public class WeaponClick : MonoBehaviour, IPointerClickHandler
    {
        public Image weaponIcon;
        private Button self;

        public void SetDetails(Sprite icon){
            weaponIcon.sprite = icon; 
            self = GetComponent<Button>();
            }

        public void OnPointerClick(PointerEventData ped)
        {
            int index = transform.GetSiblingIndex();
            GameManager.Instance.chosenJet.SwitchWeapon(GameManager.Instance.userData.aquiredWeapons[index]);
            GameManager.Instance.uIManager.gameplaySelection.weaponSelectionDialog.gameObject.SetActive(false);
            GameManager.Instance.sfxManager.EquipWeaponFX();
        }
    }
}
