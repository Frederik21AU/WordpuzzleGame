using UnityEngine;

[CreateAssetMenu(menuName = "WordSearch/WordList")]
public class WordList : ScriptableObject
{
    [Tooltip("All possible words for this level")]
    public string[] wordPool;

    [Tooltip(" How many words to randomly pick each playthrough")]
    public int wordsPerRound = 8;   
}