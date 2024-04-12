using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace UE
{
    public class PushNotificationManager : MonoBehaviour
    {
        /*public string pushToken;
        public string playFabId;
        public string lastMsg;


        private void OnPfFail(PlayFabError error)
        {
            Debug.Log("PlayFab: api error: " + error.GenerateErrorReport());
        }

        public void Start()
        {
            // PlayFabSettings.TitleId = "TITLE_ID";
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            RegisterForPush();
        }

        private void RegisterForPush()
        {
            if (string.IsNullOrEmpty(pushToken) || string.IsNullOrEmpty(playFabId))
                return;

#if UNITY_ANDROID
            var request = new AndroidDevicePushNotificationRegistrationRequest
            {
                DeviceToken = pushToken,
                SendPushNotificationConfirmation = true,
                ConfirmationMessage = "Push notifications registered successfully"
            };
            PlayFabClientAPI.AndroidDevicePushNotificationRegistration(request, OnPfAndroidReg, OnPfFail);
#endif
        }

        private void OnPfAndroidReg(AndroidDevicePushNotificationRegistrationResult result)
        {
            Debug.Log("PlayFab: Push Registration Successful");
        }

        private void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            Debug.Log("PlayFab: Received Registration Token: " + token.Token);
            pushToken = token.Token;
            RegisterForPush();
        }

        private void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            Debug.Log("PlayFab: Received a new message from: " + e.Message.From);
            lastMsg = "";
            if (e.Message.Data != null)
            {
                lastMsg += "DATA: " + e.Message.Data + "\n";
                Debug.Log("PlayFab: Received a message with data:");
                foreach (var pair in e.Message.Data)
                    Debug.Log("PlayFab data element: " + pair.Key + "," + pair.Value);
            }
            if (e.Message.Notification != null)
            {
                Debug.Log("PlayFab: Received a notification:");
                lastMsg += "TITLE: " + e.Message.Notification.Title + "\n";
                lastMsg += "BODY: " + e.Message.Notification.Body + "\n";
            }
        }*/
    }
}
