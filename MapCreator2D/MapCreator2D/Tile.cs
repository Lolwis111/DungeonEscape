using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapCreator2D
{
    public class Tile
    {
        public Rectangle Rectangle 
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        private Rectangle _rectangle = Rectangle.Empty;

        public TileType Type 
        {
            get { return _type; }
            set 
            {
                _type = value;
                setUpTexture();
            }
        }
        private TileType _type;

        private Texture2D _texture = null;
        private bool _selected = false;

        public string Message 
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _message = string.Empty;

        public int ID 
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id = 0;

        public Tile(int x, int y, TileType type)
        {
            _rectangle = new Rectangle(x, y, 30, 30);
            _type = type;

            setUpTexture();
        }

        private void setUpTexture()
        {
            switch (_type)
            {
                case TileType.None:
                    _texture = Textures.None;
                    break;
                case TileType.Destroyable:
                    _texture = Textures.Destroyable;
                    break;
                case TileType.Door:
                    _texture = Textures.Door;
                    break;
                case TileType.Grid:
                    _texture = Textures.Grid;
                    break;
                case TileType.Key:
                    _texture = Textures.Key;
                    break;
                case TileType.LevelDown:
                    _texture = Textures.LevelDown;
                    break;
                case TileType.LevelUp:
                    _texture = Textures.LevelUp;
                    break;
                case TileType.Pickaxe:
                    _texture = Textures.Pickaxe;
                    break;
                case TileType.Pliers:
                    _texture = Textures.Pliers;
                    break;
                case TileType.Wall:
                    _texture = Textures.Wall;
                    break;
                case TileType.Spawn:
                    _texture = Textures.Spawn;
                    break;
                case TileType.Message:
                    _texture = Textures.Message;
                    break;
                case TileType.Switch:
                    _texture = Textures.Switch;
                    break;
                case TileType.Half:
                    _texture = Textures.Half;
                    break;
                default:
                    throw new FormatException();
            }
        }

        public void Update()
        {
            _selected = _rectangle.Intersects(Basic.CursorRectangle);

            if (_selected && Basic.NewMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (Basic.gameWindow.window.GetInfo) Basic.gameWindow.window.SelectedTile = this;
                else this.Type = Basic.gameWindow.window.Type;
            }
        }

        public void Render()
        {
            Basic.spriteBatch.Draw(_texture, _rectangle, _selected ? Color.Yellow : Color.White);
        }

        public void Dispose() 
        {
            _texture.Dispose();
            _texture = null;
        }
    }

    public enum TileType
    {
        None,
        Wall,
        Destroyable,
        Key,
        Pliers,
        Pickaxe,
        Message,
        Door,
        Grid,
        LevelDown,
        LevelUp,
        Spawn,
        Switch,
        Half
    }
}
