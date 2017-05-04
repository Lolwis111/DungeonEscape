using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
    internal sealed class ParticleModel : VertexModel
    {
        public ParticleModel()
        {
            // Vorne

            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, -0.05f, 0.05f), Vector3.Backward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Backward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, 0.05f), Vector3.Backward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, 0.05f), Vector3.Backward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Backward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, 0.05f), Vector3.Backward, new Vector2(1f, 0f)));

            // Rechts
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, 0.05f), Vector3.Right, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, 0.05f), Vector3.Right, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, -0.05f), Vector3.Right, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, -0.05f), Vector3.Right, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, 0.05f), Vector3.Right, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Right, new Vector2(1f, 0f)));

            // Links
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Left, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, -0.05f, 0.05f), Vector3.Left, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, -0.05f, -0.05f), Vector3.Left, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, -0.05f, -0.05f), Vector3.Left, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, -0.05f), Vector3.Left, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Left, new Vector2(1f, 0f)));

            // Hinten
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, -0.05f), Vector3.Forward, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, -0.05f, -0.05f), Vector3.Forward, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, -0.05f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, -0.05f, -0.05f), Vector3.Forward, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Forward, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, -0.05f), Vector3.Forward, new Vector2(1f, 0f)));

            // Oben
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Up, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, -0.05f), Vector3.Up, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Up, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Up, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Up, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, 0.05f), Vector3.Up, new Vector2(1f, 1f)));

            // Unten
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Down, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, -0.05f), Vector3.Down, new Vector2(0f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Down, new Vector2(0f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, -0.05f), Vector3.Down, new Vector2(1f, 0f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(0.05f, 0.05f, 0.05f), Vector3.Down, new Vector2(1f, 1f)));
            VertexData.Add(new VertexPositionNormalTexture(new Vector3(-0.05f, 0.05f, 0.05f), Vector3.Down, new Vector2(0f, 1f)));

            SetUp();
        }
    }
}
