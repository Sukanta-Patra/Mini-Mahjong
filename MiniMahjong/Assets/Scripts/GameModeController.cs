using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeController : MonoBehaviour
{
    public static GameModeController instance;

    [SerializeField] private GameModeDifficulty gameModeSelected = GameModeDifficulty.Easy; // Default to Easy mode

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameModeDifficulty GetSelectedGameMode() => gameModeSelected;
    public void SetSelectedGameMode(GameModeDifficulty mode) => gameModeSelected = mode;

}
