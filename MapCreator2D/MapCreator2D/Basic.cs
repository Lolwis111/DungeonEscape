using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public static SpriteBatch spriteBatch;
        public static Rectangle CursorRectangle = Rectangle.Empty;
        public static MouseState NewMouseState;
        public static List<Tile> Tiles = new List<Tile>();
        public static Main gameWindow;

        public static void Load()
        {
            Textures.LoadTextures();

            spriteBatch = new SpriteBatch(Device);

            clearTiles();
        }

        public static void Unload()
        {
            foreach (Tile t in Tiles)
                t.Dispose();

            Textures.DisposeTextures();
            spriteBatch.Dispose();
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
            spriteBatch.Begin();

            foreach (Tile t in Tiles)
                t.Render();

            spriteBatch.End();
        }

        public static void clearTiles()
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
