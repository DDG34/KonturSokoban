namespace KonturSokoban.Models;

public sealed class GameState
{
    public Level Level { get; }
    public int LevelNumber { get; }
    public bool IsGameOver { get; private set; }

    public GameState(Level level, int levelNumber)
    {
        Level = level;
        LevelNumber = levelNumber;
        IsGameOver = false;
    }

    public void MarkGameOver() => IsGameOver = true;
}
