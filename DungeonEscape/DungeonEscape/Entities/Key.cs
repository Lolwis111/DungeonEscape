using Microsoft.Xna.Framework;
using System;
using DungeonEscape.Content;
using DungeonEscape.GUI.Items;
using DungeonEscape.Models;
using DungeonEscape.Screens;

namespace DungeonEscape.Entities
{
    public sealed class Key : Entity
	{
		private float _tempf;

	    public int Id { get; set; }

	    public Key(float x, float y, float z) : base(x, y, z)
		{
			Collision = false;
			Scale = new Vector3(0.5f, 0.5f, 1f);
			BoundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			Rotation += new Vector3(0f, 0.03f, 0f);
			_tempf += 0.05f;

            Position = new Vector3(Position.X, (float)Math.Sin(_tempf) * 0.04f, Position.Z);

            if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains && GameScreen.Player.PlayerItemBar.SetItem(new Item() { Type = ItemType.Key }))
            {
                //Sounds.Collect.Play();
                Remove();
            }

			base.Update();
		}
		
        public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Key);

			Draw(VertexModel.SpriteVertexModel);
		}

        public override string GenerateXml()
        {
            return "<entity><type>key</type>"
                + $"<position>{Position.X};{Position.Y};{Position.Z}</position>"
                   + $"<id>{Id}</id>"
                   + "</entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }
    }
}
