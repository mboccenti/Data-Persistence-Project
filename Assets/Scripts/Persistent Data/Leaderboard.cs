using UnityEngine.Serialization;

namespace Persistent_Data
{
    public class Leaderboard
    {
        public int highestTopScore { get; private set; }
        public int lowestTopScore { get; private set; }

        private const int Size = 3;
    
        public PlayerResult[] playerResults { get; private set; }

        public Leaderboard()
        {
            playerResults = new PlayerResult[Size];

            for (int i = 0; i < Size; i++)
                playerResults[i] = new();

            lowestTopScore = 0;
            highestTopScore = 0;
        }

        public Leaderboard(PlayerResult[] playerResults)
        {
            this.playerResults = playerResults;

            for (int i = 0; i < Size; i++)
            {
                if (this.playerResults[i].score > highestTopScore)
                    highestTopScore = this.playerResults[i].score;

                if(this.playerResults[i].score < lowestTopScore)
                    lowestTopScore = this.playerResults[i].score;
            }
        }

        public bool ApplyToLeaderboard(PlayerResult playerResult)
        {
            if (playerResult.score <= lowestTopScore) return false;
            
            playerResults[Size - 1] = playerResult;
            lowestTopScore = playerResult.score;

            if (playerResult.score > highestTopScore)
                highestTopScore = playerResult.score;

            for (int i = Size - 2; i >= 0; i--)
            {
                if (playerResults[i].score < lowestTopScore)
                    lowestTopScore = playerResults[i].score;

                if (playerResults[i].score < playerResult.score)
                    (playerResults[i], playerResults[i + 1]) = (playerResults[i + 1], playerResults[i]);
                else break;
            }
            
            return true;
        }
    }

    [System.Serializable]
    public class PlayerResult
    {
        [FormerlySerializedAs("PlayerName")] public string playerName;
        [FormerlySerializedAs("Score")] public int score;

        public PlayerResult()
        {
            playerName = "---//---//---";
            score = 0;
        }

        public PlayerResult(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }
}