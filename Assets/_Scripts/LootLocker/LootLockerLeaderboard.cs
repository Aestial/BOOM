using System.Collections;
using UnityEngine;
using LootLocker.Requests;

namespace Liquid.LootLocker
{
    using Events;
    using Leaderboard;
    public class LootLockerLeaderboard : MonoBehaviour, ILeaderboard
    {
        public IntVariableCallback count;
        [SerializeField]
        private string m_LeaderboardId;
        [SerializeField]
        private int m_LeaderCount = 10;
        [SerializeField]
        private StringEvent onLeaderboardRetrieved;
        private string m_LeaderboardString;

        public void Add()
        {
            StartCoroutine(AddRoutine());
        }
        public void Submit(int score)
        {
            StartCoroutine(SubmitScoreRoutine(score));
        }
        public void Retrieve()
        {
            StartCoroutine(RetrieveListRoutine());
        }

        IEnumerator AddRoutine()
        {
            yield return SubmitScoreRoutine(count.RuntimeValue);
            yield return RetrieveListRoutine();
        }
        IEnumerator SubmitScoreRoutine(int score)
        {
            bool done = false;
            string playerId = PlayerPrefs.GetString("PlayerID");
            LootLockerSDKManager.SubmitScore(playerId, score, m_LeaderboardId, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully submitted score " + score + " of " + response.member_id);
                    done = true;
                }
                else
                {
                    Debug.Log("Error submitting score. " + response.Error.ToString());
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }
        public IEnumerator RetrieveListRoutine()
        {
            bool done = false;
            LootLockerSDKManager.GetScoreList(m_LeaderboardId, m_LeaderCount, 0, (response) =>
            {
                if (response.success)
                {
                    m_LeaderboardString = "";
                    LootLockerLeaderboardMember[] members = response.items;
                    Debug.Log("Successfully retrieved score list with " + members.Length + " elements.");
                    for (int i = 0; i < members.Length; i++)
                    {
                        var str = members[i].rank + ". ";
                        if (members[i].player.name != "")
                        {
                            str += members[i].player.name;
                        }
                        else
                        {
                            str += members[i].player.id;
                        }
                        str += "\t" + members[i].score + "\n";
                        m_LeaderboardString += str;
                    }
                    Debug.Log(m_LeaderboardString);
                    onLeaderboardRetrieved.Invoke(m_LeaderboardString);
                    done = true;
                }
                else
                {
                    Debug.Log("Error getting score list from: " + m_LeaderboardId);
                    Debug.Log("Error details: " + response.Error);
                    done = true;
                }
            });
            yield return new WaitUntil(() => done == false);
        }
    }

}