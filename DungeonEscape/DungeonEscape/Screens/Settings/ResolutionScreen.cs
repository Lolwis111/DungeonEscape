﻿using DungeonEscape.GUI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape.Screens.Settings
{
    internal class ResolutionScreen : Screen
    {
        private readonly ListBox listBox = new ListBox();

        public ResolutionScreen(int currentX, int currentY)
        {
            // TODO: find matching displaymode
        }

        public override void Init()
        {
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (mode.Width < 1024)
                    continue;

                listBox.Items.Add($"{mode.Width}x{mode.Height}");
            }
        }

        public override void Render()
        {
            listBox.Render();
        }

        public override void Update()
        {
            listBox.Update();
        }
    }
}
