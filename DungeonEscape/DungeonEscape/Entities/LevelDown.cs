using Microsoft.Xna.Framework;
using System;
using DungeonEscape.Content;
using DungeonEscape.Levels;
using DungeonEscape.Models;
using DungeonEscape.Screens;

namespace DungeonEscape.Entities
{
    /// <summary>
    /// Implementiert einen Block welcher bei den Spieler bei der Aktivierung in den vorherigen Level lädt
    /// </summary>
    public sealed class LevelDown : Entity
	{
		private float _tempf;
		
        public LevelDown(float x, float y, float z) : base(x, y, z)
		{
			Collision = false;
			Scale = new Vector3(0.3f, 0.3f, 1f);
			BoundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			Rotation += new Vector3(0f, 0.03f, 0f);

			_tempf += 0.05f;

            Position = new Vector3(0f, (float)Math.Sin(_tempf) * 0.04f, 0f);

			if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains)
			{
                GameScreen.Level = new Level(GameScreen.Level.LevelNumber - 1); 
                GameScreen.Level.Init();
			}
			
            base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.LevelDown);

			Draw(VertexModel.SpriteVertexModel);
		}

        public override string GenerateXml()
        {
            return "<entity><type>levldown</type>"
                + $"<position>{Position.X};{Position.Y};{Position.Z}</position>"
                   + "</entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }
    }
}
