using Microsoft.Xna.Framework;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.Models;

namespace DungeonEscape.MapEnvironment
{
    internal sealed class Ceiling
	{
		private readonly int _width;
		private readonly int _height;

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
					VertexModel.FloorVertexModel.Draw(Matrix.CreateTranslation(i, 1f, j));
				}
			}
		}
	}
}
