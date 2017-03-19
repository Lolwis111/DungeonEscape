using Microsoft.Xna.Framework.Graphics;

namespace MapCreator2D
{
    public static class Textures
    {
        #region Fields

        public static Texture2D Wall;
        public static Texture2D Destroyable;
        public static Texture2D Key;
        public static Texture2D Pickaxe;
        public static Texture2D Pliers;
        public static Texture2D Door;
        public static Texture2D Grid;
        public static Texture2D Message;
        public static Texture2D LevelDown;
        public static Texture2D LevelUp;
        public static Texture2D None;
        public static Texture2D Spawn;
        public static Texture2D Switch;
        public static Texture2D Half;

        #endregion

        #region Methods

        public static void LoadTextures()
        {
            Wall = LoadTexture("Blocks\\Wall");
            Destroyable = LoadTexture("Blocks\\destroyableBlock");

            Switch = LoadTexture("Blocks\\switchOff");
            Half = LoadTexture("Blocks\\halfBlock");

            Key = LoadTexture("Items\\key");
            Pliers = LoadTexture("Items\\pliers");
            Pickaxe = LoadTexture("Items\\pickaxe");
            Message = LoadTexture("Items\\message");

            Door = LoadTexture("Sprites\\door");
            Grid = LoadTexture("Sprites\\grid");
            LevelDown = LoadTexture("Sprites\\levelDown");
            LevelUp = LoadTexture("Sprites\\levelUp");

            None = LoadTexture("none");
            Spawn = LoadTexture("spawn");
        }

        private static Texture2D LoadTexture(string name)
        {
            return Basic.Content.Load<Texture2D>("Textures/" + name);
        }

        public static void DisposeTextures()
        {
            Wall?.Dispose();

            Destroyable?.Dispose();

            Key?.Dispose();

            Pickaxe?.Dispose();

            Pliers?.Dispose();

            Door?.Dispose();

            Grid?.Dispose();

            LevelDown?.Dispose();

            LevelUp?.Dispose();

            None?.Dispose();

            Spawn?.Dispose();

            Message?.Dispose();

            Switch?.Dispose();

            Half?.Dispose();
        }

        #endregion
    }
}
