using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public enum Tag
    {
        RING,
        ORB, PLAYER, ENEMY, BULLET, SAND, ROCKS,
        TRIGGERNEXTTERRAIN, TERRAINDESPAWN, OBSTACLE, SNOW, WORM, DAMAGEABLE,
        SHIFTERS
    }

    public class CustomTag : MonoBehaviour
    {
        public List<Tag> tags = new List<Tag>();
        public bool HasTag(string v)
        {
            foreach (Tag tg in tags)
            {
                if (tg.ToString().ToLower() == v.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTag(Tag v)
        {
            if(tags.Contains(v)){ return true;}
            return false;
        }
    }
}
