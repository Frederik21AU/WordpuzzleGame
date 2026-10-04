using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectButton : MonoBehaviour
{
    public static int selectedLocation = 0;

    [SerializeField] private int LocationIndex;
    [SerializeField] private string overworldSceneName = "Overworld";

    public void Click()
    {
        selectedLocation = LocationIndex;
        SceneManager.LoadScene(overworldSceneName);
    }
    
}
