using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Sound 
{
    public class PlayOneShot2D : MonoBehaviour
    {       
        [SerializeField]
        private bool playOnStart = false;
        [SerializeField]
        private Sound sound = default;

        void Start()
        {
            if (playOnStart)
                Play();
        }

        public void Play() 
        {
            AudioManager.Instance.PlayOneShoot2D(
                sound.clip,
                sound.volume,
                sound.delay, 
                !sound.isPersistent
            );
        }
    }
}