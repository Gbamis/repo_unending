using System.Collections;
using System.Collections.Generic;
using UE;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using Radishmouse;
namespace UE
{
    [System.Serializable]
    public struct DamageUI
    {
        public GameObject root;
        public RectTransform damageAnchor;

        public void Enable(bool value) { root.SetActive(value); }
        public void Update(Transform obj)
        {
            Vector2 a = damageAnchor.anchoredPosition;
            Vector2 b = Camera.main.WorldToViewportPoint(obj.position);

        }
    }

    [System.Serializable]
    public struct RefillUI
    {
        public Image shieldLevel;
        public Image nitroLevel;
        public Image attractorLevel;
        public Image currentWeaponLevel;
        public Image currentWeaponIcon;

        public void SetValues(float sh, float ni, float att)
        {
            shieldLevel.fillAmount = sh;
            nitroLevel.fillAmount = ni;
            attractorLevel.fillAmount = att;
        }
    }

    public class GameplaySelection : UIController
    {
        public GamePausedDialog gamePausedDialog;
        public GameLoadingDialog gameLoadingDialog;
        public GameResultDialog gameResultDialog;
        public WeaponSelectionDialog weaponSelectionDialog;
        public GameObject mobileScreenJoyStick;
        public DamageUI damageUI;

        public RefillUI refillUI;
        public Image crossHair;

        [Header("Jet Info")]
        public Image speedometerLevel;
        public Image healthLevel;
        // public GameObject damageUI;

        [Header("Buttons")]
        public Button pauseBtn;
        public Button swapWeaponBtn;

        public TMP_Text timeFlight;
        public TMP_Text killCount;


        public void SetDetails(float currentSpeed, float currentHealth)
        {
            speedometerLevel.fillAmount = currentSpeed;
            healthLevel.fillAmount = currentHealth;
        }

        public void SetKill(int num) { killCount.text = num.ToString(); }

        public void SetTime(float time) { timeFlight.text = time.ToString() + " M"; }

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            gameResultDialog.Init(ui);
            mobileScreenJoyStick.SetActive(GameManager.Instance.isMobile);
        }

        protected override void Enable()
        {
            gamePausedDialog.gameObject.SetActive(false);
            gameLoadingDialog.gameObject.SetActive(false);
            gameResultDialog.gameObject.SetActive(false);
            weaponSelectionDialog.gameObject.SetActive(false);

            pauseBtn.onClick.AddListener(Paused);

            KeyInput.Instance.pausedCmd.IsStarted += Paused;

            KeyInput.Instance.swapCmd.IsStarted += () => OpenSwapDialog(true);

            swapWeaponBtn.onClick.AddListener(() => OpenSwapDialog(true));

            damageUI.Enable(false);
        }

        protected override void Disable()
        {
            pauseBtn.onClick.RemoveListener(Paused);
            swapWeaponBtn.onClick.RemoveListener(() => OpenSwapDialog(true));

            weaponSelectionDialog.gameObject.SetActive(false);
        }

        private void Paused()
        {
            if (!GameManager.Instance.isGameRunning) { return; }
            gamePausedDialog.gameObject.SetActive(true);
            GameManager.Instance.isGameRunning = false;
        }

        private void OpenSwapDialog(bool value) { if (!GameManager.Instance.isGameRunning) { return; } weaponSelectionDialog.gameObject.SetActive(value); }


        private void SelectEvent() { }
        private void Quit() { }
    }
}
