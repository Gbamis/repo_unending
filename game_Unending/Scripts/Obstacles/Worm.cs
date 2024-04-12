using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace UE
{
    public class Worm : MonoBehaviour, IObstacle
    {
        public CinemachineDollyCart cinemachineDollyCart;
        public Vector3 offset;
        private float track;
        private float defSpeed;

        public void Spawn(Vector3 pos)
        {
            cinemachineDollyCart.m_Position = 0;
            defSpeed = 120;
        }

        private void Update()
        {
            if (GameManager.Instance.isGameRunning) { cinemachineDollyCart.m_Speed = defSpeed; }
            else { cinemachineDollyCart.m_Speed = 0; }
            
            if (gameObject.activeSelf && cinemachineDollyCart.m_Position < track)
            {
                Vector3 pos = GameManager.Instance.playerContext.player.position;
                pos += offset;
                transform.position = pos;
            }
            else { track = Random.Range(400, 470); }

        }
    }
}
