using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeController : MonoBehaviour
{
    public static GameModeController Instance;

    [SerializeField] private GameModeDifficulty difficultySelected = GameModeDifficulty.Easy; // Default to Easy mode
    [SerializeField] private bool isTurnBasedGameMode = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool GetIsTurnBasedGameMode() => isTurnBasedGameMode;
    public void SetIsTurnBasedGameMode(bool isTurnBased) => isTurnBasedGameMode = isTurnBased;

    public GameModeDifficulty GetSelectedDifficulty() => difficultySelected;
    public void SetSelectedDifficulty(GameModeDifficulty difficulty) => difficultySelected = difficulty;

}
