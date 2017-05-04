using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Content;
using DungeonEscape.Entities.Block;
using DungeonEscape.GUI.Items;
using DungeonEscape.Models;
using DungeonEscape.Screens;

namespace DungeonEscape.Entities
{
    /// <summary>
    /// Implementiert einen Block welcher bei der Aktivierung teilweise zerstört wird
    /// </summary>
    internal sealed class GridBlock : Entity
	{
        private GamePadState _gamePadState;
		public bool Destroyed;

		public GridBlock(float x, float y, float z) : base(x, y, z)
		{
            EntityType = EntityType.GridBlock;
        }

        public override void Init()
        {
            Entity entUp = GameScreen.Level.GetEntity(Position.X, Position.Z + 1f);
            Entity entDown = GameScreen.Level.GetEntity(Position.X, Position.Z - 1f);

            if (entUp is WallBlock && entDown is WallBlock || entUp is GridBlock && entDown is GridBlock ||
                entUp is WallBlock && entDown is GridBlock || entUp is GridBlock && entDown is WallBlock)
            {
                Rotation = new Vector3(Rotation.X, (float)Math.PI / 2, Rotation.Z);
            }

            BoundingBoxScale = Utils.Utils.CompareFloats(Rotation.Y, 0.0f) ? new Vector3(1f, 1f, 0.5f) : new Vector3(0.5f, 1f, 1f);
        }

		public override void Update()
		{
            _gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);
			if (Destroyed)
			{
				Collision = false;
			}

            if (num.HasValue && num.Value < 2f && !Destroyed 
                && CheckCorrectInteraction() 
                && (GameScreen.MouseClicked || _gamePadState.Buttons.A == ButtonState.Pressed 
                && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released))
			{
                Sounds.Destroy.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
				Destroyed = true;
			}

			base.Update();
		}

		public override void Render()
        {
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Destroyed ? Textures.GridDestroyed : Textures.Grid); 
			Draw(VertexModel.SpriteVertexModel);
		}

        protected override bool CheckCorrectInteraction()
        {
            return GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Pliers;
        }

        public override string GenerateXml()
        {
            return "<entity><type>gridblock</type>" 
                + $"<position>{Position.X};{Position.Y};{Position.Z}</position>"
                   + $"<_destroyed>{(Destroyed ? "true" : "false")}</_destroyed>"
                   + "</entity>";
        }

        /*public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }*/
    }
}