using System.Collections.Generic;
using DungeonEscape.GUI;
using DungeonEscape.SaveGames;

namespace DungeonEscape.Screens
{
	public sealed class MainMenuScreen : IScreen
	{
		private readonly List<Button> _buttons = new List<Button>();

		public MainMenuScreen()
		{
			Screen.ShowMouse();

			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Neues Spiel", NewGame));
			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, "Spiel laden", LoadGame));
			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Tutorial spielen", LoadTutorial));
			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, "Optionen", Options));
			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, "Spiel beenden", Exit));

			_buttons[0].Enabled = true;
			_buttons[1].Enabled = true;
			_buttons[2].Enabled = true;
			_buttons[3].Enabled = false;
			_buttons[4].Enabled = true;

			_buttons[0].Visible = true;
			_buttons[1].Visible = true;
			_buttons[2].Visible = true;
			_buttons[3].Visible = true;
			_buttons[4].Visible = true;
		}

        public void Init()
        {

        }

		public void Update()
		{
            _buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[2].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[3].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[4].PositionX = Basic.WindowSize.Width / 2 - 230;

            foreach (var button in _buttons)
                button.Update();

            System.Threading.Thread.Sleep(1);
		}

        public void Render()
		{
			foreach (var current in _buttons)
			{
				current.Render();
			}
		}

		public void NewGame()
		{
			Basic.SetScreen(new ScoreSelectorScreen(false));
		}

		public void LoadTutorial()
		{
			Basic.SetScreen(new GameScreen(0, SaveState.One));
		}

		public void LoadGame()
		{
            Basic.SetScreen(new ScoreSelectorScreen(true));
		}

		public void Exit()
		{
			Basic.Game.Exit();
		}

		public void Options()
		{
            Basic.SetScreen(new OptionsMenuScreen(Savegamesettings.Load()));
		}
    }
}
