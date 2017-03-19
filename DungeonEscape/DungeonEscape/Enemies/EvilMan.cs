using System;
using DungeonEscape.Content;
using DungeonEscape.Debug;
using DungeonEscape.Entities;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;

namespace DungeonEscape.Enemies
{
    internal class EvilMan : Entity
    {
        private float _tempf;

        public EvilMan(float x, float y, float z) : base(x, y, z)
        {
            Scale = new Vector3(0.25f);
            BoundingBoxScale = new Vector3(1f / 3f);
        }

        public override void Update()
        {
            _tempf += 0.07f;

            Position += Vector3.Normalize(GameScreen.Camera.Position - Position) * (Camera.Speed / 2);

            Position = new Vector3(Position.X, (float)Math.Sin(_tempf) * 0.04f, Position.Z);

            base.Update();

            if (Box.Contains(GameScreen.Camera.Position) != ContainmentType.Contains)
                return;

            if (Basic.DebugMode)
            {
                Console.WriteLine("EvilMan touched you.");
            }
            else
            {
                Basic.SetScreen(new LostScreen());
            }
        }

        public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.EvilMan);
            Draw(VertexModel.BlockVertexModel);

            if(Basic.DebugMode) BoundingBoxRenderer.Render(Box, Basic.GraphicsDevice, GameScreen.Camera.View, GameScreen.Camera.Projection, Color.Red);
        }
    }
}
