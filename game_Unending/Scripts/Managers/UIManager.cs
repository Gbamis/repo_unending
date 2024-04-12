using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using System;

namespace UE
{

    [System.Serializable]
    public struct Toast
    {
        [SerializeField] private TMP_Text toastMsg;
        [SerializeField] private PlayableDirector toastAnim;
        public void SetToast(string msg)
        {
            toastMsg.text = msg;
            toastAnim.Play();

        }
    }


    [System.Serializable]
    public struct MenuBar
    {
        public GameObject root;
        public GameObject infoTab;
        public GameObject bBtn;
        public GameObject line;
        public Button connectWalletButton;
        public Text menuTitle;
        public GameObject blocker;
        readonly public void SetTitle(string ss)
        {
            try { menuTitle.text = ss; }
            catch (System.Exception e) { }
        }
        readonly public void HideBack(bool back)
        {
            bBtn.SetActive(back);
            line.SetActive(back);
        }
    }


    [System.Serializable]
    public struct Navigation
    {
        public int sindex;
        //public int cIndex;
        public string mTitle;
        public bool sMenuBar;
        public bool aPrev;
    }


    public class UIManager : MonoBehaviour
    {

        public Texture2D jetCusor;
        public LayerMask clickLayer;
        public VirtualStick virtualStick;
        public MenuBar menuBar;

        public List<UIController> uiControllers;
        public SettingsDialog settingsDialog;

        [HideInInspector] public EventSelection eventSelection;
        [HideInInspector] public GameplaySelection gameplaySelection;
        [HideInInspector] public ModeSelection ModeSelection;
        [HideInInspector] public JetSelection jetSelection;
        public Toast toast;

        public GameObject titlePanel;

        public bool isLocalLoad;

        public Stack<Navigation> navigations = new Stack<Navigation>();

        private void Start()
        {
            Cursor.SetCursor(jetCusor, Vector2.zero, CursorMode.Auto);
            foreach (UIController uc in uiControllers) { uc.Init(this); }

            ModeSelection = (ModeSelection)uiControllers[0];
            eventSelection = (EventSelection)uiControllers[1];
            jetSelection = (JetSelection)uiControllers[2];
            gameplaySelection = (GameplaySelection)uiControllers[3];

            if (isLocalLoad)
            {
                titlePanel.SetActive(true);
                StartCoroutine(LoadMenu());
                IEnumerator LoadMenu()
                {
                    yield return new WaitForSeconds(3);
                    ActivateUI(0, "MENU", true, false);
                    titlePanel.SetActive(false);
                }
            }
            else
            {
                ActivateUI(0, "", true, false);
            }

            menuBar.connectWalletButton.onClick.AddListener(() =>
            {
                GameManager.Instance.isWalletConnected = !GameManager.Instance.isWalletConnected;
                menuBar.infoTab.SetActive(GameManager.Instance.isWalletConnected);
                menuBar.blocker.SetActive(false);
            });

        }


        public void ActivateUI(int panelIndex, string menuTitle = "", bool menubar = false, bool allowPrev = false, bool isGame = false)
        {
            //if (GameManager.Instance.isWalletConnected) { menuBar.infoTab.SetActive(true); }
            //else { menuBar.infoTab.SetActive(false); }

            menuBar.root.SetActive(menubar);
            menuBar.SetTitle(menuTitle);
            menuBar.HideBack(allowPrev);

            uiControllers[panelIndex].gameObject.SetActive(true);
            for (int i = 0; i < uiControllers.Count; i++)
            {
                if (i == panelIndex)
                {
                    continue;
                }
                uiControllers[i].gameObject.SetActive(false);
            }


            Navigation nav = new Navigation()
            {
                sindex = panelIndex,
                mTitle = menuTitle,
                sMenuBar = menubar,
                aPrev = allowPrev
            };
            if (!navigations.Contains(nav) && !isGame)
            {
                navigations.Push(nav);
            }
        }

        public void BackButton()
        {
            GameManager.Instance.sfxManager.ReturnClickFX();
            Navigation bk;
            if (navigations.Count < 2)
            {
                bk = navigations.Pop();
            }
            else
            {
                bk = navigations.Pop();
                bk = navigations.Pop();
            }
            ActivateUI(bk.sindex, bk.mTitle, bk.sMenuBar, bk.aPrev);
        }

        public void SelectEvent(int index) { eventSelection.SelectEvent(index); }

        public void OnFlyButtonIn(int index)
        {
            switch (index)
            {
                case 0: GameManager.Instance.playerContext.isShieldPressed = true; break;
                case 1: GameManager.Instance.playerContext.isBulletPressed = true; break;
                case 2: GameManager.Instance.playerContext.isAttractorPressed = true; break;
                case 3: GameManager.Instance.playerContext.isNitroPressed = true; break;
            }
        }

        public void OnFlyButtonOut(int index)
        {
            switch (index)
            {
                case 0: GameManager.Instance.playerContext.isShieldPressed = false; break;
                case 1: GameManager.Instance.playerContext.isBulletPressed = false; break;
                case 2: GameManager.Instance.playerContext.isAttractorPressed = false; break;
                case 3: GameManager.Instance.playerContext.isNitroPressed = false; break;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, clickLayer))
                {
                    if (hit.collider != null)
                    {
                        // Debug.Log(hit.collider.name);
                    }
                }
            }

            // if (Input.GetKeyDown(KeyCode.Space)) { keyPressed(KeyCode.Space); }
            // if (Input.GetKeyDown(KeyCode.Return)) { keyPressed(KeyCode.Return); }
            //if (Input.GetKeyDown(KeyCode.Escape)) { keyPressed(KeyCode.Escape); }
        }

    }
}

