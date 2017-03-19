using System;
using System.Threading;

#if !DEBUG

using DungeonEscape.Debug;

#endif

namespace DungeonEscape
{
    internal static class Program
    {
        private const string MutexId = "DungeonEscape";

        [STAThread]
        private static void Main(string[] args)
        {
            Mutex mutexInstance;

            try
            {
                mutexInstance = Mutex.OpenExisting(MutexId);
                Environment.Exit(0);
            }
            catch
            {
                mutexInstance = new Mutex(true, MutexId);
            }

#if !DEBUG
            try
            {
#endif
                using (GameMain game = new GameMain(args))
                {
                    game.Run();
                }
#if !DEBUG
        }
            catch (Exception exception)
            {
                LogWriter.WriteError(exception);
            }
#endif

            mutexInstance.ReleaseMutex();
        }
    }
}

