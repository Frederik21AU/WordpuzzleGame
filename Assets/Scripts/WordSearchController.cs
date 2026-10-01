using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WordSearchController : MonoBehaviour
{
    [Header("Config")]
    public WordList wordListData;
    public LetterTileDatabase tileDatabase;
    public int rows = 10;
    public int cols = 10;

    [Header("References")]
    public LetterCell cellPrefab;
    public GridLayoutGroup grid;
    public Transform wordListContent;
    public TMP_Text wordListEntryPrefab;
    public TMP_Text statusText;

    [Header("Highlight Colors")]
    public Color normalColor = new Color(0, 0, 0, 0);
    public Color selectingColor = new Color(1f, 0.85f, 0.3f, 0.55f);
    public Color foundColor = new Color(0.4f, 0.9f, 0.4f, 0.55f);

    LetterCell[,] cells;
    List<PlacedWord> placedWords;
    Dictionary<string, TMP_Text> wordListEntries = new();
    HashSet<string> foundWords = new();
    HashSet<(int, int)> foundCells = new();

    bool dragging;
    LetterCell startCell;
    List<LetterCell> currentSelection = new();

    [Header("Win Screen")]
    public GameObject winPanel;

    void Start()
    {
        BuildGrid();
        BuildWordListUI();
        winPanel.SetActive(false);
    }

    void BuildGrid()
    {
        string[] roundWords = PickRandomWords(wordListData.wordPool, wordListData.wordsPerRound);
        char[,] letters = WordSearchGen.Generate(rows, cols, roundWords, out placedWords);

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;

        cells = new LetterCell[rows, cols];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var cell = Instantiate(cellPrefab, grid.transform);
                cell.name = $"Cell_{r}_{c}";
                cell.Setup(r, c, letters[r, c], tileDatabase.GetSprite(letters[r, c]));
                cell.SetHighlight(normalColor);
                cell.OnDown = BeginSelection;
                cell.OnEnter = ExtendSelection;
                cell.OnUp = HandlePointerUp;
                cells[r, c] = cell;
            }
        }
    }
    void HandlePointerUp(LetterCell cell)
    {
        if (dragging) EndSelection();
    }

    string[] PickRandomWords(string[] pool, int count)
    {
        var rng = new System.Random();
        var shuffled = pool.OrderBy(_ => rng.Next()).ToArray();
        int take = Mathf.Min(count, shuffled.Length);
        return shuffled.Take(take).ToArray();
    }

    void BuildWordListUI()
    {
        foreach (var pw in placedWords)
        {
            var entry = Instantiate(wordListEntryPrefab, wordListContent);
            entry.text = pw.word;
            wordListEntries[pw.word] = entry;
        }
        statusText.text = $"Find {placedWords.Count} words";
    }

    void Update()
    {
        if (dragging && Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        EndSelection();
    }

    void BeginSelection(LetterCell cell)
    {
        dragging = true;
        startCell = cell;
        currentSelection = new List<LetterCell> { cell };
        RefreshColors();
    }

    void ExtendSelection(LetterCell cell)
    {
        if (!dragging) return;

        int dRow = cell.row - startCell.row;
        int dCol = cell.col - startCell.col;

        bool straight = dRow == 0 || dCol == 0 || Mathf.Abs(dRow) == Mathf.Abs(dCol);
        if (!straight) return;

        int stepRow = System.Math.Sign(dRow);
        int stepCol = System.Math.Sign(dCol);
        int length = Mathf.Max(Mathf.Abs(dRow), Mathf.Abs(dCol)) + 1;

        var path = new List<LetterCell>();
        for (int i = 0; i < length; i++)
        {
            int r = startCell.row + stepRow * i;
            int c = startCell.col + stepCol * i;
            path.Add(cells[r, c]);
        }

        currentSelection = path;
        RefreshColors();
    }

    void EndSelection()
    {
        dragging = false;

        string selected = string.Concat(currentSelection.Select(c => c.letter));
        string reversed = new string(selected.Reverse().ToArray());

        var match = placedWords.FirstOrDefault(pw =>
            !foundWords.Contains(pw.word) && (pw.word == selected || pw.word == reversed));

        if (match != null)
        {
            foundWords.Add(match.word);
            foreach (var cell in currentSelection)
                foundCells.Add((cell.row, cell.col));

            if (wordListEntries.TryGetValue(match.word, out var entry))
                entry.fontStyle = FontStyles.Strikethrough;

            if (foundWords.Count == placedWords.Count)
            {
                statusText.text = "You found them all! 🎉";
                winPanel.SetActive(true);
            }
            else
            {
                statusText.text = $"Found {foundWords.Count}/{placedWords.Count}";
            }
        }

        currentSelection.Clear();
        RefreshColors();
    }

    void RefreshColors()
    {
        foreach (var cell in cells)
            cell.SetHighlight(foundCells.Contains((cell.row, cell.col)) ? foundColor : normalColor);

        foreach (var cell in currentSelection)
            cell.SetHighlight(selectingColor);
    }
}

