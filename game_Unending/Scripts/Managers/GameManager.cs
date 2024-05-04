using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using System.Runtime.InteropServices;

namespace UE
{
    public enum GAMEMODE{ FREE, P2E}

    [Serializable]
    public struct DATA
    {
        public float wormHitAmount;
        public float obstacleHitAmount;
    }

    public class GameManager : MonoBehaviour
    {
        public static Action OnGameRateIncreased;

        public void IncreaseGameRate() { OnGameRateIncreased(); currentLevel++; }

        public void GameEventStarted() { currentLevel = 0; }

        public static GameManager Instance { set; get; }
        public DATA scoreData;
        [HideInInspector] public JetProperty chosenJet;
        public PlayerContext playerContext;
        public PlayfabConnectManager playfabConnectManager;
        public Authentication unityConnectionManager;
        public GameEventManager gameEventManager;
        public BulletPoolManager starightBulletPoolManager;
        public BulletPoolManager missileBulletPoolManager;
        public ImpactFXPoolManager impactFXPoolManager;
        public UserData userData;
        public FXManager fXManager;
        public UIManager uIManager;
        public SFXManager sfxManager;

        public int currentLevel = 1;

        public bool isGameRunning;
        public bool isWalletConnected;
        public bool isMobile;
        public Volume globalVolume;
        public UniversalRenderer universalRenderer;
        public UniversalRenderPipelineAsset universalRenderPipelineAsset;
        public CinemachineVirtualCamera cinemachineVirtualCamera;
        public GAMEMODE gameMode;

       // [DllImport("__Internal")]
        //private static extern bool IsMobile();



        private void OnEnable() { }
        private void OnDisable() { }

        private void Awake()
        {
            Instance = this;
            Application.targetFrameRate = 120;
            
        }

        private void Start()
        {
            //isMobile = IsMobileBrowser();
            isMobile = Application.isMobilePlatform;
            GameObject.Find("[Debug Updater]").SetActive(false);
        }
        

        public bool IsMobileBrowser()
        {
            #if !UNITY_EDITOR && UNITY_WEBGL
                return IsMobile();
            #endif
            return false;
        }

    }
}
