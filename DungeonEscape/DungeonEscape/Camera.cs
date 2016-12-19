using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Screens;

namespace DungeonEscape
{
    public sealed class Camera
    {
        #region Fields

        private float _yaw, _pitch;

        /// <summary>
        /// Laufgeschwindigkeit
        /// </summary>
		private const float Speed = 0.015f;

        /// <summary>
        /// Rotationsgeschwindigkeit
        /// (normalerweise 0.0013)
        /// </summary>
		private const float RotationSpeed = 0.00013f;

		private float _tempf;

        private int _oldX, _oldY;

		private bool _movedLastFrame;

        /// <summary>
        /// Aktuelle View-Matrix der Kamera
        /// </summary>
        public Matrix View { get; set; }

        /// <summary>
        /// Aktueller Projektionsmatrix der Kamera
        /// </summary>
        public Matrix Projection { get; set; }

        /// <summary>
        /// Aktuelle Position der Kamera
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Die aktuelle Blickrichtung der Kamera
        /// </summary>
        public Vector3 LookAt { get; set; }

        /// <summary>
        /// Der aktuelle Strahl in Blickrichtung
        /// </summary>
        public Ray CameraRay { get; set; }

        /// <summary>
        /// Der aktuell druch die Kamera sichtbare Bereich
        /// </summary>
        public BoundingFrustum Frustum { get; set; }

        private Matrix _rotX, _rotY;

        #endregion

        #region Methods

        /// <summary>
        /// Erstellt eine neues Kameraobjekt
        /// </summary>
        public Camera()
		{
			Position = new Vector3(0f, 0f, 5f);
			Frustum = new BoundingFrustum(View * Projection);
            CameraRay = default(Ray);

			View = Matrix.CreateLookAt(Position, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 50f);
        }
		
        /// <summary>
        /// Berechnet den neuen Zustand der Kamera
        /// </summary>
        /// <param name="gameTime">Die Spielzeit</param>
        public void Update(GameTime gameTime)
		{
			_movedLastFrame = false;
			_tempf += 0.3f;

			CameraRay = CreateRay();
            Frustum.Matrix = View * Projection;
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 12f);

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
				Position += new Vector3(0f, (float)Math.Sin(_tempf) * 0.0025f, 0f);
			}
		
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

            if (!Contains(new Vector3(Position.X + vector.X, 0f, Position.Z)))
			{
                Position += new Vector3(vector.X, 0f, 0f);
			}

            if (!Contains(new Vector3(Position.X, 0f, Position.Z + vector.Z)))
			{
                Position += new Vector3(0f, 0f, vector.Z);
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

            Vector3 value = Vector3.Transform(new Vector3(0f, 0f, -1f), _rotX*_rotY);

            Vector3 vector = Position + value;
			LookAt = vector;

			View = Matrix.CreateLookAt(Position, vector, Vector3.Up);
		}

        public Ray CreateRay()
		{
			int x = Basic.WindowSize.Width / 2;
			int y = Basic.WindowSize.Height / 2;

            Vector3 vector = Basic.GraphicsDevice.Viewport.Unproject(new Vector3(x, y, 0f), Projection, View, Matrix.Identity);
            Vector3 value = Basic.GraphicsDevice.Viewport.Unproject(new Vector3(x, y, 1f), Projection, View, Matrix.Identity);

            Vector3 direction = value - vector;

			direction.Normalize();

			return new Ray(vector, direction);
        }

        #endregion
    }
}
