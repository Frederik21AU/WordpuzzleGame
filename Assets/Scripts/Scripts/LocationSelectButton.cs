using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectButton : MonoBehaviour
{
    public static int selectedLocation = 0;

    [SerializeField] private int LocationIndex;
    [SerializeField] private string overworldSceneName = "Overworld";
    [SerializeField] private float loadDelay = 0.2f;

    public void Click()
    {
        selectedLocation = LocationIndex;
        SceneManager.LoadScene(overworldSceneName);
    }

    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(overworldSceneName);
    }
    
}
