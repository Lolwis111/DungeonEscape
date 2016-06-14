using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    public partial class Settings : Form
    {
        private List<DisplayMode> modes = new List<DisplayMode>();
        public  Savegamesettings GameSettings = new Savegamesettings();
        public bool NewSettings = false;

        public Settings(DisplayModeCollection displayModes, Savegamesettings settings)
        {
            InitializeComponent();

            GameSettings = settings;

            foreach (DisplayMode mode in displayModes)
            {
                if (mode.Height >= 768 && mode.Width >= 1024)
                {
                    modes.Add(mode);
                    comboResolution.Items.Add(string.Format("{0}x{1}", mode.Width, mode.Height));
                    if (mode.Width == settings.Resolution.X && mode.Height == settings.Resolution.Y)
                        comboResolution.SelectedIndex = comboResolution.Items.Count - 1;
                }
            }

            checkFullscreen.Checked = settings.Fullscreen;
            trackVolume.Value = (int)(settings.Volume * 10);
            if (settings.UseLowTextures)
                comboTextures.SelectedIndex = 0;
            else
                comboTextures.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GameSettings.Fullscreen = checkFullscreen.Checked;
            GameSettings.Resolution = new Resolution() { X = int.Parse(comboResolution.Text.Split('x')[0]), Y = int.Parse(comboResolution.Text.Split('x')[1]) };

            if (comboTextures.SelectedIndex == 0)
                GameSettings.UseLowTextures = true;
            else
                GameSettings.UseLowTextures = false;

            GameSettings.Volume = trackVolume.Value / 10;

            NewSettings = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewSettings = false;
            this.Close();
        }
    }
}
