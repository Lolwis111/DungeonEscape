using DungeonEscape.Content;
using DungeonEscape.Entities.Block;
using DungeonEscape.GUI.Items;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape.Entities
{
    /// <summary>
    /// Implementiert einen Block welcher als Schiebetür fungiert.
    /// </summary>
    public sealed class DoorBlock : Entity
	{
		private float _tempI;

        private GamePadState _gamePadState;
        private Vector3 _startPosition;

	    public bool HasToMove { get; set; }

	    private float _direction = 0.05f;

	    public int Id { get; set; }

	    public DoorBlock(float x, float y, float z) : base(x, y, z)
		{
		}

        public override void Init()
        {
            if (GameScreen.Level.GetEntity(Position.X, Position.Z + 1f) is WallBlock && GameScreen.Level.GetEntity(Position.X, Position.Z - 1f) is WallBlock)
            {
                Rotation = new Vector3(Rotation.X, 1.57079637f, Rotation.Z);
            }

            BoundingBoxScale = Utils.Utils.CompareFloats(Rotation.Y, 0.0f) ? new Vector3(1f, 1f, 0.5f) : new Vector3(0.5f, 1f, 1f);

            _startPosition = Position;
        }

		public override void Update()
		{
            _gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);

            if (num.HasValue && num.Value < 2f && !HasToMove && Vector3.Distance(_startPosition, Position) < 0.01 
                && CorrectInteraction() 
                && (GameScreen.MouseClicked || _gamePadState.Buttons.A == ButtonState.Pressed 
                && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released))
			{
                //Sounds.Door.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
                HasToMove = true;
                _direction = +0.05f;

            }

			if (HasToMove && Utils.Utils.CompareFloats(Rotation.Y, 0.0f))
			{
				_tempI += _direction;
				Position -= new Vector3(_direction, 0f, 0f);

                if (_tempI > 1f || _tempI < 0f)
                {
                    HasToMove = false;
                    _direction = -_direction;
                }
			}
			else
			{
                if (HasToMove && Utils.Utils.CompareFloats(Rotation.Z, 0.0f))
				{
					_tempI += _direction;
                    Position += new Vector3(0f, 0f, _direction);

                    if (_tempI > 1f || _tempI < 0f)
                    {
                        HasToMove = false;
                        _direction = -_direction;
                    }
                }
			}

			base.Update();
		}

		public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Door);

			Draw(VertexModel.SpriteVertexModel);
		}

        protected override bool CorrectInteraction()
        {
            return (GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Key 
                && GameScreen.Player.PlayerItemBar.SelectedItem.Id == Id);
        }

        public override string GenerateXml()
        {
            return "<entity><type>doorblock</type>"
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