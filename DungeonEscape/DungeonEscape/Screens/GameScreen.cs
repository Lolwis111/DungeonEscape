using DungeonEscape.Cameras;
using DungeonEscape.Enemies;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Levels;
using DungeonEscape.SaveGames;
using Microsoft.Xna.Framework;

namespace DungeonEscape.Screens
{
    internal sealed class GameScreen : IScreen
	{
        #region Fields

        public static bool MouseClicked => (Mouse.GetState().LeftButton == ButtonState.Pressed 
            && OldMouseState.LeftButton == ButtonState.Released);

	    private readonly int _levelNumber;
        private readonly Savegamescore _save = new Savegamescore();

	    public static Camera Camera { get; private set; }

		public static Level Level { get; set; }

	    public static MouseState OldMouseState { get; set; }

	    public static GamePadState OldGamePadState { get; set; }

	    public static bool IsTutorialRound { get; set; }

	    public static bool IsLoadedGame { get; set; }

	    public static Player.Player Player { get; set; }

	    public static SaveState CurrentSaveState { get; set; } = SaveState.One;

        public static EvilMan Enemy { get; set; }

        #endregion

        #region Methods

        public GameScreen(int level, SaveState state)
        {
            // Level 0 = Tutorial; 
            IsTutorialRound = (level == 0);

            IsLoadedGame = false;
            _levelNumber = level;
            CurrentSaveState = state;
		}

        public GameScreen(Savegamescore save)
        {
            IsTutorialRound = false;
            IsLoadedGame = true;
            _save = save;
            CurrentSaveState = save.SaveState;
        }

        public void Init()
		{
			Utils.Mouse.HideMouse();

		    Camera = new Camera { Mode = Basic.DebugMode ? CameraMode.DebugCamera : CameraMode.PlayerCamera };

			Player = new Player.Player();

            if (IsLoadedGame)
            {
                Level = new Level(_save.LevelNumber) { Entities = _save.Entities };
                Level.Init();

                Player.PlayerItemBar.Items = _save.Items;
                Camera.Position = _save.PlayerPosition;
            }
            else
            {
                Level = new Level(_levelNumber);
                Level.Init();
            }

            Enemy = new EvilMan(0, 0, 0);
        }
		
        public void Update()
		{
			Level.Update();
			Camera.Update(Basic.GameTime);

			Player.Update();
            Enemy.Update();

            OldMouseState = Mouse.GetState();
            OldGamePadState = GamePad.GetState(PlayerIndex.One);
		}
		
        public void Render()
		{
			Level.Render();
			Player.Render();

            Enemy.Render();
		}

        #endregion
    }
}

