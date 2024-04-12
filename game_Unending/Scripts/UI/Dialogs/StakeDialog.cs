using System.Collections;
using System.Collections.Generic;
using UE;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{

    public class StakeDialog : UIController
    {
        public Button closeBtn;
        public Button pickJetBtn;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
        }

        protected override void Enable()
        {
            pickJetBtn.onClick.AddListener(SelectJet);
            closeBtn.onClick.AddListener(SetCloseBtn);
        }

        protected override void Disable()
        {
            pickJetBtn.onClick.AddListener(SelectJet);
            closeBtn.onClick.RemoveListener(SetCloseBtn);
        }

        private void SelectJet()
        {
            uiManagerRef.ActivateUI(2, "JET SELECTION", true, true, false);
            gameObject.SetActive(false);
            GameManager.Instance.sfxManager.SelectedItemClickFX();
        }

        private void SetCloseBtn()
        {
            gameObject.SetActive(false);
            GameManager.Instance.sfxManager.ReturnClickFX();
        }
    }
}
