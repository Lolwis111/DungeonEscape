using System;
using System.Windows.Forms;
using DungeonEscape.SaveGames;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

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

                comboBoxResolution.Items.Add($"{mode.Width}x{mode.Height}");

                if (mode.Width == settings.Resolution.X && mode.Height == settings.Resolution.Y)
                    comboBoxResolution.SelectedIndex = comboBoxResolution.Items.Count - 1;
            }

            checkBoxFullscreen.Checked = settings.Fullscreen;
            trackBarVolume.Value = (int)(settings.Volume * 10);
            comboBoxTextures.SelectedIndex = settings.UseLowTextures ? 0 : 1;

            string path = Path.Combine(Environment.CurrentDirectory,
                Basic.Content.RootDirectory, "Language");

            foreach (string file in Directory.GetFiles(path))
            {
                if (string.IsNullOrEmpty(file)
                    || Path.GetExtension(file) != ".xml"
                    || Directory.Exists(file))
                {
                    continue;
                }


                string lang = Path.GetFileName(file);

                if (string.IsNullOrEmpty(lang))
                {
                    continue;
                }

                comboBoxLanguage.Items.Add(lang);
            }

            if (comboBoxLanguage.Items.Contains(GameSettings.Language))
            {
                comboBoxLanguage.SelectedIndex = comboBoxLanguage.Items.IndexOf(GameSettings.Language);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GameSettings.Fullscreen = checkBoxFullscreen.Checked;
            GameSettings.Resolution = new Resolution
            {
                X = int.Parse(comboBoxResolution.Text.Split('x')[0]),
                Y = int.Parse(comboBoxResolution.Text.Split('x')[1])
            };

            GameSettings.UseLowTextures = (comboBoxTextures.SelectedIndex == 0);

            GameSettings.Volume = (float)trackBarVolume.Value / 10;
            GameSettings.Language = comboBoxLanguage.SelectedText;

            NewSettings = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewSettings = false;            
            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
