using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;

    [SerializeField] private string[] musicScenes = {"Splashscreen", "Mainmenu", "Overworld"};
    private static MusicManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (musicSource == null) return;

        if (System.Array.IndexOf(musicScenes, scene.name) >= 0)
        {
            if (!musicSource.isPlaying)
            {
                musicSource.UnPause();
                if (!musicSource.isPlaying) musicSource.Play();
            }
        }
        else
        {
            musicSource.Pause();
        }
    }
}