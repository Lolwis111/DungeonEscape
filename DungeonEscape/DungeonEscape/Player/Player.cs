using DungeonEscape.Content;
using Microsoft.Xna.Framework;
using DungeonEscape.GUI;
using DungeonEscape.Screens;

namespace DungeonEscape.Player
{
    internal sealed class Player
	{
	    public ItemBar PlayerItemBar { get; set; }

	    public Player()
		{
            PlayerItemBar = new ItemBar();
		}

		public void Update()
		{
            PlayerItemBar.Update();
		}

		public void Render()
		{
            Basic.SpriteBatch.DrawString(Fonts.MainFont, GameScreen.Level.Name, new Vector2(5f, 5f), Color.White);

            PlayerItemBar.Render();
		}
	}
}
