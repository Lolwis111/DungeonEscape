using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Levels;
using DungeonEscape.SaveGames;

namespace DungeonEscape.Screens
{
    public sealed class GameScreen : IScreen
	{
        #region Fields

        public static bool MouseClicked => (Mouse.GetState().LeftButton == ButtonState.Pressed 
            && OldMouseState.LeftButton == ButtonState.Released);

	    private readonly int _levelNumber;
        private readonly Savegamescore _save = new Savegamescore();

	    public static Camera Camera { get; private set; }

	    public static Effect MainEffect { get; set; }

		public static Level Level { get; set; }

	    public static MouseState OldMouseState { get; set; }

	    public static GamePadState OldGamePadState { get; set; }

	    public static bool IsTutorialRound { get; set; }

	    public static bool IsLoadedGame { get; set; }

	    public static Player Player { get; set; }

	    public static SaveState CurrentSaveState { get; set; } = SaveState.One;

	    #endregion

        #region Methods

        public GameScreen(int level, SaveState state)
        {
            // Level 0 = Tutorial; 
            IsTutorialRound = level == 0;

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
			Screen.HideMouse();

			Camera = new Camera();
			MainEffect = Basic.Content.Load<Effect>("mainEffect");
			Player = new Player();

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
		}
		
        public void Update()
		{
			Level.Update();
			Camera.Update(Basic.GameTime);
			Player.Update();
            OldMouseState = Mouse.GetState();
            OldGamePadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
		}
		
        public void Render()
		{
			Level.Render();
			Player.Render();
		}

        #endregion
    }
}

