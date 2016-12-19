using System.Runtime.InteropServices;

namespace DungeonEscape.Security
{
    public class NativeMethods
    {
        [DllImport("Security.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int checkLevel(int emptyCount, int blockCount, int spriteCount);

        [DllImport("Security.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int checkItems(int pliers, int keys, int pickaxes, int emptys);
    }
}
