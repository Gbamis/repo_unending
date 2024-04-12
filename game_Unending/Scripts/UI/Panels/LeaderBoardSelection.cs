using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UE
{
    public class LeaderBoardSelection : UIController
    {

        [SerializeField] private LeaderBoardRowItem rowItemPrefab;
        [SerializeField] private Transform listview;
        [SerializeField] private Color mineColor, otherColor;
        private RectTransform listRect;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            listRect = listview.GetComponent<RectTransform>();
        }

        protected override void Enable()
        {
            if (listview.childCount > 0) { foreach (Transform child in listview) { Destroy(child.gameObject); } }
            //GameManager.Instance.playfabConnectManager.GetLeaderBoard(CreateItems);
            bool connected = GameManager.Instance.unityConnectionManager.IsSignedInAlready();
            if (connected) { GameManager.Instance.unityConnectionManager.GetLeaderBoard(CreateItems); }

        }

        private void CreateItems(List<UEBoard> board)
        {
            foreach (UEBoard ue in board)
            {
                LeaderBoardRowItem row = Instantiate(rowItemPrefab);
                Color col = (ue.isSelf) ? mineColor : otherColor;
                row.SetData(ue.position+1, ue.displayName, ue.enemyKills, ue.totalKill, ue.distTraveled, col);
                row.transform.SetParent(listview);
                row.GetComponent<RectTransform>().localScale = Vector3.one;
                row.gameObject.SetActive(true);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(listRect);
        }

        protected override void Disable()
        {

        }
    }
}
