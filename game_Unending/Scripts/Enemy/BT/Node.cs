using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [System.Serializable]
    public class Node : ScriptableObject
    {

        [SerializeField] public NODESTATE nodeState;
        public List<Node> childNodes = new List<Node>();

        public virtual void Init(EnemyContext context)
        {
            foreach (Node node in childNodes)
            {
                node.Init(context);
            }
        }

        public virtual NODESTATE Eval() { return nodeState; }

        public virtual void OnColliderEntered(Collider other)
        {
            if (childNodes != null)
            {
                foreach (Node node in childNodes)
                {
                    node.OnColliderEntered(other);
                }
            }
        }

        public virtual void OnTakeDamage(float damage)
        {
            if (childNodes != null) { foreach (Node node in childNodes) { node.OnTakeDamage(damage); } }
        }
    }

    public enum NODESTATE
    {
        RUNNING, SUCCESS, FAILED
    }
}
