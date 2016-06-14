using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GamePadDebugger
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 486;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
        }
      
        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GamePadState g = GamePad.GetState(PlayerIndex.One);

            spriteBatch.Begin();

            if (g.IsConnected)
            {
                spriteBatch.DrawString(font, "Triggers/Sticks:", new Vector2(5, 5), Color.Red);
                spriteBatch.DrawString(font, "Stick (L); X:   " + g.ThumbSticks.Left.X.ToString(), new Vector2(5, 25), Color.Red);
                spriteBatch.DrawString(font, "Stick (L); Y:   " + g.ThumbSticks.Left.Y.ToString(), new Vector2(5, 45), Color.Red);

                spriteBatch.DrawString(font, "Stick (R); X:   " + g.ThumbSticks.Right.X.ToString(), new Vector2(5, 65), Color.Red);
                spriteBatch.DrawString(font, "Stick (R); Y:   " + g.ThumbSticks.Right.Y.ToString(), new Vector2(5, 85), Color.Red);

                spriteBatch.DrawString(font, "Trigger (L):    " + g.Triggers.Left.ToString(), new Vector2(5, 105), Color.Red);
                spriteBatch.DrawString(font, "Trigger (R):    " + g.Triggers.Right.ToString(), new Vector2(5, 125), Color.Red);

                spriteBatch.DrawString(font, "Stick (L):      " + (g.Buttons.LeftStick == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 145), Color.Red);
                spriteBatch.DrawString(font, "Stick (R):      " + (g.Buttons.RightStick == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 165), Color.Red);

                spriteBatch.DrawString(font, "Buttons:        ", new Vector2(5, 185), Color.Orange);
                spriteBatch.DrawString(font, "A:              " + (g.Buttons.A == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 205), Color.Orange);
                spriteBatch.DrawString(font, "B:              " + (g.Buttons.B == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 225), Color.Orange);
                spriteBatch.DrawString(font, "X:              " + (g.Buttons.X == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 245), Color.Orange);
                spriteBatch.DrawString(font, "Y:              " + (g.Buttons.Y == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 265), Color.Orange);

                spriteBatch.DrawString(font, "Special Buttons:", new Vector2(5, 285), Color.Violet);
                spriteBatch.DrawString(font, "Start:          " + (g.Buttons.Start == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 305), Color.Violet);
                spriteBatch.DrawString(font, "Back:           " + (g.Buttons.Back == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 325), Color.Violet);
                spriteBatch.DrawString(font, "Shoulder (R):   " + (g.Buttons.RightShoulder == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 345), Color.Violet);
                spriteBatch.DrawString(font, "Shoulder (L):   " + (g.Buttons.LeftShoulder == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 365), Color.Violet);

                spriteBatch.DrawString(font, "DPad:           ", new Vector2(5, 385), Color.Green);
                spriteBatch.DrawString(font, "Up:             " + (g.DPad.Up == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 405), Color.Green);
                spriteBatch.DrawString(font, "Right:          " + (g.DPad.Right == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 425), Color.Green);
                spriteBatch.DrawString(font, "Down:           " + (g.DPad.Down == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 445), Color.Green);
                spriteBatch.DrawString(font, "Left:           " + (g.DPad.Left == ButtonState.Pressed ? "true" : "false"), new Vector2(5, 465), Color.Green);
            }
            else
            {
                spriteBatch.DrawString(font, "Gamepad One not connected!", new Vector2(5, 1), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
