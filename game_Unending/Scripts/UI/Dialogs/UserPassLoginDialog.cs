using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine.Playables;


namespace UE
{
    public class UserPassLoginDialog : UIController
    {
        [SerializeField] private TMP_InputField usernameTXT, passwordTXT;
        [SerializeField] private Button loginBtn;
        [SerializeField] private Toggle rememberToggle;
        [SerializeField] private NicknameBarWidget nicknameBarWidget;
        [SerializeField] private TMP_Text error;

        private string nickname, pass;

        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            loginBtn.onClick.AddListener(SignUp);
        }

        protected override void Enable() { loginBtn.enabled = true; error.gameObject.SetActive(false); }

        protected override void Disable() { }

        private void SignUp()
        {
            nickname = usernameTXT.text.Trim().ToLower();
            //pass = passwordTXT.text.Trim();
            pass = "Axoon!@#$%345";

            if(nickname.Length >2){
                GameManager.Instance.unityConnectionManager.SignUp(nickname, pass, ConnectionSuccess, ConnectionFaiure);
            }

            /*if (nickname.Length < 1 || pass.Length < 1) { Debug.Log("called todat"); uiManagerRef.toast.SetToast("Username or password empty"); }
            else
            {
                //loginBtn.enabled = false;
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");

                var isValidated = hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass);
                if (isValidated)
                {
                    //loginBtn.enabled = false;
                    GameManager.Instance.unityConnectionManager.SignUp(nickname, pass, ConnectionSuccess, ConnectionFaiure);
                }
                else { uiManagerRef.toast.SetToast("Password format is incorrect"); error.gameObject.SetActive(true); }


            }*/
        }

        private void ConnectionSuccess()
        {
            gameObject.SetActive(false);
            nicknameBarWidget.SetName(nickname);

            if (rememberToggle.isOn)
            {
                PlayerPrefs.SetString("rememberme", "yes");
                PlayerPrefs.SetString("nickname", nickname);
                PlayerPrefs.SetString("passkey", pass);
                int serverVersion = PlayerPrefs.GetInt("server_version");
                PlayerPrefs.SetInt("app_version", serverVersion);
            }
        }

        private void ConnectionFaiure(string message)
        {
            uiManagerRef.toast.SetToast(message);
            loginBtn.enabled = true;
        }
    }
}
