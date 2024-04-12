using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{
    [System.Serializable]
    public struct SumaryInfo
    {
        public Image background;
        public Text eventTitle;
        public Text eventID;
        public Text eventSub;
        public Text eventStory;
    }

    public class EventSelection : UIController
    {
        public StakeDialog stakeDialog;
        public SumaryInfo sumaryInfo;
        public GameObject eventDetails;
        public Transform eventListView;
        private List<GameEventInfo> infoList;

        public Button proceedBtn;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            stakeDialog.Init(ui);
        }

        protected override void Enable()
        {
            proceedBtn.onClick.AddListener(SetBtn);
        }

        protected override void Disable()
        {
            proceedBtn.onClick.RemoveListener(SetBtn);
        }

        private void SetBtn()
        {
            if (GameManager.Instance.gameMode == GAMEMODE.FREE)
            {
                uiManagerRef.ActivateUI(2, "JET SELECTION", true, true, false);
            }
            else
            {
                stakeDialog.gameObject.SetActive(true); 
                
            }
            /*if (GameManager.Instance.isWalletConnected)
            {
                stakeDialog.gameObject.SetActive(true);
            }
            else
            {
                uiManagerRef.ActivateUI(2, "JET SELECTION", true, true, false);
            }*/

            GameManager.Instance.sfxManager.ButtonClickFX();
        }

        public void SelectEvent(int index)
        {
            if (infoList[index].isEventLocked) { return; }

            sumaryInfo.background.sprite = infoList[index].campaignBg;
            sumaryInfo.eventTitle.text = infoList[index].campaignTitle;
            sumaryInfo.eventSub.text = infoList[index].campaignSubTitle;
            sumaryInfo.eventID.text = infoList[index].campaingID;
            sumaryInfo.eventStory.text = infoList[index].campaignSummary;


            GameManager.Instance.gameEventManager.SetCurrentEvent(index);
        }
        public void CreateEventItems(List<GameEventInfo> infos)
        {
            infoList = infos;
            int index = 0;
            foreach (GameEventInfo info in infos)
            {
                GameObject uiClone = Instantiate(eventDetails);
                uiClone.transform.SetParent(eventListView);
                uiClone.GetComponent<RectTransform>().localScale = Vector3.one;
                UIClick uIClick = uiClone.GetComponent<UIClick>();
                uIClick.SetDetails(info.campaignthumbnail, index, info.campaignTitle, info.isEventLocked);
                index++;
            }
            SelectEvent(0);

        }
    }
}
