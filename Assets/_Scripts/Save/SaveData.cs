using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Save
{
    [Serializable]
    public class Data 
    {
        public string username;
        public Leaderboard results;
    }

    public class SaveData : JsonSerializer<SaveData>
    {
        [SerializeField] Data data;

        private void Start () 
        {
            data = GetFromFileOrCreate(() => { return new Data(); } );
        }
        
        public void SetName (string value)
        {
            data.username = value;
            Save(data);
        }

        public void AddResult (int score)
        {
            data.results.Add(data.username, score);
            Save(data);
        }
    }
}
