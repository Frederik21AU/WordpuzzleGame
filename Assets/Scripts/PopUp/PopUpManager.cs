using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private WordSearchController controller;
    [SerializeField] GameObject popUp_prefab;
    [SerializeField] private Transform popupParent;
    [SerializeField] private float waitTime = 5f;

    private GameObject ActivePopUp;
    private float timer =0f;
    private bool puzzleSolved = false;


    void Start()
    {
        Debug.Log("[PopUpManager] HandlePuzzleSolved received!");
        controller.OnWordFound += ResetTimer;
        controller.OnPuzzleSolved += HandlePuzzleSolved;
    }

   void Update()
    {
        if (puzzleSolved) return;

        timer += Time.deltaTime;

        if (timer >= waitTime && ActivePopUp == null)
        {
            timer = 0f;
            ActivePopUp = Instantiate(popUp_prefab, popupParent);
        }
    }
    
    void HandlePuzzleSolved()
    {
        puzzleSolved = true;
        DespawnPopUp();
    }

    void DespawnPopUp()
    {
        if (ActivePopUp != null)
        {
            Destroy(ActivePopUp);
        }
    }
    void ResetTimer()
    {
        timer = 0f;
    }















}
