using Persistent_Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [DefaultExecutionOrder(1000)]
    public class UIMenuHandler : MonoBehaviour
    {
        [SerializeField] private Transform playerResultsContainer;
        [SerializeField] private Text playerResultPrefab;

        private void Start() => DrawLeaderboard();

        public void LoadMainScene() => SceneManager.LoadScene(1);

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }

        private void DrawLeaderboard()
        {
            PlayerResult[] playerResults = CachedData.instance.leaderboard.playerResults;

            for (int i = 0; i < playerResults.Length; i++)
            {
                Text playerResultText = Instantiate(playerResultPrefab, playerResultsContainer);
                playerResultText.text = $"{i + 1}. {playerResults[i].playerName} - {playerResults[i].score}";
            }
        }
    }
}