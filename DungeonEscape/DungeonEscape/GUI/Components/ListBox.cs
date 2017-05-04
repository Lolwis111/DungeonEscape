using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonEscape.Content;
using System.Collections.Generic;

namespace DungeonEscape.GUI.Components
{
    internal sealed class ListBox : Component
    {
        #region Fields

        private KeyboardState _keyboardStateCurrent;
        private KeyboardState _keyboardStatePrevious;
        private MouseState _mouseStateCurrent;
        private MouseState _mouseStatePrevious;

        public List<object> Items = new List<object>();

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
            }
        }
        private int _selectedIndex;

        private const int ItemHeight = 40;
        private const int ItemWidth = 350;
        private const int ItemDistance = 7;

        #endregion

        #region Methods

        public ListBox()
        {
            _rect = new Rectangle(0, 0, ItemWidth, ItemHeight);
        }

        public override void Update()
        {
            _keyboardStateCurrent = Keyboard.GetState();
            _mouseStateCurrent = Mouse.GetState();

            if (_keyboardStateCurrent.IsKeyDown(Keys.Up)
                && _keyboardStatePrevious.IsKeyUp(Keys.Up))
            {
                if (_selectedIndex == 0)
                    _selectedIndex = Items.Count - 1;
                else
                    _selectedIndex--;
            }
            else if (_keyboardStateCurrent.IsKeyDown(Keys.Down)
                && _keyboardStatePrevious.IsKeyUp(Keys.Down))
            {
                if (_selectedIndex == Items.Count - 1)
                    _selectedIndex = 0;
                else
                    _selectedIndex++;
            }
            else if (_keyboardStateCurrent.IsKeyDown(Keys.Enter)
                && _keyboardStatePrevious.IsKeyDown(Keys.Enter))
            {

            }

            _rect.X = (Basic.WindowSize.Width / 2) - (ItemWidth / 2);

            _keyboardStatePrevious = _keyboardStateCurrent;
            _mouseStatePrevious = _mouseStateCurrent;
        }

        public override void Render()
        {
            for (int i = 0; i < Items.Count; i++)
            {

                int xPos = Basic.WindowSize.Width - ItemWidth - ItemDistance;

                int x = (int)(xPos + ItemWidth / 2
                    - Fonts.SettingsMenuFont.MeasureString(Items[i].ToString()).X / 2f);

                int y = (int)(_rect.Y + ItemHeight / 2
                    - Fonts.SettingsMenuFont.MeasureString(Items[i].ToString()).Y / 2f

                    + (i * (ItemHeight + ItemDistance)));

                if (i == _selectedIndex)
                {
                    Canvas.DrawBorder(2, new Rectangle(xPos, y, ItemWidth, ItemHeight), Color.Gray);
                    Basic.SpriteBatch.DrawString(Fonts.SettingsMenuFont, Items[i].ToString(),
                        new Vector2(x, y), Color.Gray);
                }
                else
                {
                    Canvas.DrawBorder(2, new Rectangle(xPos, y, ItemWidth, ItemHeight), Color.White);
                    Basic.SpriteBatch.DrawString(Fonts.SettingsMenuFont, Items[i].ToString(),
                        new Vector2(x, y), Color.White);
                }
            }
        }

        #endregion
    }
}
