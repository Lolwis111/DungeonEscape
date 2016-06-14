using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher bei den Spieler bei der Aktivierung in den nächsten Level lädt
    /// </summary>
    public sealed class LevelUp : Entity
	{
		private float tempf = 0.0f;

		public LevelUp(float x, float y, float z) : base(x, y, z)
		{
			_collision = false;
			_scale = new Vector3(0.3f, 0.3f, 1f);
			_boundingBoxScale = new Vector3(0.45f);
		}

		public override void Update()
		{
			_rotation += new Vector3(0f, 0.03f, 0f);
			tempf += 0.05f;
            _position = new Vector3(_position.X, (float)Math.Sin((double)tempf) * 0.04f, _position.Z);

			if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains)
			{
				if (GameScreen.IsTutorialRound)
				{
                    Basic.setScreen(new MainMenuScreen());
				}

				GameScreen.Level = new Level(GameScreen.Level.LevelNumber + 1);
                GameScreen.Level.Init();
			}

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.LevelUp);

			base.Draw(Model.SpriteModel);
		}
	}
}
