using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown gameModeDropdown;

    void Start()
    {
        gameModeDropdown.value = (int)GameModeController.instance.GetSelectedGameMode();
        gameModeDropdown.onValueChanged.AddListener(OnGameModeChanged);
    }

    private void OnGameModeChanged(int modeIndex)
    { 
        GameModeController.instance.SetSelectedGameMode((GameModeDifficulty)modeIndex);
    }
    
     public void OnStartClicked()
    {
        // Load Game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
