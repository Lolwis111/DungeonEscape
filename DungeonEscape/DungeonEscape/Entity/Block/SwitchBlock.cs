using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape
{
    /// <summary>
    /// Implementiert einen Block welcher als Schalter für eine Tür fungiert.
    /// </summary>
    public sealed class SwitchBlock : Entity
    {
        public bool IsActivated
        {
            get { return _active; }
            set { _active = value; }
        }
        private bool _active = false;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id = 0;

        private GamePadState gamePadState;

        public SwitchBlock(float x, float y, float z) : base(x, y, z)
        {

        }

        public override void Init()
        {
            _active = false;
            base.Init();
        }

        public override void Update()
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);

            float? num = Box.Intersects(GameScreen.Camera.CameraRay);

            if (num.HasValue && num.Value < 1f && ((Mouse.GetState().LeftButton == ButtonState.Pressed && 
                GameScreen.OldMouseState.LeftButton == ButtonState.Released) || (gamePadState.Buttons.A == ButtonState.Pressed && 
                GameScreen.OldGamePadState.Buttons.A == ButtonState.Released)))
            {
                _active = !_active;

                for (int i = 0; i < GameScreen.Level.Entities.Count; i++)
                {
                    if (GameScreen.Level.Entities[i] is DoorBlock && ((DoorBlock)GameScreen.Level.Entities[i]).ID == _id)
                    {
                        ((DoorBlock)GameScreen.Level.Entities[i]).HasToMove = true;
                    }
                }
            }

            base.Update();
        }

        public override void Render()
        {
            GameScreen.MainEffect.Parameters["DiffuseTexture"].SetValue(_active ? Textures.SwitchOn : Textures.SwitchOff);

            base.Draw(Model.BlockModel);
        }
    }
}
