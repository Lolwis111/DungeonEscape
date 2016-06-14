using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher bei der Aktivierung teilweise zerstört wird
    /// </summary>
    public sealed class GridBlock : Entity
	{
        private GamePadState gamePadState;
		public bool destroyed = false;

		public GridBlock(float x, float y, float z) : base(x, y, z)
		{
		}

        public override void Init()
        {
            Entity entUp = GameScreen.Level.getEntity(_position.X, _position.Z + 1f);
            Entity entDown = GameScreen.Level.getEntity(_position.X, _position.Z - 1f);

            if ((entUp is WallBlock && entDown is WallBlock) || (entUp is GridBlock && entDown is GridBlock) ||
                (entUp is WallBlock && entDown is GridBlock) || (entUp is GridBlock && entDown is WallBlock))
            {
                _rotation = new Vector3(_rotation.X, (float)Math.PI / 2, _rotation.Z);
            }

            if (_rotation.Y == 0f)
            {
                _boundingBoxScale = new Vector3(1f, 1f, 0.5f);
            }
            else
            {
                _boundingBoxScale = new Vector3(0.5f, 1f, 1f);
            }

        }

		public override void Update()
		{
            gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);
			if (destroyed)
			{
				_collision = false;
			}

            if (num.HasValue && num.Value < 2f && !destroyed && GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Pliers && 
                ((Mouse.GetState().LeftButton == ButtonState.Pressed && GameScreen.OldMouseState.LeftButton == ButtonState.Released) ||
                (gamePadState.Buttons.A == ButtonState.Pressed && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released)))
			{
                Sounds.Destroy.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
				destroyed = true;
			}

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(destroyed ? Textures.GridDestroyed : Textures.Grid); 
			base.Draw(Model.SpriteModel);
		}
	}
}