using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldNavigator : MonoBehaviour
{
    public GameObject[] Locations;

    private int currentLocation = 0;

    private void Start()
    {
        currentLocation = Mathf.Clamp(LocationSelectButton.selectedLocation, 0, Locations.Length - 1);
        LocationSelectButton.selectedLocation = 0;
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
            SceneManager.LoadScene("Mainmenu");
        }
    }

    private void ShowLocation()
    {
        for (int i = 0; i < Locations.Length; i++)
        {
            Locations[i].SetActive(i == currentLocation);
        }
    }
}