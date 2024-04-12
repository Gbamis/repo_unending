using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UE
{
    public class GameResultDialog : UIController
    {
        public Button returnToMenu;
        public TMP_Text distanceTxt, jetKilledText, xpsText, aptText;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
        }

        protected override void Enable()
        {
            returnToMenu.onClick.AddListener(Menu);
            int dis = GameManager.Instance.gameEventManager.currentGameEvent.outcome.distance;
            int jet = GameManager.Instance.gameEventManager.currentGameEvent.outcome.enemyKill;
            SetResult(dis, jet, 0, 0);
            GameManager.Instance.unityConnectionManager.UpdateTimeTravelStat(dis);
           // GameManager.Instance.playfabConnectManager.UpdateEnemyKillStat(jet);
           // GameManager.Instance.playfabConnectManager.UpdateTotalKillStat(jet);
           // GameManager.Instance.playfabConnectManager.UpdateTimeTravelled(dis);
        }

        protected override void Disable()
        {
            returnToMenu.onClick.RemoveListener(Menu);
        }
        private void Menu()
        {
            gameObject.SetActive(false);
            uiManagerRef.ActivateUI(1, "CAMPAIGN SELECTION", true, true);
            GameManager.Instance.sfxManager.ButtonClickFX();
        }

        public void SetResult(int dis, int jet, int xp, float apt)
        {
            distanceTxt.text = dis.ToString() + "(uEs)";
            jetKilledText.text = jet.ToString();

            gameObject.SetActive(true);
        }
    }
}
