using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DungeonEscape
{
    public sealed class Message : Entity
    {
        private float tempF = 0.0f;

        public string Text 
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _text = string.Empty;

        private bool renderText = false;

        public Message(float x, float y, float z) : base(x, y, z)
        {
            _collision = false;
            _scale = new Vector3(0.3f, 0.3f, 1.0f);
            _boundingBoxScale = new Vector3(0.3f);
        }

        public override void Update()
        {
            _rotation += new Vector3(0f, 0.03f, 0f);
            tempF += 0.05f;

            _position = new Vector3(_position.X, (float)Math.Sin((double)tempF) * 0.04f, _position.Z);

            if (Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains)
                renderText = true;
            else
                renderText = false;


            base.Update();
        }

        public override void Render()
        {
            if (renderText)
            {
                float x = (Basic.WindowSize.Width / 2) - (Basic.MainFont.MeasureString(_text).X / 2);

                Basic.SpriteBatch.Draw(Textures.Canvas, new Rectangle((int)x - 10, Basic.WindowSize.Height - 110, (int)Basic.MainFont.MeasureString(_text).X + 20, (int)Basic.MainFont.MeasureString(_text).Y + 20), Color.White);

                Basic.SpriteBatch.DrawString(Basic.MainFont, _text, new Vector2(x, Basic.WindowSize.Height - 100), Color.White);
            }


            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Message);

            base.Draw(Model.SpriteModel);
            base.Render();
        }

        /*
        public override void Render()
        {
            if (renderText)
            {
                float x = (Basic.WindowSize.Width / 2) - (Basic.MainFont.MeasureString(_text).X / 2);

                Basic.SpriteBatch.Draw(Textures.Canvas, new Rectangle((int)x - 10, Basic.WindowSize.Height - 110, (int)Basic.MainFont.MeasureString(_text).X + 20, (int)Basic.MainFont.MeasureString(_text).Y + 20), Color.White);

                Basic.SpriteBatch.DrawString(Basic.MainFont, _text, new Vector2(x, Basic.WindowSize.Height - 100), Color.White);
            }


            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Message);

            base.Draw(Model.SpriteModel);
        }*/
    }
}
