using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
    internal sealed class HalfBlockModel : VertexModel
    {
        public HalfBlockModel()
        {
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), Vector3.Backward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.Backward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), Vector3.Backward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), Vector3.Backward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.Backward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), Vector3.Backward, new Vector2(1f, 0f)));
            
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.0f), Vector3.Right, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), Vector3.Right, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Right, new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Right, new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.0f), Vector3.Right, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), Vector3.Right, new Vector2(0.5f, 0f)));

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.Left, new Vector2(0.5f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.0f), Vector3.Left, new Vector2(0.5f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), Vector3.Left, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), Vector3.Left, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), Vector3.Left, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.0f), Vector3.Left, new Vector2(0.5f, 0f)));

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), Vector3.Forward, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), Vector3.Forward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), Vector3.Forward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), Vector3.Forward, new Vector2(1f, 0f)));

            SetUp();
        }
    }
}

