using System.Collections.Generic;
using Kamina.Common;
using Kamina.Contracts;

namespace Kamina.DataAccess
{
    public sealed class WordsDataAccess : IWordsDataAccess
    {
        public List<string> GetWords()
        {
            var words = new List<string>();

            using (var reader = new FileReader().GetFileReader("Ned.txt"))
            {
                while(!reader.EndOfStream)
                {
                    words.Add(reader.ReadLine());
                }
                return words;
            }
        }
    }
}
