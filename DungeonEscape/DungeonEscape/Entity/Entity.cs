using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DungeonEscape
{
    public abstract class Entity : IComparable
    {
        #region Fields

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        protected Vector3 _position = Vector3.Zero;

        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        protected Vector3 _rotation = Vector3.Zero;

        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        protected Vector3 _scale = Vector3.One;

        public bool DrawBoundingBox
        {
            get { return _drawBoundingBox; }
            set { _drawBoundingBox = value; }
        }
        protected bool _drawBoundingBox = false;

        public bool Collision
        {
            get { return _collision; }
            set { _collision = value; }
        }
        internal bool _collision = true;

        public Vector3 BoundingBoxScale
        {
            get { return _boundingBoxScale; }
            set { _boundingBoxScale = value; }
        }
        protected Vector3 _boundingBoxScale = Vector3.One;

        public float CameraDistance
        {
            get { return _cameraDistance; }
            set { _cameraDistance = value; }
        }
        protected float _cameraDistance = 1.0f;

        public BoundingBox Box
        {
            get { return _box; }
            set { _box = value; }
        }
        internal BoundingBox _box;

        public Matrix WorldMatrix
        {
            get { return _worldMatrix; }
            set { _worldMatrix = value; }
        }
        protected Matrix _worldMatrix = Matrix.Identity;

        public Vector3 BoxMin
        {
            get { return _min; }
            set { _min = value; }
        }
        protected Vector3 _min = new Vector3(-0.5f);

        public Vector3 BoxMax
        {
            get { return _max; }
            set { _max = value; }
        }
        protected Vector3 _max = new Vector3(0.5f);

        #endregion

        #region Methods

        public Entity(float x, float y, float z)
        {
            _position = new Vector3(x, y, z);
            _rotation = Vector3.Zero;
            _scale = new Vector3(1f, 1f, 1f);
            _drawBoundingBox = false;
            _boundingBoxScale = new Vector3(1.2f, 1.2f, 1.2f);
            _collision = true;
            _box = new BoundingBox(new Vector3(-0.5f), new Vector3(0.5f));

        }

        public virtual void Update()
        {
            _worldMatrix = Matrix.CreateScale(_scale) * Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, _rotation.Z) * Matrix.CreateTranslation(_position);

            _box = new BoundingBox(Vector3.Transform(_min, Matrix.CreateScale(_boundingBoxScale) * Matrix.CreateTranslation(_position)),
                Vector3.Transform(_max, Matrix.CreateScale(_boundingBoxScale) * Matrix.CreateTranslation(_position))); 

             _cameraDistance = Vector3.Distance(_position, GameScreen.Camera.Position);
        }

        public virtual void Render()
        {
        }

        public virtual void Init()
        {
        }

        public void Remove()
        {
            GameScreen.Level.Entities.Remove(this);
        }

        public void Draw(Model model)
        {
            model.Draw(_worldMatrix);

            if (_drawBoundingBox)
                BoundingBoxRenderer.Render(_box, Basic.GraphicsDevice, GameScreen.Camera.View, GameScreen.Camera.Projection, Color.Red);
        }

        public int CompareTo(object obj)
        {
            Entity entity = (Entity)obj;
            return (int)(entity._cameraDistance * 1000f - _cameraDistance * 1000f);
        }


        #endregion
    }
}
