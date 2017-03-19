using DungeonEscape.Content;
using Microsoft.Xna.Framework;
using DungeonEscape.GUI;
using DungeonEscape.Screens;

namespace DungeonEscape
{
    internal sealed class Player
	{
	    public ItemBar PlayerItemBar
	    {
	        get { return _playerItemBar; }
	        set { _playerItemBar = value; }
	    }

	    private ItemBar _playerItemBar;

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
            Basic.SpriteBatch.DrawString(Fonts.MainFont, GameScreen.Level.Name, new Vector2(5f, 5f), Color.White);

            _playerItemBar.Render();
		}
	}
}
