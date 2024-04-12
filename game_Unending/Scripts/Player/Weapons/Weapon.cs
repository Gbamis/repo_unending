using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class Weapon : ScriptableObject
    {
        [HideInInspector] protected float nextFire = 0;
        [SerializeField] protected float fireRate;
        [SerializeField][Range(1, 90)] protected float useRate;
        [SerializeField] protected float bulletLifeSpan;
        [SerializeField] protected float bulletDamage;
        public AudioClip fireSFX;

        public float aimRange;
        protected RaycastHit hit;

        public Sprite icon;
        public Vector3 bulletSetSize;
        [Range(10, 10000)] public float magazine;

        public virtual void Init() { magazine = 10000; nextFire = 0;}

        public virtual void OnUpdate(Transform body)
        {
            if (Time.timeScale < 1 && magazine > 0) { return; }
            IsTargetSeen();
        }

        public void IsTargetSeen()
        {
            Vector3 origin = GameManager.Instance.playerContext.rayOrigin.position;
            Vector3 direction = GameManager.Instance.playerContext.rayOrigin.forward * aimRange;

            if (Physics.Raycast(origin, direction, out hit))
            {
                if (hit.collider != null && hit.collider.CustomIsTag("Enemy"))
                {
                    GameManager.Instance.uIManager.gameplaySelection.crossHair.color = Color.red;
                }
                else { GameManager.Instance.uIManager.gameplaySelection.crossHair.color = Color.white; }
            }
            else { GameManager.Instance.uIManager.gameplaySelection.crossHair.color = Color.white; }

        }

    }
}
