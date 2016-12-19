using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
    public sealed class HalfBlockVertexModel : VertexModel
    {
        public HalfBlockVertexModel()
        {
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 0f)));

            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 0f)));

            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 0f)));
            Vertex.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 0f)));

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

