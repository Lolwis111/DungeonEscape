using System;
using DungeonEscape.Debug;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;

namespace DungeonEscape.Entities
{
    public abstract class Entity : IComparable
    {
        #region Fields

        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public bool DrawBoundingBox { get; set; }

        public bool Collision { get; set; }

        public Vector3 BoundingBoxScale { get; set; }
    
        public float CameraDistance { get; set; } = 1.0f;

        public BoundingBox Box { get; set; }

        public Matrix WorldMatrix { get; set; } = Matrix.Identity;

        public Vector3 BoxMin { get; set; } = new Vector3(-0.5f);

        public Vector3 BoxMax { get; set; } = new Vector3(0.5f);

        #endregion

        #region Methods

        protected Entity(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            Rotation = Vector3.Zero;
            Scale = new Vector3(1f, 1f, 1f);
            DrawBoundingBox = false;
            BoundingBoxScale = new Vector3(1.2f, 1.2f, 1.2f);
            Collision = true;
            Box = new BoundingBox(new Vector3(-0.5f), new Vector3(0.5f));

        }

        public virtual void Update()
        {
            WorldMatrix = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) * Matrix.CreateTranslation(Position);

            Box = new BoundingBox(Vector3.Transform(BoxMin, Matrix.CreateScale(BoundingBoxScale) * Matrix.CreateTranslation(Position)),
                Vector3.Transform(BoxMax, Matrix.CreateScale(BoundingBoxScale) * Matrix.CreateTranslation(Position))); 

             CameraDistance = Vector3.Distance(Position, GameScreen.Camera.Position);
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
            vertexModel.Draw(WorldMatrix);

            if (DrawBoundingBox)
                BoundingBoxRenderer.Render(Box, Basic.GraphicsDevice, GameScreen.Camera.View, GameScreen.Camera.Projection, Color.Red);
        }

        public int CompareTo(object obj)
        {
            Entity entity = (Entity)obj;
            return (int)(entity.CameraDistance * 1000f - CameraDistance * 1000f);
        }

        protected virtual bool CorrectInteraction()
        {
            return true;
        }

        public abstract string GenerateXml();

        public abstract EntityType GetEntityType();

        #endregion
    }
}
