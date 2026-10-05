using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneLoad : MonoBehaviour
{
    [SerializeField] private float delay = 0.2f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAfterDelay(sceneName));
    }

    private IEnumerator LoadAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
}
