using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
	public sealed class BlockVertexModel : VertexModel
	{
		public BlockVertexModel()
		{
            // Vorne
			Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 0f)));

            // Rechts
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 0f)));

            // Links
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));

            // Hinten
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));

			SetUp();
		}
	}
}
