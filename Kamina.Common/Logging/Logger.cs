using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace Kamina.Common.Logging
{
    public static class Logger
    {
        public static DateTime StartTime;

        static Logger()
        {
            StartTime = DateTime.Now;
            IsoStore = IsolatedStorageFile.GetMachineStoreForAssembly();
            TryGetFileName();
            var isoStream = new IsolatedStorageFileStream("Log.txt", FileMode.OpenOrCreate, IsoStore);
            Writer = new StreamWriter(isoStream) { AutoFlush = true };
        }

        private static string TryGetFileName()
        {
            string dateTimeAsString = DateTime.Now.Ticks.ToString();
            string logLine = $"Log{dateTimeAsString}.txt";
            if (IsoStore.FileExists("Log.txt"))
            {
                if (!IsoStore.FileExists(logLine))
                {
                    IsoStore.CopyFile("Log.txt", logLine);
                }
                else
                {
                    return TryGetFileName();
                }
            }
            return logLine;
        }

        public static async Task LogAsync(string text)
        {
            await Task.Run(() => { Log(text); });
        }

        public static void Log(string text)
        {
            Writer.WriteLine(text);
        }

        private static readonly StreamWriter Writer;
        private static readonly IsolatedStorageFile IsoStore;
    }
}
