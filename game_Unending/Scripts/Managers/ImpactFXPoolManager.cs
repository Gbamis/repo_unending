using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class ImpactFXPoolManager : MonoBehaviour
    {
        [SerializeField] private int hitCount;
        [SerializeField] private GameObject hitImpactPrefab;

        private void Awake()
        {
            for (int i = 0; i < hitCount; i++)
            {
                GameObject bullet = Instantiate(hitImpactPrefab);
                bullet.SetActive(false);
                bullet.transform.parent = transform;
            }
        }

        public GameObject GetImpactFX()
        {
            foreach (Transform b in transform)
            {
                if (!b.gameObject.activeSelf)
                {
                    b.gameObject.SetActive(true);
                    return b.gameObject;
                }
            }
            return null;
        }

        public void ReturnImpactFX(GameObject bullet)
        {
            if (bullet.activeSelf) { bullet.SetActive(false); }
            bullet.transform.SetParent(transform);

        }
    }
}
