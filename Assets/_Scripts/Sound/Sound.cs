using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Sound 
{
    [System.Serializable]
    public class Sound 
    {
        public AudioClip clip = default;
        public float volume = 1.0f;
        public float delay = 0.0f;
        public bool isPersistent = false;
    }
    [System.Serializable]
    public class Loop : Sound 
    {
        public string name = "LoopName";
    }
}