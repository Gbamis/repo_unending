using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{

    public class UserData : MonoBehaviour
    {
        public List<Weapon> aquiredWeapons;
        public List<Weapon> GetAvailableWepons { get { return aquiredWeapons; } }

        public void RefillWeapons() { foreach (Weapon weapon in aquiredWeapons) { weapon.Init(); } }
    }
}
