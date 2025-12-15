namespace KonturSokoban.Models;

public readonly struct GridPosition
{
    public int X { get; }
    public int Y { get; }

    public GridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public GridPosition Offset(int dx, int dy) =>
        new(X + dx, Y + dy);

    public override string ToString() => $"({X}, {Y})";
}
