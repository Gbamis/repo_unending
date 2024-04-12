using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UE
{
    public class GameLoadingDialog : UIController
    {
        public Image progressBar;
        public TMP_Text pcHelper;

        public override void Init(UIManager ui) { uiManagerRef = ui; }

        protected override void Enable() {
            pcHelper.enabled = !GameManager.Instance.isMobile;
         }

        protected override void Disable() { }
    }
}
