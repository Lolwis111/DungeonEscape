using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MapCreator2D
{
    public static class Basic
    {
        public static ContentManager Content;
        public static GraphicsDevice Device;
        public static SpriteBatch SpriteBatch;
        public static Rectangle CursorRectangle = Rectangle.Empty;
        public static MouseState NewMouseState;
        public static List<Tile> Tiles = new List<Tile>();
        public static Main GameWindow;

        public static void Load()
        {
            Textures.LoadTextures();

            SpriteBatch = new SpriteBatch(Device);

            ClearTiles();
        }

        public static void Unload()
        {
            foreach (Tile t in Tiles)
                t.Dispose();

            Textures.DisposeTextures();
            SpriteBatch.Dispose();
        }

        public static void Update(GameTime time)
        {
            NewMouseState = Mouse.GetState();

            CursorRectangle = new Rectangle(NewMouseState.X, NewMouseState.Y, 1, 1);

            foreach (Tile t in Tiles)
                t.Update();
        }

        public static void Render()
        {
            Device.Clear(Color.LightGray);
            SpriteBatch.Begin();

            foreach (Tile t in Tiles)
                t.Render();

            SpriteBatch.End();
        }

        public static void ClearTiles()
        {
            for (int x = 0; x < 20; x++ )
            {
                for (int y = 0; y < 20; y++)
                {
                    Tiles.Add(new Tile(x * 30, y * 30, TileType.None));
                }
            }
        }
    }
}
