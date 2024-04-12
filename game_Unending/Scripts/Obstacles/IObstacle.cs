using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

namespace UE
{
    public interface IObstacle
    {
        abstract void Spawn(Vector3 pos);

    }
}
