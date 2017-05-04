using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Content
{
    internal static class Textures
    {
        #region Fields

        public static Texture2D Floor;

        public static Texture2D Ceiling;

        public static Texture2D Wall;

        public static Texture2D DestroyableBlock;

        public static Texture2D PickAxe;

        public static Texture2D Pliers;

        public static Texture2D Key;

        public static Texture2D Message;

        public static Texture2D Grid;

        public static Texture2D GridDestroyed;

        public static Texture2D Door;

        public static Texture2D LevelUp;

        public static Texture2D LevelDown;

        public static Texture2D ItemBarItem;

        public static Texture2D SelectedItemBarItem;

        public static Texture2D Canvas;

        public static Texture2D SwitchOn;

        public static Texture2D SwitchOff;

        public static Texture2D EvilMan;

        public static Texture2D Particle;

#if DEBUG
        public static Texture2D Dummy;
#endif

#endregion

        #region Methods

        public static void LoadTextures()
		{
            //Vor dem Laden alle Texturen freigeben (falls bereits vorher etwas geladen wurde)
            UndloadTexture();

#if DEBUG
            Dummy = new Texture2D(Basic.GraphicsDevice, 1, 1);
            Dummy.SetData( new[] {  Color.White } );
#endif

            //Alle Texturen laden
            //Verzeichnis: %startup%/Content/Textures/

            if (Basic.UseSmallTextures)
            {
                Floor = LoadTextures("Low\\Blocks\\floor");
                Ceiling = LoadTextures("Low\\Blocks\\ceiling");
                Wall = LoadTextures("Low\\Blocks\\wall");
                DestroyableBlock = LoadTextures("Low\\Blocks\\destroyableBlock");

                SwitchOn = LoadTextures("Low\\Blocks\\switchOn");
                SwitchOff = LoadTextures("Low\\Blocks\\switchOff");

                PickAxe = LoadTextures("Low\\Items\\pickaxe");
                Pliers = LoadTextures("Low\\Items\\pliers");
                Key = LoadTextures("Low\\Items\\key");
                Message = LoadTextures("Low\\Items\\message");

                Grid = LoadTextures("Low\\Sprites\\grid");
                GridDestroyed = LoadTextures("Low\\Sprites\\gridDestroyed");
                Door = LoadTextures("Low\\Sprites\\door");
                LevelUp = LoadTextures("Low\\Sprites\\LevelUp");
                LevelDown = LoadTextures("Low\\Sprites\\LevelDown");

                EvilMan = LoadTextures("Low\\Enemies\\evilMan");
            }
            else
            {
                Floor = LoadTextures("Normal\\Blocks\\floor");
                Ceiling = LoadTextures("Normal\\Blocks\\ceiling");
                Wall = LoadTextures("Normal\\Blocks\\wall");
                DestroyableBlock = LoadTextures("Normal\\Blocks\\destroyableBlock");

                SwitchOn = LoadTextures("Normal\\Blocks\\switchOn");
                SwitchOff = LoadTextures("Normal\\Blocks\\switchOff");

                PickAxe = LoadTextures("Normal\\Items\\pickaxe");
                Pliers = LoadTextures("Normal\\Items\\pliers");
                Key = LoadTextures("Normal\\Items\\key");
                Message = LoadTextures("Normal\\Items\\message");

                Grid = LoadTextures("Normal\\Sprites\\grid");
                GridDestroyed = LoadTextures("Normal\\Sprites\\gridDestroyed");
                Door = LoadTextures("Normal\\Sprites\\door");

                LevelUp = LoadTextures("Normal\\Sprites\\LevelUp");
                LevelDown = LoadTextures("Normal\\Sprites\\LevelDown");

                EvilMan = LoadTextures("Normal\\Enemies\\evilMan");
            }

            //GUI
            ItemBarItem = LoadTextures("GUI\\itemBarItem");
            SelectedItemBarItem = LoadTextures("GUI\\selectedItemBarItem");
            Canvas = LoadTextures("GUI\\canvas");

            Particle = new Texture2D(Basic.GraphicsDevice, 1, 1);
            Particle.SetData(new[] { Color.DarkRed });
        }

		private static Texture2D LoadTextures(string path)
		{
            //Lädt eine Textur
            return Basic.Content.Load<Texture2D>("Textures\\" + path);
		}

        public static void UndloadTexture()
        {
            //Gibt alle geladenen Texturen wieder frei

            Floor?.Dispose();

            Ceiling?.Dispose();

            Wall?.Dispose();

            DestroyableBlock?.Dispose();

            PickAxe?.Dispose();

            Pliers?.Dispose();

            Key?.Dispose();

            Grid?.Dispose();

            GridDestroyed?.Dispose();

            Door?.Dispose();

            LevelUp?.Dispose();

            LevelDown?.Dispose();

            ItemBarItem?.Dispose();

            SelectedItemBarItem?.Dispose();

            Canvas?.Dispose();

            SwitchOn?.Dispose();

            SwitchOff?.Dispose();

            EvilMan?.Dispose();

            Particle?.Dispose();

#if DEBUG
            Dummy?.Dispose();
#endif
        }

        #endregion
    }
}
