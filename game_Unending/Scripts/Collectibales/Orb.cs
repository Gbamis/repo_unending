using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class Orb : MonoBehaviour
    {
        private void Update()
        {
            float dis = Vector3.Distance(GameManager.Instance.playerContext.player.position, transform.position);
            float speed = GameManager.Instance.playerContext.attractionForce;
            float distance = GameManager.Instance.playerContext.attractionRange;
            if (dis < distance && GameManager.Instance.playerContext.isAttractionMode)
            {
                transform.position = Vector3.Lerp(transform.position, GameManager.Instance.playerContext.player.position, Time.deltaTime * speed);
            }
        }
    }
}
