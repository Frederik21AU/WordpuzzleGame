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
        Debug.Log("PopUpManager started. waitTime = " + waitTime);
        controller.OnWordFound += ResetTimer;
        controller.OnPuzzleSolved += HandlePuzzleSolved;
    }

   void Update()
    {
        if (puzzleSolved) return;

        timer += Time.deltaTime;

        if (timer >= waitTime && ActivePopUp == null)
        {
            Debug.Log("Spawning popup");
            timer = 0f;
            ActivePopUp = Instantiate(popUp_prefab, popupParent);
            PopUp popUP = ActivePopUp.GetComponentInChildren<PopUp>();
            
            if (popUP != null)
            {
                popUP.SetHint(controller.wordListData.LevelHint);
            }
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
