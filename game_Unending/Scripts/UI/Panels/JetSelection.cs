using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
namespace UE
{
    [System.Serializable]
    public struct JetsInfo
    {
        public Text jetnameUI;
        public TMP_Text maxSpeedUI, accelerationUI, handlingUI, healthUI, nitroUI, shieldUI, attractorUI;
        public Image maxSpeedLevel, accelerationLevel, handlingLevel, healthLevel, nitroLevel, shieldLevel, attractorLevel;

        public void SetInfo(JetProperty p)
        {
            jetnameUI.text = p.JetName.ToUpper();
            maxSpeedUI.text = (p.flightDetails.maxSpeed / 10).ToString("f1");
            accelerationUI.text = p.flightDetails.acceleration.ToString("f1");
            handlingUI.text = (p.flightDetails.steering / 10).ToString("f1");
            nitroUI.text = (p.refillDetails.nitro.refilRate * 10).ToString("f1");
            shieldUI.text = (p.refillDetails.shield.refilRate * 10).ToString("f1");
            attractorUI.text = (p.refillDetails.attractor.refilRate * 10).ToString("f1");
            healthUI.text = (p.flightDetails.maxHealth / 100).ToString("f1");

            maxSpeedLevel.fillAmount = p.flightDetails.maxSpeed / 100;
            accelerationLevel.fillAmount = p.flightDetails.acceleration / 10;
            handlingLevel.fillAmount = p.flightDetails.steering / 100;
            nitroLevel.fillAmount = p.refillDetails.nitro.refilRate;
            shieldLevel.fillAmount = p.refillDetails.shield.refilRate;
            attractorLevel.fillAmount = p.refillDetails.attractor.refilRate;
            healthLevel.fillAmount = p.flightDetails.maxHealth / 100;
        }
    }

    public class JetSelection : UIController
    {
        public GameObject mainCam;
        public GameObject selectionCam;
        public CinemachineFreeLook selectionVirtualCam;
        public Transform jetListView;
        public float orbitSpeed;
        public GameObject jetItemUI;
        public GameObject jetCanvas;
        public Image jetBG;
        public JetsInfo jetsInfo;

        public Transform jetParent;
        public Transform jetProfileOrigin;

        public Button startBtn;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
        }

        protected override void Enable()
        {
            //Activate(true);
            PopulateJets();
            startBtn.onClick.AddListener(StartRace);
            SelectJet(0);
            //StartCoroutine(Orbit());
        }

        protected override void Disable()
        {
            Activate(false);
            startBtn.onClick.RemoveListener(StartRace);
            // StopCoroutine(Orbit());
        }
        public void Activate(bool value)
        {
            mainCam.SetActive(!value);
            selectionVirtualCam.gameObject.SetActive(value);
            selectionCam.SetActive(value);
            jetCanvas.SetActive(value);
        }

        private IEnumerator Orbit()
        {
            while (gameObject.activeSelf)
            {
                selectionVirtualCam.m_XAxis.Value += Time.deltaTime * orbitSpeed;
                yield return null;
            }
        }

        private void PopulateJets()
        {
            if (jetParent.childCount > 0)
            {
                foreach (Transform child in jetParent)
                {
                    child.position = jetProfileOrigin.position;
                    child.GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
                    JetProperty prop = (JetProperty)child.GetComponent<Payer>().properties;
                    CreateItem(prop);
                }
            }
        }

        private void CreateItem(JetProperty p)
        {
            if (jetListView.childCount < jetParent.childCount)
            {
                GameObject clone = Instantiate(jetItemUI);
                clone.GetComponent<JetItemUI>().thumbnail.sprite = p.thumbnail;
                clone.transform.SetParent(jetListView);
                clone.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }

        }

        private void StartRace()
        {
            uiManagerRef.ActivateUI(3, "GAME", false, false, true);
            GameManager.Instance.gameEventManager.BeginEvent();
            gameObject.SetActive(false);
        }

        public void SelectJet(int index)
        {
            jetParent.GetChild(index).gameObject.SetActive(true);
            for (int i = 0; i < jetParent.childCount; i++)
            {
                if (i == index) { continue; }
                jetParent.GetChild(i).gameObject.SetActive(false);
            }
            JetProperty property = jetParent.GetChild(index).GetComponent<Payer>().properties;
            jetsInfo.SetInfo(property);
            GameManager.Instance.chosenJet = property;
            jetBG.sprite = property.backgroundImage;
        }
    }
}
