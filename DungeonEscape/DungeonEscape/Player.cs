using Microsoft.Xna.Framework;
using System;

namespace DungeonEscape
{
    public sealed class Player
	{
		public ItemBar PlayerItemBar
		{
            get { return _playerItemBar; }
            set { _playerItemBar = value; }
		}
        private ItemBar _playerItemBar = new ItemBar();

		public Player()
		{
            _playerItemBar = new ItemBar();
		}

		public void Update()
		{
            _playerItemBar.Update();
		}

		public void Render()
		{
            Basic.SpriteBatch.DrawString(Basic.MainFont, GameScreen.Level.Name, new Vector2(5f, 5f), Color.White);

            _playerItemBar.Render();
		}
	}
}
