using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class BulletPoolManager : MonoBehaviour
    {
        private int poolIndex = 0;
        [SerializeField] private int bulletCount;
        [SerializeField] private GameObject bulletPrefab;

        private void Awake()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bullet.transform.parent = transform;
            }
        }

        public GameObject GetBullet(OWNER owner, float damage)
        {
            if (poolIndex > bulletCount - 2) { poolIndex = 0; }
            int temp = poolIndex;
            poolIndex++;
            transform.GetChild(poolIndex).GetComponent<Bullet>().SetOwner(owner, damage);
            transform.GetChild(poolIndex).gameObject.SetActive(true);
            return transform.GetChild(poolIndex).gameObject;
        }

        public void ReturnBullet(GameObject bullet)
        {
            if (bullet.activeSelf)
            {
                bullet.SetActive(false);
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        }
    }
}
