using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonEscape
{
    public sealed class GameScreen : IScreen
	{
        #region Fields

        private int levelNumber = 0;
        private Savegamescore save = new Savegamescore();

		public static Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }
        private static Camera _camera = null;

		public static Effect MainEffect
        {
            get { return _mainEffect; }
            set { _mainEffect = value; }
        }
        private static Effect _mainEffect = null;

		public static Level Level
        {
            get { return _level; }
            set { _level = value; }
        }
        private static Level _level = null;

		public static MouseState OldMouseState
        {
            get { return _oldMState; }
            set { _oldMState = value; }
        }
        private static MouseState _oldMState;

        public static GamePadState OldGamePadState
        {
            get { return _oldGState; }
            set { _oldGState = value; }
        }
        private static GamePadState _oldGState;


		public static bool IsTutorialRound
        {
            get { return _tutorial; }
            set { _tutorial = value; }
        }
        private static bool _tutorial = false;

        public static bool IsLoadedGame
        {
            get { return _loaded; }
            set { _loaded = value; }
        }
        private static bool _loaded = false;

        public static Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        private static Player _player = null;

        public static SaveState CurrentSaveState
        {
            get { return _cSaveState; }
            set { _cSaveState = value; }
        }
        private static SaveState _cSaveState = SaveState.One;

        #endregion

        #region Methods

        public GameScreen(int level, SaveState state)
		{
            if (level == 0)
                _tutorial = true;
            else
                _tutorial = false;

            _loaded = false;
            levelNumber = level;
            _cSaveState = state;
		}

        public GameScreen(Savegamescore save)
        {
            _tutorial = false;
            _loaded = true;
            this.save = save;
            _cSaveState = save.SaveState;
        }

        public void Init()
		{
			Screen.HideMouse();

			_camera = new Camera();
			_mainEffect = Basic.Content.Load<Effect>("mainEffect");
			_player = new Player();

            if (_loaded)
            {
                _level = new Level(save.LevelNumber);
                _level.Entities = save.Entities;
                _level.Init();
                _player.PlayerItemBar.Items = save.Items;
                _camera.Position = save.PlayerPosition;
            }
            else
            {
                _level = new Level(levelNumber);
                _level.Init();
            }
		}
		
        public void Update()
		{
			_level.Update();
			_camera.Update(Basic.GameTime);
			_player.Update();
			_oldMState = Mouse.GetState();
            _oldGState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
		}
		
        public void Render()
		{
			_level.Render();
			_player.Render();
		}

        #endregion
    }
}

