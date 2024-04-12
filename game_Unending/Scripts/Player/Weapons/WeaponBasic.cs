using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [CreateAssetMenu(fileName = "Basic weapon", menuName = " Axxon/Player/Weapons/Basic weapon")]
    public class WeaponBasic : Weapon
    {
        public float nuzzleFlashScale;

        public override void Init()
        {
            base.Init();
        }

        public override void OnUpdate(Transform body) { base.OnUpdate(body); Fire(); }


        public void Fire()
        {
            if (magazine > 0 && (GameManager.Instance.playerContext.isBulletPressed || KeyInput.Instance.shootCmd.IsPerformed))
            {
                if (Time.time > nextFire)
                {
                    GameManager.Instance.sfxManager.FiringModeFX(fireSFX);
                    foreach (ParticleSystem child in GameManager.Instance.playerContext.nuzzleFlashes)
                    {
                        child.gameObject.SetActive(true);
                        HitPoint(child.transform.position);
                        magazine -= useRate;
                    }
                    nextFire = Time.time + fireRate;
                }
                else { foreach (ParticleSystem child in GameManager.Instance.playerContext.nuzzleFlashes) { child.gameObject.SetActive(false); } }

            }
            else
            {
                nextFire = 0;
                foreach (ParticleSystem child in GameManager.Instance.playerContext.nuzzleFlashes) { child.gameObject.SetActive(false); }
            }
        }


        private void HitPoint(Vector3 origin)
        {
            Vector3 direction = GameManager.Instance.playerContext.rayOrigin.forward * aimRange;

            if (Physics.Raycast(origin, direction, out RaycastHit hit))
            {
                if (hit.collider.CustomIsTag("snow") || hit.collider.CustomIsTag("Enemy"))
                {
                    TrailEffect(origin, hit.point, hit.transform);

                    GameObject hitFX = GameManager.Instance.impactFXPoolManager.GetImpactFX();
                    Vector3 pos = hit.point;
                    hitFX.transform.position = pos;
                    GameManager.Instance.StartCoroutine(KillBullet());
                    IEnumerator KillBullet()
                    {
                        yield return new WaitForSeconds(0.4f);
                        GameManager.Instance.impactFXPoolManager.ReturnImpactFX(hitFX);
                    }
                }
                if (hit.collider != null && hit.collider.CustomIsTag("Enemy"))
                {
                    if (hit.collider.TryGetComponent(out IDamageable dm)) { dm.TakeDamage(bulletDamage); }
                }
            }
            Debug.DrawRay(origin, direction, Color.black);
        }

        private void TrailEffect(Vector3 origin, Vector3 endPoint, Transform body)
        {
            GameObject trail = GameManager.Instance.fXManager.bulletTrailPool.GetFX();
            trail.SetActive(true);
            trail.transform.position = origin;

            GameManager.Instance.StartCoroutine(Show());
            IEnumerator Show()
            {
                yield return new WaitForSeconds(0.05f);
                trail.transform.position = endPoint;
                yield return new WaitForSeconds(0.05f);
                GameManager.Instance.fXManager.bulletTrailPool.ReturnFXPrefab(trail);
            }

        }


    }

}
