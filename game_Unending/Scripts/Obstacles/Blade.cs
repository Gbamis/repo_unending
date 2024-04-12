using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class Blade : MonoBehaviour
    {
        private float degreeAmount;
        public int rotationDirection;
        public float rotationSpeed;
        public Transform rotor;

        private void Awake()
        {
            rotationDirection = (Random.Range(-1, 2) < 1) ? -1 : 1;
            degreeAmount = Random.Range(45,90);
        }

        private void Update()
        {
            Vector3 rot = new Vector3(0, 0, rotationDirection * degreeAmount);
            rotor.Rotate(rot * Time.deltaTime * rotationSpeed);
        }
    }
}
