using System;
using System.Threading;
using DungeonEscape.Debug;

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
                // only allow instance
                mutexInstance = Mutex.OpenExisting(MutexId);
                Environment.Exit(0);
            }
            catch
            {
                mutexInstance = new Mutex(true, MutexId);
            }

            /*try
            { */
                using (GameMain game = new GameMain(args))
                {
                    game.Run();
                }
            /*}
            catch (Exception exception)
            {
                LogWriter.WriteError(exception);
            }*/

            mutexInstance.ReleaseMutex();
        }
    }
}

