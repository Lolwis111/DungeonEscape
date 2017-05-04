using DungeonEscape.Content;
using DungeonEscape.Utils;
using Microsoft.Xna.Framework;
using DungeonEscape.GUI.Components;

namespace DungeonEscape.Screens
{
    internal class LostScreen : IScreen
    {
        private readonly Button _buttonHome = new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, LanguageStrings.BackToMenu, Home_Click)
        {
            Enabled = true,
            Visible = true
        };
        
        public void Init()
        {
            Mouse.ShowMouse();
        }

        public void Update()
        {
            _buttonHome.Update();
        }

        public void Render()
        {
            Basic.SpriteBatch.DrawString(Fonts.MainFont, 
                LanguageStrings.YouLost, 
                new Vector2(
                    (float)Basic.WindowSize.Width / 2 - Fonts.MainFont.MeasureString(LanguageStrings.YouLost).X / 2, 
                    75), 
                Color.White);

            _buttonHome.Render();
        }

        private static void Home_Click()
        {
            Basic.SetScreen(new MainMenuScreen());
        }
    }
}
