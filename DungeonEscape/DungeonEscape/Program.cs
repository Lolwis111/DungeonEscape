using System;
using System.Threading;

namespace DungeonEscape
{
    static class Program
    {
        const string MutexID = "DungeonEscape";

        [STAThread]
        static void Main(string[] args)
        {
            Mutex mutexInstance = null;

            try
            {
                mutexInstance = Mutex.OpenExisting(MutexID);
                Environment.Exit(0);
            }
            catch
            {
                mutexInstance = new Mutex(true, MutexID);
            }

            using (GameMain game = new GameMain(args))
            {
                game.Run();
            }

            mutexInstance.ReleaseMutex();
        }
    }
}

