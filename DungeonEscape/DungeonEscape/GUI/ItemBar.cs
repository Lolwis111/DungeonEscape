using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace DungeonEscape
{
	public sealed class ItemBar
    {
        #region Fields

        const int Slots = 6;

		public int SelectedSlot
		{
            get { return _selectedSlot; }
            set { _selectedSlot = value; }
		}
        private int _selectedSlot = 0;

		public Item[] Items
		{
            get { return _items; }
            set { _items = value; }
		}
        private Item[] _items = new Item[Slots];

        public Item SelectedItem
		{
            get { return _selectedItem; }
            set { _selectedItem = value; }
		}
        private Item _selectedItem = new Item();

        #endregion

        #region Methods

        public ItemBar()
		{
            _items = new Item[Slots];
            Clear();

            _selectedSlot = 0;
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
                if (i == _selectedSlot)
                {
                    Basic.SpriteBatch.Draw(Textures.SelectedItemBarItem, new Rectangle(Basic.WindowSize.Width - 105, 10 + i * 100, 100, 100), Color.White);
                }
                else
                {
                    Basic.SpriteBatch.Draw(Textures.ItemBarItem, new Rectangle(Basic.WindowSize.Width - 105, 10 + i * 100, 100, 100), Color.White);
                }

                if (_items[i].Type != ItemType.None)
                {
                    if (_items[i].Type == ItemType.Key)
                    {
                        Basic.SpriteBatch.Draw(Textures.Key, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                    }
                    else if (_items[i].Type == ItemType.Pickaxe)
                    {
                        Basic.SpriteBatch.Draw(Textures.PickAxe, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                    }
                    else if (_items[i].Type == ItemType.Pliers)
                    {
                        Basic.SpriteBatch.Draw(Textures.Pliers, new Rectangle(Basic.WindowSize.Width - 100, 15 + i * 100, 90, 90), Color.White);
                    }
                }
            }
		}

		public bool SetItem(Item item)
		{
			for (int i = 0; i < Slots; i++)
			{
				if (_items[i].Type == ItemType.None)
				{
                    _items[i] = item;
					return true;
				}
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

    public struct Item
    {
        public ItemType Type;
        public int ID;

        public Item(ItemType type, int id)
        {
            ID = id;
            Type = type;
        }
    }

    public enum ItemType
    {
        Key,
        Pliers,
        Pickaxe,
        Message,
        None
    }
}
