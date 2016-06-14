using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Content
{
    public class Adapter
    {
        #region Fields

        public int ScreenWidth { get { return _screenWidth; } }
        private int _screenWidth = 0;

        public int ScreenHeight { get { return _screenHeight; } }
        private int _screenHeight = 0;

        public float AspectRatio { get { return _aspectRatio; } }
        private float _aspectRatio = 0.0f;

        public string Description { get { return _description; } }
        private string _description = "";

        public int ID { get { return _id; } }
        private int _id = 0;

        public string Name { get { return _name; } }
        private string _name = "";

        #endregion

        public static Adapter GetAdapter()
        {
            GraphicsAdapter adapter = GraphicsAdapter.DefaultAdapter;

            return new Adapter() { 
                _aspectRatio = adapter.CurrentDisplayMode.AspectRatio, 
                _description = adapter.Description,
                _id = adapter.DeviceId,
                _name = adapter.DeviceName,
                _screenHeight = adapter.CurrentDisplayMode.Height,
                _screenWidth = adapter.CurrentDisplayMode.Width 
            };
        }
    }
}
