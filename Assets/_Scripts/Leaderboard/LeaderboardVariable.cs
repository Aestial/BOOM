using UnityEngine;

namespace Liquid.Variables
{
    using Leaderboard;
    
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