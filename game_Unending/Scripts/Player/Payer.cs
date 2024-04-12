using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace UE
{
    [System.Serializable]
    public struct PlayerContext
    {
        [HideInInspector] public Transform player;
        public List<Transform> enemySpawnPoint;
        [HideInInspector] public Transform playerCamTarget;
        [HideInInspector] public float playerSpeed;
        [HideInInspector] public Rigidbody playerBody;
        [HideInInspector] public JetProperty properties;

        [Header("Input")]
        [HideInInspector] public bool isShieldPressed;
        [HideInInspector] public bool isBulletPressed;
        [HideInInspector] public bool isNitroPressed;
        [HideInInspector] public bool isAttractorPressed;

        public Transform rayOrigin;
        public float attractionForce;
        public float attractionRange;
        [HideInInspector] public bool isAttractionMode;

        [Header("FX ")]

        public List<ParticleSystem> nuzzleFlashes;
        public List<ParticleSystem> exhaustFX;
        public Transform fxRoot;
        [HideInInspector] public ParticleSystem shieldFX;
        [HideInInspector] public ParticleSystem healthFX;
    }


    public class Payer : MonoBehaviour
    {
        public Transform camTarget;
        public JetProperty properties;
        public PlayerContext playerContext;

        private void Awake()
        {
            playerContext.shieldFX = GameManager.Instance.fXManager.GetShieldPrefab();
            playerContext.shieldFX.transform.SetParent(playerContext.fxRoot);
            playerContext.shieldFX.transform.position = playerContext.fxRoot.position;

            playerContext.healthFX = GameManager.Instance.fXManager.GetHealthFXPrefab();
            playerContext.healthFX.transform.SetParent(playerContext.fxRoot);
            playerContext.healthFX.transform.position = playerContext.fxRoot.position;
        }


        private void OnEnable()
        {
            //properties.OnPropertyInit();

            playerContext.playerBody = GetComponentInChildren<Rigidbody>();
            playerContext.player = transform;
            playerContext.playerCamTarget = camTarget;
            playerContext.properties = properties;
            GameManager.Instance.playerContext = playerContext;
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
            foreach (ParticleSystem ps in playerContext.exhaustFX) { ps.Play(); }

            GameManager.OnGameRateIncreased += () => { properties.flightDetails.flySpeed += 2; };
        }
        private void OnDisbale()
        {
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
            GameManager.OnGameRateIncreased -= () => { };
        }


        private void Update()
        {
            if (!GameManager.Instance.isGameRunning) { return; }
            properties.OnPropertyProcess(playerContext.player);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CustomIsTag("triggernextterrain")) { GameManager.Instance.gameEventManager.currentGameEvent.ActivateNextTile(); }
            if (other.gameObject.CustomIsTag("terraindespawn")) { GameManager.Instance.gameEventManager.currentGameEvent.DeActivateLastTile(); }
            if (other.gameObject.CustomIsTag("Obstacle")) { playerContext.properties.flightDetails.JetHit(GameManager.Instance.scoreData.obstacleHitAmount); }
            if (other.gameObject.CustomIsTag("Orb"))
            {
                playerContext.properties.flightDetails.BoostHealth(); Destroy(other.gameObject);
                playerContext.healthFX.gameObject.SetActive(true);
                GameManager.Instance.sfxManager.HealthAdded();
            }

            try
            {
                if (other.transform.parent.gameObject.CustomIsTag("Bullet"))
                {
                    Bullet bullet = other.transform.parent.gameObject.GetComponent<Bullet>();
                    if (bullet.owner == OWNER.ENEMY) { playerContext.properties.flightDetails.JetHit(bullet.takeDamge); }
                }
            }
            catch (System.Exception e) { }

            if (other.gameObject.CustomIsTag("Snow"))
            {
                GameObject snowHit = GameManager.Instance.fXManager.snowHitFXPool.GetFX();
                if (snowHit != null)
                {
                    snowHit.transform.position = transform.position;
                    snowHit.transform.SetParent(transform);
                    snowHit.gameObject.SetActive(true);
                }

                //Handheld.Vibrate();
                GameManager.Instance.sfxManager.GameDustFX();
            }
            if (other.gameObject.CustomIsTag("Worm"))
            {
                GameObject snowHit = GameManager.Instance.fXManager.snowHitFXPool.GetFX();
                snowHit.transform.position = transform.position;
                snowHit.transform.SetParent(transform);
                snowHit.SetActive(true);
                //Handheld.Vibrate();
                GameManager.Instance.sfxManager.GameDustFX();
                playerContext.properties.flightDetails.JetHit(GameManager.Instance.scoreData.wormHitAmount);
            }
        }


    }

}