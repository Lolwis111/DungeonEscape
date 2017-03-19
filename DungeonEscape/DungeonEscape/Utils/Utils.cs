using System;
using System.Xml;

namespace DungeonEscape.Utils
{
    public sealed class Utils
    {
        private const double CompareAccuracy = 1E-10;

        public static bool CompareFloats(float a, float b)
        {
            return Math.Abs(a - b) < CompareAccuracy;
        }

        public static bool CompareFloats(double a, double b)
        {
            return Math.Abs(a - b) < CompareAccuracy;
        }

        public static XmlNode SaveSelectSingleNode(XmlNode root, string xpath)
        {
            if (root != null)
                return root.SelectSingleNode(xpath);

            throw new NullReferenceException();
        }
    }
}
