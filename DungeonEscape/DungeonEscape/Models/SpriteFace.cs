using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
	public sealed class SpriteFace : VertexModel
	{
		public SpriteFace()
		{
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(0f, 1f)));

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(1f, 0f)));

			SetUp();
		}
	}
}
