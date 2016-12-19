using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using DungeonEscape.Screens;

namespace DungeonEscape.Models
{
	public abstract class VertexModel : IDisposable
	{
	    public List<VertexPositionNormalTexture> Vertex { get; set; }

		public static VertexModel FloorVertexModel = new FloorFace();
		public static VertexModel BlockVertexModel = new BlockVertexModel();
		public static VertexModel SpriteVertexModel = new SpriteFace();
        public static VertexModel HalfBlockVertexModel = new HalfBlockVertexModel();
		
        public VertexBuffer VertexBuffer
		{
            get { return Buffer; }
            set { Buffer = value; }
		}
        internal VertexBuffer Buffer;
        private int _vertexCount;

        protected void SetUp()
		{
            Buffer = new VertexBuffer(Basic.GraphicsDevice, typeof(VertexPositionNormalTexture), Vertex.Count, BufferUsage.WriteOnly);
            Buffer.SetData(Vertex.ToArray());

            _vertexCount = Vertex.Count / 3;

            Vertex.Clear();
		}
		
        public void Draw(Matrix world)
		{
            GameScreen.MainEffect.Parameters["World"].SetValue(world);
            GameScreen.MainEffect.Parameters["View"].SetValue(GameScreen.Camera.View);
            GameScreen.MainEffect.Parameters["Projection"].SetValue(GameScreen.Camera.Projection);
            GameScreen.MainEffect.Parameters["LightPosition"].SetValue(GameScreen.Camera.Position);
            GameScreen.MainEffect.Parameters["LightDiffuseColor"].SetValue(Color.Gray.ToVector3());
            GameScreen.MainEffect.Parameters["LightSpecularColor"].SetValue(new Color(200, 200, 200).ToVector3());
            GameScreen.MainEffect.Parameters["LightDistanceSquared"].SetValue(8);
            GameScreen.MainEffect.Parameters["DiffuseColor"].SetValue(Color.Gray.ToVector3());
            GameScreen.MainEffect.Parameters["AmbientLightColor"].SetValue(Color.Black.ToVector3());
            GameScreen.MainEffect.Parameters["EmissiveColor"].SetValue(Color.Gray.ToVector3());
            GameScreen.MainEffect.Parameters["SpecularColor"].SetValue(Color.Black.ToVector3());
            GameScreen.MainEffect.Parameters["SpecularPower"].SetValue(1f);

            GameScreen.MainEffect.CurrentTechnique.Passes["Normal"].Apply();

            Basic.GraphicsDevice.SetVertexBuffer(Buffer, 0);
            Basic.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexCount);
		}

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                Buffer.Dispose();
            }

            _disposedValue = true;

            Buffer = null;

            Vertex.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}