using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [CreateAssetMenu(fileName = "Fire Node", menuName = " Axxon/Behaviour Trees/Actions/FireNode")]
    public class EnemyFireNode : Node
    {
        private EnemyContext m_context;
        private float nextFire;
        public float fireRate;
        public float aimRange;
        public float bulletLifeSpan;
        public float bulletSpeed;
        public float bulletDamage;
        public Vector3 bulletSetSize;

        public override void Init(EnemyContext context)
        {
            base.Init(context);
            m_context = context;
            nextFire = 0;
        }

        public override NODESTATE Eval()
        {
            if (canSightPlayer() && Time.timeScale == 1)
            {
                nodeState = NODESTATE.RUNNING;
                Fire();
            }

            else { nodeState = NODESTATE.FAILED; }
            return nodeState;
        }

        private bool canSightPlayer()
        {
            Vector3 origin = m_context.rayOrigin.position;
            Vector3 dir = m_context.rayOrigin.forward * aimRange;
            //Debug.DrawRay(origin, dir, Color.red);

            if (Physics.BoxCast(origin, Vector3.one, dir, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player")) { return true;}
            }
            return false;
        }

        private void Fire()
        {
            if (Time.time > nextFire)
            {
                int rand = Random.Range(-14, 15);
                float scale = Random.Range(0.7f, 1);

                Vector3 rot = new(0, 0, rand);
                Vector3 scl = new(scale, 0, scale);

                GameObject bullet = GameManager.Instance.starightBulletPoolManager.GetBullet(OWNER.ENEMY, bulletDamage);
                bullet.transform.SetPositionAndRotation(m_context.rayOrigin.position,m_context.rayOrigin.rotation);
                bullet.transform.localScale = bulletSetSize;
                Vector3 dir = m_context.rayOrigin.forward;

                bullet.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed);
                GameManager.Instance.sfxManager.EnemyFiringModeFX();
                GameManager.Instance.StartCoroutine(KillBullet(bullet));

                IEnumerator KillBullet(GameObject bullet)
                {
                    yield return new WaitForSeconds(bulletLifeSpan);
                    GameManager.Instance.missileBulletPoolManager.ReturnBullet(bullet);
                }
                nextFire = Time.time + fireRate;
            }
        }
    }
}
