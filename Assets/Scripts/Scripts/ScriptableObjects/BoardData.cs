using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewBoardData", menuName = "Word Search")]

public class BoardData : ScriptableObject
{
    public enum WordDirection
    {
        Right,
        Left,
        Up,
        Down,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }
    
    [System.Serializable]
    public class SearchingWord
    {
        public string Word;

        public int StartColumn;     // X start position in grid
        public int StartRow;        // Y start position in grid

        public int EndColumn;       // X end position in grid
        public int EndRow;          // Y end position in grid

        public WordDirection Direction;
    }

    public Vector2Int GetDirectionOffset(WordDirection direction)
    {
        if (direction == WordDirection.Right)
        {
            return new Vector2Int(1, 0);
        }

        if (direction == WordDirection.Left)
        {
            return new Vector2Int(-1, 0);
        }

        if (direction == WordDirection.Down)
        {
            return new Vector2Int(0, 1);
        }

        if (direction == WordDirection.Up)
        {
            return new Vector2Int(0, -1);
        }

        if (direction == WordDirection.UpRight)
        {
            return new Vector2Int(1, -1);
        }

        if (direction == WordDirection.UpLeft)
        {
            return new Vector2Int(-1, -1);
        }

        if (direction == WordDirection.DownRight)
        {
            return new Vector2Int(1, 1);
        }

        if (direction == WordDirection.DownLeft)
        {
            return new Vector2Int(-1, 1);
        }

        return Vector2Int.zero;
    }

    public SearchingWord[] Words;   // An array of our valid words

    [System.Serializable]
    public class BoardRow
    {
        public int Size;
        public string[] Row;

        public BoardRow() {}
        public BoardRow(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            Size = size;
            Row = new string[Size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }
    }

    public int Columns = 0;
    public int Rows = 0;

    public BoardRow[] Board;

    public void ClearWithEmptyString()
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }
    }
}
