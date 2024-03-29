using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Debug;
using DungeonEscape.Content;
using DungeonEscape.SaveGames;
using DungeonEscape.Screens;
using DungeonEscape.GUI;

namespace DungeonEscape
{
    internal static class Basic
    {
        #region Fields

        public static GameMain Game;

        public static GraphicsDevice GraphicsDevice;
        public static GraphicsDeviceManager GraphicsManager;

        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;

        public static GameTime GameTime;
        public static Random Random = new Random();

        public static GameWindow Window;
        public static Rectangle WindowSize = new Rectangle(0, 0, 1280, 720);

        public static Screen CurrentScreen;
        public static Screen SecondaryScreen;

        public static bool UseSmallTextures;

        public static GamePadState OldGamePadState;
        public static GamePadState NewGamePadState;


        private static SamplerState _samlper;
		private static KeyboardState _oldKeyboardState;
		private static KeyboardState _newKeyboardState;

        public static bool DebugMode;
        private static FpsCounter _counterFps;

        public static bool ByPassMenu;
        public static int ByPassLevel;

        #endregion

        #region Methods

        public static void Init(GameMain game, string language)
		{
			Game = game;
			GraphicsManager = game.Graphics;
			GraphicsDevice = GraphicsManager.GraphicsDevice;
			Content = game.Content;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Window = game.Window;

            DungeonEscape.Content.Content.LoadAllContent();

            LanguageStrings.LoadStrings(language);
            Sounds.SetVolume(game.SoundVolume);

			Canvas.SetUpCanvas();

		    _samlper = new SamplerState { Filter = TextureFilter.Point };

            if (DebugMode)
            {
                _counterFps = new FpsCounter();
            }

            if (ByPassMenu) SetScreen(new GameScreen(ByPassLevel, SaveState.ByPass));
			else SetScreen(new MainMenuScreen());
		}

		public static void UnloadContent()
		{
            DungeonEscape.Content.Content.UnloadAllContent();

            SpriteBatch.Dispose();

            Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
			GameTime = gameTime;
			_newKeyboardState = Keyboard.GetState();
            NewGamePadState = GamePad.GetState(PlayerIndex.One);

			if ((_newKeyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape) ||
                NewGamePadState.Buttons.Start == ButtonState.Pressed && OldGamePadState.Buttons.Start == ButtonState.Released) && 
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

            if(Debug.Debug.DrawFramerate) _counterFps.Draw(GameTime);

            SpriteBatch.End();
		}

		public static void SetScreen(Screen newScreen)
		{
            CurrentScreen = newScreen;
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
