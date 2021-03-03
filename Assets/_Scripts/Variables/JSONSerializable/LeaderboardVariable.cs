using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Liquid.Variables
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
            matches  = new List<Match>();            
        }
        public void Add(Match match)
        {
            matches.Add(match);
        }
        public void Add(string user, int score)
        {
            if (!String.IsNullOrEmpty(user) && score > 0)
            {
                Match match = new Match(user, score);
                Add(match);
                Sort();
                Truncate();
            }
        }        
        public override string ToString()
        {
            if (matches.Count > 0)
            {  
                var str = "";
                foreach(Match match in matches)
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

    [CreateAssetMenu]
    public class LeaderboardVariable : JsonSerializer<LeaderboardVariable>
    {
        [SerializeField]
        Leaderboard serializable;
        [SerializeField]
        StringVariable username;
        [SerializeField]
        IntVariableCallback count;
        [SerializeField]
        int maxLength = 10;
        [SerializeField]
        string zeroString = "No results";

        public Leaderboard Value
        {
            get 
            {
                if (serializable.matches.Count <= 0 )
                    serializable = GetFromFileOrCreate(() => { return new Leaderboard(maxLength, zeroString); });
                Debug.Log("Getting: " + this.name + ". Value: " + serializable);
                Print();
                return serializable;
            }
            set 
            { 
                if (serializable.matches.Count <= 0 )
                    serializable = GetFromFileOrCreate(() => { return new Leaderboard(maxLength, zeroString); });
                else
                    serializable = value;
                Debug.Log("Setting: " + this.name + ". Value: " + value);
                Save(serializable);
            }
        }
        
        public void Add()
        {            
            Value.Add(username.Value, count.RuntimeValue);
            Save(serializable);
        }

        public void Print()
        {
            Debug.Log(serializable.ToString());
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    
}
