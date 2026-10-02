using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text hintText;

    void Start()
    {

    }
    public void OnClick()
    {
        Destroy(gameObject);
    }

    public void OnClicked()
    {
        Debug.Log("Cat is clicked1");
        hintText.gameObject.SetActive(true);
        
        //brug dette til at slette teksen efter tid
        Destroy(hintText, 10f);

        //brug dette til at slette billedet efter tid
        Destroy(gameObject, 20f);
    }
}
