namespace DungeonEscape.Utils
{
    internal static class Mouse
	{
        public static void ShowMouse()
		{
			Basic.Game.IsMouseVisible = true;
		}

		public static void HideMouse()
		{
			Basic.Game.IsMouseVisible = false;
		}

        public static void CenterMouse()
		{
			Microsoft.Xna.Framework.Input.Mouse.SetPosition(Basic.WindowSize.Width / 2, Basic.WindowSize.Height / 2);
		}
	}
}
