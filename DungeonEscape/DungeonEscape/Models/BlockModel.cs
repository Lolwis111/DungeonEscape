using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
	public sealed class BlockModel : VertexModel
	{
		public BlockModel()
		{
            // Vorne
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 0f)));

            // Rechts
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 0f)));

            // Links
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));

            // Hinten
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));

			SetUp();
		}
	}
}
