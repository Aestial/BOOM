using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Sound 
{
    public class PlayLoop : MonoBehaviour
    {
        [SerializeField]
        private bool playOnStart = true;
        [SerializeField]
        private Loop loop = default;

        void Start()
        {
            if (playOnStart)
                Play();
        }

        public void Play() 
        {
            AudioManager.Instance.PlayLoop2D(
                loop.name,
                loop.clip,
                loop.volume,
                loop.delay, 
                !loop.isPersistent
            );
        }
        public void Stop()
        {
            AudioManager.Instance.StopLoop(loop.name);
        }
    }
}
