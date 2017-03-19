using DungeonEscape.Content;
using DungeonEscape.GUI.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using DungeonEscape.Models;

namespace DungeonEscape.Entities.Block
{
    /// <summary>
    /// Implementiert einen Block welcher bei Aktivierung zerstört wird
    /// </summary>
    internal sealed class DestroyBlock : Entity
	{
        private GamePadState _gamePadState;

		public DestroyBlock(float x, float y, float z) : base(x, y, z)
		{
            
		}

		public override void Update()
		{
            _gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);

			if (num.HasValue && num.Value < 2f 
                && CheckCorrectInteraction() 
                && (GameScreen.MouseClicked
                || _gamePadState.Buttons.A == ButtonState.Pressed 
                && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released))
			{
                //Sounds.Destroy.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
				GameScreen.Level.ToRemove.Add(this);
			}

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.DestroyableBlock);
			Draw(VertexModel.BlockVertexModel);
		}

        protected override bool CheckCorrectInteraction()
        {
            return GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Pickaxe;
        }

        public override string GenerateXml()
        {
            return $"<entity><type>destroyblock</type><position>{Position.X};{Position.Y};{Position.Z}</position></entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Block;
        }
    }
}
