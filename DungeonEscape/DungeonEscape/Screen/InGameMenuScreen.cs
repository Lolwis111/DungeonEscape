using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape
{
	public sealed class InGameMenuScreen : IScreen
	{
		private List<Button> buttons = new List<Button>();

		public InGameMenuScreen()
		{
			Screen.ShowMouse();

			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Zurück zum Spiel", new Click(Back)));
			buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Speichern und zum Hauptmenü", new Click(Exit)));

			buttons[0].Enabled = true;
			buttons[1].Enabled = true;

			buttons[0].Visible = true;
			buttons[1].Visible = true;
		}

        public void Init()
        { }

		public void Update()
		{
			buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
			buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;

			foreach (Button current in this.buttons)
			{
				current.Update();
			}

            System.Threading.Thread.Sleep(1);
		}

		public void Render()
		{
			foreach (Button current in buttons)
			{
				current.Render();
			}
		}

		public void Back()
		{
			Screen.HideMouse();
			Screen.CenterMouse();

			Basic.CurrentScreen = Basic.SecondaryScreen;
			Basic.SecondaryScreen = null;
		}

		public void Exit()
		{
			Basic.SaveGame(false);
			Basic.setScreen(new MainMenuScreen());
		}
	}
}
