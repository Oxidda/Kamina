using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Contracts.Common;

namespace Kamina.Contracts.Logic
{
    public interface IWordsDataAccess
    {
        List<string> GetWords(Language language);
    }
}
