using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [CreateAssetMenu(fileName = "Retarget Node", menuName = " Axxon/Behaviour Trees/Actions/RetagretNode")]
    public class RetargetNode : Node
    {
        private EnemyContext m_context;
        public float reTargetSpeed;
        public float distanceThresh;
        public Vector3 offset;


        public override void Init(EnemyContext context)
        {
            base.Init(context);
            m_context = context;
        }

        public override NODESTATE Eval()
        {
            RealignToPlayer();
            nodeState = NODESTATE.RUNNING;
            return nodeState;
        }

        private void RealignToPlayer()
        {
            Vector2 player = GameManager.Instance.playerContext.player.position + offset;
            Vector2 self = m_context.selfTransform.position;

            Vector2 pos = Vector2.Lerp(self, player, Time.deltaTime * reTargetSpeed);

            m_context.selfTransform.position = new Vector3(pos.x, pos.y, GameManager.Instance.playerContext.enemySpawnPoint[0].position.z);
        }
    }
}
