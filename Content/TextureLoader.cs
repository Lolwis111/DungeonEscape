using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DRW = System.Drawing;
using System.IO;

namespace Content
{
    public class TextureLoader
    {
        /// <summary>
        /// Lädt eine beliebige bitmapkompatible Bilddatei und erzeugt ein Texture2D-Objekt mit dem entsprechenden GraphicsDevice daraus.
        /// </summary>
        /// <param name="fileName">Datei die geladen werden soll.</param>
        /// <param name="device">Das zu verwendende GraphicsDevice.</param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string fileName, GraphicsDevice device)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();

            DRW.Bitmap bitmap = (DRW.Bitmap)DRW.Image.FromFile(fileName);
            int width = bitmap.Width;
            int height = bitmap.Height;
            Color[] data = new Color[width*height];
            DRW.Color alphaKey = DRW.Color.FromArgb(255, 0, 255);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    DRW.Color c = bitmap.GetPixel(x, y);
                    if(c == alphaKey)
                        data[x + width * y] = Color.FromNonPremultiplied(255, 255, 255, 0);
                    else
                        data[x + width * y] = Color.FromNonPremultiplied(c.R, c.G, c.B, c.A);
                }
            }

            bitmap?.Dispose();
            Texture2D texture = new Texture2D(device, width, height);
            texture.SetData(data);
            return texture;
        }
    }
}
