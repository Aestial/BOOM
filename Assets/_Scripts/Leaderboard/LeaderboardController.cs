using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardController : MonoBehaviour
    {
        [SerializeField] Leaderboard leaderboard;    
        public Leaderboard Leaderboard
        {
            get { return leaderboard; }
            set { leaderboard = value; }
        }        
        public void AddMatch(string user, int score)
        {
            if (leaderboard.matches.Count > 7)
            {
                // if (score > )
            }
            leaderboard.Add(user, score);
        }
        void OnEnable()
        {
     
        }
        void OnDisable()
        {

        }        
    }    
}