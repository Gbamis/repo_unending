using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class UIBase : MonoBehaviour { }

    public abstract class UIController : MonoBehaviour
    {
        protected UIManager uiManagerRef;
        private void OnEnable() { Enable(); }
        private void OnDisable() { Disable(); }

        public abstract void Init(UIManager ui);
        protected abstract void Enable();
        protected abstract void Disable();
    }
}
