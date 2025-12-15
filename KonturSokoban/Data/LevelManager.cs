using System.Collections.Generic;
using KonturSokoban.Models;

namespace KonturSokoban.Data;

public sealed class LevelManager
{
    private readonly List<(Cell[,] Grid, GridPosition StartPosition)> _levels;

    public LevelManager()
    {
        _levels = new List<(Cell[,], GridPosition)>();
        LoadLevels();
    }

    private void LoadLevels()
    {
        _levels.Add((
            ConvertRowsToGrid(new[]
            {
                new[] { W, W, W, W, W, W, W, W, W, W, W, W },
                new[] { W, P, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, C, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, G, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, W, W, W, W, W, W, W, W, W, W, W }
            }),
            new GridPosition(1, 1)
        ));

        _levels.Add((
            ConvertRowsToGrid(new[]
            {
                new[] { W, W, W, W, W, W, W, W, W, W, W, W },
                new[] { W, P, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, W, F, C, F, W, F, F, F, F, W },
                new[] { W, F, W, F, C, F, W, F, G, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, W, F, C, F, W, F, G, F, F, W },
                new[] { W, F, W, F, F, F, W, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, F, W },
                new[] { W, F, F, F, F, F, F, F, F, F, G, W },
                new[] { W, W, W, W, W, W, W, W, W, W, W, W }
            }),
            new GridPosition(1, 1)
        ));

        ////_levels.Add((
        ////    ConvertRowsToGrid(new[]
        ////    {
        ////        new[] { W, W, W, W, W, W, W, W, W, W, W, W },
        ////        new[] { W, P, F, F, F, W, F, F, C, F, F, W },
        ////        new[] { W, F, W, W, F, W, F, W, F, W, F, W },
        ////        new[] { W, F, F, F, C, F, F, F, F, F, C, W },
        ////        new[] { W, W, F, W, F, W, W, W, F, W, F, W },
        ////        new[] { W, F, F, F, F, F, F, F, F, F, F, W },
        ////        new[] { W, F, W, W, W, F, W, W, W, F, G, W },
        ////        new[] { W, F, F, F, F, F, F, F, F, F, G, W },
        ////        new[] { W, F, F, F, F, F, F, F, F, F, G, W },
        ////        new[] { W, W, W, W, W, W, W, W, W, W, W, W }
        ////    }),
        ////    new GridPosition(1, 1)
        //));
    }

    private Cell[,] ConvertRowsToGrid(Cell[][] rows)
    {
        var height = rows.Length;
        var width = rows.Length > 0 ? rows[0].Length : 0;
        var grid = new Cell[height, width];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
                grid[y, x] = rows[y][x];
        }

        return grid;
    }

    public Level GetLevel(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > _levels.Count)
            return null;

        var (grid, startPosition) = _levels[levelNumber - 1];
        return new Level(grid, startPosition);
    }

    public int GetTotalLevels() => _levels.Count;

    private static Cell W => new Cell(CellKind.Wall);
    private static Cell F => new Cell(CellKind.Floor);
    private static Cell P => new Cell(CellKind.Floor, EntityKind.Player);
    private static Cell C => new Cell(CellKind.Floor, EntityKind.Crate);
    private static Cell G => new Cell(CellKind.Goal);
}