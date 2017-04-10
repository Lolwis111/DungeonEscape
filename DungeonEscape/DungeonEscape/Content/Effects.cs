using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Content
{
    internal static class Effects
    {
        public static Effect MainEffect { get; set; }

        public static void LoadEffects()
        {
            MainEffect = Basic.Content.Load<Effect>("mainEffect");
        }

        public static void UnloadEffects()
        {
            MainEffect?.Dispose();
            MainEffect = null;
        }
    }
}
