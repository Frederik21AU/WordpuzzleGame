using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource soundEffectSource;

    [Header("Music")]
    public AudioClip music;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    [Header("UI Sounds")]
    public AudioClip buttonClickSound;
    [Range(0f, 1f)]
    public float soundEffectVolume = 1f;

    private void Start()
    {
        if (music != null)
        {
            musicSource.clip = music;
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayButtonClick()
    {
        Debug.Log("PlayButtonCLick called");
        if (buttonClickSound != null)
        {
            soundEffectSource.PlayOneShot(buttonClickSound, soundEffectVolume);
        }
    }
}
