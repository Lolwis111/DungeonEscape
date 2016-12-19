using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.GUI.Items;

namespace DungeonEscape.GUI
{
	public sealed class ItemBar
    {
        #region Fields

        private const int Slots = 6;

        public int SelectedSlot { get; set; }

        public Item[] Items { get; set; }

        public Item SelectedItem { get; set; }

        #endregion

        #region Methods

        public ItemBar()
		{
            Items = new Item[Slots];
            Clear();

            SelectedSlot = 0;
		}

		public void Update()
		{
			MouseState state = Mouse.GetState();

			if (state.ScrollWheelValue < GameScreen.OldMouseState.ScrollWheelValue)
			{
				SelectedSlot++;
			}
			else if (state.ScrollWheelValue > GameScreen.OldMouseState.ScrollWheelValue)
		    {
                SelectedSlot--;
		    }

            if (SelectedSlot < 0)
            {
                SelectedSlot = Slots - 1;
            }
            else if (SelectedSlot >= Slots)
            {
                SelectedSlot = 0;
            }

            SelectedItem = Items[SelectedSlot];
		}

		public void Render()
		{
            for (int i = 0; i < Slots; i++)
            {
                Basic.SpriteBatch.Draw(i == SelectedSlot ? Textures.SelectedItemBarItem : Textures.ItemBarItem, 
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
			    if (Items[i].Type != ItemType.None) continue;

			    Items[i] = item;
			    return true;
			}

			return false;
		}

		public void RemoveSelectedItem()
		{
            Items[SelectedSlot] = new Item(ItemType.None, -1);
		}

		public void Clear()
		{
			for (int i = 0; i < Items.Length; i++)
			{
                Items[i] = new Item(ItemType.None, -1);
			}
        }

        #endregion
    }
}
