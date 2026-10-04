using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;

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

        if (scene.name != "Mainmenu" && scene.name != "Splashscreen")
        {
            musicSource.Stop();
        }
        if (scene.name == "Mainmenu" && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}