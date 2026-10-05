using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldNavigator : MonoBehaviour
{
    public GameObject[] Locations;

    [SerializeField] private string menuSceneName = "Mainmenu";

    public static int LastLocation = 0;

    private int currentLocation = 0;

    private void Start()
    {
        int chosen = LocationSelectButton.SelectedLocation;

        currentLocation = chosen >= 0 ? chosen : LastLocation;
        currentLocation = Mathf.Clamp(currentLocation, 0, Locations.Length - 1);

        LocationSelectButton.SelectedLocation = -1;

        ShowLocation();
    }

    public void NextLocation()
    {
        if (currentLocation < Locations.Length - 1)
        {
            currentLocation++;
            ShowLocation();
        }
    }

    public void PreviousLocation()
    {
        if (currentLocation > 0)
        {
            currentLocation--;
            ShowLocation();
        }
        else
        {
            LastLocation = 0; 
            SceneManager.LoadScene(menuSceneName);
        }
    }

    private void ShowLocation()
    {
        for (int i = 0; i < Locations.Length; i++)
        {
            Locations[i].SetActive(i == currentLocation);
        }

        LastLocation = currentLocation;
    }
}