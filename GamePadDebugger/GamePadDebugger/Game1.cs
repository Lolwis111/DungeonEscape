using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamePadDebugger
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public Game1()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 480,
                PreferredBackBufferHeight = 486
            };
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("SpriteFont1");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GamePadState g = GamePad.GetState(PlayerIndex.One);

            _spriteBatch.Begin();

            if (g.IsConnected)
            {
                _spriteBatch.DrawString(_font, "Triggers/Sticks:", new Vector2(5, 5), Color.Red);
                _spriteBatch.DrawString(_font, "Stick (L); X:   " + g.ThumbSticks.Left.X.ToString(CultureInfo.InvariantCulture), new Vector2(5, 25), Color.Red);
                _spriteBatch.DrawString(_font, "Stick (L); Y:   " + g.ThumbSticks.Left.Y.ToString(CultureInfo.InvariantCulture), new Vector2(5, 45), Color.Red);

                _spriteBatch.DrawString(_font, "Stick (R); X:   " + g.ThumbSticks.Right.X.ToString(CultureInfo.InvariantCulture), new Vector2(5, 65), Color.Red);
                _spriteBatch.DrawString(_font, "Stick (R); Y:   " + g.ThumbSticks.Right.Y.ToString(CultureInfo.InvariantCulture), new Vector2(5, 85), Color.Red);

                _spriteBatch.DrawString(_font, "Trigger (L):    " + g.Triggers.Left.ToString(CultureInfo.InvariantCulture), new Vector2(5, 105), Color.Red);
                _spriteBatch.DrawString(_font, "Trigger (R):    " + g.Triggers.Right.ToString(CultureInfo.InvariantCulture), new Vector2(5, 125), Color.Red);

                _spriteBatch.DrawString(_font, "Stick (L):      " + (g.Buttons.LeftStick == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 145), Color.Red);
                _spriteBatch.DrawString(_font, "Stick (R):      " + (g.Buttons.RightStick == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 165), Color.Red);

                _spriteBatch.DrawString(_font, "Buttons:        ", new Vector2(5, 185), Color.Orange);
                _spriteBatch.DrawString(_font, "A:              " + (g.Buttons.A == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 205), Color.Orange);
                _spriteBatch.DrawString(_font, "B:              " + (g.Buttons.B == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 225), Color.Orange);
                _spriteBatch.DrawString(_font, "X:              " + (g.Buttons.X == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 245), Color.Orange);
                _spriteBatch.DrawString(_font, "Y:              " + (g.Buttons.Y == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 265), Color.Orange);

                _spriteBatch.DrawString(_font, "Special Buttons:", new Vector2(5, 285), Color.Violet);
                _spriteBatch.DrawString(_font, "Start:          " + (g.Buttons.Start == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 305), Color.Violet);
                _spriteBatch.DrawString(_font, "Back:           " + (g.Buttons.Back == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 325), Color.Violet);
                _spriteBatch.DrawString(_font, "Shoulder (R):   " + (g.Buttons.RightShoulder == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 345), Color.Violet);
                _spriteBatch.DrawString(_font, "Shoulder (L):   " + (g.Buttons.LeftShoulder == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 365), Color.Violet);

                _spriteBatch.DrawString(_font, "DPad:           ", new Vector2(5, 385), Color.Green);
                _spriteBatch.DrawString(_font, "Up:             " + (g.DPad.Up == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 405), Color.Green);
                _spriteBatch.DrawString(_font, "Right:          " + (g.DPad.Right == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 425), Color.Green);
                _spriteBatch.DrawString(_font, "Down:           " + (g.DPad.Down == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 445), Color.Green);
                _spriteBatch.DrawString(_font, "Left:           " + (g.DPad.Left == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 465), Color.Green);
            }
            else
            {
                _spriteBatch.DrawString(_font, "Gamepad One not connected!", new Vector2(5, 1), Color.Red);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
