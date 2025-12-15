using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KonturSokoban.Models;

namespace KonturSokoban.UI;

public abstract class RendererBase
{
    protected const int TileSize = 64;

    public abstract void Draw(SpriteBatch spriteBatch, GameState state, int screenWidth, int screenHeight);

    protected GridPosition CalculateGameboardOffset(int levelWidth, int levelHeight, int screenWidth, int screenHeight)
    {
        var offsetX = (screenWidth - levelWidth) / 2;
        var offsetY = (screenHeight - levelHeight) / 2 + 30;
        return new GridPosition(offsetX, offsetY);
    }

    protected Rectangle GetCellRectangle(GridPosition screenOffset, GridPosition cellPosition)
    {
        var screenX = screenOffset.X + cellPosition.X * TileSize;
        var screenY = screenOffset.Y + cellPosition.Y * TileSize;
        return new Rectangle(screenX, screenY, TileSize, TileSize);
    }

    protected void DrawHud(SpriteBatch spriteBatch, GameState state, SpriteFont font, int screenWidth, int screenHeight)
    {
        spriteBatch.DrawString(font, $"Level {state.LevelNumber}",
            new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(font, $"Moves: {state.Level.MoveCount}",
            new Vector2(10, 30), Color.White);

        if (state.IsGameOver)
        {
            spriteBatch.DrawString(font, "LEVEL COMPLETE!!! (Press SPACE)",
                new Vector2(screenWidth / 2 - 200, 50), Color.Yellow);
        }
    }
}
