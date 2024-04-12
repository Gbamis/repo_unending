using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [System.Serializable]
    public class FXPool{
        private int getIndex = 0;
        public int poolSize;
        public Transform root;
        private GameObject[] pool;
        [SerializeField] private GameObject fxPrefab;

        public void Create(){
            pool = new GameObject[poolSize];
            for(int i =0; i < poolSize; i++){
                pool[i] = Object.Instantiate(fxPrefab);
                pool[i].transform.SetParent(root);
            }
        }

        public GameObject GetFX(){
            getIndex = (getIndex> poolSize-1)? 0 : getIndex;
            int temp = getIndex;
            getIndex++;
            return pool[temp];
        }

        public void ReturnFXPrefab(GameObject fx) => fx.SetActive(false);

    }

    public class FXManager : MonoBehaviour
    {

        public FXPool snowHitFXPool;
        public FXPool bulletTrailPool;

        [SerializeField] private ParticleSystem explodeImpactFX;
        [SerializeField] private ParticleSystem shieldFX;
        [SerializeField] private GameObject healthPackPrefab;
        [SerializeField] private ParticleSystem healthAddedFX;

        public ParticleSystem GetHealthFXPrefab() { return Instantiate(healthAddedFX); }
        public ParticleSystem GetExplodePrefab() { return Instantiate(explodeImpactFX); }
        public ParticleSystem GetShieldPrefab() { return Instantiate(shieldFX); }

        private void Awake(){
            snowHitFXPool.Create();
            bulletTrailPool.Create();
        }

        public void SpawnHelathPack(Vector3 spawnPoint)
        {
            GameObject healthPack = Instantiate(healthPackPrefab);
            healthPack.transform.position = spawnPoint;
            healthPack.SetActive(true);
        }
    }
}
