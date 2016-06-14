using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace DungeonEscape
{
	public sealed class BlockModel : Model
	{
		public BlockModel()
		{
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f, 0f, 1f), new Vector2(1f, 0f)));

			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(1f, 0f)));

			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-1f, 0f, 0f), new Vector2(1f, 0f)));

			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 1f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(0f, 0f)));
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0f, 0f, -1f), new Vector2(1f, 0f)));

			base.SetUp();
		}
	}
}
