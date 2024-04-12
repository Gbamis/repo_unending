using System.Collections;
using System.Collections.Generic;
using UE;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectionDialog : UIController
{
    public Button chooseBtn;
    public Transform weaponListTransform;
    public WeaponClick weaponUIPrefab;

    private List<Weapon> weapons = new List<Weapon>();

    public override void Init(UIManager ui)
    {
        uiManagerRef = ui;
        KeyInput.Instance.swapCmd.IsStarted  +=() => gameObject.SetActive(false);
    }

    protected override void Enable()
    {
        GameManager.Instance.sfxManager.SwapWeaponFX();
        Time.timeScale = 0.05f;

        weapons = GameManager.Instance.userData.GetAvailableWepons;
        if (weaponListTransform.childCount > 0)
        {
            foreach (Transform child in weaponListTransform) { Destroy(child.gameObject); }
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            WeaponClick ui = Instantiate(weaponUIPrefab);
            ui.transform.SetParent(weaponListTransform);
            ui.GetComponent<RectTransform>().localScale = Vector3.one / 2;
            ui.SetDetails(weapons[i].icon);
            ui.gameObject.SetActive(true);
        }

        //weaponListTransform.GetChild(0).GetComponent<Button>().Select();

    }

    protected override void Disable()
    {
        Time.timeScale = 1;
        //GameManager.Instance.sfxManager.GamePausedResumeFX();
    }
}
