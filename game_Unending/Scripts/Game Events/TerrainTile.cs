using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UE
{
    public class TerrainTile : MonoBehaviour
    {
        public Transform terrainNextSpawn;
        public List<TerrainTile> allowedTerrains;

        public TerrainTile GetTerrainTile()
        {
            Rand.InitState();
            int rand = Random.Range(0, allowedTerrains.Count);
            return allowedTerrains[rand];
        }
    }
}
