using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Persistent_Data
{
    public class CachedData : MonoBehaviour
    {
        public static CachedData instance { get; private set; }

        public Leaderboard leaderboard { get; private set; }

        private void Awake()
        {
            InitSingleton();
            LoadCachedData();
        }

        private void InitSingleton()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void LoadCachedData()
        {
            string saveFilePath = Application.persistentDataPath + "/savedfile.json";

            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                PlayerResultGroup playerResultGroup = JsonUtility.FromJson<PlayerResultGroup>(json);
                leaderboard = new Leaderboard(playerResultGroup.playerResults);
            }
            else
                leaderboard = new Leaderboard();
        }

        public void SavePlayerResult(PlayerResult playerResult)
        {
            bool isApplied = leaderboard.ApplyToLeaderboard(playerResult);

            if (!isApplied) return;
        
            string saveFilePath = Application.persistentDataPath + "/savedfile.json";
            PlayerResultGroup playerResultGroup = new(leaderboard.playerResults);
            string json = JsonUtility.ToJson(playerResultGroup);
            File.WriteAllText(saveFilePath, json);
        }
    }

    [System.Serializable]
    public class PlayerResultGroup
    {
        [FormerlySerializedAs("PlayerResults")] public PlayerResult[] playerResults;

        public PlayerResultGroup(PlayerResult[] playerResults) => this.playerResults = playerResults;
    }
}