using UnityEngine;

public class OverworldNavigator : MonoBehaviour
{
    public GameObject[] Locations;

    private int currentLocation = 0;

    private void Start()
    {
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
    }

    private void ShowLocation()
    {
        for (int i = 0; i < Locations.Length; i++)
        {
            Locations[i].SetActive(i == currentLocation);
        }
    }
}