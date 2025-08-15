using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip cardFlipSound;
    [SerializeField] private AudioClip matchFoundSound;
    [SerializeField] private AudioClip mismatchSound;
    [SerializeField] private AudioClip gameOverSound;

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

    public void PlayCardFlipSound() => audioSource.PlayOneShot(cardFlipSound);
    public void PlayMatchFoundSound() => audioSource.PlayOneShot(matchFoundSound);
    public void PlayGameOverSound() => audioSource.PlayOneShot(gameOverSound);
    public void PlayButtonClickSound() => audioSource.PlayOneShot(buttonClickSound);  
    public void PlayMismatchSound() => audioSource.PlayOneShot(mismatchSound);  

}
