using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Common;
using Kamina.Common.Logging;
using Kamina.Contracts.Common;
using Kamina.Contracts.Logic;

namespace Kamina.DataAccess
{
    public sealed class WordsDataAccess : IWordsDataAccess
    {
        public WordsDataAccess()
        {
            _filesNamesByLanguage = new ConcurrentDictionary<Language, string>();
            _filesNamesByLanguage.TryAdd(Language.NL, "ned.txt");
        }

        public List<string> GetWords(Language language)
        {
            var words = new List<string>();
            string fileName;
            if (_filesNamesByLanguage.ContainsKey(language) && _filesNamesByLanguage.TryGetValue(language, out fileName))
            {
                Logger.Log($"Words found for {language}");
                using (var reader = new FileReader().GetFileReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        words.Add(reader.ReadLine());
                    }
                }
                return words;
            }

            Logger.Log($"No words found for {language}");
            return new List<string>();

        }

        private readonly ConcurrentDictionary<Language, String> _filesNamesByLanguage;
    }
}
