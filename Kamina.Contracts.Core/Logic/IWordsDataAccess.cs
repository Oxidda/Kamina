using System.Collections.Generic;
using Kamina.Contracts.Core.Common;

namespace Kamina.Contracts.Core.Logic
{
    public interface IWordsDataAccess
    {
        List<string> GetWords(Language language);
    }
}
