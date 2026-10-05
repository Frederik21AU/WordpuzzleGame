using UnityEngine;

public class WordListVisibility : MonoBehaviour
{
    [SerializeField] private GameObject wordListPanel;

    void Awake()
    {
        Debug.Log("Word list setting: " + HintSettings.showWordList);
        wordListPanel.SetActive(HintSettings.showWordList);
    }
}
