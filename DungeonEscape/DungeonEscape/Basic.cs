using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Screens;
using DungeonEscape.Debug;
using DungeonEscape.Content;
using DungeonEscape.SaveGames;

namespace DungeonEscape
{
    public static class Basic
    {
        #region Fields

        public static bool DebugMode { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }

		public static GraphicsDevice GraphicsDevice { get; set; }

		public static GraphicsDeviceManager GraphicsManager { get; set; }

		public static ContentManager Content { get; set; }

        public static GameTime GameTime { get; set; }

        public static GameMain Game { get; set; }

        public static GameWindow Window { get; set; }

        public static Rectangle WindowSize { get; set; } = new Rectangle(0, 0, 1024, 768);

        public static Random Random { get; set; }

        public static IScreen CurrentScreen { get; set; }

        public static IScreen SecondaryScreen { get; set; }

        public static SpriteFont MainFont { get; set; }

		public static SpriteFont MenuFont { get; set; }

        public static bool UseSmallTextures { get; set; }

        public static GamePadState OldGamePadState { get; set; }

        public static GamePadState NewGamePadState { get; set; }


        private static SamplerState _samlper;
		private static KeyboardState _oldKeyboardState;
		private static KeyboardState _newKeyboardState;

		private static FpsCounter _counterFps;

        public static bool ByPassMenu { get; set; }

        public static int ByPassLevel { get; set; } = -1;

        #endregion

        #region Methods

        public static void Init(GameMain game)
		{
			Game = game;
			GraphicsManager = game.Graphics;
			GraphicsDevice = GraphicsManager.GraphicsDevice;
			Content = game.Content;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Window = game.Window;

			MainFont = Content.Load<SpriteFont>("Font");
			MenuFont = Content.Load<SpriteFont>("menuFont");

            Textures.LoadTexture();
            Sounds.LoadSounds();
            Sounds.SetVolume(game.SoundVolume);

			Canvas.SetUpCanvas();

		    _samlper = new SamplerState { Filter = TextureFilter.Point };

            if (DebugMode)
            {
                _counterFps = new FpsCounter(game);
                game.Components.Add(_counterFps);
            }

            if (ByPassMenu) SetScreen(new GameScreen(ByPassLevel, SaveState.ByPass));
			else SetScreen(new MainMenuScreen());
		}

		public static void UnloadContent()
		{
            Sounds.UnloadSounds();
            Textures.UndloadTexture();
            SpriteBatch.Dispose();

            Content.Unload();

            if (_counterFps != null)
            {
                _counterFps.Dispose();
                _counterFps = null;
            }
		}

		public static void Update(GameTime gameTime)
		{
			GameTime = gameTime;
			_newKeyboardState = Keyboard.GetState();
            NewGamePadState = GamePad.GetState(PlayerIndex.One);

			if ((_newKeyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape) ||
                NewGamePadState.Buttons.Start == ButtonState.Pressed && OldGamePadState.Buttons.A == ButtonState.Released) && 
                CurrentScreen is GameScreen)
            {
				SecondaryScreen = CurrentScreen;
				SetScreen(new InGameMenuScreen());
			}

			_oldKeyboardState = _newKeyboardState;
            OldGamePadState = NewGamePadState;
            CurrentScreen.Update();
		}

		public static void Render()
		{
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, 
                SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

			GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = _samlper;
            CurrentScreen.Render();

            SpriteBatch.End();
		}

		public static void SetScreen(IScreen newScreen)
		{
            CurrentScreen = newScreen;
			CurrentScreen.Init();
		}

		public static void SaveGame(bool exitgame)
		{
            var save = new Savegamescore() 
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
