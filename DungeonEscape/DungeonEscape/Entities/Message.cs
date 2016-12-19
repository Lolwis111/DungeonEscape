using System;
using DungeonEscape.Content;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;
using DungeonEscape.Models;

namespace DungeonEscape.Entities
{
    public sealed class Message : Entity
    {
        private float _tempf;

        public string Text { get; set; }

        private bool _renderText;

        public Message(float x, float y, float z) : base(x, y, z)
        {
            Collision = false;
            Scale = new Vector3(0.3f, 0.3f, 1.0f);
            BoundingBoxScale = new Vector3(0.3f);
        }

        public override void Update()
        {
            Rotation += new Vector3(0f, 0.03f, 0f);
            _tempf += 0.05f;

            Position = new Vector3(Position.X, (float)Math.Sin(_tempf) * 0.04f, Position.Z);

            _renderText = Box.Contains(GameScreen.Camera.Position) == ContainmentType.Contains;


            base.Update();
        }

        public override void Render()
        {
            if (_renderText)
            {
                float x = (float)Basic.WindowSize.Width / 2 - Basic.MainFont.MeasureString(Text).X / 2;

                Basic.SpriteBatch.Draw(Textures.Canvas, new Rectangle((int)x - 10, Basic.WindowSize.Height - 110, 
                    (int)Basic.MainFont.MeasureString(Text).X + 20, (int)Basic.MainFont.MeasureString(Text).Y + 20), Color.White);

                Basic.SpriteBatch.DrawString(Basic.MainFont, Text, new Vector2(x, Basic.WindowSize.Height - 100), Color.White);
            }


            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Message);

            Draw(VertexModel.SpriteVertexModel);
            base.Render();
        }

        public override string GenerateXml()
        {
            return $"<entity><type>message</type><position>{Position.X};{Position.Y};{Position.Z}" +
                   $"</position><text>{Text}</text></entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Sprite;
        }

        /*
        public override void Render()
        {
            if (_renderText)
            {
                float x = (Basic.WindowSize.Width / 2) - (Basic.MainFont.MeasureString(_text).X / 2);

                Basic.SpriteBatch.Draw(Textures.Canvas, new Rectangle((int)x - 10, Basic.WindowSize.Height - 110, (int)Basic.MainFont.MeasureString(_text).X + 20, (int)Basic.MainFont.MeasureString(_text).Y + 20), Color.White);

                Basic.SpriteBatch.DrawString(Basic.MainFont, _text, new Vector2(x, Basic.WindowSize.Height - 100), Color.White);
            }


            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Message);

            base.Draw(VertexModel.SpriteVertexModel);
        }*/
    }
}
