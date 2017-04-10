
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Models
{
    internal sealed class FloorFace : VertexModel
	{
		public FloorFace(int width, int height)
		{
		    for (int x = 0; x < width; x++)
		    {
		        for (int y = 0; y < height; y++)
		        {
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + 0.5f), Vector3.Up, new Vector2(0f, 1f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + -0.5f), Vector3.Up, new Vector2(0f, 0f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y + -0.5f), Vector3.Up, new Vector2(1f, 0f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y + -0.5f), Vector3.Up, new Vector2(1f, 0f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y + 0.5f), Vector3.Up, new Vector2(1f, 1f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + 0.5f), Vector3.Up, new Vector2(0f, 1f)));

                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y + -0.5f), Vector3.Down, new Vector2(1f, 0f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + -0.5f), Vector3.Down, new Vector2(0f, 0f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + 0.5f), Vector3.Down, new Vector2(0f, 1f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + -0.5f, -0.5f, y + 0.5f), Vector3.Down, new Vector2(0f, 1f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y + 0.5f), Vector3.Down, new Vector2(1f, 1f)));
                    VertexData.Add(new VertexPositionNormalTexture(new Vector3(x + 0.5f, -0.5f, y+-0.5f), Vector3.Down, new Vector2(1f, 0f)));
                }
		    }

			SetUp();
		}
	}
}
