using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Content;

namespace DungeonEscape.GUI
{
    internal sealed class Button
    {
        #region Fields

        private readonly Click _clickFunction;

        private Rectangle _rect;
        private bool _hover;

        private MouseState _mouseStateCurrent;
		private MouseState _mouseStatePrevious;

        public string Text { get; set; }

        public int PositionX
		{
			get { return _rect.X; }
			set { _rect.X = value; }
		}
		
        public int PositionY
		{
			get { return _rect.Y; }
			set { _rect.Y = value; }
		}

        public bool Enabled { get; set; }

        public bool Visible { get; set; }

        #endregion

        #region Methods

        public Button(int x, int y, int width, int height, string text, Click click)
		{
			Text = text;
			_rect = new Rectangle(x, y, width, height);
			_clickFunction = click;
		}
		
        public void Update()
		{
            _hover = _rect.Contains(new Point(_mouseStateCurrent.X, _mouseStateCurrent.Y));
			_mouseStateCurrent = Mouse.GetState();

			if (_hover && (_mouseStateCurrent.LeftButton == ButtonState.Pressed 
                && _mouseStatePrevious.LeftButton == ButtonState.Released 
                || Basic.NewGamePadState.Buttons.A == ButtonState.Pressed 
                && Basic.OldGamePadState.Buttons.A == ButtonState.Released)  && Enabled)
			{
                Sounds.Click.Play();
                if (_clickFunction != null)
                    _clickFunction();
                else
                    throw new NotImplementedException();
			}

			_mouseStatePrevious = _mouseStateCurrent;
		}
		
        public void Render()
		{
		    if (!Visible) return;

		    float x = _rect.X + _rect.Width / 2 - Fonts.MainFont.MeasureString(Text).X / 2f;
		    float y = _rect.Y + _rect.Height / 2 - Fonts.MainFont.MeasureString(Text).Y / 2f;

		    if (Enabled)
		    {
		        Canvas.DrawBorder(5, _rect, _hover ? Color.Gray : Color.White);
		        Basic.SpriteBatch.DrawString(Fonts.MenuFont, Text, new Vector2(x, y), _hover ? Color.Gray : Color.White);

		        return;
		    }

		    Canvas.DrawBorder(5, _rect, Color.Gray);
		    Basic.SpriteBatch.DrawString(Fonts.MenuFont, Text, new Vector2(x, y), Color.Gray);
		}

        #endregion
    }
}
