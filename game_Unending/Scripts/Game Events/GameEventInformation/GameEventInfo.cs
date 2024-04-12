using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace UE
{
    [CreateAssetMenu(fileName = "GameEventDetails", menuName = " Axxon/Campaign/Campaign Details")]
    public class GameEventInfo : ScriptableObject
    {
        [Header("Data configuration")]
        public string campaingID;
        public string campaignTitle;
        public string campaignSubTitle;
        public string campaignSummary;
        public Sprite campaignBg;
        public Sprite campaignthumbnail;
        public bool isEventLocked;

        [Header("Scene configuration")]
        public LightingSettings weather;
        public Material skyMaterial;
        public Color fogColor;
        public float fogDensity;
        public  VolumeProfile postProcessData;
        public int shadowDistance;

        [Header("GamePlay")]
        public float horizontalClamp;
        public Vector2 verticalClamp;

    }
}
