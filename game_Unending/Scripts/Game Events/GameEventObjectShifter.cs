using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UE
{
    public class GameEventObjectShifter : MonoBehaviour
    {
        [SerializeField] private Tag tagToShift;
        [SerializeField] private Vector3 axis;
        [SerializeField] private List<GameObject> shiftObjects;
        private List<CustomTag> customTags = new List<CustomTag>();

        public void Init()
        {

        }

        public void NextTile()
        {
            shiftObjects.Clear();
            customTags.Clear();
            TerrainTile nextTile = GameManager.Instance.gameEventManager.currentGameEvent.currentTile;
            customTags = nextTile.GetComponentsInChildren<CustomTag>().ToList();
            var res = from x in customTags
                      where x.HasTag(tagToShift)
                      select x;
            foreach (var n in res)
            {
                shiftObjects.Add(n.gameObject);
                Rand.InitState();
                float randY = Random.Range(100,360);
                float pRot = n.gameObject.transform.rotation.y;
                Quaternion newRot = Quaternion.Euler(0,randY+pRot,0);
                n.gameObject.transform.rotation = newRot;
            }
        }
    }
}
