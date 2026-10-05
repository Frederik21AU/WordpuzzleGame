using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectButton : MonoBehaviour
{
    public static int SelectedLocation = -1;

    [SerializeField] private int locationIndex;
    [SerializeField] private string overworldSceneName = "Overworld";
    [SerializeField] private float loadDelay = 0.2f;

    public void Click()
    {
        SelectedLocation = locationIndex;
        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(overworldSceneName);
    }
}
