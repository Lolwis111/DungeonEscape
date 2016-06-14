using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonEscape
{
    public static class Basic
    {
        #region Fields

        public static bool DebugMode
        {
            get { return _debug; }
            set { _debug = value; }
        }
        private static bool _debug = false;

        public static SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
            set { _spriteBatch = value; }
        }
        private static SpriteBatch _spriteBatch;

		public static GraphicsDevice GraphicsDevice
        {
            get { return _gDevice; }
            set { _gDevice = value; }
        }
        private static GraphicsDevice _gDevice;

		public static GraphicsDeviceManager GraphicsManager
        {
            get { return _graphics; }
            set { _graphics = value; }
        }
        private static GraphicsDeviceManager _graphics;

		public static ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private static ContentManager _content;

		public static GameTime GameTime
        {
            get { return _time; }
            set { _time = value; }
        }
        private static GameTime _time;

		public static GameMain Game
        {
            get { return _game; }
            set { _game = value; }
        }
        private static GameMain _game;

		public static GameWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }
        private static GameWindow _window;

		public static Rectangle WindowSize
        {
            get { return _windowSize; }
            set { _windowSize = value; }
        }
        private static Rectangle _windowSize = new Rectangle(0, 0, 1024, 768);

        public static Random Random
        {
            get { return _rand; }
            set { _rand = value; }
        }
        private static Random _rand = new Random();

		public static IScreen CurrentScreen
        {
            get { return _cScreen; }
            set { _cScreen = value; }
        }
        private static IScreen _cScreen;

        public static IScreen SecondaryScreen
        {
            get { return _sScreen; }
            set { _sScreen = value; }
        }
        private static IScreen _sScreen;

		public static SpriteFont MainFont
        {
            get { return _mainFont; }
            set { _mainFont = value; }
        }
        private static SpriteFont _mainFont;

		public static SpriteFont MenuFont
        {
            get { return _menuFont; }
            set { _menuFont = value; }
        }
        private static SpriteFont _menuFont;

        public static bool UseSmallTextures
        {
            get { return _usTex; }
            set { _usTex = value; }
        }
        private static bool _usTex = false;

        public static GamePadState OldGamePadState
        {
            get { return _oldGState; }
            set { _oldGState = value; }
        }
        private static GamePadState _oldGState;

        public static GamePadState NewGamePadState
        {
            get { return _newGState; }
            set { _newGState = value; }
        }
        private static GamePadState _newGState;


        private static SamplerState _samlper;
		private static KeyboardState _oldKeyboardState;
		private static KeyboardState _newKeyboardState;

		private static FpsCounter _counterFPS = null;

        public static bool ByPassMenu
        {
            get { return _directLoad; }
            set { _directLoad = value; }
        }
        private static bool _directLoad = false;

        public static int ByPassLevel
        {
            get { return _directLevel; }
            set { _directLevel = value; }
        }
        private static int _directLevel = -1;

        #endregion

        #region Methods

        public static void Init(GameMain _game)
		{
			Basic._game = _game;
			_graphics = _game.Graphics;
			_gDevice = GraphicsManager.GraphicsDevice;
			_content = _game.Content;
            _spriteBatch = new SpriteBatch(_gDevice);
			_window = _game.Window;

			_mainFont = _content.Load<SpriteFont>("Font");
			_menuFont = _content.Load<SpriteFont>("menuFont");

            Textures.LoadTexture();
            Sounds.LoadSounds();
            Sounds.SetVolume(_game.SoundVolume);

			Canvas.setUpCanvas();

			_samlper = new SamplerState();
			_samlper.Filter = TextureFilter.Point;

            if (_debug)
            {
                _counterFPS = new FpsCounter(_game);
                _game.Components.Add(_counterFPS);
            }

            if (_directLoad) setScreen(new GameScreen(_directLevel, SaveState.ByPass));
			else setScreen(new MainMenuScreen());
		}

		public static void UnloadContent()
		{
            Sounds.UnloadSounds();
            Textures.UndloadTexture();
            _spriteBatch.Dispose();

            _content.Unload();

            if (_counterFPS != null)
            {
                _counterFPS.Dispose();
                _counterFPS = null;
            }
		}

		public static void Update(GameTime _gameTime)
		{
			_time = _gameTime;
			_newKeyboardState = Keyboard.GetState();
            _newGState = GamePad.GetState(PlayerIndex.One);

			if (((_newKeyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape)) || 
                (_newGState.Buttons.Start == ButtonState.Pressed && _oldGState.Buttons.A == ButtonState.Released)) && 
                _cScreen is GameScreen)
            {
				_sScreen = _cScreen;
				setScreen(new InGameMenuScreen());
			}

			_oldKeyboardState = _newKeyboardState;
            _oldGState = NewGamePadState;
			_cScreen.Update();
		}

		public static void Render()
		{
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

			_gDevice.DepthStencilState = DepthStencilState.Default;
            _gDevice.SamplerStates[0] = _samlper;
			_cScreen.Render();

            _spriteBatch.End();
		}

		public static void setScreen(IScreen newScreen)
		{
			_cScreen = newScreen;
			CurrentScreen.Init();
		}

		public static void SaveGame(bool exitgame)
		{
            Savegamescore save = new Savegamescore() 
                { 
                    Entities = GameScreen.Level.Entities, 
                    Items = GameScreen.Player.PlayerItemBar.Items, 
                    LevelNumber = GameScreen.Level.LevelNumber,
                    PlayerPosition = GameScreen.Camera.Position, 
                    SaveState = GameScreen.CurrentSaveState
                };

            Savegamescore.Save(save);

			if (exitgame)
			{
				Game.Exit();
			}
        }

        #endregion
    }
}
