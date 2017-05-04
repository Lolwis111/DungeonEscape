using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using DungeonEscape.Content;
using DungeonEscape.Screens;

namespace DungeonEscape.Models
{
    internal abstract class VertexModel : IDisposable
    {
        protected List<VertexPositionNormalTexture> VertexData = new List<VertexPositionNormalTexture>();

        public static VertexModel FloorVertexModel;
        public static VertexModel BlockVertexModel;
        public static VertexModel SpriteVertexModel;
        public static VertexModel HalfBlockVertexModel;
        public static VertexModel ParticleVertexModel;

        public static void Init(int floorWidth, int floorHeight)
        {
            FloorVertexModel = new FloorFace(floorWidth, floorHeight);
            BlockVertexModel = new BlockModel();
            SpriteVertexModel = new SpriteFace();
            HalfBlockVertexModel = new HalfBlockModel();
            ParticleVertexModel = new ParticleModel();
        }

        public VertexBuffer VertexBuffer { get; set; }
        private int _vertexCount;

        protected void SetUp()
		{
            VertexBuffer = new VertexBuffer(Basic.GraphicsDevice, typeof(VertexPositionNormalTexture), VertexData.Count, BufferUsage.WriteOnly);
            VertexBuffer.SetData(VertexData.ToArray());

            _vertexCount = VertexData.Count / 3;

            VertexData.Clear();
		}
		
        public void Draw(Matrix world)
		{
            Effects.MainEffect.Parameters["World"].SetValue(world);
            Effects.MainEffect.Parameters["View"].SetValue(GameScreen.Camera.View);
            Effects.MainEffect.Parameters["Projection"].SetValue(GameScreen.Camera.Projection);
            Effects.MainEffect.Parameters["LightPosition"].SetValue(GameScreen.Camera.Position);
            Effects.MainEffect.Parameters["LightDiffuseColor"].SetValue(Color.Gray.ToVector3());
            Effects.MainEffect.Parameters["LightSpecularColor"].SetValue(new Color(200, 200, 200).ToVector3());
            Effects.MainEffect.Parameters["LightDistanceSquared"].SetValue(8);
            Effects.MainEffect.Parameters["DiffuseColor"].SetValue(Color.Gray.ToVector3());
            Effects.MainEffect.Parameters["AmbientLightColor"].SetValue(Color.Black.ToVector3());
            Effects.MainEffect.Parameters["EmissiveColor"].SetValue(Color.Gray.ToVector3());
            Effects.MainEffect.Parameters["SpecularColor"].SetValue(Color.Black.ToVector3());
            Effects.MainEffect.Parameters["SpecularPower"].SetValue(1f);

            if(Basic.DebugMode)
                Effects.MainEffect.CurrentTechnique.Passes["Debug"].Apply();
            else
                Effects.MainEffect.CurrentTechnique.Passes["Normal"].Apply();

            Basic.GraphicsDevice.SetVertexBuffer(VertexBuffer, 0);
            Basic.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexCount);
		}

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                VertexBuffer.Dispose();
            }

            _disposedValue = true;

            VertexBuffer = null;

            VertexData.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
