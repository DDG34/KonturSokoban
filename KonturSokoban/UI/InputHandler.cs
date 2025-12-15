using Microsoft.Xna.Framework.Input;
using KonturSokoban.Models;

namespace KonturSokoban.UI;

public sealed class InputHandler
{
    public Direction GetInput(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Up))
            return Direction.Up;

        if (keyboardState.IsKeyDown(Keys.Down))
            return Direction.Down;

        if (keyboardState.IsKeyDown(Keys.Left))
            return Direction.Left;

        if (keyboardState.IsKeyDown(Keys.Right))
            return Direction.Right;

        return Direction.None;
    }
}
