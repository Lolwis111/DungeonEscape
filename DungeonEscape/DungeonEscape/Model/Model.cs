using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonEscape
{
	public abstract class Model : IDisposable
	{
        public List<VertexPositionNormalTexture> Vertex
        {
            get { return _vertex; }
            set { _vertex = value; }
        }
		protected List<VertexPositionNormalTexture> _vertex = new List<VertexPositionNormalTexture>();
        

		public static Model FloorModel = new FloorFace();
		public static Model BlockModel = new BlockModel();
		public static Model SpriteModel = new SpriteFace();
        public static Model HalfBlockModel = new HalfBlockModel();
		
        public VertexBuffer vertexBuffer
		{
            get { return _buffer; }
            set { _buffer = value; }
		}
        internal VertexBuffer _buffer = null;
        private int vertexCount = 0;

        protected void SetUp()
		{
            _buffer = new VertexBuffer(Basic.GraphicsDevice, typeof(VertexPositionNormalTexture), _vertex.Count, BufferUsage.WriteOnly);
            _buffer.SetData<VertexPositionNormalTexture>(_vertex.ToArray());
            vertexCount = _vertex.Count / 3;
            _vertex.Clear();
            _vertex = null;
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

            Basic.GraphicsDevice.SetVertexBuffer(_buffer, 0);
            Basic.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexCount);
		}

        #region IDisposable Support

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _buffer.Dispose();
                }

                disposedValue = true;
                _buffer = null;
                _vertex.Clear();
                _vertex = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
