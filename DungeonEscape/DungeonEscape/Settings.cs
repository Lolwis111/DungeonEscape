using System;
using System.Windows.Forms;
using DungeonEscape.SaveGames;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    internal sealed partial class Settings : Form
    {
        public  Savegamesettings GameSettings;
        public bool NewSettings;

        public Settings(DisplayModeCollection displayModes, Savegamesettings settings)
        {
            InitializeComponent();

            GameSettings = settings;

            foreach (DisplayMode mode in displayModes)
            {
                if (mode.Height < 768 || mode.Width < 1024) continue;

                comboResolution.Items.Add($"{mode.Width}x{mode.Height}");

                if (mode.Width == settings.Resolution.X && mode.Height == settings.Resolution.Y)
                    comboResolution.SelectedIndex = comboResolution.Items.Count - 1;
            }

            checkFullscreen.Checked = settings.Fullscreen;
            trackVolume.Value = (int)(settings.Volume * 10);
            comboTextures.SelectedIndex = settings.UseLowTextures ? 0 : 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GameSettings.Fullscreen = checkFullscreen.Checked;
            GameSettings.Resolution = new Resolution
            {
                X = int.Parse(comboResolution.Text.Split('x')[0]),
                Y = int.Parse(comboResolution.Text.Split('x')[1])
            };

            GameSettings.UseLowTextures = (comboTextures.SelectedIndex == 0);

            GameSettings.Volume = (float)trackVolume.Value / 10;

            NewSettings = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewSettings = false;            
            Close();
        }
    }
}
