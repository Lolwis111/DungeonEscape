using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher bei den Spieler bei der Aktivierung in den vorherigen Level lädt
    /// </summary>
    public sealed class LevelDown : Entity
	{
		private float tempf = 0.0f;
		
        public LevelDown(float x, float y, float z) : base(x, y, z)
		{
			_collision = false;
			_scale = new Vector3(0.3f, 0.3f, 1f);
			_boundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			_rotation += new Vector3(0f, 0.03f, 0f);

			tempf += 0.05f;

            _position = new Vector3(0f, (float)Math.Sin((double)tempf) * 0.04f, 0f);

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

			base.Draw(Model.SpriteModel);
		}
	}
}
