using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonEscape
{
    public interface IScreen
    {
        void Init();
        void Update();
        void Render();
    }

	public static class Screen
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
			Mouse.SetPosition(Basic.WindowSize.Width / 2, Basic.WindowSize.Height / 2);
		}
	}
}
