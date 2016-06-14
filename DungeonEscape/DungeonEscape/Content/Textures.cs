using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
	public static class Textures
    {
        #region Fields

        public static Texture2D Floor
        {
            get { return _floor; }
        }
        private static Texture2D _floor = null;

        public static Texture2D Ceiling
        {
            get { return _ceiling; }
        }
        private static Texture2D _ceiling = null;

        public static Texture2D Wall
        {
            get { return _wall; }
        }
        private static Texture2D _wall = null;

        public static Texture2D DestroyableBlock
        {
            get { return _destroyableBlock; }
        }
        private static Texture2D _destroyableBlock = null;

        public static Texture2D PickAxe
        {
            get { return _pickAxe; }
        }
        private static Texture2D _pickAxe = null;

        public static Texture2D Pliers
        {
            get { return _pliers; }
        }
        private static Texture2D _pliers = null;

        public static Texture2D Key
        {
            get { return _key; }
        }
        private static Texture2D _key = null;

        public static Texture2D Message
        {
            get { return _message; }
        }
        private static Texture2D _message = null;

        public static Texture2D Grid
        {
            get { return _grid; }
        }
        private static Texture2D _grid = null;

        public static Texture2D GridDestroyed
        {
            get { return _gridDestroyed; }
        }
        private static Texture2D _gridDestroyed = null;

        public static Texture2D Door
        {
            get { return _door; }
        }
        private static Texture2D _door = null;

		public static Texture2D LevelUp
        {
            get { return _levelUp; }
        }
        private static Texture2D _levelUp = null;

		public static Texture2D LevelDown
        {
            get { return _levelDown; }
        }
        private static Texture2D _levelDown = null;

        public static Texture2D ItemBarItem
        {
            get { return _itemBarItem; }
        }
        private static Texture2D _itemBarItem = null;

        public static Texture2D SelectedItemBarItem
        {
            get { return _selectedItemBarItem; }
        }
        private static Texture2D _selectedItemBarItem = null;

        public static Texture2D Canvas
        {
            get { return _canvas; }
        }
        private static Texture2D _canvas = null;

        public static Texture2D SwitchOn
        {
            get { return _switchOn; }
        }
        private static Texture2D _switchOn = null;

        public static Texture2D SwitchOff
        {
            get { return _switchOff; }
        }
        private static Texture2D _switchOff = null;

#if DEBUG
        public static Texture2D DUMMY
        {
            get { return _dummy; }
        }
        private static Texture2D _dummy = null;
#endif

#endregion

        #region Methods

        public static void LoadTexture()
		{
            //Vor dem Laden alle Texturen freigeben (falls bereits vorher etwas geladen wurde)
            UndloadTexture();

#if DEBUG
            _dummy = new Texture2D(Basic.GraphicsDevice, 1, 1);
            _dummy.SetData<Microsoft.Xna.Framework.Color>(new Microsoft.Xna.Framework.Color[] { Microsoft.Xna.Framework.Color.White });
#endif

            //Alle Texturen laden
            //Verzeichnis: %startup%/Content/Textures/

            if (Basic.UseSmallTextures)
            {
                _floor = loadTexture("Low\\Blocks\\floor");
                _ceiling = loadTexture("Low\\Blocks\\ceiling");
                _wall = loadTexture("Low\\Blocks\\wall");
                _destroyableBlock = loadTexture("Low\\Blocks\\destroyableBlock");

                _switchOn = loadTexture("Low\\Blocks\\switchOn");
                _switchOff = loadTexture("Low\\Blocks\\switchOff");

                _pickAxe = loadTexture("Low\\Items\\pickaxe");
                _pliers = loadTexture("Low\\Items\\pliers");
                _key = loadTexture("Low\\Items\\key");
                _message = loadTexture("Low\\Items\\message");

                _grid = loadTexture("Low\\Sprites\\grid");
                _gridDestroyed = loadTexture("Low\\Sprites\\gridDestroyed");
                _door = loadTexture("Low\\Sprites\\door");
                _levelUp = loadTexture("Low\\Sprites\\LevelUp");
                _levelDown = loadTexture("Low\\Sprites\\LevelDown");
            }
            else
            {
                _floor = loadTexture("Normal\\Blocks\\floor");
                _ceiling = loadTexture("Normal\\Blocks\\ceiling");
                _wall = loadTexture("Normal\\Blocks\\wall");
                _destroyableBlock = loadTexture("Normal\\Blocks\\destroyableBlock");

                _switchOn = loadTexture("Normal\\Blocks\\switchOn");
                _switchOff = loadTexture("Normal\\Blocks\\switchOff");

                _pickAxe = loadTexture("Normal\\Items\\pickaxe");
                _pliers = loadTexture("Normal\\Items\\pliers");
                _key = loadTexture("Normal\\Items\\key");
                _message = loadTexture("Normal\\Items\\message");

                _grid = loadTexture("Normal\\Sprites\\grid");
                _gridDestroyed = loadTexture("Normal\\Sprites\\gridDestroyed");
                _door = loadTexture("Normal\\Sprites\\door");

                _levelUp = loadTexture("Normal\\Sprites\\LevelUp");
                _levelDown = loadTexture("Normal\\Sprites\\LevelDown");
            }

            //GUI
            _itemBarItem = loadTexture("GUI\\itemBarItem");
            _selectedItemBarItem = loadTexture("GUI\\selectedItemBarItem");
            _canvas = loadTexture("GUI\\canvas");
		}

		private static Texture2D loadTexture(string path)
		{
            //Lädt eine Textur
            return Basic.Content.Load<Texture2D>("Textures\\" + path);
		}

        public static void UndloadTexture()
        {
            //Gibt alle geladenen Texturen wieder frei

            if (_floor != null)
            {
                _floor.Dispose();
                _floor = null;
            }

            if (_ceiling != null)
            {
                _ceiling.Dispose();
                _ceiling = null;
            }

            if (_wall != null)
            {
                _wall.Dispose();
                _wall = null;
            }

            if (_destroyableBlock != null)
            {
                _destroyableBlock.Dispose();
                _destroyableBlock = null;
            }

            if (_pickAxe != null)
            {
                _pickAxe.Dispose();
                _pickAxe = null;
            }

            if (_pliers != null)
            {
                _pliers.Dispose();
                _pliers = null;
            }

            if (_key != null)
            {
                _key.Dispose();
                _key = null;
            }

            if (_grid != null)
            {
                _grid.Dispose();
                _grid = null;
            }

            if (_gridDestroyed != null)
            {
                _gridDestroyed.Dispose();
                _gridDestroyed = null;
            }

            if (_door != null)
            {
                _door.Dispose();
                _door = null;
            }

            if (_levelUp != null)
            {
                _levelUp.Dispose();
                _levelUp = null;
            }

            if (_levelDown != null)
            {
                _levelDown.Dispose();
                _levelDown = null;
            }

            if (_itemBarItem != null)
            {
                _itemBarItem.Dispose();
                _itemBarItem = null;
            }

            if (_selectedItemBarItem != null)
            {
                _selectedItemBarItem.Dispose();
                _selectedItemBarItem = null;
            }

            if (_canvas != null)
            {
                _canvas.Dispose();
                _canvas = null;
            }

            if (_switchOn != null)
            {
                _switchOn.Dispose();
                _switchOn = null;
            }

            if (_switchOff != null)
            {
                _switchOff.Dispose();
                _switchOff = null;
            }

#if DEBUG
            if (_dummy != null)
            {
                _dummy.Dispose();
                _dummy = null;
            }
#endif
        }

        #endregion
    }
}
