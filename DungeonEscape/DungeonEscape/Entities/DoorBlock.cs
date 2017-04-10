using System;
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
    internal sealed class DoorBlock : Entity
	{
		private float _tempI;

        private GamePadState _gamePadState;
        private Vector3 _startPosition;

	    public bool HasToMove
	    {
	        get { return _hasToMove; }
	        set { _hasToMove = value; }
	    }
	    private bool _hasToMove;
   
	    private float _direction = 0.05f;

	    public int Id
	    {
	        get { return _id; }
	        set { _id = value; }
	    }
	    private int _id;

	    public DoorBlock(float x, float y, float z) : base(x, y, z)
		{
            _hasToMove = false;
        }

        public override void Init()
        {
            if (GameScreen.Level.GetEntity(Position.X, Position.Z + 1f) is WallBlock 
                && GameScreen.Level.GetEntity(Position.X, Position.Z - 1f) is WallBlock)
            {
                Rotation = new Vector3(Rotation.X, (float)Math.PI/2, Rotation.Z);
            }

            BoundingBoxScale = Utils.Utils.CompareFloats(Rotation.Y, 0.0f) ? new Vector3(1f, 1f, 0.5f) : new Vector3(0.5f, 1f, 1f);

            _startPosition = Position;
            _hasToMove = false;
        }

		public override void Update()
		{
            _gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);

            if (Vector3.Distance(_startPosition, Position) < 0.01 
                && num.HasValue && num.Value < 2f 
                && _hasToMove == false 
                && CheckCorrectInteraction() 
                && (GameScreen.MouseClicked || _gamePadState.Buttons.A == ButtonState.Pressed 
                && GameScreen.OldGamePadState.Buttons.A == ButtonState.Released))
			{
                Sounds.Door.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
                _hasToMove = true;
                _direction = +0.05f;

                Console.WriteLine($"Open Door ID: {Id}");
                Console.WriteLine($"num: {num}");
            }

			if (_hasToMove && Utils.Utils.CompareFloats(Rotation.Y, 0.0f))
			{
				_tempI += _direction;
                Position -= new Vector3(_direction, 0f, 0f);

                if (_tempI > 1f || _tempI < 0f)
                {
                    _hasToMove = false;
                    _direction = -_direction;
                }
			}
			else
			{
                if (_hasToMove && Utils.Utils.CompareFloats(Rotation.Z, 0.0f))
				{
					_tempI += _direction;
                    Position += new Vector3(0f, 0f, _direction);

                    if (_tempI > 1f || _tempI < 0f)
                    {
                        _hasToMove = false;
                        _direction = -_direction;
                    }
                }
			}

			base.Update();
		}

		public override void Render()
        {
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Door);

			Draw(VertexModel.SpriteVertexModel);
		}

        protected override bool CheckCorrectInteraction()
        {
            return (GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Key 
                && GameScreen.Player.PlayerItemBar.SelectedItem.Id == _id);
        }

        public override string GenerateXml()
        {
            return "<entity><type>doorblock</type>"
                + $"<position>{Position.X};{Position.Y};{Position.Z}</position>"
                   + $"<id>{_id}</id>"
                   + "</entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }
    }
}