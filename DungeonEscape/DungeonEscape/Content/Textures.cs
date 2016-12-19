using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Content
{
	public static class Textures
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

#if DEBUG
        public static Texture2D Dummy;
#endif

#endregion

        #region Methods

        public static void LoadTexture()
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
                Floor = LoadTexture("Low\\Blocks\\floor");
                Ceiling = LoadTexture("Low\\Blocks\\ceiling");
                Wall = LoadTexture("Low\\Blocks\\wall");
                DestroyableBlock = LoadTexture("Low\\Blocks\\destroyableBlock");

                SwitchOn = LoadTexture("Low\\Blocks\\switchOn");
                SwitchOff = LoadTexture("Low\\Blocks\\switchOff");

                PickAxe = LoadTexture("Low\\Items\\pickaxe");
                Pliers = LoadTexture("Low\\Items\\pliers");
                Key = LoadTexture("Low\\Items\\key");
                Message = LoadTexture("Low\\Items\\message");

                Grid = LoadTexture("Low\\Sprites\\grid");
                GridDestroyed = LoadTexture("Low\\Sprites\\gridDestroyed");
                Door = LoadTexture("Low\\Sprites\\door");
                LevelUp = LoadTexture("Low\\Sprites\\LevelUp");
                LevelDown = LoadTexture("Low\\Sprites\\LevelDown");
            }
            else
            {
                Floor = LoadTexture("Normal\\Blocks\\floor");
                Ceiling = LoadTexture("Normal\\Blocks\\ceiling");
                Wall = LoadTexture("Normal\\Blocks\\wall");
                DestroyableBlock = LoadTexture("Normal\\Blocks\\destroyableBlock");

                SwitchOn = LoadTexture("Normal\\Blocks\\switchOn");
                SwitchOff = LoadTexture("Normal\\Blocks\\switchOff");

                PickAxe = LoadTexture("Normal\\Items\\pickaxe");
                Pliers = LoadTexture("Normal\\Items\\pliers");
                Key = LoadTexture("Normal\\Items\\key");
                Message = LoadTexture("Normal\\Items\\message");

                Grid = LoadTexture("Normal\\Sprites\\grid");
                GridDestroyed = LoadTexture("Normal\\Sprites\\gridDestroyed");
                Door = LoadTexture("Normal\\Sprites\\door");

                LevelUp = LoadTexture("Normal\\Sprites\\LevelUp");
                LevelDown = LoadTexture("Normal\\Sprites\\LevelDown");
            }

            //GUI
            ItemBarItem = LoadTexture("GUI\\itemBarItem");
            SelectedItemBarItem = LoadTexture("GUI\\selectedItemBarItem");
            Canvas = LoadTexture("GUI\\canvas");
		}

		private static Texture2D LoadTexture(string path)
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

#if DEBUG
            Dummy?.Dispose();
#endif
        }

        #endregion
    }
}
