using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KonturSokoban.Models;

namespace KonturSokoban.UI;

public sealed class MonoGameRenderer : RendererBase
{
    private readonly Texture2D _textureWall;
    private readonly Texture2D _textureFloor;
    private readonly Texture2D _textureGoal;
    private readonly Texture2D _textureBox;
    private readonly Texture2D _texturePlayer;
    private readonly SpriteFont _font;

    public MonoGameRenderer(
        Texture2D textureWall,
        Texture2D textureFloor,
        Texture2D textureGoal,
        Texture2D textureBox,
        Texture2D texturePlayer,
        SpriteFont font)
    {
        _textureWall = textureWall;
        _textureFloor = textureFloor;
        _textureGoal = textureGoal;
        _textureBox = textureBox;
        _texturePlayer = texturePlayer;
        _font = font;
    }

    public override void Draw(SpriteBatch spriteBatch, GameState state, int screenWidth, int screenHeight)
    {
        var level = state.Level;
        var levelWidth = level.Width * TileSize;
        var levelHeight = level.Height * TileSize;
        var screenOffset = CalculateGameboardOffset(levelWidth, levelHeight, screenWidth, screenHeight);

        DrawGameboard(spriteBatch, level, screenOffset);
        DrawHud(spriteBatch, state, _font, screenWidth, screenHeight);
    }

    private void DrawGameboard(SpriteBatch spriteBatch, Level level, GridPosition screenOffset)
    {
        for (var y = 0; y < level.Height; y++)
        {
            for (var x = 0; x < level.Width; x++)
            {
                var cellPosition = new GridPosition(x, y);
                var cell = level.GetCell(cellPosition);
                var screenRect = GetCellRectangle(screenOffset, cellPosition);

                DrawCellBackground(spriteBatch, cell, screenRect);
                DrawCellEntity(spriteBatch, cell, screenRect);
            }
        }
    }

    private void DrawCellBackground(SpriteBatch spriteBatch, Cell cell, Rectangle screenRect)
    {
        var backgroundTexture = cell.IsGoal ? _textureGoal : _textureFloor;
        spriteBatch.Draw(backgroundTexture, screenRect, Color.White);
    }

    private void DrawCellEntity(SpriteBatch spriteBatch, Cell cell, Rectangle screenRect)
    {
        if (cell.IsWall)
        {
            spriteBatch.Draw(_textureWall, screenRect, Color.White);
            return;
        }

        if (cell.HasPlayer)
        {
            spriteBatch.Draw(_texturePlayer, screenRect, Color.White);
            return;
        }

        if (cell.HasCrate)
        {
            spriteBatch.Draw(_textureBox, screenRect, Color.White);
        }
    }
}
