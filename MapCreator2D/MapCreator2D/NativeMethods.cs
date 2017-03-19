using System.Runtime.InteropServices;

namespace MapCreator2D
{
    public sealed class NativeMethods
    {
        [DllImport("Security.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int checkLevel(int emptyCount, int blockCount, int spriteCount);
    }
}
