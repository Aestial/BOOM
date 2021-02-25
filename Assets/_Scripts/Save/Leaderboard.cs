using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Save
{
    [Serializable]
    public class Leaderboard
    {
        public List<Match> matches;
        public Leaderboard()
        {
            matches  = new List<Match>();
        }
        public void Add(Match match)
        {
            matches.Add(match);
        }
        public void Add(string user, int score)
        {
            Match match = new Match(user, score);
            Add(match);
        }        
    }

    [Serializable]
    public class Match
    {
        public string user;
        public int score;        
        public Match(string user, int score)
        {
            this.user = user;
            this.score = score;
        }
        
    }
}
