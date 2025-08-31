using UnityEngine;

public class AudioManagerGame : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXsource;
    [SerializeField] AudioSource loopSFXsource;

    [Header("Audio Clip")]
    public AudioClip backgroundMusic1;
    public AudioClip backgroundMusic2;
    public AudioClip backgroundMusic3;
    public AudioClip brodSeZabioUzid;
    public AudioClip camac;
    public AudioClip gotovoUpecaneSveRibe;
    public AudioClip uhvacenaRiba;
    public AudioClip upecalaSeRiba;

    private AudioClip[] musicPlaylist;
    private int currentMusicIndex = 0;

    public static AudioManagerGame instance { get; private set; }


    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicPlaylist = new AudioClip[] { backgroundMusic1, backgroundMusic2, backgroundMusic3 };
        PlayNextTrack();
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicPlaylist.Length == 0) return;

        musicSource.clip = musicPlaylist[currentMusicIndex];
        musicSource.Play();

        currentMusicIndex = (currentMusicIndex + 1) % musicPlaylist.Length;
    }

    public static void PlaySFX(AudioClip clip)
    {
        if (instance != null && clip != null)
            instance.SFXsource.PlayOneShot(clip);
    }

    public static void PlayLoopSFX(AudioClip clip)
    {
        if (instance != null && clip != null)
        {
            if (instance.loopSFXsource.isPlaying && instance.loopSFXsource.clip == clip) return;

            instance.loopSFXsource.clip = clip;
            instance.loopSFXsource.loop = true;
            instance.loopSFXsource.Play();
        }
    }

    public static void StopLoopSFX()
    {
        if (instance != null)
            instance.loopSFXsource.Stop();
    }
}
