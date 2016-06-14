using System;
using System.Collections.Generic;

namespace DungeonEscape
{
	public sealed class MainMenuScreen : IScreen
	{
		private List<Button> buttons = new List<Button>();

		public MainMenuScreen()
		{
			Screen.ShowMouse();

			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Neues Spiel", new Click(this.NewGame)));
			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, "Spiel laden", new Click(this.LoadGame)));
			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Tutorial spielen", new Click(this.LoadTutorial)));
			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, "Optionen", new Click(this.Options)));
			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, "Spiel beenden", new Click(this.Exit)));

			buttons[0].Enabled = true;
			buttons[1].Enabled = true;
			buttons[2].Enabled = true;
			buttons[3].Enabled = false;
			buttons[4].Enabled = true;

			buttons[0].Visible = true;
			buttons[1].Visible = true;
			buttons[2].Visible = true;
			buttons[3].Visible = true;
			buttons[4].Visible = true;
		}

        public void Init()
        {

        }

		public void Update()
		{
            buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[2].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[3].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[4].PositionX = Basic.WindowSize.Width / 2 - 230;

            foreach (Button button in buttons)
                button.Update();

            System.Threading.Thread.Sleep(1);
		}

        public void Render()
		{
			foreach (Button current in buttons)
			{
				current.Render();
			}
		}

		public void NewGame()
		{
			Basic.setScreen(new ScoreSelectorScreen(false));
		}

		public void LoadTutorial()
		{
			Basic.setScreen(new GameScreen(0, SaveState.One));
		}

		public void LoadGame()
		{
            Basic.setScreen(new ScoreSelectorScreen(true));
		}

		public void Exit()
		{
			Basic.Game.Exit();
		}

		public void Options()
		{
            Basic.setScreen(new OptionsMenuScreen(Savegamesettings.Load()));
		}
    }
}
