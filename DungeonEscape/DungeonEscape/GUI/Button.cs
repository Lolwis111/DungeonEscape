using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
namespace DungeonEscape
{
	public sealed class Button
    {
        #region Fields

        private Click clickFunction = null;

        private Rectangle rect = Rectangle.Empty;
		private bool hover = false;

		private MouseState mouseStateCurrent;
		private MouseState mouseStatePrevious;

        public string Text
		{
            get { return _text; }
            set  { _text = value; }
		}
        private string _text = string.Empty;

        public int PositionX
		{
			get { return rect.X; }
			set { rect.X = value; }
		}
		
        public int PositionY
		{
			get { return rect.Y; }
			set { rect.Y = value; }
		}
		
        public bool Enabled
		{
            get { return _enabled; }
            set { _enabled = value; }
        }
        private bool _enabled = true;
		
        public bool Visible
		{
            get { return _visible; }
            set { _visible = value; }
		}
        private bool _visible = true;

        #endregion

        #region Methods

        public Button(int x, int y, int width, int height, string text, Click click)
		{
			_text = text;
			rect = new Rectangle(x, y, width, height);
			clickFunction = click;
		}
		
        public void Update()
		{
			hover = rect.Contains(new Point(mouseStateCurrent.X, mouseStateCurrent.Y));
			mouseStateCurrent = Mouse.GetState();

			if (hover && mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released && _enabled)
			{
                Sounds.Click.Play();
                if (clickFunction != null)
                    clickFunction();
                else
                    throw new NotImplementedException();
			}

			mouseStatePrevious = mouseStateCurrent;
		}
		
        public void Render()
		{
			if (_visible)
			{
                float x = (float)(rect.X + rect.Width / 2) - Basic.MainFont.MeasureString(_text).X / 2f;
                float y = (float)(rect.Y + rect.Height / 2) - Basic.MainFont.MeasureString(_text).Y / 2f;

				if (_enabled)
				{
					Canvas.DrawBorder(5, rect, hover ? Color.Gray : Color.White);
                    Basic.SpriteBatch.DrawString(Basic.MenuFont, _text, new Vector2(x, y), hover ? Color.Gray : Color.White);

					return;
				}

                Canvas.DrawBorder(5, rect, Color.Gray);
                Basic.SpriteBatch.DrawString(Basic.MenuFont, _text, new Vector2(x, y), Color.Gray);
			}
        }

        #endregion
    }
}
