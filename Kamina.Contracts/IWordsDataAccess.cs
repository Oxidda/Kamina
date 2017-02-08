using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kamina.Contracts
{
    public interface IWordsDataAccess
    {
        Task<List<string>> GetWordsAsync();
    }
}
