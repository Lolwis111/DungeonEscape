using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DungeonEscape.Screens;
using DungeonEscape.Content;
using DungeonEscape.GUI.Items;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using DungeonEscape.Entities;

namespace DungeonEscape.GUI
{
    internal sealed class EditorItemBar
    {
        #region Fields

        private const int Slots = 6;

        public int SelectedSlot { get; set; }

        public Entity[] Entities { get; set; }

        public Entity SelectedEntity { get; set; }

        #endregion

        #region Methods

        public EditorItemBar()
        {
            Entities = new Entity[Slots];
            

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

            SelectedEntity = Entities[SelectedSlot];
        }

        public void Render()
        {
            for (int i = 0; i < Slots; i++)
            {
                Basic.SpriteBatch.Draw(i == SelectedSlot ? Textures.SelectedItemBarItem : Textures.ItemBarItem, new Rectangle(Basic.WindowSize.Width - 105, 5 + i * 50, 50, 50), Color.White);

                switch (Entities[i].EntityType)
                {
                    case EntityType.Undefined:
                        continue;
                    case EntityType.DestroyAbleBlock:
                        Basic.SpriteBatch.Draw(Textures.DestroyableBlock, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.DoorBlock:
                        Basic.SpriteBatch.Draw(Textures.Door, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.GridBlock:
                        Basic.SpriteBatch.Draw(Textures.Grid, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.HalfBlock:
                        Basic.SpriteBatch.Draw(Textures.Wall, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 25, 50), Color.White);
                        break;
                    case EntityType.KeySprite:
                        Basic.SpriteBatch.Draw(Textures.Key, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.LevelDownSprite:
                        Basic.SpriteBatch.Draw(Textures.LevelDown, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.LevelUpSprite:
                        Basic.SpriteBatch.Draw(Textures.LevelUp, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.MessageSprite:
                        Basic.SpriteBatch.Draw(Textures.Message, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.PickAxeSprite:
                        Basic.SpriteBatch.Draw(Textures.PickAxe, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.PliersSprite:
                        Basic.SpriteBatch.Draw(Textures.Pliers, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.SwitchBlock:
                        Basic.SpriteBatch.Draw(Textures.SwitchOn, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    case EntityType.WallBlock:
                        Basic.SpriteBatch.Draw(Textures.Wall, new Rectangle(Basic.WindowSize.Width - 100, 10 + i * 50, 50, 50), Color.White);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion
    }
}
