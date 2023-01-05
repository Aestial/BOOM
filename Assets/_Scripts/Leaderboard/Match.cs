using System;
using UnityEngine;

namespace Liquid.Leaderboard
{
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
        public override string ToString()
        {
            return $"{user}\t{score}";
        }
    }
}