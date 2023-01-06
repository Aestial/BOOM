using System.Collections;
using UnityEngine;
using LootLocker.Requests;

namespace Liquid.LootLocker
{
    using Events;
    using Variables;
    
    [System.Serializable]
    public class PlayerData
    {
        public string playerId;
        public string publicUid;
        public PlayerData(string playerId, string publicUid)
        {
            this.playerId = playerId;
            this.publicUid = publicUid;
        }
    }
    public class LootLockerManager : MonoBehaviour
    {
        public LootLockerLeaderboard leaderboard;
        public StringVariable username;

        [SerializeField]
        private StringEvent onGuestSessionStarted;

        void Start()
        {
            StartCoroutine(SetupRoutine());
        }

        IEnumerator SetupRoutine()
        {
            yield return LoginRoutine();
            yield return SetPlayerName(username.Value);
            yield return leaderboard.RetrieveListRoutine();
        }

        IEnumerator LoginRoutine()
        {
            bool done = false;
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    PlayerData guestData = new PlayerData(
                        response.player_id.ToString(),
                        response.public_uid
                    );
                    PlayerPrefs.SetString("PlayerID", guestData.playerId);
                    PlayerPrefs.SetString("PlayerUID", guestData.publicUid);
                    Debug.Log("Successfully started LootLocker session: " + guestData);
                    onGuestSessionStarted.Invoke(guestData.playerId);
                    done = true;
                }
                else
                {
                    Debug.Log("Error starting LootLocker session");
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }

        IEnumerator SetPlayerName(string name)
        {
            bool done = false;
            LootLockerSDKManager.SetPlayerName(name, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully set player name in LootLocker" + response.name);
                    done = true;
                }
                else
                {
                    Debug.Log("Error setting player name in LootLocker");
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }
    }
}


