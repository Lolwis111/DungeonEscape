using DungeonEscape.Content;
using DungeonEscape.Models;
using DungeonEscape.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonEscape.Entities.Block
{
    /// <summary>
    /// Implementiert einen Block welcher als Schalter für eine Tür fungiert.
    /// </summary>
    internal sealed class SwitchBlock : Entity
    {
        public bool IsActivated { get; set; }

        public int Id { get; set; }

        private GamePadState _gamePadState;

        public SwitchBlock(float x, float y, float z) : base(x, y, z)
        {

        }

        public override void Init()
        {
            IsActivated = false;
            base.Init();
        }

        public override void Update()
        {
            _gamePadState = GamePad.GetState(PlayerIndex.One);

            float? num = Box.Intersects(GameScreen.Camera.CameraRay);

            if (num.HasValue && num.Value < 1f && (GameScreen.MouseClicked 
                || _gamePadState.Buttons.A == ButtonState.Pressed && 
                GameScreen.OldGamePadState.Buttons.A == ButtonState.Released))
            {
                IsActivated = !IsActivated;

                foreach (Entity entity in GameScreen.Level.Entities)
                {
                    DoorBlock block = entity as DoorBlock;

                    if (block != null && block.Id == Id)
                    {
                        block.HasToMove = true;
                    }
                }
            }

            base.Update();
        }

        public override void Render()
        {
            Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(IsActivated ? Textures.SwitchOn : Textures.SwitchOff);

            Draw(VertexModel.BlockVertexModel);
        }

        public override string GenerateXml()
        {
            return $"<entity><type>switch</type><position>{Position.X};{Position.Y};{Position.Z}</position><id>{Id}</id></entity>";
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Block;
        }
    }
}
