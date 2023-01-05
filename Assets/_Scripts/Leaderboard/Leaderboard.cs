using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Liquid.Leaderboard
{
    [Serializable]
    public class Leaderboard
    {
        public List<Match> matches;
        public string zero;
        public int maxLength;

        public Leaderboard(int maxLength, string zero)
        {
            this.zero = zero;
            this.maxLength = maxLength;
            matches = new List<Match>();
        }
        public void Add(Match match)
        {
            matches.Add(match);
        }
        public void Add(string user, int score)
        {
            if (!String.IsNullOrEmpty(user) && score > 0)
            {
                var userMatch = matches.Find(match => match.user == user);
                if (userMatch == null)
                {
                    Match match = new Match(user, score);
                    Add(match);
                    Sort();
                    Truncate();
                }
                else if (userMatch.score < score)
                {
                    userMatch.score = score;
                    Sort();
                }
            }
        }
        public override string ToString()
        {
            if (matches.Count > 0)
            {
                var str = "";
                foreach (Match match in matches)
                {
                    str += match.ToString() + "\n";
                }
                return str;
            }
            return zero;
        }
        private void Sort()
        {
            matches = matches.OrderByDescending(match => match.score).ToList();
            Debug.Log(ToString());
        }
        private void Truncate()
        {
            while (matches.Count > maxLength)
            {
                matches.RemoveAt(matches.Count - 1);
            }
        }
    }   
}