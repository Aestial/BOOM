namespace Liquid.Leaderboard
{
    public interface ILeaderboard
    {
        public void Submit(int score);
        public void Retrieve();
    }
}