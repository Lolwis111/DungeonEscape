using System;

namespace MapCreator2D
{
#if WINDOWS || XBOX
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (Main game = new Main())
            {
                game.Run();
            }
        }
    }
#endif
}

