using Microsoft.Xna.Framework;
using DungeonEscape.GUI;
using DungeonEscape.Screens;

namespace DungeonEscape
{
    public sealed class Player
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
            Basic.SpriteBatch.DrawString(Basic.MainFont, GameScreen.Level.Name, new Vector2(5f, 5f), Color.White);

            PlayerItemBar.Render();
		}
	}
}
