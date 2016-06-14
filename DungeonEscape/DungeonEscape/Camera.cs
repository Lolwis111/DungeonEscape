using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
namespace DungeonEscape
{
    public sealed class Camera
    {
        #region Fields

        private float yaw = 0.0f, pitch = 0.0f;
		private const float speed = 0.015f;
		private const float rotationSpeed = 0.0013f;
		private float tempf = 0.0f;

        private int oldX = 0, oldY = 0;

		private bool movedLastFrame = false;
		
        public Matrix View
		{
            get { return _view; }
            set { _view = value; }
		}
        private Matrix _view = Matrix.Identity;
		
        public Matrix Projection
		{
            get { return _projection; }
            set { _projection = value; }
		}
        private Matrix _projection = Matrix.Identity;
		
        public Vector3 Position
		{
            get { return _position; }
            set { _position = value; }
		}
        private Vector3 _position = Vector3.Zero;
		
        public Vector3 LookAt
		{
            get { return _lookAt; }
            set { _lookAt = value; }
		}
        private Vector3 _lookAt = Vector3.Forward;
		
        public Ray CameraRay
		{
            get { return _cameraRay; }
            set { _cameraRay = value; }
		}
        private Ray _cameraRay = default(Ray);
		
        public BoundingFrustum Frustum
		{
            get { return _frustum; }
            set { _frustum = null; }
		}
        private BoundingFrustum _frustum = null;

        private Matrix rotX, rotY;

        #endregion

        #region Methods

        public Camera()
		{
			_position = new Vector3(0f, 0f, 5f);
			_frustum = new BoundingFrustum(_view * _projection);
			_cameraRay = default(Ray);

			_view = Matrix.CreateLookAt(_position, Vector3.Zero, Vector3.Up);
			_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 50f);
        }
		
        public void Update(GameTime gameTime)
		{
			movedLastFrame = false;
			tempf += 0.3f;

			_cameraRay = createRay();
            _frustum.Matrix = _view * _projection;
			_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 12f);

			KeyboardState keyboardState = Keyboard.GetState();
			GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

			if (keyboardState.IsKeyDown(Keys.W) || gamepadState.ThumbSticks.Left.Y > 0f)
			{
				Vector3 v = Vector3.Forward * speed;
				move(v);
			}
			else if (keyboardState.IsKeyDown(Keys.S) || gamepadState.ThumbSticks.Left.Y < 0f)
			{
				Vector3 v2 = Vector3.Backward * speed;
				move(v2);
			}

			if (keyboardState.IsKeyDown(Keys.A) || gamepadState.ThumbSticks.Left.X < 0f)
			{
				Vector3 v3 = Vector3.Left * speed;
				move(v3);
			}
			else if (keyboardState.IsKeyDown(Keys.D) || gamepadState.ThumbSticks.Left.X > 0f)
			{
				Vector3 v4 = Vector3.Right * speed;
                move(v4);
			}

			MouseState mouseState = Mouse.GetState();
			int dx = mouseState.X - oldX;
			int dy = mouseState.Y - oldY;

			yaw -= rotationSpeed * (float)dx;
			pitch -= rotationSpeed * (float)dy;

            yaw -= rotationSpeed * gamepadState.ThumbSticks.Right.X * 15;
            pitch += rotationSpeed * gamepadState.ThumbSticks.Right.Y * 15;

			pitch = MathHelper.Clamp(pitch, -1.5f, 1.5f);

			UpdateMatrices();

			ResetCursor();

			if (movedLastFrame)
			{
				_position += new Vector3(0f, (float)Math.Sin((double)tempf) * 0.0025f, 0f);
			}
		
        }
		
        private void ResetCursor()
		{
			if (Basic.Game.IsActive)
			{
				Mouse.SetPosition(Basic.WindowSize.Width / 2, Basic.WindowSize.Height / 2);
				oldX = Basic.WindowSize.Width / 2;
				oldY = Basic.WindowSize.Height / 2;
			}
		}
		
        private void move(Vector3 v)
		{
			movedLastFrame = true;

			Vector3 vector = Vector3.Transform(v, Matrix.CreateRotationY(yaw));

            if (!contains(new Vector3(_position.X + vector.X, 0f, _position.Z)))
			{
				_position += new Vector3(vector.X, 0f, 0f);
			}

            if (!contains(new Vector3(_position.X, 0f, _position.Z + vector.Z)))
			{
				_position += new Vector3(0f, 0f, vector.Z);
			}
		}
		
        public bool contains(Vector3 v)
		{
			bool result = false;

            for (short i = 0; i < GameScreen.Level.Entities.Count; i++ )
            {
                if (GameScreen.Level.Entities[i]._collision && GameScreen.Level.Entities[i]._box.Contains(v) == ContainmentType.Contains)
                {
                    result = true;
                    break;
                }
            }

			return result;
		}
		
        private void UpdateMatrices()
		{
            Matrix.CreateRotationX(pitch, out rotX);
            Matrix.CreateRotationY(yaw, out rotY);

            Vector3 value = Vector3.Transform(new Vector3(0f, 0f, -1f), rotX*rotY);

            Vector3 vector = _position + value;
			_lookAt = vector;

			_view = Matrix.CreateLookAt(_position, vector, Vector3.Up);
		}
		
        private Ray createRay()
		{
			int x = Basic.WindowSize.Width / 2;
			int y = Basic.WindowSize.Height / 2;
			
            Vector3 vector = Basic.GraphicsDevice.Viewport.Unproject(new Vector3((float)x, (float)y, 0f), _projection, _view, Matrix.Identity);
            Vector3 value = Basic.GraphicsDevice.Viewport.Unproject(new Vector3((float)x, (float)y, 1f), _projection, _view, Matrix.Identity);

			Vector3 direction = value - vector;

			direction.Normalize();

			return new Ray(vector, direction);
        }

        #endregion
    }
}
