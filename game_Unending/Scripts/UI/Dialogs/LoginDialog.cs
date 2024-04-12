using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UE
{
    public class LoginDialog : UIController
    {
        [SerializeField] private NicknameBarWidget nicknameBarWidget;

        [SerializeField] private Button continueBtn;
        public Button leaderBoardBtn;

        [SerializeField] private TMP_InputField nickInput;

        public override void Init(UIManager ui) { uiManagerRef = ui; }

        protected override void Enable() { continueBtn.onClick.AddListener(ConnectDevice); }

        protected override void Disable() { }

        private void ConnectDevice()
        {
            string value = nickInput.text.ToLower().Trim();
            if (value.Length == 0) { return; }
            GameManager.Instance.playfabConnectManager.UpdateDisplayName(value, gameObject, nicknameBarWidget, () => { leaderBoardBtn.enabled = true; });
        }
    }
}
