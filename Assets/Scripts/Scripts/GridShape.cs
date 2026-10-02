using UnityEngine;

[CreateAssetMenu(fileName = "NewGridShape", menuName = "WordSearch/Grid Shape")]
public class GridShape : ScriptableObject
{
    [Tooltip("X = usable cell, . = dead cell")]
    public string[] layout;

    public bool IsActive(int row, int col)
    {
        if (layout == null)
            return false;

        if (row < 0 || row >= layout.Length)
            return false;

        if (col < 0 || col >= layout[row].Length)
            return false;

        return layout[row][col] == 'X';
    }
}
