namespace KonturSokoban.Models;

public enum CellKind
{
    Floor,
    Wall,
    Goal
}

public enum EntityKind
{
    None,
    Player,
    Crate
}

public readonly struct Cell
{
    public CellKind Kind { get; }
    public EntityKind Entity { get; }

    public bool IsWall => Kind == CellKind.Wall;
    public bool IsGoal => Kind == CellKind.Goal;
    public bool HasCrate => Entity == EntityKind.Crate;
    public bool HasPlayer => Entity == EntityKind.Player;
    public bool IsEmpty => Entity == EntityKind.None;

    public Cell(CellKind kind, EntityKind entity = EntityKind.None)
    {
        Kind = kind;
        Entity = entity;
    }

    public Cell WithEntity(EntityKind entity) =>
        new(Kind, entity);
}
