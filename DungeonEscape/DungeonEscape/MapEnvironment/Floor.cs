using Microsoft.Xna.Framework;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.Models;

namespace DungeonEscape.MapEnvironment
{
    internal sealed class Floor
	{
		public void Render()
		{
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Floor);
            VertexModel.FloorVertexModel.Draw(Matrix.CreateTranslation(0, 0f, 0));
        }
	}
}
