using Microsoft.Xna.Framework;

namespace DungeonEscape.GUI.Components
{
    internal abstract class Component
    {
        public abstract void Update();
        public abstract void Render();

        public int PositionX
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        public int PositionY
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        protected Rectangle _rect;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }
        private bool _enabled;

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        private bool _visible;
    }
}
