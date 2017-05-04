using DungeonEscape.Content;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;

namespace DungeonEscape.Entities.Block
{
    internal sealed class HalfBlock : Entity
    {
        public HalfBlock(float x, float y, float z) : base(x, y, z)
		{
            EntityType = EntityType.HalfBlock;
        }

        public override void Init()
        {
            Entity entUp = GameScreen.Level.GetEntity(Position.X, Position.Z + 1f);
            Entity entDown = GameScreen.Level.GetEntity(Position.X, Position.Z - 1f);

            Entity entLeft = GameScreen.Level.GetEntity(Position.X - 1f, Position.Z);
            Entity entRight = GameScreen.Level.GetEntity(Position.X + 1f, Position.Z);

            Box = new BoundingBox(new Vector3(-0.1f), new Vector3(0.1f));

            if (entUp is WallBlock || entUp is SwitchBlock)
            {
                Rotation = new Vector3(0f, MathHelper.ToRadians(180), 0f);
                BoxMin = new Vector3(-0.5f, -0.5f, -0.1f);
            }
            else if (entDown is WallBlock || entDown is SwitchBlock)
            {
                Rotation = new Vector3(0f, MathHelper.ToRadians(0), 0f);
                BoxMax = new Vector3(0.5f, 0.5f, 0.1f);
            }
            else if (entLeft is WallBlock || entLeft is SwitchBlock)
            {
                Rotation = new Vector3(0f, MathHelper.ToRadians(90), 0f);
                BoxMax = new Vector3(0.1f, 0.5f, 0.5f);
            }
            else if (entRight is WallBlock || entRight is SwitchBlock)
            {
                Rotation = new Vector3(0f, MathHelper.ToRadians(-90), 0f);
                BoxMin = new Vector3(-0.1f, -0.5f, -0.5f);
            }

            base.Init();
        }

        public override void Render()
        {
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Wall);
            Draw(VertexModel.HalfBlockVertexModel);
        }

        public override string GenerateXml()
        {
            return $"<entity><type>halfblock</type><position>{Position.X};{Position.Y};{Position.Z}</position></entity>";
        }

        /*public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }*/
    }
}
