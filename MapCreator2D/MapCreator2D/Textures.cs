using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapCreator2D
{
    public static class Textures
    {
        public static Texture2D Wall = null;
        public static Texture2D Destroyable = null;
        public static Texture2D Key = null;
        public static Texture2D Pickaxe = null;
        public static Texture2D Pliers = null;
        public static Texture2D Door = null;
        public static Texture2D Grid = null;
        public static Texture2D Message = null;
        public static Texture2D LevelDown = null;
        public static Texture2D LevelUp = null;
        public static Texture2D None = null;
        public static Texture2D Spawn = null;
        public static Texture2D Switch = null;
        public static Texture2D Half = null;

        public static void LoadTextures()
        {
            Wall = loadTexture("Blocks/Wall");
            Destroyable = loadTexture("Blocks/destroyableBlock");

            Switch = loadTexture("Blocks/switchOff");
            Half = loadTexture("Blocks/halfBlock");

            Key = loadTexture("Items/key");
            Pliers = loadTexture("Items/pliers");
            Pickaxe = loadTexture("Items/pickaxe");
            Message = loadTexture("Items/message");

            Door = loadTexture("Sprites/door");
            Grid = loadTexture("Sprites/grid");
            LevelDown = loadTexture("Sprites/levelDown");
            LevelUp = loadTexture("Sprites/levelUp");

            None = loadTexture("none");
            Spawn = loadTexture("spawn");
        }

        private static Texture2D loadTexture(string name)
        {
            return Basic.Content.Load<Texture2D>("Textures/" + name);
        }

        public static void DisposeTextures()
        {
            if (Wall != null)
            {
                Wall.Dispose();
                Wall = null;
            }

            if (Destroyable != null)
            {
                Destroyable.Dispose();
                Destroyable = null;
            }

            if (Key != null)
            {
                Key.Dispose();
                Key = null;
            }

            if (Pickaxe != null)
            {
                Pickaxe.Dispose();
                Pickaxe = null;
            }

            if (Pliers != null)
            {
                Pliers.Dispose();
                Pliers = null;
            }

            if (Door != null)
            {
                Door.Dispose();
                Door = null;
            }

            if (Grid != null)
            {
                Grid.Dispose();
                Grid = null;
            }

            if (LevelDown != null)
            {
                LevelDown.Dispose();
                LevelDown = null;
            }

            if (LevelUp != null)
            {
                LevelUp.Dispose();
                LevelUp = null;
            }

            if (None != null)
            {
                None.Dispose();
                None = null;
            }

            if (Spawn != null)
            {
                Spawn.Dispose();
                Spawn = null;
            }

            if (Message != null)
            {
                Message.Dispose();
                Message = null;
            }

            if (Switch != null)
            {
                Switch.Dispose();
                Switch = null;
            }

            if (Half != null)
            {
                Half.Dispose();
                Half = null;
            }
        }
    }
}
