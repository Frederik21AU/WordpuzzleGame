using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private WordSearchController controller;
    [SerializeField] private GameObject popUp_prefab;
    [SerializeField] private Transform popupParent;

    private GameObject activePopUp;
    private float timer = 0f;
    private bool hintsEnabled;
    private float waitTime;
    private bool puzzleSolved = false;

    void Start()
    {
        Debug.Log("Hints enabled:" + hintsEnabled + ", delay: " + waitTime);
        hintsEnabled = HintSettings.Enabled;
        waitTime = HintSettings.Delay;

        controller.OnWordFound += ResetTimer;
        controller.OnPuzzleSolved += HandlePuzzleSolved;
    }

    void Update()
    {
        if (!hintsEnabled || puzzleSolved) return;

        timer += Time.deltaTime;

        if (timer >= waitTime && activePopUp == null)
        {
            timer = 0f;
            activePopUp = Instantiate(popUp_prefab, popupParent);

            PopUp popUp = activePopUp.GetComponentInChildren<PopUp>();
            if (popUp != null)
            {
                popUp.SetHint(controller.wordListData.LevelHint);
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
        if (activePopUp != null)
        {
            Destroy(activePopUp);
        }
    }

    void ResetTimer()
    {
        timer = 0f;
    }
}