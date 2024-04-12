using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UE
{
    public class GameEventManager : MonoBehaviour
    {
        public GameEvent currentGameEvent;
        public List<GameEvent> gameEvents = new List<GameEvent>();
        public List<GameEventInfo> gameEventInfos = new List<GameEventInfo>();

        private void Start()
        {
            EventInfoLoaded();
        }
        
        public void EventInfoLoaded()
        {
            foreach (GameEvent ge in gameEvents)
            {
                gameEventInfos.Add(ge.eventConfig);
                ge.gameObject.SetActive(false);
            }
            GameManager.Instance.uIManager.eventSelection.CreateEventItems(gameEventInfos);
        }

        public void BeginEvent()
        {
            GameManager.Instance.cinemachineVirtualCamera.m_Follow = GameManager.Instance.playerContext.playerCamTarget;
            GameManager.Instance.cinemachineVirtualCamera.m_LookAt = GameManager.Instance.playerContext.playerCamTarget;
            StartCoroutine(CountDownToStart());
            currentGameEvent.PlayEventAduio();
        }
        public void SetCurrentEvent(int index)
        {
            currentGameEvent = transform.GetChild(index).GetComponent<GameEvent>();
            foreach(Transform child in transform){
                child.gameObject.SetActive(false);
            }
            currentGameEvent.gameObject.SetActive(true);
        }


        private IEnumerator CountDownToStart()
        {
            GameManager.Instance.uIManager.gameplaySelection.gameLoadingDialog.gameObject.SetActive(true);
            float count = 0;
            while (count < 6)
            {
                GameManager.Instance.uIManager.gameplaySelection.gameLoadingDialog.progressBar.fillAmount = (count / 6);
                count += Time.deltaTime;
                yield return null;
            }
            currentGameEvent.StartGameEvent();
            GameManager.Instance.uIManager.gameplaySelection.gameLoadingDialog.gameObject.SetActive(false);
            GameManager.Instance.GameEventStarted();
        }
    }
}
