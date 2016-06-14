using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace DungeonEscape
{
    public static class Canvas
    {
        private static Texture2D canvas = null;

        public static void setUpCanvas()
        {
            Canvas.canvas = new Texture2D(Basic.GraphicsDevice, 1, 1);
            Canvas.canvas.SetData<Color>(new Color[1] { Color.White });
        }

        public static void DrawPixel(int x, int y, Color color)
        {
            Basic.SpriteBatch.Draw(Canvas.canvas, new Vector2((float)x, (float)y), color);
        }

        public static void DrawRectangle(Rectangle rect, Color color)
        {
            Basic.SpriteBatch.Draw(Canvas.canvas, rect, color);
        }

        public static void DrawBorder(int borderLenght, Rectangle rect, Color color)
        {
            for (int i = 0; i < rect.Width; i += 2)
            {
                for (int j = 0; j < rect.Height; j += 2)
                {
                    if (i < borderLenght || j < borderLenght || i >= rect.Width - borderLenght || j >= rect.Height - borderLenght)
                    {
                        Canvas.DrawPixel(i + rect.X, j + rect.Y, color);
                    }
                }
            }
        }

        public static void DrawBorder(int borderLenght, Rectangle rect, Color color, Color fillColor)
        {
            for (int i = 0; i < rect.Width; i += 2)
            {
                for (int j = 0; j < rect.Height; j += 2)
                {
                    if (i < borderLenght || j < borderLenght || i >= rect.Width - borderLenght || j >= rect.Height - borderLenght)
                        Canvas.DrawPixel(i + rect.X, j + rect.Y, color);
                    else
                        Canvas.DrawPixel(i + rect.X, j + rect.Y, fillColor);
                }
            }
        }
    }
}
