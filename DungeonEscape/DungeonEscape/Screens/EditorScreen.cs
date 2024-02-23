using DungeonEscape.Cameras;
using DungeonEscape.Levels;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape.Screens
{
    internal sealed class EditorScreen : Screen
    {
        #region Fields

        public static bool MouseClicked => (Mouse.GetState().LeftButton == ButtonState.Pressed
            && OldMouseState.LeftButton == ButtonState.Released);

        public static MouseState OldMouseState { get; set; }

        public static Camera Camera { get; set; }

        public static Level Level { get; set; }

        private readonly int _levelNumber;

        #endregion

        #region Methods

        public EditorScreen(int level)
        {
            _levelNumber = level;
        }

        public override void Init()
        {
            Utils.Mouse.ShowMouse();

            Camera = new Camera { Mode = CameraMode.DebugCamera };

            Level = new Level(_levelNumber);
            Level.Init();
        }

        public override void Render()
        {
            Level.Render();
        }

        public override void Update()
        {
            Level.Update();

            Camera.Update(Basic.GameTime);

            OldMouseState = Mouse.GetState();
        }

        #endregion
    }
}
