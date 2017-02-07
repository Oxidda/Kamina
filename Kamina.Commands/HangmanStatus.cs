using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace Kamina.Commands
{
    public static class HangmanStatus
    {
        static HangmanStatus()
        {
            isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            isoStream = new IsolatedStorageFileStream("Ned.txt", FileMode.OpenOrCreate, isoStore);
            reader = new StreamReader(isoStream);
            LoadWords();
            State = new Dictionary<ulong, HangManGame>();

        }

        public static void LoadWords()
        {
            words = new List<string>();
            while (!reader.EndOfStream)
            {
                words.Add(reader.ReadLine());
            }
        }

        public static string GetRandomWord()
        {
            var rand = new Random();

            var word = words[rand.Next(words.Count)];

            if (word.Contains("ĳ"))
            {
                word = word.Replace("ĳ", "ij");
            }
            if (word.Contains("ï"))
            {
                word = word.Replace("ï", "i");
            }
            return word;
        }

        private static StreamReader reader;
        private static List<string> words;
        private static IsolatedStorageFile isoStore;
        private static IsolatedStorageFileStream isoStream;
        public static Dictionary<ulong, HangManGame> State;
        private static int MaxMistakes = 10;
    }
}