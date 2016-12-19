using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SWF = System.Windows.Forms;
using SD = System.Drawing;

namespace MapCreator2D
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        public ControlWindow window = new ControlWindow();
        SWF.Form gameForm;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 600;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content/MapCreator";

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            IntPtr hWnd = Window.Handle;
            gameForm = SWF.Form.FromHandle(hWnd).FindForm();
        
            SWF.Application.EnableVisualStyles();
            window.Show();
            window.ClientSize = new SD.Size(window.ClientSize.Width, 600);

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
            window.Location = new SD.Point(gameForm.Location.X + gameForm.Size.Width+11, gameForm.Location.Y);

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
