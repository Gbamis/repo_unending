using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class Property : ScriptableObject
    {
        public virtual void OnPropertyInit() { }
        public virtual void OnPropertyProcess(Transform body =null) { }
    }
}
