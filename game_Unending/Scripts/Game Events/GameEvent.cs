using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.Universal;

namespace UE
{

    [System.Serializable]
    public class Outcome
    {
        public int enemyKill { set; get; }
        public int distance { set; get; }

        public void SetEnemy(int value) { enemyKill = value; }
        public void SetDistance(int value) { distance = value; }

    }

    public class GameEvent : MonoBehaviour
    {
        private List<TerrainTile> tiles;
        public TerrainTile currentTile;
        private TerrainTile previousTile;
        public GameEventInfo eventConfig;
        [SerializeField] private GameEventObstacle gameEventObstacle;
        [SerializeField] private GameEventEnemy gameEventEnemy;
        [SerializeField] private GameEventObstacleWorm gameEventObstacleWorm;
        [SerializeField] private GameEventObjectShifter gameEventObjectShifter;

        public Transform playerStartPosition;
        public Transform landStartPosition;

        public Outcome outcome;

        public float PROGRESSION_RATE;
        private float passedTime;
        private float totalFlight;


        public void StartGameEvent()
        {
            GameManager.Instance.globalVolume.sharedProfile = eventConfig.postProcessData;
            RenderSettings.skybox = eventConfig.skyMaterial;
            RenderSettings.fogColor = eventConfig.fogColor;
            RenderSettings.fogDensity = eventConfig.fogDensity;

           /* if (eventConfig.useSSAO)
            {
               GameManager.Instance.universalRendererData.rendererFeatures[0].SetActive(true);
                Debug.Log("use ssao"+GameManager.Instance.universalRendererData.rendererFeatures[0]);
            }
            else { GameManager.Instance.universalRendererData.rendererFeatures[0].SetActive(false); }*/

            GameManager.Instance.universalRenderPipelineAsset.shadowDistance = eventConfig.shadowDistance;

            tiles = GetComponentsInChildren<TerrainTile>().ToList();
            foreach (TerrainTile tt in tiles) { tt.gameObject.SetActive(false); }

            currentTile = currentTile.GetTerrainTile();
            currentTile.transform.position = landStartPosition.position;
            currentTile.gameObject.SetActive(true);

            transform.gameObject.SetActive(true);
            transform.position = landStartPosition.position;
            GameManager.Instance.playerContext.player.position = playerStartPosition.position;
            GameManager.Instance.playerContext.properties.flightDetails.ResetValues();
            GameManager.Instance.sfxManager.JetEngineFX();
            GameManager.Instance.isGameRunning = true;


            gameEventObstacle = GetComponent<GameEventObstacle>();
            gameEventEnemy = GetComponent<GameEventEnemy>();
            gameEventObstacleWorm = GetComponent<GameEventObstacleWorm>();
            gameEventObjectShifter = GetComponent<GameEventObjectShifter>();


            gameEventObstacle?.Init();
            gameEventEnemy?.Init();
            gameEventObstacleWorm?.Init();
            gameEventObjectShifter?.Init();


            GameManager.Instance.chosenJet.OnPropertyInit();
            GameManager.Instance.userData.RefillWeapons();
            totalFlight = 0;

            StartCoroutine(RATE());
        }

        public void PlayEventAduio() { GameManager.Instance.sfxManager.PlayGameEventAudio(true); }
        public void StopEventAduio()
        {
            GameManager.Instance.sfxManager.PlayGameEventAudio(false);
            GameManager.Instance.sfxManager.PlayGameTheme();
        }


        public void QuitEvent()
        {
            try { GameManager.Instance.uIManager.eventSelection.SelectEvent(transform.GetSiblingIndex()); } catch (System.Exception e) { }
            gameEventObstacle?.ClearSpawned();
            gameEventEnemy?.Clear();
            gameEventObstacleWorm?.Clear();

            StopCoroutine(RATE());
            GameManager.Instance.sfxManager.StopJetEngineFX();
            GameManager.Instance.uIManager.gameplaySelection.gameResultDialog.gameObject.SetActive(true);
            StopEventAduio();

        }

        private void GameEventEnded()
        {
            try { GameManager.Instance.uIManager.eventSelection.SelectEvent(transform.GetSiblingIndex()); } catch (System.Exception e) { }
            StopEventAduio();
        }

        public void ActivateNextTile()
        {
            TerrainTile nextTile = currentTile.GetTerrainTile();
            nextTile.gameObject.transform.position = currentTile.terrainNextSpawn.position;
            previousTile = currentTile;
            currentTile = nextTile;
            currentTile.gameObject.SetActive(true);

            gameEventObstacle?.ClearSpawned();
            gameEventObjectShifter?.NextTile();


        }

        public void DeActivateLastTile()
        {
            if (previousTile == null) { return; }
            previousTile.gameObject.SetActive(false);
        }
        public void DeActivateLastTile(Collider other) { other.transform.parent.gameObject.SetActive(false); }


        public void EnemyKilled()
        {
            gameEventEnemy.EnemyKilled();
        }

        private IEnumerator RATE()
        {
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    totalFlight += Time.deltaTime;
                    GameManager.Instance.uIManager.gameplaySelection.SetTime((int)totalFlight);
                    outcome.distance = (int)totalFlight;
                }

                if (Time.time > passedTime)
                {
                    passedTime = Time.time + PROGRESSION_RATE;
                    GameManager.Instance.IncreaseGameRate();
                }
                yield return new WaitUntil(() => GameManager.Instance.isGameRunning);
            }
        }
    }
}
