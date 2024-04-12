using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [CreateAssetMenu(fileName = "Health Node", menuName = " Axxon/Behaviour Trees/Actions/Enemy Health eNode")]
    public class EnemyHealthNode : Node
    {
        private EnemyContext m_context;
        public float health;
        private float currentHealth;

        public override void Init(EnemyContext context)
        {
            base.Init(context);
            m_context = context;
            currentHealth = health;
            m_context.healthValue.fillAmount = 1;
        }

        public override NODESTATE Eval() { nodeState = NODESTATE.RUNNING; return nodeState; }

        public override void OnColliderEntered(Collider other)
        {

            if (other.gameObject.CustomIsTag("Snow"))
            {
                GameObject snowHit = GameManager.Instance.fXManager.snowHitFXPool.GetFX();
                if (snowHit != null)
                {
                    snowHit.transform.position = m_context.selfTransform.position;
                    snowHit.transform.SetParent(m_context.selfTransform);
                    snowHit.gameObject.SetActive(true);
                }
            }
        }

        public override void OnTakeDamage(float damage)
        {
            if (currentHealth < 0)
            {
                nodeState = NODESTATE.FAILED;
                Destroy(m_context.selfTransform.gameObject);
                ParticleSystem explode = GameManager.Instance.fXManager.GetExplodePrefab();
                explode.transform.position = m_context.selfTransform.position;
                explode.gameObject.SetActive(true);
                GameManager.Instance.sfxManager.ExplodeFX();
                GameManager.Instance.gameEventManager.currentGameEvent.EnemyKilled();

            }
            else
            {
                currentHealth -= damage;
                float per = currentHealth / health;
                m_context.healthValue.fillAmount = per;
            }
        }
    }
}
