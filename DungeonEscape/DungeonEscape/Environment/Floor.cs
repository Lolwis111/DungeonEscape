using Microsoft.Xna.Framework;

namespace DungeonEscape
{
    public sealed class Floor
	{
		private int _width = 20;
		private int _height = 20;

		public Floor(int width, int height)
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
					GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Floor);
					Model.FloorModel.Draw(Matrix.CreateTranslation(i, 0f, j));
				}
			}
		}
	}
}
