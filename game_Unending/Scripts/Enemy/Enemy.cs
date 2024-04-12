using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UE
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public Node Behaviour;
        public EnemyContext context;

        private void OnDisbale()
        {
           // GameManager.Instance.gameEventManager.currentGameEvent.EnemyKilled();
        }
        public void Spawn(Node node) { gameObject.SetActive(true); Behaviour = node; Behaviour.Init(context); }
        private void Update() { if (GameManager.Instance.isGameRunning) { Behaviour.Eval(); } }
        private void OnTriggerEnter(Collider other) { Behaviour.OnColliderEntered(other); }
        public void TakeDamage(float damage) { Behaviour.OnTakeDamage(damage); }

    }

    [System.Serializable]
    public struct EnemyContext
    {
        public Transform selfTransform;
        public Transform rayOrigin;
        public Image healthValue;
    }
}
