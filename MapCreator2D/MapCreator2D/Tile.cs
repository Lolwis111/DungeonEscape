using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapCreator2D
{
    public sealed class Tile : IDisposable
    {
        public Rectangle Rectangle { get; set; }

        public TileType Type 
        {
            get { return _type; }
            set 
            {
                _type = value;
                SetUpTexture();
            }
        }
        private TileType _type;

        private Texture2D _texture;
        private bool _selected;

        public string Message { get; set; }

        public int Id { get; set; }

        public Tile(int x, int y, TileType type)
        {
            Rectangle = new Rectangle(x, y, 30, 30);
            _type = type;

            SetUpTexture();
        }

        private void SetUpTexture()
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
            _selected = Rectangle.Intersects(Basic.CursorRectangle);

            if (!_selected || Basic.NewMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return;

            if (Basic.gameWindow._window.GetInfo) Basic.gameWindow._window.SelectedTile = this;
            else this.Type = Basic.gameWindow._window.Type;
        }

        public void Render()
        {
            Basic.spriteBatch.Draw(_texture, Rectangle, _selected ? Color.Yellow : Color.White);
        }

        public void Dispose() 
        {
            _texture?.Dispose();
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
