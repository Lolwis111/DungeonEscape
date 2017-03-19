using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Content
{
    internal static class Fonts
    {
        public static SpriteFont MenuFont;
        public static SpriteFont MainFont;

        public static void LoadFonts()
        {
            MenuFont = LoadFont("menuFont");
            MainFont = LoadFont("Font");
        }

        private static SpriteFont LoadFont(string path)
        {
            return Basic.Content.Load<SpriteFont>(path);
        }

        public static void UnloadFonts()
        {
            MenuFont = null;
            MainFont = null;
        }
    }
}
