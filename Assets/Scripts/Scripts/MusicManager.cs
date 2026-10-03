using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    
    private static MusicManager instance;

   void Awake()
{
    AudioListener[] allListeners = FindObjectsOfType<AudioListener>();
Debug.Log("Number of AudioListeners found: " + allListeners.Length);
foreach (var listener in allListeners)
{
    Debug.Log("Listener on: " + listener.gameObject.name);
}


    if (instance != null && instance != this)
    {
        Debug.Log("Duplicate found, destroying self");
        Destroy(gameObject);
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);
    Debug.Log("Became instance, DontDestroyOnLoad called");

    SceneManager.sceneLoaded += OnSceneLoaded;

    if (musicSource != null)
    {
        Debug.Log("musicSource is assigned, calling Play()");
        musicSource.Play();
    }
    else
    {
        Debug.Log("musicSource is NULL - nothing will play");
    }
}




void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
{
    Debug.Log("Scene loaded: " + scene.name);

    AudioListener[] allListeners = FindObjectsOfType<AudioListener>();
    Debug.Log("Listeners after loading " + scene.name + ": " + allListeners.Length);
    foreach (var listener in allListeners)
    {
        Debug.Log("Listener on: " + listener.gameObject.name);
    }

    if (scene.name != "Mainmenu" && scene.name != "Splashscreen")
    {
        musicSource.Stop();
    }
}
}