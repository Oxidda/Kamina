using System.Collections.Generic;

namespace Kamina.Contracts
{
    public interface IWordsDataAccess
    {
        List<string> GetWords();
    }
}
