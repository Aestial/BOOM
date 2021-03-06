using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Liquid.Actions
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string scene = "";

        public void Load ()
        {
            Load(scene);
        }

        public void Load (string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        void Start()
        {
            
        }
    }   
}