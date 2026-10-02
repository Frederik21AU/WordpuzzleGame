using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacedWord
{
    public string word;
    public int row, col;
    public Vector2Int dir;
}

public static class WordSearchGen
{
    static readonly Vector2Int[] Directions =
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
    };

    public static char[,] Generate(int rows, int cols, string[] words, out List<PlacedWord> placed, int maxAttemptsPerWord = 300)
    {
        var grid = new char[rows, cols];
        placed = new List<PlacedWord>();
        var rng = new System.Random();

        var sorted = words.OrderByDescending(w => w.Length).ToArray();

        foreach (var raw in sorted)
        {
            string word = raw.Trim().ToUpper();
            if (word.Length == 0) continue;
            bool placedOk = false;

            for (int attempt = 0; attempt < maxAttemptsPerWord && !placedOk; attempt++)
            {
                var dir = Directions[rng.Next(Directions.Length)];
                int startRow = rng.Next(rows);
                int startCol = rng.Next(cols);

                int endRow = startRow + dir.x * (word.Length - 1);
                int endCol = startCol + dir.y * (word.Length - 1);
                if (endRow < 0 || endRow >= rows || endCol < 0 || endCol >= cols) continue;

                bool fits = true;
                for (int i = 0; i < word.Length; i++)
                {
                    int r = startRow + dir.x * i;
                    int c = startCol + dir.y * i;
                    char existing = grid[r, c];
                    if (existing != '\0' && existing != word[i]) { fits = false; break; }
                }
                if (!fits) continue;

                for (int i = 0; i < word.Length; i++)
                {
                    int r = startRow + dir.x * i;
                    int c = startCol + dir.y * i;
                    grid[r, c] = word[i];
                }

                placed.Add(new PlacedWord { word = word, row = startRow, col = startCol, dir = dir });
                placedOk = true;
            }

            if (!placedOk)
                Debug.LogWarning($"Could not place word: {word} (grid too small or too many words)");
        }

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (grid[r, c] == '\0')
                    grid[r, c] = (char)('A' + rng.Next(26));

        return grid;
    }
}

