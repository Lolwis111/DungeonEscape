using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher als Schiebetür fungiert.
    /// </summary>
    public sealed class DoorBlock : Entity
	{
		private float _tempI = 0.0f;

        private GamePadState _gamePadState;
        private Vector3 _startPosition;

        public bool HasToMove
        {
            get { return _hasToMove; }
            set
            {
                _hasToMove = value;
            }
        }
		private bool _hasToMove = false;

        private float _direction = 0.05f;

        public int ID 
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id = 0;

		public DoorBlock(float x, float y, float z) : base(x, y, z)
		{
		}

        public override void Init()
        {
            if (GameScreen.Level.getEntity(_position.X, _position.Z + 1f) is WallBlock && GameScreen.Level.getEntity(_position.X, _position.Z - 1f) is WallBlock)
            {
                _rotation = new Vector3(_rotation.X, 1.57079637f, _rotation.Z);
            }

            if (_rotation.Y == 0f)
            {
                _boundingBoxScale = new Vector3(1f, 1f, 0.5f);
            }
            else
            {
                _boundingBoxScale = new Vector3(0.5f, 1f, 1f);
            }

            _startPosition = _position;
        }

		public override void Update()
		{
            _gamePadState = GamePad.GetState(PlayerIndex.One);

			float? num = Box.Intersects(GameScreen.Camera.CameraRay);
            if (num.HasValue && num.Value < 2f && !_hasToMove && Vector3.Distance(_startPosition, _position) < 0.01 &&
                GameScreen.Player.PlayerItemBar.SelectedItem.Type == ItemType.Key && 
                GameScreen.Player.PlayerItemBar.SelectedItem.ID == _id && ((Mouse.GetState().LeftButton == ButtonState.Pressed && 
                GameScreen.OldMouseState.LeftButton == ButtonState.Released) || (_gamePadState.Buttons.A == ButtonState.Pressed && 
                GameScreen.OldGamePadState.Buttons.A == ButtonState.Released)))
			{
                Sounds.Door.Play();
				GameScreen.Player.PlayerItemBar.RemoveSelectedItem();
				_hasToMove = true;
                _direction = +0.05f;

            }

			if (_hasToMove && _rotation.Y == 0f)
			{
				_tempI += _direction;
				_position -= new Vector3(_direction, 0f, 0f);

                if (_tempI > 1f || _tempI < 0f)
                {
                    _hasToMove = false;
                    _direction = -_direction;
                }
			}
			else
			{
                if (_hasToMove && _rotation.Z == 0f)
				{
					_tempI += _direction;
                    _position += new Vector3(0f, 0f, _direction);

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
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Door);

			base.Draw(Model.SpriteModel);
		}
	}
}