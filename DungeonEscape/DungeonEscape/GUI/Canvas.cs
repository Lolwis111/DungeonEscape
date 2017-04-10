using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.GUI
{
    internal static class Canvas
    {
        private static Texture2D _canvas;

        public static void SetUpCanvas()
        {
            _canvas = new Texture2D(Basic.GraphicsDevice, 1, 1);
            _canvas.SetData(new[] { Color.White });
        }

        public static void DrawPixel(int x, int y, Color color)
        {
            Basic.SpriteBatch.Draw(_canvas, new Vector2(x, y), color);
        }

        public static void DrawRectangle(Rectangle rect, Color color)
        {
            Basic.SpriteBatch.Draw(_canvas, rect, color);
        }

        public static void DrawBorder(int borderLenght, Rectangle rect, Color color)
        {
            for (int i = 0; i < rect.Width; i += 2)
            {
                for (int j = 0; j < rect.Height; j += 2)
                {
                    if (i < borderLenght || j < borderLenght || i >= rect.Width - borderLenght || j >= rect.Height - borderLenght)
                    {
                        DrawPixel(i + rect.X, j + rect.Y, color);
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
                        DrawPixel(i + rect.X, j + rect.Y, color);
                    else
                        DrawPixel(i + rect.X, j + rect.Y, fillColor);
                }
            }
        }
    }
}
