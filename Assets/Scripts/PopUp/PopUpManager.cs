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
        hintsEnabled = HintSettings.Enabled;
        waitTime = HintSettings.Delay;

        Debug.Log("PopUpManager started: hints " + hintsEnabled + ", delay " + waitTime);

        controller.OnWordFound += ResetTimer;
        controller.OnPuzzleSolved += HandlePuzzleSolved;
    }

    void Update()
    {
        if (!hintsEnabled)
        {
            if (Time.frameCount % 120 == 0) Debug.Log("PopUpManager: hints are off");
            return;
        }

        if (puzzleSolved)
        {
            if (Time.frameCount % 120 == 0) Debug.Log("PopUpManager: puzzle already solved");
            return;
        }

        timer += Time.deltaTime;

        if (Time.frameCount % 120 == 0)
        {
            Debug.Log("timer " + timer + " / " + waitTime + ", popup: " + activePopUp);
        }

        if (timer >= waitTime && activePopUp == null)
        {
            Debug.Log("Spawning popup");

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