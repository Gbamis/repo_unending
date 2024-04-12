using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public enum OWNER{ NONE, PLAYER, ENEMY}
    public class Bullet : MonoBehaviour
    {
        public OWNER owner;
        public float takeDamge;

        public void SetOwner(OWNER oWNER, float damageAmount){
            owner = oWNER;
            takeDamge = damageAmount;
        }
    }
}
