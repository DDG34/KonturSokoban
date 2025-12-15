using KonturSokoban.Models;

namespace KonturSokoban.Engine;

public sealed class SokobanEngine
{
    private readonly GameState _state;

    public SokobanEngine(GameState state)
    {
        _state = state;
    }

    public GameState State => _state;

    public bool TryMove(Direction direction)
    {
        if (direction == Direction.None || _state.IsGameOver)
            return false;

        var level = _state.Level;
        var (dx, dy) = direction.ToOffset();
        var from = level.PlayerPosition;
        var to = from.Offset(dx, dy);

        if (!level.IsInside(to))
            return false;

        var targetCell = level.GetCell(to);
        if (targetCell.IsWall)
            return false;

        if (targetCell.HasCrate)
        {
            if (!TryMoveCrate(level, to, dx, dy))
                return false;
        }

        MovePlayer(level, from, to);
        level.IncrementMoveCount();

        if (level.IsWon())
            _state.MarkGameOver();

        return true;
    }

    private bool TryMoveCrate(Level level, GridPosition cratePosition, int dx, int dy)
    {
        var crateTo = cratePosition.Offset(dx, dy);

        if (!level.IsInside(crateTo))
            return false;

        var crateTarget = level.GetCell(crateTo);
        if (crateTarget.IsWall || crateTarget.HasCrate)
            return false;

        level.SetCell(crateTo, crateTarget.WithEntity(EntityKind.Crate));
        var emptyCell = level.GetCell(cratePosition).WithEntity(EntityKind.None);
        level.SetCell(cratePosition, emptyCell);

        return true;
    }

    private void MovePlayer(Level level, GridPosition from, GridPosition to)
    {
        var fromCell = level.GetCell(from).WithEntity(EntityKind.None);
        level.SetCell(from, fromCell);

        var toCell = level.GetCell(to).WithEntity(EntityKind.Player);
        level.SetCell(to, toCell);

        level.SetPlayerPosition(to);
    }
}
