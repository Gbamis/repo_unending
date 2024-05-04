using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

namespace UE
{
    public class Authentication : MonoBehaviour
    {
        [HideInInspector] public List<UEBoard> leaderboard = new List<UEBoard>();
        public int rawE, rawT, rawET;
        private string cachedName;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
            //PlayerPrefs.SetInt("app_version", 1);
            //SetupEvents();

        }



        public bool IsReturningUser() { return AuthenticationService.Instance.SessionTokenExists; }

        public bool IsNewVersion()
        {
            int appVersion = PlayerPrefs.GetInt("app_version");
            int serverVersion = PlayerPrefs.GetInt("server_version");
            // Debug.Log(appVersion + "  " + serverVersion);
            return (appVersion == serverVersion);
        }

        public bool IsSignedInAlready() { return AuthenticationService.Instance.IsSignedIn; }

        public async void DeleteAccount()
        {
            try
            {
                await AuthenticationService.Instance.DeleteAccountAsync();
                Debug.Log("account deleted");
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        public void SignOut()
        {
            AuthenticationService.Instance.SignOut();
        }

        public async void SignUp(string nickname, string password, Action suceessCallback, Action<string> failedCalledback)
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(nickname, password);
                await AuthenticationService.Instance.UpdatePlayerNameAsync(nickname);
                suceessCallback();
                Debug.Log("sign up");
            }


            catch (RequestFailedException ex)
            {
                SignIn(nickname, password, suceessCallback, failedCalledback);
                GameManager.Instance.uIManager.toast.SetToast(ex.Message);
            }
        }

        public async void SignIn(string nickname, string password, Action suceessCallback, Action<string> failedCalledback)
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(nickname, password);
                cachedName = nickname;
                //UpdateEnemyKillStat(150); UpdateTimeTravelStat(750); UpdateTotalKillStat(220);
                UpdateEnemyKillStat(rawE); UpdateTimeTravelStat(rawT); UpdateTotalKillStat(rawET);
                suceessCallback();
            }
            catch (AuthenticationException ex)
            {
                string res = ex.Message;
                failedCalledback(res);
                Debug.Log(ex.Message);
            }
            catch (RequestFailedException ex)
            {
                string res = ex.Message;
                failedCalledback(res);
                Debug.Log(ex.Message);
            }
        }

        public async void UpdateEnemyKillStat(int value)
        {
            try { await LeaderboardsService.Instance.AddPlayerScoreAsync("EnemyKills", value); }
            catch (RequestFailedException ex)
            {
                GameManager.Instance.uIManager.toast.SetToast("Network Connection Failed");
            }

        }
        public async void UpdateTotalKillStat(int value)
        {
            try { await LeaderboardsService.Instance.AddPlayerScoreAsync("TotalKills", value); }
            catch (RequestFailedException ex)
            {
                GameManager.Instance.uIManager.toast.SetToast("Network Connection Failed");
            }

        }
        public async void UpdateTimeTravelStat(int value)
        {
            try { await LeaderboardsService.Instance.AddPlayerScoreAsync("TotalTime", value); }
            catch (RequestFailedException ex)
            {
                GameManager.Instance.uIManager.toast.SetToast("Network Connection Failed");
            }
        }

        public async void GetLeaderBoard(Action<List<UEBoard>> action)
        {
            try
            {
                leaderboard.Clear();
                LeaderboardScoresPage page = await LeaderboardsService.Instance.GetScoresAsync("EnemyKills");
                List<LeaderboardEntry> leaderboardEntry = page.Results;
                Debug.Log("enemy...");
                foreach (LeaderboardEntry le in leaderboardEntry)
                {
                    string name = le.PlayerName.Remove(le.PlayerName.Length - 5);
                    leaderboard.Add(new UEBoard()
                    {
                        displayName = name,
                        enemyKills = (int)le.Score,
                        position = le.Rank,
                        isSelf = (name.ToLower() == cachedName.ToLower()) ? true : false
                    });
                }

                LeaderboardScoresPage page2 = await LeaderboardsService.Instance.GetScoresAsync("TotalKills");
                List<LeaderboardEntry> le2 = page2.Results;

                foreach (LeaderboardEntry le in le2)
                {
                    string name = le.PlayerName.Remove(le.PlayerName.Length - 5);
                    foreach (UEBoard ue in leaderboard)
                    {
                        if (name == ue.displayName)
                        {
                            ue.totalKill = (int)le.Score;
                        }
                    }
                }

                LeaderboardScoresPage page3 = await LeaderboardsService.Instance.GetScoresAsync("TotalTime");
                List<LeaderboardEntry> le3 = page3.Results;

                foreach (LeaderboardEntry le in le3)
                {
                    string name = le.PlayerName.Remove(le.PlayerName.Length - 5);
                    foreach (UEBoard ue in leaderboard)
                    {
                        if (name == ue.displayName)
                        {
                            ue.distTraveled = (int)le.Score;
                        }
                    }
                }
                action(leaderboard);
            }
            catch (RequestFailedException ex)
            {
                GameManager.Instance.uIManager.toast.SetToast("Network Connection Failed");
            }

        }

        private void SetupEvents()
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
                Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
            };

            AuthenticationService.Instance.SignInFailed += (err) => { Debug.LogError(err); };

            AuthenticationService.Instance.SignedOut += () => { Debug.Log("Player signed out."); };

            AuthenticationService.Instance.Expired += () => { Debug.Log("Player session could not be refreshed and expired."); };
        }
    }
}
