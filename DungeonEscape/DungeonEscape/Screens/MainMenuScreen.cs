using System.Collections.Generic;
using DungeonEscape.Content;
using DungeonEscape.GUI.Components;
using DungeonEscape.SaveGames;
using DungeonEscape.Screens.Settings;
using Mouse = DungeonEscape.Utils.Mouse;

namespace DungeonEscape.Screens
{
    internal sealed class MainMenuScreen : Screen
	{
		private readonly List<Button> _buttons = new List<Button>();

        public override void Init()
        {
            Mouse.ShowMouse();

            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Neues Spiel", NewGame));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, "Spiel laden", LoadGame));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Tutorial spielen", LoadTutorial));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, "Optionen", Options));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, "Spiel beenden", Exit));

            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, LanguageStrings.NewGame, NewGame));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, LanguageStrings.LoadGame, LoadGame));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, LanguageStrings.LoadTutorial, LoadTutorial));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, LanguageStrings.LoadSettings, Options));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, LanguageStrings.Exit, Exit));

            _buttons[0].Enabled = true;
            _buttons[1].Enabled = true;
            _buttons[2].Enabled = true;
            _buttons[3].Enabled = true;
            _buttons[4].Enabled = true;

            _buttons[0].Visible = true;
            _buttons[1].Visible = true;
            _buttons[2].Visible = true;
            _buttons[3].Visible = true;
            _buttons[4].Visible = true;
        }

		public override void Update()
		{
            _buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[2].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[3].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[4].PositionX = Basic.WindowSize.Width / 2 - 230;

            foreach (Button button in _buttons)
                button.Update();

            System.Threading.Thread.Sleep(1);
		}

        public override void Render()
		{
			foreach (Button current in _buttons)
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
            // Basic.SetScreen(new ResolutionScreen(1280, 720));
		}
    }
}
