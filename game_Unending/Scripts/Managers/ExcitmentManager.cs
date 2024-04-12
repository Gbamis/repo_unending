using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UE
{
    [System.Serializable]
    public struct Cell
    {
        public Vector3 rates;
    }

    public class ExcitmentManager : MonoBehaviour
    {
        [SerializeField] private Node[] BehaviourLevels;
        [SerializeField] private Cell[] cellData;

        public void SetData(){
        }
    }
}
