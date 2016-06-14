using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace DungeonEscape
{
	public sealed class SpriteFace : Model
	{
		public SpriteFace()
		{
			_vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(0f, 1f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(0f, 0f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(1f, 0f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Backward, new Vector2(1f, 0f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(1f, 1f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Backward, new Vector2(0f, 1f)));

            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(1f, 0f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(0f, 0f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(0f, 1f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(0f, 1f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0f), Vector3.Forward, new Vector2(1f, 1f)));
            _vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0f), Vector3.Forward, new Vector2(1f, 0f)));

			base.SetUp();
		}
	}
}
