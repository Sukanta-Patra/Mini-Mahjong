using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private TMP_Dropdown gameModeDropdown;

    void Start()
    {
        difficultyDropdown.value = (int)GameModeController.Instance.GetSelectedDifficulty();
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
    
        gameModeDropdown.value = GameModeController.Instance.GetIsTurnBasedGameMode() ? 0 : 1;
        gameModeDropdown.onValueChanged.AddListener(OnGameModeChanged);
    }

    private void OnDifficultyChanged(int difficultyIndex) => GameModeController.Instance.SetSelectedDifficulty((GameModeDifficulty)difficultyIndex);

    private void OnGameModeChanged(int modeIndex) => GameModeController.Instance.SetIsTurnBasedGameMode(modeIndex == 0? true : false);

    
     public void OnStartClicked()
    {
        // Load Game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
