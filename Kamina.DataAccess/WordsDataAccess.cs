using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Common;
using Kamina.Contracts;

namespace Kamina.DataAccess
{
    public sealed class WordsDataAccess : IWordsDataAccess
    {
        public Task<List<string>> GetWordsAsync()
        {
            return Task.Run(() =>
            {
                var words = new List<string>();

                using (var reader = new FileReader().GetFileReader("Ned.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        words.Add(reader.ReadLine());
                    }
                }
                return words;
            });
        }
    }
}
