using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace DungeonEscape.Cameras
{
    internal class Camera
    {
        #region Fields

        // ReSharper disable once InconsistentNaming
        protected float _yaw; 
        private float _pitch;

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

        //private bool _movedLastFrame;

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

        public CameraMode Mode { get; set; }

        private Matrix _rotX, _rotY;

        private KeyboardState _oldKeyboardState;

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
            //_movedLastFrame = false;
            /*_tempF += 0.03f;*/

            CameraRay = CreateRay();
            Frustum.Matrix = View * Projection;
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Basic.GraphicsDevice.Viewport.AspectRatio, 0.01f, 12f);

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.W) || gamepadState.ThumbSticks.Left.Y > 0f)
            {
                Move(Vector3.Forward * Speed);
            }
            else if (keyboardState.IsKeyDown(Keys.S) || gamepadState.ThumbSticks.Left.Y < 0f)
            {
                Move(Vector3.Backward * Speed);
            }

            if (keyboardState.IsKeyDown(Keys.A) || gamepadState.ThumbSticks.Left.X < 0f)
            {
                Move(Vector3.Left * Speed);
            }
            else if (keyboardState.IsKeyDown(Keys.D) || gamepadState.ThumbSticks.Left.X > 0f)
            {
                Move(Vector3.Right * Speed);
            }

            if (Basic.DebugMode)
            {
                if (keyboardState.IsKeyDown(Keys.F1) && _oldKeyboardState.IsKeyUp(Keys.F1))
                {
                    Console.WriteLine($"(X,Y,Z): {Position.X}, {Position.Y}, {Position.Z}");
                }
                else if (keyboardState.IsKeyDown(Keys.F2) && _oldKeyboardState.IsKeyUp(Keys.F2))
                {
                    Debug.Debug.DrawCeiling = !Debug.Debug.DrawCeiling;
                }
                else if (keyboardState.IsKeyDown(Keys.F3) && _oldKeyboardState.IsKeyUp(Keys.F3))
                {
                    Debug.Debug.DrawFloor = !Debug.Debug.DrawFloor;
                }
                else if (keyboardState.IsKeyDown(Keys.F4) && _oldKeyboardState.IsKeyUp(Keys.F4))
                {
                    Debug.Debug.DrawBoundingBoxes = !Debug.Debug.DrawBoundingBoxes;
                }
                else if (keyboardState.IsKeyDown(Keys.F5) && _oldKeyboardState.IsKeyUp(Keys.F5))
                {
                    Debug.Debug.DrawFramerate = !Debug.Debug.DrawFramerate;
                }

                if (keyboardState.IsKeyDown(Keys.Space) || gamepadState.DPad.Up == ButtonState.Pressed)
                {
                    Move(Vector3.Up * Speed);
                }
                else if (keyboardState.IsKeyDown(Keys.LeftShift) || gamepadState.DPad.Down == ButtonState.Pressed)
                {
                    Move(Vector3.Down * Speed);
                }
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

            /*if (_movedLastFrame && Mode != CameraMode.DebugCamera)
            {
                Position += new Vector3(0f, (float)Math.Sin(_tempF) * 0.0025f, 0f);
            }*/

            _oldKeyboardState = keyboardState;
        }

        private void ResetCursor()
        {
            if (!Basic.Game.IsActive) return;

            Mouse.SetPosition(Basic.WindowSize.Width / 2, Basic.WindowSize.Height / 2);
            _oldX = Basic.WindowSize.Width / 2;
            _oldY = Basic.WindowSize.Height / 2;
        }

        protected void Move(Vector3 v)
        {
            //_movedLastFrame = true;

            Vector3 vector = Vector3.Transform(v, Matrix.CreateRotationY(_yaw));

            switch (Mode)
            {
                case CameraMode.PlayerCamera:
                {
                    if (!Contains(new Vector3(Position.X + vector.X, Position.Y, Position.Z)))
                    {
                        Position += new Vector3(vector.X, Position.Y, 0f);
                    }

                    if (!Contains(new Vector3(Position.X, Position.Y, Position.Z + vector.Z)))
                    {
                        Position += new Vector3(0f, Position.Y, vector.Z);
                    }
                    break;
                }
                case CameraMode.DebugCamera:
                {
                    Position += vector;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool Contains(Vector3 v)
        {
            bool result = false;

            for (short i = 0; i < GameScreen.Level.Entities.Count; i++)
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
