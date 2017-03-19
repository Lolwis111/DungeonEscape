using System;
using DungeonEscape.Debug;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;

namespace DungeonEscape.Entities
{
    internal abstract class Entity : IComparable
    {
        #region Fields

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector3 _position;

        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        private Vector3 _rotation;

        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        private Vector3 _scale;

        public bool DrawBoundingBox
        {
            get { return _drawBoundingBox; }
            set { _drawBoundingBox = value; }
        }
        private bool _drawBoundingBox;

        public bool Collision
        {
            get { return _collision; }
            set { _collision = value; }
        }
        private bool _collision;

        public Vector3 BoundingBoxScale
        {
            get { return _boundingBoxScale; }
            set { _boundingBoxScale = value; }
        }
        private Vector3 _boundingBoxScale;

        public float CameraDistance
        {
            get { return _cameraDistance; }
            set { _cameraDistance = value; }
        }
        private float _cameraDistance = 1.0f;

        public BoundingBox Box
        {
            get { return _box; }
            set { _box = value; }
        }
        private BoundingBox _box;

        public Matrix WorldMatrix
        {
            get { return _worldMatrix; }
            set { _worldMatrix = value; }
        }
        private Matrix _worldMatrix = Matrix.Identity;

        public Vector3 BoxMin
        {
            get { return _boxMin; }
            set { _boxMin = value; }
        }
        private Vector3 _boxMin = new Vector3(-0.5f);

        public Vector3 BoxMax
        {
            get { return _boxMax; }
            set { _boxMax = value; }
        }
        private Vector3 _boxMax = new Vector3(0.5f);

        #endregion

        #region Methods

        protected Entity(float x, float y, float z)
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

            _box = new BoundingBox(Vector3.Transform(_boxMin, Matrix.CreateScale(_boundingBoxScale) * Matrix.CreateTranslation(_position)),
                Vector3.Transform(_boxMax, Matrix.CreateScale(_boundingBoxScale) * Matrix.CreateTranslation(_position))); 

             _cameraDistance = Vector3.Distance(Position, GameScreen.Camera.Position);
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

        public void Draw(VertexModel vertexModel)
        {
            vertexModel.Draw(_worldMatrix);

            if (_drawBoundingBox)
                BoundingBoxRenderer.Render(Box, Basic.GraphicsDevice, GameScreen.Camera.View, GameScreen.Camera.Projection, Color.Red);
        }

        public int CompareTo(object obj)
        {
            Entity entity = (Entity)obj;
            return (int)(entity.CameraDistance * 1000f - _cameraDistance * 1000f);
        }

        protected virtual bool CheckCorrectInteraction()
        {
            return true;
        }

        public virtual string GenerateXml()
        {
            return "";
        }

        public virtual EntityType GetEntityType()
        {
            return EntityType.Entity;
        }

        #endregion
    }
}
