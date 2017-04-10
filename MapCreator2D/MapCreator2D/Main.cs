using System;
using Microsoft.Xna.Framework;
using SWF = System.Windows.Forms;
using SD = System.Drawing;

namespace MapCreator2D
{
    public class Main : Game
    {
        public ControlWindow ControlWindow = new ControlWindow();
        SWF.Form _gameForm;

        public Main()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 600,
                PreferredBackBufferWidth = 600
            };

            graphics.ApplyChanges();

            Content.RootDirectory = "Content\\MapCreator";

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            IntPtr hWnd = Window.Handle;
            _gameForm = SWF.Control.FromHandle(hWnd).FindForm();
        
            SWF.Application.EnableVisualStyles();
            ControlWindow.Show();
            ControlWindow.ClientSize = new SD.Size(ControlWindow.ClientSize.Width, 600);

            Basic.Content = Content;
            Basic.Device = GraphicsDevice;
            Basic.GameWindow = this;

            Basic.Load();
        }

        protected override void UnloadContent()
        {
            Basic.Unload();


            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            ControlWindow.Location = new SD.Point(_gameForm.Location.X + _gameForm.Size.Width+11, _gameForm.Location.Y);

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
