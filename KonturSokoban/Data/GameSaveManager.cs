using System;
using System.IO;
using System.Text.Json;
using KonturSokoban.Models;

namespace KonturSokoban.Data;

public sealed class GameSaveData
{
    public int LevelNumber { get; set; }
    public int MoveCount { get; set; }
    public DateTime SaveTime { get; set; }
}

public sealed class GameSaveManager
{
    private const string SaveFilePath = "savegame.json";

    public void SaveGame(GameState state)
    {
        var saveData = new GameSaveData
        {
            LevelNumber = state.LevelNumber,
            MoveCount = state.Level.MoveCount,
            SaveTime = DateTime.Now
        };

        var json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SaveFilePath, json);
    }

    public GameSaveData LoadGame()
    {
        if (!File.Exists(SaveFilePath))
            return null;

        var json = File.ReadAllText(SaveFilePath);
        return JsonSerializer.Deserialize<GameSaveData>(json);
    }
}
