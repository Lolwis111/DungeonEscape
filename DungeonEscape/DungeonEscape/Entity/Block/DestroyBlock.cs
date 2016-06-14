using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher bei Aktivierung zerstört wird
    /// </summary>
    public sealed class DestroyBlock : Entity
	{
        private GamePadState gamePadState;

		public DestroyBlock(float x, float y, float z) : base(x, y, z)
		{
            
		}

		public override void Update()
		{
            gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);

			if (num.HasValue && num.Value < 2f && GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Pickaxe &&
                ((Mouse.GetState().LeftButton == ButtonState.Pressed && GameScreen.OldMouseState.LeftButton == ButtonState.Released) ||
                (gamePadState.Buttons.A == ButtonState.Pressed && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released)))
			{
                Sounds.Destroy.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
				GameScreen.Level.ToRemove.Add(this);
			}

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.DestroyableBlock);
			base.Draw(Model.BlockModel);
		}
	}
}
