using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Content;

namespace _3DTester
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _tex;

        public Game1()
        {
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tex = TextureLoader.LoadTexture("E:/[Bilder]/wtf.jpg", GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            _tex.Dispose();
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_tex, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
