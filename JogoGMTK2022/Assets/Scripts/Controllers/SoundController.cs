using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController sc;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource ambientationSource;


    private void Awake()
    {
        if (sc == null) { sc = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        musicSource.volume = DATA.d.musicVolume / (GameController.gc.isPaused ? 2 : 1);
        SFXSource.volume = DATA.d.SFXVolume / (GameController.gc.isPaused ? 2 : 1);
        ambientationSource.volume = DATA.d.ambientationVolume / (GameController.gc.isPaused ? 2 : 1);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) { SFXSource.PlayOneShot(clip); }
    public void PlayAmbientation(AudioClip clip)
    {
        ambientationSource.clip = clip;
        ambientationSource.Play();
    }
}