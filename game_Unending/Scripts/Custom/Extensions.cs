using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UE
{
    public static class Extensions
    {
        public static string CustomTag(this GameObject obj)
        {
            return obj.GetComponent<CustomTag>().name;
        }

        public static bool CustomIsTag(this GameObject obj, string value)
        {
            bool val = false;
            try { val = obj.GetComponent<CustomTag>().HasTag(value) ? true : false; }
            catch (System.Exception e) { }
            return val;
        }
        public static bool CustomIsTag(this Collider obj, string value)
        {
            bool val = false;
            try { val = obj.GetComponent<CustomTag>().HasTag(value) ? true : false; }
            catch (System.Exception e) { }
            return val;
        }
    }

    public static class Rand
    {
        public static int spawnCount;
        private static int seed;

        public static void InitState()
        {
            seed = DateTime.Now.Millisecond;
            UnityEngine.Random.InitState(seed);
        }

        public static void ResetState()
        {
            spawnCount = 0;
        }


        public static float Get(float min, float max)
        {
            UnityEngine.Random.InitState(seed + spawnCount);
            float value = UnityEngine.Random.Range(min, max);
            spawnCount++;
            return value;
        }
    }
}
