using System;
using System.Runtime.Serialization;

namespace DungeonEscape.Exceptions
{
    [Serializable]
    internal sealed class InvalidChecksumException : Exception
    {
        public InvalidChecksumException()
        {
        }

        public InvalidChecksumException(string message) : base(message)
        {
        }

        public InvalidChecksumException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
