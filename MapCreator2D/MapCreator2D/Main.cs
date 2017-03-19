using System;
using Microsoft.Xna.Framework;
using SWF = System.Windows.Forms;
using SD = System.Drawing;

namespace MapCreator2D
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        public ControlWindow _window = new ControlWindow();
        SWF.Form _gameForm;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 600,
                PreferredBackBufferWidth = 600
            };

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content\\MapCreator";

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            IntPtr hWnd = Window.Handle;
            _gameForm = SWF.Control.FromHandle(hWnd).FindForm();
        
            SWF.Application.EnableVisualStyles();
            _window.Show();
            _window.ClientSize = new SD.Size(_window.ClientSize.Width, 600);

            Basic.Content = Content;
            Basic.Device = GraphicsDevice;
            Basic.gameWindow = this;

            Basic.Load();
        }

        protected override void UnloadContent()
        {
            Basic.Unload();


            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _window.Location = new SD.Point(_gameForm.Location.X + _gameForm.Size.Width+11, _gameForm.Location.Y);

            Basic.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Basic.Render();

            base.Draw(gameTime);
        }
    }
}
