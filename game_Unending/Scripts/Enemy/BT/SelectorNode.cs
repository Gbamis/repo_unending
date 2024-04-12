using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
   [CreateAssetMenu(fileName = "Selector Node", menuName = " Axxon/Behaviour Trees/SelectionNode")]
    public class SelectorNode : Node
    {
        public override void Init(EnemyContext context)
        {
            base.Init(context);
        }

        public override NODESTATE Eval()
        {
            foreach(Node n in childNodes){
                switch(n.Eval()){
                    case NODESTATE.RUNNING:
                        nodeState = NODESTATE.RUNNING;
                        return nodeState;
                    case NODESTATE.SUCCESS:
                        nodeState = NODESTATE.SUCCESS;
                        return nodeState;
                    case NODESTATE.FAILED:
                        continue;
                }
            }
            nodeState = NODESTATE.FAILED;
            return nodeState;
        }
    }
}
