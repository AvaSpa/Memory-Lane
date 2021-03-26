using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip Clip;
        [Range(0, 1)]
        public float Volume;
        public bool Loop;

        [HideInInspector]
        public AudioSource Source;
    }
}