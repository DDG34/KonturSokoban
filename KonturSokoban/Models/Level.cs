namespace KonturSokoban.Models;

public sealed class Level
{
    private readonly Cell[,] _cells;

    public int Width { get; }
    public int Height { get; }
    public int MoveCount { get; private set; }
    public GridPosition PlayerPosition { get; private set; }

    public Level(Cell[,] cells, GridPosition playerPosition)
    {
        _cells = (Cell[,])cells.Clone();
        Height = _cells.GetLength(0);
        Width = _cells.GetLength(1);
        PlayerPosition = playerPosition;
        MoveCount = 0;
    }

    public bool IsInside(GridPosition position) =>
        position.X >= 0 && position.X < Width &&
        position.Y >= 0 && position.Y < Height;

    public Cell GetCell(GridPosition position) =>
        _cells[position.Y, position.X];

    public void SetCell(GridPosition position, Cell cell) =>
        _cells[position.Y, position.X] = cell;

    public bool IsWon()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var cell = _cells[y, x];
                if (cell.IsGoal && !cell.HasCrate)
                    return false;
            }
        }

        return true;
    }

    public void IncrementMoveCount() => MoveCount++;

    public void SetPlayerPosition(GridPosition position) =>
        PlayerPosition = position;
}
