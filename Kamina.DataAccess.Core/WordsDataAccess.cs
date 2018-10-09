using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Kamina.Common.Core;
using Kamina.Common.Core.Logging;
using Kamina.Contracts.Core.Common;
using Kamina.Contracts.Core.Logic;

namespace Kamina.DataAccess.Core
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
            try
            {
                var words = new List<string>();
                string fileName;
                if (_filesNamesByLanguage.ContainsKey(language) &&
                    _filesNamesByLanguage.TryGetValue(language, out fileName))
                {
                    Logger.Log($"Words found for {language}");
                    using (var reader = new FileReader().GetFileReader(fileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            words.Add(reader.ReadLine());
                        }
                    }

                    Logger.Log($"Words {words}");
                    return words;
                }

                Logger.Log($"No words found for {language}");
                return new List<string>();
            }
            catch (Exception x)
            {
                Logger.Log("Exception WordAccess : " + x.ToString());
                throw;
            }
        }

        private readonly ConcurrentDictionary<Language, String> _filesNamesByLanguage;
    }
}
