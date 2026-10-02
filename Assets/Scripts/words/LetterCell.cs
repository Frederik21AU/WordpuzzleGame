using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Image tileImage;
    public Image highlightOverlay;

    [NonSerialized] public int row;
    [NonSerialized] public int col;
    [NonSerialized] public char letter;
    [NonSerialized] public Action<LetterCell> OnDown;
    [NonSerialized] public Action<LetterCell> OnEnter;
    [NonSerialized] public Action<LetterCell> OnUp;

    public void Setup(int r, int c, char letterChar, Sprite sprite)
    {
        row = r;
        col = c;
        letter = letterChar;
        tileImage.sprite = sprite;
    }

    public void OnPointerDown(PointerEventData eventData) => OnDown?.Invoke(this);
    public void OnPointerEnter(PointerEventData eventData) => OnEnter?.Invoke(this);
    public void OnPointerUp(PointerEventData eventData) => OnUp?.Invoke(this);

    public void SetHighlight(Color c) => highlightOverlay.color = c;
}
