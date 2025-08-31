using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clip")]
    public AudioClip mainMenuMusic;

    private void Start()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
