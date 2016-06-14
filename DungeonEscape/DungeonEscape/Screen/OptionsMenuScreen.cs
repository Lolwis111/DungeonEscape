using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace DungeonEscape
{
	public sealed class OptionsMenuScreen : IScreen
	{
        private Savegamesettings gameSettings = new Savegamesettings();

        public OptionsMenuScreen(Savegamesettings settings)
        {
            this.gameSettings = settings;
        }

        public void Init()
        {
            Settings settings = new Settings(GraphicsAdapter.DefaultAdapter.SupportedDisplayModes, gameSettings);
            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gameSettings = settings.GameSettings;

                Sounds.SetVolume(gameSettings.Volume);
                Basic.GraphicsManager.IsFullScreen = gameSettings.Fullscreen;
                Basic.GraphicsManager.PreferredBackBufferWidth = gameSettings.Resolution.X;
                Basic.GraphicsManager.PreferredBackBufferHeight = gameSettings.Resolution.Y;
                Basic.GraphicsManager.ApplyChanges();

                Basic.WindowSize = new Rectangle(0, 0, gameSettings.Resolution.X, gameSettings.Resolution.Y);

                Basic.UseSmallTextures = gameSettings.UseLowTextures;

                Savegamesettings.Save(gameSettings);
            }

            Basic.Init(Basic.Game);
            Basic.setScreen(new MainMenuScreen());
        }

        public void Update()
        {
        }

        public void Render()
        {

        }
    }
}
