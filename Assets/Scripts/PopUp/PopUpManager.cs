using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private WordSearchController controller;
    [SerializeField] GameObject popUp_prefab;
    [SerializeField] private Transform popupParent;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private Transform popUpParent;

    private GameObject ActivePopUp;
    private float timer =0f;


    void start()
    {
        controller.OnWordFound += ResetTimer;
    }

   void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime && ActivePopUp == null)
        {
            timer = 0f;
            ActivePopUp = Instantiate(popUp_prefab, popUpParent);
        }
    }
    
    void ResetTimer()
    {
        timer = 0f;
    }















}
