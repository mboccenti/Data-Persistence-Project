using Persistent_Data;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Logic
{
    public class GameManager : MonoBehaviour
    {
        [FormerlySerializedAs("BrickPrefab")] public Brick brickPrefab;
        [FormerlySerializedAs("LineCount")] public int lineCount = 6;
        [FormerlySerializedAs("Ball")] public Rigidbody ball;

        [FormerlySerializedAs("UIHandler")] public UIMainSceneHandler uiHandler;
    
        private bool m_Started;
        private int m_Points;
    
        private bool m_GameOver;

        // Start is called before the first frame update
        private void Start()
        {
            const float step = 0.6f;
            int perLine = Mathf.FloorToInt(4.0f / step);
        
            int[] pointCountArray = new [] {1,1,2,2,5,5};
            for (int i = 0; i < lineCount; ++i)
            {
                for (int x = 0; x < perLine; ++x)
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    Brick brick = Instantiate(brickPrefab, position, Quaternion.identity);
                    brick.pointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoint);
                }
            }
        }

        private void Update()
        {
            if (!m_Started)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_Started = true;
                    float randomDirection = Random.Range(-1.0f, 1.0f);
                    Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                    forceDir.Normalize();

                    ball.transform.SetParent(null);
                    ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                }
            }
        }

        private void AddPoint(int point)
        {
            m_Points += point;
            uiHandler.SetScoreText(m_Points);

            if (CachedData.instance.leaderboard.highestTopScore < m_Points)
                uiHandler.SetHighestScoreText(m_Points);
        }

        public void GameOver()
        {
            if (m_GameOver) return;

            GameOverType gameOverType = m_Points > CachedData.instance.leaderboard.lowestTopScore ? GameOverType.TopScore : GameOverType.Default;
            uiHandler.ActivateGameOverMenu(gameOverType);

            if (gameOverType == GameOverType.TopScore)
                uiHandler.OnTopScoreSubmit += SaveScore;

            m_GameOver = true;
        }

        private void SaveScore(string playerName)
        {
            PlayerResult playerResult = new(playerName, m_Points);
            CachedData.instance.SavePlayerResult(playerResult);

            uiHandler.OnTopScoreSubmit -= SaveScore;
        }
    }
}
