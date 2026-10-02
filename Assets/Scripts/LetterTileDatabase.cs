using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "WordSearch/LetterTileDatabase")]
public class LetterTileDatabase : ScriptableObject
{
    [System.Serializable]
    public class LetterSprite
    {
        public char letter;
        public Sprite sprite;
    }

    public LetterSprite[] letters;
    Dictionary<char, Sprite> lookup;

    private void OnEnable()
    {
        lookup = null;
    }

    private void OnValidate()
    {
        lookup = null;
    }

    public Sprite GetSprite(char c)
    {
        if (lookup == null)
        {
            lookup = new Dictionary<char, Sprite>();
            foreach (var l in letters)
                lookup[l.letter] = l.sprite;
        }
        return lookup.TryGetValue(c, out var s) ? s : null;
    }
}