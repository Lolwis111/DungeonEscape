using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace DungeonEscape
{
	public sealed class Pliers : Entity
	{
		private float tempF = 0.0f;

		public Pliers(float x, float y, float z) : base(x, y, z)
		{
			_collision = false;
		    _scale = new Vector3(0.5f, 0.5f, 1f);
			_boundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			_rotation += new Vector3(0f, 0.03f, 0f);
			tempF += 0.05f;

            _position = new Vector3(_position.X, (float)Math.Sin((double)tempF) * 0.04f, _position.Z);

            if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains && GameScreen.Player.PlayerItemBar.SetItem(new Item() { Type = ItemType.Pliers }))
            {
                Sounds.Collect.Play();
                base.Remove();
            }

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Pliers);

			base.Draw(Model.SpriteModel);
		}
	}
}
