using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class CinematicManager : MonoBehaviour
    {
        public Camera mainCam;
        public Camera cinematicCam;

        public bool isCinematic;

        private void Update()
        {
            if (isCinematic)
            {
                GameManager.Instance.isGameRunning = false; 
            }

            EnableCenematics(isCinematic);
        }

        public void EnableCenematics(bool value)
        {
            mainCam.gameObject.SetActive(!value);
            cinematicCam.gameObject.SetActive(value);
        }
    }
}
