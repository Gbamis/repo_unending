using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{
    public class ModeSelection : UIController
    {
        public WalletConnectDialog walletConnectDialog;

        public Button freePlayBtn;
        public Button playEarnBtn;
        public Button leaderBoardBtn;
        public Button settingsBtn;
        public Button closeBlockerBtn;
        public GameObject blocker;

        [Header("Credit Panel")]
        public Button creditBtn;
        public Button closeCreditBtn;
        [SerializeField] private GameObject creditPane;


        [Header("Terms")]
        public Button termsBtn;
        public Button closeTermsBtn;
        [SerializeField] private GameObject termsPane;

        [SerializeField] private NicknameBarWidget nicknameBarWidget;
        [SerializeField] private LoginDialog loginDialog;
        [SerializeField] private UserPassLoginDialog userPassLoginDialog;



        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            loginDialog.Init(uiManagerRef);
            userPassLoginDialog.Init(uiManagerRef);
        }

        public void OpenUP() { userPassLoginDialog.gameObject.SetActive(true); }

        protected override void Enable()
        {
            freePlayBtn.onClick.AddListener(FreeClicked);
            playEarnBtn.onClick.AddListener(PlayEarn);
            leaderBoardBtn.onClick.AddListener(LeaderBoardClicked);
            nicknameBarWidget.gameObject.SetActive(true);
            settingsBtn.onClick.AddListener(() => { uiManagerRef.settingsDialog.gameObject.SetActive(true); });
            closeBlockerBtn.onClick.AddListener(() => { blocker.SetActive(false); });

            int acceptedTermss = PlayerPrefs.GetInt("acceptedTerms", 0);
            if (acceptedTermss == 0) { termsPane.SetActive(true); }
            else { termsPane.SetActive(false); }

            termsBtn.onClick.AddListener(() => { termsPane.SetActive(true); });
            closeTermsBtn.onClick.AddListener(() =>
            {
                termsPane.SetActive(false); PlayerPrefs.SetInt("acceptedTerms", 1);
            });

            creditBtn.onClick.AddListener(() => { creditPane.SetActive(true); });
            closeCreditBtn.onClick.AddListener(() => { creditPane.SetActive(false); });


            bool activeSession = GameManager.Instance.unityConnectionManager.IsSignedInAlready();
            //bool newVersion = GameManager.Instance.unityConnectionManager.IsNewVersion();

            if (!activeSession)
            {
                string name = PlayerPrefs.GetString("nickname");
                string pass = PlayerPrefs.GetString("passkey");

                //string name = "East";
                //string pass = "Kunmykel1";

                if (name.Trim().Length != 0 && pass.Trim().Length != 0)
                {
                    nicknameBarWidget.SetName(name);
                    GameManager.Instance.unityConnectionManager.SignIn(name, pass, Good, Bad);
                }
                else
                {
                    userPassLoginDialog.gameObject.SetActive(true);
                }
            }

        }

        private void Good()
        {
            userPassLoginDialog.gameObject.SetActive(false);
        }
        private void Bad(string v)
        {
            userPassLoginDialog.gameObject.SetActive(true);
        }
        protected override void Disable()
        {
            freePlayBtn.onClick.RemoveListener(FreeClicked);
            playEarnBtn.onClick.RemoveListener(PlayEarn);
            leaderBoardBtn.onClick.RemoveListener(LeaderBoardClicked);
            nicknameBarWidget.gameObject.SetActive(false);
            settingsBtn.onClick.RemoveAllListeners();
            termsBtn.onClick.RemoveAllListeners();
            closeTermsBtn.onClick.RemoveAllListeners();
            creditBtn.onClick.RemoveAllListeners();
            closeCreditBtn.onClick.RemoveAllListeners();
        }

        private void FreeClicked()
        {
            uiManagerRef.ActivateUI(1, "CAMPAIGN SELECTION", true, true);
            GameManager.Instance.sfxManager.ButtonClickFX();
            GameManager.Instance.gameMode = GAMEMODE.FREE;
        }

        private void PlayEarn()
        {
            if (!GameManager.Instance.isWalletConnected)
            {
                blocker.SetActive(true);
            }
            else
            {
                GameManager.Instance.gameMode = GAMEMODE.P2E;
                uiManagerRef.ActivateUI(1, "CAMPAIGN SELECTION", true, true);
                GameManager.Instance.sfxManager.ButtonClickFX();
            }

        }

        private void LeaderBoardClicked() { uiManagerRef.ActivateUI(4, "LEADERBOARD", true, true); GameManager.Instance.sfxManager.ButtonClickFX(); }
    }
}
