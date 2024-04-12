using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class GameEventObstacle : MonoBehaviour
    {
        [SerializeField] private float spawnInterval;
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private Transform spawnedRoot;
        [SerializeField] private float maxZ;
        [SerializeField] private float maxX;
        [SerializeField] private bool allowSpawn;

        private float nextSpawn = 0;

        public void Init() { StartCoroutine(Routine()); }

        public IEnumerator Routine()
        {
            yield return new WaitUntil(CanSpawn);
            while (GameManager.Instance.isGameRunning)
            {
                if (Time.time > nextSpawn)
                {
                    SpawnObstcale();
                    nextSpawn = Time.time + spawnInterval;
                }
                yield return new WaitUntil(CanSpawn);
            }
        }
        public void ClearSpawned()
        {
            foreach (Transform child in spawnedRoot)
            {
                Destroy(child.gameObject);
            }
        }
        public bool CanSpawn() { return allowSpawn; }

        private void SpawnObstcale()
        {
            Vector3 pos = GameManager.Instance.playerContext.player.position;
            pos.z += Random.Range(maxZ - 5, maxZ);
            pos.x += Random.Range(-maxX, maxX);

            GameObject obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = pos;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.gameObject.SetActive(true);
            obstacle.transform.SetParent(spawnedRoot);
        }
    }
}
