using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonEscape
{
    public sealed class Key : Entity
	{
		private float tempf = 0.0f;

        public int ID 
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id = 0;

		public Key(float x, float y, float z) : base(x, y, z)
		{
			_collision = false;
			_scale = new Vector3(0.5f, 0.5f, 1f);
			_boundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			_rotation += new Vector3(0f, 0.03f, 0f);
			tempf += 0.05f;

            _position = new Vector3(_position.X, (float)Math.Sin((double)tempf) * 0.04f, _position.Z);

            if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains && GameScreen.Player.PlayerItemBar.SetItem(new Item() { Type = ItemType.Key }))
            {
                Sounds.Collect.Play();
                base.Remove();
            }

			base.Update();
		}
		
        public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Key);

			base.Draw(Model.SpriteModel);
		}
	}
}
