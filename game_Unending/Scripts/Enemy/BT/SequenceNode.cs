using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    [CreateAssetMenu(fileName = "Sequence Node", menuName = " Axxon/Behaviour Trees/SequenceNode")]
    public class SequenceNode : Node
    {
        public override void Init(EnemyContext context)
        {
            base.Init(context);
        }

        public override NODESTATE Eval()
        {
            foreach (Node n in childNodes)
            {
                switch (n.Eval())
                {
                    case NODESTATE.FAILED:
                        nodeState = NODESTATE.FAILED;
                        return nodeState;
                    case NODESTATE.RUNNING:
                        continue;
                    case NODESTATE.SUCCESS:
                        continue;
                }
            }
            nodeState = NODESTATE.RUNNING;
            return nodeState;
        }
    }
}
