using System;

namespace DungeonEscape.SaveGames
{
    [Serializable]
    public sealed class LevelNotFoundException : Exception
    {
        public LevelNotFoundException()
        {
        }

        public LevelNotFoundException(string message) : base(message)
        {
        }

        public LevelNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
