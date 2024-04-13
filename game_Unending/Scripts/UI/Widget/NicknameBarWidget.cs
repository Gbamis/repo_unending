using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UE
{
    public class NicknameBarWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text nicknameTxt;
        [SerializeField] private Button sigOutBtn;
        [SerializeField] private Button deleteBtn;

        private void OnEnable()
        {
            sigOutBtn.onClick.AddListener(() =>
            {
                InvalidateSavedUser();
                GameManager.Instance.unityConnectionManager.SignOut();
                GameManager.Instance.uIManager.ModeSelection.OpenUP();
            });

            deleteBtn.onClick.AddListener(() =>
            {
                InvalidateSavedUser();
                GameManager.Instance.unityConnectionManager.DeleteAccount();
            });
        }
        public void SetName(string value) { nicknameTxt.text = value; }
        private void InvalidateSavedUser()
        {
            PlayerPrefs.DeleteKey("rememberme");
            PlayerPrefs.DeleteKey("nickname");
            PlayerPrefs.DeleteKey("passkey");
        }
    }
}
