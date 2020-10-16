using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

namespace Leaderboard
{
    // [MessagePackObject(keyAsPropertyName: true)]
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

    // [MessagePackObject]
    [Serializable]
    public class Match
    {
        // [Key(0)]
        public string user;
        // [Key(1)]
        public int score;        
        public Match(string user, int score)
        {
            this.user = user;
            this.score = score;
        }
        
    }
}
