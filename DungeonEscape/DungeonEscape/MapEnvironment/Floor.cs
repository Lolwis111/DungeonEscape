using Microsoft.Xna.Framework;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.Models;

namespace DungeonEscape.MapEnvironment
{
    internal sealed class Floor
	{
		private readonly int _width;
		private readonly int _height;

		public Floor(int width, int height)
		{
            _width = width;
            _height = height;
		}

		public void Render()
		{
            for (float i = 0; i < _width; i++)
			{
                for (float j = 0; j < _height; j++)
				{
					GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Floor);
					VertexModel.FloorVertexModel.Draw(Matrix.CreateTranslation(i, 0f, j));
				}
			}
		}
	}
}
