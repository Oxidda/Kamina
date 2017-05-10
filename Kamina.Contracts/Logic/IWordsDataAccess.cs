using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kamina.Contracts.Logic
{
    public interface IWordsDataAccess
    {
        Task<List<string>> GetWordsAsync();
    }
}
