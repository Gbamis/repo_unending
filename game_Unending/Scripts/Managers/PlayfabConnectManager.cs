using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace UE
{

    [System.Serializable]
    public class UEBoard
    {
        public int position;
        public string displayName;
        public int enemyKills;
        public int totalKill;
        public int distTraveled;
        public bool isSelf;
    }

    public class PlayfabConnectManager : MonoBehaviour
    {
        public bool clear;

        public List<UEBoard> leaderboard = new List<UEBoard>();
        private int isDeviceAlreadyUsed;

        private void Awake()
        {
            Logout();
            isDeviceAlreadyUsed = PlayerPrefs.GetInt("isDeviceConnected", 0);
        }

        public bool IsDeviceOnceConnected()
        {
            isDeviceAlreadyUsed = PlayerPrefs.GetInt("isDeviceConnected");
            if (isDeviceAlreadyUsed == 0) { return false; }
            return true;
        }

        public void Logout()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            if (clear) { PlayerPrefs.DeleteAll(); }

            Login();
        }

        private void Login()
        {
            var login = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = SystemInfo.deviceUniqueIdentifier,
                PlayerSecret = ""
            };

            PlayFabClientAPI.LoginWithCustomID(login,
             result =>
             {
                 Debug.Log("logged in" + SystemInfo.deviceUniqueIdentifier);
                 if (IsDeviceOnceConnected())
                 {
                     string nickname = PlayerPrefs.GetString("nickname");
                     UpdateDisplayName(nickname);
                 }
             },
             error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void UpdateDisplayName(string value)
        {
            var displayName = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = value
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(displayName,
            reseult => { Debug.Log("updated display name"); },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void UpdateDisplayName(string value, GameObject loginDialog, NicknameBarWidget nw, Action action)
        {
            var displayName = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = value
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(displayName,
            reseult =>
            {
                Debug.Log("updated display name");
                PlayerPrefs.SetInt("isDeviceConnected", 1);
                PlayerPrefs.SetString("nickname", value);
                loginDialog.gameObject.SetActive(false);
                nw.gameObject.SetActive(true);
            },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void UpdateEnemyKillStat(int value)
        {
            var killStat = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>(){
                    new StatisticUpdate(){
                        StatisticName = "EnemyKill",
                        Value = value
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(killStat,
            result =>
            {
                Debug.Log("enemy kill updated");

            },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void UpdateTotalKillStat(int value)
        {
            var killStat = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>(){
                    new StatisticUpdate(){
                        StatisticName = "TotalKill",
                        Value = value
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(killStat,
            result =>
            {
                Debug.Log("Total kill updated");

            },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void UpdateTimeTravelled(int time)
        {
            var dis = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>(){
                    new StatisticUpdate(){
                        StatisticName = "DistanceTraveled",
                        Value = time
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(dis,
            result =>
            {
                Debug.Log("distance updated");
            },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

        public void GetLeaderBoard(Action<List<UEBoard>> action)
        {
            leaderboard.Clear();
            var enemy = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "EnemyKill"
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(enemy,
            result =>
            {
                foreach (PlayerLeaderboardEntry pe in result.Leaderboard)
                {
                    string name = PlayerPrefs.GetString("nickname");
                    Debug.Log("name: " + name + "server name :" + pe.DisplayName);

                    leaderboard.Add(new UEBoard()
                    {
                        displayName = pe.DisplayName,
                        position = pe.Position,
                        enemyKills = pe.StatValue,
                        isSelf = (name == pe.DisplayName) ? true : false

                    });
                }
                /////////////////////////////////////
                var distance = new GetLeaderboardAroundPlayerRequest
                {
                    StatisticName = "TotalKill"
                };
                PlayFabClientAPI.GetLeaderboardAroundPlayer(distance,
                result =>
                {
                    foreach (PlayerLeaderboardEntry pe in result.Leaderboard)
                    {
                        foreach (UEBoard ue in leaderboard)
                        {
                            if (ue.displayName == pe.DisplayName)
                            {
                                ue.totalKill = pe.StatValue;
                            }
                        }
                    }

                    Debug.Log("count: " + leaderboard.Count);
                    /////////////////////////////////////
                    var distance = new GetLeaderboardAroundPlayerRequest
                    {
                        StatisticName = "DistanceTraveled"
                    };
                    PlayFabClientAPI.GetLeaderboardAroundPlayer(distance,
                    result =>
                    {
                        foreach (PlayerLeaderboardEntry pe in result.Leaderboard)
                        {
                            foreach (UEBoard ue in leaderboard)
                            {
                                if (ue.displayName == pe.DisplayName)
                                {
                                    ue.distTraveled = pe.StatValue;
                                }
                            }
                        }

                        Debug.Log("count: " + leaderboard.Count);
                        action(leaderboard);


                    },
                    error => { Debug.LogError(error.GenerateErrorReport()); }
                    );
                    //////////////////////////////////
                },
                error => { Debug.LogError(error.GenerateErrorReport()); }
                );
                ////////////////////////////////// 



            },
            error => { Debug.LogError(error.GenerateErrorReport()); }
            );
        }

    }
}
