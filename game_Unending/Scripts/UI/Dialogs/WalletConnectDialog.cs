using System.Collections;
using System.Collections.Generic;
using UE;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{
    public class WalletConnectDialog : UIController
    {
        public Button closeBtn;
        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
        }

        protected override void Enable()
        {
            Debug.Log(name + " enabled");
            closeBtn.onClick.AddListener(SetCloseBtn);
        }

        protected override void Disable()
        {
            Debug.Log(name + " disabled");
            closeBtn.onClick.RemoveListener(SetCloseBtn);
        }

        private void SetCloseBtn()
        {
            gameObject.SetActive(false);
            GameManager.Instance.sfxManager.ReturnClickFX();
        }
    }
}
