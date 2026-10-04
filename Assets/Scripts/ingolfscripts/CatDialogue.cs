using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatDialogue : MonoBehaviour
{
    [Header("Cat")]
    [SerializeField] private Image catImage;
    [SerializeField] private Sprite mouthClosedSprite;
    [SerializeField] private Sprite mouthOpenSprite;
    [SerializeField] private float mouthSwitchTime = 0.15f;

    [Header("Bubble")]
    [SerializeField] private GameObject bubble;
    [SerializeField] private TMP_Text bubbleText;
    [TextArea] [SerializeField] private string message;
    [SerializeField] private float charsPerSecond = 30f;
    [SerializeField] private float popInTime = 0.25f;

    private Coroutine speakRoutine;

    void OnEnable()
    {
        Speak(message);
    }

    public void Speak(string text)
    {
        if (speakRoutine != null) StopCoroutine(speakRoutine);
        speakRoutine = StartCoroutine(SpeakRoutine(text));
    }

    private IEnumerator SpeakRoutine(string text)
    {
        bubble.SetActive(true);
        bubble.transform.localScale = Vector3.zero;
        bubbleText.text = text;
        bubbleText.ForceMeshUpdate();
        bubbleText.maxVisibleCharacters = 0;

        int total = bubbleText.textInfo.characterCount;
        float elapsed = 0f;
        float shown = 0f;
        float mouthTimer = 0f;
        bool open = false;

        while (shown < total || elapsed < popInTime)
        {
            elapsed += Time.deltaTime;

            // Bubble grows in (smooth start and end)
            float p = Mathf.Clamp01(elapsed / popInTime);
            bubble.transform.localScale = Vector3.one * (p * p * (3f - 2f * p));

            // Text starts typing once the bubble has appeared
            if (elapsed >= popInTime)
            {
                shown += charsPerSecond * Time.deltaTime;
                bubbleText.maxVisibleCharacters = Mathf.Min((int)shown, total);
            }

            // Mouth flaps from the very start
            mouthTimer += Time.deltaTime;
            if (mouthTimer >= mouthSwitchTime)
            {
                mouthTimer = 0f;
                open = !open;
                catImage.sprite = open ? mouthOpenSprite : mouthClosedSprite;
            }

            yield return null;
        }

        bubble.transform.localScale = Vector3.one;
        catImage.sprite = mouthClosedSprite;
    }
}
