using System;
using Microsoft.Xna.Framework;

namespace DungeonEscape
{
    public class HalfBlock : Entity
    {
        public HalfBlock(float x, float y, float z) : base(x, y, z)
		{

        }

        public override void Init()
        {
            Entity entUp = GameScreen.Level.getEntity(_position.X, _position.Z + 1f);
            Entity entDown = GameScreen.Level.getEntity(_position.X, _position.Z - 1f);

            Entity entLeft = GameScreen.Level.getEntity(_position.X - 1f, _position.Z);
            Entity entRight = GameScreen.Level.getEntity(_position.X + 1f, _position.Z);

            _box = new BoundingBox(new Vector3(-0.1f), new Vector3(0.1f));

            if (entUp is WallBlock || entUp is SwitchBlock)
            {
                _rotation = new Vector3(0f, MathHelper.ToRadians(180), 0f);
                _min = new Vector3(-0.5f, -0.5f, -0.1f);
            }
            else if (entDown is WallBlock || entDown is SwitchBlock)
            {
                _rotation = new Vector3(0f, MathHelper.ToRadians(0), 0f);
                _max = new Vector3(0.5f, 0.5f, 0.1f);
            }
            else if (entLeft is WallBlock || entLeft is SwitchBlock)
            {
                _rotation = new Vector3(0f, MathHelper.ToRadians(90), 0f);
                _max = new Vector3(0.1f, 0.5f, 0.5f);
            }
            else if (entRight is WallBlock || entRight is SwitchBlock)
            {
                _rotation = new Vector3(0f, MathHelper.ToRadians(-90), 0f);
                _min = new Vector3(-0.1f, -0.5f, -0.5f);
            }

            base.Init();
        }

        public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Wall);
            base.Draw(Model.HalfBlockModel);
        }
    }
}
