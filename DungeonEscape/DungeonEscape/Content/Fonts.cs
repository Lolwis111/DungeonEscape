using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Content
{
    internal static class Fonts
    {
        public static SpriteFont MenuFont;
        public static SpriteFont MainFont;
        public static SpriteFont SettingsMenuFont;

        public static void LoadFonts()
        {
            MenuFont = LoadFont("menuFont");
            MainFont = LoadFont("Font");
            SettingsMenuFont = LoadFont("settingsMenuFont");
        }

        private static SpriteFont LoadFont(string path)
        {
            return Basic.Content.Load<SpriteFont>($"Fonts/{path}");
        }

        public static void UnloadFonts()
        {
            MenuFont = null;
            MainFont = null;
        }
    }
}
