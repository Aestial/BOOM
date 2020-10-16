using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardLoader : Loader<LeaderboardLoader>
    {
        Leaderboard leaderboard;
        LeaderboardController controller;
        
        void Start() 
        {
            leaderboard = base.GetFromFileOrCreate(Create);
        }
        
        void OnApplicationQuit()
        {
            Save(leaderboard);
        }
        
        private Leaderboard Create()
        {
            return new Leaderboard();
        }
    }
}
