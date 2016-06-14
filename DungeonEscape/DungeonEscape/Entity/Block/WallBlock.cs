using Microsoft.Xna.Framework;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen einfachen, unbeweglichen Block
    /// </summary>
    public sealed class WallBlock : Entity
	{
		public WallBlock(float x, float y, float z) : base(x, y, z)
		{
            
		}

		public override void Render()
		{
			GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Wall);
			base.Draw(Model.BlockModel);
		}
	}
}
