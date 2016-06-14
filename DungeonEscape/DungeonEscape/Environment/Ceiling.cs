using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    public sealed class Ceiling
	{
		private int _width = 20;
		private int _height = 20;

		public Ceiling(int width, int height)
		{
            _width = width;
            _height = height;
		}

		public void Render()
		{
            for (int i = 0; i < _width; i++)
			{
                for (int j = 0; j < _height; j++)
				{
					GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Ceiling);
					Model.FloorModel.Draw(Matrix.CreateTranslation((float)i, 1f, (float)j));
				}
			}
		}
	}
}
