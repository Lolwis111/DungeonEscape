using DungeonEscape.Content;
using DungeonEscape.Models;
using DungeonEscape.Screens;

namespace DungeonEscape.Entities.Block
{
    /// <summary>
    /// Implementiert einen einfachen, unbeweglichen Block
    /// </summary>
    internal sealed class WallBlock : Entity
	{
		public WallBlock(float x, float y, float z) : base(x, y, z)
		{
            EntityType = EntityType.WallBlock;
        }

		public override void Render()
		{
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Wall);
			Draw(VertexModel.BlockVertexModel);
		}

        public override string GenerateXml()
        {
            return "<entity><type>wallblock</type>"
                + $"<position>{Position.X};{Position.Y};{Position.Z}</position>"
                   + "</entity>";
        }

        /*public override EntityType GetEntityType()
        {
            return EntityType.Block;
        }*/
    }
}
