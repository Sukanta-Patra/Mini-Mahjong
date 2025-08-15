using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private TMP_Dropdown gameModeDropdown;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    void Start()
    {
        UpdateBestScoreText();

        difficultyDropdown.value = (int)GameModeController.Instance.GetSelectedDifficulty();
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        difficultyDropdown.onValueChanged.AddListener(UpdateBestScoreText);
    
        gameModeDropdown.value = GameModeController.Instance.GetIsTurnBasedGameMode() ? 0 : 1;
        gameModeDropdown.onValueChanged.AddListener(OnGameModeChanged);
        gameModeDropdown.onValueChanged.AddListener(UpdateBestScoreText);
    }

    private void OnDifficultyChanged(int difficultyIndex) => GameModeController.Instance.SetSelectedDifficulty((GameModeDifficulty)difficultyIndex);

    private void OnGameModeChanged(int modeIndex) => GameModeController.Instance.SetIsTurnBasedGameMode(modeIndex == 0? true : false);


    private void UpdateBestScoreText(int _ = 0)
    {
        AudioManager.Instance.PlayButtonClickSound();
        int score = SaveManager.Instance.GetScore(GameModeController.Instance.GetSelectedDifficulty(), GameModeController.Instance.GetIsTurnBasedGameMode());
        bestScoreText.text = $"Best Score ({GameModeController.Instance.GetSelectedDifficulty().ToString()} - " + (GameModeController.Instance.GetIsTurnBasedGameMode() ? "Turn Based" : "Time Based") + $") : {score}";
    }

     public void OnStartClicked()
    {
        AudioManager.Instance.PlayButtonClickSound();
        // Load Game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
