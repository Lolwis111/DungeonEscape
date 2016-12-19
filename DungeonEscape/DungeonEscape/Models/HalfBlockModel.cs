using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
    public sealed class HalfBlockModel : VertexModel
    {
        public HalfBlockModel()
        {
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0f, 0f, 1f), new Vector2(1f, 0f)));

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector3(1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(1f, 0f, 0f), new Vector2(0.5f, 0f)));

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-1f, 0f, 0f), new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector3(-1f, 0f, 0f), new Vector2(0.5f, 0f)));

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

