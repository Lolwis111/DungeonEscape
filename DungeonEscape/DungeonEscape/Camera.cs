using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace DungeonEscape
{
    internal sealed class Camera
    {
        #region Fields

        private float _yaw, _pitch;

        /// <summary>
        /// Laufgeschwindigkeit
        /// </summary>
		public const float Speed = 0.015f;

        /// <summary>
        /// Rotationsgeschwindigkeit
        /// (normalerweise 0.0013)
        /// </summary>
		private const float RotationSpeed = 0.0013f;

		//private float _tempF;

        private int _oldX, _oldY;

		private bool _movedLastFrame;

        /// <summary>
        /// Aktuelle View-Matrix der Kamera
        /// </summary>
        public Matrix View
        {
            get { return _view; }
            set { _view = value; }
        }
        private Matrix _view;

        /// <summary>
        /// Aktueller Projektionsmatrix der Kamera
        /// </summary>
        public Matrix Projection
        {
            get { return _projection; }
            set { _projection = value; }
        }
        private Matrix _projection;

        /// <summary>
        /// Aktuelle Position der Kamera
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector3 _position;

        /// <summary>
        /// Die aktuelle Blickrichtung der Kamera
        /// </summary>
        public Vector3 LookAt
        {
            get { return _lookAt; }
            set { _lookAt = value; }
        }
        private Vector3 _lookAt;

        /// <summary>
        /// Der aktuelle Strahl in Blickrichtung
        /// </summary>
        public Ray CameraRay
        {
            get { return _cameraRay; }
            set { _cameraRay = value; }
        }
        private Ray _cameraRay;

        /// <summary>
        /// Der aktuell druch die Kamera sichtbare Bereich
        /// </summary>
        public BoundingFrustum Frustum
        {
            get { return _frustum; }
            set { _frustum = value; }
        }
        private BoundingFrustum _frustum;

        private Matrix _rotX, _rotY;

        private KeyboardState oldKeyboardState;

        #endregion

        #region Methods

        /// <summary>
        /// Erstellt eine neues Kameraobjekt
        /// </summary>
        public Camera()
		{
			_position = new Vector3(0f, 0f, 5f);
			_frustum = new BoundingFrustum(_view * _projection);
            _cameraRay = default(Ray);

			_view = Matrix.CreateLookAt(Position, Vector3.Zero, Vector3.Up);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 50f);
        }
		
        /// <summary>
        /// Berechnet den neuen Zustand der Kamera
        /// </summary>
        /// <param name="gameTime">Die Spielzeit</param>
        public void Update(GameTime gameTime)
		{
			_movedLastFrame = false;
			//_tempF += 0.3f;

			_cameraRay = CreateRay();
            _frustum.Matrix = _view * _projection;
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 12f);

			KeyboardState keyboardState = Keyboard.GetState();
			GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

			if (keyboardState.IsKeyDown(Keys.W) || gamepadState.ThumbSticks.Left.Y > 0f)
			{
                Vector3 v = Vector3.Forward * Speed;
				Move(v);
			}
			else if (keyboardState.IsKeyDown(Keys.S) || gamepadState.ThumbSticks.Left.Y < 0f)
			{
                Vector3 v2 = Vector3.Backward * Speed;
				Move(v2);
			}

			if (keyboardState.IsKeyDown(Keys.A) || gamepadState.ThumbSticks.Left.X < 0f)
			{
                Vector3 v3 = Vector3.Left * Speed;
				Move(v3);
			}
			else if (keyboardState.IsKeyDown(Keys.D) || gamepadState.ThumbSticks.Left.X > 0f)
			{
                Vector3 v4 = Vector3.Right * Speed;
                Move(v4);
			}

		    if (keyboardState.IsKeyDown(Keys.P) && oldKeyboardState.IsKeyUp(Keys.P))
		    {
                Console.WriteLine($"(X,Y,Z): {Position.X}, {Position.Y}, {Position.Z}");
		    }

		    MouseState mouseState = Mouse.GetState();
			int dx = mouseState.X - _oldX;
			int dy = mouseState.Y - _oldY;

			_yaw -= RotationSpeed * dx;
			_pitch -= RotationSpeed * dy;

            _yaw -= RotationSpeed * gamepadState.ThumbSticks.Right.X * 15;
            _pitch += RotationSpeed * gamepadState.ThumbSticks.Right.Y * 15;

			_pitch = MathHelper.Clamp(_pitch, -1.5f, 1.5f);

			UpdateMatrices();

			ResetCursor();

			if (_movedLastFrame)
			{
				//Position += new Vector3(0f, (float)Math.Sin(_tempf) * 0.0025f, 0f);
			}

		    oldKeyboardState = keyboardState;
		}
		
        private void ResetCursor()
		{
		    if (!Basic.Game.IsActive) return;

            Mouse.SetPosition(Basic.WindowSize.Width / 2, Basic.WindowSize.Height / 2);
		    _oldX = Basic.WindowSize.Width / 2;
		    _oldY = Basic.WindowSize.Height / 2;
		}
		
        private void Move(Vector3 v)
		{
			_movedLastFrame = true;

			Vector3 vector = Vector3.Transform(v, Matrix.CreateRotationY(_yaw));

            if (!Contains(new Vector3(_position.X + vector.X, _position.Y, _position.Z)))
			{
                _position += new Vector3(vector.X, _position.Y, 0f);
			}

            if (!Contains(new Vector3(_position.X, _position.Y, _position.Z + vector.Z)))
			{
                _position += new Vector3(0f, _position.Y, vector.Z);
			}
		}

        private static bool Contains(Vector3 v)
		{
			bool result = false;

            for (short i = 0; i < GameScreen.Level.Entities.Count; i++ )
            {
                if (!GameScreen.Level.Entities[i].Collision ||
                    GameScreen.Level.Entities[i].Box.Contains(v) != ContainmentType.Contains)
                    continue;

                result = true;
                break;
            }

			return result;
		}
		
        private void UpdateMatrices()
		{
            Matrix.CreateRotationX(_pitch, out _rotX);
            Matrix.CreateRotationY(_yaw, out _rotY);

            Vector3 value = Vector3.Transform(new Vector3(0f, 0f, -1f), _rotX * _rotY);

            Vector3 vector = _position + value;
			_lookAt = vector;

			_view = Matrix.CreateLookAt(_position, vector, Vector3.Up);
		}

        public Ray CreateRay()
		{
			int x = Basic.WindowSize.Width / 2;
			int y = Basic.WindowSize.Height / 2;

            Vector3 vector = Basic.GraphicsDevice.Viewport.Unproject(new Vector3(x, y, 0f), _projection, _view, Matrix.Identity);
            Vector3 value = Basic.GraphicsDevice.Viewport.Unproject(new Vector3(x, y, 1f), _projection, _view, Matrix.Identity);

            Vector3 direction = value - vector;

			direction.Normalize();

			return new Ray(vector, direction);
        }

        #endregion
    }
}
