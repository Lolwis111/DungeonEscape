namespace DungeonEscape.Content
{
    internal static class Content
    {
        public static void LoadAllContent()
        {
            Effects.LoadEffects();
            Fonts.LoadFonts();
            Sounds.LoadSounds();
            Textures.LoadTextures();
        }

        public static void UnloadAllContent()
        {
            Effects.UnloadEffects();
            Fonts.UnloadFonts();
            Sounds.UnloadSounds();
            Textures.UndloadTexture();
        }
    }
}
