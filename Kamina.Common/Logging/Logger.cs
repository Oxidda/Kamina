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
            isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            TryGetFileName();
            isoStream = new IsolatedStorageFileStream("Log.txt", FileMode.OpenOrCreate, isoStore);
            writer = new StreamWriter(isoStream);
            writer.AutoFlush = true;
        }

        private static string TryGetFileName()
        {
            var v = DateTime.Now.Ticks.ToString();
            var s = $"Log{v}.txt";

            if (isoStore.FileExists("Log.txt"))
            {
                if (!isoStore.FileExists(s))
                {
                    isoStore.CopyFile("Log.txt", s);
                }
                else
                {
                    return TryGetFileName();
                }
            }

            return s;
        }

        public static async Task Log(string text)
        {
            await Task.Run(() => { writer.WriteLine(text); });
        }

        private static StreamWriter writer;
        private static IsolatedStorageFileStream isoStream;
        private static IsolatedStorageFile isoStore;
    }
}
