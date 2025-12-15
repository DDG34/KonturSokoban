using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KonturSokoban.Data;
using KonturSokoban.Engine;
using KonturSokoban.Models;
using KonturSokoban.UI;

namespace KonturSokoban;

public sealed class Game1 : Game
{
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;

    private Texture2D _textureWall;
    private Texture2D _textureFloor;
    private Texture2D _textureGoal;
    private Texture2D _textureBox;
    private Texture2D _texturePlayer;
    private SpriteFont _gameFont;

    private SokobanEngine _engine;
    private MonoGameRenderer _renderer;
    private InputHandler _inputHandler;
    private LevelManager _levelManager;

    private KeyboardState _previousKeyboardState;
    private int _currentLevelNumber;

    public Game1()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphicsDeviceManager.PreferredBackBufferWidth = 1200;
        _graphicsDeviceManager.PreferredBackBufferHeight = 900;
        _graphicsDeviceManager.ApplyChanges();

        _inputHandler = new InputHandler();
        _levelManager = new LevelManager();
        _currentLevelNumber = 1;

        StartLevel(_currentLevelNumber);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _textureWall = Content.Load<Texture2D>("Textures/wall");
        _textureFloor = Content.Load<Texture2D>("Textures/playingField");
        _textureGoal = Content.Load<Texture2D>("Textures/kontrolPoint");
        _textureBox = Content.Load<Texture2D>("Textures/box");
        _texturePlayer = Content.Load<Texture2D>("Textures/player");
        _gameFont = Content.Load<SpriteFont>("Fonts/File");

        _renderer = new MonoGameRenderer(
            _textureWall,
            _textureFloor,
            _textureGoal,
            _textureBox,
            _texturePlayer,
            _gameFont);
    }

    protected override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        if (currentKeyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
            return;
        }

        if (_engine == null)
            return;

        var direction = _inputHandler.GetInput(currentKeyboardState);
        if (ShouldMovePlayer(direction, currentKeyboardState) && !_engine.State.IsGameOver)
            _engine.TryMove(direction);

        if (_engine.State.IsGameOver && ShouldAdvanceToNextLevel(currentKeyboardState))
            HandleLevelComplete();

        _previousKeyboardState = currentKeyboardState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        if (_engine != null)
        {
            _spriteBatch.Begin();
            _renderer.Draw(
                _spriteBatch,
                _engine.State,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }

    private void StartLevel(int levelNumber)
    {
        var level = _levelManager.GetLevel(levelNumber);
        if (level != null)
            _engine = new SokobanEngine(new GameState(level, levelNumber));
    }

    private bool ShouldMovePlayer(Direction direction, KeyboardState currentKeyboardState)
    {
        if (direction == Direction.None)
            return false;

        var keyForDirection = GetKeyForDirection(direction);
        return _previousKeyboardState.IsKeyUp(keyForDirection);
    }

    private bool ShouldAdvanceToNextLevel(KeyboardState currentKeyboardState) =>
        currentKeyboardState.IsKeyDown(Keys.Space) &&
        _previousKeyboardState.IsKeyUp(Keys.Space);

    private void HandleLevelComplete()
    {
        _currentLevelNumber++;
        if (_currentLevelNumber > _levelManager.GetTotalLevels())
        {
            Exit();
            return;
        }
        StartLevel(_currentLevelNumber);
    }
    private Keys GetKeyForDirection(Direction direction) =>
        direction switch
        {
            Direction.Up => Keys.Up,
            Direction.Down => Keys.Down,
            Direction.Left => Keys.Left,
            Direction.Right => Keys.Right,
            _ => Keys.None
        };
}
