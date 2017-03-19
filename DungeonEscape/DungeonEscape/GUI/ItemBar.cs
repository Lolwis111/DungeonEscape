using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.GUI.Items;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace DungeonEscape.GUI
{
    internal sealed class ItemBar
    {
        #region Fields

        private const int Slots = 6;

        public int SelectedSlot
        {
            get { return _selectedSlot; }
            set { _selectedSlot = value; }
        }

        private int _selectedSlot;

        public Item[] Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private Item[] _items;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        private Item _selectedItem;

        #endregion

        #region Methods

        public ItemBar()
		{
            _items = new Item[Slots];
            Clear();

            SelectedSlot = 0;
		}

		public void Update()
		{
			MouseState state = Mouse.GetState();

			if (state.ScrollWheelValue < GameScreen.OldMouseState.ScrollWheelValue)
			{
                _selectedSlot++;
			}
			else if (state.ScrollWheelValue > GameScreen.OldMouseState.ScrollWheelValue)
		    {
                _selectedSlot--;
		    }

            if (_selectedSlot < 0)
            {
                _selectedSlot = Slots - 1;
            }
            else if (_selectedSlot >= Slots)
            {
                _selectedSlot = 0;
            }

            _selectedItem = _items[_selectedSlot];
		}

		public void Render()
		{
            for (int i = 0; i < Slots; i++)
            {
                Basic.SpriteBatch.Draw(i == _selectedSlot ? Textures.SelectedItemBarItem : Textures.ItemBarItem, 
                    new Rectangle(Basic.WindowSize.Width - 105, 10 + i * 100, 100, 100), Color.White);

                switch (Items[i].Type)
                {
                    case ItemType.Message:
                    case ItemType.None:
                        continue;
                    case ItemType.Key:
                        Basic.SpriteBatch.Draw(Textures.Key, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                        break;
                    case ItemType.Pickaxe:
                        Basic.SpriteBatch.Draw(Textures.PickAxe, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                        break;
                    case ItemType.Pliers:
                        Basic.SpriteBatch.Draw(Textures.Pliers, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
		}

		public bool SetItem(Item item)
		{
			for (int i = 0; i < Slots; i++)
			{
			    if (_items[i].Type != ItemType.None) continue;

			    _items[i] = item;
			    return true;
			}

			return false;
		}

		public void RemoveSelectedItem()
		{
            _items[_selectedSlot] = new Item(ItemType.None, -1);
		}

		public void Clear()
		{
			for (int i = 0; i < _items.Length; i++)
			{
                _items[i] = new Item(ItemType.None, -1);
			}
        }

        #endregion
    }
}
