using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{
    public class GamePausedDialog : UIController
    {
        public Button resumeBtn;
        public Button settingsBtn;
        public Button quitToMenuBtn;
        public RectTransform highlight;
        private RectTransform resumeRect, settingsRect, quitMenuRect;

        public GameObject keyRebindUI;

        public override void Init(UIManager ui) { uiManagerRef = ui; }

        protected override void Enable()
        {
            resumeBtn.onClick.AddListener(Resume);
            settingsBtn.onClick.AddListener(Settings);
            quitToMenuBtn.onClick.AddListener(QuitToMenu);

            resumeRect = resumeBtn.GetComponent<RectTransform>();
            settingsRect = settingsBtn.GetComponent<RectTransform>();
            quitMenuRect = quitToMenuBtn.GetComponent<RectTransform>();

            if(!GameManager.Instance.isMobile){ keyRebindUI.SetActive(true);}
            else{ keyRebindUI.SetActive(false);}

        }

        protected override void Disable()
        {
            resumeBtn.onClick.RemoveListener(Resume);
            settingsBtn.onClick.RemoveListener(Settings);
            quitToMenuBtn.onClick.RemoveListener(QuitToMenu);
        }

        private void Resume()
        {
            if (GameManager.Instance.isGameRunning) { return; }
            highlight.position = resumeRect.position;
            gameObject.SetActive(false);
            GameManager.Instance.isGameRunning = true;

        }
        private void Settings()
        {
            highlight.position = settingsRect.position;
            uiManagerRef.settingsDialog.gameObject.SetActive(true);

        }
        private void QuitToMenu()
        {
            highlight.position = quitMenuRect.position;
            GameManager.Instance.gameEventManager.currentGameEvent.QuitEvent();
            gameObject.SetActive(false);
            //uiManagerRef.ActivateUI(1, "CAMPAIGN SELECTION", true, true);

        }
    }
}
