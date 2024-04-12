using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class GameEventObstacleWorm : MonoBehaviour
    {
        private float nextSpawn = 0;
        [SerializeField] private Worm wormTrack;
        [SerializeField] private bool allowToSpawn;


        public void Init() { StartCoroutine(Routine()); }
        private IEnumerator Routine()
        {
            while (true)
            {
                float wait = Random.Range(10, 30);
                yield return new WaitForSeconds(wait);
                yield return new WaitUntil(() => GameManager.Instance.isGameRunning && allowToSpawn);
                SpawnWorm(true);
            }
        }

        private void SpawnWorm(bool value)
        {
            wormTrack.gameObject.SetActive(value);
            wormTrack.Spawn(Vector3.zero);
            GameManager.Instance.sfxManager.RumbleFX();
        }

        public void Clear() { wormTrack.gameObject.SetActive(false); }
    }
}
