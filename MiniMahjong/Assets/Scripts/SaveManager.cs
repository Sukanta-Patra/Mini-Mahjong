using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance;

    private const string easyTurnKey = "Easy-Turn";
    private const string easyTimeKey = "Easy-Time";
    private const string mediumTurnKey = "Medium-Turn";
    private const string mediumTimeKey = "Medium-Time";
    private const string seriousTurnKey = "Serious-Turn";
    private const string seriousTimeKey = "Serious-Time";

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

    public void SaveScore(GameModeDifficulty difficulty, bool isTurnBased, int score)
    {
        if (isTurnBased)
        {
            switch (difficulty)
            {
                case GameModeDifficulty.Easy:
                    PlayerPrefs.SetInt(easyTurnKey, score);
                    break;
                case GameModeDifficulty.Medium:
                    PlayerPrefs.SetInt(mediumTurnKey, score);
                    break;
                case GameModeDifficulty.Serious:
                    PlayerPrefs.SetInt(seriousTurnKey, score);
                    break;
            }
        }
        else
        {
            switch (difficulty)
            {
                case GameModeDifficulty.Easy:
                    PlayerPrefs.SetInt(easyTimeKey, score);
                    break;
                case GameModeDifficulty.Medium:
                    PlayerPrefs.SetInt(mediumTimeKey, score);
                    break;
                case GameModeDifficulty.Serious:
                    PlayerPrefs.SetInt(seriousTimeKey, score);
                    break;
            }
        }
    }
    
    public int GetScore(GameModeDifficulty difficulty, bool isTurnBased)
    {
        if (isTurnBased)
        {
            switch (difficulty)
            {
                case GameModeDifficulty.Easy:
                    return PlayerPrefs.GetInt(easyTurnKey, 0);
                case GameModeDifficulty.Medium:
                    return PlayerPrefs.GetInt(mediumTurnKey, 0);
                case GameModeDifficulty.Serious:
                    return PlayerPrefs.GetInt(seriousTurnKey, 0);
            }
        }
        else
        {
            switch (difficulty)
            {
                case GameModeDifficulty.Easy:
                    return PlayerPrefs.GetInt(easyTimeKey, 0);
                case GameModeDifficulty.Medium:
                    return PlayerPrefs.GetInt(mediumTimeKey, 0);
                case GameModeDifficulty.Serious:
                    return PlayerPrefs.GetInt(seriousTimeKey, 0);
            }
        }
        return 0;
    }

}
