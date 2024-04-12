using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace UE
{
    [System.Serializable]
    public struct EnemyLevel
    {
        [HideInInspector] public int currentLevel;
        [HideInInspector] public int enemyKillsCount;

        public Enemy enemyPrefab;
        [Tooltip("Enemy Behaviour simple [0 - 20] hardest")]
        public Node[] enemyBehaviourLevel;
    }

    public class GameEventEnemy : MonoBehaviour
    {
        [SerializeField] private Transform spawnedEnemyRoot;
        public EnemyLevel enemyLevel;
        public bool allowEnemySpawn;
        public PlayableDirector flyDirector;

        public void Init()
        {
            enemyLevel.currentLevel = 0;
            enemyLevel.enemyKillsCount = 0;
            GameManager.Instance.uIManager.gameplaySelection.SetKill(enemyLevel.enemyKillsCount);
            StartCoroutine(SpawnEnemy(enemyLevel.currentLevel, 1));
            //flyDirector.Play();
        }
        
        public int GetEnemyKilled { get { return enemyLevel.enemyKillsCount; } }

        public IEnumerator SpawnEnemy(int level, int numberToSpawn)
        {
            yield return new WaitForSeconds(5);
            if (allowEnemySpawn)
            {
                float rand = Random.Range(2, 4);
                yield return new WaitForSeconds(rand);
                level = Mathf.Clamp(level, 0, enemyLevel.enemyBehaviourLevel.Length - 1);

                if (spawnedEnemyRoot.childCount == 0)
                {
                    Enemy enemy = Instantiate(enemyLevel.enemyPrefab);
                    enemy.transform.SetParent(spawnedEnemyRoot);
                    
                    int randEnemy = Random.Range(0,3);
                    Vector3 startPos = GameManager.Instance.playerContext.enemySpawnPoint[randEnemy].position;
                    startPos.z += 40;
                    enemy.transform.position = startPos;
                    enemy.Spawn(enemyLevel.enemyBehaviourLevel[level]);
                }
            }

        }
        public void EnemyKilled()
        {
            enemyLevel.enemyKillsCount++;
            enemyLevel.currentLevel++;
            GameManager.Instance.uIManager.gameplaySelection.SetKill(enemyLevel.enemyKillsCount);
            GameManager.Instance.gameEventManager.currentGameEvent.outcome.enemyKill = enemyLevel.enemyKillsCount;

            GameManager.Instance.unityConnectionManager.UpdateEnemyKillStat(enemyLevel.enemyKillsCount);
            GameManager.Instance.unityConnectionManager.UpdateTotalKillStat(1);

            StartCoroutine(SpawnEnemy(enemyLevel.currentLevel, 1));
        }

        public void Clear()
        {
            foreach (Transform e in spawnedEnemyRoot)
            {
                if (e != null) { Destroy(e.gameObject); }
            }
        }
    }
}
