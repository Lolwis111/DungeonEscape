using System.Collections.Generic;
using DungeonEscape.GUI;

namespace DungeonEscape.Screens
{
	public sealed class InGameMenuScreen : IScreen
	{
		private readonly List<Button> _buttons = new List<Button>();

		public InGameMenuScreen()
		{
			Screen.ShowMouse();

			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Zurück zum Spiel", Back));
			_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Speichern und zum Hauptmenü", Exit));

			_buttons[0].Enabled = true;
			_buttons[1].Enabled = true;

			_buttons[0].Visible = true;
			_buttons[1].Visible = true;
		}

        public void Init()
        { }

		public void Update()
		{
			_buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
			_buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;

			foreach (Button current in _buttons)
			{
				current.Update();
			}

            System.Threading.Thread.Sleep(1);
		}

		public void Render()
		{
			foreach (Button current in _buttons)
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
			Basic.SetScreen(new MainMenuScreen());
		}
	}
}
