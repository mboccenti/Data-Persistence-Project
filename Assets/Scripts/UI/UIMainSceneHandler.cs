using Persistent_Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [DefaultExecutionOrder(1000)]
    public class UIMainSceneHandler : MonoBehaviour
    {
        [SerializeField] private Button[] topScoreSubmitButtons;
        [SerializeField] private Text playerScoreValueText;
        [SerializeField] private Text highestScoreValueText;
        [SerializeField] private InputField playerNameInputField;
        [SerializeField] private GameObject gameOverMenuDefault;
        [SerializeField] private GameObject gameOverMenuTopScore;

        public System.Action<string> OnTopScoreSubmit;

        private void Start()
        {
            LoadHighestScoreText();

            foreach (Button t in topScoreSubmitButtons)
                t.onClick.AddListener(SubmitPlayerName);
        }

        private void OnDestroy()
        {
            foreach (Button t in topScoreSubmitButtons)
                t.onClick.RemoveAllListeners();
        }

        private void SubmitPlayerName() => OnTopScoreSubmit.Invoke(playerNameInputField.text);

        public void LoadMenu() => SceneManager.LoadScene(0);

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void SwitchSubmitButtons(string value)
        {
            foreach (Button t in topScoreSubmitButtons)
            {
                t.gameObject.SetActive(value.Length != 0);
            }
        }

        public void SetScoreText(int value) => playerScoreValueText.text = $"Score : {value}";

        public void SetHighestScoreText(int value)
        {
            if (value > 0)
                highestScoreValueText.gameObject.SetActive(true);

            highestScoreValueText.text = $"Highest Score : {value}";
        }

        private void LoadHighestScoreText() => SetHighestScoreText(CachedData.instance.leaderboard.highestTopScore);

        public void ActivateGameOverMenu(GameOverType gameOverType)
        {
            switch (gameOverType)
            {
                case GameOverType.Default:
                    gameOverMenuDefault.SetActive(true);
                    break;
                case GameOverType.TopScore:
                    gameOverMenuTopScore.SetActive(true);
                    break;
            }
        }
    }

    public enum GameOverType { Default, TopScore }
}