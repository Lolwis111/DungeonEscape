using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonEscape.Content;
using DungeonEscape.SaveGames;
using DungeonEscape.Settings;

namespace DungeonEscape.Screens
{
    internal sealed class OptionsMenuScreen : Screen
	{
        private Savegamesettings _gameSettings;

        public OptionsMenuScreen(Savegamesettings settings)
        {
            _gameSettings = settings;
        }

        public override void Init()
        {
            SettingsForm settings = new SettingsForm(GraphicsAdapter.DefaultAdapter.SupportedDisplayModes, _gameSettings);
            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _gameSettings = settings.GameSettings;

                LanguageStrings.LoadStrings(_gameSettings.Language);

                Sounds.SetVolume(_gameSettings.Volume);
                Basic.GraphicsManager.IsFullScreen = _gameSettings.Fullscreen;
                Basic.GraphicsManager.PreferredBackBufferWidth = _gameSettings.Resolution.X;
                Basic.GraphicsManager.PreferredBackBufferHeight = _gameSettings.Resolution.Y;
                Basic.GraphicsManager.ApplyChanges();

                Basic.WindowSize = new Rectangle(0, 0, _gameSettings.Resolution.X, _gameSettings.Resolution.Y);

                Basic.UseSmallTextures = _gameSettings.UseLowTextures;

                Savegamesettings.Save(_gameSettings);
            }

            // Basic.Init(Basic.Game);
            Basic.SetScreen(new MainMenuScreen());
        }
    }
}
