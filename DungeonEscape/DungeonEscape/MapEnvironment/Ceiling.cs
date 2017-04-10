using Microsoft.Xna.Framework;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.Models;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.MapEnvironment
{
    internal sealed class Ceiling
	{
	    public void Render()
		{
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Ceiling);
            VertexModel.FloorVertexModel.Draw(Matrix.CreateTranslation(0, 1f, 0));
        }
	}
}
