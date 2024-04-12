using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UE
{
    public class LeaderBoardRowItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text positionTxt;
        [SerializeField] private TMP_Text displayNameTxt;
        [SerializeField] private TMP_Text bestKillTxt;
        [SerializeField] private TMP_Text totalKillTxt;
        [SerializeField] private TMP_Text distanceTxt;

        [SerializeField] private Image rowBg,rowChip;

        public void SetData(int pos, string name, int bestKill, int totalKill, int distance,Color color)
        {
            positionTxt.text = pos.ToString();
            displayNameTxt.text = name;
            bestKillTxt.text = bestKill.ToString();
            totalKillTxt.text = totalKill.ToString();
            distanceTxt.text = distance.ToString();
            rowBg.color= rowChip.color = color;
        }
    }
}
